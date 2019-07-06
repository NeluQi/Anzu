// (c) 2019 Nelu & 601 (github.com/NeluQi)
// This code is licensed under MIT license (see LICENSE for details)

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;

using Anzu;

using Ionic.Zip;

/// <summary>
/// Defines the <see cref="Backup" />
/// </summary>
internal class Backup {
	/// <summary>
	/// The BackupFolder
	/// </summary>
	/// <param name="Name">The Name<see cref="string"/></param>
	/// <param name="path">The path<see cref="string[]"/></param>
	/// <param name="DeleteFile">The DeleteFile<see cref="bool"/></param>
	public void BackupFolder(string Name, string[] path, bool DeleteFile = true) {
		MainWindow.BGThread = (new Thread(() => {
			var Progress = new ProgressController();
			Progress.ShowProgressBar();
			try {
				if(!Directory.Exists(Anzu.Properties.Settings.Default.MainBackupFolder)) {
					System.Windows.MessageBox.Show("Backup folder not found on disk, set new folder in settings", "Error",
					MessageBoxButton.OK, MessageBoxImage.Error);
					throw new Exception("Backup folder not found on disk, set new folder in settings");
				}

				string zipPath = Anzu.Properties.Settings.Default.MainBackupFolder + Name + " " + DateTime.Now.ToString("dd.MM.yyyy (hh-mm)") + ".zip";
				Progress.AddLog("Backup to " + zipPath);

				var pathExists = path.Where(x => Directory.Exists(x));
				if(pathExists.Count() < 1)
					throw new ArgumentNullException("all path not exists", nameof(pathExists));
				using(ZipFile zip = new ZipFile()) {
					zip.CompressionLevel = GetComLvl();
					zip.AlternateEncoding = Encoding.UTF8;
					zip.AlternateEncodingUsage = ZipOption.AsNecessary;

					zip.SaveProgress += (sender, e) => {
						switch(e.EventType) {
							case ZipProgressEventType.Saving_AfterRenameTempArchive:
								Progress.AddLog("Done");
								Progress.AddLog("Archive size (byte):" + new FileInfo(e.ArchiveName).Length.ToString());
								break;

							case ZipProgressEventType.Saving_BeforeWriteEntry:
								Progress.AddLog("Add:" + e.CurrentEntry.FileName);
								break;

							case ZipProgressEventType.Error_Saving:
								Progress.AddLog("Error " + e.CurrentEntry);
								DeleteFile = false;
								break;

							case ZipProgressEventType.Saving_AfterWriteEntry:
								Progress.SetMax(e.EntriesTotal);
								Progress.AddLog("Done:" + e.CurrentEntry.FileName);
								Progress.SetText(e.EntriesSaved + "/" + e.EntriesTotal);
								Progress.SetProgress(e.EntriesSaved);
								break;
						}
					};
					foreach(var p in pathExists)
						zip.AddDirectory(p, "");
					zip.Save(zipPath);
				}

				if(DeleteFile) {
					foreach(var p in pathExists) {
						var FileList = new DirectoryInfo(p).GetFiles();
						var DirectoryList = new DirectoryInfo(p).GetDirectories();

						Progress.AddLog("///Start delete file///");
						foreach(var current in FileList) {
							try {
								Progress.AddLog(current.Name);
								Progress.AddProgress(1);
								if(current.FullName != zipPath) {
									current.Delete();
								}
							}
							catch(Exception ex) {
								Progress.AddLog(ex.StackTrace);
							}
						}
						foreach(var current in DirectoryList) {
							try {
								Progress.AddLog(current.Name);
								Progress.AddProgress(1);
								current.Delete(true);
							}
							catch(Exception ex) {
								Progress.AddLog(ex.StackTrace);
							}
						}
					}
				}

				Progress.HideProgressBar();
			}
			catch(Exception ex) {
				Progress.AddLog(ex.StackTrace);
				Progress.HideProgressBar("!Error!");
			}
		}));

		MainWindow.BGThread.IsBackground = true;
		MainWindow.BGThread.Start();
	}

	/// <summary>
	/// The GetComLvl
	/// </summary>
	/// <returns>The <see cref="Ionic.Zlib.CompressionLevel"/></returns>
	private Ionic.Zlib.CompressionLevel GetComLvl() {
		switch(Anzu.Properties.Settings.Default.ComLvl) {
			case 0:
				return Ionic.Zlib.CompressionLevel.None;

			case 1:
				return Ionic.Zlib.CompressionLevel.Default;

			case 2:
				return Ionic.Zlib.CompressionLevel.Level1;

			case 3:
				return Ionic.Zlib.CompressionLevel.Level4;

			case 4:
				return Ionic.Zlib.CompressionLevel.BestSpeed;

			case 5:
				return Ionic.Zlib.CompressionLevel.BestCompression;

			default: return Ionic.Zlib.CompressionLevel.Default;
		}
	}
}
