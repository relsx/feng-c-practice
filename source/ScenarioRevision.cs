using System;using System.Collections.Generic;using System.Text.RegularExpressions;
static class ScenarioRevision{public static List<Problem> Apply(List<Problem> a){foreach(var p in a){switch(p.Id){
case 1:p.Statement=@"小卖部合并两笔收款。

输入两个整数 a、b（-10000 到 10000），输出 a+b。";p.Hint=@"scanf 读取两个整数，printf 输出结果。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int a, b;
    scanf(""%d%d"", &a, &b);
    printf(""%d"", a + b);
    return 0;
}
";break;
case 2:p.Statement=@"仓管核对入库与出库差额。

输入两个整数 a、b（-10000 到 10000），输出 a-b。";p.Hint=@"注意运算顺序。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int a, b;
    scanf(""%d%d"", &a, &b);
    printf(""%d"", a - b);
    return 0;
}
";break;
case 3:p.Statement=@"工厂计算每排零件的总数量。

输入两个整数 a、b（-1000 到 1000），输出 a*b。";p.Hint=@"使用乘法运算符。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int a, b;
    scanf(""%d%d"", &a, &b);
    printf(""%d"", a * b);
    return 0;
}
";break;
case 4:p.Statement=@"配送员将包裹分装并清点余件。

输入非负整数 a（0 到 10000）和正整数 b（1 到 100）。依次输出整数商、余数，以空格分隔。";p.Hint=@"整数除法 / 和取余 %。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int a, b;
    scanf(""%d%d"", &a, &b);
    printf(""%d %d"", a / b, a % b);
    return 0;
}
";break;
case 5:p.Statement=@"实验员汇总三次测量的平均读数。

输入三个整数（-1000 到 1000），输出它们的平均值，保留至少 4 位小数。";p.Hint=@"使用浮点除法，避免整数截断。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int a, b, c;
    scanf(""%d%d%d"", &a, &b, &c);
    printf(""%.6f"", (a + b + c) / 3.0);
    return 0;
}
";break;
case 6:p.Statement=@"气象站为海外访客转换温度单位。

输入摄氏温度 c（整数，-100 到 100），输出华氏温度 c*9/5+32，保留至少 4 位小数。";p.Hint=@"9.0/5.0 可以避免整数除法。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int c;
    scanf(""%d"", &c);
    printf(""%.6f"", c * 9.0 / 5.0 + 32);
    return 0;
}
";break;
case 7:p.Statement=@"园林队估算圆形花坛的占地。

输入整数半径 r（0 到 100）。使用 π=3.141592653589793，输出圆面积，保留至少 4 位小数。";p.Hint=@"面积为 π*r*r。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int r;
    scanf(""%d"", &r);
    printf(""%.6f"", 3.141592653589793 * r * r);
    return 0;
}
";break;
case 8:p.Statement=@"值班员调换两个储物柜的标签。

输入两个整数 a、b（-10000 到 10000），按 b a 的顺序输出。";p.Hint=@"输出顺序交换即可。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int a, b;
    scanf(""%d%d"", &a, &b);
    printf(""%d %d"", b, a);
    return 0;
}
";break;
case 9:p.Statement=@"测绘员只关心相对原点的距离大小。

输入一个整数 n（-100000 到 100000），输出它的绝对值。";p.Hint=@"负数要变号，0 的绝对值是 0。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int n;
    scanf(""%d"", &n);
    printf(""%d"", n < 0 ? - n : n);
    return 0;
}
";break;
case 10:p.Statement=@"机器人比较两次定位的距离平方。

输入 x1 y1 x2 y2，坐标均为 -100 到 100 的整数。输出 (x1-x2)^2+(y1-y2)^2。";p.Hint=@"这里输出平方，不需要开平方。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int a, b, c, d;
    scanf(""%d%d%d%d"", &a, &b, &c, &d);
    printf(""%d"", (a - c) * (a - c) + (b - d) * (b - d));
    return 0;
}
";break;
case 11:p.Statement=@"调度员选择载重较大的车辆。

输入两个整数（-10000 到 10000），输出较大的一个。";p.Hint=@"两个数相等时输出其共同值。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int a, b;
    scanf(""%d%d"", &a, &b);
    if (a > b) {
        printf(""%d"", a);
    } else {
        printf(""%d"", b);
    }
    return 0;
}
";break;
case 12:p.Statement=@"教练选出三次训练的最好成绩。

输入三个整数（-10000 到 10000），输出最大值。";p.Hint=@"可以逐个更新最大值。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int a, b, c, max;
    scanf(""%d%d%d"", &a, &b, &c);
    max = a;
    if (b > max) {
        max = b;
    }
    if (c > max) {
        max = c;
    }
    printf(""%d"", max);
    return 0;
}
";break;
case 13:p.Statement=@"包装线按批号的奇偶分配通道。

输入整数 n（-100000 到 100000）。偶数输出 EVEN，奇数输出 ODD。";p.Hint=@"负奇数的余数可能为 -1，判断是否等于 0。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int n;
    scanf(""%d"", &n);
    if (n % 2 == 0) {
        printf(""EVEN"");
    } else {
        printf(""ODD"");
    }
    return 0;
}
";break;
case 14:p.Statement=@"水位监测仪区分高于和低于基准线。

输入整数 n（-100000 到 100000）。正数输出 POSITIVE，负数输出 NEGATIVE，零输出 ZERO。";p.Hint=@"零需要单独处理。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int n;
    scanf(""%d"", &n);
    if (n > 0) {
        printf(""POSITIVE"");
    } else if (n < 0) {
        printf(""NEGATIVE"");
    } else {
        printf(""ZERO"");
    }
    return 0;
}
";break;
case 15:p.Statement=@"日历系统核对二月份的闰年规则。

输入年份 y（1 到 9999）。闰年输出 YES，否则输出 NO。";p.Hint=@"能被 400 整除，或能被 4 整除且不能被 100 整除。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int y;
    scanf(""%d"", &y);
    if (y % 400 == 0 || (y % 4 == 0 && y % 100 != 0)) {
        printf(""YES"");
    } else {
        printf(""NO"");
    }
    return 0;
}
";break;
case 16:p.Statement=@"学校将考试分数换算为等级。

输入整数成绩 s（0 到 100）。90~100 为 A，80~89 为 B，70~79 为 C，60~69 为 D，0~59 为 F。输出等级。";p.Hint=@"从高分到低分依次判断。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int s;
    scanf(""%d"", &s);
    if (s >= 90) {
        printf(""A"");
    } else if (s >= 80) {
        printf(""B"");
    } else if (s >= 70) {
        printf(""C"");
    } else if (s >= 60) {
        printf(""D"");
    } else {
        printf(""F"");
    }
    return 0;
}
";break;
case 17:p.Statement=@"工匠检查三根支架能否构成三角形。

输入三个边长（整数，1 到 100）。不能组成三角形输出 INVALID；等边输出 EQUILATERAL；等腰输出 ISOSCELES；其他输出 SCALENE。";p.Hint=@"先判断两边之和大于第三边，再判断相等关系。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int a, b, c;
    scanf(""%d%d%d"", &a, &b, &c);
    if (a + b <= c || a + c <= b || b + c <= a) {
        printf(""INVALID"");
    } else if (a == b && b == c) {
        printf(""EQUILATERAL"");
    } else if (a == b || b == c || a == c) {
        printf(""ISOSCELES"");
    } else {
        printf(""SCALENE"");
    }
    return 0;
}
";break;
case 18:p.Statement=@"门禁系统区分按键的字符类型。

