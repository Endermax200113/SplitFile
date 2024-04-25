using MaterialSkin.Controls;
using SplitFile.Util;
using System;
using System.Windows.Forms;

namespace SplitFile.Exceptions
{
	public class InitException : Exception, IException
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

		public void SendMessage<FileClass>(string method)
		{
			Log.Error<FileClass>("Error found!");

			string title = null;
			string file = nameof(FileClass);
			string text;

			switch (TypeException)
			{
				case TypeInitException.ERR_INITIAL_NOT_INITIALIZED:
#if DEBUG
					title = "Неинициализированный объект";
					text = "Объект не был инициализирован\n" +
						$"\tв файле: '{file}'\n" +
						$"\tв методе: '{method}'";
#else
					text = "Программа не инициализировал нужный объект, который без этого программа дальше работать не будет.";
#endif
					AnotherException.SendUsualMessage<FileClass>(
						AnotherException.TypeError.BUG,
						AnotherException.ErrorBy.APPLICATION,
						this,
						title,
						text,
						TypeException.ToString()
					);
					break;
				case TypeInitException.ERR_INITIAL_FAILED:
#if DEBUG
					title = "Неинициализированный объект";
					text = "Не удалось инициализировать объект\n" +
						$"\tв файле: '{file}'\n" +
						$"\tв методе: '{method}'";
#else
					text = "Программе не смог инициализировать нужный объект.";
#endif
					AnotherException.SendUsualMessage<FileClass>(
						AnotherException.TypeError.SEVERE_BUG, 
						AnotherException.ErrorBy.APPLICATION, 
						this, 
						title, 
						text, 
						TypeException.ToString()
					);
					break;
				case TypeInitException.ERR_INITIAL_UNKNOWN:
				default:
#if DEBUG
					title = "Неизвестная ошибка";
					text = "Неизвестная ошибка, связанная с инициализацией объекта, которая присутствует\n" +
						$"\tв файле: '{file}'\n" +
						$"\tв методе: '{method}'\n" +
						"\tв блоке 'try'.";
#else
					text = "Мы не знаем, из-за чего вызвана ошибка после попытки инициализации объекта.";
#endif
					AnotherException.SendUsualMessage<FileClass>(
						AnotherException.TypeError.BUG,
						AnotherException.ErrorBy.APPLICATION,
						this,
						title,
						text,
						TypeException.ToString()
					);
					break;
			}
		}
	}
}
