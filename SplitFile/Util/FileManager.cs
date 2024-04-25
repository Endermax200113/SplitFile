using SplitFile.Exceptions;
using SplitFile.GUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SplitFile.Util 
{
	public sealed class FileManager
	{
		internal class SeparatePanel
		{
			internal ButtonPath ButtonPath { get; }
			internal PanelDirectory PanelDirectory { get; }

			internal SeparatePanel(ButtonPath button, PanelDirectory panel)
			{
				ButtonPath = button;
				PanelDirectory = panel;
			}
		}

		internal int FreeId { get; set; } = 0;
		private List<SeparatePanel> ListSeparatePanel { get; } = new List<SeparatePanel>();
		internal ButtonFile SelectedButtonFile { get; set; } = null;

		private bool _inited = false;
		internal Panel PanelPath { get; }
		internal Panel PanelMain { get; }
		internal Control ControlAction { get; }

		internal FileManager(Panel panelPath, Panel panelMain, Control ctrlAction)
		{
			PanelPath = panelPath;
			PanelMain = panelMain;
			ControlAction = ctrlAction;
		}

		internal void Init()
		{
			if (_inited)
				return;

			Log.Debug("Initializing file manager...");
			LoadStartPanel();

			_inited = true;
		}

		private void LoadStartPanel()
		{
			Log.Debug("Loading start panel...");
			Add(null);
		}

		private void RemoveAt(int id)
		{
			Log.Debug<FileManager>($"Removing a separate panel at ID {id}...");

			try
			{
				foreach (ButtonFile btn in ListSeparatePanel[id].PanelDirectory.ListButtonFiles)
				{
					if (btn == SelectedButtonFile)
					{
						ControlAction.Enabled = false;
						SelectedButtonFile = null;
					}
				}

				ListSeparatePanel[id].ButtonPath.Dispose();
				ListSeparatePanel[id].PanelDirectory.Remove();
				ListSeparatePanel[id - 1].PanelDirectory.Unselect();
				ListSeparatePanel.RemoveAt(id);
				FreeId--;
			}
			catch (ArgumentOutOfRangeException err)
			{
				SendMessageIndexOutOfRange(err, nameof(RemoveAt));
			}
			
		}

		internal void SelectButtonFile(int idPanel, int idButton)
		{
			Log.Debug<FileManager>($"The file button (ID: {idButton}) is selecting on the panel (ID: {idPanel})...");

			try
			{
				if (!_inited)
					throw new InitException(InitException.TypeInitException.ERR_INITIAL_NOT_INITIALIZED);

				ButtonFile btnSelect = ListSeparatePanel[idPanel].PanelDirectory.ListButtonFiles[idButton];

				if (SelectedButtonFile != null)
					SelectedButtonFile.HighEmphasis = false;

				SelectedButtonFile = btnSelect;
				btnSelect.HighEmphasis = true;
				ControlAction.Enabled = true;
			}
			catch (InitException err)
			{
				err.SendMessage<FileManager>(nameof(SelectButtonFile));
			}
			catch (ArgumentOutOfRangeException err)
			{
				SendMessageIndexOutOfRange(err, nameof(SelectButtonFile));
			}
		}

		private void SendMessageIndexOutOfRange(Exception err, string method)
		{
			Log.Error<FileManager>("Error found!");

			string title = null;
			string text;

#if DEBUG
			title = "Индекс за пределы массива";
			text = "Индекс был указан неверно, т.к. он находится за пределы массива\n" +
				$"\tв файле \'{nameof(FileManager)}\'\n" +
				$"\tв методе \'{method}\'.";
#else
			text = "В программе существует список, в котором указан неверный индекс.";
#endif

			AnotherException.SendUsualMessage<FileManager>(
				AnotherException.TypeError.BUG,
				AnotherException.ErrorBy.APPLICATION,
				err,
				title,
				text,
				"ERR_LIST_OUT_OF_RANGE"
			);
		}

		internal void SelectButtonDir(int idPanel, int idButton)
		{
			Log.Debug<FileManager>($"The directory button (ID: {idButton}) is selecting in the panel (ID: {idPanel})");

			try
			{
				if (!_inited)
					throw new InitException(InitException.TypeInitException.ERR_INITIAL_NOT_INITIALIZED);

				ButtonDirectory btnSelected = ListSeparatePanel[idPanel].PanelDirectory.SelectedButtonDir;
				ButtonDirectory btnSelect = ListSeparatePanel[idPanel].PanelDirectory.ListButtonDirs[idButton];

				if (btnSelected != null)
				{
					btnSelected.UseAccentColor = false;
					btnSelected.HighEmphasis = false;
				}

				ListSeparatePanel[idPanel].PanelDirectory.SelectedButtonDir = btnSelect;
				btnSelect.UseAccentColor = true;
				btnSelect.HighEmphasis = true;
			}
			catch (InitException err)
			{
				err.SendMessage<FileManager>(nameof(SelectButtonDir));
			}
			catch (ArgumentOutOfRangeException err)
			{
				SendMessageIndexOutOfRange(err, nameof(SelectButtonDir));
			}
		}

		private void Add(DirectoryInfo dir)
		{
			PanelDirectory panel;
			ButtonPath btn;

			if (dir != null)
			{
				Log.Debug<FileManager>("Adding the panel and the path button...");

				panel = new PanelDirectory(this, FreeId, dir);
				btn = new ButtonPath(this, FreeId, FreeId == 1 ? dir.Name.Replace("\\", string.Empty) : dir.Name, dir.FullName);
			}
			else
			{
				Log.Debug("Adding start panel and path button...");

				try
				{
					if (FreeId != 0)
						throw new AnotherException("The ID is incorrect");
				}
				catch (AnotherException err)
				{
					{
						Log.Error<FileManager>("Error found!");

						string title = null;
						string text;

#if DEBUG
						title = "Неверный индентификатор";
						text = "Свободный идентификатор для инициализации не равен нулю\n" +
							$"\tв файле \'{nameof(FileManager)}\'\n" +
							$"\tв методе \'{nameof(Add)}\'.";
#else
						text = "По непонятной причине, программа начала инициализироваться не с нуля.";
#endif

						AnotherException.SendUsualMessage<FileManager>(
							AnotherException.TypeError.CRASH,
							AnotherException.ErrorBy.APPLICATION,
							err,
							title,
							text,
							"ERR_ID_INIT_NOT_ZERO"
						);
					}
				}

				panel = new PanelDirectory(this, FreeId, null);
				btn = new ButtonPath(this, FreeId, "Начало", null);
			}

			PanelMain.Controls.Add(panel);
			PanelPath.Controls.Add(btn);
			ListSeparatePanel.Add(new SeparatePanel(btn, panel));
			FreeId++;
		}

		public string GetPath()
		{
			string path = "";

			for (int i = 1; i < ListSeparatePanel.Count; i++)
			{
				path += ListSeparatePanel[i].ButtonPath.Text;

				if (i == ListSeparatePanel.Count - 1)
					break;
				else if (i == 1)
					path += "\\\\";
				else
					path += "\\";
			}

			return path;
		}

		private string[] TrimArray(string[] arr)
		{
			var list = arr.ToList();

			for (int i = 0; i < list.Count; i++)
			{
				if (list[i] == "")
					list.RemoveAt(i);
			}

			return list.ToArray();
		}

		private void FindAndSelectButtonDirectory(int idPanel, string[] dirs)
		{
			Log.Debug<FileManager>($"Searching and selecting a directory button in the panel (ID: {idPanel})");

			try
			{
				int indexButton = -1;
				List<ButtonDirectory> btnDirs = ListSeparatePanel[idPanel].PanelDirectory.ListButtonDirs;
				int count = btnDirs.Count;

				for (indexButton = 0; indexButton < count; indexButton++)
				{
					if (btnDirs[indexButton].Directory.Name == ToDriveString(dirs[idPanel], idPanel))
						break;
				}

				if (indexButton == count)
					throw new ButtonException(ButtonException.TypeButtonException.ERR_BUTTON_NOT_FOUND);

				SelectButtonDir(idPanel, indexButton);
			}
			catch (ButtonException err)
			{
				err.SendMessage<FileManager>(nameof(FindAndSelectButtonDirectory));
			}
		}

		private void FindAndSelectButtonFile(int idPanel, string[] dirs)
		{
			Log.Debug<FileManager>($"Searching and selecting a file button in the panel (ID: {idPanel})");

			try
			{
				int indexButton = -1;
				List<ButtonFile> btnFiles = ListSeparatePanel[idPanel].PanelDirectory.ListButtonFiles;
				int count = btnFiles.Count;

				for (indexButton = 0; indexButton < count; indexButton++)
				{
					if (btnFiles[indexButton].File.Name == dirs[dirs.Length - 1])
						break;
				}

				if (indexButton == count)
					throw new ButtonException(ButtonException.TypeButtonException.ERR_BUTTON_NOT_FOUND);

				SelectButtonFile(idPanel, indexButton);
			}
			catch (ButtonException err)
			{
				err.SendMessage<FileManager>(nameof(FindAndSelectButtonDirectory));
			}
		}

		private string ToDriveString(string dir, int index)
		{
			return index == 0 ? $"{dir}\\" : $"{dir}";
		}

		public void ChangePath(string path)
		{
			Log.Debug<FileManager>($"Changing path ({path})...");

			try
			{
				if (!_inited)
					throw new InitException(InitException.TypeInitException.ERR_INITIAL_NOT_INITIALIZED);

				if (path == null || path == "")
				{
					for (int i = ListSeparatePanel.Count - 1; i > 0; i--)
						RemoveAt(i);
				}
				else
				{
					if (!Directory.Exists(path) && !File.Exists(path))
						throw new FileAndDirException(FileAndDirException.TypeFileAndDirException.ERR_PATH_NOT_EXIST);

					bool isDir = Directory.Exists(path);
					string[] dirs = TrimArray(path.Split('\\'));

					for (int i = 0; i < dirs.Length; i++)
					{
						string strActive;
						
						try
						{
							strActive = ListSeparatePanel[i + 1].ButtonPath.Text;
						}
						catch (ArgumentOutOfRangeException)
						{
							strActive = null;
						}
						
						string strDir = dirs[i];

						if (strActive != strDir)
						{
							for (int j = ListSeparatePanel.Count - 1; j >= i; j--)
							{
								if (i != j)
									RemoveAt(j);
								else
								{
									FindAndSelectButtonDirectory(j, dirs);

									for (int k = i; k < dirs.Length; k++)
									{
										string gotPath = GetPath();
										string newPath = gotPath == "" ? $"{dirs[k]}\\" : $"{gotPath}\\{dirs[k]}";

										if (isDir)
											AddPanel();
										else
										{
											if (Directory.Exists(newPath))
											{
												AddPanel();

												if (dirs.Length - 2 > k)
													FindAndSelectButtonDirectory(k + 1, dirs);
											}
											else
											{
												FindAndSelectButtonFile(k, dirs);
											}
										}

										void AddPanel()
										{
											var dir = new DirectoryInfo(newPath);
											Add(dir);
										}
									}
								}
							}

							break;
						}
						else
						{
							if (i == dirs.Length - 1 && i < ListSeparatePanel.Count - 1)
							{
								for (int j = ListSeparatePanel.Count - 1; j > i + 1; j--)
									RemoveAt(j);
							}
							else
								continue;
						}
					}
				}
			}
			catch (InitException err)
			{
				err.SendMessage<FileManager>(nameof(ChangePath));
			}
			catch (FileAndDirException err)
			{
				err.SendMessage<FileManager>(nameof(ChangePath));
			}
			catch(ButtonException err)
			{
				err.SendMessage<FileManager>(nameof(ChangePath));
			}
			catch(ArgumentOutOfRangeException err)
			{
				SendMessageIndexOutOfRange(err, nameof(ChangePath));
			}

			Log.Debug<FileManager>($"Path ({path}) is changed");
		}
	}
}
