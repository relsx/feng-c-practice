using System;
using System.Windows;
using System.Windows.Controls;
sealed class AnswerWindow : Window {
 public bool ImportAnswer{get;private set;}
 public Button Reveal{get;private set;}public Button ImportButton{get;private set;}public CodeEditor AnswerEditor{get;private set;}
 public AnswerWindow(Problem p){Title="提示与标准答案 · "+p.Title;Width=840;Height=700;MinWidth=640;MinHeight=500;WindowStartupLocation=WindowStartupLocation.CenterOwner;Theme.Window(this);
  var grid=new Grid{Margin=new Thickness(22)};grid.RowDefinitions.Add(new RowDefinition{Height=GridLength.Auto});grid.RowDefinitions.Add(new RowDefinition{Height=new GridLength(1,GridUnitType.Star)});grid.RowDefinitions.Add(new RowDefinition{Height=GridLength.Auto});Content=grid;
  var top=new StackPanel();top.Children.Add(Theme.Text(p.Id.ToString("00")+" · "+p.Title,20,Theme.Blue,true));var hint=Theme.Text("先想一想\n"+p.Hint,13);hint.Margin=new Thickness(0,12,0,12);top.Children.Add(Theme.Card(hint));top.Children.Add(Theme.Text("先根据提示尝试；需要时再展开完整代码。",11,Theme.Muted));grid.Children.Add(top);
  AnswerEditor=new CodeEditor{Visibility=Visibility.Collapsed,Margin=new Thickness(0,12,0,10)};AnswerEditor.Text=p.Reference;AnswerEditor.Input.IsReadOnly=true;Grid.SetRow(AnswerEditor,1);grid.Children.Add(AnswerEditor);
  var actions=new DockPanel{Margin=new Thickness(0,12,0,0)};Grid.SetRow(actions,2);grid.Children.Add(actions);var close=Theme.Button("关闭");DockPanel.SetDock(close,Dock.Right);actions.Children.Add(close);close.Click+=(s,e)=>Close();ImportButton=Theme.Button("一键导入编码区",true);ImportButton.IsEnabled=false;DockPanel.SetDock(ImportButton,Dock.Right);actions.Children.Add(ImportButton);ImportButton.Click+=(s,e)=>{ImportAnswer=true;Close();};Reveal=Theme.Button("展开标准答案");DockPanel.SetDock(Reveal,Dock.Right);actions.Children.Add(Reveal);Reveal.Click+=(s,e)=>{AnswerEditor.Visibility=Visibility.Visible;ImportButton.IsEnabled=true;Reveal.IsEnabled=false;};actions.Children.Add(Theme.Text("By Feng · 仅供学习使用\n导入前自动备份当前草稿",10.5,Theme.Muted));
 }
}
