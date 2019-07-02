#region copyright

// (c) 2019 Nelu & 601 (github.com/NeluQi)
// This code is licensed under MIT license (see LICENSE for details)

#endregion copyright

using Anzu;
using System;
using System.Windows;

/// <summary>
///Controller for progress bar
/// </summary>
public class ProgressController
{
	/// <summary>
	/// Main window
	/// </summary>
	public static MainWindow MainWindow { get; set; }

	/// <summary>
	/// Add line to log panel
	/// </summary>
	/// <param name="log">String</param>
	public void AddLog(string log)
	{
		Console.WriteLine(log);
		MainWindow.Dispatcher.Invoke(new Action(() =>
		{
			MainWindow.OutputBlock.Text += log + Environment.NewLine;
			MainWindow.OutputBlockScroll.ScrollToEnd();
		}));
	}

	/// <summary>
	/// Add value progress bar
	/// </summary>
	/// <param name="progress"></param>
	public void AddProgress(int progress)
	{
		MainWindow.Dispatcher.Invoke(new Action(() =>
		{
			MainWindow.ProgressBar.Value += progress;
		}));
	}

	/// <summary>
	/// Hide progress bar in UI and show done windows and custom text
	/// </summary>
	/// <param name="text">Text message</param>
	public void HideProgressBar(string text = "Successfully")
	{
		MainWindow.Dispatcher.Invoke(new Action(() =>
		{
			MainWindow.ProgressStopbtn.IsEnabled = false;
			MainWindow.DoneProgress.Visibility = Visibility.Visible;
			MainWindow.TextDoneProgress.Content = text;
			MainWindow.ProgressBar.Value = MainWindow.ProgressBar.Maximum;
		}));
	}

	/// <summary>
	/// Increase bar progress by 1 value
	/// </summary>
	public void Inc()
	{
		MainWindow.Dispatcher.Invoke(new Action(() =>
		{
			MainWindow.ProgressBar.Value++;
		}));
	}

	/// <summary>
	/// Set maximum for progress bar
	/// </summary>
	/// <param name="MAX">MAX</param>
	public void SetMax(int MAX)
	{
		MainWindow.Dispatcher.Invoke(new Action(() =>
		{
			MainWindow.ProgressBar.Maximum = MAX;
		}));
	}

	/// <summary>
	/// Set minimum for progress bar
	/// </summary>
	/// <param name="MIN">MIN</param>
	public void SetMin(int MIN)
	{
		MainWindow.Dispatcher.Invoke(new Action(() =>
		{
			MainWindow.ProgressBar.Minimum = MIN;
		}));
	}

	/// <summary>
	/// Set value progress bar
	/// </summary>
	/// <param name="progress">value</param>
	public void SetProgress(int progress)
	{
		MainWindow.Dispatcher.Invoke(new Action(() =>
		{
			MainWindow.ProgressBar.Value = progress;
		}));
	}

	/// <summary>
	/// Set text above progress bar
	/// </summary>
	/// <param name="text">text</param>
	public void SetText(string text)
	{
		MainWindow.Dispatcher.Invoke(new Action(() =>
		{
			MainWindow.ProgressText.Content = text;
		}));
	}

	/// <summary>
	/// Show progress bar in UI
	/// </summary>
	public void ShowProgressBar()
	{
		MainWindow.Dispatcher.Invoke(new Action(() =>
		{
			MainWindow.DoneProgress.Visibility = Visibility.Collapsed;
			MainWindow.ProgressBar.Maximum = 100;
			MainWindow.ProgressBar.Minimum = 0;
			MainWindow.ProgressBar.Value = 0;
			MainWindow.ProgressText.Content = "";
			MainWindow.ProgressStopbtn.IsEnabled = true;
			MainWindow.OutputBlock.Text = "";
			MainWindow.ProgressPanel.Visibility = Visibility.Visible;
		}));
	}
}