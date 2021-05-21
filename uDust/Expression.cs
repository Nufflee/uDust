namespace uDust
{
  public abstract class Expression : Node
  {
    public virtual string DumpToGraphViz(GraphVizContext context)
    {
      return "";
    }
  }
}