using System;
using System.IO;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace iApprove
{
	public class MediaService : IMediaController
	{
		private IMessageHandler _messageHandler;
		private static MediaService _mediaHandler;

		public static MediaService GetInstance(IMessageHandler messageHandler)
		{
			_mediaHandler = new MediaService(messageHandler);
			return _mediaHandler;
		}

		public bool IsCameraSupported
		{
			get
			{
				return CrossMedia.Current.IsCameraAvailable;
			}
		}

		public bool IsTakePhotoSupported
		{
			get
			{
				return CrossMedia.Current.IsTakePhotoSupported;
			}
		}

		public bool IsTakeVideoSupported
		{
			get
			{
				return CrossMedia.Current.IsTakeVideoSupported;
			}
		}

		public bool IsPickPhotoSupported
		{
			get
			{
				return CrossMedia.Current.IsPickPhotoSupported;
			}
		}

		public bool IsPickVideoSupported
		{
			get
			{
				return CrossMedia.Current.IsPickVideoSupported;
			}
		}

		public MediaService(IMessageHandler messageHandler)
		{
			this._messageHandler = messageHandler;
		}

		private Plugin.Media.Abstractions.StoreCameraMediaOptions PreparePath(string fileName, string dirToSave, bool isPhoto)
		{
			// Supply media options for saving our photo after it's taken.
			var mediaOptions = new Plugin.Media.Abstractions.StoreCameraMediaOptions
			{
				Directory = "photos",
				Name = $"{DateTime.UtcNow}.jpg",
				DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Rear,
				AllowCropping = true,
				CompressionQuality = 50,
				PhotoSize = PhotoSize.Medium
			};

			if (!String.IsNullOrEmpty(dirToSave))
			{
				mediaOptions.Directory = dirToSave;
			}
			if (!String.IsNullOrEmpty(fileName))
			{
				mediaOptions.Name = fileName;
			}
			return mediaOptions;
		}
		private Plugin.Media.Abstractions.StoreVideoOptions PreparePathVideo(string fileName, string dirToSave)
		{
			// Supply media options for saving our photo after it's taken.
			var mediaOptions = new Plugin.Media.Abstractions.StoreVideoOptions
			{
				Directory = "photos",
				Name = $"{DateTime.UtcNow}.mp4",
				DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Rear,
				//AllowCropping = true
			};

			if (!String.IsNullOrEmpty(dirToSave))
			{
				mediaOptions.Directory = dirToSave;
			}
			if (!String.IsNullOrEmpty(fileName))
			{
				mediaOptions.Name = fileName;
			}
			return mediaOptions;
		}

		public async Task<string> TakePhoto(string fileName, string dirToSave, bool isFromGallery)
		{
			if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
			{
				if (null != this._messageHandler)
					await this._messageHandler.ShowMessage(Constants.msgCameraNotsupport, Constants.strCamera);
				return Constants.msgCameraNotsupport;
			}

			var mediaOptions = PreparePath(fileName, "", true);

			// Take a photo of the business receipt.
			if (!isFromGallery)
			{
				Task<MediaFile> camera = CrossMedia.Current.TakePhotoAsync(mediaOptions);
				MediaFile file = await camera;
				if (file != null)
				{
					IFileHandler fileHandler = DependencyService.Get<IFileHandler>();
					await fileHandler.CreateDirectory(dirToSave);
					if (!string.IsNullOrEmpty(dirToSave))
					{
						string destPath = Path.Combine(dirToSave, Path.GetFileName(file.Path));
						await fileHandler.MoveFile("", file.Path, destPath);
						return destPath;
					}
					else
					{
						return file.Path;
					}
				}
				else
					return "";
			}
			else
			{
				Task<MediaFile> camera = CrossMedia.Current.PickPhotoAsync();
				MediaFile file = await camera;
				if (file != null)
					return file.Path;
				else
					return "";
			}
		}

		public async Task<System.IO.Stream> TakePhotoAsStream(string fileName, string dirToSave, bool isFromGallery)
		{
			if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
			{
				if (null != this._messageHandler)
					await this._messageHandler.ShowMessage(Constants.msgCameraNotsupport, Constants.strCamera);
				return null;
			}

			var mediaOptions = PreparePath(fileName, dirToSave, true);

			// Take a photo of the business receipt.
			if (!isFromGallery)
			{
				Task<MediaFile> camera = CrossMedia.Current.TakePhotoAsync(mediaOptions);
				MediaFile file = await camera;
				if (file != null)
					return file.GetStream();
				else
					return null;
			}
			else
			{
				Task<MediaFile> camera = CrossMedia.Current.PickPhotoAsync();
				MediaFile file = await camera;
				if (file != null)
					return file.GetStream();
				else
					return null;
			}
		}

		public async Task<string> TakeVideo(string fileName, string dirToSave, bool isFromGallery)
		{
			if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakeVideoSupported)
			{
				if (null != this._messageHandler)
					await this._messageHandler.ShowMessage(Constants.msgCameraNotsupport, Constants.strCamera);
				return Constants.msgCameraNotsupport;
			}

			var mediaOptions = PreparePathVideo(fileName, dirToSave);

			// Take a photo of the business receipt.
			if (!isFromGallery)
			{
				Task<MediaFile> camera = CrossMedia.Current.TakeVideoAsync(mediaOptions);
				MediaFile file = await camera;
				if (file != null)
				{
					IFileHandler fileHandler = DependencyService.Get<IFileHandler>();
					await fileHandler.CreateDirectory(dirToSave);
					if (!string.IsNullOrEmpty(dirToSave))
					{
						string destPath = Path.Combine(dirToSave, Path.GetFileName(file.Path));
						await fileHandler.MoveFile("", file.Path, destPath);
						return destPath;
					}
					else
					{
						return file.Path;
					}
				}
				else
					return "";
			}
			else
			{
				Task<MediaFile> camera = CrossMedia.Current.PickVideoAsync();
				MediaFile file = await camera;
				if (file != null)
				{
					IFileHandler fileHandler = DependencyService.Get<IFileHandler>();
					await fileHandler.CreateDirectory(dirToSave);
					if (!string.IsNullOrEmpty(dirToSave))
					{
						string destPath = Path.Combine(dirToSave, Path.GetFileName(file.Path));
						await fileHandler.MoveFile("", file.Path, destPath);
						return destPath;
					}
					else
					{
						return file.Path;
					}
				}
				else
					return "";
			}
		}

		public async Task<Stream> TakeVideoAsStream(string fileName, string dirToSave, bool isFromGallery)
		{
			if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakeVideoSupported)
			{
				if (null != this._messageHandler)
					await this._messageHandler.ShowMessage(Constants.msgCameraNotsupport, Constants.strCamera);
				return null;
			}

			var mediaOptions = PreparePathVideo(fileName, dirToSave);

			// Take a photo of the business receipt.
			if (!isFromGallery)
			{
				Task<MediaFile> camera = CrossMedia.Current.TakeVideoAsync(mediaOptions);
				MediaFile file = await camera;
				if (file != null)
					return file.GetStream();
				else
					return null;
			}
			else
			{
				Task<MediaFile> camera = CrossMedia.Current.PickVideoAsync();
				MediaFile file = await camera;
				if (file != null)
					return file.GetStream();
				else
					return null;
			}

		}

		public void Dispose()
		{
			_messageHandler = null;
			_mediaHandler = null;
		}
	}
}