输入一个非空白 ASCII 字符。数字输出 DIGIT，英文字母输出 LETTER，其他输出 OTHER。";p.Hint=@"判断字符区间 0~9、a~z、A~Z。";p.Reference=@"#include <stdio.h>

int main(void)
{
    char c;
    scanf("" %c"", &c);
    if (c >= '0' && c <= '9') {
        printf(""DIGIT"");
    } else if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z')) {
        printf(""LETTER"");
    } else {
        printf(""OTHER"");
    }
    return 0;
}
";break;
case 19:p.Statement=@"收银台处理一条简单算式。

输入整数 a、运算符、整数 b，例如 8 / 2。a、b 范围 -100 到 100，运算符为 + - * /。输出浮点结果（至少 4 位小数），除数为 0 时输出 ERROR。";p.Hint=@"除法时先检查 0，再转换成浮点数。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int a, b;
    char op;
    double result;
    scanf(""%d %c %d"", &a, &op, &b);
    if (op == '/' && b == 0) {
        printf(""ERROR"");
    } else {
        if (op == '+') {
            result = a + b;
        } else if (op == '-') {
            result = a - b;
        } else if (op == '*') {
            result = a * b;
        } else {
            result = (double) a / b;
        }
        printf(""%.6f"", result);
    }
    return 0;
}
";break;
case 20:p.Statement=@"排班系统查询指定月份的天数。

输入年份 y（1 到 9999）和月份 m（1 到 12）。输出该月天数，二月需考虑闰年。";p.Hint=@"用数组存月份天数，单独调整二月。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int y, m, d[] = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
    scanf(""%d%d"", &y, &m);
    if (m == 2 && (y % 400 == 0 || (y % 4 == 0 && y % 100 != 0))) {
        d[1] = 29;
    }
    printf(""%d"", d[m - 1]);
    return 0;
}
";break;
case 21:p.Statement=@"储蓄计划每天比前一天多存一元。

输入整数 n（0 到 10000），输出 1+2+...+n。n=0 时输出 0。";p.Hint=@"累加或使用 n*(n+1)/2。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int n;
    scanf(""%d"", &n);
    printf(""%d"", n * (n + 1) / 2);
    return 0;
}
";break;
case 22:p.Statement=@"实验室统计一组不同顺序的排列数。

输入 n（0 到 20），输出 n!。0!=1，结果使用 64 位整数。";p.Hint=@"long long 可以容纳 20!，用 %lld 输出。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int n, i;
    long long v = 1;
    scanf(""%d"", &n);
    for (i = 1; i <= n; i++) {
        v *= i;
    }
    printf(""%lld"", v);
    return 0;
}
";break;
case 23:p.Statement=@"生物实验记录逐步增长的递推数量。

输入 n（0 到 40），输出 F(n)。定义 F(0)=0、F(1)=1，之后为前两项之和。";p.Hint=@"迭代更新两个变量。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int n, i;
    long long a = 0, b = 1, t;
    scanf(""%d"", &n);
    for (i = 0; i < n; i++) {
        t = a + b;
        a = b;
        b = t;
    }
    printf(""%lld"", a);
    return 0;
}
";break;
case 24:p.Statement=@"票务系统计算编号的数位校验和。

输入非负整数 n（0 到 1000000000），输出十进制各位数字之和。";p.Hint=@"反复 n%10 取末位、n/=10 去末位。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int n, s = 0;
    scanf(""%d"", &n);
    while (n) {
        s += n % 10;
        n /= 10;
    }
    printf(""%d"", s);
    return 0;
}
";break;
case 25:p.Statement=@"仪表测试台反转一个数值的十进制位。

输入非负整数 n（0 到 100000000），输出十进制反转后的整数。前导零省略，例如 1200 输出 21。";p.Hint=@"结果不断乘 10 并加上末位。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int n, v = 0;
    scanf(""%d"", &n);
    while (n) {
        v = v * 10 + n % 10;
        n /= 10;
    }
    printf(""%d"", v);
    return 0;
}
";break;
case 26:p.Statement=@"数学兴趣社筛选质数编号。

输入 n（0 到 100000），素数输出 YES，否则输出 NO。";p.Hint=@"0、1 不是素数；只需试除到平方根。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int n, i, ok = 1;
    scanf(""%d"", &n);
    if (n < 2) {
        ok = 0;
    }
    for (i = 2; i * i <= n; i++) {
        if (n % i == 0) {
            ok = 0;
            break;
        }
    }
    printf(ok ? ""YES"" : ""NO"");
    return 0;
}
";break;
case 27:p.Statement=@"制作等长绳段时寻找共同的最大长度。

输入非负整数 a、b（0 到 10000），输出最大公约数。约定 gcd(0,0)=0。";p.Hint=@"欧几里得算法，循环取余。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int a, b, t;
    scanf(""%d%d"", &a, &b);
    while (b) {
        t = a % b;
        a = b;
        b = t;
    }
    printf(""%d"", a);
    return 0;
}
";break;
case 28:p.Statement=@"两盏周期灯寻找再次同时闪烁的周期。

输入非负整数 a、b（0 到 10000），输出最小公倍数。任一数为 0 时结果为 0。";p.Hint=@"先求 gcd，先除后乘。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int a, b, x, y, t;
    scanf(""%d%d"", &a, &b);
    x = a;
    y = b;
    while (y) {
        t = x % y;
        x = y;
        y = t;
    }
    printf(""%d"", a == 0 || b == 0 ? 0 : a / x * b);
    return 0;
}
";break;
case 29:p.Statement=@"模拟器重复执行同一个倍率的放大。

输入底数 a（-10 到 10）和指数 n（0 到 9），输出 a^n。约定任何数的 0 次方为 1，包括 0^0。";p.Hint=@"初始化结果为 1，循环相乘。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int a, n, i;
    long long v = 1;
    scanf(""%d%d"", &a, &n);
    for (i = 0; i < n; i++) {
        v *= a;
    }
    printf(""%lld"", v);
    return 0;
}
";break;
case 30:p.Statement=@"显示屏需要确定编号占用的位数。

输入非负整数 n（0 到 1000000000），输出十进制位数，0 的位数为 1。";p.Hint=@"do...while 可以处理 0。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int n, c = 0;
    scanf(""%d"", &n);
    do {
        c++;
        n /= 10;
    } while (n);
    printf(""%d"", c);
    return 0;
}
";break;
case 31:p.Statement=@"编号审核员清点范围内的质数。

输入 n（0 到 2000），输出闭区间 [1,n] 中素数个数。";p.Hint=@"对每个数判断是否为素数。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int n, i, j, c = 0, ok;
    scanf(""%d"", &n);
    for (i = 2; i <= n; i++) {
        ok = 1;
        for (j = 2; j * j <= i; j++) {
            if (i % j == 0) {
                ok = 0;
                break;
            }
        }
        c += ok;
    }
    printf(""%d"", c);
    return 0;
}
";break;
case 32:p.Statement=@"公益活动累计奇数日的奖励积分。

输入 n（0 到 10000），输出 [1,n] 中所有奇数之和。";p.Hint=@"循环步长为 2。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int n, i, s = 0;
    scanf(""%d"", &n);
    for (i = 1; i <= n; i += 2) {
        s += i;
    }
    printf(""%d"", s);
    return 0;
}
";break;
case 33:p.Statement=@"老师为口算练习生成一行乘法结果。

输入 n（1 到 9），依次输出 n*1 到 n*9 共 9 个结果，以空格分隔。";p.Hint=@"循环 i 从 1 到 9，只输出数字。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int n, i;
    scanf(""%d"", &n);
    for (i = 1; i <= 9; i++) {
        printf(""%d "", n * i);
    }
    return 0;
}
";break;
case 34:p.Statement=@"兴趣社寻找恰好等于真因子和的号码。

