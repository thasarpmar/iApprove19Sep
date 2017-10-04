using System;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using iApprove.CustomControl;
using iApprove.iOS.Renderer;

[assembly: ExportRenderer(typeof(CustomListViewItem), typeof(CustomListViewItemRenderer))]
namespace iApprove.iOS.Renderer
{
    /// <summary>
    /// </summary>
    class CustomListViewItemRenderer : ViewCellRenderer
    {

        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);
            cell.SelectedBackgroundView = new UIView
            {
                //BackgroundColor = UIColor.FromRGB(79, 112, 145) //Old code used to change background if item is selected
                BackgroundColor = UIColor.FromRGBA(0, 0, 0, 0)
            };
            ViewCell viewCell2 = (ViewCell)item;
            //Remove GestureRecognizers if the iOS target
            Grid grid1 = (Grid)viewCell2.View;
            if (grid1.GestureRecognizers != null)
            {
                int count = grid1.GestureRecognizers.Count;
                for (int i = 0; i < count; i++)
                {
                    grid1.GestureRecognizers.RemoveAt(i);
                }
            }
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            cell.SeparatorInset = UIEdgeInsets.Zero; //This statement will remove the left side space in Lsitview
            item.Tapped += Item_Tapped;
            return cell;           

        }
        
        private void Item_Tapped(object sender, EventArgs e)
        {
            ViewCell cell = (ViewCell)sender;
        }
    }
}
