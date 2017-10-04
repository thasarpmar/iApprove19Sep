using Android.Content;
using Android.Views;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using iApprove.CustomControl;
using iApprove.Droid.Renderer;
using Android.Graphics.Drawables;

[assembly: ExportRenderer(typeof(CustomListViewItem), typeof(CustomListViewItemRenderer))]
namespace iApprove.Droid.Renderer
{
    class CustomListViewItemRenderer : ViewCellRenderer
    {
        private Android.Views.View cellCore;
        protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, ViewGroup parent, Context context)
        {
            var cell = base.GetCellCore(item, convertView, parent, context);
            /*((Android.Widget.ListView)parent).Divider = new ColorDrawable(Android.Graphics.Color.ParseColor("#c51162"));
            ((Android.Widget.ListView)parent).DividerHeight = 2;
            ((Android.Widget.ListView)parent).SetPaddingRelative(0, 0, 0, 0);
            ((Android.Widget.ListView)parent).SetFooterDividersEnabled(true);*/

            cell.SetBackgroundResource(Resource.Drawable.listitemstyle);
            return cell;
        }
    }
}