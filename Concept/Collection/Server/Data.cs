using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IridiUploadConsole.Concept.Collection.Server
{
    public class TProject
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public int Position { get; set; }
        public TProject()
        {
            Name = "";
            Id = "";
            Position = 0;
        }
    }

    public class TObject
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public int Position { get; set; }
        public List<TProject> Projects { get; set; }
        public TObject()
        {
            Name = "";
            Id = "";
            Position = 0;
            Projects = new List<TProject>();
        }
    }

    class TSelected
    {
        public string Object { get; set; }
        public string Folder { get; set; }
        public string Project { get; set; }
        public string Id { get; set; }
        public TSelected()
        {
            Object = "";
            Folder = "";
            Project = "";
            Id = "";
        }
    }
    class Data
    {
        public class Urls
        {
            //static public readonly string User = "/json/user/cloud/user/get";
            static public readonly string Download = "/json/download/cloud/objects/object/project/download/get?id=";
            static public readonly string ObjectDetail = "/json/projects/cloud/objects/object/get?id=";
            static public readonly string Object = "/json/objects/cloud/objects/get";

            static public readonly string User = "/json/user/cloud/user/get";
            static public readonly string ObjectUpdate = "/json/updateobjects/cloud/objects/update/get";
            static public readonly string Auth = "/html/login";
            static public readonly string UserUpdate = "/json/user_update/cloud/user/update/set";
        }
        static public readonly int MaxLengthNumber = 4;
        static public readonly int MaxLengthWord = 30;
        static public readonly string SelectedTag = "#Sel#";

        public int ProjectsCount = 0;
        public Interface _data = new Interface();
        
        public string Pass = "";
        public string IP = "";
        public string IrSessionId = "";
        public string Uri()
        {
            return "http://" + IP + ":8888";
        }

        static public readonly string SessionKey = "ir-session-id=";

        public TSelected Selected = new TSelected();
        public List<TObject> Objects = new List<TObject>();
    }
}
