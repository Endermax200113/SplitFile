using MaterialSkin.Controls;
using SplitFile.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SplitFile.GUI
{
	internal class ButtonFile : MaterialButton
	{
		internal FileInfo File { get; private set; }

		internal ButtonFile(
				string text,
				int idPanel,
				int idButton,
				FileInfo file
		) : base() {
			Anchor = AnchorStyles.Left | AnchorStyles.Right;
			Text = text;
			Margin = new Padding(4, 0, 4, 6);
			HighEmphasis = false;
			Icon = Properties.Resources.file;
			Name = $"ButtonSplit{idButton}OfPanel{idPanel}";
			DoubleBuffered = true;
			File = file;

			Click += AddClick;
		}

		private void AddClick(object sender, EventArgs e)
		{
			try {
				if (!ButtonException.CheckError<ButtonFile>(sender, e)) 
				{
					if (!HighEmphasis) {
						ButtonFile btn = PanelDirectory.SelectedButtonFile;

						if (btn != null)
							btn.HighEmphasis = false;

						PanelDirectory.SelectedButtonFile = (ButtonFile)sender;
						HighEmphasis = true;
						FormMain.ButtonFileSplit.Enabled = true;
					}
				}
			} 
			catch (ButtonException err) 
			{
				ButtonException.SendMessage(
						err, 
						nameof(ButtonFile), 
						nameof(AddClick), 
						((MaterialButton)sender).Name
				);
			}
		}
	}
}
