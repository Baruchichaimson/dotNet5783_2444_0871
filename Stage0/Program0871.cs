using System;

namespace  Program0871
{
partial class Program
    {

        static void Main(string[] args)
        {
            Welcome0871();
            Welcome2444();
            Console.ReadKey();

        }
        static partial void Welcome2444();
        private static void Welcome0871()
        {
            Console.Write("Enter your name: ");
            string username = Console.ReadLine();
            Console.WriteLine("{0} , welcome to first console project", username);
        }
    }


}