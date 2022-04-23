using System;

namespace Observer
{
    class Program
    {
        static void Main(string[] args)
        {
          NumberGenerator generator = new RandomNumberGenerator();
          IMyObserver observer1 = new DigitObserver();
          IMyObserver observer2 = new GraphObserver();
          generator.addObserver(observer1);
          generator.addObserver(observer2);
          generator.execute();
        }
    }
}
