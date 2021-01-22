using System;
using Xamarin.Forms;

namespace Plugin.SomeCustomViews.Shared.Controls
{
    /// <summary>
    /// Based On https://github.com/jesulink2514/XamBooksApp
    /// </summary>
    public class AdvancedEntry : Entry
    {


        /// <summary>
        /// Some Events
        /// </summary>
        #region Events 

        public delegate void BackspaceEventHandler(object sender, EventArgs e);

        public event BackspaceEventHandler OnBackspace;

        public void OnBackspacePressed()
        {
            if (OnBackspace != null)
            {
                OnBackspace(this, null);
            }
        }

        #endregion

        /// <summary>
        /// Used Bindable Properties
        /// </summary>
        #region New Bindable Properties

        public static BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(AdvancedEntry), 0);
        public int CornerRadius
        {
            get => (int)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static BindableProperty BorderThicknessProperty = BindableProperty.Create(nameof(BorderThickness), typeof(int), typeof(AdvancedEntry), 0);
        public int BorderThickness
        {
            get => (int)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }

        public static BindableProperty PaddingProperty = BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(AdvancedEntry), new Thickness(3));
        public Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }

        public static BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(AdvancedEntry), Color.Transparent);
        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        #endregion
    }
}
