using System;

namespace StrategyPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length != 2)
            {
                Console.WriteLine("Usage: dotnet StarategyPattern.dll randomseed1 randomseed2");
                Console.WriteLine("Example: dotnet StarategyPattern.dll 314 15");
                Environment.Exit(-1);
            }
            int seed1 = int.Parse(args[0]);
            int seed2 = int.Parse(args[1]);
            Player player1 = new Player("Taro", new WinningStrategy(seed1));
            Player player2 = new Player("Hana", new ProbStrategy(seed2));
            for(int i = 0; i < 1; i++)
            {
                Hand nextHand1 = player1.NextHand();
                Hand nextHand2 = player2.NextHand();
                if(nextHand1.IsStrongerThan(nextHand2))
                {
                    Console.WriteLine($"Winner: {player1}");
                    player1.Win();
                    player2.Lose();
                }
                else if(nextHand2.IsStrongerThan(nextHand1))
                {
                    Console.WriteLine($"Winner: {player2}");
                    player1.Lose();
                    player2.Win();
                }
                else
                {
                    Console.WriteLine("Even...");
                    player1.Even();
                    player2.Even();
                }
            }
            Console.WriteLine("Total result:");
            Console.WriteLine(player1.ToString());
            Console.WriteLine(player2.ToString());
        }
    }

    /// <summary>
    /// じゃんけんの手を表す
    /// </summary>
    public class Hand
    {
        public static int HANDVALUE_GUU = 0;
        public static int HANDVALUE_CHO = 1;
        public static int HANDVALUE_PAA = 2;
        public static Hand[] hand =
        {
            new Hand(HANDVALUE_GUU),
            new Hand(HANDVALUE_CHO),
            new Hand(HANDVALUE_PAA),
        };
        private static readonly String[] name =
        {
            "グー", "チョキ", "パー",
        };
        private int handValue;
        private Hand(int handValue)
        {
            this.handValue = handValue;
        }
        public static Hand getHand(int handValue)
        {
            return hand[handValue];
        }
        public bool IsStrongerThan(Hand h)
        {
            return Fight(h) == 1;
        }
        public bool IsWeekerThan(Hand h)
        {
            return Fight(h) == -1;
        }
        private int Fight(Hand h)
        {
            if (this == h)
            {
                return 0;
            }
            else if ((this.handValue + 1) % 3 == h.handValue)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
        public override string ToString()
        {
            return name[handValue];
        }
    }

    /// <summary>
    /// Strategy役
    /// </summary>
    /// <remarks>
    /// 戦略を利用するためのインターフェース(API)を定める
    /// </remarks>
    public interface IStrategy
    {
        Hand NextHand();
        void Study(bool win);
    }

    /// <summary>
    /// ConcreteStrategy役1
    /// </summary>
    /// <remarks>
    /// Strategy役が定めたAPIを実装する。
    /// </remarks>
    public class WinningStrategy : IStrategy
    {
        private Random random;
        private bool won = false;
        private Hand prevHand;
        public WinningStrategy(int seed)
        {
            random = new Random(seed);
        }

        public Hand NextHand()
        {
            if(!won)
            {
                prevHand = Hand.getHand(random.Next(3));
            }
            return prevHand;
        }

        public void Study(bool win)
        {
            won = win;
        }
    }

    /// <summary>
    /// ConcreteStrategy役2
    /// </summary>
    /// <remarks>
    /// Strategy役が定めたAPIを実装する。
    /// </remarks>
    public class ProbStrategy : IStrategy
    {
        private Random random;
        private int prevHandValue = 0;
        private int currentHandValue = 0;
        private int[][] history =
        {
            new int[] { 1, 1, 1, },
            new int[] { 1, 1, 1, },
            new int[] { 1, 1, 1, },
        };
        public ProbStrategy(int seed)
        {
            random = new Random(seed);
        }

        public Hand NextHand()
        {
            int bet = random.Next(GetSum(currentHandValue));
            int handValue = 0;
            if(bet < history[currentHandValue][0])
            {
                handValue = 0;
            }
            else if(bet < history[currentHandValue][0] + history[currentHandValue][1])
            {
                handValue = 1;
            }
            else
            {
                handValue = 2;
            }
            prevHandValue = currentHandValue;
            currentHandValue = handValue;
            return Hand.getHand(handValue);
        }

        private int GetSum(int hv)
        {
            int sum = 0;
            for(int i = 0; i < 3; i++)
            {
                sum += history[hv][i];
            }
            return sum;
        }

        public void Study(bool win)
        {
            if(win)
            {
                history[prevHandValue][currentHandValue]++;
            }
            else
            {
                history[prevHandValue][(currentHandValue + 1) % 3]++;
                history[prevHandValue][(currentHandValue + 2) % 3]++;
            }
        }

    }

    /// <summary>
    /// Context役
    /// </summary>
    /// <remarks>
    /// Strategyを利用する役。
    /// </remarks>
    public class Player
    {
        private string name;
        private IStrategy strategy;
        private int wincount;
        private int losecount;
        private int gamecount;
        public Player(string name, IStrategy strategy)
        {
            this.name = name;
            this.strategy = strategy;
        }
        public Hand NextHand()
        {
            return strategy.NextHand();
        }
        public void Win()
        {
            strategy.Study(true);
            wincount++;
            gamecount++;
        }
        public void Lose()
        {
            strategy.Study(false);
            losecount++;
            gamecount++;
        }
        public void Even()
        {
            gamecount++;
        }
        public override string ToString()
        {
            return $"[{name}:{gamecount} games, {wincount} win, {losecount} lose]";
        }
    }
}
