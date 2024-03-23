namespace SplitFile
{
	partial class FormMain
	{
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
			this.TabMerge = new System.Windows.Forms.TabPage();
			this.TabSplit = new System.Windows.Forms.TabPage();
			this.PanelSplitFiles = new System.Windows.Forms.Panel();
			this.DividerSplitInfoAndControl = new MaterialSkin.Controls.MaterialDivider();
			this.PanelSplitInfoAndControl = new System.Windows.Forms.Panel();
			this.TableSplitHelperInfo = new System.Windows.Forms.TableLayoutPanel();
			this.LabelSplitCountFilesDone = new MaterialSkin.Controls.MaterialLabel();
			this.TableSplitHelperProgress = new System.Windows.Forms.TableLayoutPanel();
			this.LabelSplitForAllFile = new MaterialSkin.Controls.MaterialLabel();
			this.LabelSplitForOneFile = new MaterialSkin.Controls.MaterialLabel();
			this.ProgressSplitForOneFile = new MaterialSkin.Controls.MaterialProgressBar();
			this.ProgressSplitForAllFile = new MaterialSkin.Controls.MaterialProgressBar();
			this.ButtonSplitFile = new MaterialSkin.Controls.MaterialButton();
			this.ButtonSplitAddFile = new MaterialSkin.Controls.MaterialFloatingActionButton();
			this.DividerSplitPath = new MaterialSkin.Controls.MaterialDivider();
			this.TabsSidebar = new MaterialSkin.Controls.MaterialTabControl();
			this.TabSettings = new System.Windows.Forms.TabPage();
			this.ImagesIconsSidebar = new System.Windows.Forms.ImageList(this.components);
			this.PanelSplitPath = new System.Windows.Forms.FlowLayoutPanel();
			this.TabSplit.SuspendLayout();
			this.PanelSplitInfoAndControl.SuspendLayout();
			this.TableSplitHelperInfo.SuspendLayout();
			this.TableSplitHelperProgress.SuspendLayout();
			this.TabsSidebar.SuspendLayout();
			this.SuspendLayout();
			// 
			// TabMerge
			// 
			resources.ApplyResources(this.TabMerge, "TabMerge");
			this.TabMerge.Name = "TabMerge";
			this.TabMerge.UseVisualStyleBackColor = true;
			// 
			// TabSplit
			// 
			this.TabSplit.Controls.Add(this.PanelSplitPath);
			this.TabSplit.Controls.Add(this.PanelSplitFiles);
			this.TabSplit.Controls.Add(this.DividerSplitInfoAndControl);
			this.TabSplit.Controls.Add(this.PanelSplitInfoAndControl);
			this.TabSplit.Controls.Add(this.DividerSplitPath);
			resources.ApplyResources(this.TabSplit, "TabSplit");
			this.TabSplit.Name = "TabSplit";
			this.TabSplit.UseVisualStyleBackColor = true;
			// 
			// PanelSplitFiles
			// 
			resources.ApplyResources(this.PanelSplitFiles, "PanelSplitFiles");
			this.PanelSplitFiles.Name = "PanelSplitFiles";
			// 
			// DividerSplitInfoAndControl
			// 
			this.DividerSplitInfoAndControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.DividerSplitInfoAndControl.Depth = 0;
			resources.ApplyResources(this.DividerSplitInfoAndControl, "DividerSplitInfoAndControl");
			this.DividerSplitInfoAndControl.MouseState = MaterialSkin.MouseState.HOVER;
			this.DividerSplitInfoAndControl.Name = "DividerSplitInfoAndControl";
			// 
			// PanelSplitInfoAndControl
			// 
			this.PanelSplitInfoAndControl.Controls.Add(this.TableSplitHelperInfo);
			this.PanelSplitInfoAndControl.Controls.Add(this.ButtonSplitFile);
			this.PanelSplitInfoAndControl.Controls.Add(this.ButtonSplitAddFile);
			resources.ApplyResources(this.PanelSplitInfoAndControl, "PanelSplitInfoAndControl");
			this.PanelSplitInfoAndControl.Name = "PanelSplitInfoAndControl";
			// 
			// TableSplitHelperInfo
			// 
			resources.ApplyResources(this.TableSplitHelperInfo, "TableSplitHelperInfo");
			this.TableSplitHelperInfo.Controls.Add(this.LabelSplitCountFilesDone, 0, 0);
			this.TableSplitHelperInfo.Controls.Add(this.TableSplitHelperProgress, 1, 0);
			this.TableSplitHelperInfo.Name = "TableSplitHelperInfo";
			// 
			// LabelSplitCountFilesDone
			// 
			resources.ApplyResources(this.LabelSplitCountFilesDone, "LabelSplitCountFilesDone");
			this.LabelSplitCountFilesDone.Depth = 0;
			this.LabelSplitCountFilesDone.MouseState = MaterialSkin.MouseState.HOVER;
			this.LabelSplitCountFilesDone.Name = "LabelSplitCountFilesDone";
			// 
			// TableSplitHelperProgress
			// 
			resources.ApplyResources(this.TableSplitHelperProgress, "TableSplitHelperProgress");
			this.TableSplitHelperProgress.Controls.Add(this.LabelSplitForAllFile, 1, 1);
			this.TableSplitHelperProgress.Controls.Add(this.LabelSplitForOneFile, 1, 0);
			this.TableSplitHelperProgress.Controls.Add(this.ProgressSplitForOneFile, 0, 0);
			this.TableSplitHelperProgress.Controls.Add(this.ProgressSplitForAllFile, 0, 1);
			this.TableSplitHelperProgress.Name = "TableSplitHelperProgress";
			// 
			// LabelSplitForAllFile
			// 
			resources.ApplyResources(this.LabelSplitForAllFile, "LabelSplitForAllFile");
			this.LabelSplitForAllFile.Depth = 0;
			this.LabelSplitForAllFile.MouseState = MaterialSkin.MouseState.HOVER;
			this.LabelSplitForAllFile.Name = "LabelSplitForAllFile";
			// 
			// LabelSplitForOneFile
			// 
			resources.ApplyResources(this.LabelSplitForOneFile, "LabelSplitForOneFile");
			this.LabelSplitForOneFile.Depth = 0;
			this.LabelSplitForOneFile.MouseState = MaterialSkin.MouseState.HOVER;
			this.LabelSplitForOneFile.Name = "LabelSplitForOneFile";
			// 
			// ProgressSplitForOneFile
			// 
			resources.ApplyResources(this.ProgressSplitForOneFile, "ProgressSplitForOneFile");
			this.ProgressSplitForOneFile.Depth = 0;
			this.ProgressSplitForOneFile.MouseState = MaterialSkin.MouseState.HOVER;
			this.ProgressSplitForOneFile.Name = "ProgressSplitForOneFile";
			// 
			// ProgressSplitForAllFile
			// 
			resources.ApplyResources(this.ProgressSplitForAllFile, "ProgressSplitForAllFile");
			this.ProgressSplitForAllFile.Depth = 0;
			this.ProgressSplitForAllFile.MouseState = MaterialSkin.MouseState.HOVER;
			this.ProgressSplitForAllFile.Name = "ProgressSplitForAllFile";
			// 
			// ButtonSplitFile
			// 
			resources.ApplyResources(this.ButtonSplitFile, "ButtonSplitFile");
			this.ButtonSplitFile.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
			this.ButtonSplitFile.Depth = 0;
			this.ButtonSplitFile.HighEmphasis = true;
			this.ButtonSplitFile.Icon = null;
			this.ButtonSplitFile.MouseState = MaterialSkin.MouseState.HOVER;
			this.ButtonSplitFile.Name = "ButtonSplitFile";
			this.ButtonSplitFile.NoAccentTextColor = System.Drawing.Color.Empty;
			this.ButtonSplitFile.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
			this.ButtonSplitFile.UseAccentColor = false;
			this.ButtonSplitFile.UseVisualStyleBackColor = true;
			// 
			// ButtonSplitAddFile
			// 
			resources.ApplyResources(this.ButtonSplitAddFile, "ButtonSplitAddFile");
			this.ButtonSplitAddFile.Depth = 0;
			this.ButtonSplitAddFile.Icon = global::SplitFile.Properties.Resources.file_white;
			this.ButtonSplitAddFile.MouseState = MaterialSkin.MouseState.HOVER;
			this.ButtonSplitAddFile.Name = "ButtonSplitAddFile";
			this.ButtonSplitAddFile.UseVisualStyleBackColor = true;
			// 
			// DividerSplitPath
			// 
			resources.ApplyResources(this.DividerSplitPath, "DividerSplitPath");
			this.DividerSplitPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.DividerSplitPath.Depth = 0;
			this.DividerSplitPath.MouseState = MaterialSkin.MouseState.HOVER;
			this.DividerSplitPath.Name = "DividerSplitPath";
			// 
			// TabsSidebar
			// 
			this.TabsSidebar.Controls.Add(this.TabSplit);
			this.TabsSidebar.Controls.Add(this.TabMerge);
			this.TabsSidebar.Controls.Add(this.TabSettings);
			this.TabsSidebar.Depth = 0;
			resources.ApplyResources(this.TabsSidebar, "TabsSidebar");
			this.TabsSidebar.ImageList = this.ImagesIconsSidebar;
			this.TabsSidebar.MouseState = MaterialSkin.MouseState.HOVER;
			this.TabsSidebar.Multiline = true;
			this.TabsSidebar.Name = "TabsSidebar";
			this.TabsSidebar.SelectedIndex = 0;
			// 
			// TabSettings
			// 
			resources.ApplyResources(this.TabSettings, "TabSettings");
			this.TabSettings.Name = "TabSettings";
			this.TabSettings.UseVisualStyleBackColor = true;
			// 
			// ImagesIconsSidebar
			// 
			this.ImagesIconsSidebar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImagesIconsSidebar.ImageStream")));
			this.ImagesIconsSidebar.TransparentColor = System.Drawing.Color.Transparent;
			this.ImagesIconsSidebar.Images.SetKeyName(0, "split.png");
			this.ImagesIconsSidebar.Images.SetKeyName(1, "merge.png");
			this.ImagesIconsSidebar.Images.SetKeyName(2, "settings.png");
			// 
			// PanelSplitPath
			// 
			resources.ApplyResources(this.PanelSplitPath, "PanelSplitPath");
			this.PanelSplitPath.Name = "PanelSplitPath";
			// 
			// FormMain
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.TabsSidebar);
			this.DrawerShowIconsWhenHidden = true;
			this.DrawerTabControl = this.TabsSidebar;
			this.Name = "FormMain";
			this.Load += new System.EventHandler(this.FormMain_Load);
			this.TabSplit.ResumeLayout(false);
			this.PanelSplitInfoAndControl.ResumeLayout(false);
			this.PanelSplitInfoAndControl.PerformLayout();
			this.TableSplitHelperInfo.ResumeLayout(false);
			this.TableSplitHelperInfo.PerformLayout();
			this.TableSplitHelperProgress.ResumeLayout(false);
			this.TableSplitHelperProgress.PerformLayout();
			this.TabsSidebar.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabPage TabMerge;
		private System.Windows.Forms.TabPage TabSplit;
		private MaterialSkin.Controls.MaterialTabControl TabsSidebar;
		private System.Windows.Forms.ImageList ImagesIconsSidebar;
		private System.Windows.Forms.TabPage TabSettings;
		private MaterialSkin.Controls.MaterialDivider DividerSplitPath;
		private System.Windows.Forms.Panel PanelSplitInfoAndControl;
		private MaterialSkin.Controls.MaterialFloatingActionButton ButtonSplitAddFile;
		private MaterialSkin.Controls.MaterialButton ButtonSplitFile;
		private System.Windows.Forms.TableLayoutPanel TableSplitHelperInfo;
		private MaterialSkin.Controls.MaterialLabel LabelSplitCountFilesDone;
		private System.Windows.Forms.TableLayoutPanel TableSplitHelperProgress;
		private MaterialSkin.Controls.MaterialProgressBar ProgressSplitForOneFile;
		private MaterialSkin.Controls.MaterialProgressBar ProgressSplitForAllFile;
		private MaterialSkin.Controls.MaterialLabel LabelSplitForOneFile;
		private MaterialSkin.Controls.MaterialLabel LabelSplitForAllFile;
		private MaterialSkin.Controls.MaterialDivider DividerSplitInfoAndControl;
		private System.Windows.Forms.Panel PanelSplitFiles;
		private System.Windows.Forms.FlowLayoutPanel PanelSplitPath;
	}
}

