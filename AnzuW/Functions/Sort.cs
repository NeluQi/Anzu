﻿// (c) 2019 Nelu & 601 (github.com/NeluQi)
// This code is licensed under MIT license (see LICENSE for details)

using System;
using System.IO;
using System.Threading;

using Anzu;

/// <summary>
/// Defines the <see cref="Sort" />
/// </summary>
internal class Sort {
	/// <summary>
	/// The SortFolder
	/// </summary>
	/// <param name="DelFile">The DelFile<see cref="bool"/></param>
	/// <param name="SortExtension">The SortExtension<see cref="bool"/></param>
	/// <param name="path">The path<see cref="string"/></param>
	public void SortFolder(bool DelFile, bool SortExtension, string path) {
		MainWindow.BGThread = (new Thread(() => {
			var Progress = new ProgressController();
			Progress.ShowProgressBar();

			try {
				if(!Directory.Exists(path))
					throw new ArgumentNullException("path not exists", nameof(path));

				var dir = new DirectoryInfo(path);
				var FileList = dir.GetFiles();
				Progress.SetMax(DelFile == true ? FileList.Length * 2 : FileList.Length);
				path += $"/Sort ({DateTime.Now.ToString("dd.MM.yyyy")})/";

				if(!SortExtension) {
					foreach(var t in FileList) {
						try {
							Progress.AddLog("Sort:" + t.Name);

							Directory.CreateDirectory(path + TypeFiles.GetTypePath(t));
							t.CopyTo(path + TypeFiles.GetTypePath(t) + t.Name, true);

							Progress.AddProgress(1);
						}
						catch(Exception ex) {
							Progress.AddLog("Error:" + t.Name);
							Progress.AddLog(ex.StackTrace.ToString());
							Progress.AddProgress(1);
						}
					}
				}
				else {
					foreach(var t in FileList) {
						try {
							Progress.AddLog("Sort:" + t.Name);
							Directory.CreateDirectory(path + t.Extension.ToString().Replace(".", ""));
							t.CopyTo(path + t.Extension.ToString().Replace(".", "") + "/" + t.Name, true);
							Progress.AddProgress(1);
						}
						catch(Exception ex) {
							Progress.AddLog("Error:" + t.Name);
							Progress.AddLog(ex.StackTrace.ToString());
							Progress.AddProgress(1);
						}
					}
				}

				if(DelFile) {
					foreach(FileInfo file in dir.GetFiles()) {
						try {
							file.Delete();
						}
						catch(Exception ex) {
							Progress.AddLog("Error:" + file.Name);
							Progress.AddLog(ex.StackTrace.ToString());
						}
						finally {
							Progress.AddProgress(1);
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
}
