using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Locations;
using Android.Widget;
using iApprove.CustomControl;
using iApprove.Droid.Renderer;
using iApprove.Interface;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace iApprove.Droid.Renderer
{
	public class CustomMapRenderer : MapRenderer, GoogleMap.IInfoWindowAdapter, IOnMapReadyCallback,IMapCallBack
	{
		GoogleMap map;
		List<CustomPin> customPins;
		bool isDrawn;
		CustomMap myCustomMap;
		private bool isAutoFocusMyLocation = true;
		private bool isMoveMapRequied = true;
		float prevZoomLevel = Constants.DefaultMapZoomLevel;

		public CustomMapRenderer()
		{
		}
		protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null)
			{
				//map.InfoWindowClick -= OnInfoWindowClick;
			}

			if (e.NewElement != null)
			{
				myCustomMap = (CustomMap)e.NewElement;
				myCustomMap.MapCallBack = this;
				customPins = myCustomMap.CustomPins;
				((MapView)Control).GetMapAsync(this);
			}
		}

		private float GetZoomLevel()
		{
			//Get prevZoom Level
			if (prevZoomLevel == Constants.DefaultMapZoomLevel)
			{
				prevZoomLevel = 15.1f;
			}
			else if(prevZoomLevel != Constants.DefaultMapZoomLevel && 
			        map.CameraPosition.Zoom != prevZoomLevel && 
			        map.CameraPosition.Zoom != map.MinZoomLevel)
			{
				prevZoomLevel = map.CameraPosition.Zoom;
			}
			return prevZoomLevel;
		}

		private void CreateMarker(Position pos, string title, string content)
		{
			map.Clear();
			var marker = new MarkerOptions();
			marker.SetPosition(new LatLng(pos.Latitude, pos.Longitude));
			marker.SetTitle(title);
			marker.SetSnippet(content);
			marker.Draggable(true);

			marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.pin));
			map.AddMarker(marker);

			////Get prevZoom Level
			//if (prevZoomLevel == Constants.DefaultMapZoomLevel)
			//{
			//	prevZoomLevel = 15.1f;
			//}
			//else if(prevZoomLevel != Constants.DefaultMapZoomLevel && 
			//        map.CameraPosition.Zoom != prevZoomLevel && 
			//        map.CameraPosition.Zoom != map.MinZoomLevel)
			//{
			//	prevZoomLevel = map.CameraPosition.Zoom;
			//}

			if (isMoveMapRequied)
			{
				map.MoveCamera(CameraUpdateFactory.NewLatLngZoom(
					new LatLng(pos.Latitude, pos.Longitude), prevZoomLevel));
			}
			isMoveMapRequied = true;
			map.AnimateCamera(CameraUpdateFactory.ZoomTo(prevZoomLevel),5000, null);
		}

		private async Task LocatePositionOnMap(double lat, double lang, bool isMarkerRequired = true, Marker myCustomMarker =null)
		{
			var result = "";
			var fortMasonPosition = new Position(lat, lang); //13.067439, 80.237617);
			var possibleAddresses = await App.geoCoder.GetAddressesForPositionAsync(fortMasonPosition);
			foreach (var a in possibleAddresses)
			{
				result += a + "\n"; break;
			};
			if (isMarkerRequired)
			{
				CreateMarker(new Position(lat, lang), "Address:", result);
			}
			else if (myCustomMarker != null) 
			{
				myCustomMarker.Title = "Address:";
				myCustomMarker.Snippet = result;
				//myCustomMarker.ShowInfoWindow();
				if (isMoveMapRequied)
				{
					GetZoomLevel();
					map.MoveCamera(CameraUpdateFactory.NewLatLngZoom(
						new LatLng(lat, lang), prevZoomLevel));
				}
				isMoveMapRequied = true;
			}

			myCustomMap.MapHandler.UpdateLocation(lat, lang, result);
		}

		public void OnMapReady(GoogleMap googleMap)
		{
			map = googleMap;
			map.SetInfoWindowAdapter(this);
			map.MarkerDragStart += (object sender, GoogleMap.MarkerDragStartEventArgs e) =>
			{
				e.Marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.ic_room_white_selected));
				e.Marker.HideInfoWindow();
			};
			map.MarkerDragEnd += (object sender, GoogleMap.MarkerDragEndEventArgs e) =>
			{
				e.Marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.pin));
				isMoveMapRequied = true;
				Marker marker = e.Marker;
				LocatePositionOnMap(marker.Position.Latitude, marker.Position.Longitude, false, marker);
			};
			map.MyLocationChange += (object sender, GoogleMap.MyLocationChangeEventArgs e) =>
			{
				if (isAutoFocusMyLocation)
				{
					//map.MoveCamera(CameraUpdateFactory.NewLatLngZoom(
					//	new LatLng(e.Location.Latitude, e.Location.Longitude), 15));
					//map.AnimateCamera(CameraUpdateFactory.ZoomTo(15), 5000, null);
                    LocatePositionOnMap(e.Location.Latitude, e.Location.Longitude);
					isAutoFocusMyLocation = false;
                    
				}
			};

			if (map!=null) //&& !isDrawn) // && customPins!=null)
			{
				//map.MapLongClick += (object sender, GoogleMap.MapLongClickEventArgs e) =>
				//{
				//	isMoveMapRequied = false;
				//	LocatePositionOnMap(e.Point.Latitude, e.Point.Longitude);

				//	/*var result = "";
				//	var fortMasonPosition = new Position(e.Point.Latitude, e.Point.Longitude); //13.067439, 80.237617);
				//	var possibleAddresses = await App.geoCoder.GetAddressesForPositionAsync(fortMasonPosition);
				//	foreach (var a in possibleAddresses){
				//		result += a + "\n"; break;
				//	};
				//	CreateMarker(new Position(e.Point.Latitude, e.Point.Longitude), "My Location", result);

				//	myCustomMap.MapHandler.UpdateLocation(e.Point.Latitude, e.Point.Longitude);*/
				//};

				map.MyLocationEnabled = true;

				map.MyLocationButtonClick += (object sender, GoogleMap.MyLocationButtonClickEventArgs e) =>
			   {
					prevZoomLevel = Constants.DefaultMapZoomLevel;
				   isAutoFocusMyLocation = true;
				   e.Handled = false;
					//Write code here to get current location and show in map
					//myCustomMap.MapHandler.GetCurrentLocation();
			   };
				isDrawn = true;
			}
		}

		protected override void OnLayout(bool changed, int l, int t, int r, int b)
		{
			base.OnLayout(changed, l, t, r, b);

			if (changed)
			{
				isDrawn = false;
			}
		}

		public Android.Views.View GetInfoContents(Marker marker)
		{
			var inflater = Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService) as Android.Views.LayoutInflater;
			if (inflater != null)
			{
				Android.Views.View view;

				view = inflater.Inflate(Resource.Layout.MapInfoWindow, null);

				var infoTitle = view.FindViewById<TextView>(Resource.Id.InfoWindowTitle);
				var infoSubtitle = view.FindViewById<TextView>(Resource.Id.InfoWindowSubtitle);

				if (infoTitle != null)
				{
					infoTitle.Text = marker.Title;
				}
				if (infoSubtitle != null)
				{
					infoSubtitle.Text = marker.Snippet;
				}
				return view;
			}
			return null;
		}

		public Android.Views.View GetInfoWindow(Marker marker)
		{
			return null;
		}

		CustomPin GetCustomPin(Marker annotation)
		{
			var position = new Position(annotation.Position.Latitude, annotation.Position.Longitude);
			foreach (var pin in customPins)
			{
				if (pin.Pin.Position == position)
				{
					return pin;
				}
			}
			return null;
		}

		public void CreatePin(double latitude, double longitude, string title, string address)
		{
			CreateMarker(new Position(latitude, longitude), title, address);
		}
	}
}
