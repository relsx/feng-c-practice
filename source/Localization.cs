using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;

static class L {
 static Dictionary<string,string> english;static KeyValuePair<string,string>[] phrases;static Dictionary<char,KeyValuePair<string,string>[]> starts;
 public static bool English{get{return AppSettings.Language=="en";}}
 public static readonly DependencyProperty ExcludedProperty=DependencyProperty.RegisterAttached("Excluded",typeof(bool),typeof(L),new PropertyMetadata(false));
 public static void Exclude(DependencyObject node){node.SetValue(ExcludedProperty,true);}
 static readonly DependencyProperty MetadataSourceProperty=DependencyProperty.RegisterAttached("MetadataSource",typeof(string),typeof(L),new PropertyMetadata(null));
 public static void SetOutput(TextBox box,string text){box.SetValue(MetadataSourceProperty,text);box.Text=T(text);}
 public static void SetRawOutput(TextBox box,string text){box.ClearValue(MetadataSourceProperty);box.Text=text;}
 static void Load(){if(english!=null)return;english=new Dictionary<string,string>();using(var stream=Assembly.GetExecutingAssembly().GetManifestResourceStream("languages.json"))using(var read=new StreamReader(stream,Encoding.UTF8)){english=new System.Web.Script.Serialization.JavaScriptSerializer{MaxJsonLength=2000000}.Deserialize<Dictionary<string,string>>(read.ReadToEnd());}phrases=english.OrderByDescending(p=>p.Key.Length).ToArray();starts=phrases.GroupBy(p=>p.Key[0]).ToDictionary(g=>g.Key,g=>g.ToArray());}
 static string Decode(string s){return s.Replace("\\n","\n").Replace("\\t","\t").Replace("\\r","\r");}
 public static string T(string text){if(text==null||!English)return text;Load();string value;if(english.TryGetValue(text,out value))return value;
  text=Regex.Replace(text,@"第\s*(\d+)\s*行\s*[·，]\s*第\s*(\d+)\s*列",m=>"Ln "+m.Groups[1].Value+" · Col "+m.Groups[2].Value);
  text=Regex.Replace(text,@"第\s*(\d+)\s*关",m=>"Level "+m.Groups[1].Value);text=Regex.Replace(text,@"第\s*(\d+)\s*章",m=>"Chapter "+m.Groups[1].Value);
  // A single pass avoids translating text already emitted by an earlier match.
  var b=new StringBuilder();for(int i=0;i<text.Length;){bool matched=false;KeyValuePair<string,string>[] candidates;if(starts.TryGetValue(text[i],out candidates)){foreach(var p in candidates){if(p.Key.Length<=text.Length-i&&string.CompareOrdinal(text,i,p.Key,0,p.Key.Length)==0){b.Append(p.Value);i+=p.Key.Length;matched=true;break;}}}if(!matched)b.Append(text[i++]);}return b.ToString();
 }
 public static string Diagnosis(string text){if(text==null)return "";if(!English)return text;var parts=Regex.Split(text,@"(「[^」]*」)");return string.Concat(parts.Select(p=>p.StartsWith("「")?p:T(p)));}
 sealed class Watch {public DependencyObject Node;public DependencyProperty Property;public string Source,Shown;public bool Updating;public DependencyPropertyDescriptor Descriptor;public EventHandler Handler;public void Render(){if(Property==ContentControl.ContentProperty&&!(Node.GetValue(Property) is string))return;Updating=true;try{Shown=T(Source);Node.SetCurrentValue(Property,Shown);}finally{Updating=false;}}}
 sealed class Scope {public Window Window;public List<Watch> Watches=new List<Watch>();public bool Queued,Closed;public void Scan(){if(Closed)return;var live=new HashSet<DependencyObject>();Visit(Window,this,live);for(int i=Watches.Count-1;i>=0;i--){var w=Watches[i];if(live.Contains(w.Node))continue;w.Descriptor.RemoveValueChanged(w.Node,w.Handler);HashSet<DependencyProperty> props;if(seen.TryGetValue(w.Node,out props))props.Remove(w.Property);Watches.RemoveAt(i);}}}
 static readonly ConditionalWeakTable<DependencyObject,HashSet<DependencyProperty>> seen=new ConditionalWeakTable<DependencyObject,HashSet<DependencyProperty>>();
 static readonly List<WeakReference> scopes=new List<WeakReference>();
 public static void Attach(Window window){var scope=new Scope{Window=window};scopes.Add(new WeakReference(scope));window.Loaded+=(s,e)=>scope.Scan();window.LayoutUpdated+=(s,e)=>{if(scope.Queued||scope.Closed)return;scope.Queued=true;window.Dispatcher.BeginInvoke(DispatcherPriority.Background,(Action)(()=>{scope.Queued=false;scope.Scan();}));};window.Closed+=(s,e)=>{scope.Closed=true;foreach(var w in scope.Watches)w.Descriptor.RemoveValueChanged(w.Node,w.Handler);scope.Watches.Clear();};}
 public static void Refresh(){foreach(var reference in scopes.ToArray()){var scope=reference.Target as Scope;if(scope==null||scope.Closed){scopes.Remove(reference);continue;}foreach(var watch in scope.Watches.ToArray())watch.Render();scope.Scan();}}
 static void WatchProperty(DependencyObject node,DependencyProperty property,Scope scope){object value=node.GetValue(property);if(!(value is string))return;HashSet<DependencyProperty> props;if(!seen.TryGetValue(node,out props)){props=new HashSet<DependencyProperty>();seen.Add(node,props);}if(!props.Add(property))return;var w=new Watch{Node=node,Property=property,Source=(string)value,Descriptor=DependencyPropertyDescriptor.FromProperty(property,node.GetType())};w.Handler=(s,e)=>{if(w.Updating)return;var current=node.GetValue(property) as string;if(current==null)return;w.Source=current;w.Render();};w.Descriptor.AddValueChanged(node,w.Handler);scope.Watches.Add(w);w.Render();}
 static void Visit(DependencyObject node,Scope scope,HashSet<DependencyObject> live){if(node==null)return;var output=node as TextBox;var metadata=node.GetValue(MetadataSourceProperty) as string;if(output!=null&&metadata!=null){string display=T(metadata);if(output.Text!=display)output.Text=display;}if((bool)node.GetValue(ExcludedProperty)||node is CodeEditor||node is ICSharpCode.AvalonEdit.TextEditor)return;live.Add(node);var window=node as Window;if(window!=null)WatchProperty(node,Window.TitleProperty,scope);var text=node as TextBlock;if(text!=null){var presenter=VisualTreeHelper.GetParent(text) as ContentPresenter;if(presenter!=null&&presenter.Content is string)return;if(text.Inlines.Count>1){foreach(var inline in text.Inlines){var run=inline as Run;if(run!=null){live.Add(run);WatchProperty(run,Run.TextProperty,scope);}}}else WatchProperty(node,TextBlock.TextProperty,scope);}
  var tb=node as TextBox;if(tb!=null){if(tb.IsReadOnly)WatchProperty(node,TextBox.TextProperty,scope);return;}
  var content=node as ContentControl;if(content!=null)WatchProperty(node,ContentControl.ContentProperty,scope);var header=node as HeaderedContentControl;if(header!=null)WatchProperty(node,HeaderedContentControl.HeaderProperty,scope);var fe=node as FrameworkElement;if(fe!=null){WatchProperty(node,FrameworkElement.ToolTipProperty,scope);if(node is Button)WatchProperty(node,System.Windows.Automation.AutomationProperties.NameProperty,scope);}
  int count=VisualTreeHelper.GetChildrenCount(node);for(int i=0;i<count;i++)Visit(VisualTreeHelper.GetChild(node,i),scope,live);
 }
}
