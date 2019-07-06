// (c) 2019 Nelu & 601 (github.com/NeluQi)
// This code is licensed under MIT license (see LICENSE for details)

namespace Anzu {
	using System;
	using System.Diagnostics;
	using System.Threading;
	using System.Windows;
	using System.Windows.Input;

	using WinForms = System.Windows.Forms;

	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		/// <summary>
		/// BG Thread
		/// </summary>
		public static Thread BGThread;

		/// <summary>
		/// Initializes a new instance of the <see cref="MainWindow"/> class.
		/// </summary>
		public MainWindow() {
			if(Environment.GetCommandLineArgs().Length > 1) {
				var CommandLinePasre = new ControllerCommand(Environment.GetCommandLineArgs());
				System.Environment.Exit(0);
			}
			else {
				InitializeComponent();
				MainBackupFolderTextBox.Text = Properties.Settings.Default.MainBackupFolder;
				ProgressPanel.Visibility = Visibility.Collapsed;
				ProgressController.MainWindow = this;
				new WindowController(WindowController.Windows.Desktop);

				OldDayTextBox.PreviewTextInput += new TextCompositionEventHandler(OnlyNum);
				FOldDayTextBox.PreviewTextInput += new TextCompositionEventHandler(OnlyNum);

				switch(Properties.Settings.Default.ComLvl) {
					case 0:
						CompressionNope.IsChecked = true;
						break;

					case 1:
						CompressionDefault.IsChecked = true;
						break;

					case 2:
						CompressionLevel1.IsChecked = true;
						break;

					case 3:
						CompressionLevel4.IsChecked = true;
						break;

					case 4:
						CompressionBestSpeed.IsChecked = true;
						break;

					case 5:
						CompressionBestCompression.IsChecked = true;
						break;
				}
			}
		}

		/// <summary>
		/// Button in the left side menu to enable the tab desktop
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_Desktop(object sender, RoutedEventArgs e) {
			new WindowController(WindowController.Windows.Desktop);
		}

		/// <summary>
		/// The OnlyNum
		/// </summary>
		/// <param name="sender">The sender<see cref="object"/></param>
		/// <param name="e">The e<see cref="TextCompositionEventArgs"/></param>
		private void OnlyNum(object sender, TextCompositionEventArgs e) {
			if(!char.IsDigit(e.Text, e.Text.Length - 1))
				e.Handled = true;
		}

