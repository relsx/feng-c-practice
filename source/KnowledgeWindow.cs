using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

sealed class KnowledgeTopic {
 public string Key,Title,Pattern,Explanation;public int FirstProblem;
 public bool Raw;
 public KnowledgeTopic(string key,string title,string pattern,string explanation,bool raw=false){Key=key;Title=title;Pattern=pattern;Explanation=explanation;Raw=raw;}
}
sealed class PracticeLesson {
 public Problem Problem;public string Goal;public List<KnowledgeTopic> New=new List<KnowledgeTopic>(),Review=new List<KnowledgeTopic>();
}
static class PracticeKnowledge {
 public static readonly List<PracticeLesson> All;
 static PracticeKnowledge(){All=Build();}
 public static PracticeLesson For(int id){return All.First(x=>x.Problem.Id==id);}
 static string Clean(string source){string s=Regex.Replace(source,@"/\*[\s\S]*?\*/|//[^\r\n]*|""(?:\\.|[^""\\])*""|'(?:\\.|[^'\\])*'",m=>m.Value.StartsWith("/*")||m.Value.StartsWith("//")?" ":"LITERAL");return Regex.Replace(s,@"^\s*#.*$","",RegexOptions.Multiline);}
 static List<KnowledgeTopic> Topics(){return new List<KnowledgeTopic>{
  new KnowledgeTopic("structure","C 程序结构、头文件与入口",@"\bmain\s*\(","#include <stdio.h> 引入标准输入输出函数的声明。int main(void) 是程序入口，花括号定义代码块，分号结束声明或语句。return 0 通常表示正常结束。变量在使用前要声明，局部变量若未初始化，其数值不可依赖。",true),
  new KnowledgeTopic("int","int、变量、初始化与赋值",@"\bint\b","int 保存整数。int value = 0 同时声明并初始化；后续 value = 8 修改值。= 是赋值，== 才是比较。声明的作用域从声明处开始，到所在代码块结束；同一作用域不能重复定义同名变量。"),
  new KnowledgeTopic("scanf","scanf 输入、格式串与取地址 &",@"\bscanf\s*\(","scanf(\"%d\", &value) 按格式读取 int；&value 表示变量地址，函数通过该地址写入。多个格式项与实参一一对应。scanf 返回成功读取项数；本软件的判题数据符合题目格式。读取字符串数组传数组名，不额外加 &。",true),
  new KnowledgeTopic("printf","printf 输出、格式类型与转义",@"\bprintf\s*\(|\bputs\s*\(|\bputchar\s*\(","printf 的格式与参数类型要匹配：%d 对应 int，%lld 对应 long long，%f 对应传入的 double，%c 输出字符，%s 输出以零字符结束的字符串。\\n 表示换行，\\t 表示制表符，\\\\ 表示反斜杠。不要输出“请输入”等题目未要求的文字。",true),
  new KnowledgeTopic("add","加法 + 与表达式",@"(?<!\+)\+(?![+=])","+ 将数值相加，先计算表达式再作为输出参数。整数计算仍受类型范围限制。运算中较窄的整数不会因为最终赋给 long long 就自动避免中间溢出，必要时应在运算之前扩展类型。"),
  new KnowledgeTopic("subtract","减法、负数与运算顺序",@"(?<![-=>])-(?![-=>])","- 可以表示减法，也可以表示一元取负。左右操作数的顺序会影响减法结果；负数不需要单独拼接减号输出。括号可以明确你希望先计算的部分。"),
  new KnowledgeTopic("multiply","乘法 *、优先级与扩展整数",@"(?<![*/])\*(?![*/=])","乘法 * 的优先级高于加减。例如 (x+y)*z 与 x+y*z 不同。乘法应先检查数据范围；使用 (long long)x*y 可在乘法之前扩展类型，避免先按 int 相乘产生溢出。指针声明和解引用也使用 *，含义取决于上下文。"),
  new KnowledgeTopic("division","整数除法 /、取余 % 与除零",@"(?<![/])/(?![/=*])|%(?!=)","两个整数相除得到整数，不能保留小数；非负整数除法的余数用 % 求得。除数为0时不能使用 / 或 %。浮点平均值应让至少一个操作数是浮点类型。对负整数，C 的整数除法向零截断，余数符号遵循被除数。"),
  new KnowledgeTopic("floating","double、浮点除法与输出精度",@"\bdouble\b|\bfloat\b","double 保存浮点数。3.0 与 3 的类型不同，先进行浮点除法才会得到小数；把已经截断的整数商赋给 double 无法恢复小数。printf 的 %.6f 输出六位小数；scanf 读取 double 使用 %lf，读取 float 使用 %f。"),
  new KnowledgeTopic("cast","显式类型转换",@"\(\s*(double|float|int|long\s+long|char|unsigned\s+char)\s*\)","(类型)表达式显式转换类型。例如 (double)count 在运算前转为浮点数。转换为整数会丢弃小数部分，窄类型也可能容纳不下原值；应先确认范围。ctype.h 函数接收的字符应先转换为 unsigned char，避免负字符值产生未定义行为。"),
  new KnowledgeTopic("const","const、常量与宏",@"\bconst\b|#\s*define","const 表示不能通过该名称修改对象；#define 在预处理阶段替换标记。宏表达式要用括号保护，避免优先级变化。圆周率这类常量可声明为 const double；不要在计算中反复散写难以辨认的数值。",true),
  new KnowledgeTopic("if","if / else 与条件选择",@"\bif\b|\belse\b","if (条件) 在条件为非0时执行相应分支，else 处理其余情况。else 与最近尚未配对的 if 结合，建议使用花括号明确代码块。连续的 if 与 if/else 链的含义不同，需先判断各情况是否互斥。"),
  new KnowledgeTopic("compare","比较与边界包含关系",@"==|!=|<=|>=|(?<![<>=-])<(?![<=])|(?<![>=-])>(?![>=])","==、!=、<、<=、>、>= 产生0或1。边界是否包含等号必须与题目一致。不能写数学式 0<x<10 来表示区间，它会先把 0<x 算成0或1；应使用 x>0 && x<10。"),
  new KnowledgeTopic("ternary","条件表达式 ?: ",@"\?","条件 ? 值A : 值B 根据真假只计算其中一项。它适合简单取值，复杂逻辑用 if/else 更易读。嵌套条件表达式容易误读，可以拆开或加括号，不必为了少一行代码牺牲清晰度。"),
  new KnowledgeTopic("logic","&&、||、! 与短路求值",@"&&|\|\||!(?!=)","&& 表示同时成立，|| 表示至少一个成立，! 取反。&& 左侧为假时不计算右侧，|| 左侧为真时不计算右侧。检查数组边界或指针有效性应放在左侧，再读取元素，利用短路避免非法访问。"),
  new KnowledgeTopic("char","char、字符常量与字符分类",@"\bchar\b","char 可保存单个字符，'a' 与 \"a\" 不同：前者是字符常量，后者是带结尾 '\\0' 的字符串。比较数字字符使用 '0'～'9'，不能与数值0～9混淆。字符算术适用于题目规定的 ASCII 数据。",true),
  new KnowledgeTopic("ctype","ctype.h 的分类与大小写函数",@"\b(isdigit|isalpha|isspace|tolower|toupper)\s*\(","ctype.h 提供字符分类和大小写转换，例如 isdigit、tolower、toupper。参数必须是 EOF 或可由 unsigned char 表示的值；处理 char 时可先转换为 unsigned char。返回值非0表示分类成立，不保证一定等于1。",true),
  new KnowledgeTopic("switch","switch、case 与 break",@"\bswitch\b","switch 按整数或字符值匹配 case 标签。case 结尾通常需要 break，否则会继续执行后面的分支；default 处理其余情况。它不能直接按字符串内容选择分支，字符串比较应使用 strcmp。"),
  new KnowledgeTopic("for","for 循环、循环变量与重复次数",@"\bfor\s*\(","for(初始化;继续条件;每轮更新) 在每次执行循环体前检查条件。i++ 先取得旧值再自增，++i 先自增；单独作为更新语句时效果相同。不要在同一个表达式中多次修改同一变量。循环边界决定是否多算或漏算一项。"),
  new KnowledgeTopic("while","while 与按条件重复",@"\bwhile\s*\(","while(条件) 在每轮开始前检查，第一次条件不成立时循环体一次也不执行。循环体必须推进状态，否则可能死循环。读取或分解数字、反复取余等问题适合 while。"),
  new KnowledgeTopic("do","do / while 至少执行一次",@"\bdo\b","do { ... } while(条件); 先执行再检查，因此至少执行一次。最后的分号不可遗漏。输入0时也必须处理一次的数字统计等场景，常可用这种结构；使用普通 while 也可以，但要单独处理0。"),
  new KnowledgeTopic("compound","复合赋值与累积状态",@"\+=|-=|\*=|/=|%=","+=、-=、*=、/=、%= 将运算结果赋回左侧，例如 sum += value。求和从0开始，连乘从1开始。每次循环更新的变量都应有明确含义，保留仍需使用的旧值再更新。"),
  new KnowledgeTopic("long","long long 与 %lld",@"\blong\s+long\b","long long 至少64位，适合阶乘、较大乘积和累积和。scanf/printf 的对应格式为 %lld。仍要在中间运算之前扩展类型；它也有上限，并不能容纳任意位数的大整数。"),
  new KnowledgeTopic("break","break / continue 与提前结束",@"\bbreak\b|\bcontinue\b","break 退出当前最内层循环或 switch；continue 跳过当前循环剩余语句，进入下一轮。查找首次位置时可在找到后结束；嵌套循环中的 break 不会自动退出外层。"),
  new KnowledgeTopic("arrays","一维数组、下标与初始化",@"\b(?:int|char|double|long)\s+\w+\s*\[","类型 a[N] 为N个连续元素分配空间，下标为0～N-1。a[N] 已越界。{0} 可把所有元素初始化为0；没有初始化的局部数组内容不可依赖。按题目上界分配容量，读取和输出使用实际长度。"),
  new KnowledgeTopic("bits","位运算与二进制",@"<<|>>|(?<=[\w\]])\s*&\s*(?=[\w(])|\^","& 按位与、| 按位或、^ 按位异或，<< 和 >> 是移位；它们不同于 &&、||。例如非负整数 x&1 检查最低位。移位位数必须在类型位宽内，带符号数移位和溢出需要格外谨慎。"),
  new KnowledgeTopic("string","字符串、结束符与长度",@"\bstrlen\s*\(|%\d*s","C 字符串以 '\\0' 结束，缓冲区至少比有效字符多一个位置。scanf 的 %Ns 将最大读取长度限制为N，遇空白停止。strlen 来自 string.h，返回不包含结束符的长度，返回类型是 size_t。",true),
  new KnowledgeTopic("fgets","fgets 读整行与残留换行",@"\bfgets\s*\(","fgets(buffer,capacity,stdin) 最多读取 capacity-1 个字符并补结束符；成功时可能把换行也读入。连续空格和空行不能用 %s 代替读取。混用 scanf 与 fgets 时，前一次输入遗留的换行可能先被读走，应明确处理。",true),
  new KnowledgeTopic("matrix","二维数组与行列坐标",@"\b\w+\s*\[[^\]\r\n]+\]\s*\[","二维数组 a[R][C] 有行、列两个下标，合法范围分别是0～R-1、0～C-1。按行连续存储，a[i][j] 的 i 和 j 各有独立职责。嵌套循环要检查内层起点是否每次正确重置。"),
  new KnowledgeTopic("pointer","指针、解引用与地址传参",@"\b(?:int|char|double|void)\s*\*|,\s*\*\s*\w","T *p 保存T对象地址，&x 取地址，*p 读写所指对象。函数参数默认按值传递；若要修改调用者变量，可传入其地址。p+i 以元素大小为单位移动，只能在同一数组的允许范围内使用。NULL 不能解引用。"),
  new KnowledgeTopic("function","自定义函数、原型与返回值",@"\b(?:void|int|long\s+long|double)\s+(?!main\b)\w+\s*\(","函数由返回类型、名称、参数列表和函数体组成；调用前需要可见的声明或定义。void 返回类型表示无返回值。形参是调用实参的副本，但通过指针可访问调用者的对象。不要返回已经离开作用域的局部变量地址。"),
  new KnowledgeTopic("recursion","递归与终止条件",@"\b(fact|gcd)\s*\(","递归是在函数中再次调用自身。必须先写可直接返回的基本情况，并让每次调用都更接近它。调用栈会保存尚未结束的各层局部状态，过深递归会消耗栈空间。阶乘和欧几里得算法能用递归，也能改写为循环。"),
  new KnowledgeTopic("strcmp","字符串内容比较",@"\b(strcmp|strncmp)\s*\(","strcmp 比较两个以零结尾的字符串，strncmp 最多比较指定字符数；返回负、零或正，零表示相同。两个数组名或指针直接用 == 比较的是地址，不能代替内容比较。指定长度时仍应保证可以安全访问对应内容。",true),
  new KnowledgeTopic("struct","struct、typedef 与字段访问",@"\bstruct\b|\btypedef\b","struct 把相关字段组成一条记录；object.field 访问字段，pointer->field 访问指针所指记录的字段。typedef 可提供类型别名。结构体赋值复制字段值，包含指针时不会自动复制指针所指的动态内存。"),
  new KnowledgeTopic("memory","malloc / free、sizeof 与链表",@"\b(malloc|calloc|free)\s*\(","malloc 分配指定字节，失败返回 NULL；calloc 还会清零。sizeof(*p) 可得到所指对象的大小。使用前检查分配是否成功，结束时释放每个分配对象。释放链表节点之前先保存 next，free 后不能再访问该节点。",true),
  new KnowledgeTopic("sizeof","sizeof 的对象大小",@"\bsizeof\s*\(","sizeof 返回对象或类型占用的字节数，结果类型为 size_t。数组的 sizeof 可得到整个数组大小，但函数参数中的数组已转为指针，sizeof(参数) 不能得到原数组长度。",true),
  new KnowledgeTopic("static","static、全局对象与存储期",@"\bstatic\b","全局对象和 static 对象有静态存储期，默认零初始化并存活到程序结束。普通局部对象在离开作用域后失效。static 局部变量在多次函数调用之间保留值，设计多组输入时要确认何时重置。"),
  new KnowledgeTopic("sortlib","qsort 与比较回调",@"\bqsort\s*\(","qsort 来自 stdlib.h，参数包含数组、元素个数、单个元素字节数与比较函数。比较函数接收 const void*，按正确类型读取后返回负、零或正；不应改写被比较的对象。不要直接相减可能溢出的两个整数作为比较结果。",true),
  new KnowledgeTopic("copy","memcpy / memset 与字节操作",@"\b(memcpy|memset)\s*\(","memcpy 按字节复制，要求源、目标区域不重叠；有重叠时使用 memmove。memset 按字节重复写入，不能把它当作给 int 数组填入任意整数值的操作。字符串复制必须为结束符保留容量。",true)
 };}
 static List<PracticeLesson> Build(){var topics=Topics();var lessons=new List<PracticeLesson>();foreach(var p in Bank.All().OrderBy(p=>p.Id)){var lesson=new PracticeLesson{Problem=p,Goal=Goals[p.Id-1]};string clean=Clean(p.Reference);foreach(var t in topics){if(!Regex.IsMatch(t.Raw?p.Reference:clean,t.Pattern))continue;if(t.FirstProblem==0){t.FirstProblem=p.Id;lesson.New.Add(t);}else lesson.Review.Add(t);}lessons.Add(lesson);}return lessons;}
 // The wording describes concepts and pitfalls, without including a full solution.
 static readonly string[] Goals={
  "分清输入、计算、输出三个阶段，认识标准输入输出及整数变量。","比较加减法的顺序，理解负数也可直接按整数输出。","练习乘法表达式，先估算输入上界对应的结果范围。","区分整数商与余数，确认除数必须非零。","理解整数除法会截断，精度要在计算阶段保留。","用明确的数学表达式完成单位换算，注意乘除先后。","认识常量与浮点输出，理解面积公式中的乘法。","通过临时变量保存旧值，避免交换时覆盖仍需使用的数据。","理解正、负、零的区分，避免对整型最小值直接取负。","拆解坐标差与平方，利用括号保持计算含义。",
  "通过比较作出选择，区分赋值与相等比较。","理解多个候选值的逐步比较与并列情况。","用取余识别奇偶，注意负奇数的余数不一定是1。","设计互斥条件，把零作为独立情况。","综合 && 与 ||，理解闰年条件中的优先级。","按从高到低或从低到高的统一顺序划分成绩区间。","先判断能否成三角形，再分类；不要遗漏非法边长组合。","区分字符与数值，认识分类函数和字符范围。","根据运算符选择计算，除法之前处理除零。","按月份分类并处理闰年的二月，关注合法下标。",
  "定义累加器的初值与循环边界，理解空范围时的结果。","从1开始连乘，认识64位整数以及0阶乘的约定。","更新状态前保留所需旧值，区分第0项与第1项。","通过取余与整数除法分解十进制数字。","理解每读入一位数字时，已有结果的位权如何变化。","理解试除上界，单独处理0、1与较小素数。","在循环中不断缩小问题，理解欧几里得算法的终止条件。","结合最大公约数，先除后乘以降低中间结果范围。","理解0次幂与连乘初值，区分幂运算与异或符号。","认识至少执行一次的循环，避免输入0时漏计。",
  "把“判断一个数”组织为可重复使用的过程，理解嵌套循环。","通过步长或条件选取目标项，检查上界是否包含。","用循环产生一组结果，统一处理分隔空格。","区分真因子与自身，检查1的特殊情况。","分阶段统计位数与计算幂和，保留原始输入供比较。","利用因子成对出现的性质，平方根不能重复计数。","理解累加结果和中间乘积的范围，必要时在乘法前扩展数值类型。","保存多个待输出的余数，认识数组与逆序遍历。","认识二进制位的统计，可用取余或位运算表达。","比较原值与反转值，分清数字表示和字符串表示。",
  "数组容量与实际输入长度不同，循环只访问有效元素。","极值初始化应来自有效输入，不能默认0是最大值。","理解全正、全负与单元素数组对极值算法的影响。","从最后一个合法下标开始，注意逆序循环的终止条件。","比较相邻或候选元素，理解排序保留重复项。","用标记值表示“未找到”，首次找到后应避免被后续匹配覆盖。","把比较条件转化为计数事件，留意等号。","识别重复出现与首次出现，理解嵌套检查的成本。","用取余将移动量归约，注意0基下标和循环索引。","理解前缀状态：每个位置保存从起点到当前位置的累计信息。",
  "认识字符串结束符，容量必须给结束符留空间。","字符串有效长度不包括结束符，逆序时不要输出结束符。","使用字符集合或大小写归一化进行分类，避免ASCII值与数值混淆。","从两端向中间比较，理解区分大小写的要求。","用字符常量比较数字字符，并正确初始化计数。","理解大小写转换只作用于字母，其他字符要保留。","读整行以保留空格，使用状态变化而不是空格数量统计单词。","逐字符修改数组，匹配条件必须区分大小写。","读取字符时处理输入中的空白，区分字符与字符串格式。","认识二维坐标与主对角线关系，即使只累计部分位置也要读完整输入。",
  "用地址传参修改调用者变量，分清指针本身与所指的值。","数组与指针遍历可表达同一段连续数据，指针边界同样需要检查。","递归必须有基本情况，并令每次调用更接近基本情况。","将欧几里得算法写成递归，理解参数值如何逐层变化。","为二分维护有序区间，不存在目标时仍须检查候选位置。","两个有序序列各有独立下标，某一侧耗尽后仍需处理另一侧。","利用有序性比较相邻值，输出次数与读取次数不一定相同。","使用计数槽把值映射为下标，并按规定处理并列。","分清“第二个元素”和“第二大不同值”，设计不存在答案时的状态。","记录以当前位置结尾的最优值，全负时也不能选择空段。",
  "窗口移动时更新进入与离开的元素，注意窗口数与数组长度的关系。","用两个前缀状态表达闭区间，统一0基或1基坐标。","交换行列坐标，检查非方阵的输出尺寸。","区分矩阵乘法与逐元素乘法，内部维度必须一致。","理解二维数组参数与行列式的运算顺序。","连续子串匹配需要逐位置检查，重叠匹配不能跳过。","识别一段连续相同字符的起止位置，输出完整次数。","用栈维护最近未配对的左括号，空栈与结束后未清空都是错误情况。","用字符串保存超出整型范围的十进制数，理解逐位进位。","用结构体关联编号与分数，排序时实现多级比较条件。",
  "认识动态内存、链表节点与next指针，释放前先保存下一节点。","通过命令操作栈，先检查是否为空再访问栈顶。","分清队首与队尾，理解出队与存储空间的关系。","定义动态规划状态，障碍处必须归零，边界与起点初始化要一致。","最少数量问题需要不可达标记，不能把所有状态初值设为0。","区分严格递增与非递减，子序列允许跳过元素。","等权网格按层扩展距离，访问标记应在入队时设置。","把命令解析、记录更新与查询排序分开；失败操作不能破坏已有状态。","对订单先检查库存是否足够，再一致地更新库存与金额，区分接受和拒绝。","用用户、图书两个维度保存借还关系，理解库存统计与业务约束的不变量。"
 };
}

