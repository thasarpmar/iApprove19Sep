using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using Plugin.Permissions;

namespace iApprove.Droid
{
	//[Activity(Label = "ProjectTemplate", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	[Activity(Label = "iApprove", Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Portrait)]
	public class MainActivity : FormsApplicationActivity
	{
		public static MainActivity CurrentActivity { get; private set; }
		public static Bundle b;
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			CurrentActivity = this;
			global::Xamarin.Forms.Forms.Init(this, bundle);
			//TODO: Init cal for Google Maps
			Xamarin.FormsMaps.Init(this, bundle);
			b = bundle;
			LoadApplication(new App());
		}

		protected override void OnResume()
		{
			base.OnResume();
			Xamarin.FormsMaps.Init(this, b);
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
		{
			PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

		protected override void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);
			if (requestCode == 201 && App.NetworkListener != null)
			{
				App.NetworkListener.OnNetworkEnabled(true);
			}
			if (requestCode == 202 && App.NetworkListener != null)
			{
				App.NetworkListener.OnGPSEnabled(true);
			}
		}
	}
}

