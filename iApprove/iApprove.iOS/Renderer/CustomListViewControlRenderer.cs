using iApprove.CustomControl;
using iApprove.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomListViewControl), typeof(CustomListViewControlRenderer))]
namespace iApprove.iOS.Renderer
{
    public class CustomListViewControlRenderer : ListViewRenderer
    {
        public CustomListViewControlRenderer()
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            if (e.NewElement == null)
                return;

            base.OnElementChanged(e);
            if (Control != null)
            {
				Control.BackgroundColor = UIColor.White;
                Control.TableFooterView = new UIView();
            }
        }
    }
}

