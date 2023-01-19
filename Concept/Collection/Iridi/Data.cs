using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IridiUploadConsole.Concept.Collection.Iridi
{
    public class TFolder
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public int Position { get; set; }
        public List<TObject> Objects { get; set; }
        public TFolder()
        {
            Name = "";
            Id = "";
            Position = 0;
            Objects = new List<TObject>();
        }
    }

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
            static public readonly string Main = "/";
            static public readonly string Upload = "/bongo_v2/endpoint.php";
            static public readonly string Auth = "/signup/auth_ajax.php";
            static public readonly string Cloud = "/my-account/cloud/";
        }

        static public readonly string Uri = "https://iridi.com";
        static public readonly string HeaderName = "qqfile";
        static public readonly string HeaderContentType = "application/octet-stream";
        static public readonly string PhpSessionKey = "PHPSESSID=";
        static public readonly string Act = "reload";
        static public readonly string ProjectSetRewrite = "skip_rewrite";

        static public readonly string SelectedTag = "#SEL#";

        static public readonly int MaxLengthNumber = 4;
        static public readonly int MaxLengthWord = 30;

        public int ObjectsCount = 0;
        public int ProjectsCount = 0;
        public Interface.TIridiAuth data { get; set; }
        public string Pass = "";
        public string Login = "";
        public string PhpSessId = "";
        public string SessId = "";
        public string PostHash = "";
        public string UserId = "";

        public TSelected Selected = new TSelected();
        public List<TFolder> Folders = new List<TFolder>();
    }
}
