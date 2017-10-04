using iApprove.CustomControl;
using iApprove.iOS.Renderer;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomMenuViewCell), typeof(MenuViewCellRenderer))]

namespace iApprove.iOS.Renderer
{
    class MenuViewCellRenderer : ViewCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);
            cell.SelectedBackgroundView = new UIView
            {
                BackgroundColor = UIColor.DarkGray,
            };

            return cell;
        }
    }
}
