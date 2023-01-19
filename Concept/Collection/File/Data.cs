using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IridiUploadConsole.Concept.Collection.File
{
    class Data
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }
        public Data()
        {
            Path = "";
        }
        public int Update()
        {
            if (System.IO.File.Exists(Path))
            {
                Name = System.IO.Path.GetFileName(Path);
                Size = new System.IO.FileInfo(Path).Length;
                return Concept.Result.Successfully;
            }
            Program.Log.Error("Invalid file path: ", Path);
            return Concept.Result.InvalidFilePath;
        }
        public void Trace()
        {
            Program.Log.Warning("File:", "");
            Program.Log.Warning("\tPath: " + Path);
            Program.Log.Warning("\tName: " + Name);
            Program.Log.Warning("\tSize: " + Size);
        }
        public int Set(string file_path)
        {
            if (System.IO.File.Exists(file_path))
            {
                Path = file_path;
                Update();
                Trace();
                return Concept.Result.Successfully;
            }
            Program.Log.Error("Invalid file path: ", file_path);
            return Concept.Result.InvalidFilePath;
        }
    }
}
