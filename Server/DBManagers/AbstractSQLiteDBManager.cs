using System.Collections.Generic;
using System.Data.SQLite;

namespace DBManagers
{
    /// <summary>
    /// Class that gives basic operations to work with a SQLite Database
    /// </summary>
    public abstract class AbstractSQLiteDBManager
    {
        private string _dbPath;

        /// <summary>
        /// Primary constructor
        /// </summary>
        /// <param name="dbPath">
        /// The ABSOLUTE path to the data base
        /// </param>
        public AbstractSQLiteDBManager(string dbPath)
        {
            _dbPath = dbPath;
        }

        /// <summary>
        /// Function that executes a query
        /// </summary>
        /// <param name="query">
        /// The query string
        /// </param>
        /// <param name="parameters">
        /// The Dictionary encapsulating all the parameter names and values used for Prepared Statements
        /// Ex: { "@userId" to 1, "@password" to "strongPass" }
        /// </param>
        /// <returns>
        /// The SQLiteDataReader from which the query results can be read
        /// </returns>
        protected SQLiteDataReader ExecuteQuery(string query, Dictionary<string, object> parameters = null)
        {
            SQLiteConnection connection = new SQLiteConnection($"URI=file:{_dbPath}");
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(query, connection);
            if (parameters != null)
                foreach (KeyValuePair<string, object> entry in parameters)
                    command.Parameters.AddWithValue(entry.Key, entry.Value);

            // When the SQLiteDataReader is closed, the connection is also closed
            return command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
        }

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
            using(SQLiteConnection connection = new SQLiteConnection($"URI=file:{_dbPath}"))
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
