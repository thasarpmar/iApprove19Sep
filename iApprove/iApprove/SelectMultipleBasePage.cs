using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using iApprove.CustomControl;
using iApprove.Model;
using iApprove.ViewModel;
using Xamarin.Forms;

namespace iApprove
{
	public class SelectMultipleBasePage<T> : BaseContentPage
	{
        List<object> tempList = new List<object>();

        public Button ApplyFilterBtn
        {
            get;
            set;
        }
		public class ImageSourceConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                bool flag = (bool)value;if(flag)
                {
                    return "CheckboxSelected.png";
                }
                else
                {

                    return "Checkbox.png";
                }                
            }

			public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
			{
                
                return null;
			}
		}
		public class WrappedSelection<T> : INotifyPropertyChanged
		{
			public T Item { get; set; }
            public string temp { get; set; }
			bool isSelected = false;
			public bool IsSelected
			{
				get
				{
					return isSelected;
				}
				set
				{
					if (isSelected != value)
					{
						isSelected = value;
						PropertyChanged(this, new PropertyChangedEventArgs("IsSelected"));
						//                      PropertyChanged (this, new PropertyChangedEventArgs (nameof (IsSelected))); // C# 6
					}
				}
			}
			public event PropertyChangedEventHandler PropertyChanged = delegate { };
		}
		public class WrappedItemSelectionTemplate : ViewCell
		{
			public WrappedItemSelectionTemplate() : base()
			{
				Label name = new Label();

				var navStack = App.CustomNavigation.NavigationStack;
			    var bindingContext = navStack[navStack.Count - 1].BindingContext;
                if(bindingContext is HomeViewModel)
                {
                  name.SetBinding(Label.TextProperty, new Binding("Item.DepartmentModel.DeptName"));  
                }
                else if(bindingContext is PendingRequestsViewModel)
                {
                    name.SetBinding(Label.TextProperty, new Binding("Item.RequestType"));
				}	

				name.FontSize = 12;
				name.TextColor = (Color)Application.Current.Resources["SubTextForeColor"];
                Image checkedStatusImage = new Image();
                checkedStatusImage.WidthRequest = 10;
                checkedStatusImage.HeightRequest = 10;
				checkedStatusImage.Margin = new Thickness(66, 0, 10, 0);
                ImageSourceConverter imageConverter = new ImageSourceConverter();
                Binding binding = new Binding("IsSelected");
                binding.Converter = imageConverter;
                binding.ConverterParameter = this;
                checkedStatusImage.SetBinding(Image.SourceProperty, binding);
				



				Grid grid = new Grid
				{
					Margin = new Thickness(0, 0, 0, 10),
                    VerticalOptions = LayoutOptions.Start,
					RowDefinitions =
				{
					   new RowDefinition { Height = GridLength.Auto }		
				},
					ColumnDefinitions =
				{
					new ColumnDefinition { Width = GridLength.Auto },
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }					
				}
				};

                grid.Children.Add(name);
                grid.Children.Add(checkedStatusImage);
                Grid.SetColumn(name,1);
                Grid.SetRow(name, 0);
				Grid.SetRow(checkedStatusImage, 0);
                Grid.SetColumn(checkedStatusImage, 0);



				// Accomodate iPhone status bar.
				//this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

				// Build the page.
				View = grid;

				//View = layout;
			}
		}




		//this part is added by pradipta
		public class WrappedItemSelectionDateListTemplate : ViewCell
		{
			public WrappedItemSelectionDateListTemplate() : base()
			{
				Label name = new Label();
				name.SetBinding(Label.TextProperty, new Binding("temp"));
                name.FontSize = 12;
                name.TextColor = (Color)Application.Current.Resources["SubTextForeColor"];
				Image checkedStatusImage = new Image();
				checkedStatusImage.WidthRequest = 10;
				//checkedStatusImage.Source = new Uri("unchecked.png")
				checkedStatusImage.HeightRequest = 10;
                checkedStatusImage.Margin = new Thickness(66, 0, 10, 0);
				ImageSourceConverter imageConverter = new ImageSourceConverter();
				Binding binding = new Binding("IsSelected");
				//binding.Mode = BindingMode.TwoWay;
				binding.Converter = imageConverter;
				binding.ConverterParameter = this;
				checkedStatusImage.SetBinding(Image.SourceProperty, binding);
               
                Grid grid = new Grid
                {
                    Margin = new Thickness(0,0,0,10),
                    VerticalOptions = LayoutOptions.Start,
					RowDefinitions =
				{
                        new RowDefinition { Height = GridLength.Auto }
				},
					ColumnDefinitions =
				{
					new ColumnDefinition { Width = GridLength.Auto },
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
				}
				};

				grid.Children.Add(name);
				grid.Children.Add(checkedStatusImage);

				Grid.SetColumn(name, 1);
				Grid.SetRow(name, 0);
				Grid.SetRow(checkedStatusImage, 0);
				Grid.SetColumn(checkedStatusImage, 0);

				// Accomodate iPhone status bar.
				//this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

				// Build the page.
				View = grid;

			}
		}
        //upto this
		public List<WrappedSelection<T>> WrappedItems = new List<WrappedSelection<T>>();

        public List<WrappedSelection<T>> WrappedDateItems = new List<WrappedSelection<T>>();      

		public SelectMultipleBasePage(List<T> items)
		{

            iApproveNavBar.MyNavigationBar.InitHeader("Filter", false);

            List<DateItem> dateItems = new List<DateItem> { new DateItem { DateSpanName = "This Week" }, new DateItem { DateSpanName = "Last Week" }, new DateItem { DateSpanName = "Last 14 days" } };
           
            WrappedItems = items.Select(item => new WrappedSelection<T>() { Item = item, IsSelected = false }).ToList();
            WrappedDateItems = dateItems.Select(item => new WrappedSelection<T>() { temp = item.DateSpanName, IsSelected = false }).ToList();

			ListView mainList = new ListView()
            {
                ItemsSource = WrappedItems,
                SeparatorVisibility = SeparatorVisibility.None,
                HasUnevenRows=true,
				ItemTemplate = new DataTemplate(typeof(WrappedItemSelectionTemplate)),
			};

			mainList.ItemSelected += (sender, e) => {
				if (e.SelectedItem == null) return;
				var o = (WrappedSelection<T>)e.SelectedItem;               
				o.IsSelected = !o.IsSelected;
                CheckApplyFilterButtonEnabling();
				((ListView)sender).SelectedItem = null; //de-select
			};



			//Content = mainList;

			//this part is added by pradipta

			ListView DateList = new ListView()
			{
				ItemsSource = WrappedDateItems,
				SeparatorVisibility = SeparatorVisibility.None,
				HasUnevenRows = true,
				ItemTemplate = new DataTemplate(typeof(WrappedItemSelectionDateListTemplate)),
			};
			DateList.ItemSelected += (sender, e) => {
				if (e.SelectedItem == null) return;
				var o = (WrappedSelection<T>)e.SelectedItem;				
				o.IsSelected = !o.IsSelected;
                CheckApplyFilterButtonEnabling();
				((ListView)sender).SelectedItem = null; //de-select
			};




            Grid outerGrid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Star },
                    new RowDefinition { Height = GridLength.Auto },
                }
            };



            ScrollView scrlView = new ScrollView();


			Grid grid = new Grid
            {
                VerticalOptions = LayoutOptions.Start,
				RowDefinitions =
				{
					 new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = new GridLength(140, GridUnitType.Absolute) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Absolute) },
					 new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = new GridLength(140, GridUnitType.Absolute) },
					 new RowDefinition { Height = new GridLength(1, GridUnitType.Absolute) },
					//new RowDefinition { Height = GridLength.Auto },
				}
            };

            Label appNameLbl = new Label();
            appNameLbl.Text = "APPLICATION NAME";
            appNameLbl.FontSize = 14;
            appNameLbl.TextColor = (Color)Application.Current.Resources["Primary"];
            appNameLbl.Margin = new Thickness(20, 20, 0, 10);


            BoxView SeparatorLine = new BoxView();

            SeparatorLine.BackgroundColor = (Color)Application.Current.Resources["SeparatorColor"];
            SeparatorLine.HeightRequest = 0.5;
			SeparatorLine.HorizontalOptions = LayoutOptions.FillAndExpand;
            SeparatorLine.VerticalOptions = LayoutOptions.End;
          



			Label requestDateLbl = new Label();
			requestDateLbl.Text = "REQUEST DATE";
			requestDateLbl.FontSize = 14;
			requestDateLbl.TextColor = (Color)Application.Current.Resources["Primary"];
			requestDateLbl.Margin = new Thickness(20, 20, 0, 10);


			BoxView SecondSeparatorLine = new BoxView();

			SecondSeparatorLine.BackgroundColor = (Color)Application.Current.Resources["SeparatorColor"];
			SecondSeparatorLine.HeightRequest = 0.5;
			SecondSeparatorLine.HorizontalOptions = LayoutOptions.FillAndExpand;
			SecondSeparatorLine.VerticalOptions = LayoutOptions.End;

            //Button ApplyFilterBtn = new Button();
            ApplyFilterBtn = new Button();
            ApplyFilterBtn.Text = "Apply Filter";
            ApplyFilterBtn.FontSize = 14;
            ApplyFilterBtn.HorizontalOptions = LayoutOptions.FillAndExpand;
            ApplyFilterBtn.VerticalOptions = LayoutOptions.End;
			ApplyFilterBtn.IsEnabled = false;
			ApplyFilterBtn.BackgroundColor = (Color)Application.Current.Resources["BannerColor"];
            ApplyFilterBtn.TextColor = (Color)Application.Current.Resources["LoginInActiveTextColor"];

            ApplyFilterBtn.Clicked += (sender, e) => {
             

               
                List<T> selectedDateItem = GetSelectedDate();

                //var PersonResultList = PendingRequestList.Where(pr => PersonList.Any(p => pr.PersonId == p.PersonId && p.Gender == "female")); 

                var navStack = App.CustomNavigation.NavigationStack;

                if (navStack.Count > 0)
                {
                    var bindingContext = navStack[navStack.Count - 1].BindingContext;

                    if (bindingContext is HomeViewModel)
                    { 
                        List<T> selecteDepartments = GetSelection();
                     var homeViewModelInstance = bindingContext as HomeViewModel;

                    if (homeViewModelInstance != null)
                    {
                        List<PendingRequests> pendingRequests = new List<PendingRequests>();

                        if (selecteDepartments.Count == 0)
                        {
                            homeViewModelInstance.PendingRequestList = new ObservableCollection<PendingRequests>(Helper.Instance.PendingRequestsList);
                        }
                        else
                        {

                            foreach (var item1 in selecteDepartments)
                            {

                                foreach (var item in homeViewModelInstance.PendingRequestList)
                                {

                                    var deptObj = item1 as DepartmentModel;

                                    if (deptObj.DeptName == item.ApplicationName)
                                    {
                                        pendingRequests.Add(item);
                                    }
                                }

                            }
                            homeViewModelInstance.PendingRequestList = new ObservableCollection<PendingRequests>(pendingRequests);
                        }
                    }
                  }
                    else if (bindingContext is PendingRequestsViewModel)
					{
						List<T> selectedRequestType = GetSelection();
                        var pendingRequestViewModelInstance = bindingContext as PendingRequestsViewModel;

						if (pendingRequestViewModelInstance != null)
						{
							List<PendingRequests> pendingRequests = new List<PendingRequests>();

							if (selectedRequestType.Count == 0)
							{
								pendingRequestViewModelInstance.PendingRequestList = new ObservableCollection<PendingRequests>(Helper.Instance.PendingRequestsList);
							}
							else
							{

                                foreach (var item1 in selectedRequestType)
								{

									foreach (var item in pendingRequestViewModelInstance.PendingRequestList)
									{

                                        var deptObj = item1 as PendingRequests;

                                        if (deptObj.RequestType == item.RequestType)
										{
											pendingRequests.Add(item);
										}
									}

								}
								pendingRequestViewModelInstance.PendingRequestList = new ObservableCollection<PendingRequests>(pendingRequests);
							}
						}
					}
                }             

                Application.Current.MainPage.Navigation.PopModalAsync();
			};


			grid.Children.Add(appNameLbl);
            grid.Children.Add(mainList);
            grid.Children.Add(SeparatorLine);
            grid.Children.Add(requestDateLbl);
            grid.Children.Add(DateList);
			grid.Children.Add(SecondSeparatorLine);
           
			Grid.SetRow(appNameLbl, 0);
            Grid.SetRow(mainList, 1);			
            Grid.SetRow(SeparatorLine, 2);
            Grid.SetRow(requestDateLbl, 3);
            Grid.SetRow(DateList, 4);
			Grid.SetRow(SecondSeparatorLine, 5);
            //Grid.SetRow(ApplyFilterBtn, 6);

            scrlView.Content = grid;

            outerGrid.Children.Add(scrlView);
			outerGrid.Children.Add(ApplyFilterBtn);
			Grid.SetRow(scrlView, 0);
			Grid.SetRow(ApplyFilterBtn, 1);


			Content = outerGrid;		
          

			//if (Device.OS == TargetPlatform.Windows)
			//{   // fix issue where rows are badly sized (as tall as the screen) on WinPhone8.1
			//	mainList.RowHeight = 40;
			//	// also need icons for Windows app bar (other platforms can just use text)
			//	ToolbarItems.Add(new ToolbarItem("All", "check.png", SelectAll, ToolbarItemOrder.Primary));
			//	ToolbarItems.Add(new ToolbarItem("None", "cancel.png", SelectNone, ToolbarItemOrder.Primary));
			//}
			//else
			//{
				ToolbarItems.Add(new ToolbarItem("All", null, SelectAll, ToolbarItemOrder.Primary));
				ToolbarItems.Add(new ToolbarItem("None", null, SelectNone, ToolbarItemOrder.Primary));
			//}
		}

        private void CheckApplyFilterButtonEnabling()
        {
            var a = GetSelection();
            var b = GetSelectedDate();

            if(a.Count >0 || b.Count> 0)
            {
                ApplyFilterBtn.IsEnabled = true;
                ApplyFilterBtn.BackgroundColor = (Color)Application.Current.Resources["Primary"];
                ApplyFilterBtn.TextColor = (Color)Application.Current.Resources["LoginActiveTextColor"];
            }
            else
            {
                ApplyFilterBtn.IsEnabled = false;
                ApplyFilterBtn.BackgroundColor = (Color)Application.Current.Resources["BannerColor"];
                ApplyFilterBtn.TextColor = (Color)Application.Current.Resources["LoginInActiveTextColor"];
            }
        }

        void SelectAll()
		{
			foreach (var wi in WrappedItems)
			{
				wi.IsSelected = true;
			}
		}
		void SelectNone()
		{
			foreach (var wi in WrappedItems)
			{
				wi.IsSelected = false;
			}
		}
		public List<T> GetSelection()
		{
			return WrappedItems.Where(item => item.IsSelected).Select(wrappedItem => wrappedItem.Item).ToList();
		}

		public List<T> GetSelectedDate()
		{
			return WrappedDateItems.Where(item => item.IsSelected).Select(wrappedItem => wrappedItem.Item).ToList();
		}


	}
}
