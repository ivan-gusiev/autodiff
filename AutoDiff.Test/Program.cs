using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using AutoDiff;

namespace AutoDiff.Test
{
    class Program
    {
        const double A = 2;
        const double B = 1;
        const double C = 4;

        static void Main(string[] args)
        {
            Console.WriteLine("Calculating {0}x^2 + {1}x + {2}", A, B, C);

            var x = DualNumber.Variable(ReadDouble());
            var result = Parabola(x);

            Console.WriteLine("F(x) is:\t{0,8}", result.Value);
            Console.WriteLine("F'(x) is:\t{0,8}", result.Derivative);

            if (Debugger.IsAttached)
            {
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Calculates value and derivative of expression in form of Ax^2 + Bx + C.
        /// </summary>
        static DualNumber Parabola(DualNumber variable)
        {
            return variable.Squared() * A + variable * B + C;
        }

        static double ReadDouble()
        {
            while (true)
            {
                Console.Write("Enter a number: ");
                var input = Console.ReadLine();
                double result;

                if (double.TryParse(input, out result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("{0} is not a number.", input);
                }
            }
        }
    }
}
