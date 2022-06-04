using System.Runtime.CompilerServices;

namespace ProxyPattern
{
    /// <summary>
    /// Proxy(代理人)の役
    /// </summary>
    public class PrinterProxy : IPrintable
    {
        /// <summary>
        /// 名前
        /// </summary>
        private string name = "";

        /// <summary>
        /// 本人
        /// </summary>
        private Printer? real;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PrinterProxy() { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">名前</param>
        public PrinterProxy(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// 名前の設定
        /// </summary>
        /// <param name="name">名前</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void setPrinterName(string name)
        {
            if (real != null)
            {
                real.setPrinterName(name);  // 本人が既に存在すれば、本人にも設定する
            }
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
        /// 表示
        /// </summary>
        /// <param name="msg">表示対象文字列</param>
        public void print(string msg)
        {
            realize();
            if (real != null)
            {
                real.print(msg);
            }
        }

        /// <summary>
        /// 「本人」を生成
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void realize()
        {
            if (real == null)
            {
                real = new Printer(name);
            }
        }
    }
}