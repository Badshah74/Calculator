using Calculator.Model;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HashSet<Key> m_validKeyStrokes = new HashSet<Key>();

        private bool m_CommaSet = false;
        private bool m_OperationSet = false;

        public string HistoryTextBoxText
        {
            get => History.Text;
            set => History.Text = value;
        }

        public string InputTextBoxText
        {
            get => Input.Text;
            set => Input.Text = value;
        }



        public MainWindow()
        {
            InitializeComponent();
            FillMapping();
            InputTextBoxText = "0";
        }

        private void FillMapping()
        {
            m_validKeyStrokes.Add(Key.D0);
            m_validKeyStrokes.Add(Key.D1);
            m_validKeyStrokes.Add(Key.D2);
            m_validKeyStrokes.Add(Key.D3);
            m_validKeyStrokes.Add(Key.D4);
            m_validKeyStrokes.Add(Key.D5);
            m_validKeyStrokes.Add(Key.D6);
            m_validKeyStrokes.Add(Key.D7);
            m_validKeyStrokes.Add(Key.D8);
            m_validKeyStrokes.Add(Key.D9);

            m_validKeyStrokes.Add(Key.NumPad0);
            m_validKeyStrokes.Add(Key.NumPad1);
            m_validKeyStrokes.Add(Key.NumPad2);
            m_validKeyStrokes.Add(Key.NumPad3);
            m_validKeyStrokes.Add(Key.NumPad4);
            m_validKeyStrokes.Add(Key.NumPad5);
            m_validKeyStrokes.Add(Key.NumPad6);
            m_validKeyStrokes.Add(Key.NumPad7);
            m_validKeyStrokes.Add(Key.NumPad8);
            m_validKeyStrokes.Add(Key.NumPad9);

            m_validKeyStrokes.Add(Key.OemComma);
            m_validKeyStrokes.Add(Key.Decimal);

            m_validKeyStrokes.Add(Key.Multiply);
            m_validKeyStrokes.Add(Key.Divide);
            m_validKeyStrokes.Add(Key.Subtract);
            m_validKeyStrokes.Add(Key.Add);

            m_validKeyStrokes.Add(Key.Enter);
            m_validKeyStrokes.Add(Key.Return);
            m_validKeyStrokes.Add(Key.Back);
        }

        private void UpdateTextBox(Key key)
        {
            if (key >= Key.D0 && key <= Key.D9)
            {
                if (Input.Text == "0")
                {
                    Input.Text = ((int)(key - 34)).ToString();
                }

                Input.Text += (int)(key - 34);
            }
            else if (key >= Key.NumPad0 && key <= Key.NumPad9)
            {
                Input.Text += (int)(key - 74);
            }
            else if (key == Key.OemComma || key == Key.Decimal)
            {
                Input.Text += ",";
            }
            else if (key == Key.Enter && key == Key.Return)
            {

            }
            else if (key == Key.Back)
            {
                if (Input.Text == "0") return;

                Input.Text = Input.Text[..^1];

                if (Input.Text == String.Empty)
                {
                    Input.Text = "0";
                }
            }
            else
            {
                switch (key)
                {
                    case Key.Multiply:
                        {
                            Input.Text += " " + "*" + " ";
                            break;
                        }
                    case Key.Divide:
                        {
                            Input.Text += " " + "*" + " ";
                            break;
                        }
                    case Key.Subtract:
                        {
                            Input.Text += " " + "/" + " ";
                            break;
                        }
                    case Key.Add:
                        {
                            Input.Text += " " + "+" + " ";
                            break;
                        }
                    default: break;

                }
            }
        }


        // User Inputs through Keyboard

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (!m_validKeyStrokes.Contains(e.Key))
            {
                e.Handled = true;
            }

            UpdateTextBox(e.Key);
        }

        // Pressing Numbers on the Keypad UI

        private void NumberKeyPad_Click(object sender, RoutedEventArgs e)
        {
            string? clickedButtonName = ((Button)sender).Content.ToString();
            if (clickedButtonName == null || Input.Text == "0" && clickedButtonName == "0")
            {
                e.Handled = true;
            }
            else
            {
                if (InputTextBoxText == "0")
                {
                    InputTextBoxText = clickedButtonName;
                }
                else
                {
                    InputTextBoxText += clickedButtonName;
                }
            }
        }

        // Pressing specific Operations on the UI

        private void PercentButton_Click(object sender, RoutedEventArgs e)
        {
            // Divide by 100 the user input

            // Operating on the user input

            if (InputTextBoxText == "0")
            {
                e.Handled = true;
                return;
            }

            InputTextBoxText = (decimal.Parse(InputTextBoxText) / 100m).ToString();
            e.Handled = true;
        }

        private void ClearEntryButton_Click(object sender, RoutedEventArgs e)
        {
            // Clear User input

            Input.Clear();
            InputTextBoxText = "0";
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            // Clear User Input and History

            Input.Clear();
            History.Clear();
            InputTextBoxText = "0";
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (InputTextBoxText == "0") return;
            if (InputTextBoxText[^1] == ',') m_CommaSet = false;

            InputTextBoxText = InputTextBoxText[..^1];

            if (InputTextBoxText == String.Empty)
            {
                InputTextBoxText = "0";
            }
        }

        private void OneFractionButton_Click(object sender, RoutedEventArgs e)
        {
            // Operating on the user input

            if (InputTextBoxText == "0")
            {
                e.Handled = true;
                return;
            }

            InputTextBoxText = (1m / decimal.Parse(InputTextBoxText)).ToString();
            e.Handled = true;
        }

        private void SquaredButton_Click(object sender, RoutedEventArgs e)
        {
            // Operating on the user input

            HistoryTextBoxText += "sqr(" + InputTextBoxText + ")";

            InputTextBoxText = ((decimal)Math.Pow((double)decimal.Parse(InputTextBoxText), 2)).ToString();
            e.Handled = true;
        }

        private void SquaredRootButton_Click(object sender, RoutedEventArgs e)
        {
            // Operating on the user input

            HistoryTextBoxText += "\u221A(" + InputTextBoxText + ")";

            InputTextBoxText = ((decimal)Math.Sqrt((double)decimal.Parse(InputTextBoxText))).ToString();
            e.Handled = true;
        }

        private void DivideButton_Click(object sender, RoutedEventArgs e)
        {
            // Take new User input

            if (m_OperationSet)
            {
                if (HistoryTextBoxText[^2] == '-' && InputTextBoxText == "0") return;

                HistoryTextBoxText += InputTextBoxText;
                HistoryTextBoxText = Logic.MathCalc(HistoryTextBoxText.Split(" ")) + " - ";
            }
            else
            {
                m_OperationSet = true;
                HistoryTextBoxText += InputTextBoxText + " - ";
            }

            InputTextBoxText = "0";
        }

        private void MultiplyButton_Click(object sender, RoutedEventArgs e)
        {
            // Take new User input

            if (m_OperationSet)
            {
                if (HistoryTextBoxText[^2] == '-' && InputTextBoxText == "0") return;

                HistoryTextBoxText += InputTextBoxText;
                HistoryTextBoxText = Logic.MathCalc(HistoryTextBoxText.Split(" ")) + " - ";
            }
            else
            {
                m_OperationSet = true;
                HistoryTextBoxText += InputTextBoxText + " - ";
            }

            InputTextBoxText = "0";
        }

        private void SubtractButton_Click(object sender, RoutedEventArgs e)
        {
            // Take new User input

            if (m_OperationSet)
            {
                if (HistoryTextBoxText[^2] == '-' && InputTextBoxText == "0") return;

                HistoryTextBoxText += InputTextBoxText;
                HistoryTextBoxText = Logic.MathCalc(HistoryTextBoxText.Split(" ")) + " - ";
            }
            else
            {
                m_OperationSet = true;
                HistoryTextBoxText += InputTextBoxText + " - ";
            }

            InputTextBoxText = "0";
        }

        private void AdditionButton_Click(object sender, RoutedEventArgs e)
        {
            // Take new User input
            /*
             *  Take Text from Input Text box add + take second input 
             *  If there are 2 values in History and + do calcutation
             *  Calculation add + Symbol wait for user input
             *  Handle multiple times of + presses      X
             *  
            */


            if (m_OperationSet)
            {
                if (HistoryTextBoxText[^2] == '+' && InputTextBoxText == "0") return;

                HistoryTextBoxText += InputTextBoxText;
                HistoryTextBoxText = Logic.MathCalc(HistoryTextBoxText.Split(" ")) + " + ";
            }
            else
            {
                m_OperationSet = true;
                HistoryTextBoxText += InputTextBoxText + " + ";
            }

            InputTextBoxText = "0";
        }

        private void SignButton_Click(object sender, RoutedEventArgs e)
        {
            if (InputTextBoxText == "0")
            {
                return;
            }

            if (InputTextBoxText[0] == '-')
            {
                InputTextBoxText = InputTextBoxText[1..];
            }
            else
            {
                InputTextBoxText = '-' + InputTextBoxText;
            }
        }

        private void CommaButton_Click(object sender, RoutedEventArgs e)
        {
            if (!m_CommaSet)
            {
                InputTextBoxText += ',';
                m_CommaSet = true;
            }
        }

        private void ResultButton_Click(object sender, RoutedEventArgs e)
        {
            if (HistoryTextBoxText == string.Empty)
            {
                HistoryTextBoxText = InputTextBoxText + " =";
            }
            else
            {

            }
        }
    }
}