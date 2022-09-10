using Game;

namespace Memento
{
    class Program
    {
        public static void Main(string[] args)
        {
            Gamer gamer = new Gamer(100);                 // 最初の所持金は100
            Game.Memento memento = gamer.CreateMemento(); // 最初の状態を保存しておく
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"==={i}");             // 回数表示
                Console.WriteLine($"現状:{gamer}");       // 現在の主人公の状態表示

                gamer.Bet();    // ゲームを進める

                Console.WriteLine($"所持金は{gamer.GetMoney()}円になりました。");       // 現在の主人公の状態表示

                // Mementoの取り扱いの決定
                if (gamer.GetMoney() > memento.GetMoney())
                {
                    Console.WriteLine("    (だいぶ増えたので、現在の状態を保存しておこう)");
                    memento = gamer.CreateMemento();
                }
                else if (gamer.GetMoney() < memento.GetMoney() / 2)
                {
                    Console.WriteLine("    (だいぶ減ったので、以前の状態に復帰しよう)");
                    gamer.RestoreMemento(memento);
                }

                // 時間待ち

                try
                {
                    Thread.Sleep(1000);
                }
                catch (ThreadInterruptedException)
                {
                }
                Console.WriteLine("");
            }
        }
    }
}