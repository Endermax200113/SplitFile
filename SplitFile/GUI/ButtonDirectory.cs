using MaterialSkin.Controls;
using SplitFile.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SplitFile.GUI
{
	internal class ButtonDirectory : MaterialButton
	{
		internal DirectoryInfo Directory { get; private set; }
		internal int IdPanel { get; private set; }

		internal ButtonDirectory(
				string text, 
				int idPanel, 
				int idButton, 
				DirectoryInfo dir
		) : base() {
			Anchor = AnchorStyles.Left | AnchorStyles.Right;
			Text = text;
			Margin = idButton == 0 ? new Padding(4, 6, 4, 3) : new Padding(4, 3, 4, 3);
			HighEmphasis = false;
			Icon = Properties.Resources.folder;
			Name = $"ButtonSplit{idButton}OfPanel{idPanel}";
			DoubleBuffered = true;
			Directory = dir;
			IdPanel = idPanel;

			Init();
		}

		private void Init() {
			IEnumerable<FileSystemInfo> files = Directory.EnumerateFileSystemInfos();
			int count = CheckSystemFile(files);

			if (count != 0)
				Click += AddClick;
			else
				Enabled = false;
		}

		private int CheckSystemFile(IEnumerable<FileSystemInfo> files) {
			int count = files.Count();

            foreach (FileSystemInfo sysFile in files)
            	if (sysFile.Attributes.HasFlag(FileAttributes.System))
					count--;
            
            return count;
		}

		private void AddClick(object sender, EventArgs e)
		{
			try
			{
				if (!ButtonException.CheckError<ButtonDirectory>(sender, e))
				{
					if (!UseAccentColor) 
					{
						if (IdPanel + 1 < FormMain.IdPanel)
						{
							int count = PanelDirectory.ListPanels.Count - 1;
							List<PanelDirectory> listPanels = PanelDirectory.ListPanels;
							List<ButtonPath> listButtons = ButtonPath.ListButtons;

							while (IdPanel < count)
							{
								PanelDirectory panel = listPanels[count];
								panel.Remove();
								listPanels.RemoveAt(count);

								ButtonPath btnPath = listButtons[count];
								btnPath.Dispose();
								listButtons.RemoveAt(count);

								count--;
							}

							FormMain.IdPanel = IdPanel + 1;
						}

						PanelDirectory newPanel = new PanelDirectory(FormMain.IdPanel, Directory);
						FormMain.PanelMainSplit.Controls.Add(newPanel);
						FormMain.IdPanel++;

						PanelDirectory panelCurrent = PanelDirectory.ListPanels[IdPanel];
						ButtonDirectory btn = panelCurrent.SelectedButtonDir;

						if (btn != null)
						{
							btn.UseAccentColor = false;
							btn.HighEmphasis = false;
						}

						panelCurrent.SelectedButtonDir = (ButtonDirectory)sender;
						UseAccentColor = true;
						HighEmphasis = true;
					}
				}
			}
			catch (ButtonException err)
			{
				ButtonException.SendMessage(
						err, 
						nameof(ButtonDirectory), 
						nameof(AddClick), 
						((MaterialButton)sender).Name
				);
			}
		}
	}
}
