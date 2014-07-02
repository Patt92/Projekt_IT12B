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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Spiel = new System.Windows.Forms.Timer(this.components);
            this.Mover = new System.Windows.Forms.Timer(this.components);
            this.Hauptmenu = new System.Windows.Forms.Timer(this.components);
            this.Editor = new System.Windows.Forms.Timer(this.components);
            this.Spiel_Laden = new System.Windows.Forms.Timer(this.components);
            this.pbSpielbrett = new System.Windows.Forms.PictureBox();
            this.Anleitung = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbSpielbrett)).BeginInit();
            this.SuspendLayout();
            // 
            // Spiel
            // 
            this.Spiel.Interval = 1;
            this.Spiel.Tick += new System.EventHandler(this.Spielbrett_Tick);
            // 
            // Mover
            // 
            this.Mover.Interval = 15;
            this.Mover.Tick += new System.EventHandler(this.Mover_Tick);
            // 
            // Hauptmenu
            // 
            this.Hauptmenu.Interval = 1;
            this.Hauptmenu.Tick += new System.EventHandler(this.Hauptmenu_Tick);
            // 
            // Editor
            // 
            this.Editor.Interval = 1;
            this.Editor.Tick += new System.EventHandler(this.Editor_Tick);
            // 
            // Spiel_Laden
            // 
            this.Spiel_Laden.Tick += new System.EventHandler(this.Spiel_Laden_Tick);
            // 
            // pbSpielbrett
            // 
            this.pbSpielbrett.BackColor = System.Drawing.Color.Black;
            this.pbSpielbrett.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbSpielbrett.BackgroundImage")));
            this.pbSpielbrett.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbSpielbrett.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbSpielbrett.Location = new System.Drawing.Point(-2, -1);
            this.pbSpielbrett.Name = "pbSpielbrett";
            this.pbSpielbrett.Size = new System.Drawing.Size(747, 722);
            this.pbSpielbrett.TabIndex = 0;
            this.pbSpielbrett.TabStop = false;
            this.pbSpielbrett.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbSpielbrett_MouseClick);
            this.pbSpielbrett.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbSpielbrett_MouseMove);
            // 
            // Anleitung
            // 
            this.Anleitung.Tick += new System.EventHandler(this.Anleitung_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(744, 721);
            this.Controls.Add(this.pbSpielbrett);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 600);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cattura";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pbSpielbrett)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbSpielbrett;
        private System.Windows.Forms.Timer Spiel;
        private System.Windows.Forms.Timer Mover;
        private System.Windows.Forms.Timer Hauptmenu;
        private System.Windows.Forms.Timer Editor;
        private System.Windows.Forms.Timer Spiel_Laden;
        private System.Windows.Forms.Timer Anleitung;
    }
}

