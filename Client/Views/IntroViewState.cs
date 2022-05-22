/**************************************************************************
 *                                                                        *
 *  File:        IntroViewState.cs                                        *
 *  Copyright:   (c) 2022, Maria Lupu                                     *
 *  Description: The source file contains the initial view state. From    *
 *               this state the user has the possibility to go in the     *
 *               log in state or the register state.                      *
 *                                                                        *
 **************************************************************************/
using System;
using System.Windows.Forms;
using Interfaces;
using Views;

namespace ViewStates
{
    /// <summary>
    /// The class that encapsulates the view state in which the user is welcomed
    /// </summary>
    public partial class IntroViewState : Form, IViewState
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

        public IntroViewState()
        {
            InitializeComponent();
            FormClosed += new FormClosedEventHandler((a1, a2) => Application.Exit());

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            ChangeState(GuiViewStateType.Login);
        }

        private void buttonInregistrare_Click(object sender, EventArgs e)
        {
            ChangeState(GuiViewStateType.Register);
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
