using MaterialSkin.Controls;
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
	internal class PanelDirectory : FlowLayoutPanel
	{
		internal static List<PanelDirectory> ListPanels { get; private set; } = new List<PanelDirectory>();
		internal DirectoryInfo Directory { get; private set; }
		internal int IdPanel { get; private set; }
		internal List<ButtonDirectory> ListButtonDirectories { get; private set; } = new List<ButtonDirectory>();
		internal List<ButtonFile> ListButtonFiles { get; private set; } = new List<ButtonFile>();
		internal ButtonDirectory SelectedButtonDir { get; set; } = null;
		internal static ButtonFile SelectedButtonFile { get; set; } = null;

		internal PanelDirectory(
				int idPanel, 
				DirectoryInfo dir
		) : base() {
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

			Init();
		}

		private void Init() {
			ListPanels.Add(this);

			if (Directory is null)
				LoadDrives();
			else {
				AddDivider();
				LoadDirectoriesAndFiles();
			}

			AddButtonPath();
		}

		private void AddButtonPath()
		{
			ButtonPath btn = new ButtonPath(
					Directory is null 
						? "Начало" 
						: Directory.Name,
					IdPanel,
					Directory
			);

			FormMain.PanelPathSplit.Controls.Add(btn);
		}

		internal void Remove() {
			FormMain.PanelMainSplit.Controls[$"DividerSplitDirectories{IdPanel}"].Dispose();
			Dispose();

			foreach (ButtonFile btn in ListButtonFiles)
				if (btn == SelectedButtonFile)
					FormMain.ButtonFileSplit.Enabled = false;
		}

		private void LoadDirectoriesAndFiles()
		{
			int idButton = 0;

            foreach (DirectoryInfo dir in Directory.GetDirectories())
            {
				if (!dir.Attributes.HasFlag(FileAttributes.System)) {
					if (AccessAccept(dir)) {
						ButtonDirectory btn = new ButtonDirectory(dir.Name, IdPanel, idButton, dir);
						Controls.Add(btn);
						ListButtonDirectories.Add(btn);

						idButton++;
					}
				}
            }

			foreach (FileInfo file in Directory.GetFiles())
			{
				if (!file.Attributes.HasFlag(FileAttributes.System)) {
					if (AccessAccept(file)) {
						ButtonFile btn = new ButtonFile(file.Name, IdPanel, idButton, file);
						Controls.Add(btn);
						ListButtonFiles.Add(btn);

						idButton++;
					}
				}
			}

			bool AccessAccept(FileSystemInfo sys) {
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

		private void LoadDrives() {
			int idButton = 0;

			foreach (DriveInfo drive in DriveInfo.GetDrives())
			{
				if (drive.IsReady)
				{
					string dirName = drive.Name;
					string nameDrive = $"{dirName} {drive.VolumeLabel}";
					DirectoryInfo dir = drive.RootDirectory;
					ButtonDirectory btn = new ButtonDirectory(nameDrive, IdPanel, idButton, dir);

					Controls.Add(btn);
					ListButtonDirectories.Add(btn);

					idButton++;
				}
			}
		}

		private void AddDivider() {
			MaterialDivider divider = new MaterialDivider()
			{
				Dock = DockStyle.Right,
				Margin = new Padding(0),
				Width = 2,
				Name = $"DividerSplitDirectories{IdPanel}"
			};

			FormMain.PanelMainSplit.Controls.Add(divider);
		}
	}
}
