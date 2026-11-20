using System;
using System.Collections.Generic;
using System.Windows.Input;

// Shared by ordinary practice and dark challenges. Only committed typing is handled;
// programmatic imports and paste are left to AvalonEdit unchanged.
static class PairCompletion {
 static char Close(char c){switch(c){case '(':return ')';case '[':return ']';case '{':return '}';case '"':return '"';case '\'':return '\'';case '（':return '）';case '［':return '］';case '｛':return '｝';case '“':return '”';case '‘':return '’';case '【':return '】';case '「':return '」';case '『':return '』';default:return '\0';}}
 static bool Quote(char c){return c=='"'||c=='\''||c=='“'||c=='‘'||c=='「'||c=='『';}
 sealed class Context {public bool Comment,Escape;public char Quote;public Stack<char> Brackets=new Stack<char>();}
 static Context Scan(string text,int end){var c=new Context();bool line=false,block=false;for(int i=0;i<end;i++){char ch=text[i],next=i+1<end?text[i+1]:'\0';if(line){if(ch=='\n'||ch=='\r')line=false;continue;}if(block){if(ch=='*'&&next=='/'){block=false;i++;}continue;}if(c.Quote!='\0'){if(c.Escape){c.Escape=false;continue;}if(ch=='\\'){c.Escape=true;continue;}if(ch==Close(c.Quote))c.Quote='\0';continue;}if(ch=='/'&&next=='/'){line=true;i++;continue;}if(ch=='/'&&next=='*'){block=true;i++;continue;}if(Quote(ch)){c.Quote=ch;continue;}if(Close(ch)!='\0')c.Brackets.Push(ch);else if(c.Brackets.Count>0&&Close(c.Brackets.Peek())==ch)c.Brackets.Pop();}c.Comment=line||block;return c;}
 public static void Attach(EditorInput input){input.TextArea.TextEntering+=(s,e)=>{if(Apply(input,e.Text))e.Handled=true;};input.PreviewKeyDown+=(s,e)=>{if(e.Key==Key.Back&&Keyboard.Modifiers==ModifierKeys.None&&Backspace(input))e.Handled=true;};}
 public static bool Apply(EditorInput input,string text){
  if(input.IsReadOnly||!input.IsEnabled||text==null||text.Length!=1)return false;
  char typed=text[0],closing=Close(typed);int start=input.SelectionStart,length=input.SelectionLength;
  var context=Scan(input.Text,start);if(context.Comment)return false;
  bool atSame=length==0&&start<input.Document.TextLength&&input.Document.GetCharAt(start)==typed;
  if(atSame&&((context.Quote!='\0'&&!context.Escape&&Close(context.Quote)==typed)||(context.Quote=='\0'&&context.Brackets.Count>0&&Close(context.Brackets.Peek())==typed))){input.Select(start+1,0);return true;}
  if(closing=='\0'||context.Quote!='\0')return false;
  // Do not create another quoted literal in the middle of an identifier.
  if(Quote(typed)&&length==0&&((start>0&&(char.IsLetterOrDigit(input.Text[start-1])||input.Text[start-1]=='_'||input.Text[start-1]=='\\'))||(start<input.Text.Length&&(char.IsLetterOrDigit(input.Text[start])||input.Text[start]=='_'))))return false;
  string selected=input.SelectedText;
  using(input.Document.RunUpdate()){input.Document.Replace(start,length,typed+selected+closing);input.Select(start+1,length);}
  return true;
 }
 public static bool Backspace(EditorInput input){if(input.IsReadOnly||!input.IsEnabled||input.SelectionLength!=0)return false;int at=input.CaretOffset;if(at==0||at>=input.Document.TextLength)return false;char open=input.Document.GetCharAt(at-1);if(Close(open)=='\0'||Close(open)!=input.Document.GetCharAt(at))return false;var context=Scan(input.Text,at-1);if(context.Comment||context.Quote!='\0')return false;using(input.Document.RunUpdate()){input.Document.Remove(at-1,2);input.Select(at-1,0);}return true;}
}
