using System;

using iApprove.CustomControl;
using iApprove.ViewModel;



using System.Collections.ObjectModel;

using iApprove.Model;
using Xamarin.Forms;

namespace iApprove.View
{
    public partial class Search : BaseContentPage
    {
        private SearchViewModel vm;
        private object navParam;

        public Search()
        {
            InitPage();
        }

        public Search(object args)
        {
            navParam = args;
            InitPage();
        }


        void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)

        {
            SearchText.IsVisible = false;
            SearchList.IsVisible = true;
            vm.PendingRequestList = new ObservableCollection<PendingRequests>(Helper.Instance.SearchByRequests(e.NewTextValue));
        }

        private void InitPage()
        {
            iApproveNavBar.MyNavigationBar.InitHeader("Search");
            InitializeComponent();
            vm = new SearchViewModel(App.NavigationServiceInstance);
            this.BindingContext = vm;
        }
    }
}




