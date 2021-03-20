using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Plugin.SomeCustomViews.Shared.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FramedEntry : Frame
    {
        public FramedEntry()
        {
            InitializeComponent();

            BindingContext = this;
        }

        #region Events
        public event EventHandler<TextChangedEventArgs> TextChanged;
        public event EventHandler<FocusEventArgs> Focused;
        public event EventHandler<FocusEventArgs> Unfocused;
        #endregion
        #region Bindable Properties 
        public static readonly BindableProperty CharacterSpacingProperty = BindableProperty.Create(nameof(CharacterSpacing), typeof(double), typeof(FramedEntry), 0.0d);

        // TODO: Font interface'i hazırla. 
        public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(nameof(FontAttributes), typeof(FontAttributes), typeof(FramedEntry), FontAttributes.None);

        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(FramedEntry), string.Empty);

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(FramedEntry), Device.GetNamedSize(NamedSize.Medium, typeof(Label)));

        public static readonly BindableProperty HorizontalTextAlignmentProperty = BindableProperty.Create(nameof(HorizontalTextAlignment), typeof(TextAlignment), typeof(FramedEntry), TextAlignment.Start);

        public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(FramedEntry), false);

        public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(FramedEntry), Keyboard.Default);

        public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create(nameof(MaxLength), typeof(int), typeof(FramedEntry), int.MaxValue);

        // TODO: PlaceHoler interface'i hazırla. 

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(FramedEntry), string.Empty);

        public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(FramedEntry), Color.Default);

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(FramedEntry), string.Empty);

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(FramedEntry), Color.Black);

        public static readonly BindableProperty VerticalTextAlignmentProperty = BindableProperty.Create(nameof(VerticalTextAlignment), typeof(TextAlignment), typeof(FramedEntry), TextAlignment.Start);

        public static readonly BindableProperty MaskProperty = BindableProperty.Create(nameof(Mask), typeof(string), typeof(FramedEntry), "");

        public static readonly BindableProperty UnMaskedTextProperty = BindableProperty.Create(nameof(Mask), typeof(string), typeof(FramedEntry), "");

        public static readonly BindableProperty EntryPaddingProperty = BindableProperty.Create(nameof(EntryPadding), typeof(Thickness), typeof(FramedEntry), new Thickness(10));

        public static BindableProperty PaddingProperty = BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(FramedEntry), new Thickness(10));

        public Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
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

        public double CharacterSpacing
        {
            get => (double)GetValue(CharacterSpacingProperty);
            set => SetValue(CharacterSpacingProperty, value);
        }

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public Color PlaceholderColor
        {
            get => (Color)GetValue(PlaceholderColorProperty);
            set => SetValue(PlaceholderColorProperty, value);
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
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public int MaxLength
        {
            get => (int)GetValue(MaxLengthProperty);
            set => SetValue(MaxLengthProperty, value);
        }

        public string Mask
        {
            get => GetValue(MaskProperty).ToString();
            set => SetValue(MaskProperty, value);
        }

        public string UnMaskedText
        {
            get => GetValue(UnMaskedTextProperty).ToString();
            set => SetValue(UnMaskedTextProperty, value);
        }
        public Thickness EntryPadding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }

        public TextAlignment VerticalTextAlignment
        {
            get => (TextAlignment)GetValue(VerticalTextAlignmentProperty);
            set => SetValue(VerticalTextAlignmentProperty, value);
        }

        public TextAlignment HorizontalTextAlignment
        {
            get => (TextAlignment)GetValue(HorizontalTextAlignmentProperty);
            set => SetValue(HorizontalTextAlignmentProperty, value);
        }

        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }

        public Keyboard Keyboard
        {
            get => (Keyboard)GetValue(KeyboardProperty);
            set => SetValue(KeyboardProperty, value);
        }

        #endregion

        private void EntryView_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextChanged?.Invoke(sender, e);
        }

        private void EntryView_Focused(object sender, FocusEventArgs e)
        {
            if (Focused != null)
            {
                Focused.Invoke(sender, e);

            }
        }

        private void EntryView_Unfocused(object sender, FocusEventArgs e)
        {
            if (Unfocused != null)
            {
                Unfocused.Invoke(sender, e);
            }
        }


    }
}