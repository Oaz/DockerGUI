using System;
using System.Collections.Generic;
using System.Linq;

namespace DockerGUI.Models
{
  public class Tree<T>
  {
    public static IEnumerable<TN> Create<TN>(
      IEnumerable<T> population,
      Func<T,bool> isRoot,
      Func<T,T,IEnumerable<T>,bool> isChildOf,
      Func<T,IEnumerable<TN>,TN> factory
      )  where TN:Node => Create(population.ToArray(), new Functions<TN> { IsRoot = isRoot, IsChildOf = isChildOf, New = factory });
    
    private class Functions<TN>
      where TN:Node
    {
      public Func<T, bool> IsRoot = _ => false;
      public Func<T, T, IEnumerable<T>, bool> IsChildOf = (_,__,___) => false;
      public Func<T, IEnumerable<TN>, TN> New = (_,__) => null!;
    }

    private static IEnumerable<TN> Create<TN>(T[] population, Functions<TN> f) where TN : Node =>
      population
        .Where(f.IsRoot)
        .Select(root => CreateInside(root, population, f))
        .ToArray();

    private static TN CreateInside<TN>(T root, T[] population, Functions<TN> f) where TN : Node
    {
      var children = population.Aggregate(new List<T>(), (take, item) =>
      {
        if(f.IsChildOf(item, root, take))
          take.Add(item);
        return take;
      });
      return f.New(root, children.Select(c => CreateInside(c, population, f)).ToArray());
    }

    public class Node
    {
      public Node(T parent, IEnumerable<Node> children)
      {
        Parent = parent;
        Children = children;
      }
      public T Parent { get; }
      public IEnumerable<Node> Children { get; }
    }
  }
}
