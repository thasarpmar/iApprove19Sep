using System;
using Xamarin.Forms.Platform.Android;
using iApprove.Droid.Renderer;
using Xamarin.Forms;
using iApprove.CustomControl;
using Android.Hardware;
using iApprove.Model;
using iApprove.Enum;

[assembly: ExportRenderer(typeof(CustomCameraView), typeof(CameraViewRenderer))]
namespace iApprove.Droid.Renderer
{
	public class CameraViewRenderer : ViewRenderer<CustomCameraView, CameraNativePreview>
	{
		CameraNativePreview cameraNativePreview;
		bool IsSnapSaved = true;
		static public CustomCameraView camPreview;
		static public CameraDataModel cameraDataModel;

		protected override void OnElementChanged(ElementChangedEventArgs<CustomCameraView> e)
		{
			try
			{
				base.OnElementChanged(e);

				if (Control == null)
				{
					cameraNativePreview = new CameraNativePreview(Context);
					SetNativeControl(cameraNativePreview);
				}

				if (e.OldElement != null)
				{
					// Unsubscribe
				}
				if (e.NewElement != null)
				{
					Control.Preview = Camera.Open(0);
					camPreview = e.NewElement;
					camPreview.CaptureCommand = new Command(async (data) =>
					  {
						  try
						  {
							  
							  cameraDataModel = (CameraDataModel)data;
							 cameraNativePreview.SourceCameraDataModel = cameraDataModel;
							  await cameraNativePreview.TakeSnaps(cameraDataModel);
						  }
						  catch (Exception ex)
						  {

						  }
					  });
					// Subscribe
				}
			}
			catch (Exception ex)
			{

			}
		}

		async void OnCameraPreviewClicked(object sender, EventArgs e)
		{
			try
			{
				//cameraDataModel = (CameraDataModel)sender;
				//// camPreview.ClickedButtonProperties = (Image)sender;
				//cameraNativePreview.SourceCameraDataModel = cameraDataModel;
				//await cameraNativePreview.TakeSnaps(cameraDataModel);

				camPreview.PhotoCallBack.Execute("RESET");
			}
			catch (Exception ex) { }
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				Control.Preview.Release();
			}
			base.Dispose(disposing);
		}
	}
}
