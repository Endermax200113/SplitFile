using MaterialSkin;
using MaterialSkin.Controls;

namespace SplitFile
{
	public partial class FormMain : MaterialForm
	{
		public FormMain()
		{
			InitializeComponent();

			MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
			materialSkinManager.AddFormToManage(this);
			materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
			materialSkinManager.ColorScheme = new ColorScheme(
					Primary.BlueGrey800, 
					Primary.BlueGrey900, 
					Primary.BlueGrey500, 
					Accent.LightBlue200, 
					TextShade.WHITE
			);

		}
	}
}
