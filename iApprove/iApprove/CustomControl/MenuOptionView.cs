using iApprove.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using iApprove.View;

namespace iApprove.CustomControl
{
    public class MenuOptionView : Grid
    {

        public MenuOptionView()
        {

            this.BackgroundColor = new Color(0, 0, 0, 0.5);
            List<MenuEachItem> list = new List<MenuEachItem>();

            list.Add(new MenuEachItem { MenuText = "Option1", ImageName = "MenuArrowDefault.png" });
            list.Add(new MenuEachItem { MenuText = "Option2", ImageName = "MenuArrowDefault.png" });
            list.Add(new MenuEachItem { MenuText = "Option3", ImageName = "MenuArrowDefault.png" });
            list.Add(new MenuEachItem { MenuText = "Option4", ImageName = "MenuArrowDefault.png" });
            list.Add(new MenuEachItem { MenuText = "Option5", ImageName = "SignOutIcon.png" });

            CustomMenuListView listView = new CustomMenuListView() { HeightRequest = 222, WidthRequest = 220 };
            listView.ItemsSource = list;
            listView.HorizontalOptions = LayoutOptions.EndAndExpand;
            listView.VerticalOptions = LayoutOptions.Start;
            listView.BackgroundColor = Color.Black;
            listView.SeparatorVisibility = SeparatorVisibility.Default;
            listView.SeparatorColor = Color.White;
            var grid = new Grid();
            var firstRow = new RowDefinition();
            var secondRow = new RowDefinition();
            grid.RowSpacing = 0;
            DataTemplate cell = new DataTemplate(typeof(CustomMenuViewCell));
            listView.ItemTemplate = cell;
            listView.ItemTapped += NavigateToPage;
            var len = new GridLength(10, GridUnitType.Star);
            firstRow.Height = GridLength.Auto;
            grid.RowDefinitions.Add(firstRow);
            secondRow.Height = len;
            grid.RowDefinitions.Add(secondRow);
            Button button1 = new Button() { BackgroundColor = Color.Transparent };
            Button button2 = new Button() { BackgroundColor = Color.Transparent };
            Grid.SetRow(button1, 1);
            Grid.SetRow(button2, 0);
            grid.Children.Add(button1);
            grid.Children.Add(button2);
            grid.Children.Add(listView);
            Grid.SetRow(listView, 0);
            this.Children.Add(grid);
            button1.Clicked += MenuHideClicked;
            button2.Clicked += MenuHideClicked;
			this.IsVisible = false;
        }

		public void MenuToggle()
        {
            try
            {
				if(this!=null)
                this.IsVisible = !this.IsVisible;
            }
            catch(Exception ex)
            {

            }
        }


        private void MenuHideClicked(object sender, EventArgs e)
        {
            this.IsVisible = false;
        }

        private void NavigateToPage(object sender, ItemTappedEventArgs e)
        {
			this.IsVisible = false;
			//App.NavigationServiceInstance.NavigateTo(Enum.PageName.LOGIN,null);
			//Navigation.PushAsync(new LoginPage());
        }
    }
}
