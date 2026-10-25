using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
static class Branding {
 public const string Title="Feng's C语言练习器 ver1.2.1.1";
 public const string Attribution="By Feng; 仅供学习使用；Email：feng111@bupt.edu.cn";
 public static UIElement Header(Action openSettings=null){
  var grid=new Grid{Margin=new Thickness(28,16,162,10)};grid.ColumnDefinitions.Add(new ColumnDefinition{Width=new GridLength(1,GridUnitType.Star)});grid.ColumnDefinitions.Add(new ColumnDefinition{Width=GridLength.Auto});
  var left=new StackPanel();var title=new TextBlock{Margin=new Thickness(0,0,0,3)};title.Inlines.Add(new Run("Feng's "){FontFamily=new FontFamily("Georgia"),FontStyle=FontStyles.Italic,FontSize=24,Foreground=Theme.Blue});title.Inlines.Add(new Run("C语言练习器"){FontSize=18,FontWeight=FontWeights.SemiBold,Foreground=Theme.Ink});title.Inlines.Add(new Run("  ver1.2.1.1"){FontSize=11,FontFamily=new FontFamily("Consolas"),Foreground=Theme.Muted});left.Children.Add(title);
  left.Children.Add(Theme.Text("By Feng  ·  仅供学习使用",11,Theme.Muted));left.Children.Add(Theme.Text("Email：feng111@bupt.edu.cn",10,Theme.Muted));grid.Children.Add(left);
  var actions=new StackPanel{Orientation=Orientation.Horizontal,VerticalAlignment=VerticalAlignment.Center};var badge=new Border{Background=Theme.White,BorderBrush=Theme.Line,BorderThickness=new Thickness(1),CornerRadius=new CornerRadius(18),Padding=new Thickness(14,9,14,9),Margin=new Thickness(0,0,12,0),Child=Theme.Text("C  /  LEARN BY DOING",10,Theme.Blue,true)};actions.Children.Add(badge);var settings=Theme.Button("设置");settings.Margin=new Thickness(0);settings.Click+=(s,e)=>{if(openSettings!=null)openSettings();};actions.Children.Add(settings);Grid.SetColumn(actions,1);grid.Children.Add(actions);Action changed=()=>{if(!grid.Dispatcher.HasShutdownStarted)grid.Dispatcher.BeginInvoke((Action)(()=>{settings.ToolTip=UpdateService.Latest!=null&&UpdateService.Latest.Newer?"设置 · 有新版本":"设置 · 外观、字号与软件更新";}));};UpdateService.Changed+=changed;grid.Unloaded+=(s,e)=>UpdateService.Changed-=changed;
  return grid;
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
