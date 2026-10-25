using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

static class Transitions {
 public const int StartupMilliseconds=800,DialogMilliseconds=500;
 sealed class State {public int Milliseconds;public bool Started,Complete,Closing,AllowClose;public FrameworkElement Root;public readonly TaskCompletionSource<bool> Ready=new TaskCompletionSource<bool>();}
 static readonly ConditionalWeakTable<Window,State> states=new ConditionalWeakTable<Window,State>();
 public static void Attach(Window window,int milliseconds){State existing;if(states.TryGetValue(window,out existing))return;var state=new State{Milliseconds=milliseconds};states.Add(window,state);
  window.SourceInitialized+=(s,e)=>{Prepare(window,state);window.Closing+=(sender,args)=>{if(args.Cancel||state.AllowClose)return;args.Cancel=true;if(state.Closing)return;state.Closing=true;if(state.Root==null){state.AllowClose=true;window.Dispatcher.BeginInvoke((Action)(()=>window.Close()));return;}state.Root.IsHitTestVisible=false;var fade=new DoubleAnimation(state.Root.Opacity,0,TimeSpan.FromMilliseconds(180)){EasingFunction=new CubicEase{EasingMode=EasingMode.EaseInOut}};fade.Completed+=(a,b)=>{state.AllowClose=true;window.Close();if(window.IsVisible){state.AllowClose=state.Closing=false;state.Root.IsHitTestVisible=true;state.Root.BeginAnimation(UIElement.OpacityProperty,null);state.Root.Opacity=1;}};state.Root.BeginAnimation(UIElement.OpacityProperty,fade);};};
  window.ContentRendered+=(s,e)=>Start(window,state);
  window.Closed+=(s,e)=>{state.Complete=true;if(state.Root!=null){state.Root.BeginAnimation(UIElement.OpacityProperty,null);state.Root.Opacity=1;}state.Ready.TrySetResult(false);};
 }
 static void Prepare(Window window,State state){if(state.Started)return;state.Root=window.Content as FrameworkElement;if(state.Root!=null)state.Root.Opacity=0;}
 static void Start(Window window,State state){if(state.Started)return;state.Started=true;if(state.Root==null)state.Root=window.Content as FrameworkElement;if(state.Root==null){state.Complete=true;state.Ready.TrySetResult(true);return;}state.Root.Opacity=1;var fade=new DoubleAnimation(0,1,TimeSpan.FromMilliseconds(state.Milliseconds)){EasingFunction=new CubicEase{EasingMode=EasingMode.EaseOut},FillBehavior=FillBehavior.Stop};fade.Completed+=(s,e)=>{state.Complete=true;state.Ready.TrySetResult(true);};state.Root.BeginAnimation(UIElement.OpacityProperty,fade);}
 public static Task<bool> WhenReady(Window window){State state;return states.TryGetValue(window,out state)?state.Ready.Task:Task.FromResult(true);}
 public static bool IsComplete(Window window){State state;return states.TryGetValue(window,out state)&&state.Complete;}
 public static int Duration(Window window){State state;return states.TryGetValue(window,out state)?state.Milliseconds:0;}
}

sealed class AppDialog : Window {
 public MessageBoxResult Result{get;private set;}
 public Button AffirmativeButton{get;private set;}public Button CancelButton{get;private set;}
 public AppDialog(string text,string caption,MessageBoxButton buttons=MessageBoxButton.OK,MessageBoxImage image=MessageBoxImage.None){Title=string.IsNullOrWhiteSpace(caption)?Branding.Title:caption;Width=620;SizeToContent=SizeToContent.Height;MaxHeight=760;MinHeight=190;ResizeMode=ResizeMode.NoResize;WindowStartupLocation=WindowStartupLocation.CenterOwner;ShowInTaskbar=false;Theme.Window(this);
  Result=buttons==MessageBoxButton.YesNo?MessageBoxResult.No:buttons==MessageBoxButton.OK?MessageBoxResult.OK:MessageBoxResult.Cancel;
  var root=new Grid{Margin=new Thickness(22)};root.RowDefinitions.Add(new RowDefinition{Height=GridLength.Auto});root.RowDefinitions.Add(new RowDefinition{Height=GridLength.Auto});root.RowDefinitions.Add(new RowDefinition{Height=GridLength.Auto});Content=root;
  var heading=Theme.Text(Title,18,Theme.Blue,true);heading.Margin=new Thickness(0,0,0,14);root.Children.Add(heading);var message=Theme.Text(text,13);message.VerticalAlignment=VerticalAlignment.Top;var scroll=new ScrollViewer{Content=message,MaxHeight=520,VerticalScrollBarVisibility=ScrollBarVisibility.Auto,Padding=new Thickness(0,0,8,0)};Grid.SetRow(scroll,1);root.Children.Add(scroll);
  var actions=new StackPanel{Orientation=Orientation.Horizontal,HorizontalAlignment=HorizontalAlignment.Right,Margin=new Thickness(0,20,0,0)};Grid.SetRow(actions,2);root.Children.Add(actions);
  bool yes=buttons==MessageBoxButton.YesNo||buttons==MessageBoxButton.YesNoCancel;AffirmativeButton=Theme.Button(yes?"是":"确定",true);AffirmativeButton.IsDefault=!yes;AffirmativeButton.Click+=(s,e)=>{Result=yes?MessageBoxResult.Yes:MessageBoxResult.OK;Close();};actions.Children.Add(AffirmativeButton);
  if(yes){var no=Theme.Button("否");no.IsDefault=true;no.IsCancel=buttons==MessageBoxButton.YesNo;no.Click+=(s,e)=>{Result=MessageBoxResult.No;Close();};actions.Children.Add(no);}
  if(buttons==MessageBoxButton.OKCancel||buttons==MessageBoxButton.YesNoCancel){CancelButton=Theme.Button("取消");CancelButton.IsCancel=true;CancelButton.Click+=(s,e)=>{Result=MessageBoxResult.Cancel;Close();};actions.Children.Add(CancelButton);}else if(buttons==MessageBoxButton.OK)AffirmativeButton.IsCancel=true;
 }
 public static MessageBoxResult Show(Window owner,string text,string caption="",MessageBoxButton buttons=MessageBoxButton.OK,MessageBoxImage image=MessageBoxImage.None){var dialog=new AppDialog(text,caption,buttons,image);if(owner!=null&&owner.IsVisible)dialog.Owner=owner;dialog.ShowDialog();return dialog.Result;}
 public static MessageBoxResult Show(string text){return Show(Application.Current==null?null:Application.Current.MainWindow,text);}
}
