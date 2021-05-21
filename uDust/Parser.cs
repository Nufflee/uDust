using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace uDust
{
  public class Parser
  {
    private Token currentToken;
    private List<Token> tokens;
    private int currentTokenIndex;

    public RootNode Parse(List<Token> tokens)
    {
      List<Expression> children = new();

      this.tokens = tokens;
      Advance();

      while (currentToken.Kind != TokenKind.EndOfFile)
      {
        Expression child = ParseStatement(-1);

        if (child != null)
        {
          children.Add(child);
        }
      }

      return new RootNode(children.ToImmutableArray());
    }

    private Expression ParseStatement(int lastPrecedence = 0)
    {
      Expression left = ParseExpression();

      if (left == null)
      {
        Advance();

        return null;
      }

      if (IsBinaryOperator(currentToken))
      {
        // In order to follow precedence rules, do not keep parsing if this operator has higher precedence than the last one.
        if (GetBinaryOperatorPrecedence(currentToken) <= lastPrecedence)
        {
          return left;
        }

        BinaryExpression binaryExpression = ParseBinaryExpression(left);

        // Keep parsing until we hit a non-binary operator to satisfy left-associativity.
        while (IsBinaryOperator(currentToken))
        {
          binaryExpression = ParseBinaryExpression(binaryExpression);
        }

        return binaryExpression;
      }

      return left;
    }

    private Expression ParseExpression()
    {
      return ParseLiteral();
    }

    private LiteralExpression ParseLiteral()
    {
      LiteralExpression expression = currentToken.Kind switch
      {
        TokenKind.IntegerLiteral => new LiteralExpression(int.Parse(currentToken.Text), currentToken),
        TokenKind.DecimalLiteral => new LiteralExpression(double.Parse(currentToken.Text), currentToken),
        TokenKind.StringLiteral => new LiteralExpression(currentToken.Text, currentToken),
        _ => null
      };

      if (expression != null)
      {
        Advance();
      }

      return expression;
    }

    private BinaryExpression ParseBinaryExpression(Expression left)
    {
      Token operatorToken = currentToken;

      Advance();

      Expression right = ParseStatement(GetBinaryOperatorPrecedence(operatorToken));

      return new BinaryExpression(left, operatorToken, right);
    }

    private void Advance()
    {
      currentToken = tokens[currentTokenIndex++];
    }
    
    private static bool IsBinaryOperator(Token token)
    {
      return token.Kind is TokenKind.Plus or TokenKind.Minus or TokenKind.Star or TokenKind.Slash;
    }

    private static int GetBinaryOperatorPrecedence(Token token)
    {
      switch (token.Kind)
      {
        case TokenKind.Plus:
        case TokenKind.Minus:
          return 0;
        case TokenKind.Star:
        case TokenKind.Slash:
          return 1;
        default:
          throw new ArgumentException($"{token.Kind} is not a binary operator");
      }
    }
  }
}