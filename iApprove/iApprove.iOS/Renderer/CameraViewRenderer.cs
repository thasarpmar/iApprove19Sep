using System;
using System.IO;
using AVFoundation;
using CoreGraphics;
using Foundation;
using iApprove.CustomControl;
using iApprove.iOS;
using iApprove.Model;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomCameraView), typeof(CameraViewRenderer))]
namespace iApprove.iOS
{
	public class CameraViewRenderer : ViewRenderer
	{
		AVCaptureSession captureSession;
		AVCaptureDeviceInput captureDeviceInput;
		AVCaptureStillImageOutput stillImageOutput;
		UIView liveCameraStream;
		UIButton takePhotoButton;
		UIButton toggleCameraButton;
		static CameraDataModel cameraModel;
		UIButton toggleFlashButton;

		byte[] encodedDataAsBytes;
		UIImage returnImage = null;
		UIImage photo = null;
		private CustomCameraView cameraView;

		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null || Element == null)
			{
				return;
			}

			if (e.NewElement != null)
			{
				this.cameraView = (CustomCameraView)e.NewElement;
				this.cameraView.CaptureCommand = new Command((data) =>
				{
					if (photo == null)
					{
						cameraModel = (CameraDataModel)data;
						CapturePhoto(cameraModel.FileDir);
					}

				},
				(condition) =>
				{
					return true;
				});

			}

