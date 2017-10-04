using Android.App;
using Android.Graphics.Drawables;
using Android.Widget;
using iApprove.CustomControl;
using iApprove.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomNavigationPage), typeof(CustomNavigationPageRenderer))]
namespace iApprove.Droid
{

    public class CustomNavigationPageRenderer : PageRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            var actionBar = ((Activity)Context).ActionBar;

            actionBar.DisplayOptions = actionBar.DisplayOptions | ActionBarDisplayOptions.ShowCustom;
            ImageView imageView = new ImageView(actionBar.ThemedContext);
            imageView.SetScaleType(ImageView.ScaleType.Center);
            imageView.SetImageResource(Resource.Drawable.icon);
            imageView.SetPadding(10, 30, 40, 30);
            ActionBar.LayoutParams layoutParams = new ActionBar.LayoutParams(ActionBar.LayoutParams.WrapContent, ActionBar.LayoutParams.WrapContent,
                Android.Views.GravityFlags.Right | Android.Views.GravityFlags.CenterVertical);
            layoutParams.RightMargin = 5;
            layoutParams.LeftMargin = 0;

            imageView.LayoutParameters = layoutParams;

            ImageView line = new ImageView(actionBar.ThemedContext);
            line.SetBackgroundColor(Android.Graphics.Color.White);
			ActionBar.LayoutParams lineLayoutParams = new ActionBar.LayoutParams(ActionBar.LayoutParams.MatchParent, ActionBar.LayoutParams.MatchParent,
			                                                                     Android.Views.GravityFlags.Right | Android.Views.GravityFlags.FillVertical);
            lineLayoutParams.RightMargin = 0;
            lineLayoutParams.LeftMargin = 0;
            //line.SetPadding(50, 2, 10, 2);
            line.SetMinimumWidth(5);
            line.SetMaxWidth(5);
            line.LayoutParameters = lineLayoutParams;

            ImageView imageView2 = new ImageView(actionBar.ThemedContext);
            imageView2.SetScaleType(ImageView.ScaleType.Center);
            imageView2.SetImageResource(Resource.Drawable.icon);
            ActionBar.LayoutParams layoutParams2 = new ActionBar.LayoutParams(ActionBar.LayoutParams.WrapContent, ActionBar.LayoutParams.WrapContent,
                Android.Views.GravityFlags.Right | Android.Views.GravityFlags.CenterVertical);
            layoutParams2.RightMargin = 5;
            layoutParams2.LeftMargin = 50;
            imageView2.LayoutParameters = layoutParams2;
   
            LinearLayout linearLayout = new LinearLayout(actionBar.ThemedContext);
            linearLayout.Orientation = Orientation.Horizontal;

            //linearLayout.AddView(imageView);
            linearLayout.AddView(line);

            actionBar.CustomView = linearLayout;
            actionBar.Title = "";
            actionBar.SetIcon(Android.Resource.Color.Transparent);
            actionBar.SetCustomView(linearLayout, layoutParams);
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            var actionBar = ((Activity)Context).ActionBar;
            actionBar.SetBackgroundDrawable(new ColorDrawable(Xamarin.Forms.Color.FromHex("#3D846A").ToAndroid()));
            actionBar.SetIcon(Android.Resource.Color.Transparent);
        }
        }
}
