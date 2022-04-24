
namespace StatePattern.Safe.State
{
  /// <summary>
  /// 昼間の状態を表すクラス
  /// </summary>
  public class DayState : IState
  {
    private static IState singleton = new DayState();
    private DayState() { }

    /// <summary>
    /// コンストラクタ(シングルトンパターン)
    /// </summary>
    public static IState getInstance()
    {
      return singleton;
    }

    /// <summary>
    /// <para>時刻設定メソッド</para>
    /// <para>夜間への変化を検出すると、Contextの状態繊維メソッドをコールする</para>
    /// </summary>
    /// <param name="context"></param>
    /// <param name="hour"></param>
    public void doClock(Context context, int hour)
    {   // 時刻設定
      if (hour < 9 || 17 <= hour)
      {
        context.changeState(NightState.getInstance());
      }
    }

    /// <summary>
    /// 金庫使用
    /// </summary>
    /// <param name="context"></param>
    public void doUse(Context context)
    {
      context.recordLog("金庫使用(昼間)");
    }

    /// <summary>
    /// 非常ベル
    /// </summary>
    /// <param name="context"></param>
    public void doAlarm(Context context)
    {
      context.callSecurityCenter("非常ベル(昼間)");
    }

    /// <summary>
    /// 通常通話
    /// </summary>
    /// <param name="context"></param>
    public void doPhone(Context context)
    {
      context.callSecurityCenter("通常の通話(昼間)");
    }

    /// <summary>
    /// 文字列表現
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      return "[昼間]";
    }
  }
}