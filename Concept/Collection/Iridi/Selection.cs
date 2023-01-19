using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IridiUploadConsole.Concept.Collection.Iridi
{
    class Selection
    {
        static public void ShowInDetail(Data iridiData)
        {
            int counter = 0;
            foreach (var folder in iridiData.Folders)
            {
                foreach (var obj in folder.Objects)
                {
                    foreach (var project in obj.Projects)
                    {
                        ConsoleColor color = ConsoleColor.Yellow;
                        string consoleLine = counter++ + "";

                        if (iridiData.Selected.Folder == folder.Name &&
                            iridiData.Selected.Object == obj.Name &&
                            iridiData.Selected.Project == project.Name)
                        {
                            consoleLine += Data.SelectedTag;
                            color = ConsoleColor.Green;
                        }

                        consoleLine += "\t" + folder.Name + "\t" + folder.Id + "\t";
                        consoleLine += obj.Name + "\t" + obj.Id + "\t";
                        consoleLine += project.Name + "\t" + project.Id;

                        Program.Log.Critical(consoleLine, color);
                    }
                }
            }
        }

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
        static public void Show(Data iridiData)
        {
            int counter = 0;
            Program.Log.Critical("Iridi projects:", ConsoleColor.Gray);
            foreach (var folder in iridiData.Folders)
            {
                foreach (var obj in folder.Objects)
                {
                    foreach (var project in obj.Projects)
                    {
                        ConsoleColor color = ConsoleColor.Yellow;

                        string consoleLine = "";
                        AddSpace(ref consoleLine, counter++ + "", Data.MaxLengthNumber);

                        if (iridiData.Selected.Folder == folder.Name &&
                            iridiData.Selected.Object == obj.Name &&
                            iridiData.Selected.Project == project.Name)
                        {
                            AddSpace(ref consoleLine, Data.SelectedTag, Data.SelectedTag.Length + 1);
                            color = ConsoleColor.Green;
                        }
                        else
                        {
                            AddSpace(ref consoleLine, "", Data.SelectedTag.Length + 1);
                        }

                        AddSpace(ref consoleLine, folder.Name, Data.MaxLengthWord);
                        AddSpace(ref consoleLine, obj.Name, Data.MaxLengthWord);
                        AddSpace(ref consoleLine, project.Name, Data.MaxLengthWord);

                        Program.Log.Critical(consoleLine, color);
                    }
                }
            }
        }
        static public int Set(Data iridiData, int numberLine)
        {
            int counter = 0;
            foreach (var folder in iridiData.Folders)
            {
                foreach (var obj in folder.Objects)
                {
                    foreach (var project in obj.Projects)
                    {
                        if (numberLine == counter++)
                        {
                            iridiData.Selected.Folder = folder.Name;
                            iridiData.Selected.Object = obj.Name;
                            iridiData.Selected.Project = project.Name;
                            iridiData.Selected.Id = project.Id;

                            Program.Log.Notice("Selected folder: ", folder.Name);
                            Program.Log.Notice("Selected object: ", obj.Name);
                            Program.Log.Notice("Selected project: ", project.Name);
                            Program.Log.Notice("Selected id: ", project.Id);

                            return Concept.Result.Successfully;
                        }
                        
                    }
                }
            }
            Program.Log.Warning("Wrong line project ", "");
            return Concept.Result.IridiWrongLineProject;
        }

        static public void ConsoleWrite(Data iridiData)
        {
            Program.Log.Alert("Iridi projects", ConsoleColor.Gray);
            Program.Log.Alert(iridiData.ProjectsCount, ConsoleColor.Red);

            foreach (var folder in iridiData.Folders)
            {
                foreach (var obj in folder.Objects)
                {
                    foreach (var project in obj.Projects)
                    {
                        Program.Log.Alert(folder.Name, ConsoleColor.Yellow);
                        Program.Log.Alert(obj.Name, ConsoleColor.Green);
                        Program.Log.Alert(project.Name, ConsoleColor.Cyan);
                    }
                }
            }
        }
        static public int Get(Data iridiData)
        {
            foreach (var folder in iridiData.Folders)
            {
                foreach (var obj in folder.Objects)
                {
                    foreach (var project in obj.Projects)
                    {
                        if (iridiData.Selected.Folder == folder.Name &&
                            iridiData.Selected.Object == obj.Name &&
                            iridiData.Selected.Project == project.Name)
                        {
                            iridiData.Selected.Id = project.Id;
                            return Concept.Result.Successfully;
                        }
                    }
                }
            }
            Program.Log.Warning("Cant find project: ", "");
            Program.Log.Warning("Selected folder: ", iridiData.Selected.Folder);
            Program.Log.Warning("Selected object: ", iridiData.Selected.Object);
            Program.Log.Warning("Selected project: ", iridiData.Selected.Project);

            return Concept.Result.IridiCantFindProject;
        }
    }
}
