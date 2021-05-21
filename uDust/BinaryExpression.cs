namespace uDust
{
  public class BinaryExpression : Expression
  {
    public Expression Left { get; }
    public Token OperatorToken { get; }
    public Expression Right { get; }

    public BinaryExpression(Expression left, Token operatorToken, Expression right)
    {
      Left = left;
      OperatorToken = operatorToken;
      Right = right;
    }

    public override string DumpToGraphViz(GraphVizContext context)
    {
      string nodeName = $"BinaryExpression{context.N}";
      string result = "";

      context.N++;

      context.Nodes.Add(nodeName, OperatorToken.Text);

      if (Left is BinaryExpression leftBinaryExpression)
      {
        result += $"{nodeName} -> BinaryExpression{context.N}\n";

        result += leftBinaryExpression.DumpToGraphViz(context);
      }
      else if (Left is LiteralExpression literalExpression)
      {
        result += $"{nodeName} -> LiteralExpression{context.N}\n";

        literalExpression.DumpToGraphViz(context);
      }

      if (Right is BinaryExpression binaryExpression)
      {
        result += $"{nodeName} -> BinaryExpression{context.N}\n";

        result += binaryExpression.DumpToGraphViz(context);
      }
      else if (Right is LiteralExpression literalExpression)
      {
        result += $"{nodeName} -> LiteralExpression{context.N}\n";

        literalExpression.DumpToGraphViz(context);
      }

      return result;
    }
  }
}