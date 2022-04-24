using StatePattern.Safe.State;

namespace StatePattern.Safe
{
  /// <summary>
  /// 金庫の状態や警備センター呼び出しを制御するためのインタフェース 
  /// </summary>
  public class Context
  {
    private int hour = 9;
    private IState state = DayState.getInstance();

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public Context()
    {
      state.doClock(this, this.hour);
    }

    /// <summary>
    /// コマンド実行メソッド
    /// </summary>
    /// <param name="action"></param>
    public void actionPerformed(string action)
    {
      if (action == "u")
      {
        state.doUse(this);
      }
      else if (action == "a")
      {
        state.doAlarm(this);
      }
      else
      {
        state.doPhone(this);
      }
    }

    /// <summary>
    /// 時刻設定メソッド
    /// </summary>
    /// <param name="hour"></param>
    public void setClock(int hour)
    {
      Console.WriteLine($"現在時刻は{hour:00}:00", hour);
      state.doClock(this, hour);
    }

    /// <summary>
    /// 状態遷移メソッド
    /// </summary>
    /// <param name="state"></param>
    public void changeState(IState state)
    {
      Console.WriteLine($"{this.state}から{state}へ状態が変化しました。");
      this.state = state;
    }

    /// <summary>
    /// 警備センター呼び出しメソッド
    /// </summary>
    /// <param name="msg"></param>
    public void callSecurityCenter(string msg)
    {
      Console.WriteLine($"Call! {msg}");
    }

    /// <summary>
    /// 警備センター呼び出しログ記録メソッド
    /// </summary>
    /// <param name="msg"></param>
    public void recordLog(string msg)
    {
      Console.WriteLine($"Record ... {msg}");
    }
  }
}