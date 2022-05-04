
namespace Interfaces
{
    /// <summary>
    /// The interface of all the view states
    /// </summary>
    public interface IViewState
    {
        /// <summary>
        /// SetOnly property that sets the view
        /// </summary>
        IView View { set; }

        /// <summary>
        /// SetOnly property that sets the DBManager
        /// </summary>
        IMyAnimeHubDBManager DbManager { set; }

        /// <summary>
        /// Function that make the state visible and let him acquire user inputs
        /// And transmit them to the server
        /// It should be an infinite loop, exiting only when the state changes
        /// </summary>
        void Show();

        /// <summary>
        /// Function that stops a state from taking inputs from the user
        /// </summary>
        void Hide();
    }
}
