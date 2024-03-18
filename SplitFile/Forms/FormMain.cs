using MaterialSkin;
using MaterialSkin.Controls;
using SplitFile.Exceptions;
using System;
using System.Collections.Generic;
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

		private List<string> _listDrives = new List<string>();

		private int IdPanel { get; set; } = 0;

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
		}

		private void LoadLogicalDrives() {
			foreach (DriveInfo drive in DriveInfo.GetDrives())
				if (drive.IsReady)
					_listDrives.Add($"{drive.Name} {drive.VolumeLabel}");
		}

		private void AddFirstPanel() {
			FlowLayoutPanel panel = new FlowLayoutPanel()
			{
				Dock = DockStyle.Left,
				FlowDirection = FlowDirection.TopDown,
				Margin = new Padding(0),
				AutoSize = true,
				WrapContents = false,
				Name = $"PanelSplitFiles{IdPanel}"
			};

			panel.HorizontalScroll.Maximum = 0;
			panel.AutoScroll = false;
			panel.VerticalScroll.Visible = true;
			panel.AutoScroll = true;

			PanelSplitFiles.Controls.Add(panel);

			AddFirstButtons(panel);
			IdPanel++;
		}

		private void AddFirstButtons(FlowLayoutPanel panel) {
			bool first = true;
			int idButton = 0;

            foreach (string nameDrive in _listDrives)
            {
				MaterialButton btn = new MaterialButton()
				{
					Anchor = AnchorStyles.Left | AnchorStyles.Right,
					Text = nameDrive,
					Margin = first ? new Padding(4, 6, 4, 6) : new Padding(4, 3, 4, 6),
					HighEmphasis = false,
					Icon = Properties.Resources.folder,
					Name = $"ButtonSplit{idButton++}OfPanel{IdPanel}"
				};

				if (first)
					first = false;

				btn.Click += ButtonFiles_Click;

				panel.Controls.Add(btn);
            }
        }

		private void ButtonFiles_Click(object sender, EventArgs e) {
			try {
				if (sender is null)
					throw new ButtonException(ButtonException.TypeButtonException.ERR_BUTTON_NOT_EXISTS);
				else if (!(sender is MaterialButton))
					throw new ButtonException(ButtonException.TypeButtonException.ERR_BUTTON_NOT_BELONG);


			} catch (ButtonException err) {
				string title;
				string text;
				MessageBoxButtons btn = MessageBoxButtons.OK;
				FlexibleMaterialForm.ButtonsPosition positionBtn = FlexibleMaterialForm.ButtonsPosition.Right;

				Console.WriteLine(err);

				switch (err.TypeException) {
					case ButtonException.TypeButtonException.ERR_BUTTON_NOT_EXISTS:
#if DEBUG
						title = "Несуществующая кнопка";
						text = "Ошибка со стороны программы. Сообщение для разработчика:\n" +
							"Несуществующий объект присутствует\n" +
							"\tв файле \'FormMain.cs\'\n" +
							"\tв методе \'ButtonFiles_Click(object, EventArgs)\'.\n\n" +
							"Стек ошибки:\n" +
							$"{err}";
#else
						title = "Программная ошибка";
						text = "Эта ошибка вызвана не из-за Вас.\n" +
							"Программа посчитала, что кнопка - это пустой объект, что на самом деле это не так. Это баг.\n" +
							"Если Вы видете эту ошибку, пожалуйста, напишите об этом по ссылке ниже:\n" +
							"https://github.com/Endermax200113/SplitFile/issues/new\n\n" +
							$"Ошибка: {err.TypeException}\n\n" +
							"Программа будет закрыта после нажатии кнопки \'ОК\'";
#endif
						break;
					case ButtonException.TypeButtonException.ERR_BUTTON_NOT_BELONG:
#if DEBUG
						title = "Объект не является кнопкой";
						text = "Ошибка со стороны программы. Сообщение для разработчика:\n" +
							"Объект, которая не является кнопкой, присутствует\n" +
							"\tв файле \'FormMain.cs\'\n" +
							"\tв методе \'ButtonFiles_Click(object, EventArgs)\'.\n\n" +
							"Стек ошибки:\n" +
							$"{err}";
#else
						title = "Программная ошибка";
						text = "Эта ошибка вызвана не из-за Вас.\n" +
							"Программа посчитала, что этот объект, на которой Вы нажали, не является кнопкой. Это баг.\n" +
							"Если Вы видете эту ошибку, пожалуйста, напишите об этом по ссылке ниже:\n" +
							"https://github.com/Endermax200113/SplitFile/issues/new\n\n" +
							$"Ошибка: {err.TypeException}\n\n" +
							"Программа будет закрыта после нажатии кнопки \'ОК\'";
#endif
						break;
					case ButtonException.TypeButtonException.ERR_BUTTON_UNKNOWN:
					default:
#if DEBUG
						title = "Неизвестная ошибка";
						text = "Ошибка со стороны программы. Сообщение для разработчика:\n" +
							"Неизвестная ошибка, которая присутствует\n" +
							"\tв файле \'FormMain.cs\'\n" +
							"\tв методе \'ButtonFiles_Click(object, EventArgs)\'\n" +
							"\tв блоке try.\n\n" + 
							"Стек ошибки:\n" +
							$"{err}";
#else
						title = "Неизвестная программная ошибка";
						text = "Эта ошибка вызвана не из-за Вас.\n" +
							"Мы не знаем, из-за чего вызвана ошибка после клика кнопки.\n" +
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
			}
		}

		private void FormMain_Load(object sender, EventArgs e)
		{
			LoadLogicalDrives();
			AddFirstPanel();
			ButtonFiles_Click(null, null);
		}
	}
}
