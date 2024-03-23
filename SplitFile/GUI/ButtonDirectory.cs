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
		internal int IdButton { get; private set; }
		private FlowLayoutPanel PanelPath { get; set; }
		private Panel PanelMain { get; set; }

		internal ButtonDirectory(
				Panel panelMain, 
				FlowLayoutPanel panelPath, 
				string text, 
				int idPanel, 
				int idButton, 
				DirectoryInfo dir, 
				bool first = false
		) : base() {
			Anchor = AnchorStyles.Left | AnchorStyles.Right;
			Text = text;
			Margin = first ? new Padding(4, 6, 4, 6) : new Padding(4, 3, 4, 6);
			HighEmphasis = false;
			Icon = Properties.Resources.folder;
			Name = $"ButtonSplit{idButton}OfPanel{idPanel}";

			Directory = dir;
			IdPanel = idPanel;
			IdButton = idButton;
			PanelPath = panelPath;
			PanelMain = panelMain;

			Click += AddClick;
        }

		private void AddClick(object sender, EventArgs e)
		{
			try
			{
				if (sender is null)
					throw new ButtonException(ButtonException.TypeButtonException.ERR_BUTTON_NOT_EXISTS);
				else if (!(sender is ButtonDirectory))
					throw new ButtonException(ButtonException.TypeButtonException.ERR_BUTTON_NOT_BELONG);
				else if (e is null)
					throw new ButtonException(ButtonException.TypeButtonException.ERR_BUTTON_NO_EVENTS);
				else
				{
					if (!UseAccentColor) {
						if (IdPanel + 1 < FormMain.IdPanel)
						{
							int count = PanelDirectory.ListPanels.Count - 1;
							List<PanelDirectory> listPanels = PanelDirectory.ListPanels;

							while (IdPanel < count)
							{
								PanelDirectory panel = listPanels[count];
								panel.Remove();
								listPanels.RemoveAt(count);

								count--;
							}

							//TODO Удалить кнопки в пути после текущей кнопки

							FormMain.IdPanel = IdPanel + 1;

							//TODO Обновить данные в пути после удаления кнопок
						}

						PanelDirectory newPanel = new PanelDirectory(PanelMain, PanelPath, FormMain.IdPanel, Directory);
						PanelMain.Controls.Add(newPanel);
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
				string title;
				string text;
				MessageBoxButtons btn = MessageBoxButtons.OK;
				FlexibleMaterialForm.ButtonsPosition positionBtn = FlexibleMaterialForm.ButtonsPosition.Right;

				Console.WriteLine(err);

				switch (err.TypeException)
				{
					case ButtonException.TypeButtonException.ERR_BUTTON_NOT_EXISTS:
#if DEBUG
						title = "Несуществующая кнопка";
						text = "Ошибка со стороны программы. Сообщение для разработчика:\n" +
							"Несуществующий объект присутствует\n" +
							"\tв файле \'ButtonDirectory.cs\'\n" +
							"\tв методе \'AddClick(object, EventArgs)\'.\n\n" +
							"Стек ошибки:\n" +
							$"{err}";
#else
						title = "Программная ошибка";
						text = "Эта ошибка вызвана не из-за Вас.\n" +
							"Программа посчитала, что кнопка - это пустой объект, что на самом деле это не так. Это баг.\n" +
							"Если Вы видете эту ошибку, пожалуйста, напишите об этом по ссылке ниже:\n" +
							"https://github.com/Endermax200113/SplitFile/issues/new\n\n" +
							$"Ошибка: {err.TypeException}\n\n" +
							"Программа будет закрыта после нажатии кнопки \'ОК\'";
#endif
						break;
					case ButtonException.TypeButtonException.ERR_BUTTON_NOT_BELONG:
#if DEBUG
						title = "Объект не является кнопкой";
						text = "Ошибка со стороны программы. Сообщение для разработчика:\n" +
							"Объект, которая не является кнопкой, присутствует\n" +
							"\tв файле \'ButtonDirectory.cs\'\n" +
							"\tв методе \'AddClick(object, EventArgs)\'.\n\n" +
							"Стек ошибки:\n" +
							$"{err}";
#else
						title = "Программная ошибка";
						text = "Эта ошибка вызвана не из-за Вас.\n" +
							"Программа посчитала, что этот объект, на которой Вы нажали, не является кнопкой. Это баг.\n" +
							"Если Вы видете эту ошибку, пожалуйста, напишите об этом по ссылке ниже:\n" +
							"https://github.com/Endermax200113/SplitFile/issues/new\n\n" +
							$"Ошибка: {err.TypeException}\n\n" +
							"Программа будет закрыта после нажатии кнопки \'ОК\'";
#endif
						break;
					case ButtonException.TypeButtonException.ERR_BUTTON_NO_EVENTS:
#if DEBUG
						title = "В кнопке нет аргументов событии";
						text = "Ошибка со стороны программы. Сообщение для разработчика:\n" +
							$"Для кнопки {((MaterialButton)sender).Name} отсутствуют аргументы событии\n" +
							"\tв файле \'ButtonDirectory.cs\'\n" +
							"\tв методе \'AddClick(object, EventArgs)\'.\n\n" +
							"Стек ошибки:\n" +
							$"{err}";
#else
						title = "Программная ошибка";
						text = "Эта ошибка вызвана не из-за Вас.\n" +
							"В кнопке, на которой Вы нажали, по какой-то причине вызван сбой.\n" +
							"Если Вы видете эту ошибку, пожалуйста, напишите об этом по ссылке ниже:\n" +
							"https://github.com/Endermax200113/SplitFile/issues/new\n\n" +
							$"Ошибка: {err.TypeException}\n\n" +
							"Программа будет закрыта после нажатии кнопки \'ОК\'";
#endif
						break;
					case ButtonException.TypeButtonException.ERR_BUTTON_UNKNOWN:
					default:
#if DEBUG
						title = "Неизвестная ошибка";
						text = "Ошибка со стороны программы. Сообщение для разработчика:\n" +
							"Неизвестная ошибка, которая присутствует\n" +
							"\tв файле \'ButtonDirectory.cs\'\n" +
							"\tв методе \'AddClick(object, EventArgs)\'\n" +
							"\tв блоке try.\n\n" +
							"Стек ошибки:\n" +
							$"{err}";
#else
						title = "Неизвестная программная ошибка";
						text = "Эта ошибка вызвана не из-за Вас.\n" +
							"Мы не знаем, из-за чего вызвана ошибка после клика кнопки.\n" +
							"Если Вы видете эту ошибку, пожалуйста, напишите об этом по ссылке ниже:\n" +
							"https://github.com/Endermax200113/SplitFile/issues/new\n\n" +
							$"Ошибка: {err.TypeException}\n" +
							"Стек ошибки:\n" +
							$"{err}\n\n" +
							"Программа будет закрыта после нажатии кнопки \'ОК\'";
#endif
						break;
				}

				MaterialMessageBox.Show(text, title, btn, positionBtn);
			}
		}
	}
}
