
namespace IridiUploadConsole.Concept.Collection
{
    class Transfer
    {
        public int IsDownloaded = 0;
        public Iridi.Data IridiData { get; set; }
        public Server.Data ServerData { get; set; }
        public File.Data FileData { get; set; }
        public Iridi.Operations Do { get; set; }
        public Server.Operations Server { get; set; }
        public Transfer()
        {
            IridiData = new Iridi.Data();
            ServerData = new Server.Data();
            FileData = new File.Data();
            Do = new Iridi.Operations(IridiData, FileData);
            Server = new Server.Operations(ServerData);
        }

        public int ServerGetProjects()
        {
            int result = Concept.Result.IridiUnknownError;

            if ((result = Server.Authentication()) != Concept.Result.Successfully) return result;
            if ((result = Server.GetProjects()) != Concept.Result.Successfully) return result;

            Server.ShowProjects();
            return Concept.Result.Successfully;
        }

        public int IridiGetProjects()
        {
            int result = Concept.Result.IridiUnknownError;

            if ((result = Do.GetSession()) != Concept.Result.Successfully) return result;
            if ((result = Do.Authentication()) != Concept.Result.Successfully) return result;
            if ((result = Do.GetObjects()) != Concept.Result.Successfully) return result;
            if ((result = Do.GetProjects()) != Concept.Result.Successfully) return result;

            Do.Selection();
            return Concept.Result.Successfully;
        }

        public int IridiSetProject(int numberLine)
        {
            return Do.SetSelection(numberLine);
        }

        public int ServerSetProject(int numberLine)
        {
            return Server.SetProject(numberLine);
        }
        public int FilePath(string path)
        {
            return FileData.Set(path);
        }

        public void IridiConsoleWrite()
        {
            Do.ConsoleWrite();
        }
        public int IridiUpload()
        {
            int result = Concept.Result.IridiUnknownError;

            // Полная проверка, чтобы не было никаких сомнений, ничего не изменилось на сайте
            // и мы точно понимаем, что есть такой проект, который мы выбрали
            if ((result = Do.GetSession()) != Concept.Result.Successfully) return result;
            if ((result = Do.Authentication()) != Concept.Result.Successfully) return result;
            if ((result = Do.GetObjects()) != Concept.Result.Successfully) return result;
            if ((result = Do.GetProjects()) != Concept.Result.Successfully) return result;

            // Находим подходящий проект под выбранные: папка, объект, проект
            if ((result = Do.GetSelection()) != Concept.Result.Successfully) return result;

            // Загружаем файл
            if ((result = Do.Upload()) != Concept.Result.Successfully) return result;

            return Concept.Result.Successfully;
        }

        public int ServerDownload()
        {
            int result = Concept.Result.IridiUnknownError;

            // Полная проверка, чтобы не было никаких сомнений, ничего не изменилось на облаке и сервере
            // и мы точно понимаем, что есть такой проект, который мы выбрали
            if ((result = Server.Authentication()) != Concept.Result.Successfully) return result;
            if ((result = Server.GetProjects()) != Concept.Result.Successfully) return result;

            // Находим подходящий проект под выбранные: объект, проект
            if ((result = Server.GetProject()) != Concept.Result.Successfully) return result;

            // Загружаем файл
            if ((result = Server.Download()) != Concept.Result.Successfully) return result;

            return Concept.Result.Successfully;
        }
    }
}
