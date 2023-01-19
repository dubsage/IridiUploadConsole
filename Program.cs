using CommandLineTool;

namespace IridiUploadConsole
{
    class Program
    {
        public static Utility.Logging Log = new Utility.Logging();
        public static Concept.Collection.TTransfers Data = new Concept.Collection.TTransfers();
        static int Main(string[] args)
        {
            if (args != null && args.Length > 0) return Arguments.Process.Commands(args);

            //Example
            /*
            Data.Add();
            var tt = Data.Get();
            tt.ServerData.IP = "192.168.1.19";
            tt.ServerData.Pass = "pass";
            tt.ServerGetProjects();*/

            //Example
            /*
            Data.Add();
            var transfer = Data.Get();

            transfer.IridiData.Login = "login";
            transfer.IridiData.Pass = "pass";
            transfer.FilePath(@"D:\projects\Iridi\TEST\PANELS PROJECT.irpz");
            transfer.IridiData.Selected.Folder = "TEST OBJECTS";
            transfer.IridiData.Selected.Object = "UMC_C3";
            transfer.IridiData.Selected.Project = "Panel";

            Data.Add();
            transfer = Data.Get();

            transfer.IridiData.Login = "login";
            transfer.IridiData.Pass = "pass";
            transfer.FilePath(@"D:\projects\Iridi\TEST\Test23.sirpz");
            transfer.IridiData.Selected.Folder = "TEST OBJECTS";
            transfer.IridiData.Selected.Object = "UMC_C3";
            transfer.IridiData.Selected.Project = "Frozen";

            foreach(var t in Data.Transfers)
            {
                t.IridiUpload();
            }*/

            Cli cli = new(typeof(CommandLine.Commands))
            {
                Introduction = "This program allows you to update existing Iridi project on the Iridi cloud. Then update the project on the server, like a UMC C3.",
                PromptText = "IU"
            };
            //optional: set list of keys to exit from the command loop
            cli.SetCancellationKeys(new() { "exit" });
            cli.Start();

            return Concept.Result.Successfully;
        }
    }
}
