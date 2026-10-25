using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

// Draw the glass highlights as vector layers. No blur, bitmap caching or scaling touches the text.
sealed class GlassCard : Border {
 public GlassCard(){CornerRadius=new CornerRadius(22);SizeChanged+=(s,e)=>RoundChild();Loaded+=(s,e)=>RoundChild();}
 void RoundChild(){var child=Child as FrameworkElement;if(child==null)return;double radius=Math.Max(0,22-Math.Min(Padding.Left,Padding.Top));child.Clip=new RectangleGeometry(new Rect(0,0,Math.Max(0,child.ActualWidth),Math.Max(0,child.ActualHeight)),radius,radius);}
 protected override void OnRender(DrawingContext dc){var r=new Rect(0,0,Math.Max(0,ActualWidth),Math.Max(0,ActualHeight));if(r.IsEmpty)return;for(int i=4;i>0;i--){var shadow=r;shadow.Inflate(i*.65,i*.65);shadow.Offset(0,1.5);dc.DrawRoundedRectangle(null,new Pen(new SolidColorBrush(Color.FromArgb(3,25,69,122)),1),shadow,22+i*.6,22+i*.6);}base.OnRender(dc);var reflection=new LinearGradientBrush(new GradientStopCollection{new GradientStop(Color.FromArgb(18,255,255,255),0),new GradientStop(Colors.Transparent,.44)},90);dc.DrawRoundedRectangle(reflection,null,r,22,22);r.Inflate(-.55,-.55);var edge=new LinearGradientBrush(new GradientStopCollection{new GradientStop(Color.FromArgb(95,255,255,255),0),new GradientStop(Color.FromArgb(5,255,255,255),.5),new GradientStop(Color.FromArgb(22,127,180,255),1)},new Point(0,0),new Point(1,1));dc.DrawRoundedRectangle(null,new Pen(edge,.8),r,22,22);}
}
