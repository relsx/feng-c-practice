using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
class Problem {
 public int Id; public string Title,Category,Statement,Hint,Reference;
 public Problem(int id,string t,string c,string s,string h,string code){Id=id;Title=t;Category=c;Statement=s;Hint=h;Reference=code;}
 public override string ToString(){return Id.ToString("00")+"  "+Title;}
}
class TestCase {public string Input,Expected; public bool Edge; public TestCase(string i,string o,bool e){Input=i;Expected=o;Edge=e;}}
static partial class Bank {
 static int V(Random r,int e,int lo,int hi,params int[] values){return e>=0?values[e%values.Length]:r.Next(lo,hi+1);}
 static bool Prime(int n){if(n<2)return false;for(int i=2;i*i<=n;i++)if(n%i==0)return false;return true;}
 static int Gcd(int a,int b){while(b!=0){int t=a%b;a=b;b=t;}return a;}
 static string F(double x){return x.ToString("F8",CultureInfo.InvariantCulture);}
 static string Join(IEnumerable<int> a){return string.Join(" ",a.Select(x=>x.ToString()).ToArray());}
 public static TestCase Generate(int id,Random r,int e){
  if(id>60)return Advanced(id,r,e);
  string input="",output=""; int a=0,b=0,c=0,n=0; long z=0;
  if(id<=4||id==8||id==11||id==27||id==28){
   int lo=id==4||id==27||id==28?0:-10000,hi=id==3?1000:10000;if(id==3)lo=-1000;
   a=V(r,e,lo,hi,0,lo,hi,1,42,0,7,13);b=V(r,e,id==4?1:lo,id==4?100:hi,id==4?1:0,id==4?100:hi,id==4?3:lo,1,42,7,id==4?100:0,13);
   input=a+" "+b+"\n";
   switch(id){case 1:z=a+b;break;case 2:z=a-b;break;case 3:z=a*b;break;case 4:output=a/b+" "+a%b;break;case 8:output=b+" "+a;break;case 11:z=Math.Max(a,b);break;case 27:z=Gcd(a,b);break;case 28:z=a==0||b==0?0:a/Gcd(a,b)*b;break;}
   if(output=="")output=z.ToString();
  } else if(id==5||id==12||id==17){
   int lo=id==17?1:id==5?-1000:-10000,hi=id==17?100:id==5?1000:10000;
   a=V(r,e,lo,hi,lo,hi,3,5,2,3,1,7);b=V(r,e,lo,hi,lo,hi,4,5,3,3,2,7);c=V(r,e,lo,hi,lo,hi,5,5,5,7,3,9);
   input=a+" "+b+" "+c+"\n";
   output=id==5?F((a+b+c)/3.0):id==12?Math.Max(a,Math.Max(b,c)).ToString():a+b<=c||a+c<=b||b+c<=a?"INVALID":a==b&&b==c?"EQUILATERAL":a==b||b==c||a==c?"ISOSCELES":"SCALENE";
  } else if(id==6||id==7||id==9||id==13||id==14){
   int lo=id==7?0:id==6?-100:-100000,hi=id==7||id==6?100:100000;
   n=V(r,e,lo,hi,0,lo,hi,1,id==7?3:-1,42,id==7?99:-99,7);input=n+"\n";
   output=id==6?F(n*9.0/5+32):id==7?F(Math.PI*n*n):id==9?Math.Abs(n).ToString():id==13?(n%2==0?"EVEN":"ODD"):n>0?"POSITIVE":n<0?"NEGATIVE":"ZERO";
  } else if(id==10){
   a=V(r,e,-100,100,0,-100,100,1,0,2,3,5);b=V(r,e,-100,100,0,-100,100,1,7,2,3,5);c=V(r,e,-100,100,0,100,-100,1,0,5,3,8);n=V(r,e,-100,100,0,100,-100,0,-7,6,3,9);
   input=a+" "+b+" "+c+" "+n+"\n";output=((a-c)*(a-c)+(b-n)*(b-n)).ToString();
  } else if(id==15||id==20){
   a=V(r,e,1,9999,2000,1900,2024,2023,1,9999,2400,2100);b=V(r,e,1,12,2,2,2,2,1,12,4,11);input=a+(id==20?" "+b:"")+"\n";
   bool leap=a%400==0||(a%4==0&&a%100!=0);int[] ds={31,28,31,30,31,30,31,31,30,31,30,31};output=id==15?(leap?"YES":"NO"):(b==2&&leap?29:ds[b-1]).ToString();
  } else if(id==16){n=V(r,e,0,100,0,59,60,69,70,79,80,90);input=n+"\n";output=n>=90?"A":n>=80?"B":n>=70?"C":n>=60?"D":"F";
  } else if(id==18){string chars="aZ09!@xY1z?5";char ch=e>=0?chars[e%chars.Length]:chars[r.Next(chars.Length)];input=ch+"\n";output=char.IsDigit(ch)?"DIGIT":char.IsLetter(ch)?"LETTER":"OTHER";
  } else if(id==19){a=V(r,e,-100,100,0,100,-100,7,-5,1,0,8);b=V(r,e,-100,100,0,-100,100,2,0,3,7,0);char op=e>=0?"+-*///+/"[e%8]:"+-*/"[r.Next(4)];input=a+" "+op+" "+b+"\n";output=op=='/'&&b==0?"ERROR":F(op=='+'?a+b:op=='-'?a-b:op=='*'?a*b:(double)a/b);
  } else if(id>=21&&id<=40){
   int[] max={10000,20,40,1000000000,100000000,100000,0,0,9,1000000000,2000,10000,9,10000,9999,100000,1000,65535,65535,100000000};
   int lo=id==33||id==34||id==36?1:0,hi=max[id-21];
   int[] edges={lo,hi,1,2,6,7,Math.Min(10,hi),lo};
   if(id==24||id==25||id==30||id==40)edges=new[]{0,hi,1,10,1200,10101,99999,12321};
   if(id==26)edges=new[]{0,1,2,3,4,97,99991,100000};
   if(id==34)edges=new[]{1,6,28,496,8128,12,10000,7};
   if(id==35)edges=new[]{0,1,153,370,371,407,1634,9474};
   if(id==36)edges=new[]{1,2,4,36,97,100000,99991,100};
   if(id==38||id==39)edges=new[]{0,1,2,3,255,256,65535,32768};
   n=V(r,e,lo,hi,edges);input=n+"\n";
   switch(id){
    case 21:z=(long)n*(n+1)/2;break;
    case 22:z=1;for(int i=1;i<=n;i++)z*=i;break;
    case 23:long x=0,y=1;for(int i=0;i<n;i++){long t=x+y;x=y;y=t;}z=x;break;
    case 24:foreach(char q in n.ToString())z+=q-'0';break;
    case 25:int t1=n;while(t1>0){z=z*10+t1%10;t1/=10;}break;
    case 26:output=Prime(n)?"YES":"NO";break;
    case 29:a=V(r,e,-10,10,0,-10,10,-1,0,2,-2,1);input=a+" "+n+"\n";z=1;for(int i=0;i<n;i++)z*=a;break;
    case 30:z=n.ToString().Length;break;
    case 31:for(int i=2;i<=n;i++)if(Prime(i))z++;break;
    case 32:for(int i=1;i<=n;i+=2)z+=i;break;
    case 33:output=Join(Enumerable.Range(1,9).Select(i=>i*n));break;
    case 34:for(int i=1;i<n;i++)if(n%i==0)z+=i;output=z==n?"YES":"NO";break;
    case 35:int k=n.ToString().Length;foreach(char q in n.ToString())z+=(long)Math.Pow(q-'0',k);output=z==n?"YES":"NO";break;
    case 36:for(int i=1;i*i<=n;i++)if(n%i==0)z+=i*i==n?1:2;break;
    case 37:for(int i=1;i<=n;i++)z+=(long)i*i;break;
    case 38:output=Convert.ToString(n,2);break;
    case 39:output=Convert.ToString(n,2).Count(q=>q=='1').ToString();break;
    case 40:output=n.ToString()==new string(n.ToString().Reverse().ToArray())?"YES":"NO";break;
   }
   if(output=="")output=z.ToString();
  } else if(id>=41&&id<=50){
   int[][] edgeArrays={new[]{0},new[]{-1000},new[]{1000},new[]{-9,-3,-5},new[]{7,7,7,7},new[]{3,1,3,0,-1},Enumerable.Range(1,50).Reverse().ToArray(),new[]{0,0,1,-1,0}};
   int[] ar=e>=0?(int[])edgeArrays[e%8].Clone():Enumerable.Range(0,r.Next(1,51)).Select(i=>r.Next(-20,21)).ToArray();
   n=ar.Length;a=V(r,e,-1000,1000,0,-1000,1000,-3,7,3,99,0);b=V(r,e,0,100,0,1,100,n,n+1,2,50,3);
   if(e<0&&id==46&&r.Next(2)==0)a=ar[r.Next(n)];
   input=n+"\n"+Join(ar)+"\n"+(id==46||id==47?a+"\n":id==49?b+"\n":"");
   switch(id){case 41:output=ar.Sum().ToString();break;case 42:output=ar.Max().ToString();break;case 43:output=ar.Min().ToString();break;case 44:output=Join(ar.Reverse());break;case 45:output=Join(ar.OrderBy(q=>q));break;case 46:output=Array.IndexOf(ar,a).ToString();break;case 47:output=ar.Count(q=>q>=a).ToString();break;case 48:output=ar.Distinct().Count().ToString();break;case 49:output=Join(Enumerable.Range(0,n).Select(i=>ar[(i+n-b%n)%n]));break;case 50:int sum=0;output=Join(ar.Select(q=>sum+=q));break;}
  } else if(id>=51&&id<=59){
   string alphabet="abcdeAEIOUxyzXYZ0123456789";
   string[] ss={"a","0",new string('a',80),"AaAa","abccba","0123456789","aEIoUxyz","abc123CBA"};
   string s=e>=0?ss[e%8]:new string(Enumerable.Range(0,r.Next(1,81)).Select(i=>alphabet[r.Next(alphabet.Length)]).ToArray());
   if(id==54&&e<0&&r.Next(2)==0){string half=s.Substring(0,s.Length/2);s=half+new string(half.Reverse().ToArray());if(s.Length==0)s="x";}
   char ca=e>=0?"a0azb9Aa"[e%8]:alphabet[r.Next(alphabet.Length)],cb=e>=0?"Z10zX0Ab"[e%8]:alphabet[r.Next(alphabet.Length)];
   if(id==57){string[] lines={""," ","   one   ","one two","a  b   c"," hello  world ",new string('a',80),"   "};s=e>=0?lines[e%8]:new string(Enumerable.Range(0,r.Next(0,81)).Select(i=>r.Next(3)==0?' ':alphabet[r.Next(13)]).ToArray());}
   input=s+"\n"+(id==58?ca+" "+cb+"\n":id==59?ca+"\n":"");
   switch(id){case 51:output=s.Length.ToString();break;case 52:output=new string(s.Reverse().ToArray());break;case 53:output=s.Count(q=>"aeiouAEIOU".Contains(q)).ToString();break;case 54:output=s==new string(s.Reverse().ToArray())?"YES":"NO";break;case 55:output=s.Count(q=>q>='0'&&q<='9').ToString();break;case 56:output=s.ToUpperInvariant();break;case 57:output=s.Split(new[]{' '},StringSplitOptions.RemoveEmptyEntries).Length.ToString();break;case 58:output=s.Replace(ca,cb);break;case 59:output=s.Count(q=>q==ca).ToString();break;}
  } else if(id==60){n=V(r,e,1,8,1,8,2,3,4,5,6,7);var sb=new StringBuilder(n+"\n");int sum=0;for(int i=0;i<n;i++){for(int j=0;j<n;j++){int v=e==0?0:e==1?-100:r.Next(-100,101);sb.Append(v).Append(' ');if(i==j)sum+=v;}sb.Append('\n');}input=sb.ToString();output=sum.ToString();
  } else throw new ArgumentException("Unknown problem");
  return new TestCase(input,output,e>=0);
 }
 public static List<TestCase> Tests(int id,int seed){var r=new Random(seed);var a=new List<TestCase>();for(int i=0;i<8;i++)a.Add(Generate(id,r,i));for(int i=0;i<20;i++)a.Add(Generate(id,r,-1));return a;}
}