		/// <summary>
		/// Button in Desktop (Desktop Backup)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_DesktopBackup(object sender, RoutedEventArgs e) {
			if(String.IsNullOrWhiteSpace(Properties.Settings.Default.MainBackupFolder)) {
				MessageBox.Show("You need to install the main backup folder in the settings", "Error",
				MessageBoxButton.OK, MessageBoxImage.Error);
			}
			else {
				var bk = new Backup();
				bk.BackupFolder("Desktop", new string[] { Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory).ToString(), Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory) }, DesktopDelFileCheck.IsChecked.Value);
			}
		}

		/// <summary>
		/// Button in the left side menu to enable the tab download
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_Download(object sender, RoutedEventArgs e) {
			new WindowController(WindowController.Windows.Download);
		}

		/// <summary>
		/// btn in download page (Download Sort)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_DownloadSort(object sender, RoutedEventArgs e) {
			if(String.IsNullOrWhiteSpace(Properties.Settings.Default.MainBackupFolder)) {
				MessageBox.Show("You need to install the main backup folder in the settings", "Error",
				MessageBoxButton.OK, MessageBoxImage.Error);
			}
			else {
				var bk = new Sort();
				bk.SortFolder(DelFiles.IsChecked.Value, SortExtended.IsChecked.Value, KnownFolders.GetPath(KnownFolder.Downloads));
			}
		}

		/// <summary>
		/// Button in the left side menu to enable the tab folder
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_Folder(object sender, RoutedEventArgs e) {
			new WindowController(WindowController.Windows.Folder);
		}

		/// <summary>
		/// The Button_Click_HideProgress
		/// </summary>
		/// <param name="sender">The sender<see cref="object"/></param>
		/// <param name="e">The e<see cref="RoutedEventArgs"/></param>
		private void Button_Click_HideProgress(object sender, RoutedEventArgs e) {
			ProgressPanel.Visibility = Visibility.Collapsed;
			DoneProgress.Visibility = Visibility.Collapsed;
		}

		/// <summary>
		/// Button in settings (Select folder)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SelectMainBackupFolder(object sender, RoutedEventArgs e) {
			using(WinForms.FolderBrowserDialog dlg = new WinForms.FolderBrowserDialog()) {
				if(dlg.ShowDialog() == WinForms.DialogResult.OK) {
					Properties.Settings.Default.MainBackupFolder = dlg.SelectedPath + @"\";
					MainBackupFolderTextBox.Text = Properties.Settings.Default.MainBackupFolder;
					Properties.Settings.Default.Save();
				}
			}
		}

		/// <summary>
		/// Button in the left side menu to enable the tab setting
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_Setting(object sender, RoutedEventArgs e) {
			new WindowController(WindowController.Windows.Settings);
		}

		/// <summary>
		/// Button in progress bar (STOP)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_StopOtherThread(object sender, RoutedEventArgs e) {
			ProgressText.Content = "Wait for closing....";
			ProgressStopbtn.IsEnabled = false;
			BGThread.Abort();
		}

		/// <summary>
		/// Exit button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Exit(object sender, RoutedEventArgs e) {
			System.Windows.Application.Current.Shutdown();
		}

		/// <summary>
		/// The Minimized
		/// </summary>
		/// <param name="sender">The sender<see cref="object"/></param>
		/// <param name="e">The e<see cref="RoutedEventArgs"/></param>
		private void Minimized(object sender, RoutedEventArgs e) {
			this.WindowState = WindowState.Minimized;
		}

		/// <summary>
		/// Drag and drop for program header
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
			this.DragMove();
		}

		/// <summary>
		/// Desktop Sort btn
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_DesktopSort(object sender, RoutedEventArgs e) {
			var bk = new Sort();
			bk.SortFolder(DSortExtension.IsChecked.Value, false, Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
			bk.SortFolder(DSortExtension.IsChecked.Value, false, Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory));
		}

		/// <summary>
		/// Download delete old files btn
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_DownloadDelOld(object sender, RoutedEventArgs e) {
			var dk = new DeleteOldFiles();
			int day = -1;
			try {
				day = Convert.ToInt32(OldDayTextBox.Text);
				if(day <= 0 || day >= 1000) {
					throw new Exception("Not valid days");
				}
				dk.Delete(day, KnownFolders.GetPath(KnownFolder.Downloads));
			}
			catch(Exception) {
				MessageBox.Show("Not valid days", "Error",
				MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		/// <summary>
		/// Sort Folder
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SortFolder(object sender, RoutedEventArgs e) {
			using(WinForms.FolderBrowserDialog dlg = new WinForms.FolderBrowserDialog()) {
				string path = null;
				if(dlg.ShowDialog() == WinForms.DialogResult.OK) {
					path = dlg.SelectedPath + @"\";

					var bk = new Sort();
					bk.SortFolder(true, SortExtendedFolder.IsChecked.Value, path);
				}
			}
		}

		/// <summary>
		/// The Button_Click_DelOldFolder
		/// </summary>
		/// <param name="sender">The sender<see cref="object"/></param>
		/// <param name="e">The e<see cref="RoutedEventArgs"/></param>
		private void Button_Click_DelOldFolder(object sender, RoutedEventArgs e) {
			using(WinForms.FolderBrowserDialog dlg = new WinForms.FolderBrowserDialog()) {
				string path = null;
				if(dlg.ShowDialog() == WinForms.DialogResult.OK) {
					path = dlg.SelectedPath + @"\";

					var sk = new DeleteOldFiles();
					int day = -1;
					try {
						day = Convert.ToInt32(FOldDayTextBox.Text);
						if(day <= 0 || day >= 1000) {
							throw new Exception("Not valid days");
						}
						sk.Delete(day, path);
					}
					catch(Exception) {
						MessageBox.Show("Not valid days", "Error",
						MessageBoxButton.OK, MessageBoxImage.Error);
					}
				}
			}
		}

		/// <summary>
		/// The Border_DragEnter
		/// </summary>
		/// <param name="sender">The sender<see cref="object"/></param>
		/// <param name="e">The e<see cref="DragEventArgs"/></param>
		private void Border_DragEnter(object sender, DragEventArgs e) {
			DropSortFiles.Visibility = Visibility.Visible;
		}

		/// <summary>
		/// The Border_DragLeave
		/// </summary>
		/// <param name="sender">The sender<see cref="object"/></param>
		/// <param name="e">The e<see cref="DragEventArgs"/></param>
		private void Border_DragLeave(object sender, DragEventArgs e) {
			DropSortFiles.Visibility = Visibility.Collapsed;
		}

		/// <summary>
		/// The Border_DragEnter_1
		/// </summary>
		/// <param name="sender">The sender<see cref="object"/></param>
		/// <param name="e">The e<see cref="DragEventArgs"/></param>
		private void Border_DragEnter_1(object sender, DragEventArgs e) {
			DropOldFiles.Visibility = Visibility.Visible;
		}

		/// <summary>
		/// The Border_DragLeave_1
		/// </summary>
		/// <param name="sender">The sender<see cref="object"/></param>
		/// <param name="e">The e<see cref="DragEventArgs"/></param>
		private void Border_DragLeave_1(object sender, DragEventArgs e) {
			DropOldFiles.Visibility = Visibility.Collapsed;
		}

		/// <summary>
		/// Drag and drop in Sort folder
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Border_Drop(object sender, DragEventArgs e) {
			string[] Args = (string[])e.Data.GetData(DataFormats.FileDrop, true);
			DropSortFiles.Visibility = Visibility.Collapsed;
			foreach(var path in Args) {
				if(System.IO.Directory.Exists(path)) {
					var bk = new Sort();
					bk.SortFolder(true, SortExtendedFolder.IsChecked.Value, path);
					break;
				}
			}
		}

		/// <summary>
		/// Drag and drop in old file folder
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Border_Drop_1(object sender, DragEventArgs e) {
			string[] Args = (string[])e.Data.GetData(DataFormats.FileDrop, true);
			DropOldFiles.Visibility = Visibility.Collapsed;
			foreach(var path in Args) {
				if(System.IO.Directory.Exists(path)) {
					var sk = new DeleteOldFiles();
					int day = -1;
					try {
						day = Convert.ToInt32(FOldDayTextBox.Text);
						if(day <= 0 || day >= 1000) {
							throw new Exception("Not valid days");
						}
						sk.Delete(day, path);
					}
					catch(Exception) {
						MessageBox.Show("Not valid days", "Error",
						MessageBoxButton.OK, MessageBoxImage.Error);
					}
				}
			}
		}

		/// <summary>
		/// The CompressionNope_Checked
		/// </summary>
		/// <param name="sender">The sender<see cref="object"/></param>
		/// <param name="e">The e<see cref="RoutedEventArgs"/></param>
		private void CompressionNope_Checked(object sender, RoutedEventArgs e) {
			CompressionDefault.IsChecked = false;
			CompressionLevel1.IsChecked = false;
			CompressionLevel4.IsChecked = false;
			CompressionBestSpeed.IsChecked = false;
			CompressionBestCompression.IsChecked = false;
			Properties.Settings.Default.ComLvl = 0;
			Properties.Settings.Default.Save();
		}

		/// <summary>
		/// The CompressionDefault_Checked
		/// </summary>
		/// <param name="sender">The sender<see cref="object"/></param>
		/// <param name="e">The e<see cref="RoutedEventArgs"/></param>
		private void CompressionDefault_Checked(object sender, RoutedEventArgs e) {
			CompressionNope.IsChecked = false;
			CompressionLevel1.IsChecked = false;
			CompressionLevel4.IsChecked = false;
			CompressionBestSpeed.IsChecked = false;
			CompressionBestCompression.IsChecked = false;
			Properties.Settings.Default.ComLvl = 1;
			Properties.Settings.Default.Save();
		}

		/// <summary>
		/// The CompressionLevel1_Checked
		/// </summary>
		/// <param name="sender">The sender<see cref="object"/></param>
		/// <param name="e">The e<see cref="RoutedEventArgs"/></param>
		private void CompressionLevel1_Checked(object sender, RoutedEventArgs e) {
			CompressionNope.IsChecked = false;
			CompressionDefault.IsChecked = false;
			CompressionLevel4.IsChecked = false;
			CompressionBestSpeed.IsChecked = false;
			CompressionBestCompression.IsChecked = false;
			Properties.Settings.Default.ComLvl = 2;
			Properties.Settings.Default.Save();
		}

		/// <summary>
		/// The CompressionLevel4_Checked
		/// </summary>
		/// <param name="sender">The sender<see cref="object"/></param>
		/// <param name="e">The e<see cref="RoutedEventArgs"/></param>
		private void CompressionLevel4_Checked(object sender, RoutedEventArgs e) {
			CompressionNope.IsChecked = false;
			CompressionDefault.IsChecked = false;
			CompressionLevel1.IsChecked = false;
			CompressionBestSpeed.IsChecked = false;
			CompressionBestCompression.IsChecked = false;
			Properties.Settings.Default.ComLvl = 3;
			Properties.Settings.Default.Save(); ;
		}

		/// <summary>
		/// The CompressionBestSpeed_Checked
		/// </summary>
		/// <param name="sender">The sender<see cref="object"/></param>
		/// <param name="e">The e<see cref="RoutedEventArgs"/></param>
		private void CompressionBestSpeed_Checked(object sender, RoutedEventArgs e) {
			CompressionNope.IsChecked = false;
			CompressionDefault.IsChecked = false;
			CompressionLevel1.IsChecked = false;
			CompressionLevel4.IsChecked = false;
			CompressionBestCompression.IsChecked = false;
			Properties.Settings.Default.ComLvl = 4;
			Properties.Settings.Default.Save();
		}

		/// <summary>
		/// The CompressionBestCompression_Checked
		/// </summary>
		/// <param name="sender">The sender<see cref="object"/></param>
		/// <param name="e">The e<see cref="RoutedEventArgs"/></param>
		private void CompressionBestCompression_Checked(object sender, RoutedEventArgs e) {
			CompressionNope.IsChecked = false;
			CompressionDefault.IsChecked = false;
			CompressionLevel1.IsChecked = false;
			CompressionLevel4.IsChecked = false;
			CompressionBestSpeed.IsChecked = false;
			Properties.Settings.Default.ComLvl = 5;
			Properties.Settings.Default.Save();
		}

		/// <summary>
		/// The Hyperlink_Click
		/// </summary>
		/// <param name="sender">The sender<see cref="object"/></param>
		/// <param name="e">The e<see cref="RoutedEventArgs"/></param>
		private void Hyperlink_Click(object sender, RoutedEventArgs e) {
			Process.Start("https://github.com/NeluQi");
		}
	}
}
