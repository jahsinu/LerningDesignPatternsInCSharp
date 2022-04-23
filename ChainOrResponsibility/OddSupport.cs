
  public class OddSupport : Support
  {
    public OddSupport(string name) : base(name) { }   // コンストラクタ
    protected override bool resolve(Trouble trouble)    // 解決用メソッド
    {
      if (trouble.getNumber() % 2 == 1)
      {
        return true;
      }
      else
      {
        return false;
      }
    }
  }
