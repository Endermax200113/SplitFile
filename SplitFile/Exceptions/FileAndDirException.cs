using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SplitFile.Exceptions
{
	public class FileAndDirException : Exception
	{
		public enum TypeFileAndDirException
		{
			ERR_PATH_NOT_EXIST,
			ERR_FILE_NOT_FOUND,
			ERR_FILE_NOT_EXIST,
			ERR_DIR_NOT_FOUND,
			ERR_DIR_NOT_EXIST,
			ERR_FILEANDDIR_UNKNOWN
		}

		private const string ERR_PATH_NOT_EXIST = "This path doesn't exist";
		private const string ERR_FILE_NOT_FOUND = "This file was not found";
		private const string ERR_FILE_NOT_EXIST = "This file doesn't exist";
		private const string ERR_DIR_NOT_FOUND = "This directory was not found";
		private const string ERR_DIR_NOT_EXIST = "This directory does'nt exist";
		private const string ERR_UNKNOWN = "Unknown error";

		public TypeFileAndDirException TypeException { get; }

		public FileAndDirException() : base(ERR_UNKNOWN)
		{
			TypeException = TypeFileAndDirException.ERR_FILEANDDIR_UNKNOWN;
		}

		public FileAndDirException(string message, TypeFileAndDirException type) : base(message)
		{
			TypeException = type;
		}

		public FileAndDirException(TypeFileAndDirException type) : base(GetMessageByType(type))
		{
			TypeException = type;
		}

		private static string GetMessageByType(TypeFileAndDirException type)
		{
			switch (type)
			{
				case TypeFileAndDirException.ERR_PATH_NOT_EXIST:
					return ERR_PATH_NOT_EXIST;
				case TypeFileAndDirException.ERR_FILE_NOT_FOUND:
					return ERR_FILE_NOT_FOUND;
				case TypeFileAndDirException.ERR_FILE_NOT_EXIST:
					return ERR_FILE_NOT_EXIST;
				case TypeFileAndDirException.ERR_DIR_NOT_FOUND:
					return ERR_DIR_NOT_FOUND;
				case TypeFileAndDirException.ERR_DIR_NOT_EXIST:
					return ERR_DIR_NOT_EXIST;
				case TypeFileAndDirException.ERR_FILEANDDIR_UNKNOWN:
				default:
					return ERR_UNKNOWN;
			}
		}

		public static void SendMessage(FileAndDirException err, string file, string method)
		{
			string title;
			string text;
			MessageBoxButtons btn = MessageBoxButtons.OK;
			FlexibleMaterialForm.ButtonsPosition positionBtn = FlexibleMaterialForm.ButtonsPosition.Right;

			Console.WriteLine(err);

			switch (err.TypeException)
			{
				case TypeFileAndDirException.ERR_PATH_NOT_EXIST:
#if DEBUG
					title = "Несуществующий путь";
					text = "Ошибка со стороны программы. Сообщение для разработчика:\n" +
						"Несуществующий путь присутствует\n" +
						$"\tв файле \'{file}\'\n" +
						$"\tв методе \'{method}\'.\n\n" +
						"Стек ошибки:\n" +
						$"{err}";
#else
					title = "Программная ошибка";
					text = "Эта ошибка, возможно, вызвана не из-за Вас.\n" +
						"Этот указанный путь не существует.\n" +
						"Если Вы видете эту ошибку, пожалуйста, напишите об этом по ссылке ниже:\n" +
						"https://github.com/Endermax200113/SplitFile/issues/new\n\n" +
						$"Ошибка: {err.TypeException}\n\n" +
						"Программа будет закрыта после нажатии кнопки \'ОК\'";
#endif
					break;
				case TypeFileAndDirException.ERR_FILE_NOT_FOUND:
#if DEBUG
					title = "Ненайденный файл";
					text = "Ошибка со стороны программы. Сообщение для разработчика:\n" +
						"Не удалось найти указанный файл\n" +
						$"\tв программном файле \'{file}\'\n" +
						$"\tв методе \'{method}\'.\n\n" +
						"Стек ошибки:\n" +
						$"{err}";
#else
					title = "Программная ошибка";
					text = "Эта ошибка, возможно, вызвана не из-за Вас.\n" +
						"Программе не удалось найти указанный файл.\n" +
						"Если Вы видете эту ошибку, пожалуйста, напишите об этом по ссылке ниже:\n" +
						"https://github.com/Endermax200113/SplitFile/issues/new\n\n" +
						$"Ошибка: {err.TypeException}\n\n" +
						"Программа будет закрыта после нажатии кнопки \'ОК\'";
#endif
					break;
				case TypeFileAndDirException.ERR_FILE_NOT_EXIST:
#if DEBUG
					title = "Несуществующий файл";
					text = "Ошибка со стороны программы. Сообщение для разработчика:\n" +
						"Указанный файл не существует\n" +
						$"\tв программном файле \'{file}\'\n" +
						$"\tв методе \'{method}\'.\n\n" +
						"Стек ошибки:\n" +
						$"{err}";
#else
					title = "Программная ошибка";
					text = "Эта ошибка, возможно, вызвана не из-за Вас.\n" +
						"Указанный файл не существует.\n" +
						"Если Вы видете эту ошибку, пожалуйста, напишите об этом по ссылке ниже:\n" +
						"https://github.com/Endermax200113/SplitFile/issues/new\n\n" +
						$"Ошибка: {err.TypeException}\n\n" +
						"Программа будет закрыта после нажатии кнопки \'ОК\'";
#endif
					break;
				case TypeFileAndDirException.ERR_DIR_NOT_FOUND:
#if DEBUG
					title = "Ненайденный каталог";
					text = "Ошибка со стороны программы. Сообщение для разработчика:\n" +
						"Не удалось найти указанный каталог\n" +
						$"\tв файле \'{file}\'\n" +
						$"\tв методе \'{method}\'.\n\n" +
						"Стек ошибки:\n" +
						$"{err}";
#else
					title = "Программная ошибка";
					text = "Эта ошибка, возможно, вызвана не из-за Вас.\n" +
						"Программе не удалось найти указанную папку.\n" +
						"Если Вы видете эту ошибку, пожалуйста, напишите об этом по ссылке ниже:\n" +
						"https://github.com/Endermax200113/SplitFile/issues/new\n\n" +
						$"Ошибка: {err.TypeException}\n\n" +
						"Программа будет закрыта после нажатии кнопки \'ОК\'";
#endif
					break;
				case TypeFileAndDirException.ERR_DIR_NOT_EXIST:
#if DEBUG
					title = "Несуществующий каталог";
					text = "Ошибка со стороны программы. Сообщение для разработчика:\n" +
						"Указанный каталог не существует\n" +
						$"\tв файле \'{file}\'\n" +
						$"\tв методе \'{method}\'.\n\n" +
						"Стек ошибки:\n" +
						$"{err}";
#else
					title = "Программная ошибка";
					text = "Эта ошибка вызвана не из-за Вас.\n" +
						"Указанная папка не существует.\n" +
						"Если Вы видете эту ошибку, пожалуйста, напишите об этом по ссылке ниже:\n" +
						"https://github.com/Endermax200113/SplitFile/issues/new\n\n" +
						$"Ошибка: {err.TypeException}\n\n" +
						"Программа будет закрыта после нажатии кнопки \'ОК\'";
#endif
					break;
				case TypeFileAndDirException.ERR_FILEANDDIR_UNKNOWN:
				default:
#if DEBUG
					title = "Неизвестная ошибка";
					text = "Ошибка со стороны программы. Сообщение для разработчика:\n" +
						"Неизвестная ошибка, связанная с файлом и каталогом, которая присутствует\n" +
						$"\tв файле \'{file}\'\n" +
						$"\tв методе \'{method}\'\n" +
						"\tв блоке try.\n\n" +
						"Стек ошибки:\n" +
						$"{err}";
#else
					title = "Неизвестная программная ошибка";
					text = "Эта ошибка вызвана не из-за Вас.\n" +
						"Мы не знаем, из-за чего вызвана ошибка, связанная с файлом и папкой.\n" +
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
