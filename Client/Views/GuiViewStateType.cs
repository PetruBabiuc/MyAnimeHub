/**************************************************************************
 *                                                                        *
 *  File:        GuiViewStateType.cs                                      *
 *  Copyright:   (c) 2022, Cezar Dondas                                   *
 *  Description: This is the file where the Enum containing the states    *
 *               of the GuiView is defined.                               *
 *                                                                        *
 **************************************************************************/
namespace Views
{
    /// <summary>
    /// Enum that contains all the view states of the GUI View.
    /// Their order matters, each of their values represent the index of the correspondig GUIViewState in the View's list
    /// Updating this enum requires updating the ViewStateFactory class
    /// </summary>
    public enum GuiViewStateType
    {
        Intro, Login, Register, Home, Profile, Search, Anime
    }
}
