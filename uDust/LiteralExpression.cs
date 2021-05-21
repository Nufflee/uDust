namespace uDust
{
  public class LiteralExpression : Expression
  {
    public object Value { get; }
    public Token Token { get; }

    public LiteralExpression(object value, Token token)
    {
      Value = value;
      Token = token;
    }

    public override string DumpToGraphViz(GraphVizContext context)
    {
      context.Nodes.Add($"LiteralExpression{context.N}", Value.ToString());

      context.N++;

      return "";
    }
  }
}