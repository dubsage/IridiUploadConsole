using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace IridiUploadConsole.Concept.Collection.Iridi
{
    class Objects
    {
        static public int Get(Data iridiData)
        {
            Program.Log.Informational("Get: Iridi Objects", "");
            Program.Log.Informational(Data.Uri + Data.Urls.Cloud);

            
            iridiData.Folders.Clear();
            //iridiData.Folders = null;
            //iridiData.Folders = new List<TFolder>();

            string cookie = Data.PhpSessionKey + iridiData.PhpSessId;
            string response = "";
            try
            {
                response = Net.Http.Get(Data.Uri, Data.Urls.Cloud, cookie);
            }
            catch (Exception) { }

            if (response == "")
            {
                Program.Log.Notice("Iridi is not available", "");
                return Concept.Result.IridiUnabledAccess;
            }

            //posthash
            string pattern_post_hash = @"(?<=<input\sid=""posthash""\shidden\stype=""text""\svalue="")(.*?)(?="">)";
            Regex rg_post_hash = new Regex(pattern_post_hash, RegexOptions.IgnoreCase);
            MatchCollection matched_post_hash = rg_post_hash.Matches(response);
            if (matched_post_hash.Count > 0)
            {
                iridiData.PostHash = matched_post_hash[0].Value;
                //Program.Params.IPostHash.Value = matched_post_hash[0].Value;
            }
            if(iridiData.PostHash == "")
            {
                Program.Log.Notice("Couldn't find post hash", "");
                return Concept.Result.IridiUnabledAccess;
            }
            Program.Log.Informational("Response:","");
            Program.Log.Informational("Post hash: ", iridiData.PostHash);


            //postuser
            string pattern_post_user = @"(?<=<input\sid=""postuser""\shidden\stype=""text""\svalue="")(.*?)(?="">)";
            Regex rg_post_user = new Regex(pattern_post_user, RegexOptions.IgnoreCase);
            MatchCollection matched_post_user = rg_post_user.Matches(response);
            if (matched_post_user.Count > 0)
            {
                iridiData.UserId = matched_post_user[0].Value;
            }
            if (iridiData.UserId == "")
            {
                Program.Log.Notice("Couldn't find user id", "");
                return Concept.Result.IridiUnabledAccess;
            }
            Program.Log.Informational("User id: ", iridiData.UserId);

            //folders and objects
            //objectFolders = new List<Interface.TObjectFolder>();
            //int ProjectsCount = 0;
            iridiData.ObjectsCount = 0;
            //int ObjectsCount = 0;

            string pattern_folders = @"(?<=<div.*\n.*\n.*\n.*<div\s*.*\s*class=""text_item""\s*.*\s*>)(.*?)(?=</div>)";
            string pattern_objects = @"(?<=<li.*\n.*\n.*\n.*<div\s*.*\s*class=""text_item""\s*.*\s*>)(.*?)(?=</div>)";
            string pattern_ids = @"(?<=data-id="")(.*?)(?="">)";

            Regex rg_folders = new Regex(pattern_folders, RegexOptions.IgnoreCase);
            Regex rg_objects = new Regex(pattern_objects, RegexOptions.IgnoreCase);
            Regex rg_ids = new Regex(pattern_ids, RegexOptions.IgnoreCase);

            MatchCollection matched_folders = rg_folders.Matches(response);
            MatchCollection matched_objects = rg_objects.Matches(response);
            MatchCollection matched_ids = rg_ids.Matches(response);

            if (matched_folders.Count > 0)
            {
                //int Counter = 0;
                int Left = matched_folders[0].Index;
                var folder = new Iridi.TFolder();
                folder.Name = matched_folders[0].Value;
                folder.Position = matched_folders[0].Index;
                iridiData.Folders.Add(folder);
                //iridiData.Folders.ElementAt(0).Name
                //objectFolders.Add(new Interface.TObjectFolder());
                //objectFolders[0].name = matched_folders[0].Value;
                //objectFolders[0].position = matched_folders[0].Index;

                int i = 1;

                for (i = 1; i < matched_folders.Count; i++)
                {
                    var folder_next = new Iridi.TFolder();
                    folder_next.Name = matched_folders[i].Value;
                    folder_next.Position = matched_folders[i].Index;
                    iridiData.Folders.Add(folder_next);

                    //objectFolders.Add(new Interface.TObjectFolder());
                    //objectFolders[i].name = matched_folders[i].Value;
                    //objectFolders[i].position = matched_folders[i].Index;
                    int Right = matched_folders[i].Index;

                    //List<Interface.TObject> obj_es = new List<Interface.TObject>();
                    for (int j = iridiData.ObjectsCount; j < matched_objects.Count; j++)
                    {
                        if (matched_objects[j].Index > Left && matched_objects[j].Index < Right)
                        {
                            var obj = new Iridi.TObject();
                            obj.Name = matched_objects[j].Value;
                            obj.Position = matched_objects[j].Index;
                            folder.Objects.Add(obj);

                            /*
                            Interface.TObject obj = new Interface.TObject();
                            obj.name = matched_objects[j].Value;
                            obj.position = matched_objects[j].Index;
                            obj_es.Add(obj);*/
                            //Counter++;
                            iridiData.ObjectsCount++;
                        }
                    }
                    folder = folder_next;
                    //objectFolders[i - 1].objects = obj_es.ToArray();
                    Left = matched_folders[i].Index;
                }

                //List<Interface.TObject> objes = new List<Interface.TObject>();
                for (int j = iridiData.ObjectsCount; j < matched_objects.Count; j++)
                {
                    if (matched_objects[j].Index > Left)
                    {
                        var obj = new Iridi.TObject();
                        obj.Name = matched_objects[j].Value;
                        obj.Position = matched_objects[j].Index;
                        folder.Objects.Add(obj);
                        /*
                        Interface.TObject obj = new Interface.TObject();
                        obj.name = matched_objects[j].Value;
                        obj.position = matched_objects[j].Index;
                        objes.Add(obj);*/
                        iridiData.ObjectsCount++;
                    }
                }
                //objectFolders[i - 1].objects = objes.ToArray();
            }

            //if (objectFolders != null && objectFolders.Count > 0)
            //{
                int IDCounter = 0;
                //for (int i = 0; i < objectFolders.Count; i++)
                foreach (var folder in iridiData.Folders)
            {
                if (matched_ids.Count > IDCounter)
                    folder.Id = matched_ids[IDCounter++].Value;
                    foreach (var obj in folder.Objects)
                {
                    if (matched_ids.Count > IDCounter)
                        obj.Id = matched_ids[IDCounter++].Value;
                }
            }
                /*
                for (int i = 0; i < iridiData.Folders.Count; i++)
                {
                    if (matched_ids.Count > Counter)
                    {
                    iridiData.Folders.ElementAt(i).Id = matched_ids[Counter].Value;
                        Counter++;
                    }

                    if (objectFolders[i].objects != null)
                    {
                        for (int j = 0; j < objectFolders[i].objects.Length; j++)
                        {
                            if (matched_ids.Count > Counter)
                            {
                                objectFolders[i].objects[j].id = matched_ids[Counter].Value;
                                Counter++;
                            }
                        }
                    }
                }*/
            //}

            //checking
            string GLOBAL_OBJECTSSTRMATCH = @"(?<=var\s*GLOBAL_OBJECTS\s*=\s*{)(.*?)(?=};)";

            Regex rg_GLOBAL_OBJECTS = new Regex(GLOBAL_OBJECTSSTRMATCH, RegexOptions.IgnoreCase);

            MatchCollection matched_GLOBAL_OBJECTSSTR = rg_GLOBAL_OBJECTS.Matches(response);

            //objectFolders = new List<Interface.TObjectFolder>();

            string GLOBAL_OBJECTSSTR = "";
            if (matched_GLOBAL_OBJECTSSTR.Count > 0)
            {
                GLOBAL_OBJECTSSTR = matched_GLOBAL_OBJECTSSTR[0].Value;
                //Program.Log.Informational("GLOBAL_OBJECTSSTR: " + GLOBAL_OBJECTSSTR);
            }

            string components_match = @"""(.*?)"":{""name"":""(.*?)"",""folder_id"":""(.*?)""}";

            Regex rg_components = new Regex(components_match, RegexOptions.IgnoreCase);

            MatchCollection matched_components = rg_components.Matches(GLOBAL_OBJECTSSTR);
            //Program.Log.Informational("ObjectsCount: " + ObjectsCount);
            //Program.Log.Informational("matched_components.Count: " + matched_components.Count);
            if (iridiData.ObjectsCount == matched_components.Count)
            {
                int Counter = 0;
                foreach (var folder in iridiData.Folders)
                {
                    foreach (var obj in folder.Objects)
                    {
                        //Program.Log.Informational("Groups[1]: " + matched_components[Counter].Groups[1].Value);
                        //Program.Log.Informational("Groups[2]: " + matched_components[Counter].Groups[2].Value);
                        //Program.Log.Informational("Groups[3]: " + matched_components[Counter].Groups[3].Value);
                        if ( Counter < matched_components.Count && 
                             matched_components[Counter].Groups.Count == 4 &&
                             folder.Id == matched_components[Counter].Groups[3].Value &&
                             obj.Name == matched_components[Counter].Groups[2].Value &&
                             obj.Id == matched_components[Counter].Groups[1].Value)
                        {
                            Counter++;
                        }
                        else
                        {
                            Program.Log.Notice("There was a problem finding iridi objects. Objects didn't pass the test.", "");
                            return Concept.Result.IridiUnabledAccess;
                        }
                    }
                }
            }
            else
            {
                Program.Log.Notice("There was a problem finding iridi objects. Objects didn't pass the test.", "");
                return Concept.Result.IridiUnabledAccess;
            }

            //Program.Log.Informational("Response:");
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
                    Program.Log.Debug("\t\t---end---", ConsoleColor.Yellow);
                }
                Program.Log.Debug("\t---end---", ConsoleColor.Yellow);
            }
            //Program.Log.Colour();

            return Concept.Result.Successfully;
        }
    }
}
