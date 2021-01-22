using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using Plugin.SomeCustomViews.Platforms.Android;
using Plugin.SomeCustomViews.Shared.Controls;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AdvancedEntry), typeof(AdvancedEntryRenderer))]
namespace Plugin.SomeCustomViews.Platforms.Android
{
    public class AdvancedEntryRenderer : EntryRenderer
    {
        public AdvancedEntry ElementV2 => Element as AdvancedEntry;

        public AdvancedEntryRenderer(Context context) : base(context)
        {

        }

        public override bool DispatchKeyEvent(KeyEvent e)
        {
            if (e.Action == KeyEventActions.Down)
            {
                if (e.KeyCode == Keycode.Del)
                {
                    if (string.IsNullOrWhiteSpace(Control.Text))
                    {
                        var entry = (AdvancedEntry)Element;
                        entry.OnBackspacePressed();
                    }
                }
            }
            return base.DispatchKeyEvent(e);
        }
        private void NativeEditText_KeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keycode.Del)
            {
                var entry = (AdvancedEntry)Element;
                entry.OnBackspacePressed();
            }
        }

        protected override FormsEditText CreateNativeControl()
        {
            var control = base.CreateNativeControl();

            UpdateView(control);
            return control;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (Control == null) return;

            if (e.PropertyName == AdvancedEntry.CornerRadiusProperty.PropertyName)
            {
                UpdateBackground();
            }
            else if (e.PropertyName == AdvancedEntry.BorderThicknessProperty.PropertyName)
            {
                UpdateBackground();
            }
            else if (e.PropertyName == AdvancedEntry.BorderColorProperty.PropertyName)
            {
                UpdateBackground();
            }

            base.OnElementPropertyChanged(sender, e);
        }


        protected override void UpdateBackgroundColor()
        {
            UpdateBackground();
        }

        protected void UpdateView(FormsEditText control)
        {
            if (control == null) return;

            GradientDrawable gradientDrawable = new GradientDrawable();
            gradientDrawable.SetColor(ElementV2.BackgroundColor.ToAndroid());
            gradientDrawable.SetCornerRadius(Context.ToPixels(ElementV2.CornerRadius));
            gradientDrawable.SetStroke((int)Context.ToPixels(ElementV2.BorderThickness), ElementV2.BorderColor.ToAndroid());
            control.SetBackground(gradientDrawable);

            var padTop = (int)Context.ToPixels(ElementV2.Padding.Top);
            var padBottom = (int)Context.ToPixels(ElementV2.Padding.Bottom);
            var padLeft = (int)Context.ToPixels(ElementV2.Padding.Left);
            var padRight = (int)Context.ToPixels(ElementV2.Padding.Right);
            control.SetPadding(padLeft, padTop, padRight, padBottom);
        }

        protected void UpdateView()
        {
            UpdateView(Control);
        }

    }
}