			try
			{
				SetupUserInterface();
				SetupEventHandlers();
				SetupLiveCameraStream();
				AuthorizeCameraUse();
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(@"			ERROR: ", ex.Message);
			}
		}

		void SetupUserInterface()
		{
			var centerButtonX = this.Bounds.GetMidX() - 35f;
			var topLeftX = this.Bounds.X + 25;
			var topRightX = this.Bounds.Right - 65;
			var bottomButtonY = this.Bounds.Bottom - 150;
			var topButtonY = this.Bounds.Top + 15;
			var buttonWidth = 50;
			var buttonHeight = 50;
			var test = UIScreen.MainScreen.Bounds.Size.Width;

			liveCameraStream = new UIView()
			{
				Frame = new CGRect(UIScreen.MainScreen.Bounds.X, UIScreen.MainScreen.Bounds.Y, UIScreen.MainScreen.Bounds.Size.Width, UIScreen.MainScreen.Bounds.Size.Height)
			};

			takePhotoButton = new UIButton()
			{
				Frame = new CGRect(275f, 225f, buttonWidth, buttonHeight)
			};
			takePhotoButton.SetBackgroundImage(UIImage.FromFile("TakePhotoButton.png"), UIControlState.Normal);

			toggleCameraButton = new UIButton()
			{
				Frame = new CGRect(topRightX, topButtonY + 5, 35, 26)
			};
			toggleCameraButton.SetBackgroundImage(UIImage.FromFile("ToggleCameraButton.png"), UIControlState.Normal);

			toggleFlashButton = new UIButton()
			{
				Frame = new CGRect(topLeftX, topButtonY, 37, 37)
			};
			toggleFlashButton.SetBackgroundImage(UIImage.FromFile("NoFlashButton.png"), UIControlState.Normal);

			this.Add(liveCameraStream);
			//this.Add(takePhotoButton);
			//this.Add(toggleCameraButton);
			//this.Add(toggleFlashButton);
		}

		void SetupEventHandlers()
		{
			takePhotoButton.TouchUpInside += (object sender, EventArgs e) =>
			{
				CapturePhoto("");
			};

			toggleCameraButton.TouchUpInside += (object sender, EventArgs e) =>
			{
				ToggleFrontBackCamera();
			};

			toggleFlashButton.TouchUpInside += (object sender, EventArgs e) =>
			{
				ToggleFlash();
			};
		}

		async public void CapturePhoto(string filePathToSave)
		{
			var videoConnection = stillImageOutput.ConnectionFromMediaType(AVMediaType.Video);
			var sampleBuffer = await stillImageOutput.CaptureStillImageTaskAsync(videoConnection);
			var jpegImage = AVCaptureStillImageOutput.JpegStillToNSData(sampleBuffer);

			photo = new UIImage(jpegImage);
			var documentDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			NSData imgData = photo.AsJPEG();
			NSError err = null;


			//Validate Path value
			string photoName = "";
			string photoDir = "";

			if (cameraModel != null)
			{
				photoDir = cameraModel.FileDir;
				photoName = cameraModel.FileName;
			}
			if (string.IsNullOrEmpty(photoName))
			{
				photoName =  DateTime.Now.ToString("ddMMyyyyhhmmsstt") + ".jpg";
			}
			if (string.IsNullOrEmpty(photoDir))
			{
				photoDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
				photoDir = Path.Combine(documentDirectory, "SamplePics");
				if (!Directory.Exists(photoDir))
				{
					Directory.CreateDirectory(photoDir);
				}
			}
			string photoPath = System.IO.Path.Combine(photoDir, photoName);

			if (imgData.Save(photoPath, false, out err))
			{
				Console.WriteLine("saved as " + photoPath);
				photo = null;
				this.cameraView.PhotoCallBack.Execute(cameraModel);
			}
			else
			{
				Console.WriteLine("NOT saved as " + photoPath + " because" + err.LocalizedDescription);
			}
		}

		void ToggleFrontBackCamera()
		{
			var devicePosition = captureDeviceInput.Device.Position;
			if (devicePosition == AVCaptureDevicePosition.Front)
			{
				devicePosition = AVCaptureDevicePosition.Back;
			}
			else
			{
				devicePosition = AVCaptureDevicePosition.Front;
			}

			var device = GetCameraForOrientation(devicePosition);
			ConfigureCameraForDevice(device);

			captureSession.BeginConfiguration();
			captureSession.RemoveInput(captureDeviceInput);
			captureDeviceInput = AVCaptureDeviceInput.FromDevice(device);
			captureSession.AddInput(captureDeviceInput);
			captureSession.CommitConfiguration();
		}

		void ToggleFlash()
		{
			var device = captureDeviceInput.Device;

			var error = new NSError();
			if (device.HasFlash)
			{
				if (device.FlashMode == AVCaptureFlashMode.On)
				{
					device.LockForConfiguration(out error);
					device.FlashMode = AVCaptureFlashMode.Off;
					device.UnlockForConfiguration();
					toggleFlashButton.SetBackgroundImage(UIImage.FromFile("NoFlashButton.png"), UIControlState.Normal);
				}
				else
				{
					device.LockForConfiguration(out error);
					device.FlashMode = AVCaptureFlashMode.On;
					device.UnlockForConfiguration();
					toggleFlashButton.SetBackgroundImage(UIImage.FromFile("FlashButton.png"), UIControlState.Normal);
				}
			}
		}

		AVCaptureDevice GetCameraForOrientation(AVCaptureDevicePosition orientation)
		{
			var devices = AVCaptureDevice.DevicesWithMediaType(AVMediaType.Video);

			foreach (var device in devices)
			{
				if (device.Position == orientation)
				{
					return device;
				}
			}
			return null;
		}

		void SetupLiveCameraStream()
		{
			captureSession = new AVCaptureSession();

			var viewLayer = liveCameraStream.Layer;
			var videoPreviewLayer = new AVCaptureVideoPreviewLayer(captureSession)
			{
				Frame = liveCameraStream.Bounds
			};
			liveCameraStream.Layer.AddSublayer(videoPreviewLayer);

			var captureDevice = AVCaptureDevice.DefaultDeviceWithMediaType(AVMediaType.Video);
			ConfigureCameraForDevice(captureDevice);
			captureDeviceInput = AVCaptureDeviceInput.FromDevice(captureDevice);
			var dictionary = new NSMutableDictionary();
			dictionary[AVVideo.CodecKey] = new NSNumber((int)AVVideoCodec.JPEG);
			stillImageOutput = new AVCaptureStillImageOutput()
			{
				OutputSettings = new NSDictionary()
			};

			captureSession.AddOutput(stillImageOutput);
			captureSession.AddInput(captureDeviceInput);
			captureSession.StartRunning();


		}

		void ConfigureCameraForDevice(AVCaptureDevice device)
		{
			var error = new NSError();
			if (device.IsFocusModeSupported(AVCaptureFocusMode.ContinuousAutoFocus))
			{
				device.LockForConfiguration(out error);
				device.FocusMode = AVCaptureFocusMode.ContinuousAutoFocus;
				device.UnlockForConfiguration();
			}
			else if (device.IsExposureModeSupported(AVCaptureExposureMode.ContinuousAutoExposure))
			{
				device.LockForConfiguration(out error);
				device.ExposureMode = AVCaptureExposureMode.ContinuousAutoExposure;
				device.UnlockForConfiguration();
			}
			else if (device.IsWhiteBalanceModeSupported(AVCaptureWhiteBalanceMode.ContinuousAutoWhiteBalance))
			{
				device.LockForConfiguration(out error);
				device.WhiteBalanceMode = AVCaptureWhiteBalanceMode.ContinuousAutoWhiteBalance;
				device.UnlockForConfiguration();
			}
		}

		async void AuthorizeCameraUse()
		{
			var authorizationStatus = AVCaptureDevice.GetAuthorizationStatus(AVMediaType.Video);
			if (authorizationStatus != AVAuthorizationStatus.Authorized)
			{
				await AVCaptureDevice.RequestAccessForMediaTypeAsync(AVMediaType.Video);
			}
		}
	}
}
