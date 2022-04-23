using System;
using System.Threading;

namespace Observer
{
  public class GraphObserver : IMyObserver
  {
    public void update(NumberGenerator generator)
    {
      Console.Write("GraphObserver: ");
      int count = generator.getNumber();
      for(int i = 0; i < count; i++)
      {
        Console.Write("*");
      }
      Console.WriteLine("");
      try
      {
        Thread.Sleep(100);
      }
      catch (ThreadInterruptedException e)
      {
      }
    }
  }
}