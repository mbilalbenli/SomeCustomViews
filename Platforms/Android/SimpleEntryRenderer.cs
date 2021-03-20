using Android.Content;
using Android.Graphics.Drawables;
using Plugin.SomeCustomViews.Shared.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SimpleEntry), typeof(Plugin.SomeCustomViews.Platforms.Android.SimpleEntryRenderer))]
namespace Plugin.SomeCustomViews.Platforms.Android
{
    public class SimpleEntryRenderer : EntryRenderer
    {
      
        public SimpleEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            var ElementV2 = Element as SimpleEntry;

            base.OnElementChanged(e);
            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(global::Android.Graphics.Color.Transparent);
                this.Control.SetBackground(gd);
                Control.SetTextColor(Element.TextColor.ToAndroid());

                var padTop = (int)Context.ToPixels(ElementV2.Padding.Top);
                var padBottom = (int)Context.ToPixels(ElementV2.Padding.Bottom);
                var padLeft = (int)Context.ToPixels(ElementV2.Padding.Left);
                var padRight = (int)Context.ToPixels(ElementV2.Padding.Right);
                Control.SetPadding(padLeft, padTop, padRight, padBottom);
            }
        }
    }
}
