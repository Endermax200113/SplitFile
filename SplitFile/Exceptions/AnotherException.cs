using MaterialSkin.Controls;
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
		public AnotherException(string message) : base(message) { }

		public static void SendMessage(Exception err, string title, string text)
		{
			MessageBoxButtons btn = MessageBoxButtons.OK;
			FlexibleMaterialForm.ButtonsPosition positionBtn = FlexibleMaterialForm.ButtonsPosition.Right;

			Console.WriteLine(err);
			MaterialMessageBox.Show(text, title, btn, positionBtn);
			FormMain.CloseProgram();
		}
	}
}
