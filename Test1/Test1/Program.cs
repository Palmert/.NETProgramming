using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] dataset = { 84, 21, 46, 84, 99, 1, 76, 56, 84, 37 };
            int max = 0;
            int min = 100;
            int sum = 0;
            double average = 0;
            max = (from data in dataset
                   orderby data descending
                   select data).First();

            Console.Out.WriteLine(max);
            min = (from data in dataset orderby data ascending select data).First();
            Console.Out.WriteLine(min);
            sum = dataset.Sum();
            Console.Out.WriteLine(sum);

            average = dataset.Average();
            Console.Out.WriteLine(average);

            int distinctCount = (from data in dataset select data).Distinct().Count();
            Console.Out.WriteLine(distinctCount);

            var greaterThan50 = (from data in dataset where data > 50 select data);
            foreach (var v in greaterThan50)
            {
                Console.Out.WriteLine(v);
            }

            int first = (from data in dataset select data).First();
            Console.Out.WriteLine(first);

            List<int> dataSet = (from data in dataset select data).ToList();
            foreach (var v in dataSet)
            {
                Console.Out.WriteLine(v);
            }

            var descending = (from data in dataset orderby data descending select data);
            foreach (var v in descending)
            {
                Console.Out.WriteLine(v);
            }

            var topThree = (from data in dataset orderby data ascending select data).Take(3);
            foreach (var v in topThree)
            {
                Console.Out.WriteLine(v);
            }
            Console.ReadLine();
        }
    }
}




