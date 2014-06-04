namespace ProjectX
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pbSpielbrett = new System.Windows.Forms.PictureBox();
            this.spielbrett = new System.Windows.Forms.Timer(this.components);
            this.pbInfo = new System.Windows.Forms.PictureBox();
            this.lblSpieler = new System.Windows.Forms.Label();
            this.lblZug = new System.Windows.Forms.Label();
            this.lblPunkte = new System.Windows.Forms.Label();
            this.lblKondition = new System.Windows.Forms.Label();
            this.pbSettings = new System.Windows.Forms.PictureBox();
            this.lblLevel = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.tbRange = new System.Windows.Forms.TextBox();
            this.lblRadius = new System.Windows.Forms.Label();
            this.lblSpielerInput = new System.Windows.Forms.Label();
            this.tbPlayers = new System.Windows.Forms.TextBox();
            this.lblLevelInput = new System.Windows.Forms.Label();
            this.btnreset = new System.Windows.Forms.Button();
            this.gbLevelEditor = new System.Windows.Forms.GroupBox();
            this.Mover = new System.Windows.Forms.Timer(this.components);
            this.lblPlayers = new System.Windows.Forms.Label();
            this.lblTurn = new System.Windows.Forms.Label();
            this.lblPoints = new System.Windows.Forms.Label();
            this.lvlcondition = new System.Windows.Forms.Label();
            this.lblLvl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbSpielbrett)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSettings)).BeginInit();
            this.gbLevelEditor.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbSpielbrett
            // 
            this.pbSpielbrett.BackColor = System.Drawing.Color.Black;
            this.pbSpielbrett.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbSpielbrett.Location = new System.Drawing.Point(-2, -1);
            this.pbSpielbrett.Name = "pbSpielbrett";
            this.pbSpielbrett.Size = new System.Drawing.Size(720, 720);
            this.pbSpielbrett.TabIndex = 0;
            this.pbSpielbrett.TabStop = false;
            this.pbSpielbrett.Paint += new System.Windows.Forms.PaintEventHandler(this.pbSpielbrett_Paint);
            this.pbSpielbrett.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbSpielbrett_MouseClick);
            this.pbSpielbrett.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbSpielbrett_MouseMove);
            // 
            // spielbrett
            // 
            this.spielbrett.Tick += new System.EventHandler(this.Spielbrett_Tick);
            // 
            // pbInfo
            // 
            this.pbInfo.BackColor = System.Drawing.Color.Black;
            this.pbInfo.Location = new System.Drawing.Point(724, -1);
            this.pbInfo.Name = "pbInfo";
            this.pbInfo.Size = new System.Drawing.Size(285, 219);
            this.pbInfo.TabIndex = 1;
            this.pbInfo.TabStop = false;
            // 
            // lblSpieler
            // 
            this.lblSpieler.AutoSize = true;
            this.lblSpieler.BackColor = System.Drawing.Color.Black;
            this.lblSpieler.Font = new System.Drawing.Font("Calibri Light", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSpieler.ForeColor = System.Drawing.Color.White;
            this.lblSpieler.Location = new System.Drawing.Point(743, 9);
            this.lblSpieler.Name = "lblSpieler";
            this.lblSpieler.Size = new System.Drawing.Size(69, 24);
            this.lblSpieler.TabIndex = 2;
            this.lblSpieler.Text = "Spieler:";
            this.lblSpieler.Visible = false;
            // 
            // lblZug
            // 
            this.lblZug.AutoSize = true;
            this.lblZug.BackColor = System.Drawing.Color.Black;
            this.lblZug.Font = new System.Drawing.Font("Calibri Light", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblZug.ForeColor = System.Drawing.Color.White;
            this.lblZug.Location = new System.Drawing.Point(743, 50);
            this.lblZug.Name = "lblZug";
            this.lblZug.Size = new System.Drawing.Size(43, 24);
            this.lblZug.TabIndex = 3;
            this.lblZug.Text = "Zug:";
            this.lblZug.Visible = false;
            // 
            // lblPunkte
            // 
            this.lblPunkte.AutoSize = true;
            this.lblPunkte.BackColor = System.Drawing.Color.Black;
            this.lblPunkte.Font = new System.Drawing.Font("Calibri Light", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPunkte.ForeColor = System.Drawing.Color.White;
            this.lblPunkte.Location = new System.Drawing.Point(743, 85);
            this.lblPunkte.Name = "lblPunkte";
            this.lblPunkte.Size = new System.Drawing.Size(71, 24);
            this.lblPunkte.TabIndex = 4;
            this.lblPunkte.Text = "Punkte:";
            this.lblPunkte.Visible = false;
            // 
            // lblKondition
            // 
            this.lblKondition.AutoSize = true;
            this.lblKondition.BackColor = System.Drawing.Color.Black;
            this.lblKondition.Font = new System.Drawing.Font("Calibri Light", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKondition.ForeColor = System.Drawing.Color.White;
            this.lblKondition.Location = new System.Drawing.Point(743, 122);
            this.lblKondition.Name = "lblKondition";
            this.lblKondition.Size = new System.Drawing.Size(90, 24);
            this.lblKondition.TabIndex = 5;
            this.lblKondition.Text = "Kondition:";
            this.lblKondition.Visible = false;
            // 
            // pbSettings
            // 
            this.pbSettings.BackColor = System.Drawing.Color.Black;
            this.pbSettings.Location = new System.Drawing.Point(725, 225);
            this.pbSettings.Name = "pbSettings";
            this.pbSettings.Size = new System.Drawing.Size(284, 494);
            this.pbSettings.TabIndex = 6;
            this.pbSettings.TabStop = false;
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.BackColor = System.Drawing.Color.Black;
            this.lblLevel.Font = new System.Drawing.Font("Calibri Light", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLevel.ForeColor = System.Drawing.Color.White;
            this.lblLevel.Location = new System.Drawing.Point(743, 163);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(56, 24);
            this.lblLevel.TabIndex = 7;
            this.lblLevel.Text = "Level:";
            this.lblLevel.Visible = false;
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.Black;
            this.btnStart.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnStart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnStart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.ForeColor = System.Drawing.Color.White;
            this.btnStart.Location = new System.Drawing.Point(249, 223);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(231, 60);
            this.btnStart.TabIndex = 8;
            this.btnStart.Text = "Spiel start";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbRange
            // 
            this.tbRange.BackColor = System.Drawing.Color.Black;
            this.tbRange.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRange.ForeColor = System.Drawing.Color.White;
            this.tbRange.Location = new System.Drawing.Point(151, 31);
            this.tbRange.Name = "tbRange";
            this.tbRange.Size = new System.Drawing.Size(43, 30);
            this.tbRange.TabIndex = 9;
            this.tbRange.Text = "8";
            // 
            // lblRadius
            // 
            this.lblRadius.AutoSize = true;
            this.lblRadius.BackColor = System.Drawing.Color.Black;
            this.lblRadius.Font = new System.Drawing.Font("Calibri Light", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRadius.ForeColor = System.Drawing.Color.White;
            this.lblRadius.Location = new System.Drawing.Point(6, 34);
            this.lblRadius.Name = "lblRadius";
            this.lblRadius.Size = new System.Drawing.Size(129, 24);
            this.lblRadius.TabIndex = 10;
            this.lblRadius.Text = "Spielfeldradius:";
            // 
            // lblSpielerInput
            // 
            this.lblSpielerInput.AutoSize = true;
            this.lblSpielerInput.BackColor = System.Drawing.Color.Black;
            this.lblSpielerInput.Font = new System.Drawing.Font("Calibri Light", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSpielerInput.ForeColor = System.Drawing.Color.White;
            this.lblSpielerInput.Location = new System.Drawing.Point(6, 73);
            this.lblSpielerInput.Name = "lblSpielerInput";
            this.lblSpielerInput.Size = new System.Drawing.Size(69, 24);
            this.lblSpielerInput.TabIndex = 11;
            this.lblSpielerInput.Text = "Spieler:";
            // 
            // tbPlayers
            // 
            this.tbPlayers.BackColor = System.Drawing.Color.Black;
            this.tbPlayers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPlayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPlayers.ForeColor = System.Drawing.Color.White;
            this.tbPlayers.Location = new System.Drawing.Point(151, 70);
            this.tbPlayers.Name = "tbPlayers";
            this.tbPlayers.Size = new System.Drawing.Size(43, 30);
            this.tbPlayers.TabIndex = 13;
            this.tbPlayers.Text = "2";
            // 
            // lblLevelInput
            // 
            this.lblLevelInput.AutoSize = true;
            this.lblLevelInput.BackColor = System.Drawing.Color.Black;
            this.lblLevelInput.Font = new System.Drawing.Font("Calibri Light", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLevelInput.ForeColor = System.Drawing.Color.White;
            this.lblLevelInput.Location = new System.Drawing.Point(6, 112);
            this.lblLevelInput.Name = "lblLevelInput";
            this.lblLevelInput.Size = new System.Drawing.Size(129, 24);
            this.lblLevelInput.TabIndex = 14;
            this.lblLevelInput.Text = "Starte in Level:";
            // 
            // btnreset
            // 
            this.btnreset.BackColor = System.Drawing.Color.Black;
            this.btnreset.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnreset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnreset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btnreset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnreset.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnreset.ForeColor = System.Drawing.Color.White;
            this.btnreset.Location = new System.Drawing.Point(6, 151);
            this.btnreset.Name = "btnreset";
            this.btnreset.Size = new System.Drawing.Size(136, 36);
            this.btnreset.TabIndex = 15;
            this.btnreset.Text = "Zurücksetzen";
            this.btnreset.UseVisualStyleBackColor = false;
            this.btnreset.Click += new System.EventHandler(this.button2_Click);
            // 
            // gbLevelEditor
            // 
            this.gbLevelEditor.BackColor = System.Drawing.Color.Black;
            this.gbLevelEditor.Controls.Add(this.tbRange);
            this.gbLevelEditor.Controls.Add(this.btnreset);
            this.gbLevelEditor.Controls.Add(this.tbPlayers);
            this.gbLevelEditor.Controls.Add(this.lblLevelInput);
            this.gbLevelEditor.Controls.Add(this.lblRadius);
            this.gbLevelEditor.Controls.Add(this.lblSpielerInput);
            this.gbLevelEditor.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbLevelEditor.ForeColor = System.Drawing.Color.White;
            this.gbLevelEditor.Location = new System.Drawing.Point(249, 319);
            this.gbLevelEditor.Name = "gbLevelEditor";
            this.gbLevelEditor.Size = new System.Drawing.Size(231, 214);
            this.gbLevelEditor.TabIndex = 16;
            this.gbLevelEditor.TabStop = false;
            this.gbLevelEditor.Text = "Leveleditor";
            // 
            // Mover
            // 
            this.Mover.Interval = 15;
            this.Mover.Tick += new System.EventHandler(this.Mover_Tick);
            // 
            // lblPlayers
            // 
            this.lblPlayers.AutoSize = true;
            this.lblPlayers.BackColor = System.Drawing.Color.Black;
            this.lblPlayers.Font = new System.Drawing.Font("Calibri Light", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayers.ForeColor = System.Drawing.Color.White;
            this.lblPlayers.Location = new System.Drawing.Point(858, 9);
            this.lblPlayers.Name = "lblPlayers";
            this.lblPlayers.Size = new System.Drawing.Size(0, 24);
            this.lblPlayers.TabIndex = 17;
            this.lblPlayers.Visible = false;
            // 
            // lblTurn
            // 
            this.lblTurn.AutoSize = true;
            this.lblTurn.BackColor = System.Drawing.Color.Black;
            this.lblTurn.Font = new System.Drawing.Font("Calibri Light", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTurn.ForeColor = System.Drawing.Color.White;
            this.lblTurn.Location = new System.Drawing.Point(858, 50);
            this.lblTurn.Name = "lblTurn";
            this.lblTurn.Size = new System.Drawing.Size(0, 24);
            this.lblTurn.TabIndex = 18;
            this.lblTurn.Visible = false;
            // 
            // lblPoints
            // 
            this.lblPoints.AutoSize = true;
            this.lblPoints.BackColor = System.Drawing.Color.Black;
            this.lblPoints.Font = new System.Drawing.Font("Calibri Light", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPoints.ForeColor = System.Drawing.Color.White;
            this.lblPoints.Location = new System.Drawing.Point(858, 85);
            this.lblPoints.Name = "lblPoints";
            this.lblPoints.Size = new System.Drawing.Size(0, 24);
            this.lblPoints.TabIndex = 19;
            this.lblPoints.Visible = false;
            // 
            // lvlcondition
            // 
            this.lvlcondition.AutoSize = true;
            this.lvlcondition.BackColor = System.Drawing.Color.Black;
            this.lvlcondition.Font = new System.Drawing.Font("Calibri Light", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlcondition.ForeColor = System.Drawing.Color.White;
            this.lvlcondition.Location = new System.Drawing.Point(858, 122);
            this.lvlcondition.Name = "lvlcondition";
            this.lvlcondition.Size = new System.Drawing.Size(0, 24);
            this.lvlcondition.TabIndex = 20;
            this.lvlcondition.Visible = false;
            // 
            // lblLvl
            // 
            this.lblLvl.AutoSize = true;
            this.lblLvl.BackColor = System.Drawing.Color.Black;
            this.lblLvl.Font = new System.Drawing.Font("Calibri Light", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLvl.ForeColor = System.Drawing.Color.White;
            this.lblLvl.Location = new System.Drawing.Point(858, 163);
            this.lblLvl.Name = "lblLvl";
            this.lblLvl.Size = new System.Drawing.Size(0, 24);
            this.lblLvl.TabIndex = 21;
            this.lblLvl.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(1008, 721);
            this.Controls.Add(this.lblLvl);
            this.Controls.Add(this.lvlcondition);
            this.Controls.Add(this.lblPoints);
            this.Controls.Add(this.lblTurn);
            this.Controls.Add(this.lblPlayers);
            this.Controls.Add(this.gbLevelEditor);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblLevel);
            this.Controls.Add(this.pbSettings);
            this.Controls.Add(this.lblKondition);
            this.Controls.Add(this.lblPunkte);
            this.Controls.Add(this.lblZug);
            this.Controls.Add(this.lblSpieler);
            this.Controls.Add(this.pbInfo);
            this.Controls.Add(this.pbSpielbrett);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Spiel";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pbSpielbrett)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSettings)).EndInit();
            this.gbLevelEditor.ResumeLayout(false);
            this.gbLevelEditor.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbSpielbrett;
        private System.Windows.Forms.Timer spielbrett;
        private System.Windows.Forms.PictureBox pbInfo;
        private System.Windows.Forms.Label lblSpieler;
        private System.Windows.Forms.Label lblZug;
        private System.Windows.Forms.Label lblPunkte;
        private System.Windows.Forms.Label lblKondition;
        private System.Windows.Forms.PictureBox pbSettings;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.Button btnStart;
        public System.Windows.Forms.TextBox tbRange;
        private System.Windows.Forms.Label lblRadius;
        private System.Windows.Forms.Label lblSpielerInput;
        public System.Windows.Forms.TextBox tbPlayers;
        private System.Windows.Forms.Label lblLevelInput;
        private System.Windows.Forms.Button btnreset;
        private System.Windows.Forms.GroupBox gbLevelEditor;
        private System.Windows.Forms.Timer Mover;
        private System.Windows.Forms.Label lblPlayers;
        private System.Windows.Forms.Label lblTurn;
        private System.Windows.Forms.Label lblPoints;
        private System.Windows.Forms.Label lvlcondition;
        private System.Windows.Forms.Label lblLvl;
    }
}

