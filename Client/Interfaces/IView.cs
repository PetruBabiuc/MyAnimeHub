/**************************************************************************
 *                                                                        *
 *  File:        IView.cs                                                 *
 *  Copyright:   (c) 2022, Elena Chelarasu                                *
 *  Description: The interface that contains all the common methods and   *
 *               properties of the views.                                 *
 *                                                                        *
 **************************************************************************/
using DataClasses;
using System.Collections.Generic;


namespace Interfaces
{
    /// <summary>
    /// The interface of application's view
    /// It exists to ensure that the Inverse Dependency principle is not violated
    /// And to provide an opportunity to further develop the application by adding new views (GUI or even CLI)
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// SetOnly property for assigning the states of the views
        /// </summary>
        List<IViewState> States { set; }

        /// <summary>
        /// Object used by the states for anime data transfer
        /// </summary>
        Anime SelectedAnime { get; set; }

        /// <summary>
        /// Object used by the states for account informations data transfer
        /// </summary>
        UserAccount CurrentAccount { get; set; }

        /// <summary>
        /// Method that starts acquiring user's requests and displaying their results
        /// It should be a blocking infinite loop
        /// </summary>
        void Start();

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
        void ChangeState(IViewState currentState, int nextState);
    }
}
