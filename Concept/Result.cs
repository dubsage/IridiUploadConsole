
namespace IridiUploadConsole.Concept
{
    class Result
    {
        //Operation completed successfully
        static public readonly int Successfully = 1;


        //Unable to access resource iridi.com
        static public readonly int IridiUnabledAccess = 10;

        //Authentication to iridi.com failed
        static public readonly int IridiUnabledAuthentication = 11;

        //Can't find project on iridi.com
        static public readonly int IridiCantFindProject = 12;

        //Data is not filled for iridi.com
        static public readonly int IridiDataIsNotFilled = 13;

        //Upload file failed at iridi.com
        static public readonly int IridiUploadFileFailed = 14;

        //Wrong line project at iridi.com
        static public readonly int IridiWrongLineProject = 15;

        //Unknown error on iridi.com
        static public readonly int IridiUnknownError = 19;


        //Cannot access on server
        static public readonly int ServerUnabledAccess = 20;

        //Authentication to server failed
        static public readonly int ServerUnabledAuthentication = 21;

        //Can't find project on server
        static public readonly int ServerCantFindProject = 22;

        //Data is not filled for server
        static public readonly int ServerDataIsNotFilled = 23;

        //Download file at server failed 
        static public readonly int ServerDownloadFileFailed = 24;

        //Wrong line project at server
        static public readonly int ServerWrongLineProject = 25;

        //Unknown error on server
        static public readonly int ServerUnknownError = 29;


        //Invalid file path
        static public readonly int InvalidFilePath = 30;

        //Wrong args
        static public readonly int WrongConsoleArgs = 40;
    }
}
