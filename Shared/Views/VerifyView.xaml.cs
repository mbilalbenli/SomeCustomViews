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
    public partial class VerifyView : ContentView
    {
        public VerifyView()
        {
            InitializeComponent();
        }


        public static readonly BindableProperty IsSeparetedProperty = BindableProperty.Create(nameof(IsSepareted), typeof(bool), typeof(VerifyView), false);
        public bool IsSepareted
        {
            get => (bool)GetValue(IsSeparetedProperty);
            set
            {
                SetValue(IsSeparetedProperty, value);

                if (value)
                {
                }

            }
        }

    }
}