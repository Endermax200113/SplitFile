using MaterialSkin.Controls;
using System;
using System.Windows.Forms;

namespace SplitFile.Exceptions
{
	public class InitException : Exception
	{
		public enum TypeInitException
		{
			ERR_INITIAL_NOT_INITIALIZED,
			ERR_INITIAL_FAILED,
			ERR_INITIAL_UNKNOWN
		}

		private const string ERR_NOT_INITIALIZED = "This object have not been initialized";
		private const string ERR_FAILED = "This object could not be initialized";
		private const string ERR_UNKNOWN = "Unknown error";

		public TypeInitException TypeException { get; }

		public InitException() : base(ERR_UNKNOWN)
		{
			TypeException = TypeInitException.ERR_INITIAL_UNKNOWN;
		}

		public InitException(string message, TypeInitException type) : base(message)
		{
			TypeException = type;
		}

		public InitException(TypeInitException type) : base(GetMessageByType(type))
		{
			TypeException = type;
		}

		private static string GetMessageByType(TypeInitException type)
		{
			switch (type)
			{
				case TypeInitException.ERR_INITIAL_NOT_INITIALIZED:
					return ERR_NOT_INITIALIZED;
				case TypeInitException.ERR_INITIAL_FAILED:
					return ERR_FAILED;
				case TypeInitException.ERR_INITIAL_UNKNOWN:
				default:
					return ERR_UNKNOWN;
			}
		}

		public static void SendMessage(InitException err, string file, string method)
		{
			string title;
			string text;
			MessageBoxButtons btn = MessageBoxButtons.OK;
			FlexibleMaterialForm.ButtonsPosition positionBtn = FlexibleMaterialForm.ButtonsPosition.Right;

			Console.WriteLine(err);

			switch (err.TypeException)
			{
				case TypeInitException.ERR_INITIAL_NOT_INITIALIZED:
#if DEBUG
					title = "Неинициализированный объект";
					text = "Ошибка со стороны программы. Сообщение для разработчика:\n" +
						"Объект не был инициализирован\n" +
						$"\tв файле: {file}\n" +
						$"\tв методе: {method}\n\n" +
						"Стек ошибки:\n" +
						$"{err}";
#else
					title = "Программная ошибка";
					text = "Эта ошибка вызвана не из-за Вас.\n" +
						"Программа не инициализировал нужный объект, который без этого программа дальше работать не будет.\n" +
						"Если Вы видете эту ошибку, пожалуйста, напишите об этом по ссылке ниже:\n" +
						"https://github.com/Endermax200113/SplitFile/issues/new\n\n" +
						$"Ошибка: {err.TypeException}\n\n" +
						"Программа будет закрыта после нажатии кнопки \'ОК\'";
#endif
					break;
				case TypeInitException.ERR_INITIAL_FAILED:
#if DEBUG
					title = "Неинициализированный объект";
					text = "Ошибка со стороны программы. Сообщение для разработчика:\n" +
						"Не удалось инициализировать объект\n" +
						$"\tв файле: {file}\n" +
						$"\tв методе: {method}\n\n" +
						"Стек ошибки:\n" +
						$"{err}";
#else
					title = "Программная ошибка";
					text = "Эта ошибка вызвана не из-за Вас.\n" +
						"Программе не смог инициализировать нужный объект. Это серьёзный баг.\n" +
						"Если Вы видете эту ошибку, пожалуйста не поленитесь, немедленно напишите об этом по ссылке ниже:\n" +
						"https://github.com/Endermax200113/SplitFile/issues/new\n\n" +
						$"Ошибка: {err.TypeException}\n" +
						"Стек ошибки:\n" +
						$"{err}\n\n" +
						"Программа будет закрыта после нажатии кнопки \'ОК\'";
#endif
					break;
				case TypeInitException.ERR_INITIAL_UNKNOWN:
				default:
#if DEBUG
					title = "Неизвестная ошибка";
					text = "Ошибка со стороны программы. Сообщение для разработчика:\n" +
						"Неизвестная ошибка, связанная с инициализацией объекта, которая присутствует\n" +
						$"\tв файле: {file}\n" +
						$"\tв методе: {method}\n" +
						"\tв блоке try.\n\n" +
						"Стек ошибки:\n" +
						$"{err}";
#else
					title = "Неизвестная программная ошибка";
					text = "Эта ошибка вызвана не из-за Вас.\n" +
						"Мы не знаем, из-за чего вызвана ошибка после попытки инициализации объекта.\n" +
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
