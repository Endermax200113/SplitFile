using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using MaterialSkin.Controls;
using SplitFile.Exceptions;
using SplitFile.Util;

namespace SplitFile.GUI
{
	internal sealed class ButtonPath : MaterialButton
	{
		internal int IdButton { get; }
		internal string Path { get; }

		private bool _inited = false;
		private readonly FileManager _fileManager;

		internal ButtonPath(FileManager fileManager, int idButton, string text, string path) : base()
		{
			Text = text;
			Name = $"ButtonSplitPath{idButton}";
			IdButton = idButton;
			DoubleBuffered = true;
			Path = path;

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
				if (!ButtonException.CheckError<ButtonPath>(sender, e))
				{
					int id = _fileManager.FreeId;

					if (IdButton + 1 < id)
						_fileManager.ChangePath(Path);
				}
			}
			catch (ButtonException err)
			{
				ButtonException.SendMessage(
					err,
					nameof(ButtonPath),
					nameof(AddClick)
				);
			}
		}
	}
}
