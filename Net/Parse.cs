
namespace IridiUploadConsole.Net
{
    class Parse
    {
        static public string Cookie(string[] cookie, string cookieKey)
        {
            if (cookie == null) return "";

            foreach (string element in cookie)
            {
                string[] values = element.Split(';');
                foreach (string value in values)
                {
                    string cookieKeyStrong = cookieKey;
                    int startPosition = value.IndexOf(cookieKeyStrong);
                    if (startPosition != -1)
                    {
                        string cookieValue = value.Substring(cookieKeyStrong.Length);
                        return cookieValue;
                    }
                }
            }
            return "";
        }
    }
}
