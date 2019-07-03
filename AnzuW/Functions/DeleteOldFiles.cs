using Anzu;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

internal class DeleteOldFiles
{
	public void Delete(int days, string path)
	{
		MainWindow.BGThread = (new Thread(() =>
		{
			var Progress = new ProgressController();
			Progress.ShowProgressBar();

			try
			{
				if (!Directory.Exists(path))
					throw new ArgumentNullException("path not exists", nameof(path));

				var dir = new DirectoryInfo(path);
				var FileList = dir.GetFiles();
				int iCnt = 0;

				foreach (var file in FileList)
				{
					file.Refresh();
					try
					{
						if (file.LastAccessTime <= DateTime.Now.AddDays(-days))
						{
							Progress.AddLog("Deleting:" + file.Name);
							file.Delete();
							iCnt += 1;
						}
					}
					catch (Exception)
					{
						Progress.AddLog("Error");
					}

					Progress.AddProgress(1);
				}

				Progress.HideProgressBar();
			}
			catch (Exception ex)
			{
				Progress.AddLog(ex.StackTrace);
				Progress.HideProgressBar("!Error!");
			}
		}));

		MainWindow.BGThread.IsBackground = true;
		MainWindow.BGThread.Start();
	}
}