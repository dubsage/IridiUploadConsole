using System;

namespace IridiUploadConsole.Arguments
{
    class Process
    {
        static public int Commands(string [] args)
        {
            if (args == null)
            {
            }
            else
            {
                if (args.Length == 3 && args[0] == "get_iridi_projects")
                {
                    Program.Log.ShowDebug = false;
                    Program.Log.ShowWarning = false;
                    Program.Log.ShowNotice = false;
                    Program.Log.ShowInformational = false;
                    Program.Log.ShowError = false;
                    Program.Log.ShowCritical = false;

                    Program.Data.Add();
                    var transfer = Program.Data.Get();

                    transfer.IridiData.Login = args[1];
                    transfer.IridiData.Pass = args[2];
                    int result = transfer.IridiGetProjects();
                    transfer.IridiConsoleWrite();
                    return result;
                }

                if (args.Length == 3 && args[0] == "get_server_projects")
                {
                    Program.Log.ShowDebug = false;
                    Program.Log.ShowWarning = false;
                    Program.Log.ShowNotice = false;
                    Program.Log.ShowInformational = false;
                    Program.Log.ShowError = false;
                    Program.Log.ShowCritical = false;

                    Program.Data.Add();
                    var transfer = Program.Data.Get();

                    transfer.ServerData.IP = args[1];
                    transfer.ServerData.Pass = args[2];
                    int result = transfer.ServerGetProjects();
                    transfer.Server.ConsoleWrite();
                    return result;
                }

                if (args.Length == 7 && args[0] == "upload")
                {
                    Program.Data.Add();
                    var transfer = Program.Data.Get();

                    transfer.IridiData.Login = args[1];
                    transfer.IridiData.Pass = args[2];
                    transfer.IridiData.Selected.Folder = args[3];
                    transfer.IridiData.Selected.Object = args[4];
                    transfer.IridiData.Selected.Project = args[5];
                    transfer.FilePath(args[6]);
                    return transfer.IridiUpload();
                }

                if (args.Length == 11 && args[0] == "upload_and_download")
                {
                    Program.Data.Add();
                    var transfer = Program.Data.Get();

                    transfer.IridiData.Login = args[1];
                    transfer.IridiData.Pass = args[2];
                    transfer.IridiData.Selected.Folder = args[3];
                    transfer.IridiData.Selected.Object = args[4];
                    transfer.IridiData.Selected.Project = args[5];
                    transfer.FilePath(args[6]);

                    transfer.ServerData.IP = args[7];
                    transfer.ServerData.Pass = args[8];
                    transfer.ServerData.Selected.Object = args[9];
                    transfer.ServerData.Selected.Project = args[10];

                    int result = transfer.IridiUpload();
                    if (result != Concept.Result.Successfully) return result;

                    return transfer.ServerDownload();
                }

                if (args.Length > 7 && args[0] == "upload_many")
                {
                    int uploadsValue = 0;
                    try { uploadsValue = Int32.Parse(args[1]); }
                    catch (Exception) { }
                    if (args.Length == 6 * uploadsValue + 2)
                    {
                        for (int i = 0; i < uploadsValue; i++)
                        {
                            Program.Data.Add();
                            var transfer = Program.Data.Get();

                            Program.Log.Alert("Login ", args[i * 6 + 2]);
                            Program.Log.Alert("Pass ", args[i * 6 + 3]);
                            Program.Log.Alert("Folder ", args[i * 6 + 4]);
                            Program.Log.Alert("Object ", args[i * 6 + 5]);
                            Program.Log.Alert("Project ", args[i * 6 + 6]);
                            Program.Log.Alert("FilePath ", args[i * 6 + 7]);

                            transfer.IridiData.Login = args[i * 6 + 2];
                            transfer.IridiData.Pass = args[i * 6 + 3];
                            transfer.IridiData.Selected.Folder = args[i * 6 + 4];
                            transfer.IridiData.Selected.Object = args[i * 6 + 5];
                            transfer.IridiData.Selected.Project = args[i * 6 + 6];
                            transfer.FilePath(args[i * 6 + 7]);


                        }
                        int result = Concept.Result.Successfully;
                        foreach (var t in Program.Data.Transfers)
                        {
                            int currentResult = t.IridiUpload();
                            if (currentResult != Concept.Result.Successfully)
                            {
                                result = currentResult;
                            }
                        }
                        return result;
                    }
                    else
                    {
                        Program.Log.Warning("Wrong console args", "");
                        return Concept.Result.WrongConsoleArgs;
                    }

                }
                if (args.Length > 8 && args[0] == "upload_and_download_many")
                {
                    int uploadsValue = 0;
                    try { uploadsValue = Int32.Parse(args[1]); }
                    catch (Exception) { }

                    int counter = 2;
                    try
                    {
                        for (int i = 0; i < uploadsValue; i++)
                        {
                            int isdownloaded = -1;
                            try { isdownloaded = Int32.Parse(args[counter++]); }
                            catch (Exception) { }
                            if (isdownloaded != 0 && isdownloaded != 1)
                            {
                                Program.Log.Warning("Wrong console args. ", "Is downloaded: " + isdownloaded);
                                return Concept.Result.WrongConsoleArgs;
                            }
                            Program.Data.Add();
                            var transfer = Program.Data.Get();
                            transfer.IsDownloaded = isdownloaded;
                            transfer.IridiData.Login = args[counter++];
                            transfer.IridiData.Pass = args[counter++];
                            transfer.IridiData.Selected.Folder = args[counter++];
                            transfer.IridiData.Selected.Object = args[counter++];
                            transfer.IridiData.Selected.Project = args[counter++];
                            transfer.FilePath(args[counter++]);
                            if (isdownloaded == 1)
                            {
                                transfer.ServerData.IP = args[counter++];
                                transfer.ServerData.Pass = args[counter++];
                                transfer.ServerData.Selected.Object = args[counter++];
                                transfer.ServerData.Selected.Project = args[counter++];
                            }
                        }
                    }
                    catch (Exception) {
                        Program.Log.Warning("Wrong console args", "");
                        return Concept.Result.WrongConsoleArgs;
                    }

                        int result = Concept.Result.Successfully;
                        foreach (var t in Program.Data.Transfers)
                        {
                            int currentResult = t.IridiUpload();
                            if (currentResult != Concept.Result.Successfully)
                            {
                                result = currentResult;
                            }
                            else if (t.IsDownloaded == 1)
                            {
                                currentResult = t.ServerDownload();
                                if (currentResult != Concept.Result.Successfully)
                                {
                                    result = currentResult;
                                }
                            }
                        }
                        return result;
                    }
                else
                {
                    Program.Log.Warning("Wrong console args", "");
                    return Concept.Result.WrongConsoleArgs;
                }

            }
            return Concept.Result.Successfully;
        }
            
        
    }
}
