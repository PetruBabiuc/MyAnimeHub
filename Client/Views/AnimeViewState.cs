/**************************************************************************
 *                                                                        *
 *  File:        AnimeViewState.cs                                        *
 *  Copyright:   (c) 2022, Petru Babiuc                                   *
 *  Description: Contains the class that encapsulates the state of the    *
 *               application where the user can see and interact with     *
 *               the page of an anime.                                    *
 *                                                                        *
 **************************************************************************/
using DataClasses;
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
    /// The class that encapsulates the view state in which the user
    /// Can see the selected (from the SearchViewState) anime
    /// See its reviews and add a review to it
    /// </summary>
    public partial class AnimeViewState : Form, IViewState
    {
        private IView _view;
        private IMyAnimeHubDBManager _dbManager;

        /// <summary>
        /// The index of the review page shown
        /// </summary>
        private int _reviewsPageIndex;

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

        public AnimeViewState()
        {
            InitializeComponent();
            _reviewsPageIndex = 0;

            // Ensures that the application closes if the form closes
            FormClosed += new FormClosedEventHandler((a1, a2) => Application.Exit());

            // Assures that the state is displayed correctly after it's made visible
            VisibleChanged += new EventHandler((a1, a2) => StateVisibilityChanged());

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
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
        /// Method used to fetch and display informations about the selected anime
        /// </summary>
        private void DisplayAnime()
        {
            Cursor = Cursors.WaitCursor;
            Anime anime = _dbManager.GetSpecificAnime(_view.SelectedAnime.ID);
            Cursor = Cursors.Arrow;

            labelTitlu.Text = anime.Name;
            labelTitlu.Left = (panelAnime.Width - labelTitlu.Width) / 2;

            labelAutor.Text = $"Autor: {anime.Author}";

            if (anime.EpisodesNumber == 0)
                labelEpisoade.Text = "Episoade: inca in desfasurare...";
            else
                labelEpisoade.Text = $"Episoade: {anime.EpisodesNumber}";

            if (anime.AverageRating == 0)
                labelNota.Text = "Nota: nu exista inca nici un review";
            else
                labelNota.Text = $"Nota: {anime.AverageRating:0.00}";

            textBoxDescriere.Text = anime.Description;

            _view.SelectedAnime.ID = anime.ID;
        }

        /// <summary>
        /// Function that loads and shows the reviews of the selected anime
        /// Each review is displayed in a separate panel.
        /// Each of those panels are in the panelReviewuri panel
        /// </summary>
        private void DisplayReviews()
        {
            Cursor = Cursors.WaitCursor;
            List<Review> reviewsList = _dbManager.GetReviews(_view.SelectedAnime.ID, _reviewsPageIndex);
            Cursor = Cursors.Arrow;
            if (reviewsList.Count == 0)
            {
                buttonPaginaUrmatoare.Enabled = false;
                if (_reviewsPageIndex != 0)
                {
                    --_reviewsPageIndex;
                    MessageBox.Show("Nu exista alte review-uri de afisat.");
                }
                return;
            }
            if (_reviewsPageIndex > 0)
                buttonPaginaAnterioara.Enabled = true;
            buttonPaginaUrmatoare.Enabled = true;
    
            // Single review panel constants
            int singleReviewPanelHeight = 40;
            int singleReviewPanelLeft = 10;
            int singleReviewPanelWidth = (panelReviewuri.Width - singleReviewPanelLeft) * 9 / 10;

            // Single review components constants
            int lineHeight = 12;
            int lineLeft = 5;
            int lineWidth = (singleReviewPanelWidth - lineLeft) * 9 / 10;
            int spaceBetweenComponents = 10;
            int componentsCurrentTop;

            int currentTop = 10;

            panelReviewuri.Controls.Clear();
            foreach (Review review in reviewsList)
            {
                componentsCurrentTop = 10;

                Panel singleReviewPanel = new Panel();
                singleReviewPanel.Width = singleReviewPanelWidth;
                singleReviewPanel.Height = 6*lineHeight+componentsCurrentTop+4*spaceBetweenComponents;
                singleReviewPanel.Left = singleReviewPanelLeft;
                singleReviewPanel.Top = currentTop;
                singleReviewPanel.BorderStyle = BorderStyle.FixedSingle;
                panelReviewuri.Controls.Add(singleReviewPanel);
                currentTop += singleReviewPanelHeight + singleReviewPanel.Height;

                Label titleLabel = new Label();
                titleLabel.Text = review.Title;
                titleLabel.AutoSize = true;
                titleLabel.Font = new Font(FontFamily.GenericSansSerif, 8.0F, FontStyle.Bold);
                titleLabel.Top = componentsCurrentTop;
                titleLabel.Height = lineHeight;
                titleLabel.Left = (singleReviewPanel.Width - titleLabel.Width) / 2;
                titleLabel.ForeColor = Color.Sienna;
                singleReviewPanel.Controls.Add(titleLabel);
                componentsCurrentTop += lineHeight + spaceBetweenComponents;

                Label authorLabel = new Label();
                authorLabel.Text = $"Autor: {review.Author}";
                authorLabel.Top = componentsCurrentTop;
                authorLabel.Width = lineWidth;
                authorLabel.Height = lineHeight;
                authorLabel.Left = lineLeft;
                authorLabel.ForeColor = Color.Sienna;
                singleReviewPanel.Controls.Add(authorLabel);
                componentsCurrentTop += lineHeight + spaceBetweenComponents;

                Label ratingLabel = new Label();
                ratingLabel.Text = $"Nota: {review.Rating}";
                ratingLabel.Top = componentsCurrentTop;
                ratingLabel.Width = lineWidth;
                ratingLabel.Height = lineHeight;
                ratingLabel.Left = lineLeft;
                ratingLabel.ForeColor = Color.Sienna;
                singleReviewPanel.Controls.Add(ratingLabel);
                componentsCurrentTop += lineHeight + spaceBetweenComponents;

                TextBox contentTextBox = new TextBox();
                contentTextBox.Text = review.Content;
                contentTextBox.Top = componentsCurrentTop;
                contentTextBox.Left = lineLeft;
                contentTextBox.Height = 3*lineHeight;
                contentTextBox.Width = lineWidth;
                contentTextBox.Multiline = true;
                contentTextBox.ScrollBars = ScrollBars.Vertical;
                contentTextBox.ForeColor = Color.Sienna;
                contentTextBox.BackColor = Color.FloralWhite;
                contentTextBox.ReadOnly = true;
                singleReviewPanel.Controls.Add(contentTextBox);
            }
        }

        private void buttonNoutati_Click(object sender, EventArgs e)
        {
            ChangeState(GuiViewStateType.Home);
        }

        private void buttonProfil_Click(object sender, EventArgs e)
        {
            _view.ChangeState(this, (int)GuiViewStateType.Profile);
        }

        private void buttonIesire_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonPaginaAnterioara_Click(object sender, EventArgs e)
        {
            --_reviewsPageIndex;
            buttonPaginaUrmatoare.Enabled = true;
            if (_reviewsPageIndex == 0)
                buttonPaginaAnterioara.Enabled = false;
            DisplayReviews();
        }

        private void buttonPaginaUrmatoare_Click(object sender, EventArgs e)
        {
            ++_reviewsPageIndex;
            DisplayReviews();
        }

        /// <summary>
        /// Resets the state by, for example emptying text boxes
        /// </summary>
        private void ResetState()
        {
            panelReviewuri.Controls.Clear();
            _reviewsPageIndex = 0;
            buttonPaginaAnterioara.Enabled = false;
            buttonPaginaUrmatoare.Enabled = true;
            textBoxTitlu.Text = "";
            trackBarRating.Value = 1;
            textBoxDescriereAdaugaReview.Text = "";
            textBoxCauta.Text = "";
        }

        /// <summary>
        /// Method updates the state if the state became visible
        /// </summary>
        private void StateVisibilityChanged()
        {
            if (!Visible)
                return;
            ResetState();
            DisplayAnime();
            DisplayReviews();
        }

        private void buttonCauta_Click(object sender, EventArgs e)
        {
            _view.SelectedAnime.Name = textBoxCauta.Text;
            ChangeState(GuiViewStateType.Search);
        }

        /// <summary>
        /// Method that adds or updates the user's review
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdauga_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                string user = _view.CurrentAccount.UserName;
                string pass = _view.CurrentAccount.Password;
                _view.CurrentAccount = _dbManager.GetUserAccount(user, pass);
                Review review = new Review();
                review.Anime = _view.SelectedAnime.Name;
                review.Author = _view.CurrentAccount.Name;
                review.Content = textBoxDescriereAdaugaReview.Text;
                review.Rating = trackBarRating.Value;
                review.Title = textBoxTitlu.Text;
                _dbManager.AddOrReplaceReview(review);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ajutorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("MyAnimeHub.chm");
        }
    }
}
