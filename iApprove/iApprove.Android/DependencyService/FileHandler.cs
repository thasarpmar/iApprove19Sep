using System;
using System.IO;
using System.Threading.Tasks;
using Android.App;
using iApprove.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(FileHandler))]
namespace iApprove.Droid
{
	public class FileHandler : IFileHandler
	{
		static string CreatePathToFile(string fileName, string dirPath)
		{
			if (!String.IsNullOrEmpty(dirPath))
			{
				return Path.Combine(dirPath, fileName);
			}
			var docsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			return Path.Combine(docsPath, fileName);
		}

		public async Task<string> ReadFileResAsync(string fileName)
		{
			var assembly = typeof(FileHandler).Assembly;
			Stream stream = assembly.GetManifestResourceStream(fileName);
			//string text = "";
			using (StreamReader sr = new System.IO.StreamReader(stream)) return await sr.ReadToEndAsync();
		}


		public async Task<byte[]> ReadFileResBytesAsync(string fileName)
		{
			var assembly = typeof(FileHandler).Assembly;
			Stream stream = assembly.GetManifestResourceStream(fileName);
			//string text = "";
			using (BufferedStream bf = new BufferedStream(stream))
			{
				byte[] data = new byte[bf.Length];
				bf.Read(data, 0, data.Length);
				return data;
			}
		}



		public async Task<string> ReadFileAsync(string fileName, string dirPath)
		{
			string path = CreatePathToFile(fileName, dirPath);
			using (StreamReader sr = File.OpenText(path))
			{
				return await sr.ReadToEndAsync();
			}
		}

		public async Task<byte[]> ReadFileBytesAsync(string fileName, string dirPath)
		{
			string path = CreatePathToFile(fileName, dirPath);

			//return File.ReadAllBytes(path);
			using (BufferedStream bf = new BufferedStream(File.OpenRead(path)))
			{
				byte[] data = new byte[bf.Length];
				bf.Read(data, 0, data.Length);
				return data;
			}
		}

		public async Task<bool> SaveFileAsync(string textData, string fileName, string dirPath, FileType fileType)
		{
			string path = CreatePathToFile(fileName, dirPath);
			using (StreamWriter sw = File.CreateText(path))
			{
				await sw.WriteAsync(textData);
			}
			return true;
		}

		public async Task<bool> SaveFileAsync(byte[] fileData, string fileName, string dirPath, FileType fileType)
		{
			string path = CreatePathToFile(fileName, dirPath);
			//File.WriteAllBytes(path, fileData);
			using (BufferedStream bf = new BufferedStream(File.Create(path)))
			{
				await bf.WriteAsync(fileData, 0, fileData.Length);
			}
			return true;
		}

		public bool IsFileExists(string path)
		{
			return System.IO.File.Exists(path);
		}

		public async Task<string> MoveResourceFileToLocal(string fileName, string targetFolder)
		{
			string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			string dbPath = Path.Combine(path, fileName);

			if (!File.Exists(dbPath))
			{
				using (var br = new BinaryReader(Application.Context.Assets.Open(fileName)))
				{
					using (var bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
					{
						byte[] buffer = new byte[2048];
						int length = 0;
						while ((length = br.Read(buffer, 0, buffer.Length)) > 0)
						{
							bw.Write(buffer, 0, length);
						}
					}
				}
			}
			return dbPath;
		}

		public async Task<string[]> ReadFileList(string dirName, string fileExt)
		{
			string[] fileList = new string[] { };
			string path = CreatePathToFile("", dirName);
			if (Directory.Exists(path))
			{
				var ext = string.IsNullOrEmpty(fileExt) ? "*" : fileExt;
				fileList = Directory.GetFiles(path, "*." + ext);
			}
			return fileList;
		}

		public async Task<bool> CreateDirectory(string dirName)
		{
			Directory.CreateDirectory(dirName);
			return true;
		}

		public async Task<bool> MoveFile(string fileName, string srcPath, string destPath)
		{
			if (File.Exists(destPath))
				File.Delete(destPath);
			File.Move(srcPath, destPath);
			return true;
		}

		public async Task<bool> DeleteFileAsync(string path, string dirPath)
		{
			//string path = CreatePathToFile(fileName, dirPath);
			if (File.Exists(path))
			{
				File.Delete(path);
				return true;
			}
			return false;
		}
		public bool DeleteFile(string path)
		{
			//string path = CreatePathToFile(fileName, dirPath);
			if (File.Exists(path))
			{
				File.Delete(path);
				return true;
			}
			return false;
		}
	}
}
