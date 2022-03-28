using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Gestfac.Views
{
    /// <summary>
    /// Interaction logic for ProductListingView.xaml
    /// </summary>
    public partial class ProductListingView : UserControl
    {
        private static readonly Regex _regexNumbers = new Regex("[^0-9.]+");

        public ProductListingView()
        {
            InitializeComponent();
        }

        private void SelectCurrentPrice(object sender, RoutedEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            if (tb != null)
            {
                tb.SelectAll();
            }
        }

        private void SelectivelyIgnoreMouseButton(object sender,
            MouseButtonEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            if (tb != null)
            {
                if (!tb.IsKeyboardFocusWithin)
                {
                    e.Handled = true;
                    tb.Focus();
                }
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            bool isHandled = true;
            string decimalPoint = ".";
            if (!_regexNumbers.IsMatch(e.Text))
            {
                if (e.Text == decimalPoint)
                {
                    string previosTyped = ((TextBox)sender).Text;
                    isHandled = previosTyped.Contains(decimalPoint);
                }
                else
                {
                    isHandled = false;
                }
            }

            e.Handled = isHandled;
        }
    }
}

