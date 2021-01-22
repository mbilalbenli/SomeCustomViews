using Plugin.SomeCustomViews.Shared.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Plugin.SomeCustomViews.Shared.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerifView : Grid
    {
        /// <summary>
        /// Defines max value of the frames entries
        /// </summary>
        private double FrameSizeMax;
        /// <summary>
        /// Defines max value of the entries font size
        /// </summary>
        private double FontSizeMax;
        /// <summary>
        /// Keeps the entered code
        /// </summary>
        private string VerifCode;

        /// <summary>
        /// for AdvancedEntry's default values 
        /// </summary>
        private static AdvancedEntry defaultEntry = new AdvancedEntry();

        public VerifView()
        {
            InitializeComponent();

            var x = new AdvancedEntry();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width != -1)
            {
                var edge = width / 62 * 10;
                this.HeightRequest = edge;
                FrameSizeMax = edge - 18;
                this.UpdateChildrenLayout();
            }
        }

        /// <summary>
        /// Some Bindable Properties for VerifView
        /// </summary>
        #region Bindable Properties


        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(VerifView), Color.Pink);
        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(VerifView), Color.White);
        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public static readonly BindableProperty FrameSizeProperty = BindableProperty.Create(nameof(FrameSize), typeof(double), typeof(VerifView), 200.00);
        public double FrameSize
        {
            get => (double)GetValue(FrameSizeProperty);
            set
            {
                if (value >= FrameSizeMax)
                {
                    SetValue(FrameSizeProperty, FrameSizeMax);
                }
                else
                {
                    SetValue(FrameSizeProperty, value);
                }
            }
        }

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(double), typeof(VerifView), 0.00);
        public double CornerRadius
        {
            get => (double)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static readonly BindableProperty ThicknessProperty = BindableProperty.Create(nameof(Thickness), typeof(double), typeof(VerifView), 1.00);
        public double Thickness
        {
            get => (double)GetValue(ThicknessProperty);
            set => SetValue(ThicknessProperty, value);
        }

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(VerifView), defaultEntry.FontSize);
        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set
            {
                if (value > FontSizeMax)
                {
                    SetValue(FontSizeProperty, FontSizeMax);
                }
                else
                {
                    SetValue(FontSizeProperty, value);
                }
            }
        }

        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(VerifView), defaultEntry.FontFamily);
        public string FontFamily
        {
            get => (string)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(VerifView));
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        #endregion

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            AdvancedEntry newEntry = new AdvancedEntry();
            var entry = sender as AdvancedEntry;
            var oldID = Convert.ToInt32(entry.AutomationId);
            MakeVerifCode(oldID, e.NewTextValue.ToCharArray().FirstOrDefault());

            if (oldID == 6) return; //max 

            if (e.OldTextValue == null || e.OldTextValue == string.Empty)
            {
                (this.FindByName("e" + (oldID + 1)) as Entry).Focus();
            }
            else if (e.NewTextValue == string.Empty)
            {
                MakeVerifCode(oldID, ' ');
            }
        }

        /// <summary>
        /// Function running when it is detected that the back key has been pressed 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Entry_OnBackSpace(object sender, EventArgs e)
        {
            var nowEntry = sender as AdvancedEntry;
            var oldID = Convert.ToInt32(nowEntry.AutomationId);
            MakeVerifCode(oldID, ' ');

            if (oldID == 1) return; //m'n 
            if (nowEntry.Text == string.Empty || nowEntry.Text == null)
            {
                var beforeEntry = this.FindByName("e" + (oldID - 1)) as AdvancedEntry;
                beforeEntry.Focus();
                beforeEntry.Text = "";
            }

        }

        /// <summary>
        /// Edits the entered values
        /// </summary>
        /// <param name="entryID"> Changed text entry's ıd </param>
        /// <param name="code"> Changed text entry's value </param>
        private void MakeVerifCode(int entryID, char code)
        {
            StringBuilder sb = new StringBuilder(VerifCode);
            if (sb.Length >= entryID)
            {
                sb[entryID - 1] = code;
                VerifCode = sb.ToString();
                Text = VerifCode;
            }
            else
            {
                for (int i = 0; i < entryID - sb.Length; i++)
                {
                    VerifCode = VerifCode + " ";
                }
                sb = new StringBuilder(VerifCode);
                sb[entryID - 1] = code;
                VerifCode = sb.ToString();
                Text = VerifCode;
            }
#if DEBUG

            System.Diagnostics.Debug.WriteLine($"Yazılan kod: {VerifCode}");

#endif
        }
    }
}