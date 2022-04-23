using System;

  public abstract class Support
  {
    private string name;            // トラブル解決者の名前
    private Support next;           // たらい回しの先
    public Support(string name)     // トラブル解決者の生成
    {
      this.name = name;
    }
    public Support setNext(Support next)    // たらい回し先を設定
    {
      this.next = next;
      return next;
    }
    public void support(Trouble trouble)  // トラブル解決の手順
    {
      if (resolve(trouble))
      {
        done(trouble);
      }
      else if (next != null)
      {
        next.support(trouble);
      }
      else
      {
        fail(trouble);
      }
    }
    public string toString()    // 文字列表現
    {
      return "[" + name + "]";
    }
    protected abstract bool resolve(Trouble trouble);   // 解決用メソッド
    protected void done(Trouble trouble)
    {
      Console.WriteLine(trouble.toString() + " is resolved by " + this.toString() + ".");
    }
    protected void fail(Trouble trouble)
    {
      Console.WriteLine(trouble.toString() + " cannot be resolved.");
    }
  }

