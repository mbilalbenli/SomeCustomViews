using Plugin.SomeCustomViews.Shared.Helpers;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Plugin.SomeCustomViews.Shared.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerifCodeView : ContentView
    {
        ColorFader colorFader = new ColorFader();

        public VerifCodeView()
        {
            InitializeComponent();

            //BindingContext = this;
        }

        public static readonly BindableProperty SizeProperty = BindableProperty.Create(nameof(Size), typeof(double), typeof(VerifCodeView), 40.0d, propertyChanged: OnVerifCodeViewChanged);
    
        public static BindableProperty UnFocusedBorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(VerifCodeView), Color.Transparent, propertyChanged: OnVerifCodeViewChanged);

        public static BindableProperty FocusedBorderColorProperty = BindableProperty.Create(nameof(FocusedBorderColor), typeof(Color), typeof(VerifCodeView), Color.Transparent, propertyChanged: OnVerifCodeViewChanged);           
        public static BindableProperty BoxBackgroundColorProperty = BindableProperty.Create(nameof(BoxBackgroundColor), typeof(Color), typeof(VerifCodeView), Color.White, propertyChanged: OnVerifCodeViewChanged);

        public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(nameof(FontAttributes), typeof(FontAttributes), typeof(VerifCodeView), FontAttributes.None);

        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(VerifCodeView), string.Empty);

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(VerifCodeView), Device.GetNamedSize(NamedSize.Medium, typeof(Label)));

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(VerifCodeView), Color.Default);

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(VerifCodeView), string.Empty);

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(double), typeof(VerifCodeView), 5.0d);

        public static readonly BindableProperty BoxSpacingProperty = BindableProperty.Create(nameof(BoxSpacing), typeof(double), typeof(VerifCodeView), 2.5);


        public double BoxSpacing
        {
            get => (double)GetValue(BoxSpacingProperty);
            set => SetValue(BoxSpacingProperty, value);
        }
        public double CornerRadius
        {
            get => (double)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public FontAttributes FontAttributes
        {
            get => (FontAttributes)GetValue(FontAttributesProperty);
            set => SetValue(FontAttributesProperty, value);
        }

        public string FontFamily
        {
            get => (string)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        public double FontSize
        {
            get
            {
                var value = (double)GetValue(FontSizeProperty);

                if (Size-15<value)
                {
                    return Size - 15;
                }
                else
                {
                    return value;
                }
            }
            set => SetValue(FontSizeProperty, value);
        }

        public Color BoxBackgroundColor
        {
            get => (Color)GetValue(BoxBackgroundColorProperty);
            set => SetValue(BoxBackgroundColorProperty, value);
        }

        public Color FocusedBorderColor
        {
            get => (Color)GetValue(FocusedBorderColorProperty);
            set => SetValue(FocusedBorderColorProperty, value);
        }
        public Color BorderColor
        {
            get => (Color)GetValue(UnFocusedBorderColorProperty);
            set => SetValue(UnFocusedBorderColorProperty, value);
        }

        public double Size
        {
            get => (double)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        private static void OnVerifCodeViewChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as VerifCodeView)?.UpdateView();
        }

        private void UpdateView()
        {
            foreach (var children in MainGrid.Children)
            {
                if (children is Frame frame)
                {
                    frame.WidthRequest = Size;
                    frame.HeightRequest = Size;
                }
            }
        }




        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var oldText = e.OldTextValue;
            var newText = e.NewTextValue;
            if (oldText == null) oldText = "";
            if (newText == null) newText = "";

            var cp = (sender as Entry).CursorPosition;

            if (codeEntry.Text.Length>6)
            {
                codeEntry.Text = codeEntry.Text.Substring(0, 6);
                for (int i = 0; i < 6; i++)
                {
                    var label = MainGrid.FindByName("Digit" + (i + 1)) as Label;
                    label.Text = codeEntry.Text[i].ToString();
                }
                return;
            }

            if (newText.Length > oldText.Length && cp < 6)
            {
                var label = MainGrid.FindByName("Digit" + (cp + 1)) as Label;

                label.Text = e.NewTextValue.Substring(cp, 1);


                (MainGrid.Children[cp + 1] as Frame).BorderColor = FocusedBorderColor;

                if (cp != 0)
                {
                    (MainGrid.Children[cp] as Frame).BorderColor = BorderColor;
                }
            }
            else if (newText.Length < 7 && oldText.Length < 7)
            {
                var label = MainGrid.FindByName("Digit" + cp) as Label;
                label.Text = "";
                if (cp != 6)
                {
                    (MainGrid.Children[cp + 1] as Frame).BorderColor = BorderColor;
                }

                if (cp != 1)
                {
                    (MainGrid.Children[cp] as Frame).BorderColor = FocusedBorderColor;
                }
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var frame = sender as Frame;
            var index = Convert.ToInt32(frame.AutomationId);

            //codeEntry.Focus();


            if (codeEntry.Text == null)
            {
                (MainGrid.Children[1] as Frame).BorderColor = FocusedBorderColor;

            }
            else if (codeEntry.Text.Length >= index)
            {
                foreach (var item in MainGrid.Children)
                {
                    if (item is Frame oldFrame)
                    {
                        oldFrame.BorderColor = BorderColor;
                    }
                }
                codeEntry.CursorPosition = index;
                (MainGrid.Children[index] as Frame).BorderColor = FocusedBorderColor;
                codeEntry.Focus();
            }
            else if (codeEntry.Text.Length > index)
            {
                codeEntry.CursorPosition = index;
                (MainGrid.Children[index] as Frame).BorderColor = FocusedBorderColor;
            }
            else
            {
                codeEntry.Focus();
            }
            codeEntry.Focus();

        }
    }
}