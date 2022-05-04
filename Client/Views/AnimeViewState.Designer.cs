namespace Views
{
    partial class AnimeViewState
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
            this.labelAnime = new System.Windows.Forms.Label();
            this.buttonCauta = new System.Windows.Forms.Button();
            this.textBoxCauta = new System.Windows.Forms.TextBox();
            this.buttonIesire = new System.Windows.Forms.Button();
            this.buttonProfil = new System.Windows.Forms.Button();
            this.buttonNoutati = new System.Windows.Forms.Button();
            this.panelAnime = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonAdauga = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBarRating = new System.Windows.Forms.TrackBar();
            this.textBoxTitlu = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelAdaugaReview = new System.Windows.Forms.Label();
            this.textBoxDescriereAdaugaReview = new System.Windows.Forms.TextBox();
            this.buttonPaginaAnterioara = new System.Windows.Forms.Button();
            this.buttonPaginaUrmatoare = new System.Windows.Forms.Button();
            this.labelDescriere = new System.Windows.Forms.Label();
            this.labelReviewuri = new System.Windows.Forms.Label();
            this.textBoxDescriere = new System.Windows.Forms.TextBox();
            this.labelNota = new System.Windows.Forms.Label();
            this.labelEpisoade = new System.Windows.Forms.Label();
            this.labelAutor = new System.Windows.Forms.Label();
            this.panelReviewuri = new System.Windows.Forms.Panel();
            this.labelTitlu = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ajutorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelAnime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRating)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelAnime
            // 
            this.labelAnime.AutoSize = true;
            this.labelAnime.Font = new System.Drawing.Font("Segoe Script", 19.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAnime.ForeColor = System.Drawing.Color.Coral;
            this.labelAnime.Location = new System.Drawing.Point(35, 31);
            this.labelAnime.Name = "labelAnime";
            this.labelAnime.Size = new System.Drawing.Size(143, 55);
            this.labelAnime.TabIndex = 48;
            this.labelAnime.Text = "Anime";
            // 
            // buttonCauta
            // 
            this.buttonCauta.BackColor = System.Drawing.Color.Linen;
            this.buttonCauta.ForeColor = System.Drawing.Color.Coral;
            this.buttonCauta.Location = new System.Drawing.Point(740, 54);
            this.buttonCauta.Name = "buttonCauta";
            this.buttonCauta.Size = new System.Drawing.Size(75, 22);
            this.buttonCauta.TabIndex = 44;
            this.buttonCauta.Text = "Cauta";
            this.buttonCauta.UseVisualStyleBackColor = false;
            this.buttonCauta.Click += new System.EventHandler(this.buttonCauta_Click);
            // 
            // textBoxCauta
            // 
            this.textBoxCauta.Location = new System.Drawing.Point(212, 54);
            this.textBoxCauta.Name = "textBoxCauta";
            this.textBoxCauta.Size = new System.Drawing.Size(513, 22);
            this.textBoxCauta.TabIndex = 43;
            // 
            // buttonIesire
            // 
            this.buttonIesire.BackColor = System.Drawing.Color.Linen;
            this.buttonIesire.ForeColor = System.Drawing.Color.Coral;
            this.buttonIesire.Location = new System.Drawing.Point(54, 365);
            this.buttonIesire.Name = "buttonIesire";
            this.buttonIesire.Size = new System.Drawing.Size(106, 54);
            this.buttonIesire.TabIndex = 42;
            this.buttonIesire.Text = "Iesire";
            this.buttonIesire.UseVisualStyleBackColor = false;
            this.buttonIesire.Click += new System.EventHandler(this.buttonIesire_Click);
            // 
            // buttonProfil
            // 
            this.buttonProfil.BackColor = System.Drawing.Color.Linen;
            this.buttonProfil.ForeColor = System.Drawing.Color.Coral;
            this.buttonProfil.Location = new System.Drawing.Point(54, 238);
            this.buttonProfil.Name = "buttonProfil";
            this.buttonProfil.Size = new System.Drawing.Size(106, 54);
            this.buttonProfil.TabIndex = 41;
            this.buttonProfil.Text = "Profil";
            this.buttonProfil.UseVisualStyleBackColor = false;
            this.buttonProfil.Click += new System.EventHandler(this.buttonProfil_Click);
            // 
            // buttonNoutati
            // 
            this.buttonNoutati.BackColor = System.Drawing.Color.Linen;
            this.buttonNoutati.ForeColor = System.Drawing.Color.Coral;
            this.buttonNoutati.Location = new System.Drawing.Point(54, 111);
            this.buttonNoutati.Name = "buttonNoutati";
            this.buttonNoutati.Size = new System.Drawing.Size(106, 54);
            this.buttonNoutati.TabIndex = 40;
            this.buttonNoutati.Text = "Noutati";
            this.buttonNoutati.UseVisualStyleBackColor = false;
            this.buttonNoutati.Click += new System.EventHandler(this.buttonNoutati_Click);
            // 
            // panelAnime
            // 
            this.panelAnime.AutoScroll = true;
            this.panelAnime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelAnime.Controls.Add(this.panel1);
            this.panelAnime.Controls.Add(this.buttonAdauga);
            this.panelAnime.Controls.Add(this.label3);
            this.panelAnime.Controls.Add(this.trackBarRating);
            this.panelAnime.Controls.Add(this.textBoxTitlu);
            this.panelAnime.Controls.Add(this.label2);
            this.panelAnime.Controls.Add(this.label1);
            this.panelAnime.Controls.Add(this.labelAdaugaReview);
            this.panelAnime.Controls.Add(this.textBoxDescriereAdaugaReview);
            this.panelAnime.Controls.Add(this.buttonPaginaAnterioara);
            this.panelAnime.Controls.Add(this.buttonPaginaUrmatoare);
            this.panelAnime.Controls.Add(this.labelDescriere);
            this.panelAnime.Controls.Add(this.labelReviewuri);
            this.panelAnime.Controls.Add(this.textBoxDescriere);
            this.panelAnime.Controls.Add(this.labelNota);
            this.panelAnime.Controls.Add(this.labelEpisoade);
            this.panelAnime.Controls.Add(this.labelAutor);
            this.panelAnime.Controls.Add(this.panelReviewuri);
            this.panelAnime.Controls.Add(this.labelTitlu);
            this.panelAnime.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelAnime.Location = new System.Drawing.Point(212, 111);
            this.panelAnime.Name = "panelAnime";
            this.panelAnime.Size = new System.Drawing.Size(603, 416);
            this.panelAnime.TabIndex = 45;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(105, 1077);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(39, 69);
            this.panel1.TabIndex = 63;
            this.panel1.Visible = false;
            // 
            // buttonAdauga
            // 
            this.buttonAdauga.BackColor = System.Drawing.Color.Linen;
            this.buttonAdauga.ForeColor = System.Drawing.Color.Coral;
            this.buttonAdauga.Location = new System.Drawing.Point(235, 1067);
            this.buttonAdauga.Name = "buttonAdauga";
            this.buttonAdauga.Size = new System.Drawing.Size(106, 54);
            this.buttonAdauga.TabIndex = 49;
            this.buttonAdauga.Text = "Adauga";
            this.buttonAdauga.UseVisualStyleBackColor = false;
            this.buttonAdauga.Click += new System.EventHandler(this.buttonAdauga_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Sienna;
            this.label3.Location = new System.Drawing.Point(16, 948);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 17);
            this.label3.TabIndex = 62;
            this.label3.Text = "Descriere:";
            // 
            // trackBarRating
            // 
            this.trackBarRating.Location = new System.Drawing.Point(75, 900);
            this.trackBarRating.Minimum = 1;
            this.trackBarRating.Name = "trackBarRating";
            this.trackBarRating.Size = new System.Drawing.Size(484, 56);
            this.trackBarRating.TabIndex = 61;
            this.trackBarRating.Value = 1;
            // 
            // textBoxTitlu
            // 
            this.textBoxTitlu.BackColor = System.Drawing.Color.FloralWhite;
            this.textBoxTitlu.ForeColor = System.Drawing.Color.Sienna;
            this.textBoxTitlu.Location = new System.Drawing.Point(75, 853);
            this.textBoxTitlu.Multiline = true;
            this.textBoxTitlu.Name = "textBoxTitlu";
            this.textBoxTitlu.Size = new System.Drawing.Size(484, 22);
            this.textBoxTitlu.TabIndex = 60;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Sienna;
            this.label2.Location = new System.Drawing.Point(16, 858);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 17);
            this.label2.TabIndex = 59;
            this.label2.Text = "Titlu:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Sienna;
            this.label1.Location = new System.Drawing.Point(16, 900);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 17);
            this.label1.TabIndex = 58;
            this.label1.Text = "Rating:";
            // 
            // labelAdaugaReview
            // 
            this.labelAdaugaReview.AutoSize = true;
            this.labelAdaugaReview.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAdaugaReview.ForeColor = System.Drawing.Color.Sienna;
            this.labelAdaugaReview.Location = new System.Drawing.Point(16, 822);
            this.labelAdaugaReview.Name = "labelAdaugaReview";
            this.labelAdaugaReview.Size = new System.Drawing.Size(114, 17);
            this.labelAdaugaReview.TabIndex = 57;
            this.labelAdaugaReview.Text = "Adauga review";
            // 
            // textBoxDescriereAdaugaReview
            // 
            this.textBoxDescriereAdaugaReview.BackColor = System.Drawing.Color.FloralWhite;
            this.textBoxDescriereAdaugaReview.ForeColor = System.Drawing.Color.Sienna;
            this.textBoxDescriereAdaugaReview.Location = new System.Drawing.Point(19, 979);
            this.textBoxDescriereAdaugaReview.Multiline = true;
            this.textBoxDescriereAdaugaReview.Name = "textBoxDescriereAdaugaReview";
            this.textBoxDescriereAdaugaReview.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxDescriereAdaugaReview.Size = new System.Drawing.Size(540, 63);
            this.textBoxDescriereAdaugaReview.TabIndex = 56;
            // 
            // buttonPaginaAnterioara
            // 
            this.buttonPaginaAnterioara.BackColor = System.Drawing.Color.Linen;
            this.buttonPaginaAnterioara.Enabled = false;
            this.buttonPaginaAnterioara.ForeColor = System.Drawing.Color.Coral;
            this.buttonPaginaAnterioara.Location = new System.Drawing.Point(19, 762);
            this.buttonPaginaAnterioara.Name = "buttonPaginaAnterioara";
            this.buttonPaginaAnterioara.Size = new System.Drawing.Size(159, 25);
            this.buttonPaginaAnterioara.TabIndex = 46;
            this.buttonPaginaAnterioara.Text = "Pagina anterioara";
            this.buttonPaginaAnterioara.UseVisualStyleBackColor = false;
            this.buttonPaginaAnterioara.Click += new System.EventHandler(this.buttonPaginaAnterioara_Click);
            // 
            // buttonPaginaUrmatoare
            // 
            this.buttonPaginaUrmatoare.BackColor = System.Drawing.Color.Linen;
            this.buttonPaginaUrmatoare.ForeColor = System.Drawing.Color.Coral;
            this.buttonPaginaUrmatoare.Location = new System.Drawing.Point(400, 762);
            this.buttonPaginaUrmatoare.Name = "buttonPaginaUrmatoare";
            this.buttonPaginaUrmatoare.Size = new System.Drawing.Size(159, 25);
            this.buttonPaginaUrmatoare.TabIndex = 47;
            this.buttonPaginaUrmatoare.Text = "Pagina urmatoare";
            this.buttonPaginaUrmatoare.UseVisualStyleBackColor = false;
            this.buttonPaginaUrmatoare.Click += new System.EventHandler(this.buttonPaginaUrmatoare_Click);
            // 
            // labelDescriere
            // 
            this.labelDescriere.AutoSize = true;
            this.labelDescriere.ForeColor = System.Drawing.Color.Sienna;
            this.labelDescriere.Location = new System.Drawing.Point(16, 225);
            this.labelDescriere.Name = "labelDescriere";
            this.labelDescriere.Size = new System.Drawing.Size(73, 17);
            this.labelDescriere.TabIndex = 54;
            this.labelDescriere.Text = "Descriere:";
            // 
            // labelReviewuri
            // 
            this.labelReviewuri.AutoSize = true;
            this.labelReviewuri.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelReviewuri.ForeColor = System.Drawing.Color.Sienna;
            this.labelReviewuri.Location = new System.Drawing.Point(16, 363);
            this.labelReviewuri.Name = "labelReviewuri";
            this.labelReviewuri.Size = new System.Drawing.Size(84, 17);
            this.labelReviewuri.TabIndex = 53;
            this.labelReviewuri.Text = "Review-uri";
            // 
            // textBoxDescriere
            // 
            this.textBoxDescriere.BackColor = System.Drawing.Color.FloralWhite;
            this.textBoxDescriere.ForeColor = System.Drawing.Color.Sienna;
            this.textBoxDescriere.Location = new System.Drawing.Point(19, 274);
            this.textBoxDescriere.Multiline = true;
            this.textBoxDescriere.Name = "textBoxDescriere";
            this.textBoxDescriere.ReadOnly = true;
            this.textBoxDescriere.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxDescriere.Size = new System.Drawing.Size(539, 63);
            this.textBoxDescriere.TabIndex = 52;
            this.textBoxDescriere.Text = "Descriere...";
            // 
            // labelNota
            // 
            this.labelNota.AutoSize = true;
            this.labelNota.ForeColor = System.Drawing.Color.Sienna;
            this.labelNota.Location = new System.Drawing.Point(16, 186);
            this.labelNota.Name = "labelNota";
            this.labelNota.Size = new System.Drawing.Size(70, 17);
            this.labelNota.TabIndex = 51;
            this.labelNota.Text = "Nota: ???";
            // 
            // labelEpisoade
            // 
            this.labelEpisoade.AutoSize = true;
            this.labelEpisoade.ForeColor = System.Drawing.Color.Sienna;
            this.labelEpisoade.Location = new System.Drawing.Point(16, 135);
            this.labelEpisoade.Name = "labelEpisoade";
            this.labelEpisoade.Size = new System.Drawing.Size(99, 17);
            this.labelEpisoade.TabIndex = 50;
            this.labelEpisoade.Text = "Episoade: ???";
            // 
            // labelAutor
            // 
            this.labelAutor.AutoSize = true;
            this.labelAutor.ForeColor = System.Drawing.Color.Sienna;
            this.labelAutor.Location = new System.Drawing.Point(15, 83);
            this.labelAutor.Name = "labelAutor";
            this.labelAutor.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelAutor.Size = new System.Drawing.Size(74, 17);
            this.labelAutor.TabIndex = 48;
            this.labelAutor.Text = "Autor: ???";
            // 
            // panelReviewuri
            // 
            this.panelReviewuri.AutoScroll = true;
            this.panelReviewuri.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelReviewuri.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelReviewuri.Location = new System.Drawing.Point(19, 395);
            this.panelReviewuri.Name = "panelReviewuri";
            this.panelReviewuri.Size = new System.Drawing.Size(540, 340);
            this.panelReviewuri.TabIndex = 46;
            // 
            // labelTitlu
            // 
            this.labelTitlu.AutoSize = true;
            this.labelTitlu.Font = new System.Drawing.Font("Segoe Script", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitlu.ForeColor = System.Drawing.Color.Sienna;
            this.labelTitlu.Location = new System.Drawing.Point(182, 14);
            this.labelTitlu.Name = "labelTitlu";
            this.labelTitlu.Size = new System.Drawing.Size(112, 55);
            this.labelTitlu.TabIndex = 49;
            this.labelTitlu.Text = "Titlu";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ajutorToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(848, 28);
            this.menuStrip1.TabIndex = 49;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ajutorToolStripMenuItem
            // 
            this.ajutorToolStripMenuItem.Name = "ajutorToolStripMenuItem";
            this.ajutorToolStripMenuItem.Size = new System.Drawing.Size(62, 24);
            this.ajutorToolStripMenuItem.Text = "Ajutor";
            this.ajutorToolStripMenuItem.Click += new System.EventHandler(this.ajutorToolStripMenuItem_Click);
            // 
            // AnimeViewState
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(848, 539);
            this.Controls.Add(this.labelAnime);
            this.Controls.Add(this.panelAnime);
            this.Controls.Add(this.buttonCauta);
            this.Controls.Add(this.textBoxCauta);
            this.Controls.Add(this.buttonIesire);
            this.Controls.Add(this.buttonProfil);
            this.Controls.Add(this.buttonNoutati);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "AnimeViewState";
            this.Text = "AnimeViewState";
            this.panelAnime.ResumeLayout(false);
            this.panelAnime.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRating)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelAnime;
        private System.Windows.Forms.Button buttonCauta;
        private System.Windows.Forms.TextBox textBoxCauta;
        private System.Windows.Forms.Button buttonIesire;
        private System.Windows.Forms.Button buttonProfil;
        private System.Windows.Forms.Button buttonNoutati;
        private System.Windows.Forms.Panel panelAnime;
        private System.Windows.Forms.Label labelAutor;
        private System.Windows.Forms.Label labelTitlu;
        private System.Windows.Forms.Label labelEpisoade;
        private System.Windows.Forms.Label labelReviewuri;
        private System.Windows.Forms.TextBox textBoxDescriere;
        private System.Windows.Forms.Label labelNota;
        private System.Windows.Forms.Label labelDescriere;
        private System.Windows.Forms.Button buttonPaginaAnterioara;
        private System.Windows.Forms.Button buttonPaginaUrmatoare;
        private System.Windows.Forms.Panel panelReviewuri;
        private System.Windows.Forms.Label labelAdaugaReview;
        private System.Windows.Forms.TextBox textBoxDescriereAdaugaReview;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxTitlu;
        private System.Windows.Forms.TrackBar trackBarRating;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonAdauga;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ajutorToolStripMenuItem;
    }
}