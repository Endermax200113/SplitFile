using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using MaterialSkin.Controls;
using SplitFile.Exceptions;

namespace SplitFile.GUI
{
	internal class ButtonPath : MaterialButton
	{
		internal static List<ButtonPath> ListButtons { get; private set; } = new List<ButtonPath>();
		private int IdButton { get; set; }

		internal ButtonPath(
				string text, 
				int idButton
		) : base()
		{
			Text = text;
			Name = $"ButtonSplitPath{idButton}";
			IdButton = idButton;
			DoubleBuffered = true;

			Init();
		}

		private void Init()
		{
			ListButtons.Add(this);

			Click += AddClick;
		}

		private void AddClick(object sender, EventArgs e)
		{
			try
			{
				if (!ButtonException.CheckError<ButtonPath>(sender, e))
				{
					if (IdButton + 1 < FormMain.IdPanel)
					{
						int count = ListButtons.Count - 1;
						List<PanelDirectory> listPanels = PanelDirectory.ListPanels;

						while (IdButton < count)
						{
							PanelDirectory panel = listPanels[count];
							panel.Remove();
							listPanels.RemoveAt(count);

                            ButtonPath btn = ListButtons[count];
							btn.Dispose();
							ListButtons.RemoveAt(count);

							count--;
						}

						ButtonDirectory btnDir = listPanels[count].SelectedButtonDir;

						if (btnDir != null)
						{
							btnDir.UseAccentColor = false;
							btnDir.HighEmphasis = false;
						}
					}
				}
			}
			catch (ButtonException err)
			{
				ButtonException.SendMessage(
						err, 
						nameof(ButtonPath), 
						nameof(AddClick), 
						((MaterialButton)sender).Name
				);
			}
		}
	}
}
