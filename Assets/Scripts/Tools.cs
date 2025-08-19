using UnityEngine;
using System.Collections.Generic;

public class Tools
{
    public static string Parametrize(string baseString, Dictionary<string, int> substitutions)
    {
        string result = "";
        foreach (KeyValuePair<string, int> substitution in substitutions)
        {
            result = baseString.Replace(substitution.Key, substitution.Value.ToString());
        }

        return result;
        
    }
}
