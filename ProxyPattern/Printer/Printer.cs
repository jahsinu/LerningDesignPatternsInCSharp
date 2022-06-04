namespace ProxyPattern
{
    /// <summary>
    /// RealSubject(実際の主体)の役
    /// </summary>
    public class Printer : IPrintable
    {
        private string name = "";

        public Printer()
        {
            heavyJob("Printerのインスタンスを作成中");
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">名前</param>
        public Printer(string name)
        {
            this.name = name;
            heavyJob($"Printerのインスタンス({name})を作成中");
        }

        /// <summary>
        /// 名前の設定
        /// </summary>
        /// <param name="name">名前</param>
        public void setPrinterName(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// 名前の取得
        /// </summary>
        /// <returns>名前</returns>
        public string getPrinterName()
        {
            return name;
        }

        /// <summary>
        /// 名前付きで表示
        /// </summary>
        /// <param name="msg">表示対象文字列</param>
        public void print(string msg)
        {
            Console.WriteLine($"=== {name} ===");
            Console.WriteLine(msg);
        }

        /// <summary>
        /// 重い作業(のつもり)
        /// </summary>
        /// <param name="msg">インスタンス生成中を示す文字列</param>
        private void heavyJob(string msg)
        {
            Console.Write(msg);
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(1000);
                Console.Write(".");
            }
            Console.WriteLine("完了。");
        }
    }

}