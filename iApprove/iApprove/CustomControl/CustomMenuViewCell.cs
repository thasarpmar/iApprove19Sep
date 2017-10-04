using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace iApprove.CustomControl
{
    public class CustomMenuViewCell : ViewCell
    {
        public CustomMenuViewCell()
        {
            var viewCellGrid = new Grid();
            var viewCellRow = new RowDefinition();
            viewCellGrid.RowDefinitions.Add(viewCellRow);
            Label label = new Label();
            label.SetBinding(Label.TextProperty, "MenuText");

            label.TextColor = Color.White;
            Grid.SetRow(label, 0);
            label.HorizontalOptions = LayoutOptions.Start;
            label.VerticalOptions = LayoutOptions.Center;
            viewCellGrid.Padding = new Thickness(8, 0, 8, 0);
            Image image = new Image();

            image.HorizontalOptions = LayoutOptions.End;
            image.SetBinding(Image.SourceProperty, "ImageName");
            viewCellGrid.Children.Add(label);
            viewCellGrid.Children.Add(image);
            this.View = viewCellGrid;

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}