输入 n（1 到 10000）。若 n 等于所有小于自身的正因子之和，输出 YES，否则 NO。";p.Hint=@"1 不是完全数；不要把自身计入。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int n, i, s = 0;
    scanf(""%d"", &n);
    for (i = 1; i < n; i++) {
        if (n % i == 0) {
            s += i;
        }
    }
    printf(s == n ? ""YES"" : ""NO"");
    return 0;
}
";break;
case 35:p.Statement=@"校验员寻找符合各位幂和规则的编号。

输入 n（0 到 9999）。若每位数字的 k 次方之和等于 n，输出 YES，否则 NO；k 是十进制位数，0 的位数为 1。";p.Hint=@"先数位数，再逐位累加。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int n, t, k = 0, s = 0, d, p, i;
    scanf(""%d"", &n);
    t = n;
    do {
        k++;
        t /= 10;
    } while (t);
    t = n;
    do {
        d = t % 10;
        p = 1;
        for (i = 0; i < k; i++) {
            p *= d;
        }
        s += p;
        t /= 10;
    } while (t);
    printf(s == n ? ""YES"" : ""NO"");
    return 0;
}
";break;
case 36:p.Statement=@"装盒车间统计可用的整数分组方案。

输入 n（1 到 100000），输出正因子个数，包括 1 和 n。";p.Hint=@"因子成对出现，完全平方数的平方根只算一次。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int n, i, c = 0;
    scanf(""%d"", &n);
    for (i = 1; i * i <= n; i++) {
        if (n % i == 0) {
            c += i * i == n ? 1 : 2;
        }
    }
    printf(""%d"", c);
    return 0;
}
";break;
case 37:p.Statement=@"模拟器累计逐级增长的平方能量。

输入 n（0 到 1000），输出 1^2+2^2+...+n^2。";p.Hint=@"累加 i*i。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int n, i, s = 0;
    scanf(""%d"", &n);
    for (i = 1; i <= n; i++) {
        s += i * i;
    }
    printf(""%d"", s);
    return 0;
}
";break;
case 38:p.Statement=@"通信模块把整数转换为二进制信号。

输入非负整数 n（0 到 65535），输出不带前导零的二进制字符串，n=0 时输出 0。";p.Hint=@"把余数保存后倒序输出。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int n, i = 0, a[32];
    scanf(""%d"", &n);
    do {
        a[i++] = n % 2;
        n /= 2;
    } while (n);
    while (i) {
        printf(""%d"", a[-- i]);
    }
    return 0;
}
";break;
case 39:p.Statement=@"设备统计二进制信号中的亮灯位。

输入 n（0 到 65535），输出二进制表示中数字 1 的个数。";p.Hint=@"可用位运算，也可反复除以 2。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int n, c = 0;
    scanf(""%d"", &n);
    while (n) {
        c += n % 2;
        n /= 2;
    }
    printf(""%d"", c);
    return 0;
}
";break;
case 40:p.Statement=@"纪念票筛选左右对称的整数编号。

输入 n（0 到 100000000），十进制表示正反一致输出 YES，否则 NO。";p.Hint=@"反转数字后比较。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int n, t, v = 0;
    scanf(""%d"", &n);
    t = n;
    while (t) {
        v = v * 10 + t % 10;
        t /= 10;
    }
    printf(v == n ? ""YES"" : ""NO"");
    return 0;
}
";break;
case 41:p.Statement=@"仓库合计一批货物的数量。

第一行输入 n（1 到 50），第二行输入 n 个整数（-1000 到 1000）。输出所有元素之和。";p.Hint=@"初始化总和为 0。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int n, i, a[100];
    scanf(""%d"", &n);
    for (i = 0; i < n; i++) {
        scanf(""%d"", &a[i]);
    }
    int s = 0;
    for (i = 0; i < n; i++) {
        s += a[i];
    }
    printf(""%d"", s);
    return 0;
}
";break;
case 42:p.Statement=@"气象站寻找一组记录中的最高读数。

第一行输入 n（1 到 50），第二行输入 n 个整数（-1000 到 1000）。输出最大值。";p.Hint=@"用第一个元素初始化，不能默认最大值为 0。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int n, i, a[100];
    scanf(""%d"", &n);
    for (i = 0; i < n; i++) {
        scanf(""%d"", &a[i]);
    }
    int m = a[0];
    for (i = 1; i < n; i++) {
        if (a[i] > m) {
            m = a[i];
        }
    }
    printf(""%d"", m);
    return 0;
}
";break;
case 43:p.Statement=@"冷链仓寻找一组记录中的最低读数。

第一行输入 n（1 到 50），第二行输入 n 个整数（-1000 到 1000）。输出最小值。";p.Hint=@"用第一个元素初始化。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int n, i, a[100];
    scanf(""%d"", &n);
    for (i = 0; i < n; i++) {
        scanf(""%d"", &a[i]);
    }
    int m = a[0];
    for (i = 1; i < n; i++) {
        if (a[i] < m) {
            m = a[i];
        }
    }
    printf(""%d"", m);
    return 0;
}
";break;
case 44:p.Statement=@"摄像系统倒放一段采样序列。

第一行输入 n（1 到 50），第二行输入 n 个整数（-1000 到 1000）。按逆序输出全部元素，以空格分隔。";p.Hint=@"从下标 n-1 开始输出。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int n, i, a[100];
    scanf(""%d"", &n);
    for (i = 0; i < n; i++) {
        scanf(""%d"", &a[i]);
    }
    for (i = n - 1; i >= 0; i--) {
        printf(""%d "", a[i]);
    }
    return 0;
}
";break;
case 45:p.Statement=@"排队系统将等待编号按升序整理。

第一行输入 n（1 到 50），第二行输入 n 个整数（-1000 到 1000）。输出升序排列后的数组，以空格分隔。";p.Hint=@"可以使用冒泡或选择排序，保留重复元素。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int n, i, a[100];
    scanf(""%d"", &n);
    for (i = 0; i < n; i++) {
        scanf(""%d"", &a[i]);
    }
    int j, t;
    for (i = 0; i < n; i++) {
        for (j = i + 1; j < n; j++) {
            if (a[i] > a[j]) {
                t = a[i];
                a[i] = a[j];
                a[j] = t;
            }
        }
    }
    for (i = 0; i < n; i++) {
        printf(""%d "", a[i]);
    }
    return 0;
}
";break;
case 46:p.Statement=@"图书检索返回编号首次出现的位置。

第一行输入 n（1 到 50），第二行输入 n 个整数（-1000 到 1000）。第三行输入待查整数 x（-1000 到 1000）。输出第一次出现的下标（从 0 开始），不存在输出 -1。";p.Hint=@"找到后立即结束查找。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int n, i, a[100];
    scanf(""%d"", &n);
    for (i = 0; i < n; i++) {
        scanf(""%d"", &a[i]);
    }
    int x, v = - 1;
    scanf(""%d"", &x);
    for (i = 0; i < n; i++) {
        if (a[i] == x) {
            v = i;
            break;
        }
    }
    printf(""%d"", v);
    return 0;
}
";break;
case 47:p.Statement=@"检测线清点达到阈值的样品。

第一行输入 n（1 到 50），第二行输入 n 个整数（-1000 到 1000）。第三行输入阈值 x（-1000 到 1000）。输出大于等于 x 的元素个数。";p.Hint=@"注意包含等于。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int n, i, a[100];
    scanf(""%d"", &n);
    for (i = 0; i < n; i++) {
        scanf(""%d"", &a[i]);
    }
    int x, c = 0;
    scanf(""%d"", &x);
    for (i = 0; i < n; i++) {
        if (a[i] >= x) {
            c++;
        }
    }
    printf(""%d"", c);
    return 0;
}
";break;
case 48:p.Statement=@"活动统计去除重复后的参与编号数。

第一行输入 n（1 到 50），第二行输入 n 个整数（-1000 到 1000）。输出不同整数的个数。";p.Hint=@"对每个元素检查前面是否已出现。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int n, i, a[100];
    scanf(""%d"", &n);
    for (i = 0; i < n; i++) {
        scanf(""%d"", &a[i]);
    }
    int j, c = 0;
    for (i = 0; i < n; i++) {
        for (j = 0; j < i; j++) {
            if (a[j] == a[i]) {
                break;
            }
        }
        if (j == i) {
            c++;
        }
    }
    printf(""%d"", c);
    return 0;
}
";break;
case 49:p.Statement=@"环形灯带把原有图案整体右移。

第一行输入 n（1 到 50），第二行输入 n 个整数（-1000 到 1000）。第三行输入 k（0 到 100）。将数组循环右移 k 位后输出，以空格分隔。";p.Hint=@"用 k%n 处理超过长度的移动。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int n, i, a[100];
    scanf(""%d"", &n);
    for (i = 0; i < n; i++) {
        scanf(""%d"", &a[i]);
    }
    int k;
    scanf(""%d"", &k);
    k %= n;
    for (i = 0; i < n; i++) {
        printf(""%d "", a[(i + n - k) % n]);
    }
    return 0;
}
";break;
case 50:p.Statement=@"财务系统展示每一笔后的累计余额。

第一行输入 n（1 到 50），第二行输入 n 个整数（-1000 到 1000）。依次输出 a[0]、a[0]+a[1]、...、全部元素之和，以空格分隔。";p.Hint=@"保存一个累计和。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int n, i, a[100];
    scanf(""%d"", &n);
    for (i = 0; i < n; i++) {
        scanf(""%d"", &a[i]);
    }
    int s = 0;
    for (i = 0; i < n; i++) {
        s += a[i];
        printf(""%d "", s);
    }
    return 0;
}
";break;
case 51:p.Statement=@"门禁检查一个不含空格的标记长度。

输入一行不含空白的 ASCII 字符串，长度 1 到 80，由英文字母和数字组成。输出字符数。";p.Hint=@"循环统计结束符之前的字符个数，无需其他标准库。";p.Reference=@"#include <stdio.h>
static int text_length(const char s[]){int n=0;while(s[n])n++;return n;}
int main(void)
{
    char s[100];
    scanf(""%99s"", s);
    printf(""%d"", (int) text_length(s));
    return 0;
}
";break;
case 52:p.Statement=@"镜像显示屏倒序显示标记。

输入一行不含空白的 ASCII 字符串，长度 1 到 80，由英文字母和数字组成。输出反转后的字符串。";p.Hint=@"先手写循环求长度，再从最后一个字符向前输出。";p.Reference=@"#include <stdio.h>
static int text_length(const char s[]){int n=0;while(s[n])n++;return n;}
int main(void)
{
    char s[100];
    scanf(""%99s"", s);
    int i;
    for (i = (int) text_length(s) - 1; i >= 0; i--) {
        putchar(s[i]);
    }
    return 0;
}
";break;
case 53:p.Statement=@"语音素材库统计标签中的元音字母。

输入一行不含空白的 ASCII 字符串，长度 1 到 80，由英文字母和数字组成。输出 a e i o u 的总个数，不区分大小写。";p.Hint=@"可先转成小写后判断。";p.Reference=@"#include <stdio.h>
int main(void)
{
    char s[100];
    scanf(""%99s"", s);
    int i, c = 0;
    for (i = 0; s[i]; i++) {
        if (strchr(""aeiouAEIOU"", s[i])) {
            c++;
        }
    }
    printf(""%d"", c);
    return 0;
}
";break;
case 54:p.Statement=@"收藏系统筛选左右对称的标签。

输入一行不含空白的 ASCII 字符串，长度 1 到 80，由英文字母和数字组成。区分大小写，正反一致输出 YES，否则 NO。";p.Hint=@"用循环求长度，再从两端向中间比较。";p.Reference=@"#include <stdio.h>
static int text_length(const char s[]){int n=0;while(s[n])n++;return n;}
int main(void)
{
    char s[100];
    scanf(""%99s"", s);
    int i, n = (int) text_length(s), ok = 1;
    for (i = 0; i < n / 2; i++) {
        if (s[i] != s[n - 1 - i]) {
            ok = 0;
        }
    }
    printf(ok ? ""YES"" : ""NO"");
    return 0;
}
";break;
case 55:p.Statement=@"登记系统清点标记中的数字字符。

输入一行不含空白的 ASCII 字符串，长度 1 到 80，由英文字母和数字组成。输出数字字符 0~9 的个数。";p.Hint=@"字符值要与字符常量比较。";p.Reference=@"#include <stdio.h>

int main(void)
{
    char s[100];
    scanf(""%99s"", s);
    int i, c = 0;
    for (i = 0; s[i]; i++) {
        if (s[i] >= '0' && s[i] <= '9') {
            c++;
        }
    }
    printf(""%d"", c);
    return 0;
}
";break;
case 56:p.Statement=@"公告牌统一把英文字母转换为大写。

输入一行不含空白的 ASCII 字符串，长度 1 到 80，由英文字母和数字组成。把小写英文字母改为大写，其他字符不变，输出结果。";p.Hint=@"字符在'a'到'z'之间时减'a'加'A'，其他字符保留。";p.Reference=@"#include <stdio.h>
int main(void)
{
    char s[100];
    scanf(""%99s"", s);
    int i;
    for (i = 0; s[i]; i++) {
        putchar((s[i]>='a'&&s[i]<='z'?s[i]-'a'+'A':s[i]));
    }
    return 0;
}
";break;
case 57:p.Statement=@"编辑器统计一句短文本的单词数。

输入一行（长度 0 到 80），只含英文字母和空格，可以为空或含连续、首尾空格。输出单词个数；单词是连续非空格字符。";p.Hint=@"用 fgets 读整行，在空格到字母的切换处计数。";p.Reference=@"#include <stdio.h>

int main(void)
{
    char s[100];
    int i, c = 0, in = 0;
    fgets(s, sizeof(s), stdin);
    for (i = 0; s[i] && s[i] != '\n'; i++) {
        if (s[i] != ' ') {
            if (!in) {
                c++;
            }
            in = 1;
        } else {
            in = 0;
        }
    }
    printf(""%d"", c);
    return 0;
}
";break;
case 58:p.Statement=@"设备升级时替换标签中的指定字符。

输入一行不含空白的 ASCII 字符串，长度 1 到 80，由英文字母和数字组成。第二行输入两个字母或数字 a、b（空格分隔）。把字符串中所有 a 替换为 b，区分大小写，输出结果。";p.Hint=@"逐字符判断。";p.Reference=@"#include <stdio.h>

int main(void)
{
    char s[100];
    scanf(""%99s"", s);
    char a, b;
    int i;
    scanf("" %c %c"", &a, &b);
    for (i = 0; s[i]; i++) {
        putchar(s[i] == a ? b : s[i]);
    }
    return 0;
}
";break;
case 59:p.Statement=@"传感日志统计指定字符出现次数。

输入一行不含空白的 ASCII 字符串，长度 1 到 80，由英文字母和数字组成。第二行输入一个字母或数字 c。输出 c 的出现次数，区分大小写。";p.Hint=@"计数初值为 0。";p.Reference=@"#include <stdio.h>

