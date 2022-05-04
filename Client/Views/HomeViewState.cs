using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Interfaces;
using Views;
using DataClasses;

namespace ViewStates
{
    /// <summary>
    /// The class that encapsulates the view state in which the user can see the news
    /// </summary>
    public partial class HomeViewState : Form, IViewState
    {
        private IView _view;

        /// <summary>
        /// The index of the news page shown
        /// </summary>
        private int _pageIndex;
        private IMyAnimeHubDBManager _dbManager;
        public HomeViewState()
        {
            InitializeComponent();
            _pageIndex = 0;

            // Ensures that the application closes if the form closes
            FormClosed += new FormClosedEventHandler((a1, a2) => Application.Exit());

            // Assures that the state is displayed correctly after it's made visible
            VisibleChanged += new EventHandler((a1, a2) => StateVisibilityChanged());

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
        }


        public IMyAnimeHubDBManager DbManager
        {
            set
            {
                _dbManager = value;
            }
        }

        public IView View
        {
            set
            {
                _view = value;
            }
        }

        /// <summary>
        /// Method used to change the state of the view
        /// </summary>
        /// <param name="nextState">
        /// The next state
        /// </param>
        public void ChangeState(GuiViewStateType nextState)
        {
            _view.ChangeState(this, (int)nextState);
        }

        /// <summary>
        /// Resets the state by, for example emptying text boxes
        /// </summary>
        private void ResetState()
        {
            _pageIndex = 0;
            textBoxCauta.Text = "";
            buttonPaginaAnterioara.Enabled = false;
            buttonPaginaUrmatoare.Enabled = true;
        }

        /// <summary>
        /// Method updates the state if the state became visible
        /// </summary>
        private void StateVisibilityChanged()
        {
            if (!Visible)
                return;
            ResetState();
            UpdateNews();
        }

        /// <summary>
        /// Updates the news page shown
        /// </summary>
        private void UpdateNews()
        {
            Cursor = Cursors.WaitCursor;
            List<News> newsList = _dbManager.GetNews(_pageIndex);
            Cursor = Cursors.Arrow;
            if (newsList.Count == 0)
            {
                buttonPaginaUrmatoare.Enabled = false;
                if (_pageIndex == 0)
                {
                    MessageBox.Show("Nu exista nici o noutate de afisat.");
                }
                else
                {
                    --_pageIndex;
                    MessageBox.Show("Nu exista alte noutati de afisat.");
                }
                return;
            }
            if (_pageIndex > 0)
                buttonPaginaAnterioara.Enabled = true;
            buttonPaginaUrmatoare.Enabled = true;
            int titleHeight = 30;
            int descriptionHeight = 60;
            int space = 5;
            int left = 10;
            int currentTop = space;
            int width = (panelNoutati.Width - left) * 9 / 10;
            panelNoutati.Controls.Clear();
            foreach (News news in newsList)
            {
                Label labelTitle = new Label();
                labelTitle.Text = news.Title;
                labelTitle.Top = currentTop;
                labelTitle.Height = titleHeight;
                labelTitle.Left = left;
                labelTitle.ForeColor = Color.Sienna;
                labelTitle.Width = width;
                labelTitle.Font = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Bold);
                currentTop += space + titleHeight;

                TextBox textBoxDescription = new TextBox();
                textBoxDescription.Text = news.Description;
                textBoxDescription.Top = currentTop;
                textBoxDescription.Left = left;
                textBoxDescription.Height = descriptionHeight;
                textBoxDescription.Width = width;
                textBoxDescription.Multiline = true;
                textBoxDescription.ScrollBars = ScrollBars.Vertical;
                textBoxDescription.ReadOnly = true;
                textBoxDescription.ForeColor = Color.Sienna;
                textBoxDescription.BackColor = Color.FloralWhite;
                currentTop += descriptionHeight + 10 * space;

                panelNoutati.Controls.Add(labelTitle);
                panelNoutati.Controls.Add(textBoxDescription);
            }
        }

        private void buttonPaginaAnterioara_Click(object sender, EventArgs e)
        {
            --_pageIndex;
            buttonPaginaUrmatoare.Enabled = true;
            if (_pageIndex == 0)
                buttonPaginaAnterioara.Enabled = false;
            UpdateNews();
        }

        private void buttonPaginaUrmatoare_Click(object sender, EventArgs e)
        {
            ++_pageIndex;
            UpdateNews();
        }

        private void buttonNoutati_Click(object sender, EventArgs e)
        {
            ResetState();
            UpdateNews();
        }

        private void buttonProfil_Click(object sender, EventArgs e)
        {
            _pageIndex = 0;
            _view.ChangeState(this, (int)GuiViewStateType.Profile);
        }

        private void buttonIesire_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonCauta_Click(object sender, EventArgs e)
        {
            _view.SelectedAnime.Name = textBoxCauta.Text;
            ChangeState(GuiViewStateType.Search);
        }

        private void ajutorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("MyAnimeHub.chm");
        }
    }
}
