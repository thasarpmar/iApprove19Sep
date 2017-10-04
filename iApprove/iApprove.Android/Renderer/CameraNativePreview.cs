using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AG = Android.Graphics;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Hardware;
using System.Threading.Tasks;
using static Android.Hardware.Camera;
using System.IO;
using iApprove.Model;
using System.Threading;
using Android.Media;

namespace iApprove.Droid.Renderer
{
	public class CameraNativePreview : ViewGroup, ISurfaceHolderCallback, IPictureCallback
	{
		public object capturePhotoHandler = new object();
		SurfaceView surfaceView;
		ISurfaceHolder holder;
		Camera.Size previewSize;
		IList<Camera.Size> supportedPreviewSizes;
		Camera camera;
		IWindowManager windowManager;
		public CameraDataModel SourceCameraDataModel;

		static bool IsFileSaved = true;

		public bool IsPreviewing { get; set; }

		public Camera Preview
		{
			get { return camera; }
			set
			{
				camera = value;
				if (camera != null)
				{
					supportedPreviewSizes = Preview.GetParameters().SupportedPreviewSizes;
					RequestLayout();
				}
			}
		}

		public CameraNativePreview(Context context)
			: base(context)
		{
			surfaceView = new SurfaceView(context);
			AddView(surfaceView);

			windowManager = Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();

			IsPreviewing = false;
			holder = surfaceView.Holder;
			holder.AddCallback(this);
		}

		protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
		{
			int width = ResolveSize(SuggestedMinimumWidth, widthMeasureSpec);
			int height = ResolveSize(SuggestedMinimumHeight, heightMeasureSpec);
			SetMeasuredDimension(width, height);

			if (supportedPreviewSizes != null)
			{
				previewSize = GetOptimalPreviewSize(supportedPreviewSizes, width, height);
			}
		}

		protected override void OnLayout(bool changed, int l, int t, int r, int b)
		{
			var msw = MeasureSpec.MakeMeasureSpec(r - l, MeasureSpecMode.Exactly);
			var msh = MeasureSpec.MakeMeasureSpec(b - t, MeasureSpecMode.Exactly);

			surfaceView.Measure(msw, msh);
			surfaceView.Layout(0, 0, r - l, b - t);
		}

