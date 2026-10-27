using System;
using System.IO;
using System.Text;

static class AppSettings {
 public static readonly int[] FontSizes={14,16,18,20,22,24};
 public static readonly int[] InterfaceFontSizes={12,13,14,15,16};
 static int uiFont=13;static string language="zh";
 public static string Language{get{Load();return language;}}
 public static void SetLanguage(string value){if(value!="zh"&&value!="en")throw new ArgumentException("language");Load();language=value;Save();L.Refresh();var h=Changed;if(h!=null)h();}
 public static int InterfaceFont{get{Load();return uiFont;}}
 public static void SetInterfaceFont(int value){Load();if(Array.IndexOf(InterfaceFontSizes,value)<0)throw new ArgumentOutOfRangeException("value");uiFont=value;Save();var h=Changed;if(h!=null)h();}
 static int font=16;static bool loaded;static string loadedRoot;
 public static bool AutoCheck=true;static bool night;
 public static bool Night{get{Load();return night;}}
 public static void SetNight(bool value){Load();night=value;Save();GlassTheme.Apply(value,true);var h=Changed;if(h!=null)h();}
 public static event Action Changed;
 static string FilePath{get{return Path.Combine(Engine.DataRoot,"settings-ver1.2.1.tsv");}}
 public static int EditorFont{get{Load();return font;}}
 public static void Load(){if(loaded&&loadedRoot==Engine.DataRoot)return;loaded=true;loadedRoot=Engine.DataRoot;font=16;uiFont=13;AutoCheck=true;night=false;language="zh";try{foreach(string line in File.ReadAllLines(FilePath)){string[] p=line.Split('\t');int value;if(p.Length!=2)continue;if(p[0]=="font"&&int.TryParse(p[1],out value)&&Array.IndexOf(FontSizes,value)>=0)font=value;if(p[0]=="uiFont"&&int.TryParse(p[1],out value)&&Array.IndexOf(InterfaceFontSizes,value)>=0)uiFont=value;if(p[0]=="auto")AutoCheck=p[1]!="0";if(p[0]=="language"&&(p[1]=="zh"||p[1]=="en"))language=p[1];if(p[0]=="theme")night=p[1]=="dark";}}catch{}}
 public static void SetFont(int value){Load();if(Array.IndexOf(FontSizes,value)<0)throw new ArgumentOutOfRangeException("value");font=value;Save();var h=Changed;if(h!=null)h();}
 public static void SetAutoCheck(bool value){Load();AutoCheck=value;Save();var h=Changed;if(h!=null)h();}
 static void Save(){Directory.CreateDirectory(Engine.DataRoot);string temp=FilePath+".tmp";File.WriteAllText(temp,"font\t"+font+"\nauto\t"+(AutoCheck?"1":"0")+"\ntheme\t"+(night?"dark":"light")+"\nuiFont\t"+uiFont+"\nlanguage\t"+language+"\n",new UTF8Encoding(false));if(File.Exists(FilePath))File.Replace(temp,FilePath,null);else File.Move(temp,FilePath);}
}
