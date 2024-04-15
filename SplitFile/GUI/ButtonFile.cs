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
using static System.Net.Mime.MediaTypeNames;

namespace SplitFile.GUI
{
	internal sealed class ButtonFile : MaterialButton
	{
		internal FileInfo File { get; }
		internal int OfIdPanel { get; }
		internal int IdButton { get; }

		private readonly FileManager _fileManager;
		private bool _inited = false;

		internal ButtonFile(FileManager fileManager, FileInfo file, int ofIdPanel, int idButton) : base()
		{
			Anchor = AnchorStyles.Left | AnchorStyles.Right;
			Text = file.Name;
			Margin = new Padding(4, 0, 4, 6);
			HighEmphasis = false;
			Icon = Properties.Resources.file;
			Name = $"ButtonSplitFile{idButton}OfPanel{ofIdPanel}";
			OfIdPanel = ofIdPanel;
			IdButton = idButton;
			File = file;

			_fileManager = fileManager;

			Init();
		}

		private void Init()
		{
			if (_inited)
				return;

			Click += AddClick;

			_inited = true;
		}

		private void AddClick(object sender, EventArgs e)
		{
			try
			{
				if (!ButtonException.CheckError<ButtonFile>(sender, e))
				{
					if (!HighEmphasis)
						_fileManager.SelectButtonFile(OfIdPanel, IdButton);
				}
			}
			catch (ButtonException err)
			{
				ButtonException.SendMessage(
					err,
					nameof(ButtonFile),
					nameof(AddClick)
				);
			}
		}
	}
}