sealed class KnowledgeWindow : Window {
 public readonly PracticeLesson Lesson;
 public KnowledgeWindow(Problem p){Lesson=PracticeKnowledge.For(p.Id);Title="知识点学习 · "+p.Id.ToString("00")+" "+p.Title;Width=900;Height=730;MinWidth=640;MinHeight=480;WindowStartupLocation=WindowStartupLocation.CenterOwner;ShowInTaskbar=false;Theme.Window(this);
  var root=new Grid();root.RowDefinitions.Add(new RowDefinition{Height=GridLength.Auto});root.RowDefinitions.Add(new RowDefinition{Height=new GridLength(1,GridUnitType.Star)});root.RowDefinitions.Add(new RowDefinition{Height=GridLength.Auto});Content=new Border{Background=Theme.Bg,Padding=new Thickness(22),Child=root};
  var header=new StackPanel{Margin=new Thickness(0,0,0,14)};header.Children.Add(Theme.Text(p.Id.ToString("00")+" · "+p.Title,21,Theme.Blue,true));var intro=Theme.Text("按题号递进学习 · 首次出现按题号判定，可随时回顾相关基础",11,Theme.Muted);intro.Margin=new Thickness(0,6,0,0);header.Children.Add(intro);root.Children.Add(header);
  var tabs=new TabControl{Background=Theme.White,BorderBrush=Theme.Line};Grid.SetRow(tabs,1);root.Children.Add(tabs);
  var study=new StackPanel{Margin=new Thickness(18)};study.Children.Add(Theme.Text("本题学习目标",15,Theme.Blue,true));var goal=Theme.Text(Lesson.Goal,14);goal.Margin=new Thickness(0,10,0,20);study.Children.Add(goal);study.Children.Add(Theme.Text("本题首次出现的语法 / 用法",15,Theme.Blue,true));
  if(Lesson.New.Count==0){var text=Theme.Text("本题没有新增语法，重点是组合运用已有知识。右侧“相关基础回顾”可补看涉及的语法。",13,Theme.Muted);text.Margin=new Thickness(0,12,0,0);study.Children.Add(text);}foreach(var topic in Lesson.New)AddTopic(study,topic,false);tabs.Items.Add(new TabItem{Header="本题学习",Content=Scroll(study)});
  var review=new StackPanel{Margin=new Thickness(18)};review.Children.Add(Theme.Text("本题涉及的已有知识",15,Theme.Blue,true));foreach(var topic in Lesson.Review)AddTopic(review,topic,true);if(Lesson.Review.Count==0)review.Children.Add(Theme.Text("这是学习起点；请先阅读“本题学习”中的入门讲解。",13,Theme.Muted));tabs.Items.Add(new TabItem{Header="相关基础回顾",Content=Scroll(review)});
  var footer=new DockPanel{Margin=new Thickness(0,16,0,0)};var close=Theme.Button("返回练习",true);close.IsCancel=true;close.IsDefault=true;close.Click+=(s,e)=>Close();DockPanel.SetDock(close,Dock.Right);footer.Children.Add(close);footer.Children.Add(Theme.Text(Branding.Attribution,10.5,Theme.Muted));Grid.SetRow(footer,2);root.Children.Add(footer);
 }
 static ScrollViewer Scroll(UIElement child){return new ScrollViewer{Content=child,VerticalScrollBarVisibility=ScrollBarVisibility.Auto,HorizontalScrollBarVisibility=ScrollBarVisibility.Disabled};}
 static void AddTopic(StackPanel panel,KnowledgeTopic topic,bool review){var content=new StackPanel();content.Children.Add(Theme.Text(topic.Title+(review?"  · 首见第 "+topic.FirstProblem.ToString("00")+" 题":""),14,Theme.Ink,true));var text=Theme.Text(topic.Explanation,13);text.Margin=new Thickness(0,8,0,0);content.Children.Add(text);var card=Theme.Card(content,new Thickness(14));card.Margin=new Thickness(0,12,0,0);panel.Children.Add(card);}
}
