using System;

namespace IridiUploadConsole.Concept.Collection.Server
{
    class Selection
    {
        static void AddSpace(ref string s, string append, int maxLength)
        {
            if (append.Length > maxLength - 1)
                s += append.Substring(0, maxLength - 1) + " ";
            else
            {
                int length = s.Length;
                s += append;

                for (int i = length + append.Length; i < length + maxLength; i++)
                {
                    s += " ";
                }
            }
        }
        static public void Show(Data serverData)
        {
            Program.Log.Critical("Server projects:", ConsoleColor.Gray);
            int counter = 0;
            foreach (var obj in serverData._data.Object.objects)
            {
                foreach (var project in obj.projects)
                {
                    ConsoleColor color = ConsoleColor.Yellow;

                    string consoleLine = "";
                    AddSpace(ref consoleLine, counter++ + "", Data.MaxLengthNumber);

                    if (
                        serverData.Selected.Object == obj.object_name &&
                        serverData.Selected.Project == project.project_name)
                    {
                        AddSpace(ref consoleLine, Data.SelectedTag, Data.SelectedTag.Length + 1);
                        color = ConsoleColor.Green;
                    }
                    else
                    {
                        AddSpace(ref consoleLine, "", Data.SelectedTag.Length + 1);
                    }

                    AddSpace(ref consoleLine, obj.object_name, Data.MaxLengthWord);
                    AddSpace(ref consoleLine, project.project_name, Data.MaxLengthWord);

                    Program.Log.Critical(consoleLine, color);
                }
            }
        }
        static public int Set(Data serverData, int numberLine)
        {
            int counter = 0;
            foreach (var obj in serverData._data.Object.objects)
            {
                foreach (var project in obj.projects)
                {
                    if (numberLine == counter++)
                        {
                        serverData.Selected.Object = obj.object_name;
                        serverData.Selected.Project = project.project_name;
                        serverData.Selected.Id = project.project_id;

                            Program.Log.Notice("Selected object: ", obj.object_name);
                            Program.Log.Notice("Selected project: ", project.project_name);
                            Program.Log.Notice("Selected id: ", project.project_id);

                            return Concept.Result.Successfully;
                        }

                    }
                }
            Program.Log.Warning("Server wrong line project ", "");
            return Concept.Result.ServerWrongLineProject;
        }

        static public void ConsoleWrite(Data serverData)
        {
            Program.Log.Alert("Server projects", ConsoleColor.Gray);
            Program.Log.Alert(serverData.ProjectsCount, ConsoleColor.Red);

            foreach (var obj in serverData._data.Object.objects)
            {
                foreach (var project in obj.projects)
                {
                    Program.Log.Alert(obj.object_name, ConsoleColor.Green);
                    Program.Log.Alert(project.project_name, ConsoleColor.Cyan);
                }
            }
        }
        static public int Get(Data serverData)
        {
            foreach (var obj in serverData._data.Object.objects)
            {
                foreach (var project in obj.projects)
                {
                    if (serverData.Selected.Object == obj.object_name &&
                        serverData.Selected.Project == project.project_name)
                    {
                        serverData.Selected.Id = project.project_id;
                        return Concept.Result.Successfully;
                    }
                }
            }
            Program.Log.Warning("Server cant find project: ", "");
            Program.Log.Warning("Selected object: ", serverData.Selected.Object);
            Program.Log.Warning("Selected project: ", serverData.Selected.Project);

            return Concept.Result.IridiCantFindProject;
        }
    }
}
