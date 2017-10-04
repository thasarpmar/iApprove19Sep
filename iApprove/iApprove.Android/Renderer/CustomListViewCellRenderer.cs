using Android.Content;
using Android.Views;
using Xamarin.Forms;
using Android.Graphics.Drawables;
using iApprove.CustomControl;
using iApprove.Droid.Renderer;

[assembly: ExportRenderer(typeof(CustomListViewCell), typeof(CustomListViewCellRenderer))]
namespace iApprove.Droid.Renderer
{
    class CustomListViewCellRenderer : Xamarin.Forms.Platform.Android.ViewCellRenderer
    {
        protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, ViewGroup parent, Context context)
        {
            var cell = base.GetCellCore(item, convertView, parent, context);
            //cell.SetPadding(10, 0, 0, 0);
            // cell.SetBackgroundColor(Android.Graphics.Color.Rgb(79, 112, 145));
            ((Android.Widget.ListView)parent).Divider = new ColorDrawable(Android.Graphics.Color.ParseColor("#515151"));
            ((Android.Widget.ListView)parent).DividerHeight = 2;
            ((Android.Widget.ListView)parent).SetPaddingRelative(0, 0, 0, 0);
            ((Android.Widget.ListView)parent).SetFooterDividersEnabled(true);

            cell.SetBackgroundResource(Resource.Drawable.listitemstyle);
            return cell;
        }
    }
}