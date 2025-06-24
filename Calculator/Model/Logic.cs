using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Calculator;

namespace Calculator.Model
{
    public class Logic
    {

        private static Dictionary<Key, Button> m_keyButtonMapping = new Dictionary<Key, Button>();


        public static void FillKeyButtonMap(MainWindow mainWindow)
        {
            m_keyButtonMapping.Add(Key.D0, mainWindow.ZeroButton); m_keyButtonMapping.Add(Key.NumPad0, mainWindow.ZeroButton);
            m_keyButtonMapping.Add(Key.D1, mainWindow.OneButton); m_keyButtonMapping.Add(Key.NumPad1, mainWindow.OneButton);
            m_keyButtonMapping.Add(Key.D2, mainWindow.TwoButton); m_keyButtonMapping.Add(Key.NumPad2, mainWindow.TwoButton);
            m_keyButtonMapping.Add(Key.D3, mainWindow.ThreeButton); m_keyButtonMapping.Add(Key.NumPad3, mainWindow.ThreeButton);
            m_keyButtonMapping.Add(Key.D4, mainWindow.FourButton); m_keyButtonMapping.Add(Key.NumPad4, mainWindow.FourButton);
            m_keyButtonMapping.Add(Key.D5, mainWindow.FiveButton); m_keyButtonMapping.Add(Key.NumPad5, mainWindow.FiveButton);
            m_keyButtonMapping.Add(Key.D6, mainWindow.SixButton); m_keyButtonMapping.Add(Key.NumPad6, mainWindow.SixButton);
            m_keyButtonMapping.Add(Key.D7, mainWindow.SevenButton); m_keyButtonMapping.Add(Key.NumPad7, mainWindow.SevenButton);
            m_keyButtonMapping.Add(Key.D8, mainWindow.EightButton); m_keyButtonMapping.Add(Key.NumPad8, mainWindow.EightButton);
            m_keyButtonMapping.Add(Key.D9, mainWindow.NineButton); m_keyButtonMapping.Add(Key.NumPad9, mainWindow.NineButton);

            m_keyButtonMapping.Add(Key.OemComma, mainWindow.CommaButton); m_keyButtonMapping.Add(Key.Decimal, mainWindow.CommaButton);

            m_keyButtonMapping.Add(Key.Enter, mainWindow.ResultButton);
            m_keyButtonMapping.Add(Key.Back, mainWindow.DeleteButton);
            m_keyButtonMapping.Add(Key.Add, mainWindow.AdditionButton);
            m_keyButtonMapping.Add(Key.Subtract, mainWindow.SubtractButton);
            m_keyButtonMapping.Add(Key.Multiply, mainWindow.MultiplyButton);
            m_keyButtonMapping.Add(Key.Divide, mainWindow.DivideButton);
        }

        public static Dictionary<Key, Button> KeyButtonMapping
        {
            get { return m_keyButtonMapping; }
        }

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