		public void SurfaceCreated(ISurfaceHolder holder)
		{
			try
			{
				if (Preview != null)
				{
					Preview.SetPreviewDisplay(holder);
				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(@"			ERROR: ", ex.Message);
			}
		}

		public void SurfaceDestroyed(ISurfaceHolder holder)
		{
			if (Preview != null)
			{
				Preview.StopPreview();
			}
		}

		public void SurfaceChanged(ISurfaceHolder holder, Android.Graphics.Format format, int width, int height)
		{
			try
			{
				var parameters = Preview.GetParameters();
				parameters.SetPreviewSize(previewSize.Width, previewSize.Height);
				RequestLayout();

				switch (windowManager.DefaultDisplay.Rotation)
				{
					case SurfaceOrientation.Rotation0:
						camera.SetDisplayOrientation(90);
						break;
					case SurfaceOrientation.Rotation90:
						camera.SetDisplayOrientation(0);
						break;
					case SurfaceOrientation.Rotation270:
						camera.SetDisplayOrientation(180);
						break;
				}

				Preview.SetParameters(parameters);
				Preview.StartPreview();
				IsPreviewing = true;
			}
			catch (Exception ex)
			{

			}
		}

		Camera.Size GetOptimalPreviewSize(IList<Camera.Size> sizes, int w, int h)
		{
			const double AspectTolerance = 0.1;
			double targetRatio = (double)w / h;

			if (sizes == null)
			{
				return null;
			}

			Camera.Size optimalSize = null;
			double minDiff = double.MaxValue;

			int targetHeight = h;
			int i = 0;
			foreach (Camera.Size size in sizes)
			{
				double ratio = (double)size.Height / size.Width;
				double gg = Math.Abs(ratio - targetRatio);
				if (Math.Abs(ratio - targetRatio) > AspectTolerance)
					continue;
				if (Math.Abs(size.Height - targetHeight) < minDiff)
				{
					optimalSize = size;
					minDiff = Math.Abs(size.Height - targetHeight);
				}
				i++;
			}
			//optimalSize = sizes[0];
			if (optimalSize == null)
			{
				minDiff = double.MaxValue;
				foreach (Camera.Size size in sizes)
				{
					if (Math.Abs(size.Height - targetHeight) < minDiff)
					{
						optimalSize = size;
						minDiff = Math.Abs(size.Height - targetHeight);
					}
				}
			}

			return optimalSize;
		}

		public async Task TakeSnaps(CameraDataModel cameraModel)
		{
			try
			{
				if (IsFileSaved)
				{
					IsFileSaved = false;
					//IPictureCallback pictureCallback = new CameraNativePreview(Context);
					this.SourceCameraDataModel = cameraModel;
					camera.EnableShutterSound(true);
					camera.TakePicture(null, null, this);
				}
			}
			catch (Exception ex)
			{

			}
		}

		public void OnPictureTaken(byte[] data, Camera camera)
		{
			try
			{
				switch (windowManager.DefaultDisplay.Rotation)
				{
					case SurfaceOrientation.Rotation0:
						SavePicture(data, 90, camera);
						break;
					case SurfaceOrientation.Rotation180:
						break;
					case SurfaceOrientation.Rotation270:
						SavePicture(data, 180, camera);
						break;
					case SurfaceOrientation.Rotation90:
						SavePicture(data, 0, camera);
						break;
					default:
						break;
				}
			}
			catch (Exception ex)
			{

			}
		}

		private void SavePicture(byte[] data, int rotate, Camera camera)
		{
			try
			{
				Task.Factory.StartNew(() =>
				{
					MediaActionSound sound = new MediaActionSound();
					sound.Play(MediaActionSoundType.ShutterClick);
				});

				//Validate Path value
				string photoName = "";
				string photoDir = "";

				if (SourceCameraDataModel != null)
				{
					photoDir = SourceCameraDataModel.FileDir;
					photoName = SourceCameraDataModel.FileName;
				}
				if (string.IsNullOrEmpty(photoName))
				{
					photoName =  DateTime.Now.ToString("ddMMyyyyhhmmsstt") + ".jpg";
				}
				if (string.IsNullOrEmpty(photoDir))
				{
					photoDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
					var folder = new Java.IO.File(photoDir + "/SamplePics/");
					if (!folder.Exists())
						folder.Mkdir();
					photoDir = folder.Path;
				}
				string photoPath = System.IO.Path.Combine(photoDir,photoName);


				//Create JPG image from byte array
				Task.Factory.StartNew(() =>
				{
					
						using (var file = new FileStream(photoPath, FileMode.Create))
						{
							try
							{
								GC.Collect();
								AG.BitmapFactory.Options options = new AG.BitmapFactory.Options();
								//options.InSampleSize = 8;
								AG.Bitmap bitmap = AG.BitmapFactory.DecodeByteArray(data, 0, data.Length, options);
								AG.Matrix mtx = new AG.Matrix();
								mtx.PreRotate(rotate);
								bitmap = AG.Bitmap.CreateBitmap(bitmap, 0, 0, bitmap.Width, bitmap.Height, mtx, false);
								bitmap.Compress(AG.Bitmap.CompressFormat.Jpeg, 30, file);
								bitmap.Recycle();
								GC.Collect();
								bitmap = null;
							}
							catch (Exception ex)
							{

							}
						}

				});
				//
				MainActivity.CurrentActivity.RunOnUiThread(() =>
					{
						if (camera != null)
						{
							camera.StartPreview();
							IsPreviewing = true;
							IsFileSaved = true;
							CameraViewRenderer.camPreview.PhotoCallBack.Execute(SourceCameraDataModel);
							// CameraSnapButtonModel Model = new CameraSnapButtonModel { Data = data, ButtonProperties = CameraPreviewRenderer.camPreview.ClickedButtonProperties, FilePath = settingsPath };
							//CameraPreviewRenderer.camPreview.ButtonImageEvent.Invoke(Model, null);
						}
					});

			}
			catch (Exception ex)
			{
				throw;
			}
		}
	}
}