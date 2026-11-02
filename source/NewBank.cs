using System;using System.Collections.Generic;using System.Linq;using System.Text;
static class NewBank {
public static bool IsNew(int id){return id>=201&&id<=260;}public static bool IsText(int id){return id==213||(id>=246&&id<=254);}
public static string Goal(int id){return Hints[id-201]+"\n\n解题前先确定输入、状态与输出；用最小规模、零值和边界数据手算验证。参考代码只使用stdio.h。";}
public static readonly string[] Hints={@"总价为份数乘单价再加配送费。",@"向上取整：(总量+7)/8，零件时需要零箱。",@"先加后减，再判断是否小于0。",@"先算需要的帐篷数，再减已有数，至少为0。",@"分别计算三项费用后相加。",@"分段计算电费，最后应用优惠与下限。",@"同时满足总分条件与三个单项条件。",@"取库存总数与容量的较小值。",@"可用容量不足按0处理，再取可拍数量与计划数量的较小值。",@"先汇总分钟，再使用整数除法。",@"先满足早场，再计算剩余票能满足多少晚场申请。",@"换算成总秒数，再依次整除和取余；分钟、秒输出两位。",@"printf的%03d会按宽度3补0，不改变原数值。",@"逐个比较并保留当前最大值。",@"不用abs函数，通过条件表达式计算两次绝对差。",@"正数累加，其他值跳过。",@"负数满足x<0。",@"比较应使用==。",@"比较a[i]*n与总和，无需浮点数。",@"保存历史最高值，相等不更新计数。",@"从第二项开始，手写绝对差并更新最大值。",@"极值用第一项初始化。",@"0基下标的偶数位置相加，奇数位置相减。",@"取余为0时才累加。",@"只有严格更大时才更新位置。",@"相等时延长当前段，否则从1重新计数。",@"与前项比较，不是求可以跳项的子序列。",@"跳过没有两个邻居的首尾站。",@"循环从1开始，避免越界。",@"下标加1才是题目权重。",@"负数取相反数后累加。",@"同时维护当前余额与历史最低余额。",@"不要把0当作正数或负数。",@"与前项不同才输出，非连续重复仍保留。",@"先更新累计和，再检查是否为0。",@"分别确认行数与列数，每一行的状态必须独立处理；判断边界后再访问相邻格。",@"分别确认行数与列数，每一行的状态必须独立处理；判断边界后再访问相邻格。",@"分别确认行数与列数，每一行的状态必须独立处理；判断边界后再访问相邻格。",@"分别确认行数与列数，每一行的状态必须独立处理；判断边界后再访问相邻格。",@"分别确认行数与列数，每一行的状态必须独立处理；判断边界后再访问相邻格。",@"分别确认行数与列数，每一行的状态必须独立处理；判断边界后再访问相邻格。",@"分别确认行数与列数，每一行的状态必须独立处理；判断边界后再访问相邻格。",@"分别确认行数与列数，每一行的状态必须独立处理；判断边界后再访问相邻格。",@"分别确认行数与列数，每一行的状态必须独立处理；判断边界后再访问相邻格。",@"分别确认行数与列数，每一行的状态必须独立处理；判断边界后再访问相邻格。",@"getchar返回int，可与EOF比较；读到换行或EOF结束。putchar输出单个字符，数字字符减去'0'才能转为数值。",@"getchar返回int，可与EOF比较；读到换行或EOF结束。putchar输出单个字符，数字字符减去'0'才能转为数值。",@"getchar返回int，可与EOF比较；读到换行或EOF结束。putchar输出单个字符，数字字符减去'0'才能转为数值。",@"getchar返回int，可与EOF比较；读到换行或EOF结束。putchar输出单个字符，数字字符减去'0'才能转为数值。",@"getchar返回int，可与EOF比较；读到换行或EOF结束。putchar输出单个字符，数字字符减去'0'才能转为数值。",@"getchar返回int，可与EOF比较；读到换行或EOF结束。putchar输出单个字符，数字字符减去'0'才能转为数值。",@"getchar返回int，可与EOF比较；读到换行或EOF结束。putchar输出单个字符，数字字符减去'0'才能转为数值。",@"getchar返回int，可与EOF比较；读到换行或EOF结束。putchar输出单个字符，数字字符减去'0'才能转为数值。",@"getchar返回int，可与EOF比较；读到换行或EOF结束。putchar输出单个字符，数字字符减去'0'才能转为数值。",@"getchar返回int，可与EOF比较；读到换行或EOF结束。putchar输出单个字符，数字字符减去'0'才能转为数值。",@"每次读取完整的一对参数；先判断命令，再更新状态。查询不能修改状态，失败的操作不能破坏原值。",@"每次读取完整的一对参数；先判断命令，再更新状态。查询不能修改状态，失败的操作不能破坏原值。",@"每次读取完整的一对参数；先判断命令，再更新状态。查询不能修改状态，失败的操作不能破坏原值。",@"每次读取完整的一对参数；先判断命令，再更新状态。查询不能修改状态，失败的操作不能破坏原值。",@"每次读取完整的一对参数；先判断命令，再更新状态。查询不能修改状态，失败的操作不能破坏原值。"};
public static void Add(List<Problem> a){
a.Add(new Problem(201,@"早餐团购",@"生活计算 · stdio",@"输入三个整数a b c（均为0～100）。食堂卖出a份早餐，每份b元，另收c元配送费。输出一个整数结果。",@"总价为份数乘单价再加配送费。",@"#include <stdio.h>
int main(void)  {
    int a,b,c;
    scanf(""%d%d%d"",&a,&b,&c);
    printf(""%d"",a*b+c);
    return 0;
}
"));
a.Add(new Problem(202,@"快递拼箱",@"生活计算 · stdio",@"输入三个整数a b c（均为0～100）。三个货架分别有a、b、c件包裹，每箱装8件。输出一个整数结果。",@"向上取整：(总量+7)/8，零件时需要零箱。",@"#include <stdio.h>
int main(void)  {
    int a,b,c;
    scanf(""%d%d%d"",&a,&b,&c);
    printf(""%d"",(a+b+c+7)/8);
    return 0;
}
"));
a.Add(new Problem(203,@"水站补给",@"生活计算 · stdio",@"输入三个整数a b c（均为0～100）。水站原有a升水，补入b升，取走c升；不足时剩余量记为0。输出一个整数结果。",@"先加后减，再判断是否小于0。",@"#include <stdio.h>
int main(void)  {
    int a,b,c;
    scanf(""%d%d%d"",&a,&b,&c);
    printf(""%d"",a+b>c?a+b-c:0);
    return 0;
}
"));
a.Add(new Problem(204,@"露营分组",@"生活计算 · stdio",@"输入三个整数a b c（均为0～100）。a名同学与b名老师露营，每组最多6人；已有c顶帐篷，每组需要一顶。输出一个整数结果。",@"先算需要的帐篷数，再减已有数，至少为0。",@"#include <stdio.h>
int main(void)  {
    int a,b,c;
    scanf(""%d%d%d"",&a,&b,&c);
    printf(""%d"",(a+b+5)/6>c?(a+b+5)/6-c:0);
    return 0;
}
"));
a.Add(new Problem(205,@"公交换乘",@"生活计算 · stdio",@"输入三个整数a b c（均为0～100）。小林乘坐a次普通公交、b次地铁、c次接驳车，单价依次为2、4、1元。输出一个整数结果。",@"分别计算三项费用后相加。",@"#include <stdio.h>
int main(void)  {
    int a,b,c;
    scanf(""%d%d%d"",&a,&b,&c);
    printf(""%d"",2*a+4*b+c);
    return 0;
}
"));
a.Add(new Problem(206,@"阶梯电费",@"生活计算 · stdio",@"输入三个整数a b c（均为0～100）。宿舍本月用了a度电，前30度每度2元，超出部分每度3元，另有b元服务费和c元优惠。优惠后的账单最低为0。输出一个整数结果。",@"分段计算电费，最后应用优惠与下限。",@"#include <stdio.h>
int main(void)  {
    int a,b,c;
    scanf(""%d%d%d"",&a,&b,&c);
    printf(""%d"",((a<=30?a*2:60+(a-30)*3)+b-c)>0?((a<=30?a*2:60+(a-30)*3)+b-c):0);
    return 0;
}
"));
a.Add(new Problem(207,@"活动达标",@"生活计算 · stdio",@"输入三个整数a b c（均为0～100）。三个活动站点积分为a、b、c。总分至少180且每站至少40才达标。输出一个整数结果。",@"同时满足总分条件与三个单项条件。",@"#include <stdio.h>
int main(void)  {
    int a,b,c;
    scanf(""%d%d%d"",&a,&b,&c);
    printf(""%d"",(a+b+c>=180&&a>=40&&b>=40&&c>=40)?1:0);
    return 0;
}
"));
a.Add(new Problem(208,@"库存装车",@"生活计算 · stdio",@"输入三个整数a b c（均为0～100）。仓库有a箱货，新增b箱，车辆最多装c箱。输出一个整数结果。",@"取库存总数与容量的较小值。",@"#include <stdio.h>
int main(void)  {
    int a,b,c;
    scanf(""%d%d%d"",&a,&b,&c);
    printf(""%d"",a+b<c?a+b:c);
    return 0;
}
"));
a.Add(new Problem(209,@"摄影存储",@"生活计算 · stdio",@"输入三个整数a b c（均为0～100）。存储卡容量为a MB，已有b MB文件，每张新照片占3 MB；计划拍c张。输出一个整数结果。",@"可用容量不足按0处理，再取可拍数量与计划数量的较小值。",@"#include <stdio.h>
int main(void)  {
    int a,b,c;
    scanf(""%d%d%d"",&a,&b,&c);
    printf(""%d"",((a>b?(a-b)/3:0)<c?(a>b?(a-b)/3:0):c));
    return 0;
}
"));
a.Add(new Problem(210,@"排班工时",@"生活计算 · stdio",@"输入三个整数a b c（均为0～100）。三名志愿者分别工作a、b、c分钟；每完整60分钟计一工时，不足部分不计。输出一个整数结果。",@"先汇总分钟，再使用整数除法。",@"#include <stdio.h>
int main(void)  {
    int a,b,c;
    scanf(""%d%d%d"",&a,&b,&c);
    printf(""%d"",(a+b+c)/60);
    return 0;
}
"));
a.Add(new Problem(211,@"展馆预约",@"生活计算 · stdio",@"输入三个整数a b c（均为0～100）。展馆有a张余票，早场申请b张、晚场申请c张，按早场优先分配。输出一个整数结果。",@"先满足早场，再计算剩余票能满足多少晚场申请。",@"#include <stdio.h>
int main(void)  {
    int a,b,c;
    scanf(""%d%d%d"",&a,&b,&c);
    printf(""%d"",a>b?(a-b<c?a-b:c):0);
    return 0;
}
"));
a.Add(new Problem(212,@"时钟校准",@"生活计算 · stdio",@"输入三个整数a b c（均为0～100）。设备记录了a小时、b分钟、c秒，分钟和秒可能超过59，需要换算成规范时刻（小时不取模）。输出规范时刻，格式为小时:两位分钟:两位秒。",@"换算成总秒数，再依次整除和取余；分钟、秒输出两位。",@"#include <stdio.h>
int main(void)  {
    int a,b,c;
    scanf(""%d%d%d"",&a,&b,&c);
    int t=a*3600+b*60+c;
    printf(""%d:%02d:%02d"",t/3600,t/60%60,t%60);
    return 0;
}
"));
a.Add(new Problem(213,@"三位校验码",@"生活计算 · stdio",@"输入三个整数a b c（均为0～100）。展会用(a+2*b+3*c)除以1000的余数作为校验码，不足三位要补前导0。输出三位校验码。",@"printf的%03d会按宽度3补0，不改变原数值。",@"#include <stdio.h>
int main(void)  {
    int a,b,c;
    scanf(""%d%d%d"",&a,&b,&c);
    printf(""%03d"",(a+2*b+3*c)%1000);
    return 0;
}
"));
a.Add(new Problem(214,@"花坛围栏",@"生活计算 · stdio",@"输入三个整数a b c（均为0～100）。三段围栏长度分别为a、b、c米，需要另外购买与最长一段等长的备用围栏。输出一个整数结果。",@"逐个比较并保留当前最大值。",@"#include <stdio.h>
int main(void)  {
    int a,b,c;
    scanf(""%d%d%d"",&a,&b,&c);
    printf(""%d"",a>b?(a>c?a:c):(b>c?b:c));
    return 0;
}
"));
a.Add(new Problem(215,@"训练间隔",@"生活计算 · stdio",@"输入三个整数a b c（均为0～100）。三天的训练步数依次为a、b、c，教练统计相邻两天步数变化的绝对值之和。输出一个整数结果。",@"不用abs函数，通过条件表达式计算两次绝对差。",@"#include <stdio.h>
int main(void)  {
    int a,b,c;
    scanf(""%d%d%d"",&a,&b,&c);
    printf(""%d"",(a>b?a-b:b-a)+(b>c?b-c:c-b));
    return 0;
}
"));
a.Add(new Problem(216,@"回收站收益",@"记录与数组 · stdio",@"仓储与观测中心需要处理一组记录。输入n（1～40），随后n个整数（-100～100）。只统计正数代表的回收收益，输出其总和。",@"正数累加，其他值跳过。",@"#include <stdio.h>
int main(void)  {
    int n,a[40],i;
    scanf(""%d"",&n);
    for(i=0;i<n;i++)scanf(""%d"",&a[i]);
    int s=0;
    for(i=0;i<n;i++)if(a[i]>0)s+=a[i];
    printf(""%d"",s);
    return 0;
}
"));
a.Add(new Problem(217,@"支出笔数",@"记录与数组 · stdio",@"仓储与观测中心需要处理一组记录。输入n（1～40），随后n个整数（-100～100）。负数表示支出，统计支出笔数。",@"负数满足x<0。",@"#include <stdio.h>
int main(void)  {
    int n,a[40],i;
    scanf(""%d"",&n);
    for(i=0;i<n;i++)scanf(""%d"",&a[i]);
    int s=0;
    for(i=0;i<n;i++)if(a[i]<0)s++;
    printf(""%d"",s);
    return 0;
}
"));
a.Add(new Problem(218,@"空柜清点",@"记录与数组 · stdio",@"仓储与观测中心需要处理一组记录。输入n（1～40），随后n个整数（-100～100）。每项表示柜内物品数的传感器读数，0表示空柜，统计空柜数量。",@"比较应使用==。",@"#include <stdio.h>
int main(void)  {
    int n,a[40],i;
    scanf(""%d"",&n);
    for(i=0;i<n;i++)scanf(""%d"",&a[i]);
    int s=0;
    for(i=0;i<n;i++)if(a[i]==0)s++;
    printf(""%d"",s);
    return 0;
}
"));
a.Add(new Problem(219,@"温度均线上方",@"记录与数组 · stdio",@"仓储与观测中心需要处理一组记录。输入n（1～40），随后n个整数（-100～100）。输出严格高于本组平均温度的读数数量。",@"比较a[i]*n与总和，无需浮点数。",@"#include <stdio.h>
int main(void)  {
    int n,a[40],i;
    scanf(""%d"",&n);
    for(i=0;i<n;i++)scanf(""%d"",&a[i]);
    int s=0,k=0;
    for(i=0;i<n;i++)s+=a[i];
    for(i=0;i<n;i++)if(a[i]*n>s)k++;
    printf(""%d"",k);
    return 0;
}
"));
a.Add(new Problem(220,@"逐日破纪录",@"记录与数组 · stdio",@"仓储与观测中心需要处理一组记录。输入n（1～40），随后n个整数（-100～100）。首项算一次纪录，此后只有严格超过此前全部读数时才记新纪录，输出纪录次数。",@"保存历史最高值，相等不更新计数。",@"#include <stdio.h>
int main(void)  {
    int n,a[40],i;
    scanf(""%d"",&n);
    for(i=0;i<n;i++)scanf(""%d"",&a[i]);
    int m=a[0],k=1;
    for(i=1;i<n;i++)if(a[i]>m) {
        m=a[i];
        k++;
    }
    printf(""%d"",k);
    return 0;
}
"));
a.Add(new Problem(221,@"仪表最大跳变",@"记录与数组 · stdio",@"仓储与观测中心需要处理一组记录。输入n（1～40），随后n个整数（-100～100）。输出相邻读数绝对差的最大值，只有一个读数时为0。",@"从第二项开始，手写绝对差并更新最大值。",@"#include <stdio.h>
int main(void)  {
    int n,a[40],i;
    scanf(""%d"",&n);
    for(i=0;i<n;i++)scanf(""%d"",&a[i]);
    int m=0,d;
    for(i=1;i<n;i++) {
        d=a[i]-a[i-1];
        if(d<0)d=-d;
        if(d>m)m=d;
    }
    printf(""%d"",m);
    return 0;
}
"));
a.Add(new Problem(222,@"海拔落差",@"记录与数组 · stdio",@"仓储与观测中心需要处理一组记录。输入n（1～40），随后n个整数（-100～100）。输出最高海拔与最低海拔的差。",@"极值用第一项初始化。",@"#include <stdio.h>
int main(void)  {
    int n,a[40],i;
    scanf(""%d"",&n);
    for(i=0;i<n;i++)scanf(""%d"",&a[i]);
    int lo=a[0],hi=a[0];
    for(i=1;i<n;i++) {
        if(a[i]<lo)lo=a[i];
        if(a[i]>hi)hi=a[i];
    }
    printf(""%d"",hi-lo);
    return 0;
}
"));
a.Add(new Problem(223,@"交替记账",@"记录与数组 · stdio",@"仓储与观测中心需要处理一组记录。输入n（1～40），随后n个整数（-100～100）。从第一项开始按加、减、加、减的顺序合并读数。",@"0基下标的偶数位置相加，奇数位置相减。",@"#include <stdio.h>
int main(void)  {
    int n,a[40],i;
    scanf(""%d"",&n);
    for(i=0;i<n;i++)scanf(""%d"",&a[i]);
    int s=0;
    for(i=0;i<n;i++)s+=(i%2==0?a[i]:-a[i]);
    printf(""%d"",s);
    return 0;
}
"));
a.Add(new Problem(224,@"偶数货品汇总",@"记录与数组 · stdio",@"仓储与观测中心需要处理一组记录。输入n（1～40），随后n个整数（-100～100）。只累计偶数读数，包括负偶数。",@"取余为0时才累加。",@"#include <stdio.h>
int main(void)  {
    int n,a[40],i;
    scanf(""%d"",&n);
    for(i=0;i<n;i++)scanf(""%d"",&a[i]);
    int s=0;
    for(i=0;i<n;i++)if(a[i]%2==0)s+=a[i];
    printf(""%d"",s);
    return 0;
}
"));
a.Add(new Problem(225,@"最早峰值站",@"记录与数组 · stdio",@"仓储与观测中心需要处理一组记录。输入n（1～40），随后n个整数（-100～100）。输出最大读数首次出现的1基站点编号。",@"只有严格更大时才更新位置。",@"#include <stdio.h>
int main(void)  {
    int n,a[40],i;
    scanf(""%d"",&n);
    for(i=0;i<n;i++)scanf(""%d"",&a[i]);
    int p=0;
    for(i=1;i<n;i++)if(a[i]>a[p])p=i;
    printf(""%d"",p+1);
    return 0;
}
"));
a.Add(new Problem(226,@"连续稳定时段",@"记录与数组 · stdio",@"仓储与观测中心需要处理一组记录。输入n（1～40），随后n个整数（-100～100）。输出连续相等读数的最长长度。",@"相等时延长当前段，否则从1重新计数。",@"#include <stdio.h>
int main(void)  {
    int n,a[40],i;
    scanf(""%d"",&n);
    for(i=0;i<n;i++)scanf(""%d"",&a[i]);
    int k=1,m=1;
    for(i=1;i<n;i++) {
        k=a[i]==a[i-1]?k+1:1;
        if(k>m)m=k;
    }
    printf(""%d"",m);
    return 0;
}
"));
a.Add(new Problem(227,@"连续升温时段",@"记录与数组 · stdio",@"仓储与观测中心需要处理一组记录。输入n（1～40），随后n个整数（-100～100）。输出连续严格升温读数段的最长长度。",@"与前项比较，不是求可以跳项的子序列。",@"#include <stdio.h>
int main(void)  {
    int n,a[40],i;
    scanf(""%d"",&n);
    for(i=0;i<n;i++)scanf(""%d"",&a[i]);
    int k=1,m=1;
    for(i=1;i<n;i++) {
        k=a[i]>a[i-1]?k+1:1;
        if(k>m)m=k;
    }
    printf(""%d"",m);
    return 0;
}
"));
a.Add(new Problem(228,@"山脊观测",@"记录与数组 · stdio",@"仓储与观测中心需要处理一组记录。输入n（1～40），随后n个整数（-100～100）。只考虑中间站，统计严格大于左右两邻居的站点数量。",@"跳过没有两个邻居的首尾站。",@"#include <stdio.h>
int main(void)  {
    int n,a[40],i;
    scanf(""%d"",&n);
    for(i=0;i<n;i++)scanf(""%d"",&a[i]);
    int k=0;
    for(i=1;i+1<n;i++)if(a[i]>a[i-1]&&a[i]>a[i+1])k++;
    printf(""%d"",k);
    return 0;
}
"));
a.Add(new Problem(229,@"站间净变化",@"记录与数组 · stdio",@"仓储与观测中心需要处理一组记录。输入n（1～40），随后n个整数（-100～100）。依次输出相邻两站的后项减前项；只有一站时输出0。",@"循环从1开始，避免越界。",@"#include <stdio.h>
int main(void)  {
    int n,a[40],i;
    scanf(""%d"",&n);
    for(i=0;i<n;i++)scanf(""%d"",&a[i]);
    if(n==1)printf(""0"");
    for(i=1;i<n;i++)printf(""%d "",a[i]-a[i-1]);
    return 0;
}
"));
a.Add(new Problem(230,@"月度加权表现",@"记录与数组 · stdio",@"仓储与观测中心需要处理一组记录。输入n（1～40），随后n个整数（-100～100）。第i项（从1编号）的权重为i，输出加权和。",@"下标加1才是题目权重。",@"#include <stdio.h>
int main(void)  {
    int n,a[40],i;
    scanf(""%d"",&n);
    for(i=0;i<n;i++)scanf(""%d"",&a[i]);
    int s=0;
    for(i=0;i<n;i++)s+=(i+1)*a[i];
    printf(""%d"",s);
    return 0;
}
"));
a.Add(new Problem(231,@"搬运总距离",@"记录与数组 · stdio",@"仓储与观测中心需要处理一组记录。输入n（1～40），随后n个整数（-100～100）。正负号表示方向，输出所有读数绝对值之和。",@"负数取相反数后累加。",@"#include <stdio.h>
int main(void)  {
    int n,a[40],i;
    scanf(""%d"",&n);
    for(i=0;i<n;i++)scanf(""%d"",&a[i]);
    int s=0;
    for(i=0;i<n;i++)s+=a[i]<0?-a[i]:a[i];
    printf(""%d"",s);
    return 0;
}
"));
a.Add(new Problem(232,@"账户最低水位",@"记录与数组 · stdio",@"仓储与观测中心需要处理一组记录。输入n（1～40），随后n个整数（-100～100）。余额从0开始依次加上各笔变动，输出过程中出现的最低余额（包括初始0）。",@"同时维护当前余额与历史最低余额。",@"#include <stdio.h>
int main(void)  {
    int n,a[40],i;
    scanf(""%d"",&n);
    for(i=0;i<n;i++)scanf(""%d"",&a[i]);
    int s=0,m=0;
    for(i=0;i<n;i++) {
        s+=a[i];
        if(s<m)m=s;
    }
    printf(""%d"",m);
    return 0;
}
"));
a.Add(new Problem(233,@"相邻同号信号",@"记录与数组 · stdio",@"仓储与观测中心需要处理一组记录。输入n（1～40），随后n个整数（-100～100）。统计相邻两项同为正数或同为负数的对数，0不计。",@"不要把0当作正数或负数。",@"#include <stdio.h>
int main(void)  {
    int n,a[40],i;
    scanf(""%d"",&n);
    for(i=0;i<n;i++)scanf(""%d"",&a[i]);
    int k=0;
    for(i=1;i<n;i++)if((a[i]>0&&a[i-1]>0)||(a[i]<0&&a[i-1]<0))k++;
    printf(""%d"",k);
    return 0;
}
"));
a.Add(new Problem(234,@"离线去噪",@"记录与数组 · stdio",@"仓储与观测中心需要处理一组记录。输入n（1～40），随后n个整数（-100～100）。连续重复读数只保留一个，按顺序输出压缩后的序列。",@"与前项不同才输出，非连续重复仍保留。",@"#include <stdio.h>
int main(void)  {
    int n,a[40],i;
    scanf(""%d"",&n);
    for(i=0;i<n;i++)scanf(""%d"",&a[i]);
    for(i=0;i<n;i++)if(i==0||a[i]!=a[i-1])printf(""%d "",a[i]);
    return 0;
}
"));
a.Add(new Problem(235,@"库存平衡检查",@"记录与数组 · stdio",@"仓储与观测中心需要处理一组记录。输入n（1～40），随后n个整数（-100～100）。依次累计货品进出，统计非空前缀累计为0的次数。",@"先更新累计和，再检查是否为0。",@"#include <stdio.h>
int main(void)  {
    int n,a[40],i;
    scanf(""%d"",&n);
    for(i=0;i<n;i++)scanf(""%d"",&a[i]);
    int s=0,k=0;
    for(i=0;i<n;i++) {
        s+=a[i];
        if(s==0)k++;
    }
    printf(""%d"",k);
    return 0;
}
"));
a.Add(new Problem(236,@"温室外圈",@"格网巡检 · stdio",@"科研站收到一块矩形格网。输入r c（各1～6），再按行输入r*c个整数（0～9）。输出边界格子的数值之和，角点只计一次。",@"分别确认行数与列数，每一行的状态必须独立处理；判断边界后再访问相邻格。",@"#include <stdio.h>
int main(void)  {
    int r,c,a[6][6],i,j,s=0;
    scanf(""%d%d"",&r,&c);
    for(i=0;i<r;i++)for(j=0;j<c;j++)scanf(""%d"",&a[i][j]);
    for(i=0;i<r;i++)for(j=0;j<c;j++) {
        if(i==0||i==r-1||j==0||j==c-1)s+=a[i][j];
    }
    printf(""%d"",s);
    return 0;
}
"));
a.Add(new Problem(237,@"温室内圈",@"格网巡检 · stdio",@"科研站收到一块矩形格网。输入r c（各1～6），再按行输入r*c个整数（0～9）。输出非边界格子的数值之和，无内部格子时为0。",@"分别确认行数与列数，每一行的状态必须独立处理；判断边界后再访问相邻格。",@"#include <stdio.h>
int main(void)  {
    int r,c,a[6][6],i,j,s=0;
    scanf(""%d%d"",&r,&c);
    for(i=0;i<r;i++)for(j=0;j<c;j++)scanf(""%d"",&a[i][j]);
    for(i=0;i<r;i++)for(j=0;j<c;j++) {
        if(i>0&&i<r-1&&j>0&&j<c-1)s+=a[i][j];
    }
    printf(""%d"",s);
    return 0;
}
"));
a.Add(new Problem(238,@"亮灯清点",@"格网巡检 · stdio",@"科研站收到一块矩形格网。输入r c（各1～6），再按行输入r*c个整数（0～9）。非0表示亮灯，输出亮灯数量。",@"分别确认行数与列数，每一行的状态必须独立处理；判断边界后再访问相邻格。",@"#include <stdio.h>
int main(void)  {
    int r,c,a[6][6],i,j,s=0;
    scanf(""%d%d"",&r,&c);
    for(i=0;i<r;i++)for(j=0;j<c;j++)scanf(""%d"",&a[i][j]);
    for(i=0;i<r;i++)for(j=0;j<c;j++) {
        if(a[i][j]!=0)s++;
    }
    printf(""%d"",s);
    return 0;
}
"));
a.Add(new Problem(239,@"棋盘能量",@"格网巡检 · stdio",@"科研站收到一块矩形格网。输入r c（各1～6），再按行输入r*c个整数（0～9）。行列均从0编号，偶数行列和位置相加，奇数行列和位置相减。",@"分别确认行数与列数，每一行的状态必须独立处理；判断边界后再访问相邻格。",@"#include <stdio.h>
int main(void)  {
    int r,c,a[6][6],i,j,s=0;
    scanf(""%d%d"",&r,&c);
    for(i=0;i<r;i++)for(j=0;j<c;j++)scanf(""%d"",&a[i][j]);
    for(i=0;i<r;i++)for(j=0;j<c;j++) {
        s+=(i+j)%2==0?a[i][j]:-a[i][j];
    }
    printf(""%d"",s);
    return 0;
}
"));
a.Add(new Problem(240,@"货架行峰值",@"格网巡检 · stdio",@"科研站收到一块矩形格网。输入r c（各1～6），再按行输入r*c个整数（0～9）。每行输出该行最大值，共r项。",@"分别确认行数与列数，每一行的状态必须独立处理；判断边界后再访问相邻格。",@"#include <stdio.h>
int main(void)  {
    int r,c,a[6][6],i,j,s=0;
    scanf(""%d%d"",&r,&c);
    for(i=0;i<r;i++)for(j=0;j<c;j++)scanf(""%d"",&a[i][j]);
    for(i=0;i<r;i++) {
        s=a[i][0];
        for(j=1;j<c;j++)if(a[i][j]>s)s=a[i][j];
        printf(""%d "",s);
    }
    return 0;
}
"));
a.Add(new Problem(241,@"水质列低值",@"格网巡检 · stdio",@"科研站收到一块矩形格网。输入r c（各1～6），再按行输入r*c个整数（0～9）。每列输出该列最小值，共c项。",@"分别确认行数与列数，每一行的状态必须独立处理；判断边界后再访问相邻格。",@"#include <stdio.h>
int main(void)  {
    int r,c,a[6][6],i,j,s=0;
    scanf(""%d%d"",&r,&c);
    for(i=0;i<r;i++)for(j=0;j<c;j++)scanf(""%d"",&a[i][j]);
    for(j=0;j<c;j++) {
        s=a[0][j];
        for(i=1;i<r;i++)if(a[i][j]<s)s=a[i][j];
        printf(""%d "",s);
    }
    return 0;
}
"));
a.Add(new Problem(242,@"最忙装配线",@"格网巡检 · stdio",@"科研站收到一块矩形格网。输入r c（各1～6），再按行输入r*c个整数（0～9）。输出行总和最大的行的1基编号，同和选择最早一行。",@"分别确认行数与列数，每一行的状态必须独立处理；判断边界后再访问相邻格。",@"#include <stdio.h>
int main(void)  {
    int r,c,a[6][6],i,j,s=0;
    scanf(""%d%d"",&r,&c);
    for(i=0;i<r;i++)for(j=0;j<c;j++)scanf(""%d"",&a[i][j]);
    int best=-1,p=0;
    for(i=0;i<r;i++) {
        s=0;
        for(j=0;j<c;j++)s+=a[i][j];
        if(s>best) {
            best=s;
            p=i;
        }
    }
    printf(""%d"",p+1);
    return 0;
}
"));
a.Add(new Problem(243,@"格网总容量",@"格网巡检 · stdio",@"科研站收到一块矩形格网。输入r c（各1～6），再按行输入r*c个整数（0～9）。每格数值表示可放置的物品数，输出总容量。",@"分别确认行数与列数，每一行的状态必须独立处理；判断边界后再访问相邻格。",@"#include <stdio.h>
int main(void)  {
    int r,c,a[6][6],i,j,s=0;
    scanf(""%d%d"",&r,&c);
    for(i=0;i<r;i++)for(j=0;j<c;j++)scanf(""%d"",&a[i][j]);
    for(i=0;i<r;i++)for(j=0;j<c;j++) {
        s+=a[i][j];
    }
    printf(""%d"",s);
    return 0;
}
"));
a.Add(new Problem(244,@"列权重检测",@"格网巡检 · stdio",@"科研站收到一块矩形格网。输入r c（各1～6），再按行输入r*c个整数（0～9）。第j列（从1编号）权重为j，输出所有格子的加权和。",@"分别确认行数与列数，每一行的状态必须独立处理；判断边界后再访问相邻格。",@"#include <stdio.h>
int main(void)  {
    int r,c,a[6][6],i,j,s=0;
    scanf(""%d%d"",&r,&c);
    for(i=0;i<r;i++)for(j=0;j<c;j++)scanf(""%d"",&a[i][j]);
    for(i=0;i<r;i++)for(j=0;j<c;j++) {
        s+=(j+1)*a[i][j];
    }
    printf(""%d"",s);
    return 0;
}
"));
a.Add(new Problem(245,@"横向巡检",@"格网巡检 · stdio",@"科研站收到一块矩形格网。输入r c（各1～6），再按行输入r*c个整数（0～9）。统计每行内部相邻两格数值不同的格对数量。",@"分别确认行数与列数，每一行的状态必须独立处理；判断边界后再访问相邻格。",@"#include <stdio.h>
int main(void)  {
    int r,c,a[6][6],i,j,s=0;
    scanf(""%d%d"",&r,&c);
    for(i=0;i<r;i++)for(j=0;j<c;j++)scanf(""%d"",&a[i][j]);
    for(i=0;i<r;i++)for(j=0;j<c;j++) {
        if(j>0&&a[i][j]!=a[i][j-1])s++;
    }
    printf(""%d"",s);
    return 0;
}
"));
a.Add(new Problem(246,@"标签数字合计",@"字符流 · stdio",@"通信终端收到一行标签。输入一行长度0～80的ASCII文本，仅含小写字母、数字与空格，可以为空。将所有数字字符按数值相加，输出总和。",@"getchar返回int，可与EOF比较；读到换行或EOF结束。putchar输出单个字符，数字字符减去'0'才能转为数值。",@"#include <stdio.h>
int main(void)  {
    int ch;
    int s=0;
    while((ch=getchar())!=EOF&&ch!='\n')if(ch>='0'&&ch<='9')s+=ch-'0';
    printf(""%d"",s);
    return 0;
}
"));
a.Add(new Problem(247,@"标签去空格",@"字符流 · stdio",@"通信终端收到一行标签。输入一行长度0～80的ASCII文本，仅含小写字母、数字与空格，可以为空。移除所有空格，输出其他字符。",@"getchar返回int，可与EOF比较；读到换行或EOF结束。putchar输出单个字符，数字字符减去'0'才能转为数值。",@"#include <stdio.h>
int main(void)  {
    int ch;
    while((ch=getchar())!=EOF&&ch!='\n')if(ch!=' ')putchar(ch);
    return 0;
}
"));
a.Add(new Problem(248,@"编号连续段",@"字符流 · stdio",@"通信终端收到一行标签。输入一行长度0～80的ASCII文本，仅含小写字母、数字与空格，可以为空。输出连续数字字符段的最长长度，没有数字为0。",@"getchar返回int，可与EOF比较；读到换行或EOF结束。putchar输出单个字符，数字字符减去'0'才能转为数值。",@"#include <stdio.h>
int main(void)  {
    int ch;
    int k=0,m=0;
    while((ch=getchar())!=EOF&&ch!='\n') {
        k=ch>='0'&&ch<='9'?k+1:0;
        if(k>m)m=k;
    }
    printf(""%d"",m);
    return 0;
}
"));
a.Add(new Problem(249,@"名牌缩写",@"字符流 · stdio",@"通信终端收到一行标签。输入一行长度0～80的ASCII文本，仅含小写字母、数字与空格，可以为空。每个非空格单词输出首字符，按原顺序拼接，不加空格。",@"getchar返回int，可与EOF比较；读到换行或EOF结束。putchar输出单个字符，数字字符减去'0'才能转为数值。",@"#include <stdio.h>
int main(void)  {
    int ch;
    int first=1;
    while((ch=getchar())!=EOF&&ch!='\n') {
        if(ch==' ')first=1;
        else {
            if(first)putchar(ch);
            first=0;
        }
    }
    return 0;
}
"));
a.Add(new Problem(250,@"脱敏标签",@"字符流 · stdio",@"通信终端收到一行标签。输入一行长度0～80的ASCII文本，仅含小写字母、数字与空格，可以为空。将所有数字字符替换为*，其他字符保留。",@"getchar返回int，可与EOF比较；读到换行或EOF结束。putchar输出单个字符，数字字符减去'0'才能转为数值。",@"#include <stdio.h>
int main(void)  {
    int ch;
    while((ch=getchar())!=EOF&&ch!='\n')putchar(ch>='0'&&ch<='9'?'*':ch);
    return 0;
}
"));
a.Add(new Problem(251,@"循环字母密文",@"字符流 · stdio",@"通信终端收到一行标签。输入一行长度0～80的ASCII文本，仅含小写字母、数字与空格，可以为空。小写字母向后移动一位，z变a，数字和空格不变。",@"getchar返回int，可与EOF比较；读到换行或EOF结束。putchar输出单个字符，数字字符减去'0'才能转为数值。",@"#include <stdio.h>
int main(void)  {
    int ch;
    while((ch=getchar())!=EOF&&ch!='\n')putchar(ch>='a'&&ch<='z'?(ch=='z'?'a':ch+1):ch);
    return 0;
}
"));
a.Add(new Problem(252,@"重复信号抑制",@"字符流 · stdio",@"通信终端收到一行标签。输入一行长度0～80的ASCII文本，仅含小写字母、数字与空格，可以为空。连续相同字符只输出一次（包括空格）。",@"getchar返回int，可与EOF比较；读到换行或EOF结束。putchar输出单个字符，数字字符减去'0'才能转为数值。",@"#include <stdio.h>
int main(void)  {
    int ch;
    int prev=-1;
    while((ch=getchar())!=EOF&&ch!='\n') {
        if(ch!=prev)putchar(ch);
        prev=ch;
    }
    return 0;
}
"));
a.Add(new Problem(253,@"名片边缘清理",@"字符流 · stdio",@"通信终端收到一行标签。输入一行长度0～80的ASCII文本，仅含小写字母、数字与空格，可以为空。删除首尾空格，内部空格保持不变。",@"getchar返回int，可与EOF比较；读到换行或EOF结束。putchar输出单个字符，数字字符减去'0'才能转为数值。",@"#include <stdio.h>
int main(void)  {
    int ch;
    char a[81];
    int n=0,l=0;
    while((ch=getchar())!=EOF&&ch!='\n')a[n++]=(char)ch;
    while(l<n&&a[l]==' ')l++;
    while(n>l&&a[n-1]==' ')n--;
    while(l<n)putchar(a[l++]);
    return 0;
}
"));
a.Add(new Problem(254,@"公告空格整理",@"字符流 · stdio",@"通信终端收到一行标签。输入一行长度0～80的ASCII文本，仅含小写字母、数字与空格，可以为空。删除首尾空格，并把内部连续空格合并成一个。",@"getchar返回int，可与EOF比较；读到换行或EOF结束。putchar输出单个字符，数字字符减去'0'才能转为数值。",@"#include <stdio.h>
int main(void)  {
    int ch;
    int started=0,pending=0;
    while((ch=getchar())!=EOF&&ch!='\n') {
        if(ch==' ') {
            if(started)pending=1;
        }
        else {
            if(pending)putchar(' ');
            putchar(ch);
            started=1;
            pending=0;
        }
    }
    return 0;
}
"));
a.Add(new Problem(255,@"传输校验和",@"字符流 · stdio",@"通信终端收到一行标签。输入一行长度0～80的ASCII文本，仅含小写字母、数字与空格，可以为空。输出整行所有字符的ASCII值之和，包括空格。",@"getchar返回int，可与EOF比较；读到换行或EOF结束。putchar输出单个字符，数字字符减去'0'才能转为数值。",@"#include <stdio.h>
int main(void)  {
    int ch;
    int sum=0;
    while((ch=getchar())!=EOF&&ch!='\n')sum+=ch;
    printf(""%d"",sum);
    return 0;
}
"));
a.Add(new Problem(256,@"校园钱包",@"情境小终端 · stdio",@"社区服务站需要一个可连续处理命令的小终端。输入q（1～30），随后q行op x（op为1、2、3，x为0～50）。余额初始0。命令1 x充值，2 x仅在余额足够时扣款，3 x查询余额（忽略x）。每次查询独占一行，其余操作不输出；数据保证至少一次查询。",@"每次读取完整的一对参数；先判断命令，再更新状态。查询不能修改状态，失败的操作不能破坏原值。",@"#include <stdio.h>
int main(void)  {
    int q,op,x,v=0;
    scanf(""%d"",&q);
    while(q--) {
        scanf(""%d%d"",&op,&x);
        if(op==1)v+=x;
        else if(op==2&&v>=x)v-=x;
        if(op==3)printf(""%d\n"",v);
    }
    return 0;
}
"));
a.Add(new Problem(257,@"限容量仓柜",@"情境小终端 · stdio",@"社区服务站需要一个可连续处理命令的小终端。输入q（1～30），随后q行op x（op为1、2、3，x为0～50）。库存初始0，上限100。命令1 x补入（超限只补到100），2 x取走（不足只取到0），3 x查询。每次查询独占一行，其余操作不输出；数据保证至少一次查询。",@"每次读取完整的一对参数；先判断命令，再更新状态。查询不能修改状态，失败的操作不能破坏原值。",@"#include <stdio.h>
int main(void)  {
    int q,op,x,v=0;
    scanf(""%d"",&q);
    while(q--) {
        scanf(""%d%d"",&op,&x);
        if(op==1) {
            v+=x;
            if(v>100)v=100;
        }
        else if(op==2) {
            v-=x;
            if(v<0)v=0;
        }
        if(op==3)printf(""%d\n"",v);
    }
    return 0;
}
"));
a.Add(new Problem(258,@"候车人数",@"情境小终端 · stdio",@"社区服务站需要一个可连续处理命令的小终端。输入q（1～30），随后q行op x（op为1、2、3，x为0～50）。人数初始0。命令1 x新增候车者，2 x车辆接走最多x人，3 x查询人数。每次查询独占一行，其余操作不输出；数据保证至少一次查询。",@"每次读取完整的一对参数；先判断命令，再更新状态。查询不能修改状态，失败的操作不能破坏原值。",@"#include <stdio.h>
int main(void)  {
    int q,op,x,v=0;
    scanf(""%d"",&q);
    while(q--) {
        scanf(""%d%d"",&op,&x);
        if(op==1)v+=x;
        else if(op==2) {
            v-=x;
            if(v<0)v=0;
        }
        if(op==3)printf(""%d\n"",v);
    }
    return 0;
}
"));
a.Add(new Problem(259,@"积分纪录台",@"情境小终端 · stdio",@"社区服务站需要一个可连续处理命令的小终端。输入q（1～30），随后q行op x（op为1、2、3，x为0～50）。纪录初始0。命令1 x提交成绩并保留最高纪录，2 x将纪录重置为x，3 x查询纪录。每次查询独占一行，其余操作不输出；数据保证至少一次查询。",@"每次读取完整的一对参数；先判断命令，再更新状态。查询不能修改状态，失败的操作不能破坏原值。",@"#include <stdio.h>
int main(void)  {
    int q,op,x,v=0;
    scanf(""%d"",&q);
    while(q--) {
        scanf(""%d%d"",&op,&x);
        if(op==1&&x>v)v=x;
        else if(op==2)v=x;
        if(op==3)printf(""%d\n"",v);
    }
    return 0;
}
"));
a.Add(new Problem(260,@"温控器终端",@"情境小终端 · stdio",@"社区服务站需要一个可连续处理命令的小终端。输入q（1～30），随后q行op x（op为1、2、3，x为0～50）。温度初始0。命令1 x升温x度，2 x设为x度，3 x查询当前温度。每次查询独占一行，其余操作不输出；数据保证至少一次查询。",@"每次读取完整的一对参数；先判断命令，再更新状态。查询不能修改状态，失败的操作不能破坏原值。",@"#include <stdio.h>
int main(void)  {
    int q,op,x,v=0;
    scanf(""%d"",&q);
    while(q--) {
        scanf(""%d%d"",&op,&x);
        if(op==1)v+=x;
        else if(op==2)v=x;
        if(op==3)printf(""%d\n"",v);
    }
    return 0;
}
"));
}
public static TestCase Generate(int id,Random rnd,int e){int k=id-201;string input="",output="";
if(k<15){int[] edge={0,100,1,30,40,60,99,50};int a=e<0?rnd.Next(101):edge[e],b=e<0?rnd.Next(101):edge[(e+2)%8],c=e<0?rnd.Next(101):edge[(e+4)%8];input=a+" "+b+" "+c+"\n";switch(k){case 0:output=(a*b+c).ToString();break;case 1:output=((a+b+c+7)/8).ToString();break;case 2:output=(Math.Max(0,a+b-c)).ToString();break;case 3:output=(Math.Max(0,(a+b+5)/6-c)).ToString();break;case 4:output=(2*a+4*b+c).ToString();break;case 5:output=(Math.Max(0,(a<=30?a*2:60+(a-30)*3)+b-c)).ToString();break;case 6:output=(a+b+c>=180&&a>=40&&b>=40&&c>=40?1:0).ToString();break;case 7:output=(Math.Min(a+b,c)).ToString();break;case 8:output=(Math.Min(Math.Max(0,a-b)/3,c)).ToString();break;case 9:output=((a+b+c)/60).ToString();break;case 10:output=(Math.Min(Math.Max(0,a-b),c)).ToString();break;case 11:output=(a*3600+b*60+c)/3600+":"+((a*3600+b*60+c)/60%60).ToString("00")+":"+((a*3600+b*60+c)%60).ToString("00");break;case 12:output=((a+2*b+3*c)%1000).ToString("000");break;case 13:output=(Math.Max(a,Math.Max(b,c))).ToString();break;case 14:output=(Math.Abs(a-b)+Math.Abs(b-c)).ToString();break;}}else if(k<35){int n=e<0?rnd.Next(1,41):new[]{1,40,2,5,6,10,20,3}[e];int[] a=Enumerable.Range(0,n).Select(i=>e==0?0:e==1?100:e==2?-100:e==3?5:e==4?i:e==5?-i:e==6?(i%2==0?10:-10):rnd.Next(-100,101)).ToArray();input=n+"\n"+string.Join(" ",a.Select(x=>x.ToString()).ToArray())+"\n";switch(k){case 15:output=(a.Where(x=>x>0).Sum()).ToString();break;case 16:output=(a.Count(x=>x<0)).ToString();break;case 17:output=(a.Count(x=>x==0)).ToString();break;case 18:output=(a.Count(x=>x*n>a.Sum())).ToString();break;case 19:output=(Enumerable.Range(0,n).Count(i=>i==0||a[i]>a.Take(i).Max())).ToString();break;case 20:output=(n==1?0:Enumerable.Range(1,n-1).Max(i=>Math.Abs(a[i]-a[i-1]))).ToString();break;case 21:output=(a.Max()-a.Min()).ToString();break;case 22:output=(a.Select((x,i)=>i%2==0?x:-x).Sum()).ToString();break;case 23:output=(a.Where(x=>x%2==0).Sum()).ToString();break;case 24:output=(Array.IndexOf(a,a.Max())+1).ToString();break;case 25:output=(RunLength(a,false)).ToString();break;case 26:output=(RunLength(a,true)).ToString();break;case 27:output=(Enumerable.Range(0,n).Count(i=>i>0&&i<n-1&&a[i]>a[i-1]&&a[i]>a[i+1])).ToString();break;case 28:output=(n==1?"0":string.Join(" ",Enumerable.Range(1,n-1).Select(i=>(a[i]-a[i-1]).ToString()).ToArray())).ToString();break;case 29:output=(a.Select((x,i)=>x*(i+1)).Sum()).ToString();break;case 30:output=(a.Sum(x=>Math.Abs(x))).ToString();break;case 31:output=(PrefixMinimum(a)).ToString();break;case 32:output=(Enumerable.Range(1,n-1).Count(i=>a[i]*a[i-1]>0)).ToString();break;case 33:output=(string.Join(" ",a.Where((x,i)=>i==0||x!=a[i-1]).Select(x=>x.ToString()).ToArray())).ToString();break;case 34:output=(ZeroPrefixes(a)).ToString();break;}}else if(k<45){int r=e<0?rnd.Next(1,7):new[]{1,6,1,6,2,3,4,5}[e],c=e<0?rnd.Next(1,7):new[]{1,6,6,1,3,2,5,4}[e];int[,] g=new int[r,c];var b=new StringBuilder(r+" "+c+"\n");for(int i=0;i<r;i++){for(int j=0;j<c;j++){g[i,j]=e==0?0:e==1?9:e==2?5:rnd.Next(10);b.Append(g[i,j]+" ");}b.AppendLine();}input=b.ToString();switch(k){case 35:output=(GridSum(g,(i,j)=>i==0||j==0||i==r-1||j==c-1)).ToString();break;case 36:output=(GridSum(g,(i,j)=>i>0&&j>0&&i<r-1&&j<c-1)).ToString();break;case 37:output=(GridSum(g,(i,j)=>g[i,j]!=0,true)).ToString();break;case 38:output=(GridSigned(g)).ToString();break;case 39:output=(string.Join(" ",Enumerable.Range(0,r).Select(i=>Enumerable.Range(0,c).Max(j=>g[i,j]).ToString()).ToArray())).ToString();break;case 40:output=(string.Join(" ",Enumerable.Range(0,c).Select(j=>Enumerable.Range(0,r).Min(i=>g[i,j]).ToString()).ToArray())).ToString();break;case 41:output=((Enumerable.Range(0,r).OrderByDescending(i=>Enumerable.Range(0,c).Sum(j=>g[i,j])).First()+1).ToString()).ToString();break;case 42:output=(GridSum(g,(i,j)=>true)).ToString();break;case 43:output=(GridWeighted(g)).ToString();break;case 44:output=(GridSum(g,(i,j)=>j>0&&g[i,j]!=g[i,j-1],true)).ToString();break;}}else if(k<55){string[] edges={""," ","000","abc xyz","  a  b  ","z9z0",new string('a',80),new string('9',80)};string alphabet="abcxyz019 ";string s=e<0?new string(Enumerable.Range(0,rnd.Next(81)).Select(i=>alphabet[rnd.Next(alphabet.Length)]).ToArray()):edges[e];input=s+"\n";switch(k){case 45:output=(s.Where(char.IsDigit).Sum(ch=>ch-48)).ToString();break;case 46:output=(s.Replace(" ","")).ToString();break;case 47:output=(DigitRun(s)).ToString();break;case 48:output=(new string(s.Split(new[]{' '},StringSplitOptions.RemoveEmptyEntries).Select(w=>w[0]).ToArray())).ToString();break;case 49:output=(new string(s.Select(ch=>char.IsDigit(ch)?'*':ch).ToArray())).ToString();break;case 50:output=(new string(s.Select(ch=>ch>='a'&&ch<='z'?(ch=='z'?'a':(char)(ch+1)):ch).ToArray())).ToString();break;case 51:output=(new string(s.Where((ch,i)=>i==0||ch!=s[i-1]).ToArray())).ToString();break;case 52:output=(s.Trim(' ')).ToString();break;case 53:output=(string.Join(" ",s.Split(new[]{' '},StringSplitOptions.RemoveEmptyEntries))).ToString();break;case 54:output=(s.Sum(ch=>(int)ch)).ToString();break;}}else{int q=e<0?rnd.Next(1,31):new[]{1,30,2,3,10,15,20,25}[e];int v=0;var b=new StringBuilder(q+"\n");var o=new List<string>();for(int i=0;i<q;i++){int op=i==q-1||i==0?3:rnd.Next(1,4),x=e==0?0:e==1?50:rnd.Next(51);b.AppendLine(op+" "+x);switch(k){case 55:if(op==1)v+=x;else if(op==2&&v>=x)v-=x;break;case 56:if(op==1)v=Math.Min(100,v+x);else if(op==2)v=Math.Max(0,v-x);break;case 57:if(op==1)v+=x;else if(op==2)v=Math.Max(0,v-x);break;case 58:if(op==1)v=Math.Max(v,x);else if(op==2)v=x;break;case 59:if(op==1)v+=x;else if(op==2)v=x;break;}if(op==3)o.Add(v.ToString());}input=b.ToString();output=string.Join("\n",o.ToArray());}return new TestCase(input,output,e>=0);}
static int RunLength(int[] a,bool inc){int best=1;for(int i=0;i<a.Length;i++){int j=i+1;while(j<a.Length&&(inc?a[j]>a[j-1]:a[j]==a[j-1]))j++;best=Math.Max(best,j-i);}return best;}
static int PrefixMinimum(int[] a){int sum=0,min=0;foreach(int x in a){sum+=x;min=Math.Min(min,sum);}return min;}
static int ZeroPrefixes(int[] a){int sum=0,count=0;foreach(int x in a){sum+=x;if(sum==0)count++;}return count;}
static int GridSum(int[,] g,Func<int,int,bool> include,bool count=false){int s=0;for(int i=0;i<g.GetLength(0);i++)for(int j=0;j<g.GetLength(1);j++)if(include(i,j))s+=count?1:g[i,j];return s;}
static int GridSigned(int[,] g){int s=0;for(int i=0;i<g.GetLength(0);i++)for(int j=0;j<g.GetLength(1);j++)s+=(i+j)%2==0?g[i,j]:-g[i,j];return s;}
static int GridWeighted(int[,] g){int s=0;for(int i=0;i<g.GetLength(0);i++)for(int j=0;j<g.GetLength(1);j++)s+=(j+1)*g[i,j];return s;}
static int DigitRun(string s){int best=0;foreach(var part in System.Text.RegularExpressions.Regex.Matches(s,@"[0-9]+"))best=Math.Max(best,part.ToString().Length);return best;}
}
