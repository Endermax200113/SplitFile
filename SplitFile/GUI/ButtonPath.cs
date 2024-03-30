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

namespace SplitFile.GUI
{
	internal class ButtonPath : MaterialButton
	{
		//TODO Проверить, есть ли смысл оставлять свойство Directory
		internal DirectoryInfo Directory { get; private set; }
		internal static List<ButtonPath> ListButtons { get; private set; } = new List<ButtonPath>();
		private int IdButton { get; set; }

		internal ButtonPath(
				string text, 
				int idButton, 
				DirectoryInfo dir
		) : base()
		{
			Text = text;
			Name = $"ButtonSplitPath{idButton}";
			IdButton = idButton;
			Directory = dir;
			DoubleBuffered = true;

			Init();
		}

		private void Init()
		{
			ListButtons.Add(this);

			Click += AddClick;
		}

		private void AddClick(object sender, EventArgs e)
		{
			try
			{
				if (sender is null)
					throw new ButtonException(ButtonException.TypeButtonException.ERR_BUTTON_NOT_EXISTS);
				else if (!(sender is ButtonPath btnPath))
					throw new ButtonException(ButtonException.TypeButtonException.ERR_BUTTON_NOT_BELONG);
				else if (e is null)
					throw new ButtonException(ButtonException.TypeButtonException.ERR_BUTTON_NO_EVENTS);
				else
				{
					if (IdButton + 1 < FormMain.IdPanel)
					{
						int count = ButtonPath.ListButtons.Count - 1;
						List<PanelDirectory> listPanels = PanelDirectory.ListPanels;

						while (IdButton < count)
						{
							PanelDirectory panel = listPanels[count];
							panel.Remove();
							listPanels.RemoveAt(count);

                            ButtonPath btn = ListButtons[count];
							btn.Dispose();
							ListButtons.RemoveAt(count);

							count--;
						}

						ButtonDirectory btnDir = listPanels[count].SelectedButtonDir;

						if (btnDir != null)
						{
							btnDir.UseAccentColor = false;
							btnDir.HighEmphasis = false;
						}
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
							"\tв файле \'ButtonPath.cs\'\n" +
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
							"\tв файле \'ButtonPath.cs\'\n" +
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
							"\tв файле \'ButtonPath.cs\'\n" +
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
							"\tв файле \'ButtonPath.cs\'\n" +
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
