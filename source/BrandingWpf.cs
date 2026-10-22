using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
static class Branding {
 public const string Title="Feng's C语言练习器 ver1.2.1";
 public const string Attribution="By Feng; 仅供学习使用；Email：feng111@bupt.edu.cn";
 public static UIElement Header(Action openSettings=null){
  var grid=new Grid{Margin=new Thickness(25,5,25,5)};grid.ColumnDefinitions.Add(new ColumnDefinition{Width=new GridLength(1,GridUnitType.Star)});grid.ColumnDefinitions.Add(new ColumnDefinition{Width=GridLength.Auto});
  var left=new StackPanel();var title=new TextBlock{Margin=new Thickness(0,0,0,2)};title.Inlines.Add(new Run("Feng's "){FontFamily=new FontFamily("Georgia"),FontStyle=FontStyles.Italic,FontSize=20,Foreground=Theme.B("#E9F2FF")});title.Inlines.Add(new Run("C语言练习器"){FontSize=16,FontWeight=FontWeights.SemiBold,Foreground=Theme.B("#FFFFFF")});title.Inlines.Add(new Run(" ver1.2.1"){FontSize=11,FontFamily=new FontFamily("Consolas"),Foreground=Theme.B("#B7CBE7")});left.Children.Add(title);
  left.Children.Add(Theme.Text("By Feng;",11,Theme.B("#E9F2FF"),true));left.Children.Add(Theme.Text("仅供学习使用；Email：feng111@bupt.edu.cn",10.5,Theme.B("#C8D9ED")));grid.Children.Add(left);
  var actions=new StackPanel{Orientation=Orientation.Horizontal,VerticalAlignment=VerticalAlignment.Center};var badge=new Border{Background=Theme.B("#12FFFFFF"),CornerRadius=new CornerRadius(12),Padding=new Thickness(13,8,13,8),Margin=new Thickness(0,0,14,0),Child=Theme.Text("C  /  LEARN BY DOING",10,Theme.B("#BED9F4"),true)};actions.Children.Add(badge);var settings=Dark.Button("设置 / SETTINGS");settings.Margin=new Thickness(0);settings.Click+=(s,e)=>{if(openSettings!=null)openSettings();};actions.Children.Add(settings);Grid.SetColumn(actions,1);grid.Children.Add(actions);Action changed=()=>{if(!grid.Dispatcher.HasShutdownStarted)grid.Dispatcher.BeginInvoke((Action)(()=>{settings.Content=UpdateService.Latest!=null&&UpdateService.Latest.Newer?"设置 · 有新版本":"设置 / SETTINGS";settings.ToolTip=UpdateService.Status;}));};UpdateService.Changed+=changed;grid.Unloaded+=(s,e)=>UpdateService.Changed-=changed;
  var glass=new Grid{ClipToBounds=true,Background=new LinearGradientBrush(new GradientStopCollection{new GradientStop(Color.FromRgb(15,36,67),0),new GradientStop(Color.FromRgb(23,56,96),.45),new GradientStop(Color.FromRgb(29,70,113),1)},new Point(0,0),new Point(1,1))};
  var glow=new System.Windows.Shapes.Ellipse{Width=680,Height=170,HorizontalAlignment=HorizontalAlignment.Center,VerticalAlignment=VerticalAlignment.Center,Margin=new Thickness(80,-50,0,0),Fill=new RadialGradientBrush(Color.FromArgb(24,115,172,238),Color.FromArgb(0,115,172,238)),IsHitTestVisible=false};glass.Children.Add(glow);glass.Children.Add(grid);
  return new Border{Background=Theme.Bg,BorderBrush=Theme.B("#284D76"),BorderThickness=new Thickness(0,0,0,1),Child=glass};
 }
}
sealed class WelcomeWindow : Window {
 public WelcomeWindow(){Title="欢迎使用 · "+Branding.Title;Width=630;Height=490;ResizeMode=ResizeMode.NoResize;WindowStartupLocation=WindowStartupLocation.CenterOwner;ShowInTaskbar=false;Theme.Window(this);
  var layout=new Grid();layout.RowDefinitions.Add(new RowDefinition{Height=GridLength.Auto});layout.RowDefinitions.Add(new RowDefinition{Height=GridLength.Auto});layout.RowDefinitions.Add(new RowDefinition{Height=new GridLength(1,GridUnitType.Star)});layout.RowDefinitions.Add(new RowDefinition{Height=GridLength.Auto});Content=new Border{Background=Theme.Bg,Padding=new Thickness(24),Child=layout};
  var heading=new StackPanel{Margin=new Thickness(0,0,0,14)};heading.Children.Add(Theme.Text(Branding.Title,22,Theme.Blue,true));heading.Children.Add(Theme.Text("90 道递进练习 · 50 关暗域挑战 · 3 个综合项目考试",12,Theme.Muted));layout.Children.Add(heading);
  var attribution=Theme.Card(Theme.Text(Branding.Attribution,13,Theme.Blue,true),new Thickness(14));attribution.Margin=new Thickness(0,0,0,12);Grid.SetRow(attribution,1);layout.Children.Add(attribution);
  var guide=new StackPanel();guide.Children.Add(Theme.Text("开始练习，只需四步",14,Theme.Ink,true));foreach(string text in new[]{"01  选题：在左侧搜索或选择题目，阅读要求和示例。","02  编写：在宽敞的编码区完成 C 程序。","03  评测：点击“编译并评测”，或按 Ctrl+Enter。","04  改进：在右侧查看实际输出，打开报告检查全部测试。"}){var row=Theme.Text(text,12);row.Margin=new Thickness(0,13,0,0);guide.Children.Add(row);}var guideCard=Theme.Card(guide);Grid.SetRow(guideCard,2);layout.Children.Add(guideCard);
  var footer=new DockPanel{Margin=new Thickness(0,16,0,0)};var close=Theme.Button("开始练习",true);close.IsDefault=true;close.IsCancel=true;close.Click+=(s,e)=>Close();DockPanel.SetDock(close,Dock.Right);footer.Children.Add(close);footer.Children.Add(Theme.Text("撤回 Ctrl+Z · 取消撤回 Ctrl+Y\n闯关：从空白编写，>90 分解锁下一关\n点击 × 或按 Esc 可关闭此说明",10.5,Theme.Muted));Grid.SetRow(footer,3);layout.Children.Add(footer);
 }
}
