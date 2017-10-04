using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using iApprove.CustomControl;
using iApprove.Droid.Renderer;

[assembly: ExportRenderer(typeof(CustomListViewControl), typeof(CustomListViewControlRenderer))]
namespace iApprove.Droid.Renderer
{
    class CustomListViewControlRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            if (e.NewElement == null)
                return;

            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Divider = new ColorDrawable(Android.Graphics.Color.White);
                Control.DividerHeight = 1;
                Control.SetFooterDividersEnabled(true);

                Android.Widget.ListView lv = (Android.Widget.ListView)Control;
                lv.Divider.SetVisible(true, true);
            }
        }
    }
}