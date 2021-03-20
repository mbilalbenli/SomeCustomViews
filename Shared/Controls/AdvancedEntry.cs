using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace Plugin.SomeCustomViews.Shared.Controls
{
    /// <summary>
    /// Based On https://github.com/jesulink2514/XamBooksApp
    /// </summary>
    public class AdvancedEntry : Xamarin.Forms.Entry
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


        #region New Bindable Properties

        /// <summaryc>
        /// a property for entry frame corner radius
        /// </summary>
        public static BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(AdvancedEntry), 0);
        public int CornerRadius
        {
            get => (int)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
        
        /// <summary>
        /// a property for entry frame thickness
        /// </summary>
        public static BindableProperty BorderThicknessProperty = BindableProperty.Create(nameof(BorderThickness), typeof(int), typeof(AdvancedEntry), 0);
        public int BorderThickness
        {
            get => (int)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }

        /// <summary>
        /// a property for entry frame padding
        /// </summary>
        public static BindableProperty PaddingProperty = BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(AdvancedEntry), new Thickness(3));
        public Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }

        /// <summary>
        /// a property for entry frame border color
        /// </summary>
        public static BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(AdvancedEntry), Color.Transparent);
        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        /// <summary>
        /// a property for entry underline color (for only android)
        /// </summary>
        public static BindableProperty UnderLineColorProperty = BindableProperty.Create(nameof(UnderLineColor), typeof(Color), typeof(AdvancedEntry), Color.Transparent);
        public Color UnderLineColor
        {
            get => (Color)GetValue(UnderLineColorProperty);
            set => SetValue(UnderLineColorProperty, value);
        }

        /// <summary>
        /// a property for entry mask.
        /// </summary>
        public static readonly BindableProperty MaskProperty = BindableProperty.Create(nameof(Mask), typeof(string), typeof(AdvancedEntry), "");
        public string Mask
        {
            get { return GetValue(MaskProperty).ToString(); }
            set { SetValue(MaskProperty, value); }
        }

        public static BindableProperty UnMaskedTextProperty = BindableProperty.Create(nameof(Mask), typeof(string), typeof(AdvancedEntry), "");
        public string UnMaskedText
        {
            get { return GetValue(UnMaskedTextProperty).ToString(); }
            set { SetValue(UnMaskedTextProperty, value); }
        }

        #endregion

        // Based on https://github.com/TBertuzzi/Xamarin.Forms.MaskedEntry
        #region Mask Entry

        IDictionary<int, char> _Positions;

        private void SetPositions()
        {
            if (string.IsNullOrEmpty(Mask))
            {
                _Positions = null;
                return;
            }

            var list = new Dictionary<int, char>();
            for (var i = 0; i < Mask.Length; i++)
            {
                if (Mask[i] != 'X')
                {
                    list.Add(i, Mask[i]);
                }
            }
            _Positions = list;
        }

        private void RemoveMask(string text)
        {
            if (string.IsNullOrEmpty(Mask))
            {
                _Positions = null;
                return;
            }

            var newString = new List<char>();
            for (int i = 0; i < text.Length; i++)
            {
                if (Mask[i]== 'X')
                {
                    newString.Add(text[i]);
                }
            }
            UnMaskedText = new string(newString.ToArray());
            Console.WriteLine(UnMaskedText);
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            var entry = sender as Xamarin.Forms.Entry;

            var text = entry.Text;

            if (string.IsNullOrWhiteSpace(text) || _Positions == null)
                return;

            if (text.Length > Mask.Length)
            {
                entry.Text = text.Remove(text.Length - 1);
                return;
            }

            foreach (var position in _Positions)
            {
                if (text.Length >= position.Key + 1)
                {
                    var value = position.Value.ToString();

                    if (text.Substring(position.Key, 1) != value)
                    {
                        UnMaskedText = text;
                        text = text.Insert(position.Key, value);
                    }
                }
            }

            RemoveMask(text);

            if (entry.Text != text)
                entry.Text = text;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == MaskProperty.PropertyName)
            {
                SetPositions();

                this.TextChanged -= OnEntryTextChanged;

                if (!string.IsNullOrEmpty(Mask))
                {
                    this.TextChanged += OnEntryTextChanged;
                }
            }
        }

        #endregion
    }
}
