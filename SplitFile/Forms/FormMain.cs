﻿using MaterialSkin;
using MaterialSkin.Controls;
using SplitFile.Exceptions;
using SplitFile.GUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SplitFile
{
	public partial class FormMain : MaterialForm
	{
		/*private readonly MaterialSkinManager _materialSkinManager = MaterialSkinManager.Instance;
		private readonly List<int> _listValuesOfPrim1 = new List<int>();
		private readonly List<int> _listValuesOfPrim2 = new List<int>();
		private readonly List<int> _listValuesOfPrim3 = new List<int>();
		private readonly List<int> _listValuesOfAccent = new List<int>();
		private readonly List<int> _listValuesOfText = new List<int>();
		private Primary _primary = Primary.Indigo500;
		private Primary _darkPrimary = Primary.Indigo700;
		private Primary _lightPrimary = Primary.Indigo700;
		private Accent _accent = Accent.Pink700;
		private TextShade _textShade = TextShade.WHITE;

		public FormMain()
		{
			InitializeComponent();

			_materialSkinManager.AddFormToManage(this);
			_materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
			ResetColorScheme();

			string[] valEnumPrim = Enum.GetNames(typeof(Primary));
			List<string> listEnumPrim = valEnumPrim.ToList();
			listEnumPrim.Sort();

			foreach (string value in listEnumPrim) {
				int valueNumber = (int)Enum.Parse(typeof(Primary), value);

				ComboTestChange1.Items.Add(value);
				_listValuesOfPrim1.Add(valueNumber);
				ComboTestChange2.Items.Add(value);
				_listValuesOfPrim2.Add(valueNumber);
				ComboTestChange3.Items.Add(value);
				_listValuesOfPrim3.Add(valueNumber);
			}

			string[] valEnumAccent = Enum.GetNames(typeof(Accent));
			List<string> listEnumAccent = valEnumAccent.ToList();
			listEnumAccent.Sort();

			foreach (string value in listEnumAccent)
			{
				int valueNumber = (int)Enum.Parse(typeof(Accent), value);

				ComboTestChange4.Items.Add(value);
				_listValuesOfAccent.Add(valueNumber);
			}

			string[] valEnumText = Enum.GetNames(typeof(TextShade));
			List<string> listEnumText = valEnumText.ToList();
			listEnumText.Sort();

            foreach (string value in listEnumText)
            {
				int valueNumber = (int)Enum.Parse(typeof(TextShade), value);

				ComboTestChange5.Items.Add(value);
				_listValuesOfText.Add(valueNumber);
			}
        }

		private void ResetColorScheme()
		{
			_materialSkinManager.ColorScheme = new ColorScheme(
					_primary,
					_darkPrimary,
					_lightPrimary,
					_accent,
					_textShade
			);
		}

		private void ComboTestChange1_SelectedIndexChanged(object sender, EventArgs e)
		{
			int value = ComboTestChange1.SelectedIndex;
			_primary = (Primary)_listValuesOfPrim1[value];
			ResetColorScheme();
		}

		private void ComboTestChange2_SelectedIndexChanged(object sender, EventArgs e)
		{
			int value = ComboTestChange2.SelectedIndex;
			_darkPrimary = (Primary)_listValuesOfPrim2[value];
			ResetColorScheme();
		}

		private void ComboTestChange3_SelectedIndexChanged(object sender, EventArgs e)
		{
			int value = ComboTestChange3.SelectedIndex;
			_lightPrimary = (Primary)_listValuesOfPrim3[value];
			ResetColorScheme();
		}

		private void ComboTestChange4_SelectedIndexChanged(object sender, EventArgs e)
		{
			int value = ComboTestChange4.SelectedIndex;
			_accent = (Accent)_listValuesOfAccent[value];
			ResetColorScheme();
		}

		private void ComboTestChange5_SelectedIndexChanged(object sender, EventArgs e)
		{
			int value = ComboTestChange5.SelectedIndex;
			_textShade = (TextShade)_listValuesOfText[value];
			ResetColorScheme();
		}

		private void ComboTestChange6_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ComboTestChange6.SelectedIndex == 0) {
				_materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
			} else {
				_materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
			}
		}*/

		public static int IdPanel { get; set; } = 0;
		internal static MaterialButton ButtonSplit { get; private set; }

		public FormMain() {
			InitializeComponent();

			MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
			materialSkinManager.AddFormToManage(this);
			materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
			materialSkinManager.ColorScheme = new ColorScheme(
					Primary.Indigo500,
					Primary.Indigo700,
					Primary.Indigo800,
					Accent.Pink700,
					TextShade.WHITE
			);

			ButtonSplit = ButtonSplitFile;
		}

		private void AddFirstPanel() {
			PanelDirectory panel = new PanelDirectory(PanelSplitFiles, PanelSplitPath, IdPanel, null, true);

			PanelSplitFiles.Controls.Add(panel);
			IdPanel++;
		}

		private void FormMain_Load(object sender, EventArgs e)
		{
			AddFirstPanel();
		}
	}
}
