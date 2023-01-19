using System.Text.RegularExpressions;
using System;

namespace IridiUploadConsole.Concept.Collection.Iridi
{
    class Session
    {
        static public int Get(Data iridiData)
        { 
            Program.Log.Informational("Get: Iridi Session", "");
            Program.Log.Informational(Data.Uri + Data.Urls.Main);

            string[] cookie = default;
            string response = "";

            try
            {
                response = Net.Http.Get(Data.Uri, Data.Urls.Main, out cookie);
            }
            catch (Exception) { }

            if (cookie == default || response=="")
            {
                Program.Log.Notice("Iridi is not available", "");
                return Concept.Result.IridiUnabledAccess;
            }

            string phpSessionId = Net.Parse.Cookie(cookie, Data.PhpSessionKey);
            if (phpSessionId == "")
            {
                Program.Log.Warning("Php session id is not available", "");
                return Concept.Result.IridiUnabledAccess;
            }
            iridiData.PhpSessId = phpSessionId;

            string bitrixSessionId = Parse(response);
            if (bitrixSessionId == "")
            {
                Program.Log.Warning("Bitrix session id is not available", "");
                return Concept.Result.IridiUnabledAccess;
            }

            iridiData.SessId = bitrixSessionId;

            Program.Log.Informational("Response: ", "");
            Program.Log.Informational("Php session id: ", iridiData.PhpSessId);
            Program.Log.Informational("Bitrix session id: ", iridiData.SessId);
            return Concept.Result.Successfully;
        }

        static public string Parse(string response)
        {
            string pattern = @"(?<='bitrix_sessid':')(.*)(?=')";
            Regex rg = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection matchedTxt = rg.Matches(response);
            if (matchedTxt.Count > 0)
            {
                if (matchedTxt[0].Value != null && matchedTxt[0].Value != "")
                {
                    return matchedTxt[0].Value;
                }
            }
            return "";
        }
    }
}
