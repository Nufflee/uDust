using System.Collections.Generic;
using System.Collections.Immutable;

namespace uDust
{
  public class GraphVizContext
  {
    public int N;
    public Dictionary<string, string> Nodes { get; }

    public GraphVizContext()
    {
      Nodes = new Dictionary<string, string>();
    }
  }

  public class RootNode
  {
    public ImmutableArray<Expression> Children { get; }

    public RootNode(ImmutableArray<Expression> children)
    {
      Children = children;
    }

    public string DumpToGraphViz()
    {
      string result = "";

      GraphVizContext context = new();

      result += "strict digraph G {\n";

      foreach (Expression expression in Children)
      {
        result += expression.DumpToGraphViz(context);
      }

      result += "\n";

      foreach ((string nodeName, string nodeLabel) in context.Nodes)
      {
        result += $"{nodeName} [label=\"{nodeLabel}\"]\n";
      }

      result += "}";

      return result;
    }
  }
}