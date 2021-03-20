using CoreGraphics;
using Plugin.SomeCustomViews.Shared.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SimpleEntry), typeof(Plugin.SomeCustomViews.Platforms.iOS.SimpleEntryRenderer))]
namespace Plugin.SomeCustomViews.Platforms.iOS
{
    public class SimpleEntryRenderer:EntryRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            var ElementV2 = Element as SimpleEntry;
            base.OnElementChanged(e);
            if (Control != null && e.NewElement != null)
            {
                Control.LeftView = new UIView(new CGRect(0, 0, ElementV2.Padding.Left, Control.Frame.Height + ElementV2.Padding.Top));
                Control.RightView = new UIView(new CGRect(0, 0, ElementV2.Padding.Right, Control.Frame.Height + ElementV2.Padding.Top));
                Control.LeftViewMode = UITextFieldViewMode.Always;
                Control.RightViewMode = UITextFieldViewMode.Always;

                Control.BorderStyle = UITextBorderStyle.None;
                Control.TextColor = Element.TextColor.ToUIColor();
            }
        }
    }
}