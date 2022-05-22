/**************************************************************************
 *                                                                        *
 *  File:        LoginViewState.cs                                        *
 *  Copyright:   (c) 2022, Cezar Dondas                                   *
 *  Description: The state of the client where the user can use his       *
 *               account's credentials to log in.                         *
 *                                                                        *
 **************************************************************************/
using System;
using System.Windows.Forms;
using Interfaces;
using Views;
using DataClasses;

namespace ViewStates
{
    /// <summary>
    /// The class that encapsulates the view state in which the user logs in
    /// </summary>
    public partial class LoginViewState : Form, IViewState
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

        public LoginViewState()
        {
            InitializeComponent();
            FormClosed += new FormClosedEventHandler((a1, a2) => Application.Exit());

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            try
            {
                UserAccount account = new UserAccount();
                account.UserName = textBoxNumeDeUtilizator.Text;
                account.Password = maskedTextBoxParola.Text;
                if (_dbManager.Login(account.UserName, account.Password))
                {
                    _view.CurrentAccount = account;
                    ChangeState(GuiViewStateType.Home);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonInapoi_Click(object sender, EventArgs e)
        {
            ChangeState(GuiViewStateType.Intro);
        }

        public void ChangeState(GuiViewStateType nextState)
        {
            _view.ChangeState(this, (int)nextState);
        }

        private void ajutorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("MyAnimeHub.chm");
        }
    }
}
