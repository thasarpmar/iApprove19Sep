using System;
using Xamarin.Forms;
using System.Linq;
using UIKit;
using CoreGraphics;

namespace iApprove.iOS.DependencyService
{
    public class MenuView : UIView
    {
        private UIView View;
        public static UITableView table;

        public MenuView()
        {
            Initialize();
        }

        void Initialize()
        {
            //Already this View will be Hidden and When NavigationBarButtonItem will  be clicked then it will be visible.         
            BackgroundColor = UIColor.Clear;
            Frame = new CGRect(UIScreen.MainScreen.Bounds.X, UIScreen.MainScreen.Bounds.Y + 64, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height - 65);

            var button = new UIButton(new CGRect(UIScreen.MainScreen.Bounds.X, UIScreen.MainScreen.Bounds.Y, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height));
            button.BackgroundColor = UIColor.Black;
            button.Alpha = (nfloat)0.8;
            button.TouchUpInside += ButtonClicked;
            this.Add(button);
        }

        private void ButtonClicked(object sender, EventArgs e)
        {
            var button = sender as UIButton;
            button.Superview.Hidden = true;
        }
    }
}
