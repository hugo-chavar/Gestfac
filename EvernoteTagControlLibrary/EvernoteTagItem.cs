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
using Windows.UI.Xaml.Controls;

namespace EvernoteTagControlLibrary
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:EvernoteTagControlLibrary"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:EvernoteTagControlLibrary;assembly=EvernoteTagControlLibrary"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    [TemplatePart(Name = "PART_InputBox", Type = typeof(AutoSuggestBox))]
    [TemplatePart(Name = "PART_DeleteTagButton", Type = typeof(System.Windows.Controls.Button))]
    [TemplatePart(Name = "PART_TagButton", Type = typeof(System.Windows.Controls.Button))]
    public class EvernoteTagItem : System.Windows.Controls.Control
    {
        static EvernoteTagItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EvernoteTagItem), new FrameworkPropertyMetadata(typeof(EvernoteTagItem)));
        }

        public EvernoteTagItem() { }
        public EvernoteTagItem(string text)
            : this()
        {
            this.Text = text;
        }

        // Text
        public string Text { get { return (string)GetValue(TextProperty); } set { SetValue(TextProperty, value); } }
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(EvernoteTagItem), new PropertyMetadata(null));

        // IsEditing, readonly
        public bool IsEditing { get { return (bool)GetValue(IsEditingProperty); } internal set { SetValue(IsEditingPropertyKey, value); } }
        private static readonly DependencyPropertyKey IsEditingPropertyKey = DependencyProperty.RegisterReadOnly("IsEditing", typeof(bool), typeof(EvernoteTagItem), new FrameworkPropertyMetadata(false));
        public static readonly DependencyProperty IsEditingProperty = IsEditingPropertyKey.DependencyProperty;

        /// <summary>
        /// Wires up delete button click and focus lost 
        /// </summary>
        public override void OnApplyTemplate()
        {
            //AutoSuggestBox inputBox = this.GetTemplateChild("PART_InputBox") as AutoSuggestBox;
            //if (inputBox != null)
            //{
            //    inputBox.LostFocus += inputBox_LostFocus;
            //    inputBox.Loaded += inputBox_Loaded;
            //}

            System.Windows.Controls.Button btn = this.GetTemplateChild("PART_TagButton") as System.Windows.Controls.Button;
            if (btn != null)
            {
                btn.Loaded += (s, e) =>
                {
                    System.Windows.Controls.Button b = s as System.Windows.Controls.Button;
                    var btnDelete = b.Template.FindName("PART_DeleteTagButton", b) as System.Windows.Controls.Button; // will only be found once button is loaded
                    if (btnDelete != null)
                    {
                        btnDelete.Click -= btnDelete_Click; // make sure the handler is applied just once
                        btnDelete.Click += btnDelete_Click;
                    }
                };

                btn.Click += (s, e) =>
                {
                    var parent = GetParent();
                    if (parent != null)
                        parent.RaiseTagClick(this); // raise the TagClick event of the EvernoteTagControl
                };
            }

            base.OnApplyTemplate();
        }

        /// <summary>
        /// Handles the click on the delete glyph of the tag button.
        /// Removes the tag from the collection.
        /// </summary>
        void btnDelete_Click(object sender, RoutedEventArgs e)
        {

            var item = FindUpVisualTree<EvernoteTagItem>(sender as FrameworkElement);
            var parent = GetParent();
            if (item != null && parent != null)
                parent.RemoveTag(item);

            e.Handled = true; // bubbling would raise the tag click event
        }

        /// <summary>
        /// When an AutoCompleteBox is created, set the focus to the textbox.
        /// Wire PreviewKeyDown event to handle Escape/Enter keys
        /// </summary>
        /// <remarks>AutoCompleteBox.Focus() is broken: http://stackoverflow.com/questions/3572299/autocompletebox-focus-in-wpf</remarks>
        void inputBox_Loaded(object sender, RoutedEventArgs e)
        {
            AutoSuggestBox acb = sender as AutoSuggestBox;

            acb.Focus(Windows.UI.Xaml.FocusState.Keyboard);

           
            //if (acb != null)
            //{
            //    var tb = acb.Template.("Text", acb) as System.Windows.Controls.TextBox;
            //    if (tb != null)
            //        tb.Focus();

            //    // PreviewKeyDown, because KeyDown does not bubble up for Enter
            //    acb.PreviewKeyDown += (s, e1) =>
            //    {
            //        var parent = GetParent();
            //        if (parent != null)
            //        {
            //            switch (e1.Key)
            //            {
            //                case (Windows.System.VirtualKey.Enter):  // accept tag
            //                    parent.Focus();
            //                    break;
            //                case (Windows.System.VirtualKey.Escape): // reject tag
            //                    parent.Focus();
            //                    parent.RemoveTag(this, true); // do not raise RemoveTag event
            //                    break;
            //            }
            //        }
            //    };
            //}
        }

        /// <summary>
        /// Set IsEditing to false when the AutoCompleteBox loses keyboard focus.
        /// This will change the template, displaying the tag as a button.
        /// </summary>
        void inputBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.IsEditing = false;
            var parent = GetParent();
            if (parent != null)
                parent.IsEditing = false;
        }

        private EvernoteTagControl GetParent()
        {
            return FindUpVisualTree<EvernoteTagControl>(this);
        }

        /// <summary>
        /// Walks up the visual tree to find object of type T, starting from initial object
        /// http://www.codeproject.com/Tips/75816/Walk-up-the-Visual-Tree
        /// </summary>
        private static T FindUpVisualTree<T>(DependencyObject initial) where T : DependencyObject
        {
            DependencyObject current = initial;
            while (current != null && current.GetType() != typeof(T))
            {
                current = VisualTreeHelper.GetParent(current);
            }
            return current as T;
        }
    }
}

