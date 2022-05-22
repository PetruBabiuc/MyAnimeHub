/**************************************************************************
 *                                                                        *
 *  File:        ViewStateFactory.cs                                      *
 *  Copyright:   (c) 2022, Petru Babiuc                                   *
 *  Description: The class used by the GUIView class to instantiate       *
 *               its states correctly.                                    *
 *                                                                        *
 **************************************************************************/
using System.Collections.Generic;
using Interfaces;
using DBManagers;
using Views;

namespace ViewStates
{
    /// <summary>
    /// The factory that creates the states of the GUI View
    /// </summary>
    public class ViewStateFactory
    {
        /// <summary>
        /// Creates the states
        /// </summary>
        /// <param name="view">
        /// The view that will manage the states
        /// </param>
        /// <returns>
        /// The created states
        /// </returns>
        public List<IViewState> CreateStates(IView view)
        {
            IMyAnimeHubDBManager dbManager = new DBManagerClientProxy();
            List<IViewState> list = new List<IViewState>();
            list.Add(new IntroViewState());
            list.Add(new LoginViewState());
            list.Add(new RegisterViewState());
            list.Add(new HomeViewState());
            list.Add(new ProfileViewState());
            list.Add(new SearchViewState());
            list.Add(new AnimeViewState());

            list.ForEach(it => it.View = view);
            list.ForEach(it => it.DbManager = dbManager);

            return list;
        }
    }
}
