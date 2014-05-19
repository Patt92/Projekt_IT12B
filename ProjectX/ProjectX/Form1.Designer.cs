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
            this.button1 = new System.Windows.Forms.Button();
            this.tbRange = new System.Windows.Forms.TextBox();
            this.lblRadius = new System.Windows.Forms.Label();
            this.Cleanup = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbSpielbrett)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSettings)).BeginInit();
            this.SuspendLayout();
            // 
            // pbSpielbrett
            // 
            this.pbSpielbrett.BackColor = System.Drawing.Color.Black;
            this.pbSpielbrett.Location = new System.Drawing.Point(-2, -1);
            this.pbSpielbrett.Name = "pbSpielbrett";
            this.pbSpielbrett.Size = new System.Drawing.Size(720, 720);
            this.pbSpielbrett.TabIndex = 0;
            this.pbSpielbrett.TabStop = false;
            this.pbSpielbrett.Paint += new System.Windows.Forms.PaintEventHandler(this.pbSpielbrett_Paint);
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
            this.lblSpieler.Location = new System.Drawing.Point(743, 13);
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
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Black;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(747, 242);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(231, 60);
            this.button1.TabIndex = 8;
            this.button1.Text = "Spiel start";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbRange
            // 
            this.tbRange.BackColor = System.Drawing.Color.Black;
            this.tbRange.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRange.ForeColor = System.Drawing.Color.White;
            this.tbRange.Location = new System.Drawing.Point(935, 319);
            this.tbRange.Name = "tbRange";
            this.tbRange.Size = new System.Drawing.Size(43, 30);
            this.tbRange.TabIndex = 9;
            this.tbRange.Text = "16";
            // 
            // lblRadius
            // 
            this.lblRadius.AutoSize = true;
            this.lblRadius.BackColor = System.Drawing.Color.Black;
            this.lblRadius.Font = new System.Drawing.Font("Calibri Light", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRadius.ForeColor = System.Drawing.Color.White;
            this.lblRadius.Location = new System.Drawing.Point(743, 322);
            this.lblRadius.Name = "lblRadius";
            this.lblRadius.Size = new System.Drawing.Size(129, 24);
            this.lblRadius.TabIndex = 10;
            this.lblRadius.Text = "Spielfeldradius:";
            // 
            // Cleanup
            // 
            this.Cleanup.Tick += new System.EventHandler(this.Cleanup_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(1008, 721);
            this.Controls.Add(this.lblRadius);
            this.Controls.Add(this.tbRange);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblLevel);
            this.Controls.Add(this.pbSettings);
            this.Controls.Add(this.lblKondition);
            this.Controls.Add(this.lblPunkte);
            this.Controls.Add(this.lblZug);
            this.Controls.Add(this.lblSpieler);
            this.Controls.Add(this.pbInfo);
            this.Controls.Add(this.pbSpielbrett);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Spiel";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pbSpielbrett)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSettings)).EndInit();
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbRange;
        private System.Windows.Forms.Label lblRadius;
        private System.Windows.Forms.Timer Cleanup;
    }
}

