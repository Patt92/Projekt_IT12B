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
            this.Main_Menu_Ticker = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbSpielbrett)).BeginInit();
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
            // Main_Menu_Ticker
            // 
            this.Main_Menu_Ticker.Tick += new System.EventHandler(this.Main_Menu_Ticker_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 721);
            this.Controls.Add(this.pbSpielbrett);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbSpielbrett)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbSpielbrett;
        private System.Windows.Forms.Timer Main_Menu_Ticker;
    }
}

