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



        public static string MathCalc(string firstterm, string secondterm, string operation)
        {
            decimal first = decimal.Parse(firstterm);
            decimal second = decimal.Parse(secondterm);
            decimal solution = 0;


            switch(operation)
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
                case "x":
                    {
                        solution = first * second;
                        break;
                    }
                case "/":
                    {
                        solution = first / second;
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Something went wrong! Opertion doesnt exist"); 
                        break;
                    }


            }

            return solution.ToString();
        }
    }
}
