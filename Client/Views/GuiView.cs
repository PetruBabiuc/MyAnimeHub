/**************************************************************************
 *                                                                        *
 *  File:        GuiView.cs                                               *
 *  Copyright:   (c) 2022, Elena Chelarasu                                *
 *  Description: The source code of the class GuiView. It is the          *
 *               "Context" component of the implementation of the state   *
 *               machine design pattern.                                  *
 *                                                                        *
 **************************************************************************/
using System.Collections.Generic;
using Interfaces;
using ViewStates;
using System.Windows.Forms;
using DataClasses;

namespace Views
{
    /// <summary>
    /// The View class that encapsulates all the GUI used to communicate with the user
    /// </summary>
    public class GuiView: IView
    {
        /// <summary>
        /// SetOnly property for assigning the states of the views
        /// </summary>
        private List<IViewState> _states;

        /// <summary>
        /// Object used by the states for anime data transfer
        /// </summary>
        private Anime _selectedAnime;

        /// <summary>
        /// Object used by the states for account informations data transfer
        /// </summary>
        private UserAccount _currentAccount;

        public List<IViewState> States
        {
            set
            {
                _states = value;
            }
        }

        public Anime SelectedAnime
        {
            get
            {
                return _selectedAnime;
            }

            set
            {
                _selectedAnime = value;
            }
        }

        public UserAccount CurrentAccount
        {
            get
            {
                return _currentAccount;
            }

            set
            {
                _currentAccount = value;
            }
        }

        public GuiView()
        {
            _selectedAnime = new Anime();

            // The View uses a factory to obtain its states
            States = new ViewStateFactory().CreateStates(this);
        }

        /// <summary>
        /// Method that starts acquiring user's requests and displaying their results
        /// It blocks the current thread by calling Application.Run
        /// </summary>
        public void Start()
        {
            Application.Run((Form)_states[0]);
        }

        /// <summary>
        /// The function used by the view states to change the current state
        /// To other states
        /// </summary>
        /// <param name="currentState">
        /// The current state
        /// </param>
        /// <param name="nextState">
        /// The next state
        /// </param>
        public void ChangeState(IViewState currentState, int nextState)
        {
            currentState.Hide();
            _states[nextState].Show();
        }
    }
}
