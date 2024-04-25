using MaterialSkin.Controls;
using SplitFile.Exceptions;
using SplitFile.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SplitFile.GUI
{
	internal sealed class ButtonDirectory : MaterialButton
	{
		internal DirectoryInfo Directory { get; }
		internal int IdButton { get; }
		internal int OfIdPanel { get; }

		private bool _inited = false;
		private readonly FileManager _fileManager;

		internal ButtonDirectory(FileManager fileManager, string text, DirectoryInfo dir, int idButton, int ofIdPanel) : base()
		{
			Anchor = AnchorStyles.Left | AnchorStyles.Right;
			Text = text;
			Margin = idButton == 0 ? new Padding(4, 6, 4, 3) : new Padding(4, 3, 4, 3);
			HighEmphasis = false;
			Icon = Properties.Resources.folder;
			Name = $"ButtonSplitDir{idButton}OfPanel{ofIdPanel}";
			Directory = dir;
			IdButton = idButton;
			OfIdPanel = ofIdPanel;

			_fileManager = fileManager;

			Init();
		}

		private void Init()
		{
			if (_inited)
				return;

			if (CheckFiles())
				Click += AddClick;
			else
				Enabled = false;

			_inited = true;
		}

		private void AddClick(object sender, EventArgs e)
		{
			try
			{
				if (!ButtonException.CheckError<ButtonDirectory>(sender, e))
				{
					if (!UseAccentColor)
						_fileManager.ChangePath(Directory.FullName);
				}
			}
			catch (ButtonException err)
			{
				err.SendMessage<ButtonDirectory>(nameof(AddClick));
			}
		}

		private bool CheckFiles()
		{
			IEnumerable<FileSystemInfo> files = Directory.EnumerateFileSystemInfos();
			int count = files.Count();

            foreach (FileSystemInfo sysFile in files)
            {
				if (sysFile.Attributes.HasFlag(FileAttributes.System))
					count--;
            }

            return count != 0;
		}
	}
}
