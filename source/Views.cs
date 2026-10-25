using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
static class Theme {
 public static readonly Brush Blue=GlassTheme.Brush("Accent"),Ink=GlassTheme.Brush("Ink"),Muted=GlassTheme.Brush("Muted"),Bg=GlassTheme.Brush("Background"),Pale=GlassTheme.Brush("Hover"),Line=GlassTheme.Brush("Line"),White=GlassTheme.Brush("Panel");
 public static Brush B(string color){Brush mapped=GlassTheme.Alias(color);if(mapped!=null)return mapped;var b=(SolidColorBrush)new BrushConverter().ConvertFromString(color);b.Freeze();return b;}
 public static TextBlock Text(string text,double size=13,Brush color=null,bool bold=false){return new TextBlock{Text=text,FontSize=size,Foreground=color??Ink,FontWeight=bold?FontWeights.SemiBold:FontWeights.Normal,VerticalAlignment=VerticalAlignment.Center,TextWrapping=TextWrapping.Wrap};}
 public static Button Button(string text,bool primary=false){return new Button{Content=text,Style=(Style)Application.Current.FindResource(primary?(object)"PrimaryButton":typeof(Button)),Padding=new Thickness(15,8,15,8),Margin=new Thickness(0,0,8,0),Cursor=System.Windows.Input.Cursors.Hand};}
 public static Border Card(UIElement content,Thickness? padding=null){return new GlassCard{Background=White,BorderBrush=Line,BorderThickness=new Thickness(1),CornerRadius=new CornerRadius(22),Padding=padding??new Thickness(16),Child=content};}
 public static void Window(Window w,bool dark=false){GlassChrome.Attach(w,dark);Transitions.Attach(w,w is MainWindow?Transitions.StartupMilliseconds:Transitions.DialogMilliseconds);w.FontFamily=new FontFamily("Microsoft YaHei UI");w.FontSize=13;w.Foreground=Ink;w.Background=Bg;w.UseLayoutRounding=true;w.SnapsToDevicePixels=true;TextOptions.SetTextFormattingMode(w,TextFormattingMode.Display);TextOptions.SetTextRenderingMode(w,TextRenderingMode.ClearType);}
 public static void Install(){GlassTheme.Install();}
 public static void Capture(FrameworkElement visual,string path,double dpi=96){visual.UpdateLayout();var bitmap=new System.Windows.Media.Imaging.RenderTargetBitmap((int)Math.Ceiling(visual.ActualWidth*dpi/96),(int)Math.Ceiling(visual.ActualHeight*dpi/96),dpi,dpi,PixelFormats.Pbgra32);bitmap.Render(visual);var encoder=new System.Windows.Media.Imaging.PngBitmapEncoder();encoder.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(bitmap));using(var f=File.Create(path))encoder.Save(f);}
 public static string CompileAdvice(string s,bool ok){return (ok?"编译成功。":"请根据 answer.c:行号 定位错误。")+"\n\n"+Diagnostics.Explain(s);}
 public static string Rules="随机正确性 60 分：20 组随机测试通过比例。\n边界正确性 20 分：8 组边界测试通过比例。\n运行质量 10 分：正确、正常退出、无标准错误且 CPU ≤250ms 的用例比例。\n简洁度 10 分：有效代码行数与有效大小各 5 分。\n\n与本题参考答案比较：行数不超过 1.6 倍（至少 12 行）、大小不超过 1.8 倍（至少 220 B）不扣分；超出后线性扣分，到阈值两倍计零。注释和空白不计大小；行数取有效文本行与语句计数的较大值，避免压成一行规避。简洁度再乘测试通过率。\n\n总分统计为每题历史最高分之和，共 90×100=9000 分。\n\n整数精确比较，浮点绝对误差 ≤0.0001，文本区分大小写；每组 3 秒 / 256 MiB。编译失败计 0 分。\n函数、指针等题目的指定写法用于学习引导，自动评测主要检查输出行为。简洁度是近似指标，不替代人工代码评审。";
}
sealed class CaseView {
 public int Number{get;set;}public string Kind{get;set;}public string State{get;set;}public string Time{get;set;}public string Cpu{get;set;}public CaseResult Case{get;set;}
}
sealed class ResultsWindow : Window {
 Evaluation evaluation;Problem problem;DataGrid grid;TextBox detail;CheckBox failures;
 public ResultsWindow(Evaluation ev,Problem p){evaluation=ev;problem=p;Title="评测结果与报告 · "+p.Title;Width=1060;Height=740;MinWidth=780;MinHeight=540;WindowStartupLocation=WindowStartupLocation.CenterOwner;Theme.Window(this);
  var layout=new Grid();layout.RowDefinitions.Add(new RowDefinition{Height=GridLength.Auto});layout.RowDefinitions.Add(new RowDefinition{Height=new GridLength(1,GridUnitType.Star)});layout.RowDefinitions.Add(new RowDefinition{Height=GridLength.Auto});Content=new Border{Background=Theme.Bg,Padding=new Thickness(22),Child=layout};
  var header=new StackPanel{Margin=new Thickness(0,0,0,16)};header.Children.Add(Theme.Text(p.Id.ToString("00")+"  "+p.Title,21,Theme.Ink,true));header.Children.Add(Theme.Text(ev.Score.ToString("0.#")+" / 100    随机 "+ev.NormalScore.ToString("0.#")+" / 60    边界 "+ev.EdgeScore.ToString("0.#")+" / 20    运行质量 "+ev.QualityScore.ToString("0.#")+" / 10    简洁度 "+(ev.LineScore+ev.SizeScore).ToString("0.#")+" / 10",14,Theme.Blue,true));header.Children.Add(Theme.Text("随机种子 "+ev.Seed+"  ·  "+ev.Time,11,Theme.Muted));layout.Children.Add(header);
  var tabs=new TabControl{BorderBrush=Theme.Line,Background=Theme.White};Grid.SetRow(tabs,1);layout.Children.Add(tabs);
  var dataTab=new TabItem{Header="测试数据与诊断"};tabs.Items.Add(dataTab);GlassMotion.WatchTabs(tabs);var data=new Grid{Margin=new Thickness(12)};data.RowDefinitions.Add(new RowDefinition{Height=GridLength.Auto});data.RowDefinitions.Add(new RowDefinition{Height=new GridLength(1,GridUnitType.Star)});dataTab.Content=data;
  failures=new CheckBox{Content="只看失败用例",Margin=new Thickness(0,0,0,10)};failures.Checked+=(s,e)=>Populate();failures.Unchecked+=(s,e)=>Populate();data.Children.Add(failures);
  var panes=new Grid();panes.ColumnDefinitions.Add(new ColumnDefinition{Width=new GridLength(.9,GridUnitType.Star)});panes.ColumnDefinitions.Add(new ColumnDefinition{Width=new GridLength(10)});panes.ColumnDefinitions.Add(new ColumnDefinition{Width=new GridLength(1.1,GridUnitType.Star)});Grid.SetRow(panes,1);data.Children.Add(panes);
  grid=new DataGrid{IsReadOnly=true,CanUserAddRows=false,CanUserDeleteRows=false,AutoGenerateColumns=false,SelectionMode=DataGridSelectionMode.Single,SelectionUnit=DataGridSelectionUnit.FullRow};
  foreach(var pair in new[]{new[]{"编号","Number"},new[]{"类型","Kind"},new[]{"结果","State"},new[]{"耗时 ms","Time"}})grid.Columns.Add(new DataGridTextColumn{Header=pair[0],Binding=new System.Windows.Data.Binding(pair[1]),Width=new DataGridLength(1,DataGridLengthUnitType.Star)});grid.SelectionChanged+=(s,e)=>ShowCase();panes.Children.Add(grid);
  var splitter=new GridSplitter{Width=5,HorizontalAlignment=HorizontalAlignment.Center,VerticalAlignment=VerticalAlignment.Stretch,Background=Theme.Line};Grid.SetColumn(splitter,1);panes.Children.Add(splitter);
  detail=ReadBox();Grid.SetColumn(detail,2);panes.Children.Add(detail);
  var metrics=new TabItem{Header="简洁度评分"};metrics.Content=ReadBox(CodeMetrics.Describe(ev));tabs.Items.Add(metrics);
  var compile=new TabItem{Header="编译信息"};compile.Content=ReadBox(Theme.CompileAdvice(ev.Compiler,ev.Compiled));tabs.Items.Add(compile);
  var report=new TabItem{Header="完整报告与代码"};report.Content=ReadBox(Engine.Report(ev));tabs.Items.Add(report);
  var actions=new DockPanel{Margin=new Thickness(0,14,0,0)};Grid.SetRow(actions,2);layout.Children.Add(actions);var close=Theme.Button("关闭");DockPanel.SetDock(close,Dock.Right);actions.Children.Add(close);close.Click+=(s,e)=>Close();var export=Theme.Button("导出 TXT 报告",true);DockPanel.SetDock(export,Dock.Right);actions.Children.Add(export);export.Click+=(s,e)=>ExportReport();actions.Children.Add(Theme.Text("可拖动中间分隔条调整数据与详情宽度",11,Theme.Muted));Populate();if(!ev.Compiled)tabs.SelectedItem=compile;
 }
 TextBox ReadBox(string text=""){return new TextBox{Text=text,IsReadOnly=true,AcceptsReturn=true,VerticalContentAlignment=VerticalAlignment.Top,TextWrapping=TextWrapping.NoWrap,VerticalScrollBarVisibility=ScrollBarVisibility.Auto,HorizontalScrollBarVisibility=ScrollBarVisibility.Auto,FontFamily=new FontFamily("Consolas, Microsoft YaHei UI"),FontSize=13,Background=Theme.White,Padding=new Thickness(12),BorderBrush=Theme.Line};}
 void Populate(){if(grid==null)return;var rows=evaluation.Cases.Select((c,i)=>new CaseView{Number=i+1,Kind=c.Test.Edge?"边界":"随机",State=c.Passed?"通过":"失败",Time=c.Run.Millis.ToString("0.0"),Cpu=c.Run.CpuMillis.ToString("0.0"),Case=c}).Where(x=>failures.IsChecked!=true||!x.Case.Passed).ToList();grid.ItemsSource=rows;grid.SelectedItem=rows.FirstOrDefault(x=>!x.Case.Passed)??rows.FirstOrDefault();if(rows.Count==0)detail.Text=failures.IsChecked==true?"当前没有失败用例。":"尚未执行测试。";}
 void ShowCase(){var v=grid.SelectedItem as CaseView;if(v==null)return;var c=v.Case;detail.Text="输入：\n"+c.Test.Input.TrimEnd('\r','\n')+"\n\n预期输出：\n"+c.Test.Expected+"\n\n实际输出：\n"+(c.Run.Output==""?"（无输出）":c.Run.Output.TrimEnd('\r','\n'))+"\n\n诊断：\n"+c.Diagnosis+(c.Passed?"":"\n题目提示："+problem.Hint)+"\n\n墙钟："+v.Time+" ms\nCPU："+v.Cpu+" ms\n退出码："+c.Run.ExitCode+"\n标准错误："+(c.Run.Error==""?"（无）":Diagnostics.Explain(c.Run.Error));}
 public void ExportReport(){var dialog=new Microsoft.Win32.SaveFileDialog{Filter="评测报告 (*.txt)|*.txt",FileName=problem.Id.ToString("00")+"_评测报告.txt"};if(dialog.ShowDialog(this)==true)try{File.WriteAllText(dialog.FileName,Engine.Report(evaluation),new UTF8Encoding(true));}catch(Exception ex){AppDialog.Show(this,ex.Message,"导出失败");}}
}
sealed class Celebration : Canvas {
 readonly Random random=new Random();TextBlock score;ScaleTransform scale;DispatcherTimer finish;List<Rectangle> ribbons=new List<Rectangle>();public bool IsAnimating{get;private set;}public double DurationMilliseconds{get{return 1000;}}
 public Celebration(){IsHitTestVisible=false;ClipToBounds=true;Visibility=Visibility.Hidden;}
 public void Play(double points){if(ActualWidth<1)return;if(finish!=null)finish.Stop();Children.Clear();ribbons.Clear();Visibility=Visibility.Visible;IsAnimating=true;
  double center=ActualWidth*.7;Brush[] colors={Theme.Blue,Theme.B("#66BEFF"),Theme.B("#97CFFE"),Theme.B("#71DFD5"),Theme.B("#FFC16F")};
  for(int i=0;i<44;i++){var ribbon=new Rectangle{Width=4+random.Next(4),Height=7+random.Next(7),Fill=colors[i%colors.Length],RadiusX=1,RadiusY=1,Opacity=0};SetLeft(ribbon,center);SetTop(ribbon,ActualHeight*.75);var move=new TranslateTransform();var rotate=new RotateTransform(random.Next(360));var group=new TransformGroup();group.Children.Add(rotate);group.Children.Add(move);ribbon.RenderTransform=group;Children.Add(ribbon);ribbons.Add(ribbon);double x=(random.NextDouble()-.5)*ActualWidth*.75,y=-35-random.NextDouble()*ActualHeight*.55;
   move.BeginAnimation(TranslateTransform.XProperty,new DoubleAnimation(0,x,TimeSpan.FromMilliseconds(1000)){EasingFunction=new QuadraticEase{EasingMode=EasingMode.EaseOut}});
   var rise=new DoubleAnimationUsingKeyFrames();rise.KeyFrames.Add(new EasingDoubleKeyFrame(0,KeyTime.FromTimeSpan(TimeSpan.Zero)));rise.KeyFrames.Add(new EasingDoubleKeyFrame(y,KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(460)),new QuadraticEase{EasingMode=EasingMode.EaseOut}));rise.KeyFrames.Add(new EasingDoubleKeyFrame(y+28,KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(1000))));move.BeginAnimation(TranslateTransform.YProperty,rise);
   rotate.BeginAnimation(RotateTransform.AngleProperty,new DoubleAnimation(0,240+random.Next(240),TimeSpan.FromMilliseconds(1000)));
   var fade=new DoubleAnimationUsingKeyFrames();fade.KeyFrames.Add(new LinearDoubleKeyFrame(0,KeyTime.FromTimeSpan(TimeSpan.Zero)));fade.KeyFrames.Add(new LinearDoubleKeyFrame(1,KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(140))));fade.KeyFrames.Add(new LinearDoubleKeyFrame(.85,KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(600))));fade.KeyFrames.Add(new LinearDoubleKeyFrame(0,KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(1000))));ribbon.BeginAnimation(OpacityProperty,fade);
  }
  score=Theme.Text(points.ToString("0.#")+" 分",44,Theme.Blue,true);score.RenderTransformOrigin=new Point(.5,.5);scale=new ScaleTransform(.75,.75);score.RenderTransform=scale;score.Measure(new Size(double.PositiveInfinity,double.PositiveInfinity));SetLeft(score,center-score.DesiredSize.Width/2);SetTop(score,ActualHeight*.4-score.DesiredSize.Height/2);Children.Add(score);
  var pulse=new DoubleAnimationUsingKeyFrames();pulse.KeyFrames.Add(new EasingDoubleKeyFrame(.75,KeyTime.FromTimeSpan(TimeSpan.Zero)));pulse.KeyFrames.Add(new EasingDoubleKeyFrame(1.22,KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(360)),new CubicEase{EasingMode=EasingMode.EaseOut}));pulse.KeyFrames.Add(new EasingDoubleKeyFrame(1,KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(1000)),new CubicEase{EasingMode=EasingMode.EaseInOut}));scale.BeginAnimation(ScaleTransform.ScaleXProperty,pulse);scale.BeginAnimation(ScaleTransform.ScaleYProperty,pulse);
  score.BeginAnimation(OpacityProperty,new DoubleAnimation(1,0,TimeSpan.FromMilliseconds(400)){BeginTime=TimeSpan.FromMilliseconds(600)});
  finish=new DispatcherTimer{Interval=TimeSpan.FromMilliseconds(1000)};finish.Tick+=(s,e)=>{finish.Stop();Visibility=Visibility.Hidden;Children.Clear();IsAnimating=false;};finish.Start();
 }
}
