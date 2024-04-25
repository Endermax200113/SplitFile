using MaterialSkin.Controls;
using SplitFile.Util;
using System;
using System.Windows.Forms;

namespace SplitFile.Exceptions
{
	public class ButtonException : Exception, IException
	{
		public enum TypeButtonException {
			ERR_BUTTON_NOT_EXISTS,
			ERR_BUTTON_NOT_FOUND,
			ERR_BUTTON_NOT_BELONG,
			ERR_BUTTON_NO_EVENTS,
			ERR_BUTTON_UNKNOWN
		}

		private const string ERR_NOT_EXISTS = "The button does not exists";
		private const string ERR_NOT_FOUND = "Couldn't find the button";
		private const string ERR_NOT_BELONG = "This object does not belong to the material button";
		private const string ERR_NO_EVENTS = "There are no events in the button";
		private const string ERR_UNKNOWN = "Unknown error";

		public TypeButtonException TypeException { get; }

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
				case TypeButtonException.ERR_BUTTON_NOT_FOUND:
					return ERR_NOT_FOUND;
				case TypeButtonException.ERR_BUTTON_NOT_BELONG:
					return ERR_NOT_BELONG;
				case TypeButtonException.ERR_BUTTON_NO_EVENTS:
					return ERR_NO_EVENTS;
				case TypeButtonException.ERR_BUTTON_UNKNOWN:
				default:
					return ERR_UNKNOWN;
			}
		}

		public static bool CheckError<ButtonClass>(object sender, EventArgs e)
		{
			Log.Debug<ButtonClass>("Checking a button class...");

			if (sender is null)
				throw new ButtonException(TypeButtonException.ERR_BUTTON_NOT_EXISTS);
			else if (!(sender is ButtonClass))
				throw new ButtonException(TypeButtonException.ERR_BUTTON_NOT_BELONG);
			else if (e is null)
				throw new ButtonException(TypeButtonException.ERR_BUTTON_NO_EVENTS);

			return false;
		}

		public void SendMessage<FileClass>(string method)
		{
			Log.Error<FileClass>("Error found!");

			string title = null;
			string file = nameof(FileClass);
			string text;

			switch (TypeException)
			{
				case TypeButtonException.ERR_BUTTON_NOT_EXISTS:
#if DEBUG
					title = "Несуществующая кнопка";
					text = "Несуществующий объект присутствует\n" +
						$"\tв файле \'{file}\'\n" +
						$"\tв методе \'{method}\'.";
#else
					text = "Программа посчитала, что кнопка - это пустой объект, что на самом деле это не так.";
#endif
					break;
				case TypeButtonException.ERR_BUTTON_NOT_FOUND:
#if DEBUG
					title = "Ненайденная кнопка";
					text = "Не удалось найти кнопку\n" +
						$"\tв файле \'{file}\'\n" +
						$"\tв методе \'{method}\'.";
#else
					text = "Программе не удалось найти нужную кнопку.";
#endif
					break;
				case TypeButtonException.ERR_BUTTON_NOT_BELONG:
#if DEBUG
					title = "Объект не является кнопкой";
					text = "Объект, которая не является кнопкой, присутствует\n" +
						$"\tв файле \'{file}\'\n" +
						$"\tв методе \'{method}\'.";
#else
					text = "Программа посчитала, что этот объект, на которой Вы нажали, не является кнопкой.";
#endif
					break;
				case TypeButtonException.ERR_BUTTON_NO_EVENTS:
#if DEBUG
					title = "В кнопке нет аргументов событии";
					text = "Для этой кнопки отсутствуют аргументы событии\n" +
						$"\tв файле \'{file}\'\n" +
						$"\tв методе \'{method}\'.";
#else
					text = "В кнопке, на которой Вы нажали, по какой-то причине вызван сбой.";
#endif
					break;
				case TypeButtonException.ERR_BUTTON_UNKNOWN:
				default:
#if DEBUG
					title = "Неизвестная ошибка";
					text = "Неизвестная ошибка, связанная с кнопкой, которая присутствует\n" +
						$"\tв файле \'{file}\'\n" +
						$"\tв методе \'{method}\'\n" +
						"\tв блоке try.";
#else
					text = "Мы не знаем, из-за чего вызвана ошибка после клика кнопки.";
#endif
					break;
			}

			AnotherException.SendUsualMessage<FileClass>(
				AnotherException.TypeError.BUG,
				AnotherException.ErrorBy.APPLICATION,
				this,
				title,
				text,
				TypeException.ToString()
			);
		}
	}
}
