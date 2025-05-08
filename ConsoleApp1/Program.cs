using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Message message = new Message();
            //message.Name = "Test";
            //message.Password = "232";
            //message.Email = "Test@gmail.com";

            //Console.WriteLine(message.Name = "Test");
            //Console.ReadLine();

            ICollection<string> fruits = new List<string>();
            fruits.Add("Elma");
            fruits.Add("Ayva");

            Console.WriteLine(fruits.Count);
            foreach (string fruit in fruits)
            {
                Console.WriteLine(fruit);
            }

            List<string> balls = new List<string>();
            balls.Add("big ball");
            balls.Add("small ball");

            Console.Write(balls[0]);
            Console.ReadLine();
        }
    }
    public class User
    {
        public string Name { get; set; }
        public string Password { get; set; }

    }
    public class Message : User
    {
        public string Email { get; set; }
    }
}
