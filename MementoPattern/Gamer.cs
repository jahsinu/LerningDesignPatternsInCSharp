using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    internal class Gamer
    {
        private int money;                                // 所持金
        private List<string> fruits = new List<string>(); // フルーツ
        private Random random = new Random();             // 乱数発生器
        private static string[] fruitsname =              // フルーツ名の表
        {
            "リンゴ", "ぶどう", "バナナ", "みかん",
        };
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="money"></param>
        public Gamer(int money)
        {
            this.money = money;
        }

        /// <summary>
        /// 現在の所持金を得る
        /// </summary>
        /// <returns></returns>
        public int GetMoney()
        {
            return money;
        }

        /// <summary>
        /// 賭ける・・・(ゲームの進行)
        /// </summary>
        public void Bet()
        {
            int dice = random.Next(6) + 1;
            if (dice == 1)                // 1の目・・・所持金が増える
            {
                money += 100;
                Console.WriteLine("所持金が増えました。");
            }
            else if (dice == 2)           // 2の目・・・所持金が半分になる
            {
                money /= 2;
                Console.WriteLine("所持金が半分になりました。");
            }
            else if (dice == 6)            // 6の目・・・フルーツをもらう
            {
                string f = GetFruits();
                Console.WriteLine($"フルーツ({f})をもらいました。");
                fruits.Add(f);
            }
            else                         // それ以外・・・何も起きない
            {
                Console.WriteLine("何も起こりませんでした。");
            }
        }

        /// <summary>
        /// スナップショットを撮る
        /// </summary>
        /// <returns></returns>
        public Memento CreateMemento()
        {
            Memento m = new Memento(money);
            foreach (string f in fruits)
            {
                if (f.StartsWith("おいしい"))    // フルーツはおいしいものだけ保存
                {
                    m.AddFruits(f);
                }
            }
            return m;
        }

        /// <summary>
        /// アンドゥを行う
        /// </summary>
        /// <param name="memento"></param>
        public void RestoreMemento(Memento memento)
        {
            money = memento.GetMoney();
            fruits = memento.GetFruits();
        }

        /// <summary>
        /// 文字列表現
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"[money = {money}, fruits = {string.Join(",",fruits)}]";
        }

        /// <summary>
        /// フルーツを1個得る
        /// </summary>
        /// <returns></returns>
        private string GetFruits()
        {
            string prefix = "";
            if (random.Next(2) > 0)
            {
                prefix = "おいしい";
            }
            return prefix + fruitsname[random.Next(fruitsname.Length)];
        }
    }
}
