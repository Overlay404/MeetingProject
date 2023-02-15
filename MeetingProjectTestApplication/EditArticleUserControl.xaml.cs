using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MeetingProjectTestApplication
{
    /// <summary>
    /// Логика взаимодействия для EditArticleUserControl.xaml
    /// </summary>
    public partial class EditArticleUserControl : UserControl
    {


        public string MdText
        {
            get { return (string)GetValue(MdTextProperty); }
            set { SetValue(MdTextProperty, value); }
        }

        public static readonly DependencyProperty MdTextProperty =
            DependencyProperty.Register("MdText", typeof(string), typeof(EditArticleUserControl));



        public EditArticleUserControl()
        {
            MdText = "Начните вводить";
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SymbolGenerate("# ", false);
        }

        private void SymbolGenerate(string symbol, bool isDoublegenerate)
        {
            string textLine = EditTextBox.GetLineText(EditTextBox.GetLineIndexFromCharacterIndex(EditTextBox.SelectionStart));
            if (textLine.Contains(symbol) || textLine == "" || textLine == Environment.NewLine) return;

            string fullText = EditTextBox.Text;
            int cursorPosition = EditTextBox.SelectionStart;
            if(isDoublegenerate)
            {
                EditTextBox.Text = fullText.Replace(textLine, symbol + textLine + symbol);
                EditTextBox.SelectionStart = cursorPosition;
                return;
            }
            EditTextBox.Text = fullText.Replace(textLine, symbol + textLine);
            EditTextBox.SelectionStart = cursorPosition;
        }

        private void Border_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            SymbolGenerate("*", true);
        }

        private void Border_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            SymbolGenerate("~", true);
        }
    }
}
