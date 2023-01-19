using System;
using System.IO;
using System.Net.Http;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Xml.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
//using Newtonsoft.Json;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO.Compression;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Threading;

namespace IridiUploadConsole.Concept.Collection.Iridi
{
    class Authentication
    {
        static public int Set(Data iridiData)
        {
            if (iridiData.Login == "" || iridiData.Pass == "")
            {
                Program.Log.Notice("Login or password incorrect","");
                Program.Log.Notice("Login: ", iridiData.Login);

                if (iridiData.Pass == "")
                    Program.Log.Notice("Password: ", iridiData.Pass);
                else
                    Program.Log.Notice("Password: ", "***");

                return Concept.Result.IridiDataIsNotFilled;
            }

            Program.Log.Informational("Post: Iridi Auth", "");
            Program.Log.Informational(Data.Uri + Data.Urls.Auth);

            string formItem = "AUTH_FORM=Y&TYPE=AUTH&backurl=%2F&USER_LOGIN="
                + iridiData.Login + "&USER_PASSWORD="
                + iridiData.Pass + "&USER_REMEMBER=Y";

            string cookie = Data.PhpSessionKey + iridiData.PhpSessId;
            string contentType = "application/x-www-form-urlencoded; charset=UTF-8";

            try
            {
                string response = Net.Http.Post(Data.Uri, Data.Urls.Auth, cookie, contentType, formItem);
                iridiData.data = JsonConvert.DeserializeObject<Interface.TIridiAuth>(response);
                //Program.Log.Notice("response: ", response);
            }
            catch (Exception) { }

            if (iridiData.data == null)
            {
                Program.Log.Notice("Deserialize auth object response is failed", "");
                return Concept.Result.IridiUnabledAccess;
            }
            

            if (iridiData.data.status == 0)
            {
                Program.Log.Informational("Status: ", "ok");
                Program.Log.Informational("User id: " + iridiData.data.sess.SESS_AUTH.USER_ID);
                return Concept.Result.Successfully;
            }
            else
            {
                Program.Log.Informational("Status: " + iridiData.data.status);
                Program.Log.Notice("Iridi authentication failed","");
                return Concept.Result.IridiUnabledAuthentication;
            }
        }
    }
}
