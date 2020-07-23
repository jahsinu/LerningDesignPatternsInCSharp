using System;

namespace AdapterPattern_Extend
{
    /// <summary>
    /// Adaptee役
    /// </summary>
    /// <remarks>
    /// <para>Target役が定めるメソッドとは異なるメソッドを持つ</para>
    /// <para>既存クラス等を再利用するなど</para>
    /// </remarks>
    public class Banner
    {
        private string para;
        public Banner(string para)
        {
            this.para = para;
        }
        public void ShowWithParen()
        {
            Console.WriteLine("(" + para + ")");
        }
        public void ShowWithAster()
        {
            Console.WriteLine("*" + para + "*");
        }
    }

    /// <summary>
    /// Target役
    /// </summary>
    /// <remarks>
    /// 必要となるメソッドを定める
    /// </remarks>
    public interface IPrint
    {
        void PrintWeak();
        void PrintStrong();
    }

    /// <summary>
    /// Adapter役
    /// </summary>
    /// <remarks>
    /// Target役が定めるメソッドを満たしつつ、Adaptee役のメソッドを利用する
    /// </remarks>
    public class PrintBanner : Banner, IPrint
    {
        public PrintBanner(String para) : base(para)
        {
        }
        public void PrintWeak()
        {
            ShowWithParen();
        }
        public void PrintStrong()
        {
            ShowWithAster();
        }
    }

    /// <summary>
    /// Client役
    /// </summary>
    /// <remarks>
    /// Target役が定めるメソッドで仕事をする
    /// </remarks>
    class Program
    {
        static void Main(string[] args)
        {
            IPrint p = new PrintBanner("Hello");
            p.PrintWeak();
            p.PrintStrong();
        }
    }
}
