using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator;

namespace Calculator.Model
{
    public class Logic
    {





        public static string MathCalc(string[] strings)
        {
            if (strings.Length != 3)
            {
                throw new ArgumentException("string array not 3");
            }

            decimal first = decimal.Parse(strings[0]);
            decimal second = decimal.Parse(strings[2]);
            decimal solution = 0;


            switch(strings[1])
            {
                case "+":
                    {
                        solution = first + second;
                        break;
                    }
                case "-":
                    {
                        solution = first - second;
                        break;
                    }
                case "*":
                    {
                        solution = first * second;
                        break;
                    }
                case "/":
                    {
                        solution = first / second;
                        break;
                    }
                default: break;
            }

            return solution.ToString();
        }
    }
}
