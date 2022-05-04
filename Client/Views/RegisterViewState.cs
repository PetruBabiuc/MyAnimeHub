using System;
using System.Windows.Forms;
using Interfaces;
using Views;
using DataClasses;

namespace ViewStates
{
    /// <summary>
    /// The class that encapsulates the view state in which the user can register
    /// </summary>
    public partial class RegisterViewState : Form, IViewState
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

        public RegisterViewState()
        {
            InitializeComponent();
            FormClosed += new FormClosedEventHandler((a1, a2) => Application.Exit());

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
        }

        public void ChangeState(GuiViewStateType nextState)
        {
            _view.ChangeState(this, (int)nextState);
        }

        private void buttonInregistrare_Click(object sender, EventArgs e)
        {
            try
            {
                UserAccount account = new UserAccount();
                account.UserName = textBoxNumeDeUtilizator.Text;
                account.Password = maskedTextBoxParola.Text;
                account.Name = textBoxNume.Text;

                // The mail ant the birth date are optional
                if (textBoxMail.Text.Length > 0)
                {
                    account.Mail = textBoxMail.Text;
                }

                if (textBoxZi.Text.Length > 0 &&
                    textBoxLuna.Text.Length > 0 &&
                    textBoxAn.Text.Length > 0)
                {
                    account.BirthDate = $"{textBoxZi.Text}/{textBoxLuna}/{textBoxAn}";
                }
                if(_dbManager.RegisterUserAccount(account))
                    ChangeState(GuiViewStateType.Intro);
            }
            catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonInapoi_Click(object sender, EventArgs e)
        {
            ChangeState((int)GuiViewStateType.Intro);
        }

        private void ajutorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("MyAnimeHub.chm");
        }
    }
}
