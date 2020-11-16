using System;
using System.Collections.Generic;

namespace _13112020
{
    class Student
    {
        public string PIB { get; set; }
        public DateTime Birthday { get; set; }
        private List<int> marks = new List<int>();// {10, 12, 8, 9, 6};
        public void SetMarks()
        {
            marks.Add(new Random().Next(1, 13));
        }
        public void SetMarksH(int m)
        {
            marks.Add(m);
        }
        public override string ToString()
        {
            // string m = "";
            //foreach (var mark in marks)
            //    m += mark + " ";
            string m = String.Join(" ", marks);
            // return $"|{PIB,15}|{Birthday.ToString("yyyy-MM-dd"),12} |{m}|";
            return $"|{PIB,15}|{Birthday,11:yyyy-MM-dd} |{m,20}|";
        }
    }
    delegate void ExamDelegate();
    class Teacher
    {
        public string PIB { get; set; }
        // public event ExamDelegate ExamEvent;
        public event Action ExamEvent;
        private Action<int>[] arr = new Action<int>[3];
        public event Action<int> Haliava
        {
            add
            {
                int i = 0;
                for (; i < arr.Length; i++)
                    if (arr[i] == null)
                    {
                        arr[i] = value;
                        break;
                    }
                if (value.Target is Student st)
                    Console.WriteLine("->" + st);
                if (i == arr.Length)
                    Console.WriteLine("Халява занята");
            }
            remove
            {
                int i = 0;
                for (; i < arr.Length; i++)
                    if (arr[i] == value)
                    {
                        arr[i] = null;
                        return;// break;
                    }
                //  if (i == arr.Length)
                Console.WriteLine("Такого метода немає");
            }
        }
        public void ExamSet()
        {
            Console.WriteLine($"PIB:{PIB}");
            /*if (ExamEvent != null)
                ExamEvent();*/
            ExamEvent?.Invoke();
        }
        public void ExamH()
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i]?.Invoke(5);
        }
    }

    class Program
    {
        // event
        static void Task1()
        {
            Student Ivan = new Student { PIB = "Ivan", Birthday = new DateTime(1989, 10, 23) };
            Student Petro = new Student { PIB = "Petro", Birthday = new DateTime(1989, 10, 23) };
            Console.WriteLine(Ivan);
            Console.WriteLine(Petro);
            for (int i = 0; i < 10; i++)
            {
                Ivan.SetMarks();
                Petro.SetMarks();
            }

            Console.WriteLine(Ivan);
            Console.WriteLine(Petro);
        }
        static void Task2()
        {
            Student Ivan = new Student { PIB = "Ivan", Birthday = new DateTime(1989, 10, 23) };
            Student Petro = new Student { PIB = "Petro", Birthday = new DateTime(1989, 10, 23) };
            Console.WriteLine(Ivan);
            Console.WriteLine(Petro);
            Console.WriteLine("----------------------------------");
            Teacher Inna = new Teacher() { PIB = "Inna" };
            Inna.ExamEvent += Petro.SetMarks;
            Inna.ExamEvent += Ivan.SetMarks;
            Inna.ExamSet();
            Console.WriteLine(Ivan);
            Console.WriteLine(Petro);
            Console.WriteLine("----------------------------------");

            Inna.ExamEvent -= Petro.SetMarks;
            Inna.ExamSet();
            Console.WriteLine(Ivan);
            Console.WriteLine(Petro);
            //Teacher Anna = new Teacher() { PIB = "Anna"};
            //Anna.ExamSet();
            Teacher Anna = new Teacher() { PIB = "Anna" };
            Anna.ExamEvent += Anna_ExamEvent;
            Anna.ExamEvent += Petro.SetMarks;
            Anna.ExamEvent += () => Console.WriteLine("End exam"); ;
            Anna.ExamSet();
            Console.WriteLine(Ivan);
            Console.WriteLine(Petro);
        }
        static void Task3()
        {
            Student[] gr =
            {
                new Student { PIB = "Ivan", Birthday = new DateTime(1989, 10, 23) },
                 new Student { PIB = "Petro", Birthday = new DateTime(2000, 10, 23) },
                 new Student { PIB = "Stepan", Birthday = new DateTime(2003, 10, 23) },
                new Student { PIB = "Igor", Birthday = new DateTime(2002, 10, 23) }
            };
            foreach (var item in gr)
                Console.WriteLine(item);
            Console.WriteLine("----------------------------------");
            Teacher Inna = new Teacher() { PIB = "Inna" };
            foreach (var VARIABLE in gr)
                Inna.Haliava += VARIABLE.SetMarksH;
            Inna.Haliava -= gr[1].SetMarksH;
            Inna.Haliava -= gr[3].SetMarksH;
            Inna.Haliava += gr[3].SetMarksH;

            Inna.ExamH();
            foreach (var item in gr)
                Console.WriteLine(item);
            Console.WriteLine("----------------------------------");


        }
        private static void Anna_ExamEvent()
        {
            Console.WriteLine("Start exam");
        }
        static void Main(string[] args)
        {
            //  Task1();
            //Task2();
            Task3();
        }
    }
}