using System.Collections.Generic;
using System.Diagnostics;

namespace uDust
{
  public class Lexer
  {
    public static List<Token> Lex(string code)
    {
      List<Token> tokens = new();
      int i = 0;
      int line = 0;

      while (i < code.Length)
      {
        char c = code[i];
        TokenKind kind;

        switch (c)
        {
          case '+':
            kind = TokenKind.Plus;
            break;
          case '-':
            kind = TokenKind.Minus;
            break;
          case '*':
            kind = TokenKind.Star;
            break;
          case '/':
            kind = TokenKind.Slash;
            break;
          case ';':
            kind = TokenKind.Semicolon;
            break;
          case '\n':
            line++;
            tokens.Add(new Token("\\n", TokenKind.EndOfLine, new TextRange((line - 1)..(line - 1), i..(i + 1))));
            i++;
            continue;
          default:
            if (char.IsDigit(c))
            {
              int start = i;

              kind = TokenKind.IntegerLiteral;

              while (i < code.Length)
              {
                if (code[i] == '.')
                {
                  kind = TokenKind.DecimalLiteral;
                }
                else if (!char.IsDigit(code[i]))
                {
                  break;
                }

                i++;
              }

              tokens.Add(new Token(code.Substring(start, i - start), kind, new TextRange(line..line, start..i)));

              continue;
            }

            if (c is '"' or '\'')
            {
              int start = i;
              bool foundEnd = false;

              i++;

              while (i < code.Length)
              {
                if (code[i] is '"' or '\'')
                {
                  foundEnd = true;

                  i++;
                  break;
                }

                i++;
              }

              Debug.Assert(foundEnd, "Unterminated string errors are not handled yet");

              tokens.Add(new Token(code.Substring(start, i - start), TokenKind.StringLiteral, new TextRange(line..line, start..i)));

              continue;
            }

            i++;
            continue;
        }

        tokens.Add(new Token(c.ToString(), kind, new TextRange(line..line, i..(i + 1))));

        i++;
      }

      tokens.Add(new Token("", TokenKind.EndOfFile, new TextRange(line..line, i..i)));

      return tokens;
    }
  }
}