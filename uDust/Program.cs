using System;
using System.Collections.Generic;

namespace uDust
{
  public static class Program
  {
    private static void Main(string[] args)
    {
      const string code = @"1 * 2 + 3 + 4 / 6;";
      
      // ((1 * 2) + 3) + (4 / 5)
      
      List<Token> tokens = Lexer.Lex(code);

      Console.WriteLine("Code: " + code);

      foreach (Token token in tokens)
      {
        Console.WriteLine($"({token.Range.Line.Start}, {token.Range.Column.Start}:{token.Range.Column.End}): {token.Kind} `{token.Text.Replace("\n", "\\n")}`");
      }

      RootNode root = new Parser().Parse(tokens);

      Console.WriteLine();
      Console.WriteLine($"// {code}");
      Console.WriteLine(root.DumpToGraphViz());
    }
  }
}