int main(void)
{
    char s[100];
    scanf(""%99s"", s);
    char c;
    int i, n = 0;
    scanf("" %c"", &c);
    for (i = 0; s[i]; i++) {
        if (s[i] == c) {
            n++;
        }
    }
    printf(""%d"", n);
    return 0;
}
";break;
case 60:p.Statement=@"格网分析员合计主对角线上的读数。

第一行输入 n（1 到 8），接着输入 n 行，每行 n 个整数（-100 到 100）。输出主对角线（行下标等于列下标）的元素之和。";p.Hint=@"读入所有元素，只累加 i==j 的位置。";p.Reference=@"#include <stdio.h>

int main(void)
{
    int n, i, j, x, s = 0;
    scanf(""%d"", &n);
    for (i = 0; i < n; i++) {
        for (j = 0; j < n; j++) {
            scanf(""%d"", &x);
            if (i == j) {
                s += x;
            }
        }
    }
    printf(""%d"", s);
    return 0;
}
";break;
case 61:p.Statement=@"维护人员用函数交换两台设备的参数。

输入两个整数（-10000 到 10000），使用指针函数交换，输出交换后的两个值。";p.Hint=@"函数接收两个地址，用临时变量交换解引用后的值。";p.Reference=@"#include <stdio.h>
void swap(int * a, int * b)
{
    int t = * a;
    * a = * b;
    * b = t;
}

int main(void)
{
    int a, b;
    scanf(""%d%d"", &a, &b);
    swap(&a, &b);
    printf(""%d %d"", a, b);
    return 0;
}
";break;
case 62:p.Statement=@"监测端以指针顺序读取连续数据。

输入 n（1 到 100），随后 n 个整数（-1000 到 1000）。用指针遍历，输出总和。";p.Hint=@"让指针从数组起点移动到末尾。";p.Reference=@"#include <stdio.h>
int main(void)
{
    int n, a[100], * p, s = 0;
    scanf(""%d"", &n);
    for (p = a; p < a + n; p++) {
        scanf(""%d"", p);
    }
    for (p = a; p < a + n; p++) {
        s += * p;
    }
    printf(""%d"", s);
    return 0;
}
";break;
case 63:p.Statement=@"算法课堂用递归计算排列数量。

输入 n（0 到 20），使用递归函数计算并输出 n!，使用 long long。";p.Hint=@"0! 为 1；递归返回 n 乘以 (n-1)!。";p.Reference=@"#include <stdio.h>
long long fact(int n)
{
    if (n == 0) {
        return 1;
    }
    return n * fact(n - 1);
}

int main(void)
{
    int n;
    scanf(""%d"", &n);
    printf(""%lld"", fact(n));
    return 0;
}
";break;
case 64:p.Statement=@"裁剪工具用递归寻找共同的长度单位。

输入两个非负整数（0 到 100000），递归求最大公约数；gcd(0,0)=0。";p.Hint=@"当 b=0 返回 a，否则调用 gcd(b,a%b)。";p.Reference=@"#include <stdio.h>
int gcd(int a, int b)
{
    if (b == 0) {
        return a;
    }
    return gcd(b, a % b);
}

int main(void)
{
    int a, b;
    scanf(""%d%d"", &a, &b);
    printf(""%d"", gcd(a, b));
    return 0;
}
";break;
case 65:p.Statement=@"有序目录查找目标编号的最早位置。

输入 n（1 到 100）、n 个非递减整数（-1000 到 1000）、目标 x。输出首次出现的 0 起始下标，不存在输出 -1。";p.Hint=@"找到大于等于 x 的第一个位置，再检查是否相等。";p.Reference=@"#include <stdio.h>
int main(void)
{
    int n, a[100], x, i, l = 0, r, m;
    scanf(""%d"", &n);
    for (i = 0; i < n; i++) {
        scanf(""%d"", &a[i]);
    }
    scanf(""%d"", &x);
    r = n;
    while (l < r) {
        m = (l + r) / 2;
        if (a[m] < x) {
            l = m + 1;
        } else {
            r = m;
        }
    }
    printf(""%d"", l < n && a[l] == x ? l : - 1);
    return 0;
}
";break;
case 66:p.Statement=@"仓管合并两份已排好序的清单。

输入 n,m（各 1 到 100），随后分别输入两个非递减数组（值 -1000 到 1000）。输出合并后的有序数组，保留重复项。";p.Hint=@"两个指针分别指向两个数组最小的未处理元素。";p.Reference=@"#include <stdio.h>
int main(void)
{
    int n, m, a[100], b[100], i = 0, j = 0, k;
    scanf(""%d%d"", &n, &m);
    for (k = 0; k < n; k++) {
        scanf(""%d"", &a[k]);
    }
    for (k = 0; k < m; k++) {
        scanf(""%d"", &b[k]);
    }
    while (i < n || j < m) {
        if (j == m || (i < n && a[i] <= b[j])) {
            printf(""%d "", a[i++]);
        } else {
            printf(""%d "", b[j++]);
        }
    }
    return 0;
}
";break;
case 67:p.Statement=@"登记员整理已排序清单中的重复编号。

输入 n（1 到 100）和 n 个非递减整数（-1000 到 1000），输出去重后的数组。";p.Hint=@"仅在当前值与前一个值不同的时候输出。";p.Reference=@"#include <stdio.h>
int main(void)
{
    int n, a[100], i;
    scanf(""%d"", &n);
    for (i = 0; i < n; i++) {
        scanf(""%d"", &a[i]);
    }
    for (i = 0; i < n; i++) {
        if (i == 0 || a[i] != a[i - 1]) {
            printf(""%d "", a[i]);
        }
    }
    return 0;
}
";break;
case 68:p.Statement=@"销售员寻找最受欢迎的商品编号。

输入 n（1 到 100）及 n 个整数（-20 到 20）。输出出现最多的值及次数；并列选择较小的值。";p.Hint=@"用 41 个计数槽记录频率，从小到大更新最佳值。";p.Reference=@"#include <stdio.h>
int main(void)
{
    int n, c[41] = { 0 }, i, x, b = 0;
    scanf(""%d"", &n);
    for (i = 0; i < n; i++) {
        scanf(""%d"", &x);
        c[x + 20]++;
    }
    for (i = 1; i < 41; i++) {
        if (c[i] > c[b]) {
            b = i;
        }
    }
    printf(""%d %d"", b - 20, c[b]);
    return 0;
}
";break;
case 69:p.Statement=@"比赛需要找出第二高的不同成绩。

输入 n（1 到 100）和 n 个整数（-1000 到 1000）。输出第二大的不同值，只有一个不同值时输出 NONE。";p.Hint=@"维护最大值与第二大值，遇到相等最大值时跳过。";p.Reference=@"#include <stdio.h>
int main(void)
{
    int n, i, x, a = - 1001, b = - 1001;
    scanf(""%d"", &n);
    for (i = 0; i < n; i++) {
        scanf(""%d"", &x);
        if (x > a) {
            b = a;
            a = x;
        } else if (x < a && x > b) {
            b = x;
        }
    }
    if (b == - 1001) {
        printf(""NONE"");
    } else {
        printf(""%d"", b);
    }
    return 0;
}
";break;
case 70:p.Statement=@"商店寻找连续日期内的最大收益。

