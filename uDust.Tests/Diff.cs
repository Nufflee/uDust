using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Xunit;

namespace uDust.Tests
{
  public class Diff
  {
    public static void Assert<T>(T a, T b)
    {
      (PropertyInfo, object, object)? diff = DiffProperties(a, b);

      bool equal = diff == null;

      Debug.Assert(equal == Equals(a, b), "This should NEVER happen.");

      if (equal)
      {
        return;
      }

      (PropertyInfo field, object valueA, object valueB) = diff.Value;

      Xunit.Assert.True(false, $"Mismatch in {field.Name}: {valueA} != {valueB}");
    }

    public static void Assert<T>(List<T> a, List<T> b)
    {
      Xunit.Assert.True(a.Count == b.Count, $"a.Count ({a.Count}) != b.Count ({b.Count})");

      for (int i = 0; i < a.Count; i++)
      {
        Assert(a[i], b[i]);
      }
    }

    private static (PropertyInfo, object, object)? DiffProperties<T>(T a, T b)
    {
      foreach (PropertyInfo field in typeof(T).GetProperties())
      {
        object valueA = field.GetValue(a);
        object valueB = field.GetValue(b);

        if (!Equals(valueA, valueB))
        {
          return (field, valueA, valueB);
        }
      }

      return null;
    }
  }
}