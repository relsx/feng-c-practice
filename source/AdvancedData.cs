using System;
using System.Collections.Generic;
static partial class Bank {
public static void AddAdvanced(List<Problem> a) {
a.Add(new Problem(61,@"指针交换",@"进阶一 · 函数与指针",@"输入两个整数（-10000 到 10000），使用指针函数交换，输出交换后的两个值。",@"函数接收两个地址，用临时变量交换解引用后的值。",@"#include <stdio.h>
#include <stdlib.h>
#include <string.h>

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
"));
a.Add(new Problem(62,@"指针遍历求和",@"进阶一 · 函数与指针",@"输入 n（1 到 100），随后 n 个整数（-1000 到 1000）。用指针遍历，输出总和。",@"让指针从数组起点移动到末尾。",@"#include <stdio.h>
#include <stdlib.h>
#include <string.h>

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
"));
a.Add(new Problem(63,@"递归阶乘",@"进阶一 · 函数与指针",@"输入 n（0 到 20），使用递归函数计算并输出 n!，使用 long long。",@"0! 为 1；递归返回 n 乘以 (n-1)!。",@"#include <stdio.h>
#include <stdlib.h>
#include <string.h>

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
"));
a.Add(new Problem(64,@"递归最大公约数",@"进阶一 · 函数与指针",@"输入两个非负整数（0 到 100000），递归求最大公约数；gcd(0,0)=0。",@"当 b=0 返回 a，否则调用 gcd(b,a%b)。",@"#include <stdio.h>
#include <stdlib.h>
#include <string.h>

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
"));
a.Add(new Problem(65,@"二分查找首位置",@"进阶二 · 数组与矩阵",@"输入 n（1 到 100）、n 个非递减整数（-1000 到 1000）、目标 x。输出首次出现的 0 起始下标，不存在输出 -1。",@"找到大于等于 x 的第一个位置，再检查是否相等。",@"#include <stdio.h>
#include <stdlib.h>
#include <string.h>

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
"));
a.Add(new Problem(66,@"合并有序数组",@"进阶二 · 数组与矩阵",@"输入 n,m（各 1 到 100），随后分别输入两个非递减数组（值 -1000 到 1000）。输出合并后的有序数组，保留重复项。",@"两个指针分别指向两个数组最小的未处理元素。",@"#include <stdio.h>
#include <stdlib.h>
#include <string.h>

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
"));
a.Add(new Problem(67,@"有序数组去重",@"进阶二 · 数组与矩阵",@"输入 n（1 到 100）和 n 个非递减整数（-1000 到 1000），输出去重后的数组。",@"仅在当前值与前一个值不同的时候输出。",@"#include <stdio.h>
#include <stdlib.h>
#include <string.h>

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
"));
a.Add(new Problem(68,@"众数与次数",@"进阶二 · 数组与矩阵",@"输入 n（1 到 100）及 n 个整数（-20 到 20）。输出出现最多的值及次数；并列选择较小的值。",@"用 41 个计数槽记录频率，从小到大更新最佳值。",@"#include <stdio.h>
#include <stdlib.h>
#include <string.h>

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
"));
a.Add(new Problem(69,@"第二大不同值",@"进阶二 · 数组与矩阵",@"输入 n（1 到 100）和 n 个整数（-1000 到 1000）。输出第二大的不同值，只有一个不同值时输出 NONE。",@"维护最大值与第二大值，遇到相等最大值时跳过。",@"#include <stdio.h>
#include <stdlib.h>
#include <string.h>

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
"));
a.Add(new Problem(70,@"最大连续子段和",@"进阶二 · 数组与矩阵",@"输入 n（1 到 100）和 n 个整数（-1000 到 1000），输出非空连续子段的最大和。",@"记录以当前位置结尾的最佳和，处理全负数。",@"#include <stdio.h>
#include <stdlib.h>
#include <string.h>

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
"));
a.Add(new Problem(71,@"固定窗口最大和",@"进阶二 · 数组与矩阵",@"输入 n,k（1≤k≤n≤100），随后 n 个整数（-1000 到 1000）。输出所有长度为 k 的连续窗口中的最大和。",@"先求第一个窗口和，每次加新元素减去旧元素。",@"#include <stdio.h>
#include <stdlib.h>
#include <string.h>

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
"));
a.Add(new Problem(72,@"前缀和区间查询",@"进阶二 · 数组与矩阵",@"输入 n,q（各 1 到 100），n 个整数（-1000 到 1000），随后 q 行 l,r（0≤l≤r<n）。每行输出闭区间 [l,r] 的和。",@"前缀和多开一个零元素，区间和为 s[r+1]-s[l]。",@"#include <stdio.h>
#include <stdlib.h>
#include <string.h>

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
"));
a.Add(new Problem(73,@"矩阵转置",@"进阶二 · 数组与矩阵",@"输入行数 n、列数 m（各 1 到 8），随后 n×m 个整数（-100 到 100）。输出转置后的 m 行矩阵，每行 n 个整数。",@"输入 a[i][j]，输出按列遍历 a[i][j]。",@"#include <stdio.h>
#include <stdlib.h>
#include <string.h>

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
"));
a.Add(new Problem(74,@"矩阵乘法",@"进阶二 · 数组与矩阵",@"输入 n,m,k（各 1 到 6），随后 n×m 矩阵 A 与 m×k 矩阵 B（值 -20 到 20）。输出 n×k 的 A×B。",@"C[i][j] 是 A 的第 i 行与 B 的第 j 列对应元素乘积之和。",@"#include <stdio.h>
#include <stdlib.h>
#include <string.h>

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
"));
a.Add(new Problem(75,@"二维行列式",@"进阶二 · 数组与矩阵",@"输入 2×2 矩阵四个整数（-1000 到 1000），输出行列式 a*d-b*c。",@"函数接收二维数组，计算两条对角线乘积之差。",@"#include <stdio.h>
#include <stdlib.h>
#include <string.h>

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
"));
a.Add(new Problem(76,@"子串出现次数",@"进阶三 · 字符串与结构体",@"输入两个非空小写字符串 s,t（长度各 1 到 100），输出 t 在 s 中出现次数，允许重叠。",@"每个起始位置用 strncmp 比较，注意边界。",@"#include <stdio.h>
#include <stdlib.h>
#include <string.h>

int main(void)
{
    char s[101], t[101];
    int i, c = 0, n, m;
    scanf(""%100s%100s"", s, t);
    n = strlen(s);
    m = strlen(t);
    for (i = 0; i + m <= n; i++) {
        if (strncmp(s + i, t, m) == 0) {
            c++;
        }
    }
    printf(""%d"", c);
    return 0;
}
"));
a.Add(new Problem(77,@"连续字符压缩",@"进阶三 · 字符串与结构体",@"输入一个非空小写字符串（长度 1 到 100），输出各连续段字符及次数，例如 aaabb 输出 a3b2。",@"找到一段相同字符的末尾，输出该字符和长度。",@"#include <stdio.h>
#include <stdlib.h>
#include <string.h>

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
"));
a.Add(new Problem(78,@"括号配对检查",@"进阶三 · 字符串与结构体",@"输入由 ()[]{} 组成的非空字符串（长度 1 到 100），全部正确配对输出 YES，否则 NO。",@"左括号入栈，右括号必须与栈顶匹配。",@"#include <stdio.h>
#include <stdlib.h>
#include <string.h>

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
"));
a.Add(new Problem(79,@"大整数加法",@"进阶三 · 字符串与结构体",@"输入两个非负十进制整数字符串（各 1 到 100 位，无前导零，0 除外），输出它们的和。",@"从末位开始逐位相加，保存进位，最后倒序输出。",@"#include <stdio.h>
#include <stdlib.h>
#include <string.h>

int main(void)
{
    char a[101], b[101], s[102];
    int i, j, k = 0, c = 0, v;
    scanf(""%100s%100s"", a, b);
    i = strlen(a) - 1;
    j = strlen(b) - 1;
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
"));
a.Add(new Problem(80,@"结构体成绩排名",@"进阶三 · 字符串与结构体",@"输入 n（1 到 50），随后 n 行学生编号（互不相同，1 到 1000）及成绩（0 到 100）。按成绩降序、同分编号升序输出编号和成绩。",@"结构体保存记录，排序时实现两个排序条件。",@"#include <stdio.h>
#include <stdlib.h>
#include <string.h>

typedef struct { int id, score; } Student;

int cmp(const void * a, const void * b)
{
    const Student * x = a, * y = b;
    return x -> score != y -> score ? y -> score - x -> score : x -> id - y -> id;
}

int main(void)
{
    Student a[50];
    int n, i;
    scanf(""%d"", &n);
    for (i = 0; i < n; i++) {
        scanf(""%d%d"", &a[i] . id, &a[i] . score);
    }
    qsort(a, n, sizeof(Student), cmp);
    for (i = 0; i < n; i++) {
        printf(""%d %d\n"", a[i] . id, a[i] . score);
    }
    return 0;
}
"));
a.Add(new Problem(81,@"链表逆序输出",@"进阶四 · 数据结构",@"输入 n（1 到 100），随后 n 个整数（-1000 到 1000）。用链表头插法保存，输出逆序数据并释放内存。",@"每个新结点插入表头，遍历后释放每个结点。",@"#include <stdio.h>
#include <stdlib.h>
#include <string.h>

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
"));
a.Add(new Problem(82,@"栈命令处理",@"进阶四 · 数据结构",@"输入 q（1 到 100），随后 q 条命令：1 x 入栈（x 为 -1000 到 1000）；2 弹出并输出；3 输出栈顶不移除；4 输出长度。空栈的 2/3 输出 EMPTY；入栈无输出。",@"数组与 top 表示栈，先检查是否为空。",@"#include <stdio.h>
#include <stdlib.h>
#include <string.h>

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
"));
a.Add(new Problem(83,@"队列命令处理",@"进阶四 · 数据结构",@"输入 q（1 到 100），随后 q 条命令：1 x 入队（-1000 到 1000）；2 出队并输出；3 输出队首不移除；4 输出长度。空队列的 2/3 输出 EMPTY；入队无输出。",@"使用 head 与 tail，出队只移动 head。",@"#include <stdio.h>
#include <stdlib.h>
#include <string.h>

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
"));
a.Add(new Problem(84,@"带障碍网格路径数",@"进阶五 · 算法与动态规划",@"输入 n,m（各 1 到 10），随后 n×m 个 0/1（1 为障碍）。从左上到右下仅向右或向下，输出路径数。起点或终点为障碍时输出 0。",@"当前位置路径数是上方与左方之和，障碍格归零。",@"#include <stdio.h>
#include <stdlib.h>
#include <string.h>

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
"));
a.Add(new Problem(85,@"最少硬币数",@"进阶五 · 算法与动态规划",@"输入金额 s（0 到 200），币种数 n（1 到 8），随后 n 个面额（1 到 50，可重复）。每种无限枚，输出组成金额所需最少枚数；无法组成输出 -1。",@"dp[0]=0，逐个金额尝试每种可使用面额。",@"#include <stdio.h>
#include <stdlib.h>
#include <string.h>

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
"));
a.Add(new Problem(86,@"最长严格递增子序列",@"进阶五 · 算法与动态规划",@"输入 n（1 到 100）和 n 个整数（-1000 到 1000），输出最长严格递增子序列长度，元素可不连续。",@"dp[i] 从所有更小的前驱值转移，初始为 1。",@"#include <stdio.h>
#include <stdlib.h>
#include <string.h>

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
"));
a.Add(new Problem(87,@"迷宫最短距离",@"进阶五 · 算法与动态规划",@"输入 n,m（各 1 到 10），随后 n×m 个 0/1（1 为墙）。从左上到右下可上下左右移动，输出最少步数；无法到达输出 -1。",@"广度优先搜索，入队时标记距离，阻塞起点时直接无解。",@"#include <stdio.h>
#include <stdlib.h>
#include <string.h>

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
"));
a.Add(new Problem(88,@"项目考试 · 学生成绩管理",@"终章 · 综合项目考试",@"编写可连续处理命令的成绩系统。输入 q（1 到 100），随后 q 条命令。1 id score：新增或修改（id 1..100，score 0..100），输出 OK；2 id：查询，输出成绩或 NOT_FOUND；3 id：删除，输出 OK 或 NOT_FOUND；4：输出人数和平均分（6 位小数，空集平均 0）；5：按成绩降序、同分编号升序输出所有 id score，空集输出 EMPTY。每个查询、操作或列表各占行。",@"拆分增改、查询、删除、统计和排名函数；用结构体或索引数组管理有效记录。",@"#include <stdio.h>
#include <stdlib.h>
#include <string.h>

typedef struct { int id, score; } Student;

int cmp(const void * a, const void * b)
{
    const Student * x = a, * y = b;
    return x -> score != y -> score ? y -> score - x -> score : x -> id - y -> id;
}

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
                qsort(a, n, sizeof(Student), cmp);
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
"));
a.Add(new Problem(89,@"项目考试 · 仓储订单系统",@"终章 · 综合项目考试",@"输入 n,q（1≤n≤20，1≤q≤100），n 个初始库存（0..1000）。商品 id 为 1..n。命令：1 id x 入库（x 1..100），输出 OK；2 id x 出库，库存足够才扣减并输出 OK，否则输出 REJECT；3 id 输出当前库存；4 输出所有库存总量；5 k 随后 k 对 id x（1≤k≤10）：批量订单，同一商品可重复，仅当各商品累计需求全部满足才一起扣减并输出 OK，否则不改库存并输出 REJECT。所有结果一行一个。",@"批量订单先累计需求，再验证，最后统一扣减，保证失败时库存不变。",@"#include <stdio.h>
#include <stdlib.h>
#include <string.h>

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
"));
a.Add(new Problem(90,@"项目考试 · 图书借阅系统",@"终章 · 综合项目考试",@"输入 n,m,q（1≤n,m≤20，1≤q≤100），n 个图书初始总册数（0..10）。用户 id 为 1..m，书 id 为 1..n。命令：1 user book 借书：用户借阅总量最多 3 册，同种书最多 1 册，余量足够才成功；2 user book 还书：必须有借阅记录；上述均输出 OK 或 REJECT。3 book 输出该书可借册数；4 user 输出该用户借阅总数；5 输出所有图书可借总数与已借总数。每条结果独占一行。",@"用二维数组保存用户与图书关系，同时维护库存及用户借阅量，操作成功时同步更新。",@"#include <stdio.h>
#include <stdlib.h>
#include <string.h>

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
"));
} }