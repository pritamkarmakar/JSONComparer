using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PK.JsonComparer
{
    public class JsonComparer : IComparer<string>
    {
        /// <summary>
        /// Compare two strings
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(string x, string y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            if (x == y) return 0;

            return -1;
        }

        /// <summary>
        /// Method to replace the values with empty Guid
        /// </summary>
        /// <param name="original">original string</param>
        /// <param name="replacements">array of values to replace</param>
        /// <returns>return updated string</returns>
        private string ReplaceGuid(string original, string[] replacements)
        {
            for (int i = 0; i < replacements.Count(); i++)
            {
                original = original.Replace(replacements[i], Guid.Empty.ToString());
            }
            return original;
        }

        /// <summary>
        /// Method to replace the values with empty Guid
        /// </summary>
        /// <param name="original">original string</param>
        /// <param name="replacements">array of values to replace</param>
        /// <returns>return updated string</returns>
        private string ReplaceWithGivenValues(string original, string[] replacements, string[] values)
        {
            for (int i = 0; i < replacements.Count(); i++)
            {
                original = original.Replace(replacements[i], values[i]);
            }
            return original;
        }

        /// <summary>
        /// Method to find the values of all the properties that we have to replace
        /// </summary>
        /// <param name="json"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        private string[] ReplacementBuilder(string json, string[] keys)
        {
            HashSet<string> retval = new HashSet<string>();

            for (int i = 0; i < keys.Count(); i++)
            {
                MatchCollection matchCollection = Regex.Matches(json, '"' + keys[i] + '"');
                if (matchCollection.Count > 0)
                {
                    for (int j = 0; j < matchCollection.Count; j++)
                    {
                        var indexOfColon = json.IndexOf(':', matchCollection[j].Index);

                        // key values can be in these formats -
                        //"key":["value1","value2"]
                        //"key":"value"
                        // so we will check what is the character post ':'. and will find the end index of that character and accordingly will retrieve the value
                        char c = json[indexOfColon + 1];
                        var endIndexOfC = json.IndexOf(c, indexOfColon + 2);
                        //var endIndexOfValue = json.IndexOf(',', endIndexOfKey);
                        retval.Add(json.Substring(indexOfColon + 2, endIndexOfC - indexOfColon - 2));
                    }
                }
            }
            return retval.ToArray();
        }

        /// <summary>
        /// Method to replace json property value with empty guid
        /// </summary>
        /// <param name="json">input json string</param>
        /// <param name="keys">all the keys that we want to replace</param>
        /// <returns>return the string after replacement</returns>
        public string ReplaceValues(string json, string[] keys)
        {
            if (String.IsNullOrEmpty(json))
                return null;
            return ReplaceGuid(json, ReplacementBuilder(json, keys));
        }

        /// <summary>
        /// Method to replace json property values with given values.
        /// In the 'values' array object keep the value of a key in same index
        /// </summary>
        /// <param name="json">input json string</param>
        /// <param name="keys">all the keys that we want to replace</param>
        /// <param name="values">corresponding values of the key in same index</param>
        /// <returns>return the string after replacement</returns>
        public string ReplaceValues(string json, string[] keys, string[] values)
        {
            if (String.IsNullOrEmpty(json))
                return null;
            return ReplaceWithGivenValues(json, ReplacementBuilder(json, keys), values);
        }
    }
}
