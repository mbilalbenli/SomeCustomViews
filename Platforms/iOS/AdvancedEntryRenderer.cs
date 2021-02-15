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
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            if (Element == null)
            {
                return;
            }

            BackspaceDetectHandler(Element);


            //SetNativeControl(textField);

            base.OnElementChanged(e);
        }

        private void BackspaceDetectHandler(Entry element)
        {
            var entry = (AdvancedEntry)element;
            var textField = new CustomTextField();

            textField.EditingChanged += OnEditingChanged;
            textField.OnDeleteBackwardKey += (sender, a) =>
            {
                entry.OnBackspacePressed();
            };
        }

        IElementController ElementController => Element as IElementController;

        void OnEditingChanged(object sender, EventArgs eventArgs)
        {
            ElementController.SetValueFromRenderer(Entry.TextProperty, Control.Text);
        }

    }


    public class CustomTextField : UITextField
    {
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