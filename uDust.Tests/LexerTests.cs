using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Xunit;

namespace uDust.Tests
{
  public class LexerTests
  {
    [Fact]
    public void TestSimpleArithmetic()
    {
      List<Token> expectedTokens = new()
      {
        new Token("69", TokenKind.IntegerLiteral, new TextRange(..0, 0..2)),
        new Token("+", TokenKind.Plus, new TextRange(..0, 3..4)),
        new Token("420", TokenKind.IntegerLiteral, new TextRange(..0, 5..8)),
      };

      Diff.Assert(expectedTokens, Lexer.Lex("69 + 420"));
    }

    [Fact]
    public void TestComplexArithmetic()
    {
      List<Token> expectedTokens = new()
      {
        new Token("69", TokenKind.IntegerLiteral, new TextRange(..0, 0..2)),
        new Token("+", TokenKind.Plus, new TextRange(..0, 3..4)),
        new Token("420", TokenKind.IntegerLiteral, new TextRange(..0, 5..8)),
        new Token("*", TokenKind.Star, new TextRange(..0, 9..10)),
        new Token("42069", TokenKind.IntegerLiteral, new TextRange(..0, 11..16)),
        new Token("/", TokenKind.Slash, new TextRange(..0, 17..18)),
        new Token("69420", TokenKind.IntegerLiteral, new TextRange(..0, 19..24)),
        new Token("-", TokenKind.Minus, new TextRange(..0, 25..26)),
        new Token("0", TokenKind.IntegerLiteral, new TextRange(..0, 27..28)),
      };

      Diff.Assert(expectedTokens, Lexer.Lex("69 + 420 * 42069 / 69420 - 0"));
    }
    
    [Fact]
    public void TestArithmeticWithSpaces()
    {
      List<Token> expectedTokens = new()
      {
        new Token("1", TokenKind.IntegerLiteral, new TextRange(..0, 0..1)),
        new Token("+", TokenKind.Plus, new TextRange(..0, 3..4)),
        new Token("6", TokenKind.IntegerLiteral, new TextRange(..0, 8..9)),
        new Token("*", TokenKind.Star, new TextRange(..0, 17..18)),
        new Token("9", TokenKind.IntegerLiteral, new TextRange(..0, 19..20)),
      };

      Diff.Assert(expectedTokens, Lexer.Lex("1  +    6        * 9"));
    }
    
    [Fact]
    public void TestWeirdShit()
    {
      List<Token> expectedTokens = new()
      {
        new Token("1", TokenKind.IntegerLiteral, new TextRange(..0, 0..1)),
        new Token("+", TokenKind.Plus, new TextRange(..0, 1..2)),
        new Token("+", TokenKind.Plus, new TextRange(..0, 2..3)),
        new Token("+", TokenKind.Plus, new TextRange(..0, 3..4)),
        new Token("+", TokenKind.Plus, new TextRange(..0, 4..5)),
        new Token("+", TokenKind.Plus, new TextRange(..0, 5..6)),
        new Token("+", TokenKind.Plus, new TextRange(..0, 6..7)),
        new Token("+", TokenKind.Plus, new TextRange(..0, 7..8)),
        new Token("2", TokenKind.IntegerLiteral, new TextRange(..0, 8..9)),
        new Token("*", TokenKind.Star, new TextRange(..0, 9..10)),
        new Token("*", TokenKind.Star, new TextRange(..0, 10..11)),
        new Token("*", TokenKind.Star, new TextRange(..0, 11..12)),
        new Token("3", TokenKind.IntegerLiteral, new TextRange(..0, 12..13)),
        new Token("/", TokenKind.Slash, new TextRange(..0, 13..14)),
        new Token("/", TokenKind.Slash, new TextRange(..0, 14..15)),
        new Token("/", TokenKind.Slash, new TextRange(..0, 15..16)),
        new Token("5", TokenKind.IntegerLiteral, new TextRange(..0, 16..17)),
      };

      Diff.Assert(expectedTokens, Lexer.Lex("1+++++++2***3///5"));
    }

    [Fact]
    public void TestStringLiterals()
    {
      /*List<Token> expectedTokens = new()
      {
        
      };

      Diff.Assert(expectedTokens, Lexer.Lex("'string'*2+\""));*/
    }
  }
}