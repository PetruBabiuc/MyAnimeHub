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
using DataClasses;

namespace Views
{
    /// <summary>
    /// The class that encapsulates the view state in which the user sees the matches of the searched anime and can select one for getting more informations
    /// </summary>
    public partial class SearchViewState : Form, IViewState
    {
        private IView _view;
        private IMyAnimeHubDBManager _dbManager;
        private int _pageIndex;

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

        public SearchViewState()
        {
            InitializeComponent();
            FormClosed += new FormClosedEventHandler((a1, a2) => Application.Exit());
            VisibleChanged += new EventHandler((a1, a2) => StateVisibilityChanged());

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
        }

        public void ChangeState(GuiViewStateType nextState)
        {
            _view.ChangeState(this, (int)nextState);
        }

        private void buttonIesire_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonProfil_Click(object sender, EventArgs e)
        {
            ChangeState(GuiViewStateType.Profile);
        }

        private void buttonNoutati_Click(object sender, EventArgs e)
        {
            ChangeState(GuiViewStateType.Home);
        }

        private void UpdateSearchResults()
        {
            Cursor = Cursors.WaitCursor;
            List<Anime> animeList = _dbManager.GetAnimeList(_view.SelectedAnime.Name, _pageIndex);
            Cursor = Cursors.Arrow;
            if (animeList.Count == 0)
            {
                buttonPaginaUrmatoare.Enabled = false;
                if (_pageIndex == 0)
                {
                    MessageBox.Show("Nu a fost gasit nici un rezultat.");
                }
                else
                {
                    --_pageIndex;
                    MessageBox.Show("Nu exista alte rezultate de afisat.");
                }
                return;
            }
            if (_pageIndex > 0)
                buttonPaginaAnterioara.Enabled = true;
            buttonPaginaUrmatoare.Enabled = true;

            int nameHeight = 30;
            int space = 20;
            int currentTop = space;
            int left = 10;
            int width = (panelRezultate.Width - left) * 9 / 10;

            panelRezultate.Controls.Clear();
            foreach (Anime anime in animeList)
            {
                Label labelAnimeName = new Label();
                labelAnimeName.Text = anime.Name;
                labelAnimeName.Top = currentTop;
                labelAnimeName.Height = nameHeight;
                labelAnimeName.Left = left;
                labelAnimeName.ForeColor = Color.Sienna;
                labelAnimeName.Width = width;
                labelAnimeName.Cursor = Cursors.Hand;
                labelAnimeName.Font = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Bold);
                labelAnimeName.Click += new EventHandler((p1, p2) => SelectAnime(anime.Name, anime.ID));

                panelRezultate.Controls.Add(labelAnimeName);

                currentTop += space + nameHeight;
            }
        }

        private void buttonCauta_Click(object sender, EventArgs e)
        {
            _view.SelectedAnime.Name = textBoxCauta.Text;
            UpdateSearchResults();
        }

        private void buttonPaginaAnterioara_Click(object sender, EventArgs e)
        {
            --_pageIndex;
            buttonPaginaUrmatoare.Enabled = true;
            if (_pageIndex == 0)
                buttonPaginaAnterioara.Enabled = false;
            UpdateSearchResults();
        }

        private void StateVisibilityChanged()
        {
            if (!Visible)
                return;
            ResetState();
            UpdateSearchResults();
        }

        private void buttonPaginaUrmatoare_Click(object sender, EventArgs e)
        {
            ++_pageIndex;
            UpdateSearchResults();
        }

        private void ResetState()
        {
            textBoxCauta.Text = "";
            _pageIndex = 0;
            buttonPaginaAnterioara.Enabled = false;
            buttonPaginaUrmatoare.Enabled = true;
        }

        private void SelectAnime(string name, int animeId)
        {
            _view.SelectedAnime.Name = name;
            _view.SelectedAnime.ID = animeId;
            ChangeState(GuiViewStateType.Anime);
        }

        private void ajutorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("MyAnimeHub.chm");
        }
    }
}
