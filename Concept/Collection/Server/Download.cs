using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IridiUploadConsole.Concept.Collection.Server
{
    class Download
    {
        static public int Process(Data serverData)
        {
            Interface.TProjectDownloadedStatus downloadStatus;
            int result = Interface.Get<Interface.TProjectDownloadedStatus>(serverData, out downloadStatus, Data.Urls.Download + serverData.Selected.Id);
            serverData._data.ProjectDownloadedStatus = downloadStatus;
            if (result != Concept.Result.Successfully)
            {
                return result;
            }
            if (serverData._data.ProjectDownloadedStatus.project_status == 1)
            {
                Program.Log.Notice("Project downloaded");
            }
            else
            {
                Program.Log.Notice("Project didnt downloaded. Status: " + serverData._data.ProjectDownloadedStatus.project_status);
                return Concept.Result.ServerDownloadFileFailed;
            }
            return Concept.Result.Successfully;
        }
    }
}