输入 n（1 到 100）和 n 个整数（-1000 到 1000），输出非空连续子段的最大和。";p.Hint=@"记录以当前位置结尾的最佳和，处理全负数。";p.Reference=@"#include <stdio.h>
int main(void)
{
    int n, i, x, cur = 0, best = - 1001;
    scanf(""%d"", &n);
    for (i = 0; i < n; i++) {
        scanf(""%d"", &x);
        if (cur < 0) {
            cur = 0;
        }
        cur += x;
        if (cur > best) {
            best = cur;
        }
    }
    printf(""%d"", best);
    return 0;
}
";break;
case 71:p.Statement=@"监测员寻找固定时长内的最大累计值。

输入 n,k（1≤k≤n≤100），随后 n 个整数（-1000 到 1000）。输出所有长度为 k 的连续窗口中的最大和。";p.Hint=@"先求第一个窗口和，每次加新元素减去旧元素。";p.Reference=@"#include <stdio.h>
int main(void)
{
    int n, k, a[100], i, s = 0, b;
    scanf(""%d%d"", &n, &k);
    for (i = 0; i < n; i++) {
        scanf(""%d"", &a[i]);
    }
    for (i = 0; i < k; i++) {
        s += a[i];
    }
    b = s;
    for (i = k; i < n; i++) {
        s += a[i] - a[i - k];
        if (s > b) {
            b = s;
        }
    }
    printf(""%d"", b);
    return 0;
}
";break;
case 72:p.Statement=@"账本对多段日期查询累计变动。

输入 n,q（各 1 到 100），n 个整数（-1000 到 1000），随后 q 行 l,r（0≤l≤r<n）。每行输出闭区间 [l,r] 的和。";p.Hint=@"前缀和多开一个零元素，区间和为 s[r+1]-s[l]。";p.Reference=@"#include <stdio.h>
int main(void)
{
    int n, q, s[101] = { 0 }, i, x, l, r;
    scanf(""%d%d"", &n, &q);
    for (i = 0; i < n; i++) {
        scanf(""%d"", &x);
        s[i + 1] = s[i] + x;
    }
    while (q--) {
        scanf(""%d%d"", &l, &r);
        printf(""%d\n"", s[r + 1] - s[l]);
    }
    return 0;
}
";break;
case 73:p.Statement=@"地图软件交换格网的行列方向。

