using MaterialSkin.Controls;
using SplitFile.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SplitFile.GUI
{
	internal sealed class PanelDirectory : FlowLayoutPanel
	{
		internal DirectoryInfo Directory { get; }
		internal int IdPanel { get; }
		internal ButtonDirectory SelectedButtonDir { get; set; }
		internal List<ButtonDirectory> ListButtonDirs { get; } = new List<ButtonDirectory>();
		internal List<ButtonFile> ListButtonFiles { get; } = new List<ButtonFile>();

		private readonly FileManager _fileManager;
		private bool _inited = false;

		internal PanelDirectory(FileManager fileManager, int idPanel, DirectoryInfo dir) : base()
		{
			Dock = DockStyle.Right;
			FlowDirection = FlowDirection.TopDown;
			Margin = new Padding(0);
			AutoSize = true;
			WrapContents = false;
			Name = $"PanelSplitDirectory{idPanel}";
			HorizontalScroll.Maximum = 0;
			AutoScroll = false;
			VerticalScroll.Visible = true;
			AutoScroll = true;
			Directory = dir;
			IdPanel = idPanel;

			_fileManager = fileManager;

			Init();
		}

		private void Init()
		{
			if (_inited)
				return;

			Log.Debug<PanelDirectory>($"Initializing a directory panel ({Name})...");

			if (Directory == null)
				LoadDrives();
			else
			{
				AddDivider();
				LoadDirectoriesAndFiles();
			}

			_inited = true;
		}

		internal void Unselect()
		{
			Log.Debug<PanelDirectory>($"The selected directory button ({SelectedButtonDir.Name}) will soon turn off...");

			SelectedButtonDir.UseAccentColor = false;
			SelectedButtonDir.HighEmphasis = false;
			SelectedButtonDir = null;
		}

		internal void Remove()
		{
			Log.Debug<PanelDirectory>($"The directory panel ({Name}) is removing...");

			_fileManager.PanelMain.Controls[$"DividerSplitDirectories{IdPanel}"].Dispose();
			Dispose();
		}

		private void AddDivider()
		{
			var divider = new MaterialDivider()
			{
				Dock = DockStyle.Right,
				Margin = new Padding(0),
				Width = 2,
				Name = $"DividerSplitDirectories{IdPanel}"
			};

			Log.Debug<PanelDirectory>($"Adding divider ({divider.Name})...");
			_fileManager.PanelMain.Controls.Add(divider);
		}

		private void LoadDirectoriesAndFiles()
		{
			Log.Debug<PanelDirectory>($"Loading directories ({Name})...");

			int idButton = 0;

			foreach (DirectoryInfo dir in Directory.GetDirectories())
			{
				if (!dir.Attributes.HasFlag(FileAttributes.System) && AccessAccept(dir))
				{
					var btn = new ButtonDirectory(_fileManager, dir.Name, dir, idButton, IdPanel);

					Controls.Add(btn);
					ListButtonDirs.Add(btn);
					idButton++;
				}
			}

			Log.Debug<PanelDirectory>($"Loading files ({Name})...");

			idButton = 0;

			foreach (FileInfo file in Directory.GetFiles())
			{
				if (!file.Attributes.HasFlag(FileAttributes.System) && AccessAccept(file))
				{
					var btn = new ButtonFile(_fileManager, file, IdPanel, idButton);

					Controls.Add(btn);
					ListButtonFiles.Add(btn);
					idButton++;
				}
			}

			bool AccessAccept(FileSystemInfo sys)
			{
				try
				{
					if (sys is DirectoryInfo dir)
						dir.GetAccessControl();
					else if (sys is FileInfo file)
						file.GetAccessControl();

					return true;
				}
				catch (UnauthorizedAccessException)
				{
					return false;
				}
			}
		}

		private void LoadDrives()
		{
			Log.Debug<PanelDirectory>("Loading drives...");

			int idButton = 0;

			foreach (DriveInfo drive in DriveInfo.GetDrives())
			{
				if (drive.IsReady)
				{
					string dirName = drive.Name;
					string nameDrive = $"{dirName} {drive.VolumeLabel}".Replace("\\", string.Empty);
					DirectoryInfo dir = drive.RootDirectory;
					var btn = new ButtonDirectory(_fileManager, nameDrive, dir, idButton, IdPanel);
					
					Controls.Add(btn);
					ListButtonDirs.Add(btn);

					idButton++;
				}
			}
		}
	}
}
