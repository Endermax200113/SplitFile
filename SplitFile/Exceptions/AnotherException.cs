using MaterialSkin.Controls;
using SplitFile.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
			APPLICATION,
			USER
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

			MessageBoxButtons btn = MessageBoxButtons.OKCancel;
			FlexibleMaterialForm.ButtonsPosition positionBtn = FlexibleMaterialForm.ButtonsPosition.Right;

			string title;
			string text;
			string pathFile = Log.GetPathFile();

#if DEBUG
			title = titleErr;
			text = (type == TypeError.BUG ? 
					"Ошибка" : 
				type == TypeError.SEVERE_BUG ? 
					"Серьёзная ошибка" : 
					"Сбой программы") +
				" со стороны " +
				(by == ErrorBy.APPLICATION ?
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
				(by == ErrorBy.APPLICATION ?
					"не из-за Вас." :
					"с Вашей стороны.") +
				$"\n{textErr} Это " +
				(type == TypeError.BUG ?
					"баг" :
				type == TypeError.SEVERE_BUG ?
					"серьёзный баг" :
					"сбой программы") +
				$".\n\nЕсли Вы видете эту ошибку, пожалуйста," +
				(type == TypeError.BUG ?
					"" :
				type == TypeError.SEVERE_BUG ?
					" не поленитесь,":
					" немедленно") +
				" напишите об этом по ссылке ниже:\n" +
				"https://github.com/Endermax200113/SplitFile/issues/new\n" +
				"Прикрепите этот файл, когда Вы будете писать:\n" +
				$"{pathFile}\n\n" +
				$"Ошибка: {typeErr}\n\n" +
				"Нажмите 'OK', чтобы открыть журнал и закрыть программу.\n" +
				"Нажите 'Cancel', чтобы закрыть программу.";
#endif

			Log.Error(err);
			
			DialogResult dlg = MaterialMessageBox.Show(text, title, btn, positionBtn);

			if (dlg == DialogResult.OK)
			{
				var process = new Process();
				var startInfo = new ProcessStartInfo
				{
					WindowStyle = ProcessWindowStyle.Hidden,
					FileName = "cmd.exe",
					Arguments = $"/C explorer /select, {pathFile}"
				};

				process.StartInfo = startInfo;
				process.Start();
			}

			FormMain.CloseProgram();
		}
	}
}
