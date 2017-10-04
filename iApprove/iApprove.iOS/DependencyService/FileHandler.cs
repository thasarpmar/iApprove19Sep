using System;
using System.IO;
using System.Threading.Tasks;
using Foundation;
using iApprove.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(FileHandler))]
namespace iApprove.iOS
{
	public class FileHandler : IFileHandler
	{
		public static string DocumentsPath
		{
			get
			{
				var urlList = NSFileManager.DefaultManager.GetUrls(NSSearchPathDirectory.DocumentDirectory,
																		   NSSearchPathDomain.User);
				if (urlList.Length > 0)
				{
					var documentsDirUrl = urlList[urlList.Length - 1];
					return documentsDirUrl.Path;
				}
				return null;
			}
		}
		static string CreatePathToFile(string fileName, string dirPath)
		{
			if (!String.IsNullOrEmpty(dirPath))
			{
				return Path.Combine(dirPath, fileName);
			}
			return Path.Combine(DocumentsPath, fileName);
		}

		public async Task<string> ReadFileResAsync(string fileName)
		{
			var assembly = typeof(FileHandler).Assembly;
			//string resFileName = String.Format("{0}.{1}", assembly.GetName().Name, fileName);
			Stream stream = assembly.GetManifestResourceStream(fileName);
			//string text = "";
			using (StreamReader sr = new System.IO.StreamReader(stream))
			{
				return await sr.ReadToEndAsync();
			}
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
			string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

			string dbPath = Path.Combine(libFolder, fileName);

			if (!File.Exists(dbPath))
			{
				var existingDb = NSBundle.MainBundle.PathForResource(Path.GetFileName(dbPath),
																	 Path.GetExtension(dbPath));
				File.Copy(existingDb, dbPath);
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
			File.Move(srcPath, destPath);
			return true;
		}

		public async Task<bool> DeleteFileAsync(string fileName, string dir)
		{
			try
			{
				if (File.Exists(fileName))
				{
					File.Delete(fileName);
					return true;
				}
			}
			catch (Exception ex)
			{

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
