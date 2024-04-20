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
	public sealed class AnotherException : Exception
	{
		public enum TypeError
		{
			BUG,
			SEVERE_BUG,
			CRASH
		}

		public enum ErrorBy
		{
			Application,
			User
		}

		public AnotherException(string message) : base(message)
		{
			Log.Error(message);
		}

		public static void SendMessage(Exception err, string title, string text)
		{
			Log.Info("Sending an error message to the user...");

			MessageBoxButtons btn = MessageBoxButtons.OK;
			FlexibleMaterialForm.ButtonsPosition positionBtn = FlexibleMaterialForm.ButtonsPosition.Right;

			Log.Error(err);
			MaterialMessageBox.Show(text, title, btn, positionBtn);
			FormMain.CloseProgram();
		}

		public static void SendUsualMessage(TypeError type, ErrorBy by, Exception err, string titleErr, string textErr, string typeErr)
		{
			Log.Info("Sending an error message to the user...");

			MessageBoxButtons btn = MessageBoxButtons.OK;
			FlexibleMaterialForm.ButtonsPosition positionBtn = FlexibleMaterialForm.ButtonsPosition.Right;

			string title;
			string text;

#if DEBUG
			title = titleErr;
			text = (type == TypeError.BUG ? 
					"Ошибка" : 
				type == TypeError.SEVERE_BUG ? 
					"Серьёзная ошибка" : 
					"Сбой программы") +
				" со стороны " +
				(by == ErrorBy.Application ?
					"программы" :
					"пользователя") +
				". Сообщение для разработчика:\n" +
				$"{textErr}\n\n" +
				"Журнал записи:\n" +
				$"{Log.GetPathFile()}\n\n" +
				"Стек ошибки:\n" +
				$"{err}";
#else
			title = type == TypeError.BUG ?
					"Программная ошибка" : 
				type == TypeError.SEVERE_BUG ? 
					"Критическая ошибка" : 
					"Сбой программы";
			text = (type == TypeError.BUG ?
					"Эта ошибка вызвана" :
				type == TypeError.SEVERE_BUG ?
					"Эта серьёзная ошибка вызвана" :
					"Этот сбой программы вызван") +
				" " +
				(by == ErrorBy.Application ?
					"не из-за Вас." :
					"с Вашей стороны.") +
				$"\n{textErr} Это " +
				(type == TypeError.BUG ?
					"баг" :
				type == TypeError.SEVERE_BUG ?
					"серьёзный баг" :
					"сбой программы") +
				$".\nЕсли Вы видете эту ошибку, пожалуйста," +
				(type == TypeError.BUG ?
					"" :
				type == TypeError.SEVERE_BUG ?
					"не поленитесь,":
					"немедленно") +
				" напишите об этом по ссылке ниже:\n" +
				"https://github.com/Endermax200113/SplitFile/issues/new\n" +
				"Прикрепите этот файл, когда Вы будете писать:\n" +
				$"{Log.GetPathFile()}\n\n" +
				$"Ошибка: {typeErr}\n\n" +
				"Программа будет закрыта после нажатии кнопки 'ОК'";
#endif

			Log.Error(err);
			MaterialMessageBox.Show(text, title, btn, positionBtn);
			FormMain.CloseProgram();
		}
	}
}
