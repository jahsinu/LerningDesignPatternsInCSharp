using System;
using System.Collections.Generic;

namespace Observer
{
  public class RandomNumberGenerator : NumberGenerator
  {
    private Random random = new Random();   // 乱数発生器
    private int number;   // 現在の数
    public override int getNumber()    // 数を取得する
    {
      return number;
    }
    public override void execute()
    {
      for (int i = 0; i < 20; i++)
      {
        number = random.Next(50);
        notifyOvservers();
      }
    }
  }
}