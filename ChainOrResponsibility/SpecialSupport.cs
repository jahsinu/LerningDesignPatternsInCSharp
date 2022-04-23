
  public class SpecialSupport : Support
  {
    private int number;   // この番号だけが解決できる
    public SpecialSupport(string name, int number) : base(name)
    {
      this.number = number;
    }
    protected override bool resolve(Trouble trouble)    // 解決用メソッド
    {
      if (trouble.getNumber() == number)
      {
        return true;
      }
      else
      {
        return false;
      }
    }
  }
