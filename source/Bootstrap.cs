using System;
using System.IO;
using System.Reflection;
static class Bootstrap {
 [STAThread]static void Main(string[] args){AppDomain.CurrentDomain.AssemblyResolve+=(s,e)=>{if(new AssemblyName(e.Name).Name!="ICSharpCode.AvalonEdit")return null;using(var input=Assembly.GetExecutingAssembly().GetManifestResourceStream("AvalonEdit.dll"))using(var ms=new MemoryStream()){input.CopyTo(ms);return Assembly.Load(ms.ToArray());}};Program.Run(args);}
}
