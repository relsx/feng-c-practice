using System;
using System.IO;
using System.Text;

static class AppSettings {
 public static readonly int[] FontSizes={14,16,18,20,22,24};
 static int font=16;static bool loaded;static string loadedRoot;
 public static bool AutoCheck=true;
 public static event Action Changed;
 static string FilePath{get{return Path.Combine(Engine.DataRoot,"settings-ver1.2.1.tsv");}}
 public static int EditorFont{get{Load();return font;}}
 public static void Load(){if(loaded&&loadedRoot==Engine.DataRoot)return;loaded=true;loadedRoot=Engine.DataRoot;font=16;AutoCheck=true;try{foreach(string line in File.ReadAllLines(FilePath)){string[] p=line.Split('\t');int value;if(p.Length!=2)continue;if(p[0]=="font"&&int.TryParse(p[1],out value)&&Array.IndexOf(FontSizes,value)>=0)font=value;if(p[0]=="auto")AutoCheck=p[1]!="0";}}catch{}}
 public static void SetFont(int value){Load();if(Array.IndexOf(FontSizes,value)<0)throw new ArgumentOutOfRangeException("value");font=value;Save();var h=Changed;if(h!=null)h();}
 public static void SetAutoCheck(bool value){Load();AutoCheck=value;Save();var h=Changed;if(h!=null)h();}
 static void Save(){Directory.CreateDirectory(Engine.DataRoot);string temp=FilePath+".tmp";File.WriteAllText(temp,"font\t"+font+"\nauto\t"+(AutoCheck?"1":"0")+"\n",new UTF8Encoding(false));if(File.Exists(FilePath))File.Replace(temp,FilePath,null);else File.Move(temp,FilePath);}
}
