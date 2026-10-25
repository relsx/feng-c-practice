using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

static class GlassMotion {
 public static void Reveal(UIElement element){if(element==null)return;element.BeginAnimation(UIElement.OpacityProperty,new DoubleAnimation(.12,1,TimeSpan.FromMilliseconds(260)){EasingFunction=new CubicEase{EasingMode=EasingMode.EaseOut},FillBehavior=FillBehavior.Stop});}
 public static void WatchTabs(TabControl tabs){tabs.SelectionChanged+=(s,e)=>{if(e.Source!=tabs)return;var item=tabs.SelectedItem as TabItem;if(item!=null)Reveal(item.Content as UIElement);};}
}
