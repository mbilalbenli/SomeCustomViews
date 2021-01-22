using CoreGraphics;
using Foundation;
using Plugin.SomeCustomViews.Platforms.iOS;
using Plugin.SomeCustomViews.Shared.Controls;
using System;
using System.ComponentModel;
using System.Drawing;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(AdvancedEntry), typeof(AdvancedEntryRenderers))]
namespace Plugin.SomeCustomViews.Platforms.iOS
{
    public class AdvancedEntryRenderers : EntryRenderer
    {
        public AdvancedEntry ElementV2 => Element as AdvancedEntry;
        public UITextFieldCustomize ControlV2 => Control as UITextFieldCustomize;
        public IElementController ElementController => Element as IElementController;

        protected override UITextField CreateNativeControl()
        {
            UITextFieldCustomize control = new UITextFieldCustomize(RectangleF.Empty)
            {
                Padding = ElementV2.Padding,
                BorderStyle = UITextBorderStyle.RoundedRect,
                ClipsToBounds = true
            };

            UpdateView(control);

            return control;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            if (Element == null) return;

            BackspaceDetectHandler((AdvancedEntry)Element);

            base.OnElementChanged(e);
        }

        protected void UpdateView(UITextFieldCustomize control)
        {
            if (control == null) return;

            control.Layer.CornerRadius = ElementV2.CornerRadius;
            control.Layer.BorderWidth = ElementV2.BorderThickness;
            control.Layer.BorderColor = ElementV2.BorderColor.ToCGColor();

        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == AdvancedEntry.PaddingProperty.PropertyName)
            {
                UpdatePadding();
            }

            base.OnElementPropertyChanged(sender, e);
        }

        protected void UpdatePadding()
        {
            if (Control == null) return;

            ControlV2.Padding = ElementV2.Padding;
        }

        private void BackspaceDetectHandler(AdvancedEntry entry)
        {
            var textField = new UITextFieldCustomize();

            textField.EditingChanged += OnEditingChanged;
            textField.OnDeleteBackward += (sender, i) =>
            {
                entry.OnBackspacePressed();
            };

            SetNativeControl(textField);
        }

        private void OnEditingChanged(object sender, EventArgs e)
        {
            ElementController.SetValueFromRenderer(Entry.TextProperty, Control.Text);
        }
    }

    public class UITextFieldCustomize : UITextField
    {

        public delegate void DeleteBackwardEventHandler(object sender, EventArgs e);

        public event DeleteBackwardEventHandler OnDeleteBackward;


        private Thickness _padding = new Thickness(3);
        public Thickness Padding
        {
            get => _padding;
            set
            {
                if (_padding != value)
                {
                    _padding = value;
                    //InvalidateIntrinsicContentSize();
                }
            }
        }

        public UITextFieldCustomize()
        {

        }
        public UITextFieldCustomize(NSCoder coder) : base(coder)
        {

        }

        public UITextFieldCustomize(CGRect rect) : base(rect)
        {

        }

        public void OnDeleteBackwardPressed()
        {
            if (OnDeleteBackward != null)
            {
                OnDeleteBackward(null, null);
            }
        }

        public override void DeleteBackward()
        {
            base.DeleteBackward();
            OnDeleteBackwardPressed();
        }

        public override CGRect TextRect(CGRect forBounds)
        {
            var insets = new UIEdgeInsets(
                (float)Padding.Top,
                (float)Padding.Left,
                (float)Padding.Bottom,
                (float)Padding.Right);

            return insets.InsetRect(forBounds);
        }

        public override CGRect PlaceholderRect(CGRect forBounds)
        {
            var insets = new UIEdgeInsets(
                (float)Padding.Top,
                (float)Padding.Left,
                (float)Padding.Bottom,
                (float)Padding.Right);

            return insets.InsetRect(forBounds);
        }

        public override CGRect EditingRect(CGRect forBounds)
        {
            var insets = new UIEdgeInsets
                ((float)Padding.Top,
                (float)Padding.Left,
                (float)Padding.Bottom,
                (float)Padding.Right);

            return insets.InsetRect(forBounds);
        }

    }
}
