
  public class LimitSupport : Support
  {
    private int limit;    // この番号未満なら解決できる
    public LimitSupport(string name, int limit) : base(name)    // コンストラクタ
    {
      this.limit = limit;
    }
    protected override bool resolve(Trouble trouble)    // 解決用メソッド
    {
      if (trouble.getNumber() < limit)
      {
        return true;
      }
      else
      {
        return false;
      }
    }
  }

