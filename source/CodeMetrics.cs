using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
sealed class CodeMetrics {
 public int Lines,Bytes;
 public static CodeMetrics Measure(string source){
  // Strings/chars are kept intact; only actual comments are removed.
  string clean=Regex.Replace(source,@"/\*[\s\S]*?\*/|//[^\r\n]*|""(?:\\.|[^""\\])*""|'(?:\\.|[^'\\])*'",m=>m.Value.StartsWith("//")||m.Value.StartsWith("/*")?Regex.Replace(m.Value,@"[^\r\n]"," "):m.Value);
  string literalsHidden=Regex.Replace(clean,@"""(?:\\.|[^""\\])*""|'(?:\\.|[^'\\])*'","V");
  int physical=literalsHidden.Split('\n').Count(s=>s.Trim().Length>0&&s.Trim()!="{"&&s.Trim()!="}");
  int logical=Regex.Matches(literalsHidden,@";|\b(if|for|while|switch|case|return)\b|^\s*#",RegexOptions.Multiline).Count;
  string compact=Regex.Replace(clean,@"""(?:\\.|[^""\\])*""|'(?:\\.|[^'\\])*'|\s+",m=>char.IsWhiteSpace(m.Value[0])?"":m.Value);
  return new CodeMetrics{Lines=Math.Max(physical,logical),Bytes=Encoding.UTF8.GetByteCount(compact)};
 }
 public static double Component(int value,int reference,double factor,int allowance){double limit=Math.Max(allowance,reference*factor);return value<=limit?5:Math.Max(0,5*(2-value/limit));}
 public static void Apply(Evaluation ev,string reference){var actual=Measure(ev.Code);var standard=Measure(reference);ev.CodeLines=actual.Lines;ev.CodeBytes=actual.Bytes;ev.ReferenceLines=standard.Lines;ev.ReferenceBytes=standard.Bytes;
  double correctness=ev.Cases.Count==0?0:ev.Cases.Count(c=>c.Passed)/(double)ev.Cases.Count;
  ev.LineScore=Component(actual.Lines,standard.Lines,1.6,12)*correctness;ev.SizeScore=Component(actual.Bytes,standard.Bytes,1.8,220)*correctness;
 }
 public static string Describe(Evaluation ev){return "有效行数 "+ev.CodeLines+" / 参考 "+ev.ReferenceLines+" · "+ev.LineScore.ToString("0.#")+"/5 分\n有效大小 "+ev.CodeBytes+" B / 参考 "+ev.ReferenceBytes+" B · "+ev.SizeScore.ToString("0.#")+"/5 分\n注释与空白不计大小；行数取有效文本行与语句计数的较大值。\n行数 ≤ 参考的 1.6 倍（至少 12 行）、大小 ≤ 1.8 倍（至少 220 B）不扣分；超出后线性扣分，到阈值两倍计 0。简洁度按测试通过率折算。";}
}
