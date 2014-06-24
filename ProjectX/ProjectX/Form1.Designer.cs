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
            this.spielbrett = new System.Windows.Forms.Timer(this.components);
            this.lblSpieler = new System.Windows.Forms.Label();
            this.lblZug = new System.Windows.Forms.Label();
            this.lblPunkte = new System.Windows.Forms.Label();
            this.lblKondition = new System.Windows.Forms.Label();
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
            this.lblcondition = new System.Windows.Forms.Label();
            this.lblLvl = new System.Windows.Forms.Label();
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.pnlAction = new System.Windows.Forms.Panel();
            this.btnAttack = new System.Windows.Forms.Button();
            this.lblKritisch = new System.Windows.Forms.Label();
            this.lblAngriff = new System.Windows.Forms.Label();
            this.lblKritschText = new System.Windows.Forms.Label();
            this.lblAngriffText = new System.Windows.Forms.Label();
            this.lblLifegauge = new System.Windows.Forms.Label();
            this.pnlLeben = new System.Windows.Forms.Panel();
            this.lblLiifegaugeText = new System.Windows.Forms.Label();
            this.pnlEnd = new System.Windows.Forms.Panel();
            this.pbSpielbrett = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.gbLevelEditor.SuspendLayout();
            this.pnlInfo.SuspendLayout();
            this.pnlAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSpielbrett)).BeginInit();
            this.SuspendLayout();
            // 
            // spielbrett
            // 
            this.spielbrett.Tick += new System.EventHandler(this.Spielbrett_Tick);
            // 
            // lblSpieler
            // 
            this.lblSpieler.AutoSize = true;
            this.lblSpieler.BackColor = System.Drawing.Color.Black;
            this.lblSpieler.Font = new System.Drawing.Font("Calibri Light", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSpieler.ForeColor = System.Drawing.Color.White;
            this.lblSpieler.Location = new System.Drawing.Point(3, 3);
            this.lblSpieler.Name = "lblSpieler";
            this.lblSpieler.Size = new System.Drawing.Size(69, 24);
            this.lblSpieler.TabIndex = 2;
            this.lblSpieler.Text = "Spieler:";
            // 
            // lblZug
            // 
            this.lblZug.AutoSize = true;
            this.lblZug.BackColor = System.Drawing.Color.Black;
            this.lblZug.Font = new System.Drawing.Font("Calibri Light", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblZug.ForeColor = System.Drawing.Color.White;
            this.lblZug.Location = new System.Drawing.Point(255, 3);
            this.lblZug.Name = "lblZug";
            this.lblZug.Size = new System.Drawing.Size(43, 24);
            this.lblZug.TabIndex = 3;
            this.lblZug.Text = "Zug:";
            // 
            // lblPunkte
            // 
            this.lblPunkte.AutoSize = true;
            this.lblPunkte.BackColor = System.Drawing.Color.Black;
            this.lblPunkte.Font = new System.Drawing.Font("Calibri Light", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPunkte.ForeColor = System.Drawing.Color.White;
            this.lblPunkte.Location = new System.Drawing.Point(535, 3);
            this.lblPunkte.Name = "lblPunkte";
            this.lblPunkte.Size = new System.Drawing.Size(71, 24);
            this.lblPunkte.TabIndex = 4;
            this.lblPunkte.Text = "Punkte:";
            // 
            // lblKondition
            // 
            this.lblKondition.AutoSize = true;
            this.lblKondition.BackColor = System.Drawing.Color.Black;
            this.lblKondition.Font = new System.Drawing.Font("Calibri Light", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKondition.ForeColor = System.Drawing.Color.White;
            this.lblKondition.Location = new System.Drawing.Point(3, 38);
            this.lblKondition.Name = "lblKondition";
            this.lblKondition.Size = new System.Drawing.Size(90, 24);
            this.lblKondition.TabIndex = 5;
            this.lblKondition.Text = "Kondition:";
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.BackColor = System.Drawing.Color.Black;
            this.lblLevel.Font = new System.Drawing.Font("Calibri Light", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLevel.ForeColor = System.Drawing.Color.White;
            this.lblLevel.Location = new System.Drawing.Point(535, 38);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(56, 24);
            this.lblLevel.TabIndex = 7;
            this.lblLevel.Text = "Level:";
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
            this.lblPlayers.Location = new System.Drawing.Point(78, 3);
            this.lblPlayers.Name = "lblPlayers";
            this.lblPlayers.Size = new System.Drawing.Size(0, 24);
            this.lblPlayers.TabIndex = 17;
            // 
            // lblTurn
            // 
            this.lblTurn.AutoSize = true;
            this.lblTurn.BackColor = System.Drawing.Color.Black;
            this.lblTurn.Font = new System.Drawing.Font("Calibri Light", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTurn.ForeColor = System.Drawing.Color.White;
            this.lblTurn.Location = new System.Drawing.Point(304, 3);
            this.lblTurn.Name = "lblTurn";
            this.lblTurn.Size = new System.Drawing.Size(0, 24);
            this.lblTurn.TabIndex = 18;
            // 
            // lblPoints
            // 
            this.lblPoints.AutoSize = true;
            this.lblPoints.BackColor = System.Drawing.Color.Black;
            this.lblPoints.Font = new System.Drawing.Font("Calibri Light", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPoints.ForeColor = System.Drawing.Color.White;
            this.lblPoints.Location = new System.Drawing.Point(612, 3);
            this.lblPoints.Name = "lblPoints";
            this.lblPoints.Size = new System.Drawing.Size(0, 24);
            this.lblPoints.TabIndex = 19;
            // 
            // lblcondition
            // 
            this.lblcondition.AutoSize = true;
            this.lblcondition.BackColor = System.Drawing.Color.Black;
            this.lblcondition.Font = new System.Drawing.Font("Calibri Light", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcondition.ForeColor = System.Drawing.Color.White;
            this.lblcondition.Location = new System.Drawing.Point(93, 39);
            this.lblcondition.Name = "lblcondition";
            this.lblcondition.Size = new System.Drawing.Size(0, 24);
            this.lblcondition.TabIndex = 20;
            // 
            // lblLvl
            // 
            this.lblLvl.AutoSize = true;
            this.lblLvl.BackColor = System.Drawing.Color.Black;
            this.lblLvl.Font = new System.Drawing.Font("Calibri Light", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLvl.ForeColor = System.Drawing.Color.White;
            this.lblLvl.Location = new System.Drawing.Point(597, 38);
            this.lblLvl.Name = "lblLvl";
            this.lblLvl.Size = new System.Drawing.Size(0, 24);
            this.lblLvl.TabIndex = 21;
            // 
            // pnlInfo
            // 
            this.pnlInfo.BackColor = System.Drawing.Color.Black;
            this.pnlInfo.Controls.Add(this.pnlEnd);
            this.pnlInfo.Controls.Add(this.lblSpieler);
            this.pnlInfo.Controls.Add(this.lblLvl);
            this.pnlInfo.Controls.Add(this.lblPlayers);
            this.pnlInfo.Controls.Add(this.lblcondition);
            this.pnlInfo.Controls.Add(this.lblZug);
            this.pnlInfo.Controls.Add(this.lblLevel);
            this.pnlInfo.Controls.Add(this.lblPoints);
            this.pnlInfo.Controls.Add(this.lblTurn);
            this.pnlInfo.Controls.Add(this.lblPunkte);
            this.pnlInfo.Controls.Add(this.lblKondition);
            this.pnlInfo.Location = new System.Drawing.Point(0, 748);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(748, 80);
            this.pnlInfo.TabIndex = 22;
            this.pnlInfo.Visible = false;
            // 
            // pnlAction
            // 
            this.pnlAction.BackColor = System.Drawing.Color.Black;
            this.pnlAction.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlAction.Controls.Add(this.btnClose);
            this.pnlAction.Controls.Add(this.btnAttack);
            this.pnlAction.Controls.Add(this.lblKritisch);
            this.pnlAction.Controls.Add(this.lblAngriff);
            this.pnlAction.Controls.Add(this.lblKritschText);
            this.pnlAction.Controls.Add(this.lblAngriffText);
            this.pnlAction.Controls.Add(this.lblLifegauge);
            this.pnlAction.Controls.Add(this.pnlLeben);
            this.pnlAction.Controls.Add(this.lblLiifegaugeText);
            this.pnlAction.Location = new System.Drawing.Point(521, 46);
            this.pnlAction.Name = "pnlAction";
            this.pnlAction.Size = new System.Drawing.Size(0, 0);
            this.pnlAction.TabIndex = 23;
            // 
            // btnAttack
            // 
            this.btnAttack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAttack.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnAttack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btnAttack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAttack.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAttack.ForeColor = System.Drawing.Color.White;
            this.btnAttack.Location = new System.Drawing.Point(4, 102);
            this.btnAttack.Name = "btnAttack";
            this.btnAttack.Size = new System.Drawing.Size(125, 42);
            this.btnAttack.TabIndex = 7;
            this.btnAttack.Text = "Attacke!";
            this.btnAttack.UseVisualStyleBackColor = true;
            this.btnAttack.Click += new System.EventHandler(this.btnAttack_Click);
            // 
            // lblKritisch
            // 
            this.lblKritisch.AutoSize = true;
            this.lblKritisch.BackColor = System.Drawing.Color.Black;
            this.lblKritisch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKritisch.ForeColor = System.Drawing.Color.White;
            this.lblKritisch.Location = new System.Drawing.Point(59, 85);
            this.lblKritisch.Name = "lblKritisch";
            this.lblKritisch.Size = new System.Drawing.Size(15, 13);
            this.lblKritisch.TabIndex = 6;
            this.lblKritisch.Text = "X";
            // 
            // lblAngriff
            // 
            this.lblAngriff.AutoSize = true;
            this.lblAngriff.BackColor = System.Drawing.Color.Black;
            this.lblAngriff.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAngriff.ForeColor = System.Drawing.Color.White;
            this.lblAngriff.Location = new System.Drawing.Point(59, 61);
            this.lblAngriff.Name = "lblAngriff";
            this.lblAngriff.Size = new System.Drawing.Size(15, 13);
            this.lblAngriff.TabIndex = 5;
            this.lblAngriff.Text = "X";
            // 
            // lblKritschText
            // 
            this.lblKritschText.AutoSize = true;
            this.lblKritschText.BackColor = System.Drawing.Color.Black;
            this.lblKritschText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKritschText.ForeColor = System.Drawing.Color.White;
            this.lblKritschText.Location = new System.Drawing.Point(1, 85);
            this.lblKritschText.Name = "lblKritschText";
            this.lblKritschText.Size = new System.Drawing.Size(53, 13);
            this.lblKritschText.TabIndex = 4;
            this.lblKritschText.Text = "Kritisch:";
            // 
            // lblAngriffText
            // 
            this.lblAngriffText.AutoSize = true;
            this.lblAngriffText.BackColor = System.Drawing.Color.Black;
            this.lblAngriffText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAngriffText.ForeColor = System.Drawing.Color.White;
            this.lblAngriffText.Location = new System.Drawing.Point(1, 61);
            this.lblAngriffText.Name = "lblAngriffText";
            this.lblAngriffText.Size = new System.Drawing.Size(52, 13);
            this.lblAngriffText.TabIndex = 3;
            this.lblAngriffText.Text = "Angriff: ";
            // 
            // lblLifegauge
            // 
            this.lblLifegauge.AutoSize = true;
            this.lblLifegauge.BackColor = System.Drawing.Color.Black;
            this.lblLifegauge.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLifegauge.ForeColor = System.Drawing.Color.White;
            this.lblLifegauge.Location = new System.Drawing.Point(44, 13);
            this.lblLifegauge.Name = "lblLifegauge";
            this.lblLifegauge.Size = new System.Drawing.Size(41, 13);
            this.lblLifegauge.TabIndex = 2;
            this.lblLifegauge.Text = "20/20";
            // 
            // pnlLeben
            // 
            this.pnlLeben.BackColor = System.Drawing.Color.Lime;
            this.pnlLeben.Location = new System.Drawing.Point(4, 30);
            this.pnlLeben.Name = "pnlLeben";
            this.pnlLeben.Size = new System.Drawing.Size(125, 16);
            this.pnlLeben.TabIndex = 1;
            // 
            // lblLiifegaugeText
            // 
            this.lblLiifegaugeText.AutoSize = true;
            this.lblLiifegaugeText.BackColor = System.Drawing.Color.Black;
            this.lblLiifegaugeText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLiifegaugeText.ForeColor = System.Drawing.Color.White;
            this.lblLiifegaugeText.Location = new System.Drawing.Point(1, 13);
            this.lblLiifegaugeText.Name = "lblLiifegaugeText";
            this.lblLiifegaugeText.Size = new System.Drawing.Size(37, 13);
            this.lblLiifegaugeText.TabIndex = 0;
            this.lblLiifegaugeText.Text = "Burg:";
            // 
            // pnlEnd
            // 
            this.pnlEnd.Location = new System.Drawing.Point(174, 30);
            this.pnlEnd.Name = "pnlEnd";
            this.pnlEnd.Size = new System.Drawing.Size(326, 44);
            this.pnlEnd.TabIndex = 22;
            this.pnlEnd.Visible = false;
            this.pnlEnd.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlEnd_Paint);
            // 
            // pbSpielbrett
            // 
            this.pbSpielbrett.BackColor = System.Drawing.Color.Black;
            this.pbSpielbrett.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbSpielbrett.Location = new System.Drawing.Point(-2, -1);
            this.pbSpielbrett.Name = "pbSpielbrett";
            this.pbSpielbrett.Size = new System.Drawing.Size(750, 750);
            this.pbSpielbrett.TabIndex = 0;
            this.pbSpielbrett.TabStop = false;
            this.pbSpielbrett.Click += new System.EventHandler(this.pbSpielbrett_Click);
            this.pbSpielbrett.Paint += new System.Windows.Forms.PaintEventHandler(this.pbSpielbrett_Paint);
            this.pbSpielbrett.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbSpielbrett_MouseClick);
            this.pbSpielbrett.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbSpielbrett_MouseMove);
            // 
            // btnClose
            // 
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(113, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(16, 16);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(747, 823);
            this.Controls.Add(this.pnlAction);
            this.Controls.Add(this.pnlInfo);
            this.Controls.Add(this.gbLevelEditor);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.pbSpielbrett);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Spiel";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.gbLevelEditor.ResumeLayout(false);
            this.gbLevelEditor.PerformLayout();
            this.pnlInfo.ResumeLayout(false);
            this.pnlInfo.PerformLayout();
            this.pnlAction.ResumeLayout(false);
            this.pnlAction.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSpielbrett)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbSpielbrett;
        private System.Windows.Forms.Timer spielbrett;
        private System.Windows.Forms.Label lblSpieler;
        private System.Windows.Forms.Label lblZug;
        private System.Windows.Forms.Label lblPunkte;
        private System.Windows.Forms.Label lblKondition;
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
        private System.Windows.Forms.Label lblcondition;
        private System.Windows.Forms.Label lblLvl;
        private System.Windows.Forms.Panel pnlInfo;
        private System.Windows.Forms.Panel pnlAction;
        private System.Windows.Forms.Label lblLifegauge;
        private System.Windows.Forms.Panel pnlLeben;
        private System.Windows.Forms.Label lblLiifegaugeText;
        private System.Windows.Forms.Label lblKritisch;
        private System.Windows.Forms.Label lblAngriff;
        private System.Windows.Forms.Label lblKritschText;
        private System.Windows.Forms.Label lblAngriffText;
        private System.Windows.Forms.Button btnAttack;
        private System.Windows.Forms.Panel pnlEnd;
        private System.Windows.Forms.Button btnClose;
    }
}

