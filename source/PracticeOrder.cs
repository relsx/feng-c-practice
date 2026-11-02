using System;using System.Linq;using System.Collections.Generic;
static class PracticeOrder {
public static readonly int[][] Groups={new[]{1,2,3,201,205,4,202,210,5,6,7,10,212,213,8},new[]{11,12,214,9,215,13,14,208,203,211,204,209,207,16,18,15,20,17,19,206},new[]{21,32,33,37,22,29,23,24,30,25,40,39,26,31,36,34,35,27,28},new[]{41,42,43,216,217,218,44,46,47,224,231,222,225,229,230,223,219,220,221,233,226,227,228,232,235,234,45,48,49,50,38},new[]{51,55,59,56,58,53,255,246,247,250,251,248,252,249,253,254,57,52,54,77},new[]{243,60,238,236,237,239,240,241,242,244,245,73,75,74},new[]{63,64,61,62},new[]{67,68,69,65,66,72,71,70,76,79,80},new[]{260,259,258,256,257,82,83,78,81},new[]{84,85,86,87},new[]{88,89,90}};
public static readonly string[] Stages={@"01 · 输入输出与直接计算",@"02 · 比较、分支与边界",@"03 · 循环与基础数论",@"04 · 数组、遍历与统计",@"05 · 字符流与字符串",@"06 · 二维数组与矩阵",@"07 · 函数、递归与指针",@"08 · 查找、排序与组合算法",@"09 · 状态终端与数据结构",@"10 · 动态规划与路径搜索",@"11 · 综合项目考试"};
public static readonly int[] Ids=Groups.SelectMany(a=>a).ToArray();
static readonly Dictionary<int,int> positions=Ids.Select((id,i)=>new{id,i}).ToDictionary(x=>x.id,x=>x.i+1);
public static int Number(int id){int n;return positions.TryGetValue(id,out n)?n:id;}
public static List<Problem> Arrange(List<Problem> problems){var byId=problems.ToDictionary(p=>p.Id);var ordered=new List<Problem>();for(int g=0;g<Groups.Length;g++)foreach(int id in Groups[g]){var p=byId[id];p.Category=Stages[g];ordered.Add(p);}return ordered;}
}
