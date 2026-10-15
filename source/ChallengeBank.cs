using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
sealed class ChallengeLevel {
 public int Number; public Problem Problem; public string Learning;
 public int Seconds{get{return 180+(Number-1)*420/49;}}
}
static class ChallengeBank {
 static string C(string body,string global=""){return "#include <stdio.h>\n#include <stdlib.h>\n#include <string.h>\n#include <limits.h>\n\n"+global+"\nint main(void)\n{\n"+body+"\n    return 0;\n}\n";}
 static ChallengeLevel L(int n,string title,string statement,string lesson,string body,string global=""){
  return new ChallengeLevel{Number=n,Problem=new Problem(100+n,title,"暗域 · 第 "+((n-1)/10+1)+" 章",statement,"闯关答案仅在通关后开放。",C(body,global)),Learning=lesson+Extension(n)+"\n\n阅读知识讲解后，请独立设计算法并从空白文件编写。只输出题目规定内容。"};
 }
 static string Extension(int n){switch(n){case 1:return "\n新增要求：普通前缀与绝对下标加权前缀表示不同信息。查询的局部权重从1开始，需要将绝对下标转换为相对下标。";case 4:return "\n新增要求：扫描时记录栈深度最大值；错误定位以输入位置为准，区分错误右括号和扫描结束尚未闭合。";case 10:return "\n新增要求：大整数比较先比长度、等长再按字典序；减法从低位逐位借位，必须先选择较大的被减数。相等时差为0。";case 13:return "\n新增要求：完整匹配结束位置与起点有长度偏移，输出为1基下标；无匹配仍须输出第二行0。";case 18:return "\n新增要求：三重乘积要核对两个内部维度。可选任意结合顺序，但两次中间矩阵尺寸不同；乘法不满足交换律。";case 24:return "\n新增要求：访问状态是(行,列,是否持钥匙)。同一位置没钥匙和有钥匙是不同状态，可以分别入队；否则可能错误地剪掉合法路线。";case 32:return "\n新增要求：有限库存不能用无限硬币转移。每类最多取规定数量，或拆成对应数量的0/1物品；零库存不得产生可达状态。";case 33:return "\n新增要求：最优长度相同时累计前驱计数，严格更优时替换计数；相等值不能互相延伸。数量按下标序列计数并及时取模。";default:return "";}}
 public static readonly List<ChallengeLevel> Levels=TeachNewSyntax(Build());
 static List<ChallengeLevel> TeachNewSyntax(List<ChallengeLevel> levels){
  string[][] notes={
   new[]{@"#include", "预处理：#include <stdio.h> 提供输入输出声明，stdlib.h 提供分配和排序，string.h 提供字符串操作，limits.h 提供整型边界。仅在需要对应功能时包含头文件。"},
   new[]{@"\bint\b", "类型与初始化：int 为整数，long long 容量更大；局部变量先初始化再读取。声明后的 = 设置初值。{0} 将数组全部初始化为0。"},
   new[]{@"\bmain\b", "入口与函数：int main(void) 表示无参数入口；return 0 正常退出。函数定义由返回类型、名称、参数列表和函数体组成，调用前要有声明。"},
   new[]{@"\bscanf\b", "输入与输出：scanf 按格式读取，&x 提供变量地址；%d、%lld、%llu、%s 分别对应 int、long long、unsigned long long、字符串。字符数组传数组名，%Ns 限制最大读取长度；printf 不传 &。格式和类型必须一致。"},
   new[]{@"\bfor\b|\bwhile\b", "循环：for(初值;条件;更新) 和 while(条件) 都在条件成立时重复；i++/i-- 使用旧值后增减，++i/--i 先增减。while(q--) 用旧值判断，再将q减1。break 退出当前循环，continue 进入下一轮。"},
   new[]{@"\[", "数组与地址：下标从0开始，大小N的合法下标为0到N-1。a[i] 是元素，&a[i] 与 a+i 表示其地址。数组参数通常转为指针，不携带长度，需另传长度。"},
   new[]{@"\+|\-", "表达式：+ - * / % 进行算术运算，整数除法截断。+= -= *= /= %= 是复合赋值。先检查溢出；(long long)x*y 在乘法前扩展类型。比较使用 == != < <= > >=，= 是赋值。"},
   new[]{@"\bif\b|\?", "选择与逻辑：if/else 决定分支；条件?真值:假值 是条件运算符。&& 和 || 短路，! 取逻辑反；先测试范围再访问数组。它们不同于逐位的 &、|。"},
   new[]{@"\bchar\b", "字符与字符串：'a' 是字符，\"abc\" 是包含结尾 '\0' 的字符串；字符可通过相减得到相对编码。\\n 是换行，\\t 是制表符，\\\\ 表示一个反斜杠；字符串缓冲区多留结束符的位置。"},
   new[]{@"\bstrlen\b|\bstrcmp\b|\bstrncmp\b", "字符串库：strlen 返回不含结束符的长度，返回类型 size_t；strcmp/strncmp 返回负、零、正，零表示内容相同。不能用 == 比较两个字符串的内容。strncmp 只比较最多指定字节数。"},
   new[]{@"\bstruct\b|\btypedef\b", "记录类型：struct 合并字段，typedef 提供别名。对象.field 访问字段，pointer->field 访问指针所指对象字段；const 表示不得通过该变量改写对象。"},
   new[]{@"\bvoid\s*\*|\bint\s*\*|\bchar\s*\*", "指针：T *p 保存T对象的地址；*p 解引用，&x 取地址。NULL 不能解引用；指针运算只能在同一数组的合法范围内。void* 是通用对象指针，读取字段前要转回正确类型。"},
   new[]{@"\bqsort\b|\bsizeof\b", "排序与大小：sizeof 返回对象或类型的字节数。qsort(base,count,sizeof(element),compare) 通过比较函数排序；比较函数接收两个 const void* 并返回负、零或正。不要修改被比较的元素。"},
   new[]{@"\bstatic\b", "存储期：静态局部或全局对象生存到程序结束并默认零初始化；普通局部对象随作用域结束而失效，未初始化的值不可依赖。避免把局部对象地址带出其生存期。"},
   new[]{@">>|<<|\&1|\&-i|\|=", "位运算：& 按位与，| 按位或，^ 按位异或；<< 和 >> 移位。1<<i 构造第i位，x&1 测最低位，i&-i 提取低位。移位位数要在类型宽度内，注意有符号数规则。"},
   new[]{@"\bmemcpy\b", "内存复制：memcpy(dest,src,size) 按字节复制，不保证字符串结束符，要求区域不重叠；memmove 允许重叠。sizeof(整个数组) 可计算静态数组总字节数，指针的 sizeof 不能代替数组大小。"}
  };
  var seen=new HashSet<string>();foreach(var l in levels){var fresh=new List<string>();foreach(var a in notes)if(!seen.Contains(a[0])&&System.Text.RegularExpressions.Regex.IsMatch(l.Problem.Reference,a[0])){seen.Add(a[0]);fresh.Add(a[1]);}if(fresh.Count>0)l.Learning+="\n\n本关首次出现的 C 语法与用法\n\n"+string.Join("\n\n",fresh.ToArray());}return levels;
 }
 static List<ChallengeLevel> Build(){return new List<ChallengeLevel>{
 L(1,"密室账本 · 双重区间和","输入 n q（1≤n≤80，1≤q≤20），随后 n 个整数（绝对值≤1000），再输入 q 组 l r（1≤l≤r≤n）。每次查询输出两个整数：区间普通和，以及从左端开始以1,2,3,...为权重的加权和，即 Σ(i-l+1)*a[i]。",
 "新语法：#include 引入声明；int main(void) 是程序入口；scanf 返回成功读取项数，%d 对应 int，& 取变量地址；printf 的 %lld 对应 long long；\\n 换行。int a[81] 声明定长数组，下标从 0 开始。for(初始化;条件;更新) 重复执行；if 只执行条件成立的分支；return 0 表示正常退出。{ } 限定代码块和局部变量作用域。\n用法：前缀数组 s[i] 记录前 i 项之和，区间信息可由两个前缀相减得到。注意 1 基坐标与 0 基数组的换算。预处理 O(n)，每次查询 O(1)。",
 "    int n,q,l,r; long long s[81]={0},w[81]={0};\n    scanf(\"%d%d\",&n,&q);\n    for(int i=1;i<=n;i++){int x;scanf(\"%d\",&x);s[i]=s[i-1]+x;w[i]=w[i-1]+(long long)i*x;}\n    while(q--){scanf(\"%d%d\",&l,&r);long long v=s[r]-s[l-1];printf(\"%lld %lld\\n\",v,w[r]-w[l-1]-(l-1)*v);}") ,
 L(2,"环形回廊 · 最大连续和","输入 n（1≤n≤80）和 n 个整数（-100≤a[i]≤100）。数组首尾相接；选择长度 1～n 的连续段，输出最大和。不能选择空段。",
 "新用法：+= 是累加赋值，?: 是条件运算符，% 求余可把循环下标映射回数组。&& / || 是短路逻辑，单独的 | / & 是位运算。\n环形段最多取 n 项，不能重复元素；全负数仍须选择一个元素。先写 O(n²) 枚举验证，再理解最大子段和及补集的关系。",
 "    int n,a[80],best=INT_MIN;scanf(\"%d\",&n);for(int i=0;i<n;i++)scanf(\"%d\",a+i);\n    for(int i=0;i<n;i++){int s=0;for(int k=0;k<n;k++){s+=a[(i+k)%n];if(s>best)best=s;}}printf(\"%d\\n\",best);") ,
 L(3,"无重复符文","输入一个仅含小写英文字母的字符串，长度 1～120。输出不含重复字符的最长连续子串长度。",
 "新语法：char 是字符类型，字符串以 '\\0' 结束；char s[121] 至少多留一个位置。scanf 的 %120s 限制长度，字符串数组作为地址传入。s[i]-'a' 将字母映射到 0～25；strlen 来自 string.h，返回 size_t。\n双指针维护一个没有重复字符的窗口，记录每个字符最近位置，左边界只能前进。O(n)。",
 "    char s[121];int last[26],l=0,best=0;scanf(\"%120s\",s);for(int i=0;i<26;i++)last[i]=-1;\n    for(int i=0;s[i];i++){int c=s[i]-'a';if(last[c]>=l)l=last[c]+1;last[c]=i;if(i-l+1>best)best=i-l+1;}printf(\"%d\\n\",best);") ,
 L(4,"括号祭坛","输入长度 1～120、仅含 ()[]{} 的字符串。正确嵌套输出 YES 和最大嵌套深度。错误输出 NO 和首次错误位置（从1开始）；遇到错误右括号立即停止扫描，若仅有未闭合左括号，错误位置记为长度+1。",
 "新语法：switch 按整型值分支，case 后用 break 结束分支；continue 跳到下一次循环。== 比较而 = 赋值。字符字面量用单引号。\n栈后进先出，右括号必须匹配最近尚未配对的左括号，空栈时不能弹出。结束时栈必须为空。O(n)。",
 "    char s[121],st[121];int t=0,best=0,bad=0,i;scanf(\"%120s\",s);for(i=0;s[i];i++){char c=s[i];if(c=='('||c=='['||c=='{'){st[t++]=c;if(t>best)best=t;}else{if(!t){bad=i+1;break;}char x=st[--t];if(!((x=='('&&c==')')||(x=='['&&c==']')||(x=='{'&&c=='}'))){bad=i+1;break;}}}if(!bad&&t)bad=(int)strlen(s)+1;if(bad)printf(\"NO %d\\n\",bad);else printf(\"YES %d\\n\",best);") ,
 L(5,"封印合并","输入 n（1≤n≤30），随后 n 组整数 l r（-30≤l≤r≤30）。将闭区间排序，合并相交或端点相等的区间；第一行输出数量，随后每行输出一个合并区间，按左端点递增。相邻但不相交的不合并。",
 "新语法：struct 把多个字段组成一个记录，a[i].l 访问字段；typedef 给类型别名。qsort 来自 stdlib.h，比较函数通过 const void* 接收元素地址，再转换为记录指针；-> 访问指针所指记录的字段。函数原型、参数及返回值要一致，比较函数返回负/零/正。\n按左端点排序后扫描，当前区间只可能与最后输出的区间相交。O(n log n)。",
 "    int n,m=0;P a[30],b[30];scanf(\"%d\",&n);for(int i=0;i<n;i++)scanf(\"%d%d\",&a[i].l,&a[i].r);qsort(a,n,sizeof(P),cmp);for(int i=0;i<n;i++){if(m&&a[i].l<=b[m-1].r){if(a[i].r>b[m-1].r)b[m-1].r=a[i].r;}else b[m++]=a[i];}printf(\"%d\\n\",m);for(int i=0;i<m;i++)printf(\"%d %d\\n\",b[i].l,b[i].r);",
 "typedef struct {int l,r;} P;\nint cmp(const void *x,const void *y){const P *a=x,*b=y;return a->l==b->l?a->r-b->r:a->l-b->l;}") ,
 L(6,"下一座灯塔","输入 n（1≤n≤100）和 n 个整数。对每个位置输出它右侧第一个严格更大元素的 1 基下标；不存在输出 0。输出一行 n 项。元素绝对值≤1000。",
 "新用法：a+i 与 &a[i] 在数组范围内表示同一元素地址；后缀 i++ 先取旧值再加一，前缀 ++i 先加再取值。避免一个表达式中多次改动同一个变量。\n单调栈保存尚未找到答案的下标，每个元素进出一次。注意严格更大，不接受相等值。O(n)。",
 "    int n,a[100],st[100],ans[100]={0},t=0;scanf(\"%d\",&n);for(int i=0;i<n;i++)scanf(\"%d\",&a[i]);for(int i=0;i<n;i++){while(t&&a[st[t-1]]<a[i])ans[st[--t]]=i+1;st[t++]=i;}for(int i=0;i<n;i++)printf(\"%d%c\",ans[i],i==n-1?'\\n':' ');") ,
 L(7,"移动守望窗","输入 n k（1≤k≤n≤100）和 n 个整数（绝对值≤1000）。输出每个连续 k 项窗口的最大值，共 n-k+1 项。",
 "新用法：队列用数组和头尾下标实现，区间通常采用 [head,tail)；与栈相比，它还会移除离开窗口的旧元素。\n单调队列存下标，先删除过期下标，再从队尾删除不可能成为最大值的元素。每个元素最多进出一次，O(n)。",
 "    int n,k,a[100],d[100],h=0,t=0;scanf(\"%d%d\",&n,&k);for(int i=0;i<n;i++)scanf(\"%d\",&a[i]);for(int i=0;i<n;i++){while(h<t&&d[h]<=i-k)h++;while(h<t&&a[d[t-1]]<=a[i])t--;d[t++]=i;if(i>=k-1)printf(\"%d%c\",a[d[h]],i==n-1?'\\n':' ');}") ,
 L(8,"搬运极限","输入 n d（1≤d≤n≤60）及 n 个包裹重量（1～100）。按原顺序分成最多 d 个非空连续组，输出能实现的最小组重量上限。",
 "新语法：函数可返回判断结果（用 int 的 0/1）；将可行性测试拆成函数有助于避免重复逻辑。\n答案二分要求可行性单调。下界是最重包裹，上界是总和；给定上限用贪心计算最少组数。O(n log 总和)。",
 "    int n,d,a[60],lo=0,hi=0;scanf(\"%d%d\",&n,&d);for(int i=0;i<n;i++){scanf(\"%d\",&a[i]);hi+=a[i];if(a[i]>lo)lo=a[i];}while(lo<hi){int mid=lo+(hi-lo)/2,g=1,s=0;for(int i=0;i<n;i++){if(s+a[i]>mid){g++;s=0;}s+=a[i];}if(g<=d)hi=mid;else lo=mid+1;}printf(\"%d\\n\",lo);") ,
 L(9,"质数守卫","输入 l r（0≤l≤r≤10000）。输出 [l,r] 内质数的数量及它们的和，两个整数。",
 "新语法：static 局部或全局数组默认零初始化并拥有静态存储期；自动局部数组若没有初始化，其内容不可依赖。\n筛法标记合数，0 和 1 必须单独排除。从 i*i 开始标记时要防止乘法溢出。O(r log log r)。",
 "    int l,r,c=0;long long s=0;static int bad[10001];scanf(\"%d%d\",&l,&r);bad[0]=bad[1]=1;for(int i=2;i*i<=r;i++)if(!bad[i])for(int j=i*i;j<=r;j+=i)bad[j]=1;for(int i=l;i<=r;i++)if(!bad[i]){c++;s+=i;}printf(\"%d %lld\\n\",c,s);") ,
 L(10,"巨数门锁 · 双运算","输入两个无前导零的非负十进制整数，每个最多80位，分别占一行。第一行输出精确的和，第二行输出差的绝对值，两行均不得有前导零。",
 "新用法：整型有固定范围，不能用 long long 保存 80 位数；逐字符转数字与进位可突破位数限制。\n从个位开始相加，维护 carry，最后反向输出。边界是 0、长短不同、全 9 连续进位。O(位数)。",
 "    char a[81],b[81];scanf(\"%80s%80s\",a,b);calc(a,b,0);int cmp=(int)strlen(a)-(int)strlen(b);if(!cmp)cmp=strcmp(a,b);if(cmp>=0)calc(a,b,1);else calc(b,a,1);",
 "void calc(char *a,char *b,int sub){char out[82];int i=(int)strlen(a)-1,j=(int)strlen(b)-1,t=0,c=0;while(i>=0||j>=0||(!sub&&c)){int x=(i>=0?a[i--]-'0':0),y=(j>=0?b[j--]-'0':0);int v=sub?x-y-c:x+y+c;if(sub){c=v<0;if(c)v+=10;}else c=v/10;out[t++]=(char)('0'+v%10);}while(t>1&&out[t-1]=='0')t--;while(t)putchar(out[--t]);putchar('\\n');}") ,
 L(11,"巨数门锁 · 乘法","输入两个无前导零的非负十进制整数，每个最多 40 位。输出精确乘积，不得有前导零。",
 "新用法：二维嵌套循环完成竖式乘法，低位数组可同时累积多组乘积；统一处理进位。\n最多需要两数位数之和的位置，最高位处理完再去除前导零，乘积 0 保留一位。O(nm)。",
 "    char a[41],b[41];int v[82]={0};scanf(\"%40s%40s\",a,b);int n=(int)strlen(a),m=(int)strlen(b);for(int i=0;i<n;i++)for(int j=0;j<m;j++)v[i+j]+=(a[n-1-i]-'0')*(b[m-1-j]-'0');for(int i=0;i<n+m;i++){v[i+1]+=v[i]/10;v[i]%=10;}int t=n+m;while(t>0&&!v[t])t--;for(;t>=0;t--)putchar('0'+v[t]);putchar('\\n');") ,
 L(12,"异进制密码","输入 b 和无前导零数字串 s，2≤b≤16，s 长度 1～12，由 0～9、A～F 中合法 b 进制数字组成。输出其十进制值（≤16¹²-1）。",
 "新语法：unsigned long long 是非负长整数，输出使用 %llu；显式类型转换 (int) 或 (char) 应确认数值范围。\nHorner 法逐位乘基数再加下一位。输入上界决定整数类型，16¹² 超过 32 位。O(长度)。",
 "    int b;char s[13];unsigned long long x=0;scanf(\"%d%12s\",&b,s);for(int i=0;s[i];i++){int d=s[i]<='9'?s[i]-'0':s[i]-'A'+10;x=x*b+d;}printf(\"%llu\\n\",x);") ,
 L(13,"重叠回声 · 定位","输入两个小写字符串 text pattern，长度分别1～200、1～40。第一行输出出现次数，允许重叠。第二行按递增顺序输出所有匹配起点（从1开始）；无匹配时第二行输出0。",
 "新语法：memset(ptr,value,bytes) 按字节初始化内存，不能用它给 int 数组设置任意整数值。\nKMP 的前缀函数记录前后缀相等的最长长度；匹配失败复用前缀信息。匹配完成后也要回退而非清零，才会统计重叠。O(n+m)。",
 "    char s[201],p[41];int pi[40]={0},at[200],c=0;scanf(\"%200s%40s\",s,p);int m=(int)strlen(p);for(int i=1,j=0;i<m;i++){while(j&&p[i]!=p[j])j=pi[j-1];if(p[i]==p[j])j++;pi[i]=j;}for(int i=0,j=0;s[i];i++){while(j&&s[i]!=p[j])j=pi[j-1];if(s[i]==p[j])j++;if(j==m){at[c++]=i-m+2;j=pi[j-1];}}printf(\"%d\\n\",c);if(!c)puts(\"0\");else for(int i=0;i<c;i++)printf(\"%d%c\",at[i],i==c-1?'\\n':' ');") ,
 L(14,"回文镜室","输入长度 1～120 的小写字符串，输出最长连续回文子串的长度。",
 "新用法：奇数回文与偶数回文的中心不同。while 在读取数组之前检查上下界，利用 && 的短路避免越界。\n中心扩展是 O(n²) 的可验证起点，记录的是连续子串长度，不是非连续子序列。",
 "    char s[121];scanf(\"%120s\",s);int n=(int)strlen(s),best=1;for(int c=0;c<n;c++)for(int even=0;even<2;even++){int l=c,r=c+even;while(l>=0&&r<n&&s[l]==s[r]){if(r-l+1>best)best=r-l+1;l--;r++;}}printf(\"%d\\n\",best);") ,
 L(15,"压缩铭文","输入编码串，形如 a3b12c1：小写字母后接 1～20 的十进制次数，每个段必有次数，最多 12 段。输出解码后的字符串。",
 "新用法：解析可变长数字需要持续读取到非数字字符；不要假设次数只有一位。putchar 输出单个字符。\n定义明确的扫描指针，把“取字母”和“取完整数字”分成阶段。最长解码长度 240。O(输入+输出)。",
 "    char s[61];scanf(\"%60s\",s);int i=0;while(s[i]){char c=s[i++];int n=0;while(s[i]>='0'&&s[i]<='9')n=n*10+s[i++]-'0';while(n--)putchar(c);}putchar('\\n');") ,
 L(16,"多项式机关","输入次数 n 和 x（0≤n≤8，-3≤x≤3），随后按最高次到常数项输入 n+1 个系数（-20～20）。输出多项式在 x 处的整数值。",
 "新用法：乘加递推比反复求幂更简单，避免 math.h 的浮点 pow 带来精度和额外转换。\nHorner 法将每一项串成累积运算；次数 0 也要读取常数项。O(n)。",
 "    int n,x,a;long long v=0;scanf(\"%d%d\",&n,&x);for(int i=0;i<=n;i++){scanf(\"%d\",&a);v=v*x+a;}printf(\"%lld\\n\",v);") ,
 L(17,"稀疏阵列转置","输入行数 r、列数 c、非零项数 k（1≤r,c≤8，0≤k≤r*c），再输入 k 组 1 基行、列、值。位置互不重复，值非零且绝对值≤99。输出转置后非零项数，再按新行、列递增输出每组行 列 值。",
 "新语法：二维数组 int a[R][C] 连续按行存储，a[i][j] 有两个合法下标；sizeof 得到对象占用字节数。\n转置交换行列，稀疏表示只输出非零项。固定二维存储适合小范围，注意零项输入。",
 "    int r,c,k,x,y,v,a[8][8]={0};scanf(\"%d%d%d\",&r,&c,&k);for(int i=0;i<k;i++){scanf(\"%d%d%d\",&x,&y,&v);a[x-1][y-1]=v;}printf(\"%d\\n\",k);for(int j=0;j<c;j++)for(int i=0;i<r;i++)if(a[i][j])printf(\"%d %d %d\\n\",j+1,i+1,a[i][j]);") ,
 L(18,"矩阵共鸣 · 三重乘积","输入 r k t c（各1～6），接着 r*k 矩阵A、k*t矩阵B、t*c矩阵C，元素-10～10。输出 A*B*C 的 r*c 矩阵，每行 c 项。矩阵按行输入。",
 "新用法：三层循环的每个下标具有不同职责，乘积的内部维度必须相等。每个结果单元独立清零后累加。\n矩阵乘法是线性组合，不能逐位置直接相乘。O(rkc)。",
 "    int r,k,t,c,a[6][6],b[6][6],d[6][6],ab[6][6]={0};scanf(\"%d%d%d%d\",&r,&k,&t,&c);for(int i=0;i<r;i++)for(int j=0;j<k;j++)scanf(\"%d\",&a[i][j]);for(int i=0;i<k;i++)for(int j=0;j<t;j++)scanf(\"%d\",&b[i][j]);for(int i=0;i<t;i++)for(int j=0;j<c;j++)scanf(\"%d\",&d[i][j]);for(int i=0;i<r;i++)for(int j=0;j<t;j++)for(int h=0;h<k;h++)ab[i][j]+=a[i][h]*b[h][j];for(int i=0;i<r;i++)for(int j=0;j<c;j++){int v=0;for(int h=0;h<t;h++)v+=ab[i][h]*d[h][j];printf(\"%d%c\",v,j==c-1?'\\n':' ');}") ,
 L(19,"螺旋迷宫","输入 r c（1～8）及 r*c 个矩阵整数（绝对值≤99）。从左上角开始，按右、下、左、上的顺时针螺旋顺序输出全部元素。",
 "新用法：四条边界控制尚未访问的矩形；收缩边界后需要再次检查，单行或单列不能重复访问。\n循环不变量是剩余元素始终位于当前矩形内。O(rc)。",
 "    int r,c,a[8][8],t=0,l=0,first=1;scanf(\"%d%d\",&r,&c);for(int i=0;i<r;i++)for(int j=0;j<c;j++)scanf(\"%d\",&a[i][j]);int b=r-1,h=c-1;while(t<=b&&l<=h){for(int j=l;j<=h;j++)out(a[t][j],&first);t++;for(int i=t;i<=b;i++)out(a[i][h],&first);h--;if(t<=b){for(int j=h;j>=l;j--)out(a[b][j],&first);b--;}if(l<=h){for(int i=b;i>=t;i--)out(a[i][l],&first);l++;}}putchar('\\n');",
 "void out(int v,int *first){if(!*first)putchar(' ');printf(\"%d\",v);*first=0;}") ,
 L(20,"快速幂封印","输入 a b m（0≤a≤100000，0≤b≤10⁹，1≤m≤100000）。输出 a^b 模 m；定义任何 a 的 0 次幂为 1。",
 "新语法：b&1 检查最低位，b>>=1 右移赋值；位运算只用于整型，注意符号和溢出。\n二进制快速幂把指数拆为二的幂；乘法用 long long。m=1 时任何结果都为 0。O(log b)。",
 "    long long a,b,m,v;scanf(\"%lld%lld%lld\",&a,&b,&m);v=1%m;a%=m;while(b){if(b&1)v=v*a%m;a=a*a%m;b>>=1;}printf(\"%lld\\n\",v);") ,
 L(21,"逆序之门","输入 n（1≤n≤150）和 n 个整数（-1000～1000）。输出逆序对数量：i<j 且 a[i]>a[j]。",
 "新语法：递归函数必须有终止条件，参数用半开区间能统一空区间；函数调用栈保存每层局部变量。\n归并时，右侧值小于左侧值会一次形成多个逆序对，相等不算。O(n log n)。",
 "    int n;scanf(\"%d\",&n);for(int i=0;i<n;i++)scanf(\"%d\",&a[i]);printf(\"%lld\\n\",sort(0,n));",
 "int a[150],tmp[150];\nlong long sort(int l,int r){if(r-l<2)return 0;int m=(l+r)/2,i=l,j=m,t=l;long long s=sort(l,m)+sort(m,r);while(i<m||j<r){if(j==r||(i<m&&a[i]<=a[j]))tmp[t++]=a[i++];else{s+=m-i;tmp[t++]=a[j++];}}for(i=l;i<r;i++)a[i]=tmp[i];return s;}") ,
 L(22,"第 k 枚魂石","输入 n k（1≤k≤n≤150）和 n 个整数（绝对值≤1000）。输出第 k 小的值，重复值按重复次数计数。",
 "新用法：分区算法维护左右范围与枢轴，比完整排序更贴近第 k 小问题；但本关允许任何正确算法。qsort 比较器必须保持传递性。\n区分“第 k 小元素”和“第 k 个不同值”；可先排序作验证，再实现快速选择。",
 "    int n,k,a[150];scanf(\"%d%d\",&n,&k);for(int i=0;i<n;i++)scanf(\"%d\",&a[i]);qsort(a,n,sizeof(int),cmp);printf(\"%d\\n\",a[k-1]);",
 "int cmp(const void *a,const void *b){int x=*(const int*)a,y=*(const int*)b;return (x>y)-(x<y);}") ,
 L(23,"联盟印记","输入 n q（1≤n≤30，1≤q≤60），后有 q 行 op a b，a,b 为 1～n。op=U 合并所属集合；op=Q 查询是否同集合，逐行输出 YES 或 NO。",
 "新用法：递归路径压缩会改写父节点数组，函数参数值传递，但全局数组的修改会保留。读取命令字符用 scanf(\" %c\",...) 的前导空格跳过空白。\n并查集支持合并与连通查询，根节点满足 parent[x]==x。路径压缩加按秩合并效率更高。",
 "    int n,q,a,b;char op;scanf(\"%d%d\",&n,&q);for(int i=1;i<=n;i++)p[i]=i;while(q--){scanf(\" %c%d%d\",&op,&a,&b);a=find(a);b=find(b);if(op=='U')p[a]=b;else puts(a==b?\"YES\":\"NO\");}",
 "int p[31];\nint find(int x){return p[x]==x?x:(p[x]=find(p[x]));}") ,
 L(24,"钥匙迷雾","输入 r c（2～10），随后 r 行地图：.可走，#墙，K钥匙，D锁门。最多一把钥匙和一扇门，可无钥匙或无门；经过K永久获得钥匙，持钥匙才能进D。起终点为.。从左上到右下仅上下左右，输出最少步数，不可达 -1。",
 "新用法：二维坐标映射到一维队列索引，访问标记必须在入队时设置。\nBFS 按距离分层，只适合等权边。四个方向先检查界限再访问格子，避免重复入队。O(rc)。",
 "    int r,c,d[10][10][2],qx[200],qy[200],qs[200],h=0,t=1,dx[]={1,-1,0,0},dy[]={0,0,1,-1};char a[10][11];scanf(\"%d%d\",&r,&c);for(int i=0;i<r;i++){scanf(\"%10s\",a[i]);for(int j=0;j<c;j++)d[i][j][0]=d[i][j][1]=-1;}qx[0]=qy[0]=qs[0]=0;d[0][0][0]=0;while(h<t){int x=qx[h],y=qy[h],s=qs[h++];for(int k=0;k<4;k++){int u=x+dx[k],v=y+dy[k];if(u<0||u>=r||v<0||v>=c||a[u][v]=='#'||(a[u][v]=='D'&&!s))continue;int z=s||a[u][v]=='K';if(d[u][v][z]<0){d[u][v][z]=d[x][y][s]+1;qx[t]=u;qy[t]=v;qs[t++]=z;}}}int x=d[r-1][c-1][0],y=d[r-1][c-1][1];printf(\"%d\\n\",x<0?y:y<0?x:x<y?x:y);") ,
 L(25,"机关依赖","输入 n m（1≤n≤15，0≤m≤n(n-1)），再输入 m 条不同的有向边 u v（1 基，无自环）。输出字典序最小的拓扑序；若有环输出 CYCLE。",
 "新用法：enum 可给有限状态命名，const 表示不经该变量修改；这些声明能替代不清楚的魔法数字。\n入度为零的节点可执行。每次取编号最小的可执行节点，剩余节点却无零入度意味着有环。O(n²+m)。",
 "    int n,m,u,v,a[16][16]={0},deg[16]={0},used[16]={0},out[15],t=0;scanf(\"%d%d\",&n,&m);while(m--){scanf(\"%d%d\",&u,&v);a[u][v]=1;deg[v]++;}for(int k=0;k<n;k++){u=0;for(int i=1;i<=n;i++)if(!used[i]&&!deg[i]){u=i;break;}if(!u)break;used[u]=1;out[t++]=u;for(v=1;v<=n;v++)if(a[u][v])deg[v]--;}if(t<n)puts(\"CYCLE\");else for(int i=0;i<n;i++)printf(\"%d%c\",out[i],i==n-1?'\\n':' ');") ,
 L(26,"最短暗道","输入 n m s（2≤n≤15，s 为 1～n），再输入 m 条不同有向边 u v w（无自环，1≤w≤30）。输出从 s 到每个点的最短距离，不可达输出 -1。",
 "新用法：INF 是哨兵值，选择它时为相加留出空间，不能直接对 INT_MAX 加边权。\nDijkstra 反复确定尚未确定的最小距离点；要求非负权。小图可用 O(n²) 数组实现，避免过早引入堆。",
 "    int n,m,s,u,v,w,g[16][16],d[16],used[16]={0};scanf(\"%d%d%d\",&n,&m,&s);for(int i=1;i<=n;i++){d[i]=1000000;for(int j=1;j<=n;j++)g[i][j]=1000000;}while(m--){scanf(\"%d%d%d\",&u,&v,&w);g[u][v]=w;}d[s]=0;for(int k=0;k<n;k++){u=0;for(int i=1;i<=n;i++)if(!used[i]&&(!u||d[i]<d[u]))u=i;if(d[u]==1000000)break;used[u]=1;for(v=1;v<=n;v++)if(d[v]>d[u]+g[u][v])d[v]=d[u]+g[u][v];}for(int i=1;i<=n;i++)printf(\"%d%c\",d[i]==1000000?-1:d[i],i==n?'\\n':' ');") ,
 L(27,"负重诅咒","输入 n m s（2≤n≤12），m 条不同有向边 u v w（无自环，-10≤w≤20）。存在从 s 可达的负权环时输出 NEGATIVE；否则输出从 s 到各点最短距离，不可达 -1。",
 "新用法：结构体数组可以表示边集，逐边遍历比矩阵更适合松弛。\nBellman-Ford 经过 n-1 轮后，再能松弛即存在源点可达的负环。只能从有限距离点松弛，否则会把不可达负环误判。O(nm)。",
 "    int n,m,s,d[13],u[150],v[150],w[150];scanf(\"%d%d%d\",&n,&m,&s);for(int i=0;i<m;i++)scanf(\"%d%d%d\",&u[i],&v[i],&w[i]);for(int i=1;i<=n;i++)d[i]=1000000;d[s]=0;int neg=0;for(int k=0;k<n;k++){int changed=0;for(int i=0;i<m;i++)if(d[u[i]]<1000000&&d[v[i]]>d[u[i]]+w[i]){d[v[i]]=d[u[i]]+w[i];changed=1;}if(!changed)break;if(k==n-1)neg=1;}if(neg)puts(\"NEGATIVE\");else for(int i=1;i<=n;i++)printf(\"%d%c\",d[i]==1000000?-1:d[i],i==n?'\\n':' ');") ,
 L(28,"连接遗迹","输入 n m（2≤n≤12），m 条不同无向边 u v w（无自环，1≤w≤40）。输出连接全部节点的最小生成树总权重，无法连接输出 DISCONNECTED。",
 "新用法：无向图必须存两个方向，生成树的边数是 n-1。\nPrim 从已连接点集向外选择最小边，与最短路不同，它更新的是接入成本而非路径距离。O(n²)。",
 "    int n,m,u,v,w,g[13][13],d[13],used[13]={0},sum=0,ok=1;scanf(\"%d%d\",&n,&m);for(int i=1;i<=n;i++){d[i]=1000000;for(int j=1;j<=n;j++)g[i][j]=1000000;}while(m--){scanf(\"%d%d%d\",&u,&v,&w);g[u][v]=g[v][u]=w;}d[1]=0;for(int k=0;k<n;k++){u=0;for(int i=1;i<=n;i++)if(!used[i]&&(!u||d[i]<d[u]))u=i;if(d[u]==1000000){ok=0;break;}used[u]=1;sum+=d[u];for(v=1;v<=n;v++)if(!used[v]&&d[v]>g[u][v])d[v]=g[u][v];}if(ok)printf(\"%d\\n\",sum);else puts(\"DISCONNECTED\");") ,
 L(29,"全域暗道","输入 n m q（2≤n≤10，1≤q≤20），m 条不同有向边 u v w（无自环，1～30），再输入 q 对 s t。逐行输出最短距离，不可达 -1。",
 "新用法：二维动态规划中，循环顺序表达了状态依赖，不能任意交换。\nFloyd 以 k 为允许新增的中间点，k 必须在最外层。初始化对角线 0，其他点 INF。O(n³+q)。",
 "    int n,m,q,u,v,w,d[11][11];scanf(\"%d%d%d\",&n,&m,&q);for(int i=1;i<=n;i++)for(int j=1;j<=n;j++)d[i][j]=i==j?0:1000000;while(m--){scanf(\"%d%d%d\",&u,&v,&w);d[u][v]=w;}for(int k=1;k<=n;k++)for(int i=1;i<=n;i++)for(int j=1;j<=n;j++)if(d[i][j]>d[i][k]+d[k][j])d[i][j]=d[i][k]+d[k][j];while(q--){scanf(\"%d%d\",&u,&v);printf(\"%d\\n\",d[u][v]==1000000?-1:d[u][v]);}") ,
 L(30,"词缀档案","输入 n q（1～30），随后 n 个小写单词（长1～20，可重复），再输入 q 个小写前缀（长1～10）。逐行输出以该前缀开头的单词数量，重复单词重复计数。",
 "新语法：malloc 分配指定字节，失败返回 NULL；calloc 会清零；free 释放后不能继续使用。固定上界已知时也可用静态数组。\nTrie 的边代表字符，节点计数记录经过它的单词数；不是只统计终止节点。",
 "    int n,q;char s[21];static int ch[601][26],cnt[601];int nodes=0;scanf(\"%d%d\",&n,&q);for(int i=0;i<n;i++){scanf(\"%20s\",s);int u=0;for(int j=0;s[j];j++){int c=s[j]-'a';if(!ch[u][c])ch[u][c]=++nodes;u=ch[u][c];cnt[u]++;}}while(q--){scanf(\"%20s\",s);int u=0,ok=1;for(int j=0;s[j];j++){u=ch[u][s[j]-'a'];if(!u){ok=0;break;}}printf(\"%d\\n\",ok?cnt[u]:0);}") ,
 L(31,"硬币咒式","输入 n target（1≤n≤6，0≤target≤60）和 n 个不同正整数面值（1～20）。每种无限枚，输出凑出 target 的组合数，不区分顺序；target=0 时答案为1。",
 "新用法：long long 适合计数，但必须核对组合数上界。\n完全背包计数组合时面值在外层，金额递增；反过来会计入排列。dp[0]=1 表示空组合。O(n*target)。",
 "    int n,t,a[6];long long dp[61]={1};scanf(\"%d%d\",&n,&t);for(int i=0;i<n;i++)scanf(\"%d\",&a[i]);for(int i=0;i<n;i++)for(int j=a[i];j<=t;j++)dp[j]+=dp[j-a[i]];printf(\"%lld\\n\",dp[t]);") ,
 L(32,"限量符币","输入 n target（1≤n≤6，0≤target≤100），随后n行面值和可用数量：面值各不相同（1～20），数量0～10。输出凑出target的最少枚数，库存有限，不可凑出为 -1。",
 "新用法：计数状态与最优化状态的初始值不同，最小化问题使用 INF 而不是零。\n状态表示“精确凑出此金额的最少枚数”，只有可达状态才能转移。O(n*target)。",
 "    int n,t,a,c,dp[101];scanf(\"%d%d\",&n,&t);for(int i=0;i<=t;i++)dp[i]=10000;dp[0]=0;while(n--){scanf(\"%d%d\",&a,&c);while(c--)for(int j=t;j>=a;j--)if(dp[j]>dp[j-a]+1)dp[j]=dp[j-a]+1;}printf(\"%d\\n\",dp[t]==10000?-1:dp[t]);") ,
 L(33,"升序魂链 · 数量","输入n（1～120）和n个整数（绝对值≤1000）。输出最长严格递增子序列的长度和数量。不同下标选择视为不同序列，数量模1000000007；子序列不要求连续。",
 "新用法：子序列可以跳过元素，子串或子段必须连续。\ndp[i] 是以 i 结尾的最优长度，而非前 i 项的整体答案。只从更小的前项转移。O(n²)，也可用二分优化。",
 "    int n,a[120],dp[120],best=0;long long cnt[120],ways=0;scanf(\"%d\",&n);for(int i=0;i<n;i++)scanf(\"%d\",&a[i]);for(int i=0;i<n;i++){dp[i]=1;cnt[i]=1;for(int j=0;j<i;j++)if(a[j]<a[i]){if(dp[i]<dp[j]+1){dp[i]=dp[j]+1;cnt[i]=cnt[j];}else if(dp[i]==dp[j]+1)cnt[i]=(cnt[i]+cnt[j])%1000000007;}if(dp[i]>best){best=dp[i];ways=cnt[i];}else if(dp[i]==best)ways=(ways+cnt[i])%1000000007;}printf(\"%d %lld\\n\",best,ways);") ,
 L(34,"共鸣子序列","输入两个小写字符串，每个长度 1～70。输出最长公共子序列长度。",
 "新用法：二维状态 dp[i][j] 常用额外第0行、0列表示空前缀。\n末尾字符相同可同时取用，否则分别舍弃一边并取较优者。不能将公共子串算法用于本题。O(nm)。",
 "    char a[71],b[71];static int dp[71][71];scanf(\"%70s%70s\",a,b);int n=(int)strlen(a),m=(int)strlen(b);for(int i=1;i<=n;i++)for(int j=1;j<=m;j++)dp[i][j]=a[i-1]==b[j-1]?dp[i-1][j-1]+1:(dp[i-1][j]>dp[i][j-1]?dp[i-1][j]:dp[i][j-1]);printf(\"%d\\n\",dp[n][m]);") ,
 L(35,"铭文修复","输入两个小写字符串，各长1～60。允许插入、删除或替换一个字符，每次成本1。输出把第一个变成第二个的最小成本。",
 "新用法：状态转移对应具体操作，字符相同的替换成本为 0。\n编辑距离应初始化到空串的成本，然后比较插入、删除、替换三条路径。O(nm)。",
 "    char a[61],b[61];int dp[61][61];scanf(\"%60s%60s\",a,b);int n=(int)strlen(a),m=(int)strlen(b);for(int i=0;i<=n;i++)dp[i][0]=i;for(int j=0;j<=m;j++)dp[0][j]=j;for(int i=1;i<=n;i++)for(int j=1;j<=m;j++){int x=dp[i-1][j]+1,y=dp[i][j-1]+1,z=dp[i-1][j-1]+(a[i-1]!=b[j-1]);dp[i][j]=x<y?x:y;if(z<dp[i][j])dp[i][j]=z;}printf(\"%d\\n\",dp[n][m]);") ,
 L(36,"探险背包","输入 n capacity（1≤n≤20，0≤capacity≤80），后有 n 行 weight value（1≤weight≤20，0≤value≤100）。每件至多选一次，输出不超容量的最大总价值。",
 "新用法：一维压缩 DP 必须保留上一轮状态。\n0/1 背包容量倒序更新，正序会允许一件物品重复使用。容量为0时答案0。O(n*capacity)。",
 "    int n,c,w,v,dp[81]={0};scanf(\"%d%d\",&n,&c);while(n--){scanf(\"%d%d\",&w,&v);for(int j=c;j>=w;j--)if(dp[j]<dp[j-w]+v)dp[j]=dp[j-w]+v;}printf(\"%d\\n\",dp[c]);") ,
 L(37,"平衡双塔","输入 n（1～20）和 n 个正整数（1～20），将所有数分成两组（允许空组），输出两组和之差绝对值的最小值。",
 "新用法：char 数组也可存布尔0/1状态，避免浪费 int 空间；无需使用 C++ 的 bool。\n总和固定，找到不超过总和一半的最大可达子集和，容量倒序以保证每个元素只用一次。O(n*总和)。",
 "    int n,a[20],s=0;char dp[401]={1};scanf(\"%d\",&n);for(int i=0;i<n;i++){scanf(\"%d\",&a[i]);s+=a[i];}for(int i=0;i<n;i++)for(int j=s;j>=a[i];j--)if(dp[j-a[i]])dp[j]=1;int x=s/2;while(!dp[x])x--;printf(\"%d\\n\",s-2*x);") ,
 L(38,"矩阵连锁","输入矩阵个数 n（1～8）和 n+1 个维度 p[0..n]（1～30），第 i 个矩阵为 p[i-1]*p[i]。只改变括号，输出最少标量乘法次数。",
 "新用法：区间 DP 按区间长度递增，单个矩阵成本0；分割点 k 划分成两个已求解的较短区间。\n合并成本由区间的外部维度及分割维度决定，不能使用局部最小贪心。O(n³)。",
 "    int n,p[9],d[9][9]={0};scanf(\"%d\",&n);for(int i=0;i<=n;i++)scanf(\"%d\",&p[i]);for(int len=2;len<=n;len++)for(int i=1;i+len-1<=n;i++){int j=i+len-1;d[i][j]=INT_MAX;for(int k=i;k<j;k++){int v=d[i][k]+d[k+1][j]+p[i-1]*p[k]*p[j];if(v<d[i][j])d[i][j]=v;}}printf(\"%d\\n\",d[1][n]);") ,
 L(39,"深渊最长路","输入 n m（2～15），m 条不同有向边 u v w，保证 u<v，w 为1～20。输出从1到n的最长路径权重，不可达为 -1。",
 "新用法：最长路不能直接套用一般图最短路；本题通过 u<v 保证 DAG，并给定一个拓扑序。\n不可达初始化为负哨兵，不能从它转移。按拓扑顺序做最大化 DP。O(n²+m)。",
 "    int n,m,u,v,w,g[16][16]={0},d[16];scanf(\"%d%d\",&n,&m);while(m--){scanf(\"%d%d%d\",&u,&v,&w);g[u][v]=w;}for(int i=1;i<=n;i++)d[i]=-1;d[1]=0;for(u=1;u<=n;u++)if(d[u]>=0)for(v=u+1;v<=n;v++)if(g[u][v]&&d[v]<d[u]+g[u][v])d[v]=d[u]+g[u][v];printf(\"%d\\n\",d[n]);") ,
 L(40,"动态账本","输入 n q（1≤n≤80，1≤q≤50）及初始 n 个整数（绝对值≤100）。后续 q 行：A i delta 给第i项加delta（-100～100）；Q l r 查询闭区间和。逐行输出查询结果。",
 "新用法：x&-x 提取最低有效位，使用有符号整型时本题下标范围远离溢出。\n树状数组支持单点增量和前缀和，必须从1开始；区间查询由两个前缀相减。O(log n)。",
 "    int n,q,x,l,r;char op;scanf(\"%d%d\",&n,&q);for(int i=1;i<=n;i++){scanf(\"%d\",&x);add(i,x,n);}while(q--){scanf(\" %c%d%d\",&op,&l,&r);if(op=='A')add(l,r,n);else printf(\"%lld\\n\",sum(r)-sum(l-1));}",
 "long long bit[81];\nvoid add(int i,int v,int n){for(;i<=n;i+=i&-i)bit[i]+=v;}\nlong long sum(int i){long long s=0;for(;i>0;i-=i&-i)s+=bit[i];return s;}") ,
 L(41,"区间守望","输入 n q（1≤n≤80，1≤q≤50）和 n 个整数（绝对值≤1000）。操作 S i v 将第i项设为v；M l r 查询区间最大值。逐行输出所有查询结果。",
 "新用法：递归树节点用 p*2 和 p*2+1 表示左右子节点，通常分配 4*n 空间。\n线段树把区间拆分并合并最大值，单点设值与区间最大值均 O(log n)。注意负数与初始化。",
 "    int n,q,l,r;char op;scanf(\"%d%d\",&n,&q);for(int i=1;i<=n;i++)scanf(\"%d\",&a[i]);build(1,1,n);while(q--){scanf(\" %c%d%d\",&op,&l,&r);if(op=='S')set(1,1,n,l,r);else printf(\"%d\\n\",query(1,1,n,l,r));}",
 "int a[81],tr[324];\nint mx(int a,int b){return a>b?a:b;}\nvoid build(int p,int l,int r){if(l==r){tr[p]=a[l];return;}int m=(l+r)/2;build(p*2,l,m);build(p*2+1,m+1,r);tr[p]=mx(tr[p*2],tr[p*2+1]);}\nvoid set(int p,int l,int r,int i,int v){if(l==r){tr[p]=v;return;}int m=(l+r)/2;if(i<=m)set(p*2,l,m,i,v);else set(p*2+1,m+1,r,i,v);tr[p]=mx(tr[p*2],tr[p*2+1]);}\nint query(int p,int l,int r,int x,int y){if(x<=l&&r<=y)return tr[p];int m=(l+r)/2,v=INT_MIN;if(x<=m)v=mx(v,query(p*2,l,m,x,y));if(y>m)v=mx(v,query(p*2+1,m+1,r,x,y));return v;}") ,
 L(42,"范围诅咒","输入 n q（1≤n≤80，1≤q≤50）和 n 个整数（绝对值≤100）。操作 A l r delta 给区间每项加delta（-100～100）；Q l r 查询区间和。逐行输出查询结果。",
 "新用法：差分是前缀的逆操作；两个树状数组能将区间增量转化为区间求和。\n也可用懒标记线段树：更新整个覆盖区间时记录延迟增量，下次访问子区间再下传。标记对区间和的贡献是增量乘长度。",
 "    int n,q,l,r,v;char op;scanf(\"%d%d\",&n,&q);N=n;for(int i=1;i<=n;i++){scanf(\"%d\",&v);range(i,i,v);}while(q--){scanf(\" %c%d%d\",&op,&l,&r);if(op=='A'){scanf(\"%d\",&v);range(l,r,v);}else printf(\"%lld\\n\",sum(r)-sum(l-1));}",
 "long long b[82],c[82];int N;\nvoid add(long long *a,int i,long long v){for(;i<=N;i+=i&-i)a[i]+=v;}\nlong long get(long long *a,int i){long long s=0;for(;i;i-=i&-i)s+=a[i];return s;}\nvoid range(int l,int r,int v){add(b,l,v);add(b,r+1,-v);add(c,l,(long long)v*(l-1));add(c,r+1,-(long long)v*r);}\nlong long sum(int i){return get(b,i)*i-get(c,i);}") ,
 L(43,"多重回声","输入小写 text（长1～200），再输入 n（1～10）和 n 个小写模式串（长1～25，可重复）。对每个模式逐行输出出现次数，允许重叠。",
 "新用法：多模式共享前缀时，可组合 Trie 与失败链接，形成 Aho-Corasick 自动机。\n每个模式的终止状态需独立记录，重复模式不能丢失；可先用多次 KMP 验证。总输出对应输入模式顺序。",
 "    char s[201],p[26];int n;scanf(\"%200s%d\",s,&n);while(n--){scanf(\"%25s\",p);int c=0,m=(int)strlen(p);for(int i=0;s[i];i++)if(strncmp(s+i,p,m)==0)c++;printf(\"%d\\n\",c);}") ,
 L(44,"强连通暗域","输入 n m（1≤n≤14），m 条不同有向边 u v（无自环）。输出强连通分量数量。",
 "新用法：Tarjan 的 lowlink 维护 DFS 子树能到达的最早栈内节点，是否仍在栈中与是否访问过不同。\n也可通过正反图两次 DFS，或本题小图的传递闭包验证。相互可达定义一个等价类。",
 "    int n,m,u,v,g[15][15]={0},used[15]={0},c=0;scanf(\"%d%d\",&n,&m);for(int i=1;i<=n;i++)g[i][i]=1;while(m--){scanf(\"%d%d\",&u,&v);g[u][v]=1;}for(int k=1;k<=n;k++)for(int i=1;i<=n;i++)for(int j=1;j<=n;j++)g[i][j]|=g[i][k]&&g[k][j];for(int i=1;i<=n;i++)if(!used[i]){c++;for(int j=1;j<=n;j++)if(g[i][j]&&g[j][i])used[j]=1;}printf(\"%d\\n\",c);") ,
 L(45,"断桥审判","输入 n m（2≤n≤14），m 条不同无向边 u v（无自环）。输出桥的数量：删去该边会使连通分量数量增加。图可能不连通。",
 "新用法：无向 DFS 必须区分父边与回边，low[child]>dfn[parent] 才说明该边是桥。\n遍历所有未访问节点以覆盖不连通图。也可逐边删除后计连通分量，作为小图验证算法。",
 "    int n,m,u,v,ans=0;scanf(\"%d%d\",&n,&m);for(int i=0;i<m;i++){scanf(\"%d%d\",&u,&v);g[u][v]=g[v][u]=1;}for(int i=1;i<=n;i++)if(!dfn[i])dfs(i,0,n);printf(\"%d\\n\",bridges);",
 "int g[15][15],dfn[15],low[15],timer,bridges;\nvoid dfs(int u,int p,int n){dfn[u]=low[u]=++timer;for(int v=1;v<=n;v++)if(g[u][v]&&v!=p){if(!dfn[v]){dfs(v,u,n);if(low[v]>dfn[u])bridges++;if(low[v]<low[u])low[u]=low[v];}else if(dfn[v]<low[u])low[u]=dfn[v];}}") ,
 L(46,"能量管网","输入 n m（2≤n≤10），m 条不同有向边 u v capacity（无自环，1～30；允许反向边也存在）。输出从1到n的最大流。",
 "新用法：残量网络每次减少正向容量并增加反向容量，反向边允许撤销先前选择。\nEdmonds-Karp 用 BFS 找增广路，瓶颈是路上最小残量。源点和汇点不同，本题矩阵实现可聚合残量。",
 "    int n,m,u,v,w,g[11][11]={0},flow=0;scanf(\"%d%d\",&n,&m);while(m--){scanf(\"%d%d%d\",&u,&v,&w);g[u][v]+=w;}for(;;){int p[11]={0},q[11],h=0,t=1;p[1]=-1;q[0]=1;while(h<t&&!p[n]){u=q[h++];for(v=1;v<=n;v++)if(!p[v]&&g[u][v]>0){p[v]=u;q[t++]=v;}}if(!p[n])break;w=INT_MAX;for(v=n;v!=1;v=p[v])if(g[p[v]][v]<w)w=g[p[v]][v];for(v=n;v!=1;v=p[v]){g[p[v]][v]-=w;g[v][p[v]]+=w;}flow+=w;}printf(\"%d\\n\",flow);") ,
 L(47,"加权迷宫","输入 r c（2～8），随后 r*c 个0～9整数。0为墙，1～9为进入格子的代价，起终点非0。从左上到右下上下左右行走，起点代价不计。输出最小总代价，不可达 -1。",
 "新用法：网格是一种隐式图，通过坐标生成邻接节点而无需建立完整边集。\n代价不同时 BFS 最少步数不再保证最小费用，应使用 Dijkstra。更新费用包含目的格，起点初始化0。",
 "    int r,c,a[64],d[64],used[64]={0},dx[]={1,-1,0,0},dy[]={0,0,1,-1};scanf(\"%d%d\",&r,&c);int n=r*c;for(int i=0;i<n;i++){scanf(\"%d\",&a[i]);d[i]=1000000;}d[0]=0;for(int k=0;k<n;k++){int u=-1;for(int i=0;i<n;i++)if(!used[i]&&(u<0||d[i]<d[u]))u=i;if(u<0||d[u]==1000000)break;used[u]=1;for(int t=0;t<4;t++){int x=u/c+dx[t],y=u%c+dy[t];if(x>=0&&x<r&&y>=0&&y<c&&a[x*c+y]&&d[x*c+y]>d[u]+a[x*c+y])d[x*c+y]=d[u]+a[x*c+y];}}printf(\"%d\\n\",d[n-1]==1000000?-1:d[n-1]);") ,
 L(48,"远征路线 · 综合","输入 n（2～9）及 n*n 个对称距离（对角0，其余1～30）。从城市1出发，恰好访问每城一次后返回1，输出最小总路程。",
 "新语法：1<<i 构造位掩码，(mask>>i)&1 测试是否包含第i项；数组尺寸随 2^n 增长。移位位数必须小于类型宽度。\n状态压缩 DP 记录访问集合与当前位置，最终还须加返程。O(2^n*n²)，不要把不同集合混在同一个状态。",
 "    int n,g[9][9];static int dp[512][9];scanf(\"%d\",&n);for(int i=0;i<n;i++)for(int j=0;j<n;j++)scanf(\"%d\",&g[i][j]);int all=1<<n;for(int s=0;s<all;s++)for(int i=0;i<n;i++)dp[s][i]=1000000;dp[1][0]=0;for(int s=1;s<all;s++)for(int u=0;u<n;u++)if(dp[s][u]<1000000)for(int v=0;v<n;v++)if(!(s&(1<<v))&&dp[s|(1<<v)][v]>dp[s][u]+g[u][v])dp[s|(1<<v)][v]=dp[s][u]+g[u][v];int best=1000000;for(int i=1;i<n;i++)if(best>dp[all-1][i]+g[i][0])best=dp[all-1][i]+g[i][0];printf(\"%d\\n\",best);") ,
 L(49,"算式审判 · 综合","输入不含空格的合法整数算式（长度≤120）：含0～99、二元 + - *、括号以及一元负号；没有除法。按标准优先级输出 long long 整数结果，所有中间结果绝对值≤10¹²。",
 "新语法：递归下降解析把文法分为表达式、乘法项、基本项；函数可互相递归，须预先声明原型。全局扫描指针或传入指针参数都可传递位置。\n一元负号比乘法优先级高，左右结合的二元运算按扫描顺序处理。不能简单删括号后计算。",
 "    scanf(\"%120s\",s);printf(\"%lld\\n\",expr());",
 "char s[121];int pos;\nlong long expr(void);\nlong long factor(void){if(s[pos]=='-'){pos++;return -factor();}if(s[pos]=='('){pos++;long long v=expr();pos++;return v;}long long v=0;while(s[pos]>='0'&&s[pos]<='9')v=v*10+s[pos++]-'0';return v;}\nlong long term(void){long long v=factor();while(s[pos]=='*'){pos++;v*=factor();}return v;}\nlong long expr(void){long long v=term();while(s[pos]=='+'||s[pos]=='-'){char c=s[pos++];long long w=term();v=c=='+'?v+w:v-w;}return v;}") ,
 L(50,"黑塔仓库 · 终局项目","实现带快照的库存终端。输入 q（1～80）和 q 行命令；商品id为1～10。ADD id amount：增加库存（1～30），不输出。SELL id amount：库存足够则减库存并输出 OK，否则不变并输出 FAIL。GET id：输出库存。SNAP：新建完整库存快照，输出从1递增的快照编号。LOAD k：恢复第k个快照，编号有效；输出 OK。TOTAL：输出总库存。TOP：输出库存最多的商品id和库存；并列选id最小。初始库存全0，快照不可随加载改变。",
 "新语法：strcmp(a,b)==0 判断字符串内容相等；不能用 a==b 比较字符串。memcpy(dest,src,sizeof...) 复制对象字节，两块区域不得重叠。\n项目方法：将解析、状态修改、输出分离；失败操作必须保持状态不变。快照是深拷贝，不是指向当前数组的别名。通过不变量和命令序列验证 ADD/SELL/LOAD 相互影响。最终评测包含重复加载、多快照、失败交易和并列榜首。",
 "    int q,a[11]={0},snap[81][11],sn=0,id,v;char op[10];scanf(\"%d\",&q);while(q--){scanf(\"%9s\",op);if(!strcmp(op,\"ADD\")){scanf(\"%d%d\",&id,&v);a[id]+=v;}else if(!strcmp(op,\"SELL\")){scanf(\"%d%d\",&id,&v);if(a[id]>=v){a[id]-=v;puts(\"OK\");}else puts(\"FAIL\");}else if(!strcmp(op,\"GET\")){scanf(\"%d\",&id);printf(\"%d\\n\",a[id]);}else if(!strcmp(op,\"SNAP\")){memcpy(snap[++sn],a,sizeof(a));printf(\"%d\\n\",sn);}else if(!strcmp(op,\"LOAD\")){scanf(\"%d\",&v);memcpy(a,snap[v],sizeof(a));puts(\"OK\");}else if(!strcmp(op,\"TOTAL\")){int s=0;for(int i=1;i<=10;i++)s+=a[i];printf(\"%d\\n\",s);}else{int best=1;for(int i=2;i<=10;i++)if(a[i]>a[best])best=i;printf(\"%d %d\\n\",best,a[best]);}}")
 };}

