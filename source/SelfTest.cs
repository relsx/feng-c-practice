using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Linq;
static class SelfTest {
 public static void Run(string path,bool quick=false){
  Engine.EnsureCompiler();var log=new StringBuilder();int failed=0,total=0;Action<bool,string> check=(ok,name)=>{total++;if(!ok)failed++;log.AppendLine((ok?"PASS ":"FAIL ")+name);File.WriteAllText(path,log.ToString(),Encoding.UTF8);};
  foreach(var p in quick?Bank.All().Take(0):Bank.All()){
   foreach(int seed in new[]{20261012,1234567}){
    var ev=Engine.Judge(p,p.Reference,seed,CancellationToken.None,(a,b)=>{});bool ok=ev.Compiled&&ev.Cases.Count==28&&ev.Cases.All(c=>c.Passed)&&ev.Score>=99.9;
    check(ok,p.Id.ToString("00")+" "+p.Title+" seed="+seed+" score="+ev.Score);
    if(!ok){log.AppendLine(Engine.Report(ev));File.WriteAllText(path,log.ToString(),Encoding.UTF8);}
   }
  }
  string why;
  var first=Bank.All()[0];var simple=CodeMetrics.Measure(first.Reference);var commented=CodeMetrics.Measure(first.Reference+"\n// int if while; fake lines\n/* long comment */\n");
  check(simple.Bytes==commented.Bytes&&simple.Lines==commented.Lines,"Comments and blank lines do not penalize code");
  check(CodeMetrics.Measure("char *s=\"a b // c\";").Bytes==CodeMetrics.Measure("char *s=\"ab // c\";").Bytes+1,"Whitespace and comment markers inside literals are preserved");
  check(CodeMetrics.Measure("int a=0;a++;a++;a++;").Lines>=4,"Minifying does not hide statement count");
  check(CodeMetrics.Component(12,4,1.6,12)==5&&CodeMetrics.Component(24,4,1.6,12)==0,"Line threshold and linear penalty boundaries");
  string bloated=first.Reference+"\nint unused(void){int x=0;\n"+string.Join("\n",Enumerable.Repeat("x+=0;",150).ToArray())+"\nreturn x;}\n";
  var fat=Engine.Judge(first,bloated,42,CancellationToken.None,(a,b)=>{});
  check(fat.Compiled&&fat.Cases.All(c=>c.Passed)&&fat.Score==90&&fat.LineScore==0&&fat.SizeScore==0,"Correct bloated code loses 10 simplicity points");
  check(Engine.Match("12 3"," \n12\t3 \n",out why),"Whitespace normalization");
  check(!Engine.Match("2432902008176640000","2432902008176640001",out why),"64-bit integer exactness");
  check(Engine.Match("3.14159265","3.1416",out why),"Float absolute tolerance");
  check(!Engine.Match("3.14159265","3.14",out why),"Float precision rejection");
  check(!Engine.Match("1.00000000","nan",out why),"NaN rejection");
  check(!Engine.Match("1.00000000","inf",out why),"Infinity rejection");
  check(!Engine.Match("10","0010",out why,true),"Binary string exactness");
  check(!Engine.Match("0","00",out why,true),"Numeric-looking string exactness");
  check(!Engine.Match("12","please input 12",out why),"Extra output rejection");
  var wrong=Engine.Judge(Bank.All()[0],"#include <stdio.h>\nint main(){puts(\"WRONG\");return 0;}",42,CancellationToken.None,(a,b)=>{});
  check(wrong.Compiled&&wrong.Score==0&&wrong.Cases.All(c=>!c.Passed),"Wrong answer score zero and per-case diagnosis");
  var error=Engine.Judge(Bank.All()[0],"int main(void) { return ???; }",42,CancellationToken.None,(a,b)=>{});
  check(!error.Compiled&&error.Score==0&&error.Compiler.Contains("answer.c:"),"Compiler line error");
  string dir=Path.Combine(Engine.DataRoot,"negative-tests");Directory.CreateDirectory(dir);
  Action<string,string,Action<Execution>> program=(name,code,verify)=>{string src=Path.Combine(dir,name+".c"),exe=Path.Combine(dir,name+".exe");File.WriteAllText(src,code);var c=Engine.Execute(Engine.CompilerPath,"-B\""+Path.GetDirectoryName(Engine.CompilerPath)+"\" \""+src+"\" -o \""+exe+"\"","",10000,CancellationToken.None,dir);check(c.ExitCode==0&&c.Failure=="",name+" compilation");if(File.Exists(exe)){var run=Engine.Execute(exe,"","",name=="timeout"?250:3000,CancellationToken.None,dir);log.AppendLine("DETAIL "+name+": "+run.Failure+", output="+run.Output.Length+", elapsed="+run.Millis);verify(run);}};
  program("timeout","int main(){for(;;){}return 0;}",e=>check(e.Failure.Contains("超时"),"Timeout enforcement"));
  program("flood","#include <stdio.h>\nint main(){while(1)puts(\"xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\");}",e=>check(e.Failure.Contains("64 KiB"),"Output flood limit"));
  program("crash","int main(){volatile int *p=(int*)0;*p=1;return 0;}",e=>check(e.Failure.Contains("异常退出"),"Crash detection"));
  program("memory","#include <stdlib.h>\n#include <stdio.h>\nint main(){void *p=malloc(400*1024*1024);puts(p?\"BAD\":\"LIMITED\");if(p)free(p);return 0;}",e=>check(e.Output.Trim()=="LIMITED","256MiB memory limit"));
  string loop=Path.Combine(dir,"timeout.exe");using(var ct=new CancellationTokenSource()){ct.CancelAfter(100);var e=Engine.Execute(loop,"","",3000,ct.Token,dir);check(e.Failure=="已停止","Cancellation terminates process");}
  var t1=Bank.Tests(45,1234);var t2=Bank.Tests(45,1234);var t3=Bank.Tests(45,1235);
  check(t1.Select(t=>t.Input).SequenceEqual(t2.Select(t=>t.Input)),"Seed reproduces inputs");
  check(!t1.Select(t=>t.Input).SequenceEqual(t3.Select(t=>t.Input)),"Fresh seeds produce new random inputs");
  log.AppendLine("\nTOTAL "+total+", FAILED "+failed+", "+(failed==0?"ALL PASSED":"CHECK FAILURES"));File.WriteAllText(path,log.ToString(),Encoding.UTF8);Environment.ExitCode=failed==0?0:1;
 }
}
