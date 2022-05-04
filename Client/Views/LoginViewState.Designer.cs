namespace ViewStates
{
    partial class LoginViewState
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
            this.labelNumeDeUtilizator = new System.Windows.Forms.Label();
            this.labelParola = new System.Windows.Forms.Label();
            this.textBoxNumeDeUtilizator = new System.Windows.Forms.TextBox();
            this.maskedTextBoxParola = new System.Windows.Forms.MaskedTextBox();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.buttonInapoi = new System.Windows.Forms.Button();
            this.labelInregistrare = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ajutorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelNumeDeUtilizator
            // 
            this.labelNumeDeUtilizator.AutoSize = true;
            this.labelNumeDeUtilizator.ForeColor = System.Drawing.Color.Coral;
            this.labelNumeDeUtilizator.Location = new System.Drawing.Point(44, 54);
            this.labelNumeDeUtilizator.Name = "labelNumeDeUtilizator";
            this.labelNumeDeUtilizator.Size = new System.Drawing.Size(122, 17);
            this.labelNumeDeUtilizator.TabIndex = 0;
            this.labelNumeDeUtilizator.Text = "Nume de utilizator";
            // 
            // labelParola
            // 
            this.labelParola.AutoSize = true;
            this.labelParola.ForeColor = System.Drawing.Color.Coral;
            this.labelParola.Location = new System.Drawing.Point(117, 112);
            this.labelParola.Name = "labelParola";
            this.labelParola.Size = new System.Drawing.Size(49, 17);
            this.labelParola.TabIndex = 1;
            this.labelParola.Text = "Parola";
            // 
            // textBoxNumeDeUtilizator
            // 
            this.textBoxNumeDeUtilizator.Location = new System.Drawing.Point(188, 49);
            this.textBoxNumeDeUtilizator.Name = "textBoxNumeDeUtilizator";
            this.textBoxNumeDeUtilizator.Size = new System.Drawing.Size(206, 22);
            this.textBoxNumeDeUtilizator.TabIndex = 2;
            // 
            // maskedTextBoxParola
            // 
            this.maskedTextBoxParola.Location = new System.Drawing.Point(188, 107);
            this.maskedTextBoxParola.Name = "maskedTextBoxParola";
            this.maskedTextBoxParola.PasswordChar = '*';
            this.maskedTextBoxParola.Size = new System.Drawing.Size(206, 22);
            this.maskedTextBoxParola.TabIndex = 3;
            // 
            // buttonLogin
            // 
            this.buttonLogin.BackColor = System.Drawing.Color.Linen;
            this.buttonLogin.ForeColor = System.Drawing.Color.Coral;
            this.buttonLogin.Location = new System.Drawing.Point(118, 333);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(88, 33);
            this.buttonLogin.TabIndex = 4;
            this.buttonLogin.Text = "Login";
            this.buttonLogin.UseVisualStyleBackColor = false;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // buttonInapoi
            // 
            this.buttonInapoi.BackColor = System.Drawing.Color.Linen;
            this.buttonInapoi.ForeColor = System.Drawing.Color.Coral;
            this.buttonInapoi.Location = new System.Drawing.Point(477, 333);
            this.buttonInapoi.Name = "buttonInapoi";
            this.buttonInapoi.Size = new System.Drawing.Size(88, 33);
            this.buttonInapoi.TabIndex = 5;
            this.buttonInapoi.Text = "Inapoi";
            this.buttonInapoi.UseVisualStyleBackColor = false;
            this.buttonInapoi.Click += new System.EventHandler(this.buttonInapoi_Click);
            // 
            // labelInregistrare
            // 
            this.labelInregistrare.AutoSize = true;
            this.labelInregistrare.Font = new System.Drawing.Font("Segoe Script", 19.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInregistrare.ForeColor = System.Drawing.Color.Coral;
            this.labelInregistrare.Location = new System.Drawing.Point(310, 29);
            this.labelInregistrare.Name = "labelInregistrare";
            this.labelInregistrare.Size = new System.Drawing.Size(120, 55);
            this.labelInregistrare.TabIndex = 6;
            this.labelInregistrare.Text = "Login";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.maskedTextBoxParola);
            this.panel1.Controls.Add(this.textBoxNumeDeUtilizator);
            this.panel1.Controls.Add(this.labelParola);
            this.panel1.Controls.Add(this.labelNumeDeUtilizator);
            this.panel1.Location = new System.Drawing.Point(118, 109);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(447, 173);
            this.panel1.TabIndex = 7;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ajutorToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(726, 28);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ajutorToolStripMenuItem
            // 
            this.ajutorToolStripMenuItem.Name = "ajutorToolStripMenuItem";
            this.ajutorToolStripMenuItem.Size = new System.Drawing.Size(62, 24);
            this.ajutorToolStripMenuItem.Text = "Ajutor";
            this.ajutorToolStripMenuItem.Click += new System.EventHandler(this.ajutorToolStripMenuItem_Click);
            // 
            // LoginViewState
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(726, 408);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelInregistrare);
            this.Controls.Add(this.buttonInapoi);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "LoginViewState";
            this.Text = "MyAnimeHub";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelNumeDeUtilizator;
        private System.Windows.Forms.Label labelParola;
        private System.Windows.Forms.TextBox textBoxNumeDeUtilizator;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxParola;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Button buttonInapoi;
        private System.Windows.Forms.Label labelInregistrare;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ajutorToolStripMenuItem;
    }
}