using iApprove.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iApprove.Enum;
using Xamarin.Forms;
using iApprove.Services;
using iApprove.View;
using iApprove.CustomControl;

[assembly: Dependency(typeof(NavigationService))]
namespace iApprove.Services
{
	/// <summary>
	/// This class is pecifically done for MAPICS POC
	/// </summary>
    public class NavigationService : INavigationService
    {
        public static Dictionary<PageName, Type> PageMapping = new Dictionary<PageName, Type>();
        private ContentPage appContext;
        public NavigationService()
        {
           
        }

        public void CreatePageMap()
        {
            #region "Page Mapping Init"
            PageMapping.Clear();
		
            //iApprove Page Mapping for Navigation
            PageMapping.Add(PageName.IA_LOGIN, typeof(LoginPage));
            PageMapping.Add(PageName.IA_HOME, typeof(HomePage));
            PageMapping.Add(PageName.IA_PENDINGREQUESTS, typeof(PendingRequestsPage));
            PageMapping.Add(PageName.IA_PENDINGREQUESTSDETAILS, typeof(PendingRequestDetailsPage));
            PageMapping.Add(PageName.IA_NOTIFICATIONS, typeof(NotificationsPage));
            PageMapping.Add(PageName.IA_SEARCH, typeof(Search));

            #endregion
        }

		public void NavigateTo(PageName page, object param, bool isStartPage = false)
        {
			BaseContentPage screenObj = null;
			if (param != null)
			{				
                try
                {
                    screenObj = (BaseContentPage)Activator.CreateInstance(PageMapping[page], param);
                }
                catch (Exception ex)
                {

                }
			}
			else
			{
				screenObj = (BaseContentPage)Activator.CreateInstance(PageMapping[page]);
			}

			if (!isStartPage)
			{
                if (screenObj != null)
                {
                    if (App.IsMasterDetailFlow)
                    {
                        //((MasterDetailPage)Application.Current.MainPage).Detail.Navigation.PushAsync(screenObj, true);

                        ((MasterDetailPage)Application.Current.MainPage).Detail = screenObj;
                    }
                    else
                    {
                        Application.Current.MainPage.Navigation.PushAsync(screenObj, true);
                    }
                }
			}
			else
			{
				SetStartPage(screenObj);
			}
        }
		/// <summary>
		/// Used to set the starting page to the application
		/// </summary>
		/// <param name="page">Page.</param>
		public void SetStartPage(Page page)
		{
			//if (page as LoginPage != null)
			//{
			//	Application.Current.MainPage = new CustomNavigationPage(page); //, false);
			//}
			//else
			//{
			//	Application.Current.MainPage = new CustomNavigationPage(page);
			//}
			Application.Current.MainPage = new CustomNavigationPage(page);
			App.CustomNavigation = Application.Current.MainPage.Navigation;
		}

        public T GetPageInstance<T>(PageName page) where T:new()
        {
            return (T)Activator.CreateInstance(PageMapping[page]);
        }

        public void SetAppContext(object context)
        {
            this.appContext = (ContentPage)context;
        }

		public void GotoHomePage()
		{
			Application.Current.MainPage.Navigation.PopToRootAsync(true);
		}

		public void GoBack()
		{
			Application.Current.MainPage.Navigation.PopAsync();
		}
	}
}
