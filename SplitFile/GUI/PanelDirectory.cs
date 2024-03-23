using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
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
		private Panel PanelMain { get; set; }
		private bool IsFirst { get; set; }
		internal int IdPanel { get; private set; }
		private FlowLayoutPanel PanelPath { get; set; }
		internal List<ButtonDirectory> ListButtonDirectories { get; private set; } = new List<ButtonDirectory>();
		internal List<ButtonFile> ListButtonFiles { get; private set; } = new List<ButtonFile>();
		internal ButtonDirectory SelectedButtonDir { get; set; } = null;
		internal static ButtonFile SelectedButtonFile { get; set; } = null;

		internal PanelDirectory(
				Panel mainPanel, 
				FlowLayoutPanel panelPath, 
				int idPanel, 
				DirectoryInfo dir, 
				bool first = false
		) : base() {
			Dock = DockStyle.Left;
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
			PanelMain = mainPanel;
			IsFirst = first;
			IdPanel = idPanel;
			PanelPath = panelPath;

			Init();
		}

		private void Init() {
			ListPanels.Add(this);

			if (!IsFirst)
				AddDivider();

			if (Directory is null)
				LoadDrives();
			else
				LoadDirectoriesAndFiles();
		}

		internal void Remove() {
			PanelMain.Controls[$"DividerSplitDirectories{IdPanel}"].Dispose();
			Dispose();
		}

		private void LoadDirectoriesAndFiles()
		{
			int idButton = 0;
			bool first = true;

            foreach (DirectoryInfo dir in Directory.GetDirectories())
            {
				if (!dir.Attributes.HasFlag(FileAttributes.System)) {
					ButtonDirectory btn = new ButtonDirectory(PanelMain, PanelPath, dir.Name, IdPanel, idButton, dir, first);
					Controls.Add(btn);
					ListButtonDirectories.Add(btn);
					
					if (first)
						first = false;

					idButton++;
				}
            }

			foreach (FileInfo file in Directory.GetFiles())
			{
				if (!file.Attributes.HasFlag(FileAttributes.System)) {
					ButtonFile btn = new ButtonFile(PanelMain, PanelPath, file.Name, IdPanel, idButton, file, first);
					Controls.Add(btn);
					ListButtonFiles.Add(btn);

					if (first)
						first = false;

					idButton++;
				}
			}
		}

		private void LoadDrives() {
			bool first = true;
			int idButton = 0;

			foreach (DriveInfo drive in DriveInfo.GetDrives())
			{
				if (drive.IsReady)
				{
					string dirName = drive.Name;
					string nameDrive = $"{dirName} {drive.VolumeLabel}";
					DirectoryInfo dir = drive.RootDirectory;
					ButtonDirectory btn = new ButtonDirectory(PanelMain, PanelPath, nameDrive, IdPanel, idButton, dir, first);

					if (first)
						first = false;

					Controls.Add(btn);
					ListButtonDirectories.Add(btn);

					idButton++;
				}
			}
		}

		private void AddDivider() {
			MaterialDivider divider = new MaterialDivider()
			{
				Dock = DockStyle.Left,
				Margin = new Padding(0),
				Width = 2,
				Name = $"DividerSplitDirectories{IdPanel}"
			};

			PanelMain.Controls.Add(divider);
		}
	}
}
