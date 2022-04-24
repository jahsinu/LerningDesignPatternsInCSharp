
namespace StatePattern.Safe.State
{
  /// <summary>
  /// 夜間の状態を表すクラス 
  /// </summary>
  public class NightState : IState
  {
    private static IState singleton = new NightState();
    private NightState() { }

    /// <summary>
    /// コンストラクタ(シングルトンパターン)
    /// </summary>
    public static IState getInstance()
    {
      return singleton;
    }

    /// <summary>
    /// <para>時刻設定メソッド</para>
    /// <para>昼間への変化を検出すると、Contextの状態繊維メソッドをコールする</para>
    /// </summary>
    /// <param name="context"></param>
    /// <param name="hour"></param>
    public void doClock(Context context, int hour)
    {   // 時刻設定
      if (9 <= hour && hour < 17)
      {
        context.changeState(DayState.getInstance());
      }
    }

    /// <summary>
    /// 金庫使用
    /// </summary>
    /// <param name="context"></param>
    public void doUse(Context context)
    {
      context.callSecurityCenter("非常:夜間の金庫使用!");
    }

    /// <summary>
    /// 非常ベル
    /// </summary>
    /// <param name="context"></param>
    public void doAlarm(Context context)
    {
      context.callSecurityCenter("非常ベル(夜間)");
    }

    /// <summary>
    /// 通常通話
    /// </summary>
    /// <param name="context"></param>
    public void doPhone(Context context)
    {
      context.recordLog("夜間の通話録音");
    }

    /// <summary>
    /// 文字列表現
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      return "[夜間]";
    }
  }
}