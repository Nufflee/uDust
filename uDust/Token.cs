using System;

namespace uDust
{
  public enum TokenKind
  {
    Plus,
    Minus,
    Star,
    Slash,
    Semicolon,
    EndOfLine,
    EndOfFile,
    IntegerLiteral,
    DecimalLiteral,
    StringLiteral
  }

  public class TextRange
  {
    public Range Line { get; }
    public Range Column { get; }

    public TextRange(Range line, Range column)
    {
      Line = line;
      Column = column;
    }

    public static bool operator ==(TextRange left, TextRange right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(TextRange left, TextRange right)
    {
      return !Equals(left, right);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((TextRange) obj);
    }

    protected bool Equals(TextRange other)
    {
      return Line.Equals(other.Line) && Column.Equals(other.Column);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(Line, Column);
    }

    public override string ToString()
    {
      return $"(Lines: {Line}, Columns: {Column})";
    }
  }

  public class Token
  {
    public string Text { get; }
    public TokenKind Kind { get; }
    public TextRange Range { get; }

    public Token(string text, TokenKind kind, TextRange range)
    {
      Text = text;
      Kind = kind;
      Range = range;
    }

    public static bool operator ==(Token left, Token right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(Token left, Token right)
    {
      return !Equals(left, right);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((Token) obj);
    }

    protected bool Equals(Token other)
    {
      return Text == other.Text && Kind == other.Kind && Equals(Range, other.Range);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(Text, (int) Kind, Range);
    }
  }
}