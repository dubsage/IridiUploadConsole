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
using System.Collections;
using System.Collections.ObjectModel;
using System.Dynamic;

namespace IridiUploadConsole.Concept.Collection.Iridi
{
    class Upload
    {
        static public int UploadFile(Data iridiData, File.Data fileData)
        {
            int result = fileData.Update();
            if (result != Concept.Result.Successfully) return result;

            NameValueCollection payload = new NameValueCollection();
            payload.Add("user_id", iridiData.UserId);
            payload.Add("posthash", iridiData.PostHash);
            payload.Add("id", iridiData.Selected.Id);
            payload.Add("act", Data.Act);
            payload.Add("project_set_rewrite", Data.ProjectSetRewrite);
            //Guid myuuid = Guid.NewGuid();
            payload.Add("qquuid", Guid.NewGuid().ToString());
            payload.Add("qqfilename", fileData.Name);
            payload.Add("qqtotalfilesize", fileData.Size.ToString());

            Net.UploadSettings settings = new Net.UploadSettings();
            settings.FilePath = fileData.Path;
            settings.Cookie = Data.PhpSessionKey + iridiData.PhpSessId;
            settings.HeaderContentType = Data.HeaderContentType;
            settings.HeaderFileName = fileData.Name;
            settings.HeaderName = Data.HeaderName;
            settings.Uri = Data.Uri;
            settings.Url = Data.Urls.Upload;
            settings.Payload = payload;


            Program.Log.Notice("Iridi Upload file: ", "");
            Program.Log.Notice("Name: ", fileData.Name);
            Program.Log.Notice("Size: ", fileData.Size);
            string response = "";
            try
            {
                response = Net.Http.UploadFile(settings);
            }
            catch (Exception) { }
            if (response == "")
            {
                Program.Log.Notice("Couldn't upload file at address: ", Data.Urls.Upload);
                return Concept.Result.IridiUnabledAccess;
            }
            string pattern = @"(?<=""success"":)(.*?)(?=,)";
            Regex rg = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection matchedTxt = rg.Matches(response);
            if (matchedTxt.Count > 0)
            {
                //Program.Log.Warning("matchedTxt[0].Value: " + matchedTxt[0].Value);
                if (matchedTxt[0].Value == "true")
                {
                    string patternPID = @"(?<=\x22pid\x22:)(\d*?)(?=,)";
                    Regex rgPID = new Regex(patternPID, RegexOptions.IgnoreCase);
                    MatchCollection matchedPID = rgPID.Matches(response);
                    if (matchedPID.Count > 0 && matchedPID[0].Value != null && matchedPID[0].Value != "")
                    {
                        string newPID = matchedPID[0].Value;
                        Program.Log.Warning("Iridi Cloud upload successful.");
                        Program.Log.Warning("\tSelected folder: " + iridiData.Selected.Folder);
                        Program.Log.Warning("\tSelected object: " + iridiData.Selected.Object);
                        Program.Log.Warning("\tSelected project: " + iridiData.Selected.Project);
                        Program.Log.Warning("\tCurrent PID: " + iridiData.Selected.Id);
                        Program.Log.Warning("\tNext PID: " + newPID);
                        return Concept.Result.Successfully;
                    }
                }
            }
            Program.Log.Warning("Iridi Cloud upload failed.");
            Program.Log.Warning("\tSelected folder: " + iridiData.Selected.Folder);
            Program.Log.Warning("\tSelected object: " + iridiData.Selected.Object);
            Program.Log.Warning("\tSelected project: " + iridiData.Selected.Project);
            Program.Log.Warning("\tCurrent PID: " + iridiData.Selected.Id);
            return Concept.Result.IridiUploadFileFailed;
        }
    }
}
