// (c) 2019 Nelu & 601 (github.com/NeluQi)
// This code is licensed under MIT license (see LICENSE for details)

using System.Windows;

using Anzu;

/// <summary>
/// Defines the <see cref="WindowController" />
/// </summary>
internal class WindowController {
	/// <summary>
	/// Defines the ActiveWindow
	/// </summary>
	private static Windows ActiveWindow = Windows.NULL;

	/// <summary>
	/// Defines the Form
	/// </summary>
	private MainWindow Form = Application.Current.Windows[0] as MainWindow;

	/// <summary>
	/// Initializes a new instance of the <see cref="WindowController"/> class.
	/// </summary>
	/// <param name="SelectWindow">The SelectWindow<see cref="Windows"/></param>
	public WindowController(Windows SelectWindow) {
		if(SelectWindow == ActiveWindow)
			return;
		Hide(ActiveWindow);
		Show(SelectWindow);
		ActiveWindow = SelectWindow;
	}

	/// <summary>
	/// Defines the Windows
	/// </summary>
	public enum Windows {
		/// <summary>
		/// Defines the Desktop
		/// </summary>
		Desktop,

		/// <summary>
		/// Defines the Download
		/// </summary>
		Download,

		/// <summary>
		/// Defines the Folder
		/// </summary>
		Folder,

		/// <summary>
		/// Defines the File
		/// </summary>
		File,

		/// <summary>
		/// Defines the Settings
		/// </summary>
		Settings,

		/// <summary>
		/// Defines the NULL
		/// </summary>
		NULL
	}

	/// <summary>
	/// The Hide
	/// </summary>
	/// <param name="win">The win<see cref="Windows"/></param>
	private void Hide(Windows win) {
		switch(win) {
			case Windows.Desktop:
				Form.DesktopGrid.Visibility = Visibility.Collapsed;
				Form.btnDesktop.IsEnabled = true;
				break;

			case Windows.Download:
				Form.DownloadGrid.Visibility = Visibility.Collapsed;
				Form.btnDownload.IsEnabled = true;
				break;

			case Windows.File:
				//
				break;

			case Windows.Folder:
				Form.FolderGrid.Visibility = Visibility.Collapsed;
				Form.btnFolder.IsEnabled = true;
				break;

			case Windows.Settings:
				Form.SettingGrid.Visibility = Visibility.Collapsed;
				Form.btnSetting.IsEnabled = true;
				break;

			case Windows.NULL:
				Form.DesktopGrid.Visibility = Visibility.Collapsed;
				Form.btnDesktop.IsEnabled = true;
				Form.DownloadGrid.Visibility = Visibility.Collapsed;
				Form.btnDownload.IsEnabled = true;
				Form.FolderGrid.Visibility = Visibility.Collapsed;
				Form.btnFolder.IsEnabled = true;
				Form.SettingGrid.Visibility = Visibility.Collapsed;
				Form.btnSetting.IsEnabled = true;
				break;
		}
	}

	/// <summary>
	/// The Show
	/// </summary>
	/// <param name="win">The win<see cref="Windows"/></param>
	private void Show(Windows win) {
		switch(win) {
			case Windows.Desktop:
				Form.DesktopGrid.Visibility = Visibility.Visible;
				Form.btnDesktop.IsEnabled = false;
				break;

			case Windows.Download:
				Form.DownloadGrid.Visibility = Visibility.Visible;
				Form.btnDownload.IsEnabled = false;
				break;

			case Windows.File:
				//
				break;

			case Windows.Folder:
				Form.FolderGrid.Visibility = Visibility.Visible;
				Form.btnFolder.IsEnabled = false;
				break;

			case Windows.Settings:
				Form.SettingGrid.Visibility = Visibility.Visible;
				Form.btnSetting.IsEnabled = false;
				break;
		}
	}
}
