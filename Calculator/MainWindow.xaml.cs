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
        private bool m_userDidInput = false;
        private bool m_CommaSet = false;

        private string m_firstConstant = string.Empty;
        private string m_Symbol = string.Empty;
        private string m_secondConstant = string.Empty;


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
            Logic.FillKeyButtonMap(this);
            InputTextBoxText = "0";
        }

        // User Inputs through Keyboard

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Logic.KeyButtonMapping.TryGetValue(e.Key, out Button pressedButton))
            {
                pressedButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }

            e.Handled = true;
        }

        // Pressing Numbers on the Keypad UI

        private void NumberKeyPad_Click(object sender, RoutedEventArgs e)
        {
            string? clickedButtonName = ((Button)sender).Content.ToString();

            if (clickedButtonName == null)
            {
                return;
            }

            if (InputTextBoxText == "0")
            {
                InputTextBoxText = clickedButtonName;
            }
            else
            {
                InputTextBoxText += clickedButtonName;
            }

            Keyboard.ClearFocus();
            m_userDidInput = true;
        }

        /* Mathematical Operations which take two Inputs
         *  Addition
         *  Subtraction
         *  Multiplication
         *  Division
         *  
         *  Too Add More See Logic.cs / MathCalc
        */

        private void TwoInputOperation_Click(object sender, RoutedEventArgs e)
        {
            string? buttonContent = ((Button)sender).Content.ToString();
            string cleanButtonContentName = buttonContent != null ? buttonContent : string.Empty;

            // Check if OperationButton was pressed before

            if (m_Symbol != string.Empty)
            {
                if (m_userDidInput == false)
                {
                    if (cleanButtonContentName != m_Symbol)
                    {
                        m_Symbol = cleanButtonContentName;
                        HistoryTextBoxText = m_firstConstant + " " + m_Symbol + " ";
                    }

                    e.Handled = true;
                    return;
                }


                m_secondConstant = InputTextBoxText;
                HistoryTextBoxText += " " + m_secondConstant;

                // Do Calculation
                string calculation = Logic.MathTwoInputCalculation(m_firstConstant, m_secondConstant, m_Symbol);

                m_firstConstant = calculation;
                m_secondConstant = string.Empty;
            }
            else
            {
                m_firstConstant = InputTextBoxText;
            }

            m_Symbol = cleanButtonContentName;
            HistoryTextBoxText = m_firstConstant + " " + m_Symbol + " ";

            m_CommaSet = false;
            m_userDidInput = false;
            InputTextBoxText = "0";
            Keyboard.ClearFocus();
            e.Handled = true;
        }

        // Pressing specific Operations on the UI that manupulate in the only one Input

        private void OneInputOperation_Click(object sender, RoutedEventArgs e)
        {
            if (InputTextBoxText == "0")
            {
                e.Handled = true;
                return;
            }

            // Do Calculation
            string calculation = Logic.MathOneInputCalculation(((Button)sender).Name, InputTextBoxText, out string HistoryText);


            // Check on which constant we do operation on
            if (m_Symbol == string.Empty)
            {
                m_firstConstant = HistoryText;
            }
            else
            {
                m_secondConstant = HistoryText;
            }

            // Update History Text Box
            HistoryTextBoxText = m_firstConstant;
            if (m_Symbol != string.Empty)
            {
                HistoryTextBoxText += " " + m_Symbol + " ";
            }
            else
            {
                HistoryTextBoxText += m_Symbol;
            }
            HistoryTextBoxText += m_secondConstant;

            // Show Value in InputBox
            InputTextBoxText = calculation;
            Keyboard.ClearFocus();
            e.Handled = true;
        }

        private void ClearEntryButton_Click(object sender, RoutedEventArgs e)
        {
            // Clear User input

            InputTextBoxText = "0";
            m_CommaSet = false;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            // Clear User Input and History

            History.Clear();
            InputTextBoxText = "0";
            SetMemberValuesToDefault();
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
            if (m_Symbol == string.Empty)
            {
                HistoryTextBoxText = InputTextBoxText + " =";
                return;
            }
            if (InputTextBoxText == string.Empty)
            {
                return;
            }
            if (m_userDidInput == false)
            {
                return;
            }

            m_secondConstant = InputTextBoxText;
            HistoryTextBoxText += m_secondConstant + " = ";

            InputTextBoxText = Logic.MathTwoInputCalculation(m_firstConstant, m_secondConstant, m_Symbol);

            SetMemberValuesToDefault();
            e.Handled = true;
        }

        private void SetMemberValuesToDefault()
        {
              m_userDidInput = false;
              m_CommaSet = false;
              m_firstConstant = string.Empty;
              m_Symbol = string.Empty;
              m_secondConstant = string.Empty;
        }

    }
}