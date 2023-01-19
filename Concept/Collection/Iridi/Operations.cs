using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IridiUploadConsole.Concept.Collection.Iridi
{
    class Operations
    {
        Iridi.Data _iridiData { get; set; }
        File.Data _fileData { get; set; }
        public Operations(Iridi.Data iridiData, File.Data fileData)
        {
            if (iridiData == null) _iridiData = new Iridi.Data();
            else _iridiData = iridiData;

            if (fileData == null) _fileData = new File.Data();
            else _fileData = fileData;
        }
        public Operations()
        {
            _iridiData = new Iridi.Data();
            _fileData = new File.Data();
        }

        public int GetSession()
        {
            return Session.Get(_iridiData);
        }
        public int Authentication()
        {
            return Iridi.Authentication.Set(_iridiData);
        }
        public int GetObjects()
        {
            return Iridi.Objects.Get(_iridiData);
        }
        public int GetProjects()
        {
            return Iridi.Projects.Get(_iridiData);
        }
        public int Upload()
        {
            return Iridi.Upload.UploadFile(_iridiData, _fileData);
        }
        public void Selection()
        {
            Iridi.Selection.Show(_iridiData);
            //Iridi.Selection.ShowInDetail(_iridiData);
        }

        public int SetSelection(int numberLine)
        {
            int result = Iridi.Selection.Set(_iridiData, numberLine);
            Iridi.Selection.Show(_iridiData);
            return result;
        }

        public int GetSelection()
        {
            int result = Iridi.Selection.Get(_iridiData);
            //Iridi.Selection.Get(_iridiData);
            return result;
        }

        public void ConsoleWrite()
        {
            Iridi.Selection.ConsoleWrite(_iridiData);
        }
    }
}
