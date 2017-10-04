using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace iApprove.CustomControl
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TopMenuView : ContentView
	{
		public TopMenuView()
		{
			InitializeComponent();
		}
		private string seletedMenu;
		public string SelectMenu
		{
			get { return this.seletedMenu; }
			set
			{
				this.seletedMenu = value;
				HighlightMenu(value);
			}
		}

		private void HighlightMenu(string selIndex)
		{
			switch (selIndex)
			{
				case "1":
                    ApplyMenuBack(Color.White, Color.Transparent, Color.Transparent, Color.Transparent);
                 	ApplyMenuIcon("ic_room_white_selected", "ic_assignment_white", 
					                      "ic_camera_alt_white", "ic_developer_board_white");
					ApplyMenuTextBold(FontAttributes.Bold, FontAttributes.None, FontAttributes.None, FontAttributes.None);
					ApplyMenuTextColor(Color.FromHex("#3D846A"), Color.White, Color.White, Color.White);
					break;

				case "2":
					ApplyMenuBack(Color.Transparent, Color.White, Color.Transparent, Color.Transparent);
                    ApplyMenuIcon("ic_room_white", "ic_assignment_selected",
					                      "ic_camera_alt_white", "ic_developer_board_white");
					ApplyMenuTextBold(FontAttributes.None, FontAttributes.Bold, FontAttributes.None, FontAttributes.None);
					ApplyMenuTextColor(Color.White, Color.FromHex("#3D846A"), Color.White, Color.White);
					break;

				case "3":
					ApplyMenuBack(Color.Transparent, Color.Transparent, Color.White, Color.Transparent);
                    ApplyMenuIcon("ic_room_white", "ic_assignment_white",
					                      "ic_camera_alt_selected", "ic_developer_board_white");
					ApplyMenuTextBold(FontAttributes.None, FontAttributes.None, FontAttributes.Bold, FontAttributes.None);
                    ApplyMenuTextColor(Color.White, Color.White, Color.FromHex("#3D846A"), Color.White);
					break;

				case "4":
					ApplyMenuBack(Color.Transparent, Color.Transparent, Color.Transparent, Color.White);
                    ApplyMenuIcon("ic_room_white", "ic_assignment_white",
					                      "ic_camera_alt_white", "ic_developer_board_selected");
					ApplyMenuTextBold(FontAttributes.None, FontAttributes.None, FontAttributes.None, FontAttributes.Bold);
                    ApplyMenuTextColor(Color.White, Color.White, Color.White, Color.FromHex("#3D846A"));
					break;
			}
		}
		private void ApplyMenuBack(Color menu1, Color menu2, Color menu3, Color menu4)
		{
			Menu1.BackgroundColor = menu1;
			Menu2.BackgroundColor = menu2;
			Menu3.BackgroundColor = menu3;
			Menu4.BackgroundColor = menu4;
		}
		private void ApplyMenuIcon(string img1, string img2, string img3, string img4)
		{
			Menu1Icon.Source = img1;
			Menu2Icon.Source = img2;
			Menu3Icon.Source = img3;
			Menu4Icon.Source = img4;
		}
		private void ApplyMenuTextBold(FontAttributes attr1, FontAttributes attr2, FontAttributes attr3, FontAttributes attr4)
		{
			Menu1Border.FontAttributes = attr1;
			Menu2Border.FontAttributes = attr2;
			Menu3Border.FontAttributes = attr3;
			Menu4Border.FontAttributes = attr4;
		}
		private void ApplyMenuTextColor(Color color1, Color color2, Color color3, Color color4)
		{
			Menu1Border.TextColor = color1;
			Menu2Border.TextColor = color2;
			Menu3Border.TextColor = color3;
			Menu4Border.TextColor = color4;
		}
	}
}
