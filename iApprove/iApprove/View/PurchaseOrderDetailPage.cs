using ProjectTemplate.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ProjectTemplate.CustomControl;
using ProjectTemplate.Model;

namespace ProjectTemplate.View
{
	public partial class PurchaseOrderDetailPage : BaseContentPage
	{
        #region "Properties"
        private PODetailViewModel vm;
		public string PoNumber;
		private object navParam;
		#endregion

		#region "Methods"

		public PurchaseOrderDetailPage()
        {
			InitPage();
        }
		public PurchaseOrderDetailPage(object args)
		{
			this.navParam = args;
			InitPage();
		}
		private void InitPage()
		{
			vm = new PODetailViewModel(App.NavigationServiceInstance);
			this.BindingContext = vm;

			InitializeComponent();
			this.poItemList.ItemSelected += poItem_Selected;

			vm.LoadData(PoNumber);
		}


        private void poItem_Selected(object sender, SelectedItemChangedEventArgs e)
        {
			if (poItemList.SelectedItem == null) return;
			poItemList.SelectedItem = null;
			ItemDetailModel itemDetailModel = (ItemDetailModel)e.SelectedItem;
			vm.ItemSelectedCommand.Execute(itemDetailModel);
        }

        /// <summary>
        /// On selecting an list item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnSelection(object sender, SelectedItemChangedEventArgs e)
        {
        }

        /// <summary>
        /// On tapping the list view item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnTap(object sender, ItemTappedEventArgs e)
        {
			//vm.ItemTapped.Execute(null);
        }

        /// <summary>
        /// On pull down refreshing the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnRefresh(object sender, EventArgs e)
        {
        }
        #endregion

		#region "Events"

		#endregion
    }
}
