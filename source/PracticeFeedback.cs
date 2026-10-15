using System;
using System.Linq;
static class PracticeFeedback {
 public static string Warning(Evaluation ev){if(ev==null)return "";string score="本次得分 "+ev.Score.ToString("0.#")+" / 100 分。";
  if(!ev.Compiled)return "得分警告 · "+score+"编译未通过，请查看右侧中英文诊断，修改后重试。";
  if(ev.Cases.Any(c=>c.Run.Failure!=""||c.Run.ExitCode!=0))return "得分警告 · "+score+"部分程序运行失败，请查看失败用例的退出码和诊断。";
  if(ev.Score<60)return "得分警告 · "+score+"尚未达到60分，请对照实际输出与预期输出继续改进。";
  return "";
 }
}
