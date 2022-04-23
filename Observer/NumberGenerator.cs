using System.Collections.Generic;

namespace Observer
{
  public abstract class NumberGenerator
  {
    private List<IMyObserver> observers = new List<IMyObserver>();    // Observer達を保持
    public void addObserver(IMyObserver observer)   // Observerを追加
    {
      observers.Add(observer);
    }
    public void deleteObserver(IMyObserver observer)    // Observerを削除
    {
      observers.Remove(observer);
    }
    public void notifyOvservers()   // Observerへ通知
    {
      foreach(IMyObserver o in observers)
      {
        o.update(this);
      }
    }
    public abstract int getNumber();
    public abstract void execute();
  }
}