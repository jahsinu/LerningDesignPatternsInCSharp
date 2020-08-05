using System;
using System.Collections.Generic;

namespace FactroyMethodPattern
{
    using Framework;
    using IDCard;
    class Program
    {
        static void Main(string[] args)
        {
            Factory factory = new IDCardFactory();
            Product card1 = factory.Create("hogehoge");
            Product card2 = factory.Create("fugafuga");
            Product card3 = factory.Create("barbar");
            // hogehogeのIDカードを作ります。
            // fugafugaのIDカードを作ります。
            // barbarのIDカードを作ります。

            card1.Use();
            card2.Use();
            card3.Use();
            // hogehogeのIDカードを使います。
            // fugafugaのIDカードを使います。
            // barbarのIDカードを使います。

            foreach(KeyValuePair<int, string> owner in ((IDCardFactory)factory).OwnerDic)
            {
                Console.WriteLine(owner);
            }
            // hogehoge
            // fugafuga
            // barbar
        }
    }
}

namespace Framework
{
    /// <summary>
    /// Product役
    /// </summary>
    /// <remarks>
    /// 作成されるインスタンスが持つAPIを規定する抽象クラス
    /// </remarks>
    public abstract class Product
    {
        // 製品(product)は、useできるものである、と規定
        public abstract void Use();
    }

    /// <summary>
    /// Creator役
    /// </summary>
    /// <remarks>
    /// Product役を生成する抽象クラス
    /// </remarks>
    public abstract class Factory
    {
        // TemplateMethodパターンを使用して、製品(product)を「作成」し
        // 「登録」するロジックを規定
        public Product Create(string owner)
        {
            Product p = CreateProduct(owner);
            RegisterProduct(p);
            return p;
        }

        // productを「作成」「登録」する具体的処理は抽象メソッドとして
        // 派生クラスに実装を任せる

        // インスタンス生成メソッドを抽象メソッドとすることで、
        // スーパークラスを具体的なクラス名の束縛から解放する
        protected abstract Product CreateProduct(string owner);
        protected abstract void RegisterProduct(Product product);
    }
}

namespace IDCard
{
    using Framework;

    /// <summary>
    /// ConcreteProduct役
    /// </summary>
    public class IDCard : Product
    {
        public string Owner { get; private set; } = "";

        internal IDCard(string owner)
        {
            Console.WriteLine(owner + "のIDカードを作ります。");
            Owner = owner;
        }

        // スーパークラスの抽象メソッドを実装
        public override void Use()
        {
            Console.WriteLine(Owner + "のIDカードを使います。");
        }
    }

    /// <summary>
    /// ConcreteCreator役
    /// </summary>
    public class IDCardFactory : Factory
    {
        //public List<string> Owners { get; private set; } = new List<string>();
        private int idNum = 1;
        public Dictionary<int, string> OwnerDic { get; private set; } = new Dictionary<int, string>();

        // スーパークラスの抽象メソッドを実装
        protected override Product CreateProduct(string owner)
        {
            // 製品(product)を作成
            return new IDCard(owner);
        }
        protected override void RegisterProduct(Product product)
        {
            // 製品(product)を登録
            //Owners.Add(((IDCard)product).Owner);
            OwnerDic.Add(idNum++, ((IDCard)product).Owner);
        }
    }
}
