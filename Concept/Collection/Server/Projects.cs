using System.Collections.Generic;
using System.Threading;

namespace IridiUploadConsole.Concept.Collection.Server
{
    class Projects
    {
        static public int GetProjects(Data serverData)
        {
            serverData._data = new Interface();
            int result = Concept.Result.Successfully;

            Interface.TUserUpdate userUpdate;
            result = Interface.Get<Interface.TUserUpdate>(serverData, out userUpdate, Data.Urls.UserUpdate);
            serverData._data.UserUpdate = userUpdate;
            if (result != Concept.Result.Successfully)
            {
                return result;
            }

            Thread.Sleep(500);
            Interface.TObjectUpdate objectUpdate;
            result = Interface.Get<Interface.TObjectUpdate>(serverData, out objectUpdate, Data.Urls.ObjectUpdate);
            serverData._data.ObjectUpdate = objectUpdate;
            if (result != Concept.Result.Successfully)
            {
                return result;
            }

            Thread.Sleep(500);
            Interface.TUser user;
            result = Interface.Get<Interface.TUser>(serverData, out user, Data.Urls.User);
            serverData._data.User = user;
            if (result != Concept.Result.Successfully)
            {
                return result;
            }

            serverData.ProjectsCount = 0;
            for (int i = 0; i < 200; i++)
            {
                bool finded = false;

                //Program.Log.Colour();
                Thread.Sleep(500);
                Interface.TObject _object;
                result = Interface.Get<Interface.TObject>(serverData, out _object, Data.Urls.Object);
                serverData._data.Object = _object;
                if (result != Concept.Result.Successfully)
                {
                    return result;
                }
                if (serverData._data.Object.objects_last_update != "") finded = true;

                List<Interface.TObjectDetail> objects = new List<Interface.TObjectDetail>();
                foreach (Interface.TObjectDetail obj in serverData._data.Object.objects)
                {
                    finded = true; 

                    Interface.TObjectDetail elementObject;
                    result = Interface.Get<Interface.TObjectDetail>(serverData, out elementObject, Data.Urls.ObjectDetail+ obj.object_id);
                    //serverData._data.Object.objects = objectDetail;
                    if (result != Concept.Result.Successfully)
                    {
                        return result;
                    }
                    serverData.ProjectsCount+= elementObject.projects.Length;
                    objects.Add(elementObject);
                }
                if (finded)
                {
                    serverData._data.Object.objects = objects.ToArray();
                    break;
                }
            }
            return Concept.Result.Successfully;
        }
        /*
        static public int Get<T>(Data serverData, out T data, string url) where T : Interface.TBase, new()
        {
            data = default;
            try
            {
                Program.Log.Notice("Get: ", "http to server");
                Program.Log.Notice(serverData.Uri() + url);

                string cookie = Data.SessionKey + serverData.IrSessionId;
                string response = "";
                try
                {
                    response = Net.Http.Get(serverData.Uri(), url, cookie);
                }
                catch (Exception) { }

                if (response == "")
                {
                    Program.Log.Notice("Server is not available", "");
                    return Concept.Result.ServerUnabledAccess;
                }

                data = JsonConvert.DeserializeObject<T>(response);
                if (data != null) data.Trace();
                else
                {
                    Program.Log.Notice("Deserialize object is failed", "");
                    return Concept.Result.ServerUnabledAccess;
                }
            }
            catch (Exception e)
            {
                Program.Log.Notice("Deserialize object is failed", "");
                Program.Log.Error(System.Reflection.MethodBase.GetCurrentMethod().Name + " ", e);
                return Concept.Result.ServerUnabledAccess;
            }
            return Concept.Result.Successfully;
        }*/
    }
}
