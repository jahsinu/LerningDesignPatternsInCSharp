using StatePattern.Safe;

namespace StatePattern
{
  class Program
  {
    /// <summary>
    /// テスト用Mainメソッド
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
      // Contextインスタンスを作成
      Context context = new Context();

      while (true)
      {
        ShowCommand();
        string? line;
        // 入力コマンド取得
        if ((line = Console.ReadLine()) != null)
        {
          if ((int.TryParse(line, out int value)) && (0 < value && value <= 24))
          {
            // 1〜24の数値なら時刻設定実施
            context.setClock(value);
          }
          else if (line.Trim().Length == 1)
          {
            string action = line.Trim();
            // 有効なコマンド文字であれば、contextのコマンド実行関数をコール
            switch (action)
            {
              case "u":
              case "a":
              case "p":
                context.actionPerformed(action);
                break;

              default:
                goto CommInvalid;
            }
          }
          else
          {
            goto CommInvalid;
          }
        }
        else
        {
          goto CommInvalid;
        }
      CommInvalid:
        Console.WriteLine("有効なコマンド文字を入力してください。");
      }
    }

    /// <summary>
    /// コマンド表示メソッド
    /// </summary>
    static void ShowCommand()
    {
      Console.WriteLine("");
      Console.WriteLine("***********************************");
      Console.WriteLine("コマンドを指定してください。");
      Console.WriteLine("1-24 : 現在時刻を設定する");
      Console.WriteLine("u : 金庫を使用する");
      Console.WriteLine("a : 非常ベルを使用する");
      Console.WriteLine("p : 停止ボタンを押す");
      Console.WriteLine();
    }
  }
}