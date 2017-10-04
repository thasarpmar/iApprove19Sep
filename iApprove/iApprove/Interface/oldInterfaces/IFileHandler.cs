using System;
using System.Threading.Tasks;

namespace iApprove
{
	public interface IFileHandler
	{
		Task<string> ReadFileAsync(string fileName, string dirPath);
		Task<byte[]> ReadFileBytesAsync(string fileName, string dirPath);
		Task<string> ReadFileResAsync(string fileName);
		Task<byte[]> ReadFileResBytesAsync(string fileName);
		Task<bool> SaveFileAsync(byte[] fileData, string fileName, string dirPath, FileType fileType);
		Task<bool> SaveFileAsync(string textData, string fileName, string dirPath, FileType fileType);
		bool IsFileExists(string path);
		Task<string> MoveResourceFileToLocal(string fileName, string targetFolder);
		Task<string[]> ReadFileList(string dirName,string fileExt);
		Task<bool> CreateDirectory(string dirName);
		Task<bool> MoveFile(string fileName, string srcPath, string destPath);
		Task<bool> DeleteFileAsync(string fileName, string dirPath=null);
		bool DeleteFile(string path);
	}
}
