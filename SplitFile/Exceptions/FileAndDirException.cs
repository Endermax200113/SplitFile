using MaterialSkin.Controls;
using SplitFile.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SplitFile.Exceptions
{
	public class FileAndDirException : Exception, IException
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

		public void SendMessage<FileClass>(string method)
		{
			Log.Error<FileClass>("Error found!");

			string title = null;
			string file = typeof(FileClass).Name;
			string text;

			switch (TypeException)
			{
				case TypeFileAndDirException.ERR_PATH_NOT_EXIST:
#if DEBUG
					title = "Несуществующий путь";
					text = "Несуществующий путь присутствует\n" +
						$"\tв файле \'{file}\'\n" +
						$"\tв методе \'{method}\'.";
#else
					text = "Этот указанный путь не существует.";
#endif
					break;
				case TypeFileAndDirException.ERR_FILE_NOT_FOUND:
#if DEBUG
					title = "Ненайденный файл";
					text = "Не удалось найти указанный файл\n" +
						$"\tв программном файле \'{file}\'\n" +
						$"\tв методе \'{method}\'.";
#else
					text = "Программе не удалось найти указанный файл.";
#endif
					break;
				case TypeFileAndDirException.ERR_FILE_NOT_EXIST:
#if DEBUG
					title = "Несуществующий файл";
					text = "Указанный файл не существует\n" +
						$"\tв программном файле \'{file}\'\n" +
						$"\tв методе \'{method}\'.";
#else
					text = "Указанный файл не существует.";
#endif
					break;
				case TypeFileAndDirException.ERR_DIR_NOT_FOUND:
#if DEBUG
					title = "Ненайденный каталог";
					text = "Не удалось найти указанный каталог\n" +
						$"\tв файле \'{file}\'\n" +
						$"\tв методе \'{method}\'.";
#else
					text = "Программе не удалось найти указанную папку.";
#endif
					break;
				case TypeFileAndDirException.ERR_DIR_NOT_EXIST:
#if DEBUG
					title = "Несуществующий каталог";
					text = "Указанный каталог не существует\n" +
						$"\tв файле \'{file}\'\n" +
						$"\tв методе \'{method}\'.";
#else
					text = "Указанная папка не существует.";
#endif
					break;
				case TypeFileAndDirException.ERR_FILEANDDIR_UNKNOWN:
				default:
#if DEBUG
					title = "Неизвестная ошибка";
					text = "Неизвестная ошибка, связанная с файлом и каталогом, которая присутствует\n" +
						$"\tв файле \'{file}\'\n" +
						$"\tв методе \'{method}\'\n" +
						"\tв блоке 'try'.";
#else
					text = "Мы не знаем, из-за чего вызвана ошибка, связанная с файлом и папкой.";
#endif
					break;
			}

			AnotherException.SendUsualMessage<FileAndDirException>(
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
