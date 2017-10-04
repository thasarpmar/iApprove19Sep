using System;
using System.Collections.Generic;
using CoreGraphics;
using MapKit;
using iApprove.CustomControl;
using iApprove.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace iApprove.iOS.Renderer
{
	public class CustomMapRenderer : MapRenderer
	{
		UIView customPinView;
		List<CustomPin> customPins;
		CustomMap myCustomMap;
		float prevZoomLevel = Constants.DefaultMapZoomLevel;

		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null)
			{
				var nativeMap = Control as MKMapView;
				nativeMap.GetViewForAnnotation = null;
				//nativeMap.CalloutAccessryControlTapped -= OnCalloutAccessoryControlTapped;
				//nativeMap.DidSelectAnnotationView -= OnDidSelectAnnotationView;
				//nativeMap.DidDeselectAnnotationView -= OnDidDeselectAnnotationView;
				nativeMap.DidUpdateUserLocation -= _DidUpdateUserLocation;
				nativeMap = null;
			}

			if (e.NewElement != null)
			{
				myCustomMap = (CustomMap)e.NewElement;
				var nativeMap = Control as MKMapView;

				customPins = myCustomMap.CustomPins;	

				nativeMap.GetViewForAnnotation = GetViewForAnnotation;
				nativeMap.DidUpdateUserLocation += _DidUpdateUserLocation;
				nativeMap.AddGestureRecognizer(new UILongPressGestureRecognizer(async (UILongPressGestureRecognizer obj) =>
				{
					var touchPoint = obj.LocationInView(nativeMap);
					var touchMapPoint = nativeMap.ConvertPoint(touchPoint, nativeMap);
					var result = "";
					var fortMasonPosition = new Position(touchMapPoint.Latitude, touchMapPoint.Longitude);
					var possibleAddresses = await App.geoCoder.GetAddressesForPositionAsync(fortMasonPosition);
					foreach (var a in possibleAddresses)
					{
						result += a; break;
					};
					//result = result.Replace("\n", ",");
					myCustomMap.MapHandler.UpdateLocation(touchMapPoint.Latitude, touchMapPoint.Longitude, result);
				}));

				//nativeMap.CalloutAccessoryControlTapped += OnCalloutAccessoryControlTapped;
				//nativeMap.DidSelectAnnotationView += OnDidSelectAnnotationView;
				//nativeMap.DidDeselectAnnotationView += OnDidDeselectAnnotationView;
			}
		}

		async void _DidUpdateUserLocation(object sender, MKUserLocationEventArgs locEventArgs)
		{
			var lat = locEventArgs.UserLocation.Coordinate.Latitude;
			var longi = locEventArgs.UserLocation.Coordinate.Longitude;

			var result = "";
			var fortMasonPosition = new Position(lat, longi); //13.067439, 80.237617);
			var possibleAddresses = await App.geoCoder.GetAddressesForPositionAsync(fortMasonPosition);
			foreach (var a in possibleAddresses)
			{
				result += a + "\n"; break;
			};
			myCustomMap.MapHandler.UpdateLocation(lat, longi,result);
		}

		MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
		{
			MKAnnotationView annotationView = null;

			if (annotation is MKUserLocation)
				return null;

			var anno = annotation as MKPointAnnotation;
			var customPin = GetCustomPin(anno);
			if (customPin == null)
			{
				//throw new Exception("Custom pin not found");
				return null;
			}

			annotationView = mapView.DequeueReusableAnnotation(customPin.Id);
			if (annotationView == null)
			{
				annotationView = new CustomMKAnnotationView(annotation, customPin.Id);
				annotationView.Image = UIImage.FromFile("pin.png");
				annotationView.CalloutOffset = new CGPoint(0, 0);
				annotationView.LeftCalloutAccessoryView = new UIImageView(UIImage.FromFile("ic_room_white_selected@2x.png"));
				//annotationView.RightCalloutAccessoryView = UIButton.FromType(UIButtonType.DetailDisclosure);
				((CustomMKAnnotationView)annotationView).Id = customPin.Id;
				//((CustomMKAnnotationView)annotationView).Url = customPin.Url;
			}
			annotationView.CanShowCallout = true;

			return annotationView;
		}

		//void OnCalloutAccessoryControlTapped(object sender, MKMapViewAccessoryTappedEventArgs e)
		//{
		//	//var customView = e.View as CustomMKAnnotationView;
		//	//if (!string.IsNullOrWhiteSpace(customView.Url))
		//	//{
		//	//	UIApplication.SharedApplication.OpenUrl(new Foundation.NSUrl(customView.Url));
		//	//}
		//}

		//void OnDidSelectAnnotationView(object sender, MKAnnotationViewEventArgs e)
		//{
		//	var customView = e.View as CustomMKAnnotationView;
		//	customPinView = new UIView();

		//	//if (customView.Id == "Xamarin")
		//	//{
		//	//	customPinView.Frame = new CGRect(0, 0, 200, 84);
		//	//	var image = new UIImageView(new CGRect(0, 0, 200, 84));
		//	//	image.Image = UIImage.FromFile("xamarin.png");
		//	//	customPinView.AddSubview(image);
		//	//	customPinView.Center = new CGPoint(0, -(e.View.Frame.Height + 75));
		//	//	e.View.AddSubview(customPinView);
		//	//}
		//}

		//void OnDidDeselectAnnotationView(object sender, MKAnnotationViewEventArgs e)
		//{
		//	if (!e.View.Selected)
		//	{
		//		customPinView.RemoveFromSuperview();
		//		customPinView.Dispose();
		//		customPinView = null;
		//	}
		//}

		CustomPin GetCustomPin(MKPointAnnotation annotation)
		{
			if (annotation != null)
			{
				var position = new Position(annotation.Coordinate.Latitude, annotation.Coordinate.Longitude);
				foreach (var pin in myCustomMap.CustomPins)
				{
					if (pin.Pin.Position == position)
					{
						return pin;
					}
				}
			}
			return null;
		}
	}
}
