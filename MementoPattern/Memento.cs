using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Memento
    {
        int money;
        List<string> fruits;

        public int GetMoney()
        {
            return money;
        }

        internal Memento(int money)
        {
            this.money = money;
            this.fruits = new List<string>();
        }

        internal void AddFruits(string fruits)
        {
            this.fruits.Add(fruits);
        }

        internal List<string> GetFruits()
        {
            return new List<string>(this.fruits);
        }
    }
}
