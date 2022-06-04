
namespace ProxyPattern
{
    /// <summary>
    /// Subject(主体)の役
    /// </summary>
    public interface IPrintable
    {
        /// <summary>
        /// 名前の設定
        /// </summary>
        /// <param name="name">名前</param>
        public abstract void setPrinterName(string name);

        /// <summary>
        /// 名前の取得
        /// </summary>
        /// <returns>名前</returns>
        public abstract string getPrinterName();

        /// <summary>
        /// 文字列表示(プリントアウト)
        /// </summary>
        /// <param name="msg">表示対象文字列</param>
        public abstract void print(string msg);
    }
}