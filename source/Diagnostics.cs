using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
static class Diagnostics {
 // Translation is deterministic and keeps every original diagnostic verbatim.
 public static string Translate(string line){
  string l=line.ToLowerInvariant(); string advice="";
  if(l.Contains("undeclared")||l.Contains("undefined symbol"))advice="未声明的变量或未定义的符号。检查拼写、声明位置，以及是否包含对应头文件或实现函数。";
  else if(l.Contains("implicit declaration"))advice="函数在声明前被调用。请包含正确头文件或在调用之前写函数原型。";
  else if(l.Contains("redefinition")||l.Contains("defined twice"))advice="重复定义。检查同名变量、函数或结构体是否定义了两次。";
  else if(l.Contains("conflicting types")||l.Contains("incompatible"))advice="类型不兼容。检查声明与定义、参数、指针和返回值类型是否一致。";
  else if(l.Contains("too many")&&l.Contains("argument"))advice="函数实参数量过多，请与函数声明逐项核对。";
  else if(l.Contains("too few")&&l.Contains("argument"))advice="函数实参数量不足，请补齐需要的参数。";
  else if(l.Contains("lvalue"))advice="需要可赋值的左值。等号左边应是变量或可写的数组元素；比较时使用 ==。";
  else if(l.Contains("division by zero"))advice="检测到除零。使用 / 或 % 前必须确保除数不为 0。";
  else if(l.Contains("invalid")&&l.Contains("operand"))advice="运算对象类型无效。检查算术、指针和位运算操作数的类型。";
  else if(l.Contains("expected")){
   var m=Regex.Match(line,@"['""`]([^'""`]+)['""`]\s+expected|expected\s+['""`]([^'""`]+)['""`]");
   string token=m.Success?(m.Groups[1].Success?m.Groups[1].Value:m.Groups[2].Value):"";
   advice=token==";"?"这里需要分号 ;。也检查上一行是否遗漏分号。":token=="}"?"这里需要右花括号 }，请检查代码块是否配对。":token==")"?"这里需要右括号 )，请检查条件表达式或函数参数的括号。":token=="{"?"这里需要左花括号 {，请检查函数或代码块定义。":token=="expression"||l.Contains("expression")?"这里需要有效表达式。检查多余的运算符、空参数、括号和语句格式。":"语法解析需要 "+(token==""?"其他有效语法元素":token)+"。错误也可能来自上一行未闭合的括号、字符串或语句。";
  }
  else if(l.Contains("stray")||l.Contains("invalid character"))advice="存在无效字符。检查全角符号、中文引号及误输入的字符。";
  else if(l.Contains("unterminated")||l.Contains("missing terminating"))advice="字符串或注释未闭合，请补齐对应引号或 */。";
  else if(l.Contains("cannot convert")||l.Contains("cast"))advice="类型转换存在问题。核对源类型与目标类型，避免把整数当作有效地址。";
  else if(l.Contains("include file")||l.Contains("file not found"))advice="找不到头文件或文件。检查 #include 拼写及文件路径。";
  else if(l.Contains("array")&&l.Contains("size"))advice="数组大小无效。数组长度必须合法，不能为负数或超出可用内存。";
  else if(l.Contains("warning"))advice="编译警告。程序可能仍可运行，请按原文位置检查类型转换、声明或不安全用法。";
  else if(l.Contains("error")||l.Contains("fatal"))advice="编译或运行错误。请结合原文和行号定位；此条诊断暂未匹配专门译法。";
  else if(Regex.IsMatch(line,@"[A-Za-z]{3,}"))advice="未收录此诊断的准确译法。已保留英文原文，请结合输入、退出码和对应代码定位。";
  if(advice=="")return "";
  var location=Regex.Match(line,@"answer\.c:(\d+)(?::(\d+))?");
  return (location.Success?"第 "+location.Groups[1].Value+" 行"+(location.Groups[2].Success?"，第 "+location.Groups[2].Value+" 列":"")+"：":"")+advice;
 }
 public static string Explain(string raw){if(string.IsNullOrWhiteSpace(raw))return "无诊断信息。";var sb=new StringBuilder();foreach(string line in raw.Replace("\r\n","\n").Split('\n')){if(line=="")continue;sb.AppendLine(line);string cn=Translate(line);if(cn!="")sb.AppendLine("中文解释："+cn);}return sb.ToString().TrimEnd();}
}
