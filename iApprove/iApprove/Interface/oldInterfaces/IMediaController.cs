using System;
using System.Threading.Tasks;

namespace iApprove
{
	public interface IMediaController
	{
		Task<string> TakePhoto(string fileName, string dirToSave, bool isFromGallery);
		Task<System.IO.Stream> TakePhotoAsStream(string fileName, string dirToSave, bool isFromGallery);
		Task<string> TakeVideo(string fileName, string dirToSave, bool isFromGallery);
		Task<System.IO.Stream> TakeVideoAsStream(string fileName, string dirToSave, bool isFromGallery);

		bool IsCameraSupported { get; }
		bool IsTakePhotoSupported { get; }
		bool IsTakeVideoSupported { get; }
		bool IsPickPhotoSupported { get; }
		bool IsPickVideoSupported { get; }
	}
}