 public static List<TestCase> Tests(int id,int seed){var r=new Random(seed);var tests=new List<TestCase>();for(int i=0;i<8;i++)tests.Add(Generate(id,r,i));for(int i=0;i<20;i++)tests.Add(Generate(id,r,-1));return tests;}
 static string J<T>(IEnumerable<T> a){return string.Join(" ",a.Select(x=>Convert.ToString(x,CultureInfo.InvariantCulture)).ToArray());}
 static bool Prime(int n){if(n<2)return false;for(int i=2;i*i<=n;i++)if(n%i==0)return false;return true;}
 static string Letters(Random r,int n){return new string(Enumerable.Range(0,n).Select(i=>(char)('a'+r.Next(4))).ToArray());}
 static string Digits(Random r,int n){return ((char)('1'+r.Next(9))).ToString()+new string(Enumerable.Range(1,n-1).Select(i=>(char)('0'+r.Next(10))).ToArray());}
 static int[] Arr(Random r,int n,int lo,int hi,int edge){return Enumerable.Range(0,n).Select(i=>edge==0?lo:edge==1?hi:edge==2?0:r.Next(lo,hi+1)).ToArray();}
 static string BigAdd(string a,string b){var s=new StringBuilder();int i=a.Length-1,j=b.Length-1,c=0;while(i>=0||j>=0||c>0){int v=c+(i>=0?a[i--]-'0':0)+(j>=0?b[j--]-'0':0);s.Append((char)('0'+v%10));c=v/10;}return new string(s.ToString().Reverse().ToArray()).TrimStart('0').PadLeft(1,'0');}
 static string BigSubtract(string a,string b){if(a.Length<b.Length||(a.Length==b.Length&&string.CompareOrdinal(a,b)<0)){string t=a;a=b;b=t;}var output=new StringBuilder();int borrow=0,j=b.Length-1;for(int i=a.Length-1;i>=0;i--){int v=a[i]-'0'-(j>=0?b[j--]-'0':0)-borrow;borrow=v<0?1:0;if(v<0)v+=10;output.Append((char)('0'+v));}return new string(output.ToString().Reverse().ToArray()).TrimStart('0').PadLeft(1,'0');}
 static string BigMul(string a,string b){int[] d=new int[a.Length+b.Length+1];for(int i=0;i<a.Length;i++)for(int j=0;j<b.Length;j++)d[i+j]+=(a[a.Length-1-i]-'0')*(b[b.Length-1-j]-'0');for(int i=0;i<d.Length-1;i++){d[i+1]+=d[i]/10;d[i]%=10;}return new string(d.Reverse().Select(x=>(char)('0'+x)).ToArray()).TrimStart('0').PadLeft(1,'0');}
 // Independent managed-language oracles. Boundary index 0..7 is reproducible.
 public static TestCase Generate(int id,Random r,int e){
  int k=id-100;bool edge=e>=0;int n=edge?(e==0?1:e==1?80:8):r.Next(2,35);string input="",expected="";var output=new List<string>();
  if(k==1||k==2||k==6||k==7||k==8||k==21||k==22||k==33||k==37||k==40||k==41||k==42){
   if(edge&&e==1&&(k==21||k==22||k==33))n=k==33?120:150;
   if(k==37)n=edge?(e==0?1:20):r.Next(1,21);if(k==8)n=Math.Min(n,60);
   int limit=k==1||k==6||k==7||k==21||k==22||k==33||k==41?1000:100;
   int[] a=Arr(r,n,k==8||k==37?1:-limit,k==8?100:k==37?20:limit,e);if(k==8||k==37)for(int i=0;i<n;i++)if(a[i]==0)a[i]=1;
   if(k==1){int q=10;var sb=new StringBuilder(n+" "+q+"\n"+J(a)+"\n");for(int i=0;i<q;i++){int l=i==0?0:r.Next(n),h=i==0?n-1:r.Next(l,n);sb.AppendLine((l+1)+" "+(h+1));output.Add(a.Skip(l).Take(h-l+1).Sum()+" "+a.Skip(l).Take(h-l+1).Select((x,j)=>(long)x*(j+1)).Sum());}input=sb.ToString();}
   else if(k==2){input=n+"\n"+J(a)+"\n";int best=int.MinValue;for(int i=0;i<n;i++){int sum=0;for(int j=0;j<n;j++){sum+=a[(i+j)%n];best=Math.Max(best,sum);}}expected=best.ToString();}
   else if(k==6){input=n+"\n"+J(a)+"\n";expected=J(Enumerable.Range(0,n).Select(i=>Enumerable.Range(i+1,n-i-1).Where(j=>a[j]>a[i]).Select(j=>j+1).FirstOrDefault()));}
   else if(k==7){int w=edge?(e%2==0?1:n):r.Next(1,n+1);input=n+" "+w+"\n"+J(a)+"\n";expected=J(Enumerable.Range(0,n-w+1).Select(i=>a.Skip(i).Take(w).Max()));}
   else if(k==8){int d=edge?(e%2==0?1:n):r.Next(1,n+1);input=n+" "+d+"\n"+J(a)+"\n";int[,] dp=new int[n+1,d+1];for(int i=1;i<=n;i++)for(int j=0;j<=d;j++)dp[i,j]=1000000;for(int i=1;i<=n;i++)for(int j=1;j<=d;j++){int sum=0;for(int t=i-1;t>=0;t--){sum+=a[t];dp[i,j]=Math.Min(dp[i,j],Math.Max(dp[t,j-1],sum));}}expected=dp[n,d].ToString();}
   else if(k==21){input=n+"\n"+J(a)+"\n";long inv=0;for(int i=0;i<n;i++)for(int j=i+1;j<n;j++)if(a[i]>a[j])inv++;expected=inv.ToString();}
   else if(k==22){int which=edge?(e%2==0?1:n):r.Next(1,n+1);input=n+" "+which+"\n"+J(a)+"\n";expected=a.OrderBy(x=>x).ElementAt(which-1).ToString();}
   else if(k==33){input=n+"\n"+J(a)+"\n";int[] dp=new int[n];long[] counts=new long[n];for(int i=0;i<n;i++){dp[i]=1;for(int j=0;j<i;j++)if(a[j]<a[i])dp[i]=Math.Max(dp[i],dp[j]+1);if(dp[i]==1)counts[i]=1;else for(int j=0;j<i;j++)if(a[j]<a[i]&&dp[j]+1==dp[i])counts[i]=(counts[i]+counts[j])%1000000007;}int best=dp.Max();long ways=0;for(int i=0;i<n;i++)if(dp[i]==best)ways=(ways+counts[i])%1000000007;expected=best+" "+ways;}
   else if(k==37){input=n+"\n"+J(a)+"\n";var sums=new HashSet<int>{0};foreach(int x in a)foreach(int s in sums.ToArray())sums.Add(s+x);int total=a.Sum();expected=sums.Min(s=>Math.Abs(total-2*s)).ToString();}
   else{int q=35;var sb=new StringBuilder(n+" "+q+"\n"+J(a)+"\n");for(int i=0;i<q;i++){int l=i==0?0:r.Next(n),h=i==0?n-1:r.Next(l,n);if(i%3==1){int v=r.Next(-100,101);if(k==40){sb.AppendLine("A "+(l+1)+" "+v);a[l]+=v;}else if(k==41){sb.AppendLine("S "+(l+1)+" "+v);a[l]=v;}else{sb.AppendLine("A "+(l+1)+" "+(h+1)+" "+v);for(int j=l;j<=h;j++)a[j]+=v;}}else{sb.AppendLine((k==41?"M ":"Q ")+(l+1)+" "+(h+1));output.Add((k==41?a.Skip(l).Take(h-l+1).Max():a.Skip(l).Take(h-l+1).Sum()).ToString());}}input=sb.ToString();}
  }
  else if(k==3||k==4||k==13||k==14||k==30||k==34||k==35||k==43){
   string s=edge?(e==0?"a":e==1?new string('a',k==13||k==43?200:120):e==2?"abacaba":e==3?"abcdabcd":"abcabcaba"):Letters(r,r.Next(1,101));
   if(k==3){input=s+"\n";int best=0;for(int i=0;i<s.Length;i++){var set=new HashSet<char>();int j=i;while(j<s.Length&&set.Add(s[j]))j++;best=Math.Max(best,j-i);}expected=best.ToString();}
   else if(k==4){string[] boundaries={"()","([{}])", ")(","((", "([)]", "{}[]()",new string('(',60)+new string(')',60), "{[()]}]"};s=edge?boundaries[e]:new string(Enumerable.Range(0,r.Next(1,90)).Select(i=>"()[]{}"[r.Next(6)]).ToArray());input=s+"\n";var stack=new Stack<char>();int depth=0,bad=0;for(int i=0;i<s.Length;i++){char c=s[i];if("([{".Contains(c)){stack.Push(c);depth=Math.Max(depth,stack.Count);}else if(stack.Count==0||"([{"[")]}".IndexOf(c)]!=stack.Pop()){bad=i+1;break;}}if(bad==0&&stack.Count>0)bad=s.Length+1;expected=bad==0?"YES "+depth:"NO "+bad;}
   else if(k==13||k==43){int q=k==13?1:edge?10:r.Next(1,11);var sb=new StringBuilder(s+"\n"+(k==43?q+"\n":""));for(int i=0;i<q;i++){string p=edge?(e==0?"a":e==1?"aa":e==2?"aba":e==7?new string('a',k==13?40:25):"abc"):r.Next(2)==0?s.Substring(r.Next(s.Length),1):Letters(r,r.Next(1,12));sb.AppendLine(p);int count=0;for(int j=0;j+p.Length<=s.Length;j++)if(s.Substring(j,p.Length)==p)count++;output.Add(count.ToString());if(k==13){var positions=Enumerable.Range(0,Math.Max(0,s.Length-p.Length+1)).Where(j=>s.Substring(j,p.Length)==p).Select(j=>j+1).ToArray();output.Add(positions.Length==0?"0":J(positions));}}input=sb.ToString();}
   else if(k==14){input=s+"\n";int best=1;for(int i=0;i<s.Length;i++)for(int j=i;j<s.Length;j++){string t=s.Substring(i,j-i+1);if(t==new string(t.Reverse().ToArray()))best=Math.Max(best,t.Length);}expected=best.ToString();}
   else if(k==30){int q=10;var words=Enumerable.Range(0,20).Select(i=>edge&&e==0?"a":Letters(r,r.Next(1,21))).ToArray();var sb=new StringBuilder("20 "+q+"\n"+string.Join("\n",words)+"\n");for(int i=0;i<q;i++){string p=i%2==0?words[i].Substring(0,Math.Min(3,words[i].Length)):Letters(r,5);sb.AppendLine(p);output.Add(words.Count(w=>w.StartsWith(p,StringComparison.Ordinal)).ToString());}input=sb.ToString();}
   else{string a=s.Substring(0,Math.Min(k==34?70:60,s.Length)),b=edge?(e%2==0?a:"dddddd"):Letters(r,r.Next(1,50));input=a+"\n"+b+"\n";int[,] dp=new int[a.Length+1,b.Length+1];if(k==35){for(int i=0;i<=a.Length;i++)dp[i,0]=i;for(int j=0;j<=b.Length;j++)dp[0,j]=j;}for(int i=1;i<=a.Length;i++)for(int j=1;j<=b.Length;j++)dp[i,j]=k==34?(a[i-1]==b[j-1]?dp[i-1,j-1]+1:Math.Max(dp[i-1,j],dp[i,j-1])):Math.Min(dp[i-1,j-1]+(a[i-1]==b[j-1]?0:1),Math.Min(dp[i-1,j]+1,dp[i,j-1]+1));expected=dp[a.Length,b.Length].ToString();}
  }
  else if(k==5){n=edge?(e==0?1:30):r.Next(1,31);var intervals=new List<int[]>();for(int i=0;i<n;i++){int l=edge&&e==1?-30:r.Next(-30,31),h=edge&&e==1?30:r.Next(l,31);intervals.Add(new[]{l,h});}input=n+"\n"+string.Join("\n",intervals.Select(a=>J(a)))+"\n";var merged=new List<int[]>();foreach(var a in intervals.OrderBy(x=>x[0]).ThenBy(x=>x[1])){if(merged.Count>0&&a[0]<=merged.Last()[1])merged.Last()[1]=Math.Max(merged.Last()[1],a[1]);else merged.Add((int[])a.Clone());}expected=merged.Count+"\n"+string.Join("\n",merged.Select(a=>J(a)));}
  else if(k==9){int l=edge?(e==0?0:e==1?10000:2):r.Next(10000),h=edge?(e==0?1:e==1?10000:10000):r.Next(l,10001);input=l+" "+h+"\n";var primes=Enumerable.Range(l,h-l+1).Where(Prime).ToArray();expected=primes.Length+" "+primes.Sum();}
  else if(k==10||k==11){int limit=k==10?80:40;string a=edge?(e==0?"0":e==1?new string('9',limit):e==2?"1":Digits(r,limit)):Digits(r,r.Next(1,limit+1));string b=edge?(e==0?"0":e==1?"1":e==2?new string('9',limit):Digits(r,limit)):Digits(r,r.Next(1,limit+1));input=a+"\n"+b+"\n";expected=k==10?BigAdd(a,b)+"\n"+BigSubtract(a,b):BigMul(a,b);}
  else if(k==12){int b=edge?(e==0?2:16):r.Next(2,17),len=edge?(e==0?1:12):r.Next(1,13);string s=edge&&e==0?"0":new string(Enumerable.Range(0,len).Select(i=>"0123456789ABCDEF"[r.Next(i==0?1:0,b)]).ToArray());long v=0;foreach(char c in s)v=v*b+"0123456789ABCDEF".IndexOf(c);input=b+" "+s+"\n";expected=v.ToString();}
  else if(k==15){var sb=new StringBuilder();var dec=new StringBuilder();int count=edge?12:r.Next(1,13);for(int i=0;i<count;i++){char c=(char)('a'+r.Next(26));int v=edge?(e%2==0?1:20):r.Next(1,21);sb.Append(c).Append(v);dec.Append(new string(c,v));}input=sb+"\n";expected=dec.ToString();}
  else if(k==16){n=edge?(e==0?0:8):r.Next(9);int x=edge?(e==0?0:e==1?-3:3):r.Next(-3,4);int[] a=Arr(r,n+1,-20,20,e);input=n+" "+x+"\n"+J(a)+"\n";long v=0;foreach(int c in a)v=v*x+c;expected=v.ToString();}
  else if(k==17||k==18||k==19||k==24||k==47){
   int rows=edge?(e==0?(k==24||k==47?2:1):k==18?6:k==24?10:8):r.Next(k==24||k==47?2:1,k==18?7:9),cols=edge?(e==0?(k==24||k==47?2:1):e==2?1:k==18?6:k==24?10:8):r.Next(k==24||k==47?2:1,k==18?7:9);if(k==24||k==47)cols=Math.Max(2,cols);
   int[,] a=new int[rows,cols];for(int i=0;i<rows;i++)for(int j=0;j<cols;j++)a[i,j]=k==24?(edge?(e==1?0:1):r.Next(3)>0?1:0):k==47?(edge?(e==1?0:9):r.Next(10)):r.Next(-10,11);
   var sb=new StringBuilder(rows+" "+cols+"\n");
   if(k==17){var entries=new List<string>();for(int i=0;i<rows;i++)for(int j=0;j<cols;j++)if(a[i,j]!=0&&!(edge&&e==0))entries.Add((i+1)+" "+(j+1)+" "+a[i,j]);input=rows+" "+cols+" "+entries.Count+"\n"+string.Join("\n",entries)+"\n";output.Add(entries.Count.ToString());for(int j=0;j<cols;j++)for(int i=0;i<rows;i++)if(a[i,j]!=0&&!(edge&&e==0))output.Add((j+1)+" "+(i+1)+" "+a[i,j]);}
   else if(k==18){int mid=edge?(e==0?1:6):r.Next(1,7),t=edge?(e==0?1:6):r.Next(1,7);int[,] aa=new int[rows,mid],bb=new int[mid,t],cc=new int[t,cols];sb=new StringBuilder(rows+" "+mid+" "+t+" "+cols+"\n");foreach(var mat in new[]{aa,bb,cc})for(int i=0;i<mat.GetLength(0);i++){for(int j=0;j<mat.GetLength(1);j++){mat[i,j]=edge&&e==0?0:r.Next(-10,11);sb.Append(mat[i,j]).Append(' ');}sb.AppendLine();}for(int i=0;i<rows;i++){var line=new List<int>();for(int j=0;j<cols;j++){int v=0;for(int x=0;x<mid;x++)for(int y=0;y<t;y++)v+=aa[i,x]*bb[x,y]*cc[y,j];line.Add(v);}output.Add(J(line));}input=sb.ToString();}
   else if(k==19){for(int i=0;i<rows;i++){for(int j=0;j<cols;j++)sb.Append(a[i,j]).Append(' ');sb.AppendLine();}input=sb.ToString();var values=new List<int>();bool[,] seen=new bool[rows,cols];int x=0,y=0,dir=0;int[] dx={0,1,0,-1},dy={1,0,-1,0};for(int t=0;t<rows*cols;t++){values.Add(a[x,y]);seen[x,y]=true;int nx=x+dx[dir],ny=y+dy[dir];if(nx<0||nx>=rows||ny<0||ny>=cols||seen[nx,ny]){dir=(dir+1)%4;nx=x+dx[dir];ny=y+dy[dir];}x=nx;y=ny;}expected=J(values);}
   else{a[0,0]=a[rows-1,cols-1]=1;if(k==24&&!(edge&&e==1)){a[0,1]=2;a[rows-1,cols-2]=3;}for(int i=0;i<rows;i++){if(k==24){for(int j=0;j<cols;j++)sb.Append(a[i,j]==0?'#':a[i,j]==2?'K':a[i,j]==3?'D':'.');}else for(int j=0;j<cols;j++)sb.Append(a[i,j]).Append(' ');sb.AppendLine();}input=sb.ToString();int size=rows*cols;var edges=new List<int[]>();for(int x=0;x<rows;x++)for(int y=0;y<cols;y++)if(a[x,y]>0)foreach(int[] delta in new[]{new[]{1,0},new[]{-1,0},new[]{0,1},new[]{0,-1}}){int u=x+delta[0],v=y+delta[1];if(u>=0&&u<rows&&v>=0&&v<cols&&a[u,v]>0){if(k==24){for(int held=0;held<2;held++)if(a[u,v]!=3||held==1){int z=held==1||a[u,v]==2?1:0;edges.Add(new[]{(x*cols+y)*2+held,(u*cols+v)*2+z,1});}}else edges.Add(new[]{x*cols+y,u*cols+v,a[u,v]});}}var d=Distances(k==24?size*2:size,edges,0);int result=k==24?Math.Min(d[(size-1)*2],d[(size-1)*2+1]):d[size-1];expected=(result>=Inf?-1:result).ToString();}
  }
  else if(k==20){long a=edge?(e==0?0:100000):r.Next(100001),b=edge?(e==0?0:e==1?1000000000:1):r.Next(1000000001),m=edge?(e==0?1:100000):r.Next(1,100001);input=a+" "+b+" "+m+"\n";long v=1%m;while(b>0){if(b%2==1)v=v*a%m;a=a*a%m;b/=2;}expected=v.ToString();}
  else if(k==23){n=edge?(e==0?1:30):r.Next(1,31);int[] group=Enumerable.Range(0,n).ToArray();var sb=new StringBuilder(n+" 40\n");for(int i=0;i<40;i++){int a=r.Next(n),b=i==0?a:r.Next(n);if(i%3==0){sb.AppendLine("U "+(a+1)+" "+(b+1));int old=group[b],to=group[a];for(int j=0;j<n;j++)if(group[j]==old)group[j]=to;}else{sb.AppendLine("Q "+(a+1)+" "+(b+1));output.Add(group[a]==group[b]?"YES":"NO");}}input=sb.ToString();}
  else if(k==25||k==26||k==27||k==28||k==29||k==39||k==44||k==45||k==46){
   n=edge?(e==0?(k==25||k==44?1:2):k==25||k==26||k==39?15:k==27||k==28?12:k==44||k==45?14:10):r.Next(2,k==25||k==26||k==39?16:k==27||k==28?13:k==44||k==45?15:11);
   var edges=new List<int[]>();bool undirected=k==28||k==45;for(int u=0;u<n;u++)for(int v=0;v<n;v++)if(u!=v&&(!undirected||u<v)&&(k!=39||u<v)){bool include=edge?(e==0?false:e==1?true:e==2?v==u+1:r.Next(3)==0):r.Next(4)==0;if(include)edges.Add(new[]{u,v,k==27?(edge&&e==1?-1:r.Next(-10,21)):r.Next(1,k==28?41:k==39?21:31)});}
   int s=edge?0:r.Next(n),q=10;var sb=new StringBuilder(n+" "+edges.Count+(k==26||k==27?" "+(s+1):k==29?" "+q:"")+"\n");foreach(int[] a in edges)sb.AppendLine((a[0]+1)+" "+(a[1]+1)+(k==25||k==44||k==45?"":" "+a[2]));input=sb.ToString();
   if(k==25){int[] deg=new int[n];foreach(var a in edges)deg[a[1]]++;var done=new HashSet<int>();var order=new List<int>();for(int t=0;t<n;t++){int u=Enumerable.Range(0,n).Where(i=>!done.Contains(i)&&deg[i]==0).DefaultIfEmpty(-1).First();if(u<0)break;done.Add(u);order.Add(u+1);foreach(var a in edges.Where(a=>a[0]==u))deg[a[1]]--;}expected=order.Count<n?"CYCLE":J(order);}
   else if(k==26||k==27){var d=Distances(n,edges,s);bool neg=edges.Any(a=>d[a[0]]<Inf&&d[a[1]]>d[a[0]]+a[2]);expected=neg?"NEGATIVE":J(d.Select(x=>x>=Inf?-1:x));}
   else if(k==28){int[] group=Enumerable.Range(0,n).ToArray();int sum=0,count=0;foreach(var a in edges.OrderBy(a=>a[2]))if(group[a[0]]!=group[a[1]]){int old=group[a[1]],to=group[a[0]];for(int i=0;i<n;i++)if(group[i]==old)group[i]=to;sum+=a[2];count++;}expected=count==n-1?sum.ToString():"DISCONNECTED";}
   else if(k==29){for(int i=0;i<q;i++){int a=i==0?0:r.Next(n),b=i==0?a:r.Next(n);sb.AppendLine((a+1)+" "+(b+1));var d=Distances(n,edges,a);output.Add((d[b]>=Inf?-1:d[b]).ToString());}input=sb.ToString();}
   else if(k==39){int[] d=Enumerable.Repeat(-1,n).ToArray();d[0]=0;for(int u=0;u<n;u++)if(d[u]>=0)foreach(var a in edges.Where(a=>a[0]==u))d[a[1]]=Math.Max(d[a[1]],d[u]+a[2]);expected=d[n-1].ToString();}
   else if(k==44){bool[,] reach=new bool[n,n];for(int i=0;i<n;i++){var visited=Reach(n,edges,i,false);foreach(int j in visited)reach[i,j]=true;}var seen=new HashSet<int>();int count=0;for(int i=0;i<n;i++)if(!seen.Contains(i)){count++;for(int j=0;j<n;j++)if(reach[i,j]&&reach[j,i])seen.Add(j);}expected=count.ToString();}
   else if(k==45){int before=Components(n,edges),bridges=0;for(int i=0;i<edges.Count;i++)if(Components(n,edges.Where((a,j)=>i!=j).ToList())>before)bridges++;expected=bridges.ToString();}
   else{ // Max flow oracle via exhaustive minimum cuts, not augmentation.
    int best=int.MaxValue;for(int mask=0;mask<(1<<n);mask++)if((mask&1)!=0&&(mask&(1<<(n-1)))==0){int cap=0;foreach(var a in edges)if((mask&(1<<a[0]))!=0&&(mask&(1<<a[1]))==0)cap+=a[2];best=Math.Min(best,cap);}expected=best.ToString();}
  }
  else if(k==31||k==32){n=edge?(e==0?1:6):r.Next(1,7);int target=edge?(e==0?0:k==31?60:100):r.Next(k==31?61:101);int[] coins=Enumerable.Range(1,20).OrderBy(i=>r.Next()).Take(n).ToArray();if(edge&&e==1)coins=Enumerable.Range(1,n).ToArray();input=n+" "+target+"\n"+J(coins)+"\n";if(k==31){long[,] dp=new long[n+1,target+1];for(int i=0;i<=n;i++)dp[i,0]=1;for(int i=1;i<=n;i++)for(int j=1;j<=target;j++)for(int count=0;count*coins[i-1]<=j;count++)dp[i,j]+=dp[i-1,j-count*coins[i-1]];expected=dp[n,target].ToString();}else{int[] quantities=Enumerable.Range(0,n).Select(i=>edge&&e==1?0:r.Next(11)).ToArray();input=n+" "+target+"\n"+string.Join("\n",Enumerable.Range(0,n).Select(i=>coins[i]+" "+quantities[i]))+"\n";int[,] dp=new int[n+1,target+1];for(int i=0;i<=n;i++)for(int j=1;j<=target;j++)dp[i,j]=10000;for(int i=1;i<=n;i++)for(int j=0;j<=target;j++)for(int c=0;c<=quantities[i-1]&&c*coins[i-1]<=j;c++)dp[i,j]=Math.Min(dp[i,j],dp[i-1,j-c*coins[i-1]]+c);expected=(dp[n,target]==10000?-1:dp[n,target]).ToString();}}
  else if(k==36){n=edge?(e==0?1:20):r.Next(1,21);int cap=edge?(e==0?0:80):r.Next(81);int[] weights=Enumerable.Range(0,n).Select(i=>r.Next(1,21)).ToArray(),values=Enumerable.Range(0,n).Select(i=>r.Next(101)).ToArray();input=n+" "+cap+"\n"+string.Join("\n",Enumerable.Range(0,n).Select(i=>weights[i]+" "+values[i]))+"\n";int[,] dp=new int[n+1,cap+1];for(int i=1;i<=n;i++)for(int j=0;j<=cap;j++){dp[i,j]=dp[i-1,j];if(j>=weights[i-1])dp[i,j]=Math.Max(dp[i,j],dp[i-1,j-weights[i-1]]+values[i-1]);}expected=dp[n,cap].ToString();}
  else if(k==38){n=edge?(e==0?1:8):r.Next(1,9);int[] p=Enumerable.Range(0,n+1).Select(i=>edge&&e==1?30:r.Next(1,31)).ToArray();input=n+"\n"+J(p)+"\n";int[,] memo=new int[n,n];Func<int,int,int> f=null;f=(l,h)=>{if(l==h)return 0;if(memo[l,h]>0)return memo[l,h];int best=int.MaxValue;for(int j=l;j<h;j++)best=Math.Min(best,f(l,j)+f(j+1,h)+p[l]*p[j+1]*p[h+1]);return memo[l,h]=best;};expected=f(0,n-1).ToString();}
  else if(k==48){n=edge?(e==0?2:9):r.Next(2,9);int[,] a=new int[n,n];for(int i=0;i<n;i++)for(int j=i+1;j<n;j++)a[i,j]=a[j,i]=edge&&e==1?30:r.Next(1,31);var sb=new StringBuilder(n+"\n");for(int i=0;i<n;i++)sb.AppendLine(J(Enumerable.Range(0,n).Select(j=>a[i,j])));input=sb.ToString();int best=int.MaxValue;Action<int,int,int> f=null;f=(u,mask,sum)=>{if(sum>=best)return;if(mask==(1<<n)-1){best=Math.Min(best,sum+a[u,0]);return;}for(int v=1;v<n;v++)if((mask&(1<<v))==0)f(v,mask|(1<<v),sum+a[u,v]);};f(0,1,0);expected=best.ToString();}
  else if(k==49){long value;string expr=edge?new[]{"0","-(2+3)*4","1-2-3","--9","2*(3+4*5)","99*99*99","(-1)*(-2)+3","1--2"}[e]:Expression(r,3,out value);input=expr+"\n";expected=EvalExpression(expr).ToString();}
  else if(k==50){var lines=new List<string>();int[] stock=new int[11];var snapshots=new List<int[]>();Action<string> command=line=>{lines.Add(line);string[] z=line.Split(' ');int idd=z.Length>1?int.Parse(z[1]):0,v=z.Length>2?int.Parse(z[2]):0;switch(z[0]){case "ADD":stock[idd]+=v;break;case "SELL":if(stock[idd]>=v){stock[idd]-=v;output.Add("OK");}else output.Add("FAIL");break;case "GET":output.Add(stock[idd].ToString());break;case "SNAP":snapshots.Add((int[])stock.Clone());output.Add(snapshots.Count.ToString());break;case "LOAD":stock=(int[])snapshots[idd-1].Clone();output.Add("OK");break;case "TOTAL":output.Add(stock.Sum().ToString());break;case "TOP":int best=Enumerable.Range(1,10).OrderByDescending(i=>stock[i]).ThenBy(i=>i).First();output.Add(best+" "+stock[best]);break;}};foreach(string z in new[]{"TOP","SELL 1 1","SNAP","ADD 1 10","ADD 2 10","TOP","SNAP","SELL 1 10","LOAD 1","TOTAL","LOAD 2","GET 1"})command(z);for(int i=lines.Count;i<80;i++){int idd=r.Next(1,11),v=r.Next(1,31);string[] ops={"ADD "+idd+" "+v,"SELL "+idd+" "+v,"GET "+idd,"SNAP","LOAD "+r.Next(1,snapshots.Count+1),"TOTAL","TOP"};command(ops[r.Next(ops.Length)]);}input=lines.Count+"\n"+string.Join("\n",lines)+"\n";}
  else throw new ArgumentException("Unknown challenge "+id);
  if(output.Count>0)expected=string.Join("\n",output);return new TestCase(input,expected+"\n",edge);
 }
 const int Inf=1000000;
 static int[] Distances(int n,List<int[]> edges,int source){int[] d=Enumerable.Repeat(Inf,n).ToArray();d[source]=0;for(int k=0;k<n-1;k++){bool changed=false;foreach(var a in edges)if(d[a[0]]<Inf&&d[a[1]]>d[a[0]]+a[2]){d[a[1]]=d[a[0]]+a[2];changed=true;}if(!changed)break;}return d;}
 static HashSet<int> Reach(int n,List<int[]> edges,int start,bool undirected){var seen=new HashSet<int>{start};var q=new Queue<int>();q.Enqueue(start);while(q.Count>0){int u=q.Dequeue();foreach(var e in edges){int v=e[0]==u?e[1]:undirected&&e[1]==u?e[0]:-1;if(v>=0&&seen.Add(v))q.Enqueue(v);}}return seen;}
 static int Components(int n,List<int[]> edges){var seen=new HashSet<int>();int count=0;for(int i=0;i<n;i++)if(!seen.Contains(i)){count++;seen.UnionWith(Reach(n,edges,i,true));}return count;}
 static string Expression(Random r,int depth,out long value){if(depth==0){value=r.Next(20);return value.ToString();}long a,b;string left=Expression(r,depth-1,out a),right=Expression(r,depth-1,out b);char op="+-*"[r.Next(3)];value=op=='+'?a+b:op=='-'?a-b:a*b;bool neg=r.Next(4)==0;if(neg)value=-value;return (neg?"-":"")+"("+left+op+right+")";}
 // Shunting-yard oracle; independent from recursive-descent C answer.
 static long EvalExpression(string s){var values=new Stack<long>();var ops=new Stack<char>();Action apply=()=>{char op=ops.Pop();long b=values.Pop();if(op=='~'){values.Push(-b);return;}long a=values.Pop();values.Push(op=='+'?a+b:op=='-'?a-b:a*b);};Func<char,int> prec=c=>c=='~'?3:c=='*'?2:c=='+'||c=='-'?1:0;bool unary=true;for(int i=0;i<s.Length;i++){char c=s[i];if(char.IsDigit(c)){long v=0;while(i<s.Length&&char.IsDigit(s[i]))v=v*10+s[i++]-'0';i--;values.Push(v);unary=false;}else if(c=='('){ops.Push(c);unary=true;}else if(c==')'){while(ops.Peek()!='(')apply();ops.Pop();unary=false;}else{char op=c=='-'&&unary?'~':c;while(ops.Count>0&&ops.Peek()!='('&&(prec(ops.Peek())>prec(op)||prec(ops.Peek())==prec(op)&&op!='~'))apply();ops.Push(op);unary=true;}}while(ops.Count>0)apply();return values.Pop();}
}
