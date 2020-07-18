using System;
using System.Collections.Generic;

namespace IteratorPattern
{
    /// <summary>
    /// 数え上げ、スキャンを行うインタフェース
    /// </summary>
    /// <remarks>
    /// iterator役
    /// </remarks>
    public interface IIterator
    {
        bool HasNext();
        object Next();
    }

    /// <summary>
    /// 集合体を表すインタフェース
    /// </summary>
    /// <remarks>
    /// Aggreagate役
    /// </remarks>
    public interface IAggregate
    {
        IIterator Iterator();
    }

    /// <summary>
    /// 本を表すクラス
    /// </summary>
    public class Book
    {
        private string name;
        public Book(string name)
        {
            this.name = name;
        }
        public string GetName()
        {
            return name;
        }
    }

    /// <summary>
    /// 本棚を表すクラス
    /// </summary>
    /// <remarks>
    /// ConcreteAggregate役
    /// </remarks>
    public class BookShelf : IAggregate
    {
        private List<Book> books = new List<Book>();
        private int last = 0;
        public BookShelf() { }
        public Book GetBookAt(int index)
        {
            return books[index];
        }
        public void AppendBook(Book book)
        {
            this.books.Add(book);
            last++;
        }
        public int GetLength()
        {
            return last;
        }
        public IIterator Iterator()
        {
            return new BookShelfIterator(this);
        }
    }

    /// <summary>
    /// 本棚をスキャンするクラス
    /// </summary>
    /// <remarks>
    /// ConcreteIterator役
    /// </remarks>
    public class BookShelfIterator : IIterator
    {
        private BookShelf bookShelf;
        private int index;
        public BookShelfIterator(BookShelf bookshelf)
        {
            this.bookShelf = bookshelf;
            this.index = 0;
        }
        public bool HasNext()
        {
            if(index < bookShelf.GetLength())
            {
                return true;
            }
            return false;
        }
        public object Next()
        {
            Book book = bookShelf.GetBookAt(index);
            index++;
            return book;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            BookShelf bookShelf = new BookShelf();
            bookShelf.AppendBook(new Book("Around the World in 80 Days"));
            bookShelf.AppendBook(new Book("Bible"));
            bookShelf.AppendBook(new Book("Cinderella"));
            bookShelf.AppendBook(new Book("Daddy-Long-Legs"));

            // イテレータを利用することで、BookShelf内のコレクション実装に
            // 依存せず、統一されたインタフェースでコレクションをスキャンできる。

            // イテレータはIIterator型として受け取る。
            // BookShelfIteratorに依存せず、IIteratorを実装したクラスを
            // 使用可能になる。
            // (HasNext()とNext()が実装されていることが保証される)
            // BookShelfが別のイテレータを返すよう修正されても、以下の
            // 走査処理は変更する必要がない。
            IIterator it = bookShelf.Iterator();
            while(it.HasNext())
            {
                Book book = (Book)it.Next();
                System.Console.WriteLine(book.GetName());
            }
        }
    }
}
