using System;
using System.IO;
using System.Net;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            WebRequest request = WebRequest.Create("https://coderbyte.com/api/challenges/json/age-counting");
            WebResponse response = request.GetResponse();
            var strEqual = "age=";
            var indexOfAge = 0;
            var indexOfEqual = 0;
            var indexOfComma = 0;
            var numberGTE = 0;
            var challengeToken = "s6ofjtk5pa3";
            var result = string.Empty;

            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string strResponse = reader.ReadToEnd();
                //The idea is going trimming the string response until nothing left after read the value I need to extract
                while (strResponse.Length > 10)
                {
                    indexOfAge = strResponse.IndexOf(strEqual);
                    strResponse = strResponse.Remove(0, indexOfAge);
                    indexOfAge = strResponse.IndexOf(strEqual);
                    indexOfEqual = indexOfAge + strEqual.Length;
                    indexOfComma = strResponse.IndexOf(",");
                    if (indexOfComma - indexOfEqual >= 0)
                    {
                        var age = int.Parse(strResponse.Substring(indexOfEqual, indexOfComma - indexOfEqual));
                        if (age >= 50) numberGTE++;
                        if (indexOfComma > 0)
                            strResponse = strResponse.Remove(0, indexOfComma);
                    }
                }
                var resultLength = numberGTE.ToString().Length - 1;
                for (var i = 0; i <= resultLength; i++)
                {
                    result += numberGTE.ToString()[i];
                    if (challengeToken != string.Empty)
                    {
                        if (i < resultLength)
                        {
                            result += challengeToken[0];
                            challengeToken = challengeToken.Remove(0, 1);
                        }
                        else
                        {
                            result += challengeToken;
                            challengeToken = string.Empty;
                        }

                    }
                }
                Console.WriteLine(result);
            }
            response.Close();
        }
    }
}

