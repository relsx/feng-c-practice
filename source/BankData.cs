using System;
using System.Collections.Generic;
static partial class Bank {
public static List<Problem> All() { var a=new List<Problem>();
a.Add(new Problem(1,@"两数之和",@"输入输出",@"输入两个整数 a、b（-10000 到 10000），输出 a+b。",@"scanf 读取两个整数，printf 输出结果。",@"#include <stdio.h>

int main(void)
{
    int a, b;
    scanf(""%d%d"", &a, &b);
    printf(""%d"", a + b);
    return 0;
}
"));
a.Add(new Problem(2,@"两数之差",@"输入输出",@"输入两个整数 a、b（-10000 到 10000），输出 a-b。",@"注意运算顺序。",@"#include <stdio.h>

int main(void)
{
    int a, b;
    scanf(""%d%d"", &a, &b);
    printf(""%d"", a - b);
    return 0;
}
"));
a.Add(new Problem(3,@"两数之积",@"输入输出",@"输入两个整数 a、b（-1000 到 1000），输出 a*b。",@"使用乘法运算符。",@"#include <stdio.h>

int main(void)
{
    int a, b;
    scanf(""%d%d"", &a, &b);
    printf(""%d"", a * b);
    return 0;
}
"));
a.Add(new Problem(4,@"商和余数",@"输入输出",@"输入非负整数 a（0 到 10000）和正整数 b（1 到 100）。依次输出整数商、余数，以空格分隔。",@"整数除法 / 和取余 %。",@"#include <stdio.h>

int main(void)
{
    int a, b;
    scanf(""%d%d"", &a, &b);
    printf(""%d %d"", a / b, a % b);
    return 0;
}
"));
a.Add(new Problem(5,@"三个数的平均值",@"输入输出",@"输入三个整数（-1000 到 1000），输出它们的平均值，保留至少 4 位小数。",@"使用浮点除法，避免整数截断。",@"#include <stdio.h>

int main(void)
{
    int a, b, c;
    scanf(""%d%d%d"", &a, &b, &c);
    printf(""%.6f"", (a + b + c) / 3.0);
    return 0;
}
"));
a.Add(new Problem(6,@"摄氏转华氏",@"输入输出",@"输入摄氏温度 c（整数，-100 到 100），输出华氏温度 c*9/5+32，保留至少 4 位小数。",@"9.0/5.0 可以避免整数除法。",@"#include <stdio.h>

int main(void)
{
    int c;
    scanf(""%d"", &c);
    printf(""%.6f"", c * 9.0 / 5.0 + 32);
    return 0;
}
"));
a.Add(new Problem(7,@"圆的面积",@"输入输出",@"输入整数半径 r（0 到 100）。使用 π=3.141592653589793，输出圆面积，保留至少 4 位小数。",@"面积为 π*r*r。",@"#include <stdio.h>

int main(void)
{
    int r;
    scanf(""%d"", &r);
    printf(""%.6f"", 3.141592653589793 * r * r);
    return 0;
}
"));
a.Add(new Problem(8,@"交换两个数",@"输入输出",@"输入两个整数 a、b（-10000 到 10000），按 b a 的顺序输出。",@"输出顺序交换即可。",@"#include <stdio.h>

int main(void)
{
    int a, b;
    scanf(""%d%d"", &a, &b);
    printf(""%d %d"", b, a);
    return 0;
}
"));
a.Add(new Problem(9,@"绝对值",@"输入输出",@"输入一个整数 n（-100000 到 100000），输出它的绝对值。",@"负数要变号，0 的绝对值是 0。",@"#include <stdio.h>

int main(void)
{
    int n;
    scanf(""%d"", &n);
    printf(""%d"", n < 0 ? - n : n);
    return 0;
}
"));
a.Add(new Problem(10,@"两点距离的平方",@"输入输出",@"输入 x1 y1 x2 y2，坐标均为 -100 到 100 的整数。输出 (x1-x2)^2+(y1-y2)^2。",@"这里输出平方，不需要开平方。",@"#include <stdio.h>

int main(void)
{
    int a, b, c, d;
    scanf(""%d%d%d%d"", &a, &b, &c, &d);
    printf(""%d"", (a - c) * (a - c) + (b - d) * (b - d));
    return 0;
}
"));
a.Add(new Problem(11,@"较大的数",@"条件判断",@"输入两个整数（-10000 到 10000），输出较大的一个。",@"两个数相等时输出其共同值。",@"#include <stdio.h>

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
"));
a.Add(new Problem(12,@"三个数的最大值",@"条件判断",@"输入三个整数（-10000 到 10000），输出最大值。",@"可以逐个更新最大值。",@"#include <stdio.h>

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
"));
a.Add(new Problem(13,@"奇数或偶数",@"条件判断",@"输入整数 n（-100000 到 100000）。偶数输出 EVEN，奇数输出 ODD。",@"负奇数的余数可能为 -1，判断是否等于 0。",@"#include <stdio.h>

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
"));
a.Add(new Problem(14,@"判断正负",@"条件判断",@"输入整数 n（-100000 到 100000）。正数输出 POSITIVE，负数输出 NEGATIVE，零输出 ZERO。",@"零需要单独处理。",@"#include <stdio.h>

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
"));
a.Add(new Problem(15,@"判断闰年",@"条件判断",@"输入年份 y（1 到 9999）。闰年输出 YES，否则输出 NO。",@"能被 400 整除，或能被 4 整除且不能被 100 整除。",@"#include <stdio.h>

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
"));
a.Add(new Problem(16,@"成绩等级",@"条件判断",@"输入整数成绩 s（0 到 100）。90~100 为 A，80~89 为 B，70~79 为 C，60~69 为 D，0~59 为 F。输出等级。",@"从高分到低分依次判断。",@"#include <stdio.h>

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
"));
a.Add(new Problem(17,@"三角形类型",@"条件判断",@"输入三个边长（整数，1 到 100）。不能组成三角形输出 INVALID；等边输出 EQUILATERAL；等腰输出 ISOSCELES；其他输出 SCALENE。",@"先判断两边之和大于第三边，再判断相等关系。",@"#include <stdio.h>

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
"));
a.Add(new Problem(18,@"字符分类",@"条件判断",@"输入一个非空白 ASCII 字符。数字输出 DIGIT，英文字母输出 LETTER，其他输出 OTHER。",@"判断字符区间 0~9、a~z、A~Z。",@"#include <stdio.h>

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
"));
a.Add(new Problem(19,@"简易计算器",@"条件判断",@"输入整数 a、运算符、整数 b，例如 8 / 2。a、b 范围 -100 到 100，运算符为 + - * /。输出浮点结果（至少 4 位小数），除数为 0 时输出 ERROR。",@"除法时先检查 0，再转换成浮点数。",@"#include <stdio.h>

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
"));
a.Add(new Problem(20,@"月份天数",@"条件判断",@"输入年份 y（1 到 9999）和月份 m（1 到 12）。输出该月天数，二月需考虑闰年。",@"用数组存月份天数，单独调整二月。",@"#include <stdio.h>

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
"));
a.Add(new Problem(21,@"从 1 加到 n",@"循环与数论",@"输入整数 n（0 到 10000），输出 1+2+...+n。n=0 时输出 0。",@"累加或使用 n*(n+1)/2。",@"#include <stdio.h>

int main(void)
{
    int n;
    scanf(""%d"", &n);
    printf(""%d"", n * (n + 1) / 2);
    return 0;
}
"));
a.Add(new Problem(22,@"阶乘",@"循环与数论",@"输入 n（0 到 20），输出 n!。0!=1，结果使用 64 位整数。",@"long long 可以容纳 20!，用 %lld 输出。",@"#include <stdio.h>

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
"));
a.Add(new Problem(23,@"斐波那契数",@"循环与数论",@"输入 n（0 到 40），输出 F(n)。定义 F(0)=0、F(1)=1，之后为前两项之和。",@"迭代更新两个变量。",@"#include <stdio.h>

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
"));
a.Add(new Problem(24,@"数字各位之和",@"循环与数论",@"输入非负整数 n（0 到 1000000000），输出十进制各位数字之和。",@"反复 n%10 取末位、n/=10 去末位。",@"#include <stdio.h>

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
"));
a.Add(new Problem(25,@"整数反转",@"循环与数论",@"输入非负整数 n（0 到 100000000），输出十进制反转后的整数。前导零省略，例如 1200 输出 21。",@"结果不断乘 10 并加上末位。",@"#include <stdio.h>

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
"));
a.Add(new Problem(26,@"判断素数",@"循环与数论",@"输入 n（0 到 100000），素数输出 YES，否则输出 NO。",@"0、1 不是素数；只需试除到平方根。",@"#include <stdio.h>

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
"));
a.Add(new Problem(27,@"最大公约数",@"循环与数论",@"输入非负整数 a、b（0 到 10000），输出最大公约数。约定 gcd(0,0)=0。",@"欧几里得算法，循环取余。",@"#include <stdio.h>

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
"));
a.Add(new Problem(28,@"最小公倍数",@"循环与数论",@"输入非负整数 a、b（0 到 10000），输出最小公倍数。任一数为 0 时结果为 0。",@"先求 gcd，先除后乘。",@"#include <stdio.h>

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
"));
a.Add(new Problem(29,@"整数幂",@"循环与数论",@"输入底数 a（-10 到 10）和指数 n（0 到 9），输出 a^n。约定任何数的 0 次方为 1，包括 0^0。",@"初始化结果为 1，循环相乘。",@"#include <stdio.h>

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
"));
a.Add(new Problem(30,@"数字位数",@"循环与数论",@"输入非负整数 n（0 到 1000000000），输出十进制位数，0 的位数为 1。",@"do...while 可以处理 0。",@"#include <stdio.h>

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
"));
a.Add(new Problem(31,@"统计区间素数",@"循环与数论",@"输入 n（0 到 2000），输出闭区间 [1,n] 中素数个数。",@"对每个数判断是否为素数。",@"#include <stdio.h>

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
"));
a.Add(new Problem(32,@"奇数求和",@"循环与数论",@"输入 n（0 到 10000），输出 [1,n] 中所有奇数之和。",@"循环步长为 2。",@"#include <stdio.h>

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
"));
a.Add(new Problem(33,@"一行乘法表",@"循环与数论",@"输入 n（1 到 9），依次输出 n*1 到 n*9 共 9 个结果，以空格分隔。",@"循环 i 从 1 到 9，只输出数字。",@"#include <stdio.h>

int main(void)
{
    int n, i;
    scanf(""%d"", &n);
    for (i = 1; i <= 9; i++) {
        printf(""%d "", n * i);
    }
    return 0;
}
"));
a.Add(new Problem(34,@"判断完全数",@"循环与数论",@"输入 n（1 到 10000）。若 n 等于所有小于自身的正因子之和，输出 YES，否则 NO。",@"1 不是完全数；不要把自身计入。",@"#include <stdio.h>

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
"));
a.Add(new Problem(35,@"阿姆斯特朗数",@"循环与数论",@"输入 n（0 到 9999）。若每位数字的 k 次方之和等于 n，输出 YES，否则 NO；k 是十进制位数，0 的位数为 1。",@"先数位数，再逐位累加。",@"#include <stdio.h>

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
"));
a.Add(new Problem(36,@"正因子个数",@"循环与数论",@"输入 n（1 到 100000），输出正因子个数，包括 1 和 n。",@"因子成对出现，完全平方数的平方根只算一次。",@"#include <stdio.h>

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
"));
a.Add(new Problem(37,@"平方和",@"循环与数论",@"输入 n（0 到 1000），输出 1^2+2^2+...+n^2。",@"累加 i*i。",@"#include <stdio.h>

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
"));
a.Add(new Problem(38,@"转换二进制",@"循环与数论",@"输入非负整数 n（0 到 65535），输出不带前导零的二进制字符串，n=0 时输出 0。",@"把余数保存后倒序输出。",@"#include <stdio.h>

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
"));
a.Add(new Problem(39,@"二进制中 1 的个数",@"循环与数论",@"输入 n（0 到 65535），输出二进制表示中数字 1 的个数。",@"可用位运算，也可反复除以 2。",@"#include <stdio.h>

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
"));
a.Add(new Problem(40,@"回文整数",@"循环与数论",@"输入 n（0 到 100000000），十进制表示正反一致输出 YES，否则 NO。",@"反转数字后比较。",@"#include <stdio.h>

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
"));
a.Add(new Problem(41,@"数组求和",@"数组",@"第一行输入 n（1 到 50），第二行输入 n 个整数（-1000 到 1000）。输出所有元素之和。",@"初始化总和为 0。",@"#include <stdio.h>

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
"));
a.Add(new Problem(42,@"数组最大值",@"数组",@"第一行输入 n（1 到 50），第二行输入 n 个整数（-1000 到 1000）。输出最大值。",@"用第一个元素初始化，不能默认最大值为 0。",@"#include <stdio.h>

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
"));
a.Add(new Problem(43,@"数组最小值",@"数组",@"第一行输入 n（1 到 50），第二行输入 n 个整数（-1000 到 1000）。输出最小值。",@"用第一个元素初始化。",@"#include <stdio.h>

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
"));
a.Add(new Problem(44,@"数组逆序",@"数组",@"第一行输入 n（1 到 50），第二行输入 n 个整数（-1000 到 1000）。按逆序输出全部元素，以空格分隔。",@"从下标 n-1 开始输出。",@"#include <stdio.h>

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
"));
a.Add(new Problem(45,@"数组升序排序",@"数组",@"第一行输入 n（1 到 50），第二行输入 n 个整数（-1000 到 1000）。输出升序排列后的数组，以空格分隔。",@"可以使用冒泡或选择排序，保留重复元素。",@"#include <stdio.h>

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
"));
a.Add(new Problem(46,@"查找首个下标",@"数组",@"第一行输入 n（1 到 50），第二行输入 n 个整数（-1000 到 1000）。第三行输入待查整数 x（-1000 到 1000）。输出第一次出现的下标（从 0 开始），不存在输出 -1。",@"找到后立即结束查找。",@"#include <stdio.h>

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
"));
a.Add(new Problem(47,@"统计达标元素",@"数组",@"第一行输入 n（1 到 50），第二行输入 n 个整数（-1000 到 1000）。第三行输入阈值 x（-1000 到 1000）。输出大于等于 x 的元素个数。",@"注意包含等于。",@"#include <stdio.h>

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
"));
a.Add(new Problem(48,@"不同元素个数",@"数组",@"第一行输入 n（1 到 50），第二行输入 n 个整数（-1000 到 1000）。输出不同整数的个数。",@"对每个元素检查前面是否已出现。",@"#include <stdio.h>

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
"));
a.Add(new Problem(49,@"数组循环右移",@"数组",@"第一行输入 n（1 到 50），第二行输入 n 个整数（-1000 到 1000）。第三行输入 k（0 到 100）。将数组循环右移 k 位后输出，以空格分隔。",@"用 k%n 处理超过长度的移动。",@"#include <stdio.h>

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
"));
a.Add(new Problem(50,@"数组前缀和",@"数组",@"第一行输入 n（1 到 50），第二行输入 n 个整数（-1000 到 1000）。依次输出 a[0]、a[0]+a[1]、...、全部元素之和，以空格分隔。",@"保存一个累计和。",@"#include <stdio.h>

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
"));
a.Add(new Problem(51,@"字符串长度",@"字符串与矩阵",@"输入一行不含空白的 ASCII 字符串，长度 1 到 80，由英文字母和数字组成。输出字符数。",@"strlen 或循环到 \0。",@"#include <stdio.h>
#include <string.h>

int main(void)
{
    char s[100];
    scanf(""%99s"", s);
    printf(""%d"", (int) strlen(s));
    return 0;
}
"));
a.Add(new Problem(52,@"反转字符串",@"字符串与矩阵",@"输入一行不含空白的 ASCII 字符串，长度 1 到 80，由英文字母和数字组成。输出反转后的字符串。",@"从末尾字符向前输出。",@"#include <stdio.h>
#include <string.h>

int main(void)
{
    char s[100];
    scanf(""%99s"", s);
    int i;
    for (i = (int) strlen(s) - 1; i >= 0; i--) {
        putchar(s[i]);
    }
    return 0;
}
"));
a.Add(new Problem(53,@"统计元音",@"字符串与矩阵",@"输入一行不含空白的 ASCII 字符串，长度 1 到 80，由英文字母和数字组成。输出 a e i o u 的总个数，不区分大小写。",@"可先转成小写后判断。",@"#include <stdio.h>
#include <string.h>

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
"));
a.Add(new Problem(54,@"回文字符串",@"字符串与矩阵",@"输入一行不含空白的 ASCII 字符串，长度 1 到 80，由英文字母和数字组成。区分大小写，正反一致输出 YES，否则 NO。",@"比较两端字符。",@"#include <stdio.h>
#include <string.h>

int main(void)
{
    char s[100];
    scanf(""%99s"", s);
    int i, n = (int) strlen(s), ok = 1;
    for (i = 0; i < n / 2; i++) {
        if (s[i] != s[n - 1 - i]) {
            ok = 0;
        }
    }
    printf(ok ? ""YES"" : ""NO"");
    return 0;
}
"));
a.Add(new Problem(55,@"统计数字字符",@"字符串与矩阵",@"输入一行不含空白的 ASCII 字符串，长度 1 到 80，由英文字母和数字组成。输出数字字符 0~9 的个数。",@"字符值要与字符常量比较。",@"#include <stdio.h>

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
"));
a.Add(new Problem(56,@"转换大写",@"字符串与矩阵",@"输入一行不含空白的 ASCII 字符串，长度 1 到 80，由英文字母和数字组成。把小写英文字母改为大写，其他字符不变，输出结果。",@"使用 toupper 或字符偏移。",@"#include <stdio.h>
#include <ctype.h>

int main(void)
{
    char s[100];
    scanf(""%99s"", s);
    int i;
    for (i = 0; s[i]; i++) {
        putchar(toupper((unsigned char) s[i]));
    }
    return 0;
}
"));
a.Add(new Problem(57,@"统计单词",@"字符串与矩阵",@"输入一行（长度 0 到 80），只含英文字母和空格，可以为空或含连续、首尾空格。输出单词个数；单词是连续非空格字符。",@"用 fgets 读整行，在空格到字母的切换处计数。",@"#include <stdio.h>

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
"));
a.Add(new Problem(58,@"替换指定字符",@"字符串与矩阵",@"输入一行不含空白的 ASCII 字符串，长度 1 到 80，由英文字母和数字组成。第二行输入两个字母或数字 a、b（空格分隔）。把字符串中所有 a 替换为 b，区分大小写，输出结果。",@"逐字符判断。",@"#include <stdio.h>

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
"));
a.Add(new Problem(59,@"字符出现次数",@"字符串与矩阵",@"输入一行不含空白的 ASCII 字符串，长度 1 到 80，由英文字母和数字组成。第二行输入一个字母或数字 c。输出 c 的出现次数，区分大小写。",@"计数初值为 0。",@"#include <stdio.h>

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
"));
a.Add(new Problem(60,@"矩阵主对角线之和",@"字符串与矩阵",@"第一行输入 n（1 到 8），接着输入 n 行，每行 n 个整数（-100 到 100）。输出主对角线（行下标等于列下标）的元素之和。",@"读入所有元素，只累加 i==j 的位置。",@"#include <stdio.h>

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
"));
AddAdvanced(a); return a; } }