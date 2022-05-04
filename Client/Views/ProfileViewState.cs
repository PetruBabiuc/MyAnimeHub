using DataClasses;
using Views;
using Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Views
{
    /// <summary>
    /// The class that encapsulates the view state in which the user is able to see his profile and update it
    /// </summary>
    public partial class ProfileViewState : Form, IViewState
    {
        private IView _view;
        private IMyAnimeHubDBManager _dbManager;

        public IView View
        {
            set
            {
                _view = value;
            }
        }

        public IMyAnimeHubDBManager DbManager
        {
            set
            {
                _dbManager = value;
            }
        }

        public ProfileViewState()
        {
            InitializeComponent();
            FormClosed += new FormClosedEventHandler((a1, a2) => Application.Exit());
            Shown += new EventHandler((a1, a2) => FillProfileFields());

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
        }

        private void buttonActualizeaza_Click(object sender, EventArgs e)
        {
            try
            {
                UserAccount account = new UserAccount();
                account.UserName = textBoxNumeDeUtilizator.Text;
                account.Password = maskedTextBoxParola.Text;
                account.Name = textBoxNume.Text;

                // The mail and date of birth are optional
                if (textBoxMail.Text.Length > 0)
                    account.Mail = textBoxMail.Text;
                if (textBoxZi.Text.Length > 0 &&
                   textBoxLuna.Text.Length > 0 &&
                   textBoxAn.Text.Length > 0) 
                {
                    account.BirthDate = $"{textBoxZi.Text}/{textBoxLuna.Text}/{textBoxAn.Text}";
                }
                if (_dbManager.UpdateProfile(account))
                    _view.CurrentAccount = account;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonNoutati_Click(object sender, EventArgs e)
        {
            ChangeState(GuiViewStateType.Home);
        }

        public void ChangeState(GuiViewStateType nextState)
        {
            _view.ChangeState(this, (int)nextState);
        }

        /// <summary>
        /// Loads the user's profile into the fields
        /// </summary>
        private void FillProfileFields()
        {
            UserAccount account = _view.CurrentAccount;
            Cursor = Cursors.WaitCursor;
            account = _dbManager.GetUserAccount(account.UserName, account.Password);
            Cursor = Cursors.Default;
            textBoxNumeDeUtilizator.Text = account.UserName;
            maskedTextBoxParola.Text = account.Password;
            textBoxNume.Text = account.Name;
            if(account.BirthDate!=null)
            {
                string[] stringArray = account.BirthDate.Split("/".ToCharArray());
                textBoxZi.Text = stringArray[0];
                textBoxLuna.Text = stringArray[1];
                textBoxAn.Text = stringArray[2];
            }
            if (account.Mail != null)
                textBoxMail.Text = account.Mail;
        }

        private void buttonIesire_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ajutorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("MyAnimeHub.chm");
        }
    }
}
