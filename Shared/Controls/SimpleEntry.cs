using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Plugin.SomeCustomViews.Shared.Controls
{
    public class SimpleEntry : Entry
    {
        public SimpleEntry()
        {
            this.TextChanged += SimpleEntry_TextChanged;
        }

        public static readonly BindableProperty MaskProperty = BindableProperty.Create(nameof(Mask), typeof(string), typeof(SimpleEntry), "");

        public static readonly BindableProperty UnMaskedTextProperty = BindableProperty.Create(nameof(Mask), typeof(string), typeof(SimpleEntry), "");

        public static BindableProperty PaddingProperty = BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(SimpleEntry), new Thickness(10));
        
        public Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
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


        private void SimpleEntry_TextChanged(object sender, TextChangedEventArgs e)
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


        #region Mask

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
                if (Mask[i] == 'X')
                {
                    newString.Add(text[i]);
                }
            }
            UnMaskedText = new string(newString.ToArray());
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == MaskProperty.PropertyName)
            {
                SetPositions();

                this.TextChanged -= SimpleEntry_TextChanged;

                if (!string.IsNullOrEmpty(Mask))
                {
                    this.TextChanged += SimpleEntry_TextChanged;
                }
            }
        }

        #endregion
    }
}
