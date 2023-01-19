using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace IridiUploadConsole.Concept.Collection.Iridi
{
    class Projects
    {
        static public int Get(Data iridiData)
        {
            

            string cookie = Data.PhpSessionKey + iridiData.PhpSessId;
            string contentType = "application/x-www-form-urlencoded";
            //string response = Net.Http.Get(Data.Uri, Data.Urls.Cloud, cookie);

            iridiData.ProjectsCount = 0;
            foreach (var folder in iridiData.Folders)
            {
                //Program.Log.Informational("Iridi folder: " + folder.name);
                //Program.Log.Informational("Number: " + folder.id);
                foreach (var obj in folder.Objects)
                {
                    //Program.Log.Informational("\tIridi object: " + obj.name);
                    //Program.Log.Informational("\tNumber: " + obj.id);
                    Program.Log.Informational("Get: Iridi Projects", "");
                    Program.Log.Informational(Data.Uri + Data.Urls.Cloud);

                    string payload = "ID=" + obj.Id + "&tab=11&ajax=Y&act=get_object&sessid=" + iridiData.SessId;

                    //string cookie = Data.PhpSessionKey + iridiData.PhpSessId;
                    string response = "";
                    try
                    {
                        response = Net.Http.Post(Data.Uri, Data.Urls.Cloud, cookie, contentType, payload);
                    }
                    catch (Exception) { }
                    if (response == "")
                    {
                        Program.Log.Notice("Couldn't get HTML at address: ", obj.Id);
                        return Concept.Result.IridiUnabledAccess;
                    }

                    //string html = _http.GetProjectDefault(obj.id);

                    string projects = @"(?<=data-val="")(.*?)(?="").*\n.*(?<=data-id="")(.*?)(?="")";
                    Regex rg_projects = new Regex(projects, RegexOptions.IgnoreCase);
                    MatchCollection matched_projects = rg_projects.Matches(response);

                    //List<Interface.TProject> projectsI = new List<Interface.TProject>();
                    foreach (Match match in matched_projects)
                    {
                        if (match.Groups.Count == 3)
                        {
                            var project = new Iridi.TProject();
                            project.Name = match.Groups[1].Value;
                            project.Id = match.Groups[2].Value;
                            project.Position = match.Index;
                            /*
                            Interface.TProject projectI = new Interface.TProject();
                            projectI.name = match.Groups[1].Value;
                            projectI.id = match.Groups[2].Value;
                            projectI.position = match.Index;*/
                            obj.Projects.Add(project);
                            iridiData.ProjectsCount++;
                            //projectsI.Add(projectI);
                            //ProjectsCount++;
                        }
                    }
                    //obj.projects = projectsI.ToArray();
                    //Program.Log.Colour();
                }
            }

            Program.Log.Debug("Response:","");
            Program.Log.Debug("Iridi cloud folders: ",ConsoleColor.Yellow);
            foreach (var folder in iridiData.Folders)
            {
                Program.Log.Debug("\tfolder name: " + folder.Name);
                Program.Log.Debug("\tfolder id: " + folder.Id);
                Program.Log.Debug("\tIridi cloud objects: ", ConsoleColor.Yellow);
                foreach (var obj in folder.Objects)
                {
                    Program.Log.Debug("\t\tobject name: " + obj.Name);
                    Program.Log.Debug("\t\tobject id: " + obj.Id);
                    Program.Log.Debug("\t\tIridi cloud projects: ", ConsoleColor.Yellow);
                    foreach (var project in obj.Projects)
                    {
                        Program.Log.Debug("\t\t\tproject name: " + project.Name);
                        Program.Log.Debug("\t\t\tproject id: " + project.Id);
                        Program.Log.Debug("\t\t\t---end---", ConsoleColor.Yellow);
                    }
                    Program.Log.Debug("\t\t---end---", ConsoleColor.Yellow);
                }
                Program.Log.Debug("\t---end---", ConsoleColor.Yellow);
            }

            return Concept.Result.Successfully;
        }
    }
}
