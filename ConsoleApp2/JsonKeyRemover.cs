using System;
using System.IO;
using System.Net;

public class JsonKeyRemover
{

    public static void RemoveJsonEntries()
    {

        WebRequest request = WebRequest.Create("https://coderbyte.com/api/challenges/json/json-cleaning");
        WebResponse response = request.GetResponse();
        Stream dataStream = response.GetResponseStream();
        StreamReader reader = new StreamReader(dataStream);

        var responseString = reader.ReadToEnd();

        responseString = responseString.Replace(@"""""", @""" """);
        var purgedString = ReplaceCharacterOnArray(responseString, "-");
        purgedString = ReplaceCharacterOnArray(purgedString, " ");
        purgedString = ReplaceCharacterOnObject(purgedString, @"N\/A");
        purgedString = ReplaceCharacterOnObject(purgedString, @"-");
        purgedString = ReplaceCharacterOnObject(purgedString, @" ");


        Console.WriteLine(purgedString);
        Console.ReadLine();

        response.Close();
        dataStream.Close();
        reader.Close();

    }

    public static string ReplaceCharacterOnObject(string stringToRepalce, string valueToReplace)
    {
        var indexOfKeyValue = stringToRepalce.IndexOf($@"{valueToReplace}");
        var postSuffix = string.Empty;

        if (stringToRepalce[indexOfKeyValue + valueToReplace.Length + 1] == ',') postSuffix = ",";

        if (indexOfKeyValue < 2) return stringToRepalce;

;        if (stringToRepalce[indexOfKeyValue - 2] == ':')
        {
            var sufix = stringToRepalce.Substring(indexOfKeyValue - 1, valueToReplace.Length + 2);
            var prefix = "";
            var counter = 0;
            var position = -2;
            while (counter < 2)
            {
                prefix += stringToRepalce[indexOfKeyValue + position];
                if (stringToRepalce[indexOfKeyValue + position] == '"')
                    counter++;
                position--;
            }
            return stringToRepalce.Replace(Reverse(prefix) + sufix + postSuffix, "");
        }
        return stringToRepalce;
    }
    public static string ReplaceCharacterOnArray(string stringToReplace, string valueToReplace)
    {
        var result = stringToReplace.Replace($@",""{valueToReplace}""]", "]");
        result = result.Replace($@",""{valueToReplace}"",", ",");
        result = result.Replace($@"[""{valueToReplace}"",", "[");
        result = result.Replace($@"[""{valueToReplace}""]", "");
        return result;
    }

    public static string Reverse(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

}