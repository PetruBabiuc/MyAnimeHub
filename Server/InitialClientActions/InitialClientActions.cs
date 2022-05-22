/**************************************************************************
 *                                                                        *
 *  File:        InitialClientActions.cs                                  *
 *  Copyright:   (c) 2022, Petru Babiuc                                   *
 *  Description: The enum of the possible initial actions of the          *
 *               client.                                                  *
 *                                                                        *
 **************************************************************************/
namespace InitialClientActions
{
    /// <summary>
    /// Enum of the actions that users can make before logging in
    /// </summary>
    public enum InitialAction { LoginSuccessful, LoginFailed, RegistrationSuccessful, RegistrationFailed};
}
