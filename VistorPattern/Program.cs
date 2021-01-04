using System;
using System.Collections;
using System.Collections.Generic;

namespace VistorPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Making root entries...");
                Directory rootdir = new Directory("root");
                Directory bindir = new Directory("bin");
                Directory tmpdir = new Directory("tmp");
                Directory usrdir = new Directory("usr");
                rootdir.Add(bindir);
                rootdir.Add(tmpdir);
                rootdir.Add(usrdir);
                bindir.Add(new File("vi", 10000));
                bindir.Add(new File("latex", 20000));
                rootdir.Accept(new ListVisitor());

                Console.WriteLine("");
                Console.WriteLine("Making user entries...");
                Directory yuki = new Directory("yuki");
                Directory hanako = new Directory("hanako");
                Directory tomura = new Directory("tomura");
                usrdir.Add(yuki);
                usrdir.Add(hanako);
                usrdir.Add(tomura);
                yuki.Add(new File("diary.txt", 100));
                yuki.Add(new File("Composite.cs", 200));
                hanako.Add(new File("memo.tex", 300));
                tomura.Add(new File("game.doc", 400));
                tomura.Add(new File("junk.mail", 500));
                rootdir.Accept(new ListVisitor());
            }
            catch (FileTreatmentException e)
            {
                e.ToString();
            }
        }
    }

    /// <summary>
    /// Visitor役
    /// </summary>
    /// <remarks>
    /// データ構造の各要素を訪問し、処理を行うためのAPIを規定
    /// </remarks>
    public abstract class Visitor
    {
        public abstract void Visit(File file);
        public abstract void Visit(Directory directory);
    }

    /// <summary>
    /// ConcreteVisitor役
    /// </summary>
    /// <remarks>
    /// データ構造の各要素を訪問して行う処理のAPIを実装
    /// </remarks>
    public class ListVisitor : Visitor
    {
        private string CurrentDir { get; set; }
        public override void Visit(File file)
        {
            Console.WriteLine($"{CurrentDir}/{file}");
        }

        public override void Visit(Directory directory)
        {
            Console.WriteLine($"{CurrentDir}/{directory}");
            string saveDir = CurrentDir;
            CurrentDir = $"{CurrentDir}/{directory.GetName()}";
            IEnumerator it = directory.Iterator();
            while(it.MoveNext())
            {
                Entry entry = (Entry)it.Current;
                entry.Accept(this);
            }
            CurrentDir = saveDir;
        }
    }

    /// <summary>
    /// Element役
    /// </summary>
    /// <remarks>
    /// Visitorの訪問先になる。
    /// Visitorを受け入れるためのAcceptメソッドの実装を強制する。
    /// </remarks>
    public interface Element
    {
        void Accept(Visitor v);
    }

    public abstract class Entry : Element
    {
        public abstract string GetName();
        public abstract int GetSize();
        public virtual Entry Add(Entry entry)
        {
            throw new FileTreatmentException();
        }
        public virtual IEnumerator Iterator()
        {
            throw new FileTreatmentException();
        }
        public override string ToString()
        {
            return $"{GetName()} ({GetSize()})";
        }
        public abstract void Accept(Visitor v);
    }

    /// <summary>
    /// ConcreteElement役1
    /// </summary>
    /// <remarks>
    /// Visitorの訪問を受け入れるAcceptメソッドを実装する。
    /// (Accept()呼び出しによりVisitorがVisit()することを許すイメージ)
    /// </remarks>
    public class File : Entry
    {
        private string name;
        private int size;
        public File(string name, int size)
        {
            this.name = name;
            this.size = size;
        }

        public override string GetName()
        {
            return name;
        }

        public override int GetSize()
        {
            return size;
        }

        public override void Accept(Visitor v)
        {
            // this は Fileインスタンス なので Visit(File file) が
            // 呼び出される。
            v.Visit(this);
        }
    }

    /// <summary>
    /// ConcreteElement役2、ObjectStructure役
    /// </summary>
    /// <remarks>
    /// ConcreteElement役であると共に、Elementの集合を扱う
    /// </remarks>
    public class Directory : Entry
    {
        private string name;
        private List<Entry> directory = new List<Entry>();
        public Directory(string name)
        {
            this.name = name;
        }

        public override string GetName()
        {
            return name;
        }

        public override int GetSize()
        {
            int size = 0;
            foreach(Entry e in directory)
            {
                size += e.GetSize();
            }
            return size;
        }

        public override Entry Add(Entry entry)
        {
            directory.Add(entry);
            return this;
        }

        public override IEnumerator Iterator()
        {
            return directory.GetEnumerator();
        }

        public override void Accept(Visitor v)
        {
            // this は Directoryインスタンス なので Visit(Directory directory) が
            // 呼び出される。
            v.Visit(this);
        }
    }

    public class FileTreatmentException : Exception
    {
        public FileTreatmentException() { }
        public FileTreatmentException(string msg) : base(msg) { }
    }

}
