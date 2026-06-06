using System;
using System.Linq;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.WriteLine("\n--- Оберіть завдання (1-2) або 0 для виходу ---");
                Console.Write("Ваш вибір: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": RunTask1(); break;
                    case "2": RunTask2(); break;
                    case "0": return;
                    default: Console.WriteLine("Невірний вибір, спробуйте ще раз."); break;
                }
            }
        }

        static void RunTask1()
        {
            Console.WriteLine("\n=== ЗАВДАННЯ 1: МАСИВ ТРИКУТНИКІВ ===");
            
            Triangle[] triangles = new Triangle[]
            {
                new Triangle(3, 4, 5, 2),
                new Triangle(5, 5, 5, 1),
                new Triangle(6, 8, 10, 3),
                new Triangle(7, 10, 5, 2)
            };

            Console.WriteLine("\n--- Впорядковано за кольорами ---");
            var byColor = triangles.OrderBy(t => t.Color).ToArray();
            PrintTriangleArray(byColor);

            Console.WriteLine("\n--- Впорядковано за площею ---");
            var byArea = triangles.OrderBy(t => t.CalculateArea()).ToArray();
            PrintTriangleArray(byArea);

            Console.WriteLine("\n--- Впорядковано за периметром ---");
            var byPerimeter = triangles.OrderBy(t => t.CalculatePerimeter()).ToArray();
            PrintTriangleArray(byPerimeter);
        }

        static void PrintTriangleArray(Triangle[] array)
        {
            foreach (var t in array)
            {
                t.PrintSides();
                Console.WriteLine($"Периметр: {t.CalculatePerimeter()}, Площа: {t.CalculateArea():F2}, Колір: {t.Color}");
            }
        }

        static void RunTask2()
        {
            Console.WriteLine("\n=== ЗАВДАННЯ 2: ІЄРАРХІЯ КЛАСІВ ===");

            Person[] people = new Person[]
            {
                new Engineer("Олексій", 28, "ІТ", "Senior C# Developer"),
                new Person("Марія", 19),
                new Employee("Іван", 35, "Бухгалтерія"),
                new Worker("Петро", 42, "Цех №1", "Слесар 5 розряду"),
                new Engineer("Анна", 31, "R&D", "Проектувальник")
            };

            var sortedPeople = people.OrderBy(p => p.GetType().Name).ToArray();

            Console.WriteLine("\nВпорядкований масив за типами об'єктів:");
            foreach (var person in sortedPeople)
            {
                person.Show();
                Console.WriteLine();
            }
        }
    }

    class Triangle
    {
        protected int a;
        protected int b;
        protected int c;
        protected int color;

        public Triangle(int a, int b, int c, int color)
        {
            if (!IsValidTriangle(a, b, c))
            {
                this.a = 1;
                this.b = 1;
                this.c = 1;
            }
            else
            {
                this.a = a;
                this.b = b;
                this.c = c;
            }
            this.color = color;
        }

        public int A
        {
            get { return a; }
            set { if (IsValidTriangle(value, b, c)) a = value; }
        }

        public int B
        {
            get { return b; }
            set { if (IsValidTriangle(a, value, c)) b = value; }
        }

        public int C
        {
            get { return c; }
            set { if (IsValidTriangle(a, b, value)) c = value; }
        }

        public int Color
        {
            get { return color; }
        }

        protected bool IsValidTriangle(int sa, int sb, int sc)
        {
            return sa > 0 && sb > 0 && sc > 0 && (sa + sb > sc) && (sa + sc > sb) && (sb + sc > sa);
        }

        public void PrintSides()
        {
            Console.Write($"Трикутник зі сторонами: a={a}, b={b}, c={c} | ");
        }

        public int CalculatePerimeter()
        {
            return a + b + c;
        }

        public double CalculateArea()
        {
            double p = CalculatePerimeter() / 2.0;
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }
    }

    class Person
    {
        protected string name;
        protected int age;

        public Person(string name, int age)
        {
            this.name = name;
            this.age = age;
        }

        public virtual void Show()
        {
            Console.Write($"[Персона] Ім'я: {name}, Вік: {age}");
        }
    }

    class Employee : Person
    {
        protected string department;

        public Employee(string name, int age, string department) : base(name, age)
        {
            this.department = department;
        }

        public override void Show()
        {
            Console.Write($"[Службовець] Ім'я: {name}, Вік: {age}, Відділ: {department}");
        }
    }

    class Worker : Employee
    {
        protected string qualification;

        public Worker(string name, int age, string department, string qualification) : base(name, age, department)
        {
            this.qualification = qualification;
        }

        public override void Show()
        {
            Console.Write($"[Робітник] Ім'я: {name}, Вік: {age}, Відділ: {department}, Кваліфікація: {qualification}");
        }
    }

    class Engineer : Employee
    {
        protected string specialization;

        public Engineer(string name, int age, string department, string specialization) : base(name, age, department)
        {
            this.specialization = specialization;
        }

        public override void Show()
        {
            Console.Write($"[Інженер] Ім'я: {name}, Вік: {age}, Відділ: {department}, Спеціалізація: {specialization}");
        }
    }
}