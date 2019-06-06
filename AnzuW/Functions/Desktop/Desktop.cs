using AnzuW;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
///����� ��� ������� Desktop
/// </summary>
internal class Desktop
{
	public void Backup() //������� ������
	{
		//���� �������� ������, ����� ������� ����� ����� ��� �������� ����

		//����� �����
		MainWindow.BGThread = (new Thread(() =>
		{
			////////////////////���� ������////////////////////////
			///

			//����������� ������� ���� � UI
			//������� ���������� ������� ����
			var Progress = new ProgressController();

			Progress.ShowProgressBar(); //�������� ���

			// try catch ����� ��� ��������� ������ ������� STOP �� UI
			// ���� ���� ������ STOP �� ���������� ����� ������� � catch (��� �� ���� ��������� ����������)
			try
			{
				//��������
				DirectoryInfo dir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
				var FileList = dir.GetFiles();

				Progress.SetMax(FileList.Length + 1); //������������� ��� ���� ������������ ����� (�� ��������� �� 0 �� 100, �� �� ������� �� 0 �� ��-�� ������)
				Progress.SetText("0/" + FileList.Length); //��������� ������ ��� ������� �����

				for (int i = 0; i < FileList.Length; i++) //������ �� ������ ������
				{
					FileInfo temp = (FileInfo)FileList[i];

					temp.CopyTo(AnzuW.Properties.Settings.Default.MainBackupFolder + "\\" + temp.Name); //�����������

					//File.SetAttributes(temp.FullName.ToString(), FileAttributes.Normal);
					//File.Delete(temp.FullName.ToString());

					Progress.Inc(); //����������� �������� ��� �� 1
					Progress.SetText(i + "/" + FileList.Length); //������ ����� ����� ��� �����
				}

				Progress.HideProgressBar(); //�������� ���
			}
			catch (Exception ex)
			{
				Progress.HideProgressBar(); //������� ���
			}
		}));

		MainWindow.BGThread.IsBackground = true; //����������� ������������� ��� ������
		MainWindow.BGThread.Start(); //������ ������
	}
}