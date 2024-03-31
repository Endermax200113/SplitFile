using MaterialSkin.Controls;
using System;
using System.Windows.Forms;

namespace SplitFile.Exceptions
{
	public class ButtonException : Exception
	{
		public enum TypeButtonException {
			ERR_BUTTON_NOT_EXISTS,
			ERR_BUTTON_NOT_BELONG,
			ERR_BUTTON_NO_EVENTS,
			ERR_BUTTON_UNKNOWN
		}

		private const string ERR_NOT_EXISTS = "The button does not exists";
		private const string ERR_NOT_BELONG = "This object does not belong to the material button";
		private const string ERR_NO_EVENTS = "There are no events in the button";
		private const string ERR_UNKNOWN = "Unknown error";

		public TypeButtonException TypeException { get; private set; }

		public ButtonException() : base(ERR_UNKNOWN) {
			TypeException = TypeButtonException.ERR_BUTTON_UNKNOWN;
		}

		public ButtonException(string message, TypeButtonException type) : base(message) {
			TypeException = type;
		}

		public ButtonException(TypeButtonException typeException) : base(GetMessageByType(typeException))
		{
			TypeException = typeException;
		}

		private static string GetMessageByType(TypeButtonException type)
		{
			switch (type) {
				case TypeButtonException.ERR_BUTTON_NOT_EXISTS:
					return ERR_NOT_EXISTS;
				case TypeButtonException.ERR_BUTTON_NOT_BELONG:
					return ERR_NOT_BELONG;
				case TypeButtonException.ERR_BUTTON_NO_EVENTS:
					return ERR_NO_EVENTS;
				case TypeButtonException.ERR_BUTTON_UNKNOWN:
				default:
					return ERR_UNKNOWN;
			}
		}

		internal static bool CheckError<T>(object sender, EventArgs e)
		{
			if (sender is null)
				throw new ButtonException(TypeButtonException.ERR_BUTTON_NOT_EXISTS);
			else if (!(sender is T))
				throw new ButtonException(TypeButtonException.ERR_BUTTON_NOT_BELONG);
			else if (e is null)
				throw new ButtonException(TypeButtonException.ERR_BUTTON_NO_EVENTS);

			return false;
		}

		internal static void SendMessage(ButtonException err, string file, string method, string name)
		{
			string title;
			string text;
			MessageBoxButtons btn = MessageBoxButtons.OK;
			FlexibleMaterialForm.ButtonsPosition positionBtn = FlexibleMaterialForm.ButtonsPosition.Right;

			Console.WriteLine(err);

			switch (err.TypeException)
			{
				case TypeButtonException.ERR_BUTTON_NOT_EXISTS:
#if DEBUG
					title = "Несуществующая кнопка";
					text = "Ошибка со стороны программы. Сообщение для разработчика:\n" +
						"Несуществующий объект присутствует\n" +
						$"\tв файле \'{file}\'\n" +
						$"\tв методе \'{method}\'.\n\n" +
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
				case TypeButtonException.ERR_BUTTON_NOT_BELONG:
#if DEBUG
					title = "Объект не является кнопкой";
					text = "Ошибка со стороны программы. Сообщение для разработчика:\n" +
						"Объект, которая не является кнопкой, присутствует\n" +
						$"\tв файле \'{file}\'\n" +
						$"\tв методе \'{method}\'.\n\n" +
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
				case TypeButtonException.ERR_BUTTON_NO_EVENTS:
#if DEBUG
					title = "В кнопке нет аргументов событии";
					text = "Ошибка со стороны программы. Сообщение для разработчика:\n" +
						$"Для кнопки {name} отсутствуют аргументы событии\n" +
						$"\tв файле \'{file}\'\n" +
						$"\tв методе \'{method}\'.\n\n" +
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
				case TypeButtonException.ERR_BUTTON_UNKNOWN:
				default:
#if DEBUG
					title = "Неизвестная ошибка";
					text = "Ошибка со стороны программы. Сообщение для разработчика:\n" +
						"Неизвестная ошибка, которая присутствует\n" +
						$"\tв файле \'{file}\'\n" +
						$"\tв методе \'{method}\'\n" +
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
			FormMain.CloseProgram();
		}
	}
}
