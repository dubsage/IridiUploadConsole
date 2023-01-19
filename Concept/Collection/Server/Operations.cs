using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IridiUploadConsole.Concept.Collection.Server
{
    class Operations
    {
        Server.Data _serverData { get; set; }
        public Operations(Server.Data serverData)
        {
            if (serverData == null) _serverData = new Server.Data();
            else _serverData = serverData;
        }
        public Operations()
        {
            _serverData = new Server.Data();
        }

        public int Authentication()
        {
            return Server.Authentication.Set(_serverData);
        }
        public int GetProjects()
        {
            return Server.Projects.GetProjects(_serverData);
        }
        public void ShowProjects()
        {
            Server.Selection.Show(_serverData);
        }
        public int SetProject(int numberLine)
        {
            return Server.Selection.Set(_serverData, numberLine);
        }
        public int GetProject()
        {
            return Server.Selection.Get(_serverData);
        }
        public void ConsoleWrite()
        {
            Server.Selection.ConsoleWrite(_serverData);
        }
        public int Download()
        {
            return Server.Download.Process(_serverData);
        }
    }
}
