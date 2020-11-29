using System;
using System.Collections.Generic;

namespace CompositePattern
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
                rootdir.PrintList();

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
                rootdir.PrintList();
            }
            catch (FileTreatmentException e)
            {
                e.ToString();
            }
        }
    }

    public class FileTreatmentException : Exception
    {
        public FileTreatmentException() { }
        public FileTreatmentException(string msg) : base(msg) { }
    }

    /// <summary>
    /// Componet役
    /// </summary>
    /// <remarks>
    /// Leaf役とComposite役を同一視するための役
    /// Leaf役とComposite役に共通のスーパークラス
    /// </remarks>
    public abstract class Entry
    {
        public abstract string GetName();
        public abstract int GetSize();
        public virtual Entry Add(Entry entry)
        {
            throw new FileTreatmentException();
        }
        public void PrintList()
        {
            PrintList("");
        }
        internal abstract void PrintList(string prefix);

        public override string ToString()
        {
            return $"{GetName()} ({GetSize()})";
        }
    }

    /// <summary>
    /// Leaf役
    /// </summary>
    /// <remarks>
    /// 「中身」を表す役
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

        internal override void PrintList(string prefix)
        {
            Console.WriteLine($"{prefix}/{this}");
        }
    }

    /// <summary>
    /// Composite役
    /// </summary>
    /// <remarks>
    /// 「容器」を表す役
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

        internal override void PrintList(string prefix)
        {
            Console.WriteLine($"{prefix}/{this}");
            foreach(Entry e in directory)
            {
                e.PrintList($"{prefix}/{name}");
            }
        }
    }
}