输入行数 n、列数 m（各 1 到 8），随后 n×m 个整数（-100 到 100）。输出转置后的 m 行矩阵，每行 n 个整数。";p.Hint=@"输入 a[i][j]，输出按列遍历 a[i][j]。";p.Reference=@"#include <stdio.h>
int main(void)
{
    int n, m, a[8][8], i, j;
    scanf(""%d%d"", &n, &m);
    for (i = 0; i < n; i++) {
        for (j = 0; j < m; j++) {
            scanf(""%d"", &a[i][j]);
        }
    }
    for (j = 0; j < m; j++) {
        for (i = 0; i < n; i++) {
            printf(""%d "", a[i][j]);
        }
        puts("""");
    }
    return 0;
}
";break;
case 74:p.Statement=@"图像实验计算两张矩阵的乘积。

输入 n,m,k（各 1 到 6），随后 n×m 矩阵 A 与 m×k 矩阵 B（值 -20 到 20）。输出 n×k 的 A×B。";p.Hint=@"C[i][j] 是 A 的第 i 行与 B 的第 j 列对应元素乘积之和。";p.Reference=@"#include <stdio.h>
int main(void)
{
    int n, m, k, a[6][6], b[6][6], i, j, t, s;
    scanf(""%d%d%d"", &n, &m, &k);
    for (i = 0; i < n; i++) {
        for (j = 0; j < m; j++) {
            scanf(""%d"", &a[i][j]);
        }
    }
    for (i = 0; i < m; i++) {
        for (j = 0; j < k; j++) {
            scanf(""%d"", &b[i][j]);
        }
    }
    for (i = 0; i < n; i++) {
        for (j = 0; j < k; j++) {
            s = 0;
            for (t = 0; t < m; t++) {
                s += a[i][t] * b[t][j];
            }
            printf(""%d "", s);
        }
        puts("""");
    }
    return 0;
}
";break;
case 75:p.Statement=@"测绘实验计算二维变换的行列式。

输入 2×2 矩阵四个整数（-1000 到 1000），输出行列式 a*d-b*c。";p.Hint=@"函数接收二维数组，计算两条对角线乘积之差。";p.Reference=@"#include <stdio.h>
int det(int a[2][2])
{
    return a[0][0] * a[1][1] - a[0][1] * a[1][0];
}

int main(void)
{
    int a[2][2], i, j;
    for (i = 0; i < 2; i++) {
        for (j = 0; j < 2; j++) {
            scanf(""%d"", &a[i][j]);
        }
    }
    printf(""%d"", det(a));
    return 0;
}
";break;
case 76:p.Statement=@"检索器统计可重叠出现的关键词。

输入两个非空小写字符串 s,t（长度各 1 到 100），输出 t 在 s 中出现次数，允许重叠。";p.Hint=@"先循环求两个长度；枚举每个合法起点，再逐字符比较，保留重叠匹配。";p.Reference=@"#include <stdio.h>
static int text_length(const char s[]){int n=0;while(s[n])n++;return n;}
static int same_prefix(const char a[],const char b[],int n){int i;for(i=0;i<n;i++)if(a[i]!=b[i])return 1;return 0;}
int main(void)
{
    char s[101], t[101];
    int i, c = 0, n, m;
    scanf(""%100s%100s"", s, t);
    n = text_length(s);
    m = text_length(t);
    for (i = 0; i + m <= n; i++) {
        if (same_prefix(s + i, t, m) == 0) {
            c++;
        }
    }
    printf(""%d"", c);
    return 0;
}
";break;
case 77:p.Statement=@"通信端压缩连续重复的字符段。

输入一个非空小写字符串（长度 1 到 100），输出各连续段字符及次数，例如 aaabb 输出 a3b2。";p.Hint=@"找到一段相同字符的末尾，输出该字符和长度。";p.Reference=@"#include <stdio.h>
int main(void)
{
    char s[101];
    int i = 0, j;
    scanf(""%100s"", s);
    while (s[i]) {
        j = i + 1;
        while (s[j] == s[i]) {
            j++;
        }
        printf(""%c%d"", s[i], j - i);
        i = j;
    }
    return 0;
}
";break;
case 78:p.Statement=@"编辑器检查嵌套括号是否成对。

输入由 ()[]{} 组成的非空字符串（长度 1 到 100），全部正确配对输出 YES，否则 NO。";p.Hint=@"左括号入栈，右括号必须与栈顶匹配。";p.Reference=@"#include <stdio.h>
int main(void)
{
    char s[101], st[101], c;
    int i, top = 0, ok = 1;
    scanf(""%100s"", s);
    for (i = 0; s[i]; i++) {
        c = s[i];
        if (c == '(' || c == '[' || c == '{') {
            st[top++] = c;
        } else if (top == 0) {
            ok = 0;
        } else {
            char p = st[-- top];
            if (!((p == '(' && c == ')') || (p == '[' && c == ']') || (p == '{' && c == '}'))) {
                ok = 0;
            }
        }
    }
    printf(ok && top == 0 ? ""YES"" : ""NO"");
    return 0;
}
";break;
case 79:p.Statement=@"财务工具相加两个超长非负整数。

输入两个非负十进制整数字符串（各 1 到 100 位，无前导零，0 除外），输出它们的和。";p.Hint=@"先循环求长度，再从末位逐位相加并保存进位。";p.Reference=@"#include <stdio.h>
static int text_length(const char s[]){int n=0;while(s[n])n++;return n;}
int main(void)
{
    char a[101], b[101], s[102];
    int i, j, k = 0, c = 0, v;
    scanf(""%100s%100s"", a, b);
    i = text_length(a) - 1;
    j = text_length(b) - 1;
    while (i >= 0 || j >= 0 || c) {
        v = c;
        if (i >= 0) {
            v += a[i--] - '0';
        }
        if (j >= 0) {
            v += b[j--] - '0';
        }
        s[k++] = v % 10 + '0';
        c = v / 10;
    }
    while (k) {
        putchar(s[-- k]);
    }
    return 0;
}
";break;
case 80:p.Statement=@"教务处按成绩和编号整理学生记录。

输入 n（1 到 50），随后 n 行学生编号（互不相同，1 到 1000）及成绩（0 到 100）。按成绩降序、同分编号升序输出编号和成绩。";p.Hint=@"用结构体保存记录，使用双循环按成绩降序、同分编号升序交换。";p.Reference=@"#include <stdio.h>
typedef struct { int id, score; } Student;

int main(void)
{
    Student a[50];
    int n, i;
    scanf(""%d"", &n);
    for (i = 0; i < n; i++) {
        scanf(""%d%d"", &a[i] . id, &a[i] . score);
    }
    { int x,y; for(x=0;x<n;x++)for(y=x+1;y<n;y++)if(a[x].score<a[y].score||(a[x].score==a[y].score&&a[x].id>a[y].id)){Student t=a[x];a[x]=a[y];a[y]=t;} }
    for (i = 0; i < n; i++) {
        printf(""%d %d\n"", a[i] . id, a[i] . score);
    }
    return 0;
}
";break;
case 81:p.Statement=@"数据结构课堂用链表逆序保存样本。

输入 n（1 到 100），随后 n 个整数（-1000 到 1000）。用链表头插法保存，输出逆序数据并释放内存。";p.Hint=@"每个新结点插入表头，遍历后释放每个结点。";p.Reference=@"#include <stdio.h>
#include <stdlib.h>
typedef struct Node { int value; struct Node * next; } Node;

int main(void)
{
    int n, i;
    Node * head = NULL, * p;
    scanf(""%d"", &n);
    for (i = 0; i < n; i++) {
        p = malloc(sizeof(Node));
        if (!p) {
            return 1;
        }
        scanf(""%d"", &p -> value);
        p -> next = head;
        head = p;
    }
    while (head) {
        printf(""%d "", head -> value);
        p = head;
        head = head -> next;
        free(p);
    }
    return 0;
}
";break;
case 82:p.Statement=@"工具面板模拟后进先出的操作记录。

输入 q（1 到 100），随后 q 条命令：1 x 入栈（x 为 -1000 到 1000）；2 弹出并输出；3 输出栈顶不移除；4 输出长度。空栈的 2/3 输出 EMPTY；入栈无输出。";p.Hint=@"数组与 top 表示栈，先检查是否为空。";p.Reference=@"#include <stdio.h>
int main(void)
{
    int q, op, x, a[100], top = 0;
    scanf(""%d"", &q);
    while (q--) {
        scanf(""%d"", &op);
        if (op == 1) {
            scanf(""%d"", &x);
            a[top++] = x;
        } else if (op == 4) {
            printf(""%d\n"", top);
        } else if (top == 0) {
            puts(""EMPTY"");
        } else {
            printf(""%d\n"", op == 2 ? a[-- top] : a[top - 1]);
        }
    }
    return 0;
}
";break;
case 83:p.Statement=@"服务台模拟先到先办的排队记录。

输入 q（1 到 100），随后 q 条命令：1 x 入队（-1000 到 1000）；2 出队并输出；3 输出队首不移除；4 输出长度。空队列的 2/3 输出 EMPTY；入队无输出。";p.Hint=@"使用 head 与 tail，出队只移动 head。";p.Reference=@"#include <stdio.h>
int main(void)
{
    int q, op, x, a[100], h = 0, t = 0;
    scanf(""%d"", &q);
    while (q--) {
        scanf(""%d"", &op);
        if (op == 1) {
            scanf(""%d"", &x);
            a[t++] = x;
        } else if (op == 4) {
            printf(""%d\n"", t - h);
        } else if (h == t) {
            puts(""EMPTY"");
        } else {
            printf(""%d\n"", op == 2 ? a[h++] : a[h]);
        }
    }
    return 0;
}
";break;
case 84:p.Statement=@"送货机器人统计只能右行下行的路线。

输入 n,m（各 1 到 10），随后 n×m 个 0/1（1 为障碍）。从左上到右下仅向右或向下，输出路径数。起点或终点为障碍时输出 0。";p.Hint=@"当前位置路径数是上方与左方之和，障碍格归零。";p.Reference=@"#include <stdio.h>
int main(void)
{
    int n, m, i, j, x, d[11][11] = { 0 };
    scanf(""%d%d"", &n, &m);
    d[0][1] = 1;
    for (i = 1; i <= n; i++) {
        for (j = 1; j <= m; j++) {
            scanf(""%d"", &x);
            if (!x) {
                d[i][j] = d[i - 1][j] + d[i][j - 1];
            }
        }
    }
    printf(""%d"", d[n][m]);
    return 0;
}
";break;
case 85:p.Statement=@"自动售货机寻找最省硬币的付款方案。

输入金额 s（0 到 200），币种数 n（1 到 8），随后 n 个面额（1 到 50，可重复）。每种无限枚，输出组成金额所需最少枚数；无法组成输出 -1。";p.Hint=@"dp[0]=0，逐个金额尝试每种可使用面额。";p.Reference=@"#include <stdio.h>
int main(void)
{
    int s, n, a[8], d[201], i, j;
    scanf(""%d%d"", &s, &n);
    for (i = 0; i < n; i++) {
        scanf(""%d"", &a[i]);
    }
    d[0] = 0;
    for (i = 1; i <= s; i++) {
        d[i] = 1000;
        for (j = 0; j < n; j++) {
            if (a[j] <= i && d[i - a[j]] + 1 < d[i]) {
                d[i] = d[i - a[j]] + 1;
            }
        }
    }
    printf(""%d"", d[s] == 1000 ? - 1 : d[s]);
    return 0;
}
";break;
case 86:p.Statement=@"教练寻找可以跳过日期的严格进步序列。

输入 n（1 到 100）和 n 个整数（-1000 到 1000），输出最长严格递增子序列长度，元素可不连续。";p.Hint=@"dp[i] 从所有更小的前驱值转移，初始为 1。";p.Reference=@"#include <stdio.h>
int main(void)
{
    int n, a[100], d[100], i, j, b = 0;
    scanf(""%d"", &n);
    for (i = 0; i < n; i++) {
        scanf(""%d"", &a[i]);
    }
    for (i = 0; i < n; i++) {
        d[i] = 1;
        for (j = 0; j < i; j++) {
            if (a[j] < a[i] && d[j] + 1 > d[i]) {
                d[i] = d[j] + 1;
            }
        }
        if (d[i] > b) {
            b = d[i];
        }
    }
    printf(""%d"", b);
    return 0;
}
";break;
case 87:p.Statement=@"机器人在四向迷宫中寻找最短路线。

输入 n,m（各 1 到 10），随后 n×m 个 0/1（1 为墙）。从左上到右下可上下左右移动，输出最少步数；无法到达输出 -1。";p.Hint=@"广度优先搜索，入队时标记距离，阻塞起点时直接无解。";p.Reference=@"#include <stdio.h>
int main(void)
{
    int n, m, a[10][10], d[10][10], qx[100], qy[100], h = 0, t = 0, i, j, k, x, y, nx, ny, dx[] = { - 1, 1, 0, 0 }, dy[] = { 0, 0, - 1, 1 };
    scanf(""%d%d"", &n, &m);
    for (i = 0; i < n; i++) {
        for (j = 0; j < m; j++) {
            scanf(""%d"", &a[i][j]);
            d[i][j] = - 1;
        }
    }
    if (!a[0][0]) {
        d[0][0] = 0;
        qx[t] = 0;
        qy[t++] = 0;
    }
    while (h < t) {
        x = qx[h];
        y = qy[h++];
        for (k = 0; k < 4; k++) {
            nx = x + dx[k];
            ny = y + dy[k];
            if (nx >= 0 && nx < n && ny >= 0 && ny < m && !a[nx][ny] && d[nx][ny] < 0) {
                d[nx][ny] = d[x][y] + 1;
                qx[t] = nx;
                qy[t++] = ny;
            }
        }
    }
    printf(""%d"", d[n - 1][m - 1]);
    return 0;
}
";break;
case 88:p.Statement=@"学校需要可增改查删并生成排名的成绩终端。

编写可连续处理命令的成绩系统。输入 q（1 到 100），随后 q 条命令。1 id score：新增或修改（id 1..100，score 0..100），输出 OK；2 id：查询，输出成绩或 NOT_FOUND；3 id：删除，输出 OK 或 NOT_FOUND；4：输出人数和平均分（6 位小数，空集平均 0）；5：按成绩降序、同分编号升序输出所有 id score，空集输出 EMPTY。每个查询、操作或列表各占行。";p.Hint=@"将命令分开处理；排名时复制有效记录，用双循环比较成绩和编号。";p.Reference=@"#include <stdio.h>
typedef struct { int id, score; } Student;

int main(void)
{
    int s[101], q, op, id, v, i, n, sum;
    Student a[100];
    for (i = 0; i <= 100; i++) {
        s[i] = - 1;
    }
    scanf(""%d"", &q);
    while (q--) {
        scanf(""%d"", &op);
        if (op == 1) {
            scanf(""%d%d"", &id, &v);
            s[id] = v;
            puts(""OK"");
        } else if (op == 2) {
            scanf(""%d"", &id);
            if (s[id] < 0) {
                puts(""NOT_FOUND"");
            } else {
                printf(""%d\n"", s[id]);
            }
        } else if (op == 3) {
            scanf(""%d"", &id);
            puts(s[id] < 0 ? ""NOT_FOUND"" : ""OK"");
            s[id] = - 1;
        } else {
            n = 0;
            sum = 0;
            for (i = 1; i <= 100; i++) {
                if (s[i] >= 0) {
                    a[n] . id = i;
                    a[n++] . score = s[i];
                    sum += s[i];
                }
            }
            if (op == 4) {
                printf(""%d %.6f\n"", n, n ? (double) sum / n : 0.0);
            } else {
                { int x,y; for(x=0;x<n;x++)for(y=x+1;y<n;y++)if(a[x].score<a[y].score||(a[x].score==a[y].score&&a[x].id>a[y].id)){Student t=a[x];a[x]=a[y];a[y]=t;} }
                if (!n) {
                    puts(""EMPTY"");
                }
                for (i = 0; i < n; i++) {
                    printf(""%d %d\n"", a[i] . id, a[i] . score);
                }
            }
        }
    }
    return 0;
}
";break;
case 89:p.Statement=@"仓库需要支持整单校验的批量出库终端。

输入 n,q（1≤n≤20，1≤q≤100），n 个初始库存（0..1000）。商品 id 为 1..n。命令：1 id x 入库（x 1..100），输出 OK；2 id x 出库，库存足够才扣减并输出 OK，否则输出 REJECT；3 id 输出当前库存；4 输出所有库存总量；5 k 随后 k 对 id x（1≤k≤10）：批量订单，同一商品可重复，仅当各商品累计需求全部满足才一起扣减并输出 OK，否则不改库存并输出 REJECT。所有结果一行一个。";p.Hint=@"批量订单先累计需求，再验证，最后统一扣减，保证失败时库存不变。";p.Reference=@"#include <stdio.h>
int main(void)
{
    int n, q, s[21] = { 0 }, need[21], op, id, x, k, i, ok, sum;
    scanf(""%d%d"", &n, &q);
    for (i = 1; i <= n; i++) {
        scanf(""%d"", &s[i]);
    }
    while (q--) {
        scanf(""%d"", &op);
        if (op <= 3) {
            scanf(""%d"", &id);
            if (op == 3) {
                printf(""%d\n"", s[id]);
            } else {
                scanf(""%d"", &x);
                if (op == 1) {
                    s[id] += x;
                    puts(""OK"");
                } else if (s[id] >= x) {
                    s[id] -= x;
                    puts(""OK"");
                } else {
                    puts(""REJECT"");
                }
            }
        } else if (op == 4) {
            sum = 0;
            for (i = 1; i <= n; i++) {
                sum += s[i];
            }
            printf(""%d\n"", sum);
        } else {
            for (i = 1; i <= n; i++) {
                need[i] = 0;
            }
            scanf(""%d"", &k);
            while (k--) {
                scanf(""%d%d"", &id, &x);
                need[id] += x;
            }
            ok = 1;
            for (i = 1; i <= n; i++) {
                if (need[i] > s[i]) {
                    ok = 0;
                }
            }
            if (ok) {
                for (i = 1; i <= n; i++) {
                    s[i] -= need[i];
                }
            }
            puts(ok ? ""OK"" : ""REJECT"");
        }
    }
    return 0;
}
";break;
case 90:p.Statement=@"社区图书角需要同步维护借阅关系与库存。

输入 n,m,q（1≤n,m≤20，1≤q≤100），n 个图书初始总册数（0..10）。用户 id 为 1..m，书 id 为 1..n。命令：1 user book 借书：用户借阅总量最多 3 册，同种书最多 1 册，余量足够才成功；2 user book 还书：必须有借阅记录；上述均输出 OK 或 REJECT。3 book 输出该书可借册数；4 user 输出该用户借阅总数；5 输出所有图书可借总数与已借总数。每条结果独占一行。";p.Hint=@"用二维数组保存用户与图书关系，同时维护库存及用户借阅量，操作成功时同步更新。";p.Reference=@"#include <stdio.h>
int main(void)
{
    int n, m, q, s[21] = { 0 }, loan[21][21] = { 0 }, count[21] = { 0 }, op, u, b, i, avail, used;
    scanf(""%d%d%d"", &n, &m, &q);
    for (i = 1; i <= n; i++) {
        scanf(""%d"", &s[i]);
    }
    while (q--) {
        scanf(""%d"", &op);
        if (op <= 2) {
            scanf(""%d%d"", &u, &b);
            if (op == 1) {
                if (s[b] > 0 && !loan[u][b] && count[u] < 3) {
                    s[b]--;
                    loan[u][b] = 1;
                    count[u]++;
                    puts(""OK"");
                } else {
                    puts(""REJECT"");
                }
            } else if (loan[u][b]) {
                s[b]++;
                loan[u][b] = 0;
                count[u]--;
                puts(""OK"");
            } else {
                puts(""REJECT"");
            }
        } else if (op == 3) {
            scanf(""%d"", &b);
            printf(""%d\n"", s[b]);
        } else if (op == 4) {
            scanf(""%d"", &u);
            printf(""%d\n"", count[u]);
        } else {
            avail = used = 0;
            for (i = 1; i <= n; i++) {
                avail += s[i];
            }
            for (i = 1; i <= m; i++) {
                used += count[i];
            }
            printf(""%d %d\n"", avail, used);
        }
    }
    return 0;
}
";break;
} }NewBank.Add(a);return PracticeOrder.Arrange(a);}}