/**************************************************************************
 *                                                                        *
 *  File:        JsonUtilities.cs                                         *
 *  Copyright:   (c) 2022, Petru Babiuc                                   *
 *  Description: The class that contains the serialization and            *
 *               deserialization of objects methods from/to JSON          *
 *               strings.                                                 *
 *                                                                        *
 **************************************************************************/
using Newtonsoft.Json;


namespace CommonHelpers
{
    /// <summary>
    /// The class used to serialize and deserialize object using the JSON format
    /// It uses only public properties of the serialized/deserialized objects
    /// </summary>
    public class JsonUtilities
    {
        /// <summary>
        /// Deserializes an object of the T type
        /// </summary>
        /// <typeparam name="T">
        /// The type of the deserialized object
        /// </typeparam>
        /// <param name="json">
        /// The string containing the JSON where the object is serialized
        /// </param>
        /// <returns></returns>
        public static T FromJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// Function to serialize objects in a string using the JSON format
        /// It ignores the NULL properties
        /// </summary>
        /// <param name="obj">
        /// The serialized object
        /// </param>
        /// <returns>
        /// The string containing the JSON serialization of the object
        /// </returns>
        public static string ToJson(object obj)
        {
            JsonSerializerSettings dontIncludeNullProperties = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            string json = JsonConvert.SerializeObject(obj, Formatting.None, dontIncludeNullProperties);
            return json;
        }
    }
}
