
  public class NoSupport : Support
  {
    public NoSupport(string name) : base(name) { }
    protected override bool resolve(Trouble trouble)    // 解決用メソッド
    {
      return false;   // 自分は何も処理しない
    }
  }

