using System;
using System.Threading;

namespace Observer
{
  public class DigitObserver : IMyObserver
  {
    public void update(NumberGenerator generator)
    {
      Console.WriteLine($"DigitObserver: {generator.getNumber()}");
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