using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IridiUploadConsole.Concept.Collection.Server
{
    class Authentication
    {
        static public int Set(Data serverData)
        {
            //Post(string uri, string url, out string[] cookie, string contentType, string payload)
            string[] cookie = default;
            string contentType = "application/x-www-form-urlencoded";
            string payload = "Password=" + serverData.Pass + "&name=authform&Login=admin";

            Program.Log.Notice("Post: ", "authentication on server");
            Program.Log.Notice(serverData.Uri() + Data.Urls.Auth);

            try
            {
                Net.Http.Post(serverData.Uri(), Data.Urls.Auth, out cookie, contentType, payload);
                    }
            catch (Exception) { }
            if (cookie == null)
            {
                Program.Log.Notice("Server is not available", "");
                return Concept.Result.ServerUnabledAccess;
            }

            string sessionId = Net.Parse.Cookie(cookie, Data.SessionKey);
            if (sessionId == "")
            {
                Program.Log.Warning("Server session id is not available", "");
                return Concept.Result.ServerUnabledAccess;
            }

            serverData.IrSessionId = sessionId;
            Program.Log.Informational("sessionId: ", serverData.IrSessionId);

            return Concept.Result.Successfully;
        }
    }
}
