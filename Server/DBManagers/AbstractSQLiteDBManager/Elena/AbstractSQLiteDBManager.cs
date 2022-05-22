/**************************************************************************
 *                                                                        *
 *  File:        AbstractSQLiteDBManager.cs                               *
 *  Copyright:   (c) 2022, Elena Chelarasu                                *
 *  Description: The file where the method ExecuteNonQuery of the         *
 *               AbstractSQLiteDBManager class is defined.                *
 *                                                                        *
 **************************************************************************/
using System.Collections.Generic;
using System.Data.SQLite;

namespace DBManagers
{
    public abstract partial class AbstractSQLiteDBManager
    {
        /// <summary>
        /// Functions that executes a non-query command (UPDATE, INSERT etc)
        /// </summary>
        /// <param name="commandString">
        /// The command string
        /// </param>
        /// <param name="parameters">
        /// The Dictionary encapsulating all the parameter names and values used for Prepared Statements
        /// Ex: { "@userId" to 1, "@password" to "strongPass" }
        /// </param>
        protected void ExecuteNonQuery(string commandString, Dictionary<string, object> parameters = null)
        {
            using (SQLiteConnection connection = new SQLiteConnection($"URI=file:{_dbPath}"))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(commandString, connection))
                {
                    if (parameters != null)
                        foreach (KeyValuePair<string, object> entry in parameters)
                            command.Parameters.AddWithValue(entry.Key, entry.Value);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
