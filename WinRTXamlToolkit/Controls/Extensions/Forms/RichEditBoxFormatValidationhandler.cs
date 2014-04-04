using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace WinRTXamlToolkit.Controls.Extensions.Forms
{
    public class RichEditBoxFormatValidationHandler : FieldValidationHandler<RichEditBox>
    {
        internal void Detach()
        {
            Field.TextChanged -= OnTextBoxTextChanged;
            Field.Loaded -= OnTextBoxLoaded;
            Field = null;
        }

        internal void Attach(RichEditBox textBox)
        {
            if (Field == textBox)
            {
                return;
            }

            if (Field != null)
            {
                this.Detach();
            }

            Field = textBox;
            Field.TextChanged += OnTextBoxTextChanged;
            Field.Loaded += OnTextBoxLoaded;

            this.Validate();
        }

        private void OnTextBoxLoaded(object sender, RoutedEventArgs e)
        {
            this.Validate();
        }

        private void OnTextBoxTextChanged(object sender, RoutedEventArgs e)
        {
            Field.TextChanged -= OnTextBoxTextChanged;
            this.Validate();
            Field.TextChanged += OnTextBoxTextChanged;
        }

        /// <summary>
        /// Gets the field value.
        /// </summary>
        /// <returns></returns>
        protected override string GetFieldValue()
        {
            string text;
            Field.Document.GetText(TextGetOptions.None, out text);
            return text;
        }

        /// <summary>
        /// Gets the max length of the form field.
        /// </summary>
        /// <returns></returns>
        protected override int GetMaxLength()
        {
            return GetFieldValue().Length;
        }
    }
}
