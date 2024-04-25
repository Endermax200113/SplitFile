using MaterialSkin;
using MaterialSkin.Controls;
using SplitFile.Exceptions;
using SplitFile.GUI;
using SplitFile.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SplitFile
{
	public sealed partial class FormMain : MaterialForm
	{
		private static FileManager _fileManagerSplit;

		public FormMain() {
			InitializeComponent();

			MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
			materialSkinManager.AddFormToManage(this);
			materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
			materialSkinManager.ColorScheme = new ColorScheme(
					Primary.Indigo500,
					Primary.Indigo700,
					Primary.Indigo800,
					Accent.Pink700,
					TextShade.WHITE
			);
		}

		private void Init() {
			Log.Init();
			Log.Info("Start application...");
			Log.Info("====================");

			_fileManagerSplit = new FileManager(PanelSplitPath, PanelSplitFiles, ButtonSplitFile);
			
			_fileManagerSplit.Init();
		}

		private void FormMain_Load(object sender, EventArgs e)
		{
			Init();
		}

		private void ButtonSplitAddFile_Click(object sender, EventArgs e)
		{
			try
			{
				if (!ButtonException.CheckError<MaterialFloatingActionButton>(sender, e))
				{
					using (OpenFileDialog dlg = DialogSplitOpenFile)
					{
						string path = _fileManagerSplit.GetPath();

						if (path == "")
							dlg.InitialDirectory = Properties.Resources.GuidMyComputer;
						else
							dlg.InitialDirectory = path;

						if (dlg.ShowDialog() == DialogResult.OK)
						{
							path = dlg.FileName;

							_fileManagerSplit.ChangePath(path);
						}
					}
				}
			}
			catch (ButtonException err)
			{
				err.SendMessage<FormMain>(nameof(ButtonSplitAddFile_Click));
			}
		}

		public static void CloseProgram() {
			Log.Info("Aplication closing...");

			Application.Exit();
		}

		private void ButtonSplitFile_Click(object sender, EventArgs e)
		{
			try
			{
				if (!ButtonException.CheckError<MaterialButton>(sender, e))
				{
					if (_fileManagerSplit.SelectedButtonFile == null || _fileManagerSplit.SelectedButtonFile.File == null)
						throw new FileAndDirException(FileAndDirException.TypeFileAndDirException.ERR_FILE_NOT_EXIST);


				}
			}
			catch (ButtonException err)
			{
				err.SendMessage<FormMain>(nameof(ButtonSplitAddFile_Click));
			}
			catch (FileAndDirException err)
			{
				err.SendMessage<FormMain>(nameof(ButtonSplitAddFile_Click));
			}
		}

		private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			Log.Info("==================");
			Log.Info("Application closed");
			Log.End();
		}
	}
}
