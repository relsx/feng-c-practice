using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
class Execution { public string Output="",Error="",Failure=""; public int ExitCode; public double Millis,CpuMillis; public bool Healthy {get{return Failure==""&&ExitCode==0&&Error.Trim()==""&&CpuMillis<=250;}} }
class CaseResult {public TestCase Test;public Execution Run;public bool Passed;public string Diagnosis;}
class Evaluation {public List<CaseResult> Cases=new List<CaseResult>(); public string Compiler="";public bool Compiled;public int Seed,ProblemId; public double Score,NormalScore,EdgeScore,QualityScore,LineScore,SizeScore;public int CodeLines,CodeBytes,ReferenceLines,ReferenceBytes;public string Code,Title,Time;}
sealed class Job : IDisposable {
 IntPtr handle;
 [StructLayout(LayoutKind.Sequential)] struct Basic {public long PerProcessUserTimeLimit,PerJobUserTimeLimit;public uint LimitFlags;public UIntPtr MinimumWorkingSetSize,MaximumWorkingSetSize;public uint ActiveProcessLimit;public UIntPtr Affinity;public uint PriorityClass,SchedulingClass;}
 [StructLayout(LayoutKind.Sequential)] struct Io {public ulong ReadOperationCount,WriteOperationCount,OtherOperationCount,ReadTransferCount,WriteTransferCount,OtherTransferCount;}
 [StructLayout(LayoutKind.Sequential)] struct Limits {public Basic BasicLimitInformation;public Io IoInfo;public UIntPtr ProcessMemoryLimit,JobMemoryLimit,PeakProcessMemoryUsed,PeakJobMemoryUsed;}
 [DllImport("kernel32.dll",CharSet=CharSet.Unicode)] static extern IntPtr CreateJobObject(IntPtr a,string n);
 [DllImport("kernel32.dll",SetLastError=true)] static extern bool SetInformationJobObject(IntPtr h,int c,IntPtr p,uint l);
 [DllImport("kernel32.dll",SetLastError=true)] static extern bool AssignProcessToJobObject(IntPtr h,IntPtr p);
 [DllImport("kernel32.dll")] static extern bool CloseHandle(IntPtr h);
 public Job(){handle=CreateJobObject(IntPtr.Zero,null);if(handle==IntPtr.Zero)throw new Exception("无法创建进程资源限制。");var l=new Limits();l.BasicLimitInformation.LimitFlags=0x2000|0x100|0x8;l.BasicLimitInformation.ActiveProcessLimit=1;l.ProcessMemoryLimit=new UIntPtr(256*1024*1024);int size=Marshal.SizeOf(l);IntPtr mem=Marshal.AllocHGlobal(size);try{Marshal.StructureToPtr(l,mem,false);if(!SetInformationJobObject(handle,9,mem,(uint)size))throw new Exception("无法设置进程资源限制。");}catch{Dispose();throw;}finally{Marshal.FreeHGlobal(mem);}}
 public void Add(Process p){if(!AssignProcessToJobObject(handle,p.Handle)&&!p.HasExited)throw new Exception("无法应用进程资源限制："+Marshal.GetLastWin32Error());}
 public void Dispose(){if(handle!=IntPtr.Zero){CloseHandle(handle);handle=IntPtr.Zero;}}
}
static class Engine {
 public static string DataRoot=Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"CPracticeLab-ver1.1");
 static string compiler;
 public static string CompilerPath {get{EnsureCompiler();return compiler;}}
 public static void EnsureCompiler(){
  if(compiler!=null&&File.Exists(compiler))return;
  string folder=Path.Combine(DataRoot,"compiler-0.9.27"),marker=Path.Combine(folder,".complete");Directory.CreateDirectory(DataRoot);
  using(var mutex=new Mutex(false,"Local\\CPracticeLab-CompilerInstall")){
   bool held=false;try{try{held=mutex.WaitOne(20000);}catch(AbandonedMutexException){held=true;}if(!held)throw new Exception("编译器正在初始化，请稍后再试。");
    if(!File.Exists(marker)){Directory.CreateDirectory(folder);using(Stream s=Assembly.GetExecutingAssembly().GetManifestResourceStream("compiler.zip"))using(var zip=new ZipArchive(s,ZipArchiveMode.Read)){foreach(var entry in zip.Entries){string dest=Path.GetFullPath(Path.Combine(folder,entry.FullName));if(!dest.StartsWith(Path.GetFullPath(folder)+Path.DirectorySeparatorChar,StringComparison.OrdinalIgnoreCase))throw new Exception("编译器包路径无效。");if(entry.Name==""){Directory.CreateDirectory(dest);continue;}Directory.CreateDirectory(Path.GetDirectoryName(dest));using(Stream src=entry.Open())using(Stream dst=File.Create(dest))src.CopyTo(dst);}}File.WriteAllText(marker,"0.9.27");}
   }finally{if(held)mutex.ReleaseMutex();}
  }
  compiler=Path.Combine(folder,"tcc","tcc.exe");if(!File.Exists(compiler))throw new Exception("内置编译器缺失。");
 }
 static string Quote(string s){return "\""+s+"\"";}
 public static Execution Execute(string exe,string args,string input,int timeout,CancellationToken ct,string cwd){
  var result=new Execution();var watch=Stopwatch.StartNew();
  using(var p=new Process())using(var job=new Job()){
   p.StartInfo=new ProcessStartInfo(exe,args){WorkingDirectory=cwd,CreateNoWindow=true,UseShellExecute=false,RedirectStandardInput=true,RedirectStandardOutput=true,RedirectStandardError=true,StandardOutputEncoding=new UTF8Encoding(false,false),StandardErrorEncoding=new UTF8Encoding(false,false)};
   p.StartInfo.EnvironmentVariables["TMP"]=cwd;p.StartInfo.EnvironmentVariables["TEMP"]=cwd;
   int count=0,overflow=0;
   Func<StreamReader,Task<string>> read=reader=>Task.Factory.StartNew(()=>{var sb=new StringBuilder();char[] buf=new char[1024];int num;try{while((num=reader.Read(buf,0,buf.Length))>0){int total=Interlocked.Add(ref count,Encoding.UTF8.GetByteCount(buf,0,num));if(total>65536){Interlocked.Exchange(ref overflow,1);try{p.Kill();}catch{}break;}sb.Append(buf,0,num);}}catch(IOException){}return sb.ToString();},CancellationToken.None,TaskCreationOptions.LongRunning,TaskScheduler.Default);
   try{
    p.Start();job.Add(p);
    Task<string> stdout=read(p.StandardOutput),stderr=read(p.StandardError);
    Task writer=Task.Factory.StartNew(()=>{try{byte[] data=new UTF8Encoding(false).GetBytes(input);p.StandardInput.BaseStream.Write(data,0,data.Length);p.StandardInput.BaseStream.Flush();p.StandardInput.Close();}catch(IOException){}catch(ObjectDisposedException){} });
    while(!p.WaitForExit(20)){if(ct.IsCancellationRequested){result.Failure="已停止";break;}if(watch.ElapsedMilliseconds>timeout){result.Failure="运行超时（"+(timeout/1000.0).ToString("0.#")+" 秒）";break;}if(Volatile.Read(ref overflow)!=0){result.Failure="输出超过 64 KiB 限制";break;}}
    if(result.Failure!=""&&!p.HasExited){try{p.Kill();}catch{} }
    job.Dispose();p.WaitForExit(1000);
    if(Volatile.Read(ref overflow)!=0)result.Failure="输出超过 64 KiB 限制";
    Task.WaitAll(new Task[]{stdout,stderr,writer},2000);
    result.Output=stdout.IsCompleted?stdout.Result:"";result.Error=stderr.IsCompleted?stderr.Result:"";
    if(p.HasExited){result.ExitCode=p.ExitCode;result.CpuMillis=p.TotalProcessorTime.TotalMilliseconds;if(result.ExitCode!=0&&result.Failure=="")result.Failure="异常退出（代码 "+result.ExitCode+" / 0x"+unchecked((uint)result.ExitCode).ToString("X8")+"）";}
    else result.Failure="进程无法结束";
   }catch(Exception ex){job.Dispose();try{if(!p.HasExited)p.Kill();}catch{}result.Failure="运行环境错误："+ex.Message;}
  }
  watch.Stop();result.Millis=watch.Elapsed.TotalMilliseconds;return result;
 }
 public static bool Match(string expected,string actual,out string why,bool strictText=false){
  string[] want=Regex.Split(expected.Trim(),@"\s+").Where(x=>x!="").ToArray(),got=Regex.Split(actual.Trim(),@"\s+").Where(x=>x!="").ToArray();
  if(want.Length!=got.Length){why="输出项数不符：应有 "+want.Length+" 项，实际 "+got.Length+" 项。请检查是否漏输出，或输出了输入提示等额外内容。";return false;}
  for(int i=0;i<want.Length;i++){
   if(want[i]==got[i])continue;
   double a,b;long ia,ib;
   bool integer=Regex.IsMatch(want[i],@"^[+-]?\d+$");
   if(strictText){why="第 "+(i+1)+" 项字符串不符：预期「"+want[i]+"」，实际「"+got[i]+"」。";return false;}
   if(integer&&long.TryParse(want[i],NumberStyles.Integer,CultureInfo.InvariantCulture,out ia)&&long.TryParse(got[i],NumberStyles.Integer,CultureInfo.InvariantCulture,out ib)&&ia==ib)continue;
   if(!integer&&double.TryParse(want[i],NumberStyles.Float,CultureInfo.InvariantCulture,out a)&&double.TryParse(got[i],NumberStyles.Float,CultureInfo.InvariantCulture,out b)&&!double.IsNaN(b)&&!double.IsInfinity(b)&&Math.Abs(a-b)<=0.0001)continue;
   why="第 "+(i+1)+" 项不同：预期「"+want[i]+"」，实际「"+got[i]+"」。";return false;
  }
  why="输出符合要求";return true;
 }
 public static Evaluation Judge(Problem problem,string code,int seed,CancellationToken ct,Action<int,int> progress){
  EnsureCompiler();string dir=Path.Combine(DataRoot,"runs",Guid.NewGuid().ToString("N"));Directory.CreateDirectory(dir);
  var ev=new Evaluation{Seed=seed,ProblemId=problem.Id,Code=code,Title=problem.Title,Time=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")};
  try{
   CodeMetrics.Apply(ev,problem.Reference);
   string source=Path.Combine(dir,"answer.c"),exe=Path.Combine(dir,"answer.exe");File.WriteAllText(source,code,new UTF8Encoding(false));
   var compile=Execute(CompilerPath,"-B"+Quote(Path.GetDirectoryName(CompilerPath))+" -Wall "+Quote(source)+" -o "+Quote(exe),"",15000,ct,dir);
   ev.Compiler=(compile.Error+compile.Output).Replace(source,"answer.c");
   if(compile.Failure!=""||compile.ExitCode!=0||!File.Exists(exe)){ev.Compiler=compile.Failure+"\r\n"+ev.Compiler;return ev;}
   ct.ThrowIfCancellationRequested();ev.Compiled=true;
   var tests=problem.Id>=101?ChallengeBank.Tests(problem.Id,seed):Bank.Tests(problem.Id,seed);int i=0;
   foreach(var test in tests){ct.ThrowIfCancellationRequested();var run=Execute(exe,"",test.Input,3000,ct,dir);ct.ThrowIfCancellationRequested();string why="";bool pass=run.Failure==""&&run.ExitCode==0&&Match(test.Expected,run.Output,out why,problem.Id==38||problem.Id==52||problem.Id==56||problem.Id==58||problem.Id==122||problem.Id==123||problem.Id==124);
    if(run.Failure!="")why=run.Failure+"。请检查死循环、除零、数组越界或非法内存访问。";
    if(!pass&&run.Failure==""&&why=="")why="运行未正常结束。";
    ev.Cases.Add(new CaseResult{Test=test,Run=run,Passed=pass,Diagnosis=why});progress(++i,tests.Count);
   }
   var normal=ev.Cases.Where(q=>!q.Test.Edge).ToList();var edges=ev.Cases.Where(q=>q.Test.Edge).ToList();
   ev.NormalScore=60.0*normal.Count(q=>q.Passed)/normal.Count;ev.EdgeScore=20.0*edges.Count(q=>q.Passed)/edges.Count;
   ev.QualityScore=10.0*ev.Cases.Count(q=>q.Passed&&q.Run.Healthy)/ev.Cases.Count;CodeMetrics.Apply(ev,problem.Reference);ev.Score=Math.Round(ev.NormalScore+ev.EdgeScore+ev.QualityScore+ev.LineScore+ev.SizeScore,1);
   return ev;
  }finally{try{Directory.Delete(dir,true);}catch{}}
 }
 public static string Report(Evaluation ev){return PracticeTools.Report(ev);}
}
