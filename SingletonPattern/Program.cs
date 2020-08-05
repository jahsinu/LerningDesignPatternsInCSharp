using System;

namespace SingletonPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            Singleton obj1 = Singleton.GetInstance();
            Singleton obj2 = Singleton.GetInstance();
            if(obj1 == obj2)
            {
                Console.WriteLine("obj1とobj2は同じインスタンスです。");
            }
            else
            {
                Console.WriteLine("obj1とobj2は同じインスタンスではありません。");
            }
            Console.WriteLine("End");
        }
    }

    /// <summary>
    /// Singleton役
    /// </summary>
    public class Singleton
    {
        // クラスロード時に自身をインスタンス化(唯一のインスタンス)
        // クラス変数にインスタンスを保持
        private static Singleton singleton = new Singleton();

        // privateにすることで、直接インスタンス化されることを防ぐ
        // これによって、インスタンスの単一性を保証する
        private Singleton()
        {
            Console.WriteLine("インスタンスを生成しました。");
        }

        // クラス変数に保持したインスタンスを返却
        public static Singleton GetInstance() => singleton;
    }
}
