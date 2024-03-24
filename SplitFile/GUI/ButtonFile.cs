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
	internal class ButtonFile : MaterialButton
	{
		internal FileInfo File { get; private set; }
		internal int IdPanel { get; private set; }
		internal int IdButton { get; private set; }
		private FlowLayoutPanel PanelPath { get; set; }
		private Panel PanelMain { get; set; }

		internal ButtonFile(
				Panel panelMain,
				FlowLayoutPanel panelPath,
				string text,
				int idPanel,
				int idButton,
				FileInfo file,
				bool first = false
		) : base() {
			Anchor = AnchorStyles.Left | AnchorStyles.Right;
			Text = text;
			Margin = first ? new Padding(4, 6, 4, 6) : new Padding(4, 3, 4, 6);
			HighEmphasis = false;
			Icon = Properties.Resources.file;
			Name = $"ButtonSplit{idButton}OfPanel{idPanel}";

			File = file;
			IdPanel = idPanel;
			IdButton = idButton;
			PanelPath = panelPath;
			PanelMain = panelMain;

			Click += AddClick;
		}

		private void AddClick(object sender, EventArgs e)
		{
			try {
				if (sender is null)
					throw new ButtonException(ButtonException.TypeButtonException.ERR_BUTTON_NOT_EXISTS);
				else if (!(sender is ButtonFile file))
					throw new ButtonException(ButtonException.TypeButtonException.ERR_BUTTON_NOT_BELONG);
				else if (e is null)
					throw new ButtonException(ButtonException.TypeButtonException.ERR_BUTTON_NO_EVENTS);
				else {
					if (!HighEmphasis) {
						ButtonFile btn = PanelDirectory.SelectedButtonFile;

						if (btn != null)
							btn.HighEmphasis = false;

						PanelDirectory.SelectedButtonFile = file;
						HighEmphasis = true;
						FormMain.ButtonSplit.Enabled = true;
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
							"\tв файле \'ButtonFile.cs\'\n" +
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
							"\tв файле \'ButtonFile.cs\'\n" +
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
							"\tв файле \'ButtonFile.cs\'\n" +
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
							"\tв файле \'ButtonFile.cs\'\n" +
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
