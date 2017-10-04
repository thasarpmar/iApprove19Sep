using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace iApprove.CustomControl
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AccordionButton : ContentView
	{
		public AccordionButton()
		{
			InitializeComponent();
		}

		#region "Properties"
		private bool isSelected = false;
		public bool IsSelected
		{
			get { return this.isSelected; }
			set
			{
				this.isSelected = value;
				ChangeSelectionChanged();
			}
		}
		private Xamarin.Forms.View associatedView;
		public Xamarin.Forms.View AssociatedView
		{
			get { return this.associatedView; }
			set 
			{ 
				this.associatedView = value;
				this.associatedView.IsVisible = false;
			}
		}
		public string MenuText
		{
			get { return this.MenuTitle.Text; }
			set { this.MenuTitle.Text = value; }
		}
		#endregion

		#region "Methods"
		private void ChangeSelectionChanged()
		{
			if (this.AssociatedView != null)
			{
				this.AssociatedView.IsVisible = isSelected;
			}
			this.MenuExpandMarker.Text = isSelected ? "-" : "+";

			if (this.isSelected)
			{
				MenuTitle.FontAttributes = FontAttributes.Bold;
				MenuFrame.BackgroundColor = Color.FromHex("#A3DFA6");
			}
			else
			{
				MenuTitle.FontAttributes = FontAttributes.None;
				MenuFrame.BackgroundColor = Color.FromHex("#DAE0DE");
			}
		}
		#endregion

		#region "Events"
		public void OnButtonTapped(Object sender, EventArgs args)
		{
            this.IsSelected = !this.isSelected;
			if (ItemTappedCallBack != null) ItemTappedCallBack.Execute(this);
		}

		public ICommand ItemTappedCallBack { get; set; }
		#endregion
	}
}
