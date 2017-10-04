using ProjectTemplate.Interface;
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
    public partial class ShipmentConfirmation : BaseContentPage
    {
		private ItemDetailModel navParam = null;
		private ShipmentConfirmViewModel vm;

		public ShipmentConfirmation()
		{
			vm = new ShipmentConfirmViewModel(App.NavigationServiceInstance);
			this.BindingContext = vm;
			InitializeComponent();
		}
		public ShipmentConfirmation(object args)
		{
			vm = new ShipmentConfirmViewModel(App.NavigationServiceInstance);
			this.BindingContext = vm;
			InitializeComponent();
			this.navParam = (ItemDetailModel)args;
			vm.LoadData(this.navParam); //this.navParam.ItemNo, this.navParam.ItemNo);
		}

    }
}
