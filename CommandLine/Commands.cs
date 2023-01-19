using CommandLineTool.Attributes;

namespace IridiUploadConsole.CommandLine
{
    [App("Iridi Upload")]
    class Commands
    {
        [Command("add", "Add transfer project")]
        private static void AddTransfer()
        {
            Program.Data.Add();
        }
        [Command("delete", "Delete last transfer project")]
        private static void DeleteTransfer()
        {
            Program.Data.Delete();
        }

        [Command("set", "Change transfer project")]
        private static void SetTransfer([ParamArgument()] int index)
        {
            Program.Data.SetCurrent(index);
        }

        [Command("ilogin", "Set iridi login")]
        private static int IridiLogin([ParamArgument()] string login)
        {
            Concept.Collection.Transfer transfer = Program.Data.Get();
            if (transfer == null)
            {
                Program.Log.Notice("Couldn't get data", "");
                return Concept.Result.IridiDataIsNotFilled;
            }     
            transfer.IridiData.Login = login;
            return Concept.Result.Successfully;
        }
        [Command("sip", "Set IP at server")]
        private static int ServerIP([ParamArgument()] string ip)
        {
            Concept.Collection.Transfer transfer = Program.Data.Get();
            if (transfer == null)
            {
                Program.Log.Notice("Couldn't get data", "");
                return Concept.Result.IridiDataIsNotFilled;
            }
            transfer.ServerData.IP = ip;
            return Concept.Result.Successfully;
        }
        [Command("ipass", "Set iridi password")]
        private static int IridiPass([ParamArgument()] string pass)
        {
            Concept.Collection.Transfer transfer = Program.Data.Get();
            if (transfer == null)
            {
                Program.Log.Notice("Couldn't get data", "");
                return Concept.Result.IridiDataIsNotFilled;
            }
            transfer.IridiData.Pass = pass;
            return Concept.Result.Successfully;
        }
        [Command("iprojects", "Get projects from iridi")]
        private static int GetIProjects()
        {
            Concept.Collection.Transfer transfer = Program.Data.Get();
            if (transfer == null)
            {
                Program.Log.Notice("Couldn't get data", "");
                return Concept.Result.IridiDataIsNotFilled;
            }

            return transfer.IridiGetProjects();
        }

        [Command("iset", "Select iridi project by line number")]
        private static int SetIProject([ParamArgument()] int numberLine)
        {
            Concept.Collection.Transfer transfer = Program.Data.Get();
            if (transfer == null)
            {
                Program.Log.Notice("Couldn't get data", "");
                return Concept.Result.IridiDataIsNotFilled;
            }

            return transfer.IridiSetProject(numberLine);
        }
        [Command("filepath", "Set file path")]
        private static int SetFilePath([ParamArgument()] string path)
        {
            Concept.Collection.Transfer transfer = Program.Data.Get();
            if (transfer == null)
            {
                Program.Log.Notice("Couldn't get data", "");
                return Concept.Result.IridiDataIsNotFilled;
            }

            return transfer.FilePath(path);
        }

        [Command("ifolder", "Manual set iridi folder")]
        private static int SetIFolder([ParamArgument()] string folder)
        {
            Concept.Collection.Transfer transfer = Program.Data.Get();
            if (transfer == null)
            {
                Program.Log.Notice("Couldn't get data", "");
                return Concept.Result.IridiDataIsNotFilled;
            }

            transfer.IridiData.Selected.Folder = folder;
            return Concept.Result.Successfully;
        }

        [Command("iobject", "Manual set iridi object")]
        private static int SetIObject([ParamArgument()] string obj)
        {
            Concept.Collection.Transfer transfer = Program.Data.Get();
            if (transfer == null)
            {
                Program.Log.Notice("Couldn't get data", "");
                return Concept.Result.IridiDataIsNotFilled;
            }

            transfer.IridiData.Selected.Object = obj;
            return Concept.Result.Successfully;
        }

        [Command("iproject", "Manual set iridi project")]
        private static int SetIProject([ParamArgument()] string project)
        {
            Concept.Collection.Transfer transfer = Program.Data.Get();
            if (transfer == null)
            {
                Program.Log.Notice("Couldn't get data", "");
                return Concept.Result.IridiDataIsNotFilled;
            }

            transfer.IridiData.Selected.Project = project;
            return Concept.Result.Successfully;
        }

        [Command("iupload", "Upload file at iridi")]
        private static int IUploadFile()
        {
            Concept.Collection.Transfer transfer = Program.Data.Get();
            if (transfer == null)
            {
                Program.Log.Notice("Couldn't get data", "");
                return Concept.Result.IridiDataIsNotFilled;
            }

            return transfer.IridiUpload();
        }


        [Command("spass", "Set server password")]
        private static int ServerPass([ParamArgument()] string pass)
        {
            Concept.Collection.Transfer transfer = Program.Data.Get();
            if (transfer == null)
            {
                Program.Log.Notice("Couldn't get data", "");
                return Concept.Result.IridiDataIsNotFilled;
            }
            transfer.ServerData.Pass = pass;
            return Concept.Result.Successfully;
        }
        [Command("sprojects", "Get projects from server")]
        private static int GetSProjects()
        {
            Concept.Collection.Transfer transfer = Program.Data.Get();
            if (transfer == null)
            {
                Program.Log.Notice("Couldn't get data", "");
                return Concept.Result.IridiDataIsNotFilled;
            }

            return transfer.ServerGetProjects();
        }

        [Command("sset", "Select server project by line number")]
        private static int SetSProject([ParamArgument()] int numberLine)
        {
            Concept.Collection.Transfer transfer = Program.Data.Get();
            if (transfer == null)
            {
                Program.Log.Notice("Couldn't get data", "");
                return Concept.Result.IridiDataIsNotFilled;
            }

            return transfer.ServerSetProject(numberLine);
        }


        [Command("sobject", "Manual set server object")]
        private static int SetSObject([ParamArgument()] string obj)
        {
            Concept.Collection.Transfer transfer = Program.Data.Get();
            if (transfer == null)
            {
                Program.Log.Notice("Couldn't get data", "");
                return Concept.Result.IridiDataIsNotFilled;
            }
            
            transfer.ServerData.Selected.Object = obj;
            return Concept.Result.Successfully;
        }

        [Command("sproject", "Manual set server project")]
        private static int SetSProject([ParamArgument()] string project)
        {
            Concept.Collection.Transfer transfer = Program.Data.Get();
            if (transfer == null)
            {
                Program.Log.Notice("Couldn't get data", "");
                return Concept.Result.IridiDataIsNotFilled;
            }

            transfer.ServerData.Selected.Project = project;
            return Concept.Result.Successfully;
        }

        [Command("sdownload", "Download file at server")]
        private static int SDownloadFile()
        {
            Concept.Collection.Transfer transfer = Program.Data.Get();
            if (transfer == null)
            {
                Program.Log.Notice("Couldn't get data", "");
                return Concept.Result.IridiDataIsNotFilled;
            }

            return transfer.ServerDownload();
        }

    }
}
