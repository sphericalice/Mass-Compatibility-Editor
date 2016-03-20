namespace MassCompatibilityEditor
{
    partial class frmHome
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupMassRemove = new System.Windows.Forms.GroupBox();
            this.lblSelectedMoveRemove = new System.Windows.Forms.Label();
            this.numPokemonMax = new System.Windows.Forms.NumericUpDown();
            this.btnRemoveCompatibility = new System.Windows.Forms.Button();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblFrom = new System.Windows.Forms.Label();
            this.lblPokemonRemove = new System.Windows.Forms.Label();
            this.numPokemonMin = new System.Windows.Forms.NumericUpDown();
            this.lblTMNumberRemove = new System.Windows.Forms.Label();
            this.numTMNumberRemove = new System.Windows.Forms.NumericUpDown();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLoadRom = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuChangeMode = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTM = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHM = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTutor = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupMassAdd = new System.Windows.Forms.GroupBox();
            this.lblSelectedMoveAdd = new System.Windows.Forms.Label();
            this.btnLoadSpeciesFile = new System.Windows.Forms.Button();
            this.btnAddCompatibility = new System.Windows.Forms.Button();
            this.lblTMNumberAdd = new System.Windows.Forms.Label();
            this.numTMNumberAdd = new System.Windows.Forms.NumericUpDown();
            this.groupMassRemove.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPokemonMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPokemonMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTMNumberRemove)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.groupMassAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTMNumberAdd)).BeginInit();
            this.SuspendLayout();
            // 
            // groupMassRemove
            // 
            this.groupMassRemove.Controls.Add(this.lblSelectedMoveRemove);
            this.groupMassRemove.Controls.Add(this.numPokemonMax);
            this.groupMassRemove.Controls.Add(this.btnRemoveCompatibility);
            this.groupMassRemove.Controls.Add(this.lblTo);
            this.groupMassRemove.Controls.Add(this.lblFrom);
            this.groupMassRemove.Controls.Add(this.lblPokemonRemove);
            this.groupMassRemove.Controls.Add(this.numPokemonMin);
            this.groupMassRemove.Controls.Add(this.lblTMNumberRemove);
            this.groupMassRemove.Controls.Add(this.numTMNumberRemove);
            this.groupMassRemove.Location = new System.Drawing.Point(12, 26);
            this.groupMassRemove.Name = "groupMassRemove";
            this.groupMassRemove.Size = new System.Drawing.Size(260, 122);
            this.groupMassRemove.TabIndex = 0;
            this.groupMassRemove.TabStop = false;
            this.groupMassRemove.Text = "Mass-remove compatibility";
            // 
            // lblSelectedMoveRemove
            // 
            this.lblSelectedMoveRemove.AutoSize = true;
            this.lblSelectedMoveRemove.Location = new System.Drawing.Point(9, 73);
            this.lblSelectedMoveRemove.Name = "lblSelectedMoveRemove";
            this.lblSelectedMoveRemove.Size = new System.Drawing.Size(87, 13);
            this.lblSelectedMoveRemove.TabIndex = 10;
            this.lblSelectedMoveRemove.Text = "No ROM loaded.";
            // 
            // numPokemonMax
            // 
            this.numPokemonMax.Location = new System.Drawing.Point(201, 40);
            this.numPokemonMax.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numPokemonMax.Name = "numPokemonMax";
            this.numPokemonMax.Size = new System.Drawing.Size(52, 20);
            this.numPokemonMax.TabIndex = 9;
            this.numPokemonMax.Value = new decimal(new int[] {
            411,
            0,
            0,
            0});
            this.numPokemonMax.ValueChanged += new System.EventHandler(this.numPokemonMax_ValueChanged);
            // 
            // btnRemoveCompatibility
            // 
            this.btnRemoveCompatibility.Location = new System.Drawing.Point(12, 92);
            this.btnRemoveCompatibility.Name = "btnRemoveCompatibility";
            this.btnRemoveCompatibility.Size = new System.Drawing.Size(242, 23);
            this.btnRemoveCompatibility.TabIndex = 8;
            this.btnRemoveCompatibility.Text = "&Remove compatibility";
            this.btnRemoveCompatibility.UseVisualStyleBackColor = true;
            this.btnRemoveCompatibility.Click += new System.EventHandler(this.btnRemoveCompatibility_Click);
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(180, 42);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(16, 13);
            this.lblTo.TabIndex = 6;
            this.lblTo.Text = "to";
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new System.Drawing.Point(86, 42);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(30, 13);
            this.lblFrom.TabIndex = 5;
            this.lblFrom.Text = "From";
            // 
            // lblPokemonRemove
            // 
            this.lblPokemonRemove.AutoSize = true;
            this.lblPokemonRemove.Location = new System.Drawing.Point(74, 24);
            this.lblPokemonRemove.Name = "lblPokemonRemove";
            this.lblPokemonRemove.Size = new System.Drawing.Size(55, 13);
            this.lblPokemonRemove.TabIndex = 3;
            this.lblPokemonRemove.Text = "Pokémon:";
            // 
            // numPokemonMin
            // 
            this.numPokemonMin.Location = new System.Drawing.Point(122, 40);
            this.numPokemonMin.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numPokemonMin.Name = "numPokemonMin";
            this.numPokemonMin.Size = new System.Drawing.Size(52, 20);
            this.numPokemonMin.TabIndex = 2;
            this.numPokemonMin.ValueChanged += new System.EventHandler(this.numPokemonMin_ValueChanged);
            // 
            // lblTMNumberRemove
            // 
            this.lblTMNumberRemove.AutoSize = true;
            this.lblTMNumberRemove.Location = new System.Drawing.Point(8, 24);
            this.lblTMNumberRemove.Name = "lblTMNumberRemove";
            this.lblTMNumberRemove.Size = new System.Drawing.Size(36, 13);
            this.lblTMNumberRemove.TabIndex = 1;
            this.lblTMNumberRemove.Text = "TM #:";
            // 
            // numTMNumberRemove
            // 
            this.numTMNumberRemove.Location = new System.Drawing.Point(27, 40);
            this.numTMNumberRemove.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numTMNumberRemove.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTMNumberRemove.Name = "numTMNumberRemove";
            this.numTMNumberRemove.Size = new System.Drawing.Size(40, 20);
            this.numTMNumberRemove.TabIndex = 0;
            this.numTMNumberRemove.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTMNumberRemove.ValueChanged += new System.EventHandler(this.numTMNumberRemove_ValueChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuChangeMode});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(549, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuLoadRom,
            this.menuExit});
            this.menuFile.Name = "menuFile";
            this.menuFile.ShortcutKeyDisplayString = "";
            this.menuFile.Size = new System.Drawing.Size(46, 20);
            this.menuFile.Text = "&File...";
            // 
            // menuLoadRom
            // 
            this.menuLoadRom.Name = "menuLoadRom";
            this.menuLoadRom.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuLoadRom.Size = new System.Drawing.Size(173, 22);
            this.menuLoadRom.Text = "&Load ROM";
            this.menuLoadRom.Click += new System.EventHandler(this.menuLoadROM_Click);
            // 
            // menuExit
            // 
            this.menuExit.Name = "menuExit";
            this.menuExit.Size = new System.Drawing.Size(173, 22);
            this.menuExit.Text = "E&xit";
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // menuChangeMode
            // 
            this.menuChangeMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuTM,
            this.menuHM,
            this.menuTutor});
            this.menuChangeMode.Name = "menuChangeMode";
            this.menuChangeMode.Size = new System.Drawing.Size(84, 20);
            this.menuChangeMode.Text = "&Editor Mode";
            // 
            // menuTM
            // 
            this.menuTM.Checked = true;
            this.menuTM.CheckOnClick = true;
            this.menuTM.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuTM.Name = "menuTM";
            this.menuTM.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.menuTM.Size = new System.Drawing.Size(181, 22);
            this.menuTM.Text = "&TMs";
            this.menuTM.Click += new System.EventHandler(this.menuTM_Click);
            // 
            // menuHM
            // 
            this.menuHM.CheckOnClick = true;
            this.menuHM.Name = "menuHM";
            this.menuHM.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.menuHM.Size = new System.Drawing.Size(181, 22);
            this.menuHM.Text = "&HMs";
            this.menuHM.Click += new System.EventHandler(this.menuHM_Click);
            // 
            // menuTutor
            // 
            this.menuTutor.CheckOnClick = true;
            this.menuTutor.Name = "menuTutor";
            this.menuTutor.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.menuTutor.Size = new System.Drawing.Size(181, 22);
            this.menuTutor.Text = "&Move Tutor";
            this.menuTutor.Click += new System.EventHandler(this.menuTutor_Click);
            // 
            // groupMassAdd
            // 
            this.groupMassAdd.Controls.Add(this.lblSelectedMoveAdd);
            this.groupMassAdd.Controls.Add(this.btnLoadSpeciesFile);
            this.groupMassAdd.Controls.Add(this.btnAddCompatibility);
            this.groupMassAdd.Controls.Add(this.lblTMNumberAdd);
            this.groupMassAdd.Controls.Add(this.numTMNumberAdd);
            this.groupMassAdd.Location = new System.Drawing.Point(278, 26);
            this.groupMassAdd.Name = "groupMassAdd";
            this.groupMassAdd.Size = new System.Drawing.Size(260, 122);
            this.groupMassAdd.TabIndex = 10;
            this.groupMassAdd.TabStop = false;
            this.groupMassAdd.Text = "Mass-add compatibility";
            // 
            // lblSelectedMoveAdd
            // 
            this.lblSelectedMoveAdd.AutoSize = true;
            this.lblSelectedMoveAdd.Location = new System.Drawing.Point(6, 73);
            this.lblSelectedMoveAdd.Name = "lblSelectedMoveAdd";
            this.lblSelectedMoveAdd.Size = new System.Drawing.Size(87, 13);
            this.lblSelectedMoveAdd.TabIndex = 11;
            this.lblSelectedMoveAdd.Text = "No ROM loaded.";
            // 
            // btnLoadSpeciesFile
            // 
            this.btnLoadSpeciesFile.Location = new System.Drawing.Point(73, 26);
            this.btnLoadSpeciesFile.Name = "btnLoadSpeciesFile";
            this.btnLoadSpeciesFile.Size = new System.Drawing.Size(179, 34);
            this.btnLoadSpeciesFile.TabIndex = 9;
            this.btnLoadSpeciesFile.Text = "&Load species file";
            this.btnLoadSpeciesFile.UseVisualStyleBackColor = true;
            this.btnLoadSpeciesFile.Click += new System.EventHandler(this.btnLoadSpeciesFile_Click);
            // 
            // btnAddCompatibility
            // 
            this.btnAddCompatibility.Enabled = false;
            this.btnAddCompatibility.Location = new System.Drawing.Point(11, 92);
            this.btnAddCompatibility.Name = "btnAddCompatibility";
            this.btnAddCompatibility.Size = new System.Drawing.Size(242, 23);
            this.btnAddCompatibility.TabIndex = 8;
            this.btnAddCompatibility.Text = "&Add compatibility";
            this.btnAddCompatibility.UseVisualStyleBackColor = true;
            this.btnAddCompatibility.Click += new System.EventHandler(this.btnAddCompatibility_Click);
            // 
            // lblTMNumberAdd
            // 
            this.lblTMNumberAdd.AutoSize = true;
            this.lblTMNumberAdd.Location = new System.Drawing.Point(8, 24);
            this.lblTMNumberAdd.Name = "lblTMNumberAdd";
            this.lblTMNumberAdd.Size = new System.Drawing.Size(36, 13);
            this.lblTMNumberAdd.TabIndex = 1;
            this.lblTMNumberAdd.Text = "TM #:";
            // 
            // numTMNumberAdd
            // 
            this.numTMNumberAdd.Location = new System.Drawing.Point(27, 40);
            this.numTMNumberAdd.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numTMNumberAdd.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTMNumberAdd.Name = "numTMNumberAdd";
            this.numTMNumberAdd.Size = new System.Drawing.Size(40, 20);
            this.numTMNumberAdd.TabIndex = 0;
            this.numTMNumberAdd.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTMNumberAdd.ValueChanged += new System.EventHandler(this.numTMNumberAdd_ValueChanged);
            // 
            // frmHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 157);
            this.Controls.Add(this.groupMassAdd);
            this.Controls.Add(this.groupMassRemove);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmHome";
            this.Text = "Mass Compatibility Editor";
            this.Load += new System.EventHandler(this.frmHome_Load);
            this.groupMassRemove.ResumeLayout(false);
            this.groupMassRemove.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPokemonMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPokemonMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTMNumberRemove)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupMassAdd.ResumeLayout(false);
            this.groupMassAdd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTMNumberAdd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupMassRemove;
        private System.Windows.Forms.Button btnRemoveCompatibility;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Label lblPokemonRemove;
        private System.Windows.Forms.NumericUpDown numPokemonMin;
        private System.Windows.Forms.Label lblTMNumberRemove;
        private System.Windows.Forms.NumericUpDown numTMNumberRemove;
        private System.Windows.Forms.NumericUpDown numPokemonMax;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuLoadRom;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.GroupBox groupMassAdd;
        private System.Windows.Forms.Button btnLoadSpeciesFile;
        private System.Windows.Forms.Button btnAddCompatibility;
        private System.Windows.Forms.Label lblTMNumberAdd;
        private System.Windows.Forms.NumericUpDown numTMNumberAdd;
        private System.Windows.Forms.ToolStripMenuItem menuChangeMode;
        private System.Windows.Forms.ToolStripMenuItem menuTM;
        private System.Windows.Forms.ToolStripMenuItem menuHM;
        private System.Windows.Forms.ToolStripMenuItem menuTutor;
        private System.Windows.Forms.Label lblSelectedMoveRemove;
        private System.Windows.Forms.Label lblSelectedMoveAdd;
    }
}

