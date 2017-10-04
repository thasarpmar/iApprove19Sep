using Xamarin.Forms;
using iApprove.CustomControl;
using iApprove.Droid.Renderer;
using Xamarin.Forms.Platform.Android;
using System;
using System.Collections.Generic;

[assembly: ExportRenderer(typeof(CustomButton), typeof(CustomButtonRenderer))]
namespace iApprove.Droid.Renderer
{
    public class CustomButtonRenderer : ButtonRenderer
    {
        public static Action<int> OnClickPositive = null;
        public static Action<int> OnClickNegative = null;
        public static Dictionary<ButtonTypeName, Color> ButtonTypeMapping = new Dictionary<ButtonTypeName, Color>();

        public CustomButtonRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var btn = (Android.Widget.Button)Control;
                CustomButton btnCust = (CustomButton)e.NewElement;
                UpdateButtonBehaviour(btnCust.ButtonType, btn);                
            }
        }

        private void UpdateButtonBehaviour(ButtonTypeName type, Android.Widget.Button btn)
        {
            switch(type)
            {
                case ButtonTypeName.POSITIVE:
                    //btn.SetBackgroundColor(Color.FromHex("#B71C1C").ToAndroid()); // Color.Green.ToAndroid());
					btn.SetBackgroundColor(Color.FromHex("#2E2E2E").ToAndroid());
                    break;
                case ButtonTypeName.NEGATIVE:
                    btn.SetBackgroundColor(Color.Gray.ToAndroid());
                    break;
                case ButtonTypeName.EMAIL:
                    btn.SetBackgroundColor(Color.Lime.ToAndroid());
                    break;
                case ButtonTypeName.DIAL:
                    btn.SetBackgroundColor(Color.Orange.ToAndroid());
                    break;
                case ButtonTypeName.CONFIRM:
                    btn.SetBackgroundColor(Color.Blue.ToAndroid());
                    break;
            }
        }
    }
}