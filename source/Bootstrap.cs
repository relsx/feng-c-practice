using System;
using System.IO;
using System.Reflection;
static class Bootstrap {
 [STAThread]static void Main(string[] args){AppDomain.CurrentDomain.AssemblyResolve+=(s,e)=>{if(new AssemblyName(e.Name).Name!="ICSharpCode.AvalonEdit")return null;using(var input=Assembly.GetExecutingAssembly().GetManifestResourceStream("AvalonEdit.dll"))using(var ms=new MemoryStream()){input.CopyTo(ms);return Assembly.Load(ms.ToArray());}};try{if(args.Length==2&&args[0]=="--apply-update"){Environment.ExitCode=UpdateService.Apply(args[1]);return;}Program.Run(args);}catch(Exception ex){Environment.ExitCode=1;try{string file=args.Length>=2&&args[0].StartsWith("--")?args[1]:Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"CPracticeLab-ver1.1","startup-error.txt");Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(file)));File.AppendAllText(file,"\nFATAL: "+ex);}catch{}if(args.Length==0)System.Windows.MessageBox.Show("启动失败："+ex.Message,"Feng's C语言练习器 ver1.2.1");}}
}
