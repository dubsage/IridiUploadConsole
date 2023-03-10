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

namespace IridiUploadConsole.Concept.Collection.Server
{
    class Interface
    {
        static public int Get<T>(Data serverData, out T data, string url) where T : Interface.TBase, new()
        {
            data = default;
            try
            {
                Program.Log.Notice("Get: ", "http to server");
                Program.Log.Notice(serverData.Uri() + url);

                string cookie = Data.SessionKey + serverData.IrSessionId;
                string response = "";
                try
                {
                    response = Net.Http.Get(serverData.Uri(), url, cookie);
                }
                catch (Exception) { }

                if (response == "")
                {
                    Program.Log.Notice("Server is not available", "");
                    return Concept.Result.ServerUnabledAccess;
                }

                data = JsonConvert.DeserializeObject<T>(response);
                if (data != null) data.Trace();
                else
                {
                    Program.Log.Notice("Deserialize object is failed", "");
                    return Concept.Result.ServerUnabledAccess;
                }
            }
            catch (Exception e)
            {
                Program.Log.Notice("Deserialize object is failed", "");
                Program.Log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name + " ", e);
                return Concept.Result.ServerUnabledAccess;
            }
            return Concept.Result.Successfully;
        }

        public TUserUpdate UserUpdate { get; set; }
        public TUser User { get; set; }
        public TObjectUpdate ObjectUpdate { get; set; }
        public TObject Object { get; set; }
        public TObjectDetail ObjectDetail { get; set; }
        public TProjectDownloadedStatus ProjectDownloadedStatus { get; set; }

        public interface TBase
        {
            void Trace();
        }
        public class TError : TBase
        {
            public string error { get; set; }
            public virtual void Trace()
            {
                //Program.Log.Informational("Parsed response:","");
                if (error != null && error != "")
                {
                    Program.Log.Error(error);
                }
            }
        }
        public class TUserUpdate : TError
        {
            public int user_status { get; set; }
            public int update_user_status { get; set; }
            public override void Trace()
            {
                base.Trace();
                Program.Log.Informational("user_status: ", user_status);
                Program.Log.Informational("update_user_status: ", update_user_status);
            }
        }
        public class TUser : TError
        {
            public int user_status { get; set; }
            public string user_last_update { get; set; }
            public string user_login { get; set; }
            public string user_name { get; set; }
            public int test_mode_status { get; set; }
            public int attached_object { get; set; }
            public override void Trace()
            {
                base.Trace();
                Program.Log.Informational("user_status: ", user_status);
                Program.Log.Informational("user_last_update: ", user_last_update);
                Program.Log.Informational("user_login: ", user_login);
                Program.Log.Informational("user_name: ", user_name);
                Program.Log.Informational("test_mode_status: ", test_mode_status);
                Program.Log.Informational("attached_object: ", attached_object);
            }
        }

        public class TObjectUpdate : TError
        {
            public int objects_status { get; set; }
            public override void Trace()
            {
                base.Trace();
                Program.Log.Informational("objects_status: ", objects_status);
            }
        }


        public class TObjectElement
        {
            public int object_id { get; set; }
            public int object_type { get; set; }
            public string object_name { get; set; }
            public TProject[] projects { get; set; }
            public void Trace()
            {
                Program.Log.Informational("\tobject_id: ", object_id);
                Program.Log.Informational("\tobject_type: ", object_type);
                Program.Log.Informational("\tobject_name: ", object_name);
            }
        }
        public class TObject : TError
        {
            public int objects_status { get; set; }
            public string objects_last_update { get; set; }
            //public TObjectElement[] objects { get; set; }
            public TObjectDetail[] objects { get; set; }
            public override void Trace()
            {
                base.Trace();
                Program.Log.Informational("objects_status: ", objects_status);
                Program.Log.Informational("objects_last_update: ", objects_last_update);
                /*
                foreach (TObjectElement objectElement in objects)
                {
                    objectElement.Trace();
                }*/

                Program.Log.Informational("objects:", ConsoleColor.Yellow);
                foreach (TObjectDetail objectElement in objects)
                {
                    //Program.Log.Debug("\tobjects_status: " + objects_status);
                    Program.Log.Informational("\tobject_id: ", objectElement.object_id);
                    Program.Log.Informational("\tobject_type: ", objectElement.object_type);
                    Program.Log.Informational("\tobject_name: ", objectElement.object_name);

                    Program.Log.Informational("\tprojects:", ConsoleColor.Yellow);
                    foreach (TProject project in objectElement.projects)
                    {
                        project.Trace();
                        Program.Log.Informational("\t\t---end---", ConsoleColor.Yellow);
                    }
                    Program.Log.Informational("\t---end---", ConsoleColor.Yellow);
                }
            }
        }

        public class TProject
        {
            public string project_id { get; set; }
            public string project_name { get; set; }
            public string project_upid { get; set; }
            public string project_status { get; set; }
            public void Trace()
            {
                Program.Log.Informational("\tproject_id: ", project_id);
                Program.Log.Informational("\tproject_name: ", project_name);
                Program.Log.Informational("\tproject_upid: ", project_upid);
                Program.Log.Informational("\tproject_status: ", project_status);
            }
        }

        public class TObjectDetail : TError
        {
            public int objects_status { get; set; }
            public int object_id { get; set; }
            public int object_type { get; set; }
            public string object_name { get; set; }
            public TProject[] projects { get; set; }
            public override void Trace()
            {
                base.Trace();
                Program.Log.Informational("objects_status: ",objects_status);
                Program.Log.Informational("object_id: ",object_id);
                Program.Log.Informational("object_type: ",object_type);
                Program.Log.Informational("object_name: ",object_name);

                Program.Log.Informational("projects:", ConsoleColor.Yellow);
                foreach (TProject project in projects)
                {
                    project.Trace();
                    Program.Log.Informational("\t---end---", ConsoleColor.Yellow);
                }
            }
        }

        public class TProjectDownloadedStatus : TError
        {
            public int project_status { get; set; }
            public override void Trace()
            {
                base.Trace();
                Program.Log.Informational("project_status: ", project_status);
            }
            //public string error { get; set; }
        }
    }
}
