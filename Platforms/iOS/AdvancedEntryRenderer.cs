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


[assembly: ExportRenderer(typeof(AdvancedEntry), typeof(AdvancedEntryRenderer))]
namespace Plugin.SomeCustomViews.Platforms.iOS
{
    public class AdvancedEntryRenderer : EntryRenderer, IUITextFieldDelegate
    {
        public AdvancedEntry ElementV2 => Element as AdvancedEntry;

        protected override UITextField CreateNativeControl()
        {
            var control = new CustomTextField2(RectangleF.Empty)
            {
                Padding = ElementV2.Padding,
                BorderStyle = UITextBorderStyle.RoundedRect,
                ClipsToBounds = true
            };

            UpdateBackground(control);

            return control;
        }

        protected void UpdateBackground(UITextField control)
        {
            if (control == null) return;
            control.Layer.CornerRadius = ElementV2.CornerRadius;
            control.Layer.BorderWidth = ElementV2.BorderThickness;
            control.Layer.BorderColor = ElementV2.BorderColor.ToCGColor();
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Entry> e)
        {
            if (Element == null)
            {
                return;
            }




            var entry = (AdvancedEntry)Element;
            var textField = new CustomTextField2();

            textField.EditingChanged += OnEditingChanged;
            textField.OnDeleteBackwardKey += (sender, a) =>
            {
                entry.OnBackspacePressed();
            };

            textField.Layer.CornerRadius = ElementV2.CornerRadius;
            textField.Layer.BorderWidth = ElementV2.BorderThickness;
            textField.Layer.BorderColor = ElementV2.BorderColor.ToCGColor();

            SetNativeControl(textField);

            base.OnElementChanged(e);
        }

        IElementController ElementController => Element as IElementController;

        private void OnEditingChanged(object sender, EventArgs eventArgs)
        {
            ElementController.SetValueFromRenderer(Xamarin.Forms.Entry.TextProperty, Control.Text);
        }

    }


    public class CustomTextField2 : UITextField
    {
        #region View

        private Thickness _padding = new Thickness(5);

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
        public CustomTextField2()
        {
        }
        public CustomTextField2(NSCoder coder) : base(coder)
        {
        }

        public CustomTextField2(CGRect rect) : base(rect)
        {
        }

        public override CGRect TextRect(CGRect forBounds)
        {
            var insets = new UIEdgeInsets((float)Padding.Top, (float)Padding.Left, (float)Padding.Bottom, (float)Padding.Right);
            return insets.InsetRect(forBounds);
        }

        public override CGRect PlaceholderRect(CGRect forBounds)
        {
            var insets = new UIEdgeInsets((float)Padding.Top, (float)Padding.Left, (float)Padding.Bottom, (float)Padding.Right);
            return insets.InsetRect(forBounds);
        }

        public override CGRect EditingRect(CGRect forBounds)
        {
            var insets = new UIEdgeInsets((float)Padding.Top, (float)Padding.Left, (float)Padding.Bottom, (float)Padding.Right);
            return insets.InsetRect(forBounds);
        }

        #endregion

        // A delegate type for hooking up change notifications.
        public delegate void DeleteBackwardKeyEventHandler(object sender, EventArgs e);

        // An event that clients can use to be notified whenever the
        // elements of the list change.
        public event DeleteBackwardKeyEventHandler OnDeleteBackwardKey;


        public void OnDeleteBackwardKeyPressed()
        {
            if (OnDeleteBackwardKey != null)
            {
                OnDeleteBackwardKey(null, null);
            }
        }

        public override void DeleteBackward()
        {
            base.DeleteBackward();
            OnDeleteBackwardKeyPressed();
        }
    }
}