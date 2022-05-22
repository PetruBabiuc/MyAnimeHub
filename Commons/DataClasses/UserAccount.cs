/**************************************************************************
 *                                                                        *
 *  File:        UserAccount.cs                                           *
 *  Copyright:   (c) 2022, Cezar Dondas                                   *
 *  Description: Data class that encapsulates the attributes of an user   *
 *               account                                                  *
 *                                                                        *
 **************************************************************************/
using CommonHelpers;
using System;

namespace DataClasses
{
    /// <summary>
    /// The class encapsulating an user account's data
    /// Its properties setters have many checks generating exceptions (of ArgumentException type) if the given values are not valid. 
    /// </summary>
    public class UserAccount
    {
        private string _userName, _password, _name, _mail, _birthDate;
        /// <summary>
        /// Should match the ^[0-9a-zA-Z]{5,20}$ Regex
        /// </summary>
        public string UserName
        {
            set
            {
                if (!RegexUtilities.RegexIsMatchString(@"^[0-9a-zA-Z]{5,20}$", value))
                    throw new ArgumentException("Numele de utilizator trebuie:\n1. Sa contina doar cifre si litere\n2. Sa aiba o lungime intre 5 si 20 de caractere");
                _userName = value;
            }
            get
            {
                return _userName;
            }
        }

        /// <summary>
        /// Should match the ^[0-9a-zA-Z]{5,20}$ Regex
        /// </summary>
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (!RegexUtilities.RegexIsMatchString(@"^[0-9a-zA-Z]{5,20}$", value))
                    throw new ArgumentException("Parola trebuie:\n1. Sa contina doar cifre si litere\n2. Sa aiba o lungime intre 5 si 20 de caractere");
                _password = value;
            }
        }

        /// <summary>
        /// Should match the ^[a-zA-Z]*[0-9]*$ Regex and have the length between 5 and 20 characters
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (!(value.Length >= 5 && value.Length <= 20 && RegexUtilities.RegexIsMatchString(@"^[a-zA-Z]*[0-9]*$", value)))
                    throw new ArgumentException("Numele trebuie:\n1. Sa contina doar cifre si litere\n2. Sa aiba o lungime intre 5 si 20 de caractere\n3. Cifrele, daca exista, sa fie doar la final");
                _name = value;
            }
        }

        /// <summary>
        /// Should match the ^[a-zA-Z][0-9a-zA-Z]+@((yahoo)|(gmail))\.((ro)|(com))$ Regex
        /// </summary>
        public string Mail
        {
            get
            {
                return _mail;
            }
            set
            {
                if (!RegexUtilities.RegexIsMatchString(@"^[a-zA-Z][0-9a-zA-Z]+@((yahoo)|(gmail))\.((ro)|(com))$", value))
                    throw new ArgumentException("Mail-ul trebuie:\n1. Sa contina doar cifre si litere\n2. Sa se termine cu:\n\t2.1. @yahoo.ro\n\t2.2. @yahoo.com\n\t2.3. @gmail.com\n\t2.4. @gmail.ro");
                _mail = value;
            }
        }

        /// <summary>
        /// Should have the DAY/MONTH/YEAR format
        /// The year should not be less than (TheCurrentYear - 100) and above (TheCurrentYear - 14)
        /// </summary>
        public string BirthDate
        {
            get
            {
                return _birthDate;
            }
            set
            {
                string[] stringArray = value.Split("/".ToCharArray());
                if (stringArray.Length != 3)
                    throw new ArgumentException("Data trebuie sa fie in formatul: ZI/LUNA/AN!");
                int d, m, y;
                int[] validDayNumbers = { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
                int currentYear = DateTime.Now.Year;

                if (!int.TryParse(stringArray[2], out y) || y < currentYear - 100 || y > currentYear - 14)
                    throw new ArgumentException($"Anul trebuie sa fie un numar intreg intre {currentYear - 100} si {currentYear - 14}");

                if (!int.TryParse(stringArray[1], out m) || m < 1 || m > 12)
                    throw new ArgumentException("Luna trebuie sa fie un numar intreg intre 1 si 12");

                if (!int.TryParse(stringArray[0], out d) || d < 1 || d > validDayNumbers[m - 1])
                    throw new ArgumentException($"Ziua trebuie sa fie un numar intreg intre 1 si {validDayNumbers[m - 1]} (in cazul lunii a {m}-a)");

                _birthDate = value;
            }
        }
    }
}
