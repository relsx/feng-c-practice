using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Rendering;
using System.Xml;
using System.IO;
using System.Windows.Input;
sealed class EditorInput : TextEditor {
 public event EventHandler SelectionChanged;
 public EditorInput(){TextArea.SelectionChanged+=(s,e)=>{if(SelectionChanged!=null)SelectionChanged(this,e);};TextArea.Caret.PositionChanged+=(s,e)=>{if(SelectionChanged!=null)SelectionChanged(this,e);};}
 public int CaretIndex{get{return CaretOffset;}set{CaretOffset=value;}}
 public int GetLineIndexFromCharacterIndex(int i){return Document.GetLineByOffset(Math.Max(0,Math.Min(i,Document.TextLength))).LineNumber-1;}
 public int GetCharacterIndexFromLineIndex(int i){return Document.GetLineByNumber(i+1).Offset;}
 public Rect CharacterRect(int index){var view=TextArea.TextView;view.EnsureVisualLines();var loc=Document.GetLocation(index);var p=new TextViewPosition(loc);Point top=view.GetVisualPosition(p,VisualYPosition.LineTop)-view.ScrollOffset,bottom=view.GetVisualPosition(p,VisualYPosition.LineBottom)-view.ScrollOffset;top=view.TranslatePoint(top,this);bottom=view.TranslatePoint(bottom,this);return new Rect(top,new Size(1,Math.Max(1,bottom.Y-top.Y)));}
 public int Hit(Point p){Point v=TranslatePoint(p,TextArea.TextView);var pos=TextArea.TextView.GetPosition(v+TextArea.TextView.ScrollOffset);return pos.HasValue?Document.GetOffset(pos.Value.Location):-1;}
}
sealed class CodeEditor : Grid {
 public EditorInput Input{get;private set;}
 public event EventHandler CodeChanged;
 Button undoButton,redoButton;
 public bool CanUndo{get{return !Input.IsReadOnly&&Input.Document.UndoStack.CanUndo;}}
 public bool CanRedo{get{return !Input.IsReadOnly&&Input.Document.UndoStack.CanRedo;}}
 public void Undo(){if(CanUndo)Input.Undo();UpdateHistory();Input.Focus();}
 public void Redo(){if(CanRedo)Input.Redo();UpdateHistory();Input.Focus();}
 public void AttachHistory(Button undo,Button redo){undoButton=undo;redoButton=redo;undo.Click+=(s,e)=>Undo();redo.Click+=(s,e)=>Redo();Input.Document.UndoStack.PropertyChanged+=(s,e)=>UpdateHistory();Input.IsEnabledChanged+=(s,e)=>UpdateHistory();UpdateHistory();}
 public void UpdateHistory(){if(undoButton!=null)undoButton.IsEnabled=CanUndo;if(redoButton!=null)redoButton.IsEnabled=CanRedo;}
 public bool HandleHistory(KeyEventArgs e){if((Keyboard.Modifiers&ModifierKeys.Control)==0)return false;if(e.Key==Key.Z){e.Handled=true;if((Keyboard.Modifiers&ModifierKeys.Shift)!=0)Redo();else Undo();return true;}if(e.Key==Key.Y){e.Handled=true;Redo();return true;}return false;}
 public int InputChangeCount{get;private set;}
 public string Text{get{return Input.Text;}set{Input.Text=value;}}
 public CodeEditor(){Background=Theme.White;Input=new EditorInput{FontFamily=new FontFamily("Consolas"),FontSize=16,Foreground=Theme.Ink,Background=Theme.White,ShowLineNumbers=true,LineNumbersForeground=Theme.B("#8893B4"),WordWrap=false,HorizontalScrollBarVisibility=ScrollBarVisibility.Auto,VerticalScrollBarVisibility=ScrollBarVisibility.Auto,Padding=new Thickness(5),Margin=new Thickness(5,8,8,8)};
  Input.Options.ConvertTabsToSpaces=true;Input.Options.IndentationSize=4;Input.Options.EnableHyperlinks=false;Input.Options.EnableEmailHyperlinks=false;
  string xshd=@"<SyntaxDefinition name='C' xmlns='http://icsharpcode.net/sharpdevelop/syntaxdefinition/2008'><Color name='Keyword' foreground='#7045BE'/><Color name='Comment' foreground='#43877C'/><Color name='String' foreground='#AB5A32'/><Color name='Number' foreground='#246BBB'/><Color name='Preprocessor' foreground='#9560AD'/><RuleSet><Span color='Comment' begin='//'/><Span color='Comment' multiline='true' begin='/\*' end='\*/'/><Span color='String' begin='&quot;' end='&quot;'><RuleSet><Rule>\\.</Rule></RuleSet></Span><Span color='String' begin='&apos;' end='&apos;'><RuleSet><Rule>\\.</Rule></RuleSet></Span><Rule color='Preprocessor'>^\s*\#.*</Rule><Keywords color='Keyword'><Word>auto</Word><Word>break</Word><Word>case</Word><Word>char</Word><Word>const</Word><Word>continue</Word><Word>default</Word><Word>do</Word><Word>double</Word><Word>else</Word><Word>enum</Word><Word>extern</Word><Word>float</Word><Word>for</Word><Word>goto</Word><Word>if</Word><Word>inline</Word><Word>int</Word><Word>long</Word><Word>register</Word><Word>restrict</Word><Word>return</Word><Word>short</Word><Word>signed</Word><Word>sizeof</Word><Word>static</Word><Word>struct</Word><Word>switch</Word><Word>typedef</Word><Word>union</Word><Word>unsigned</Word><Word>void</Word><Word>volatile</Word><Word>while</Word><Word>_Bool</Word><Word>_Complex</Word><Word>_Imaginary</Word></Keywords><Rule color='Number'>\b(0[xX][0-9a-fA-F]+|\d+(\.\d+)?([eE][+-]?\d+)?)[uUlLfF]*\b</Rule></RuleSet></SyntaxDefinition>";
  using(var reader=XmlReader.Create(new StringReader(xshd)))Input.SyntaxHighlighting=ICSharpCode.AvalonEdit.Highlighting.Xshd.HighlightingLoader.Load(reader,HighlightingManager.Instance);
  Children.Add(Input);Input.TextChanged+=(s,e)=>{InputChangeCount++;if(CodeChanged!=null)CodeChanged(this,EventArgs.Empty);};}
 public void SetFontSize(double size){if(size<8||size>48)throw new ArgumentOutOfRangeException("size");Input.FontSize=size;RefreshView();}
 public void RefreshView(){Input.UpdateLayout();Input.TextArea.TextView.Redraw();}
 public void SetDark(){Background=Dark.Panel;Input.Background=Dark.Panel;Input.Foreground=Dark.Ink;Input.LineNumbersForeground=Dark.Muted;Input.TextArea.SelectionBrush=Theme.B("#526487");foreach(var pair in new[]{new[]{"Keyword","#94BAFF"},new[]{"Comment","#70B19F"},new[]{"String","#ECB78B"},new[]{"Number","#95DFE4"},new[]{"Preprocessor","#D5A9F4"}}){var color=Input.SyntaxHighlighting.GetNamedColor(pair[0]);if(color!=null)color.Foreground=new SimpleHighlightingBrush((Color)ColorConverter.ConvertFromString(pair[1]));}RefreshView();}
}
