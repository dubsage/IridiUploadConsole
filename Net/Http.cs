using System;
using System.IO;
using System.Net;
using System.Collections.Specialized;

namespace IridiUploadConsole.Net
{
    class UploadSettings
    {
        public string Uri { get; set; }
        public string Url { get; set; }
        public string FilePath { get; set; }
        public string Cookie { get; set; }
        public string HeaderName { get; set; }
        public string HeaderFileName { get; set; }
        public string HeaderContentType { get; set; }
        public NameValueCollection Payload { get; set; }
        public UploadSettings()
        {
            Uri = "";
            Url = "";
            FilePath = "";
            Cookie = "";
            HeaderName = "";
            HeaderFileName = "";
            HeaderContentType = "";
            Payload = new NameValueCollection();
        }
    }
    class Http
    {
        
        static public string UploadFile(UploadSettings settings)//string uri, string url, string file_path, string cookie, NameValueCollection nvc)
        {
            string response = "";
/*
            Program.Log.Informational("Uploading: ", settings.FilePath + " to:");
            Program.Log.Informational(settings.Uri + settings.Url);*/

            string boundary = "----WebKitFormBoundary1" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(settings.Uri + settings.Url);

            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";
           
            wr.UserAgent = Concept.Collection.Default.UserAgent;

            if (settings.Cookie != "")
            {
                var cookieContainer = new System.Net.CookieContainer();
                cookieContainer.SetCookies(new System.Uri(settings.Uri), settings.Cookie);// "PHPSESSID=" + IPhpSessId.Value);
                wr.CookieContainer = cookieContainer;
            }

            wr.KeepAlive = true;
            wr.Credentials = System.Net.CredentialCache.DefaultCredentials;

            Stream rs = wr.GetRequestStream();
            
            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            foreach (string key in settings.Payload.Keys)
            {
                rs.Write(boundarybytes, 0, boundarybytes.Length);
                string formitem = string.Format(formdataTemplate, key, settings.Payload[key]);
                byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                rs.Write(formitembytes, 0, formitembytes.Length);
            }
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
            string header = string.Format(headerTemplate, settings.HeaderName, settings.HeaderFileName, settings.HeaderContentType);
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            FileStream fileStream = new FileStream(settings.FilePath, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[4096];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }
            fileStream.Close();

            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);
            rs.Close();

            WebResponse wresp = null;
            try
            {
                wresp = wr.GetResponse();
                Stream stream = wresp.GetResponseStream();
                StreamReader reader = new StreamReader(stream);

                response = reader.ReadToEnd();
                stream.Close();
                wresp.Close();
            }
            catch (Exception ex)
            {
                Program.Log.Error("Error uploading file Response: ","");
                Program.Log.Error(settings.Uri + settings.Url);
                Program.Log.Error(ex);

                if (wresp != null)
                {
                    wresp.Close();
                    wresp = null;
                }
            }
            finally
            {
                wr = null;
            }
            return response;
        }
        static public string Get(string uri, string url, out string[] cookie)
        {
            /*
            Program.Log.Informational("Get:", "");
            Program.Log.Informational("uri + url");*/
            string response = "";
            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(uri + url);

            //wr.Timeout = 500;
            wr.Method = "GET";
            wr.UserAgent = "c# app";
            wr.KeepAlive = true;
            wr.Credentials = System.Net.CredentialCache.DefaultCredentials;

            //string[] 
            cookie = default;
            HttpWebResponse wresp = null;
            try
            {
                wresp = (HttpWebResponse)wr.GetResponse();
                WebHeaderCollection headers = wresp.Headers;
                cookie = headers.GetValues("Set-Cookie");

                Stream stream = wresp.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                response = reader.ReadToEnd();

                stream.Close();
                wresp.Close();
            }
            catch (Exception ex)
            {
                Program.Log.Error("Error get http request: ", "");
                Program.Log.Error(uri + url);
                Program.Log.Error(ex);

                if (wresp != null)
                {
                    wresp.Close();
                    wresp = null;
                }
            }
            finally
            {
                wr = null;
            }
            return response;
        }

        static public string Get(string uri, string url, string cookie)
        {
            /*
            Program.Log.Informational("Get:", "");
            Program.Log.Informational("uri + url");*/
            string response = "";
            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(uri + url);

            //wr.Timeout = 500;
            wr.Method = "GET";
            wr.UserAgent = "c# app";
            wr.KeepAlive = true;
            wr.Credentials = System.Net.CredentialCache.DefaultCredentials;

            if (cookie != "")
            {
                var cookieContainer = new System.Net.CookieContainer();
                cookieContainer.SetCookies(new System.Uri(uri), cookie);// "PHPSESSID=" + Program.Params.IPhpSessId.Value);
                wr.CookieContainer = cookieContainer;
            }

            HttpWebResponse wresp = null;
            try
            {
                wresp = (HttpWebResponse)wr.GetResponse();
                WebHeaderCollection headers = wresp.Headers;
                //cookies = headers.GetValues("Set-Cookie");

                Stream stream = wresp.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                response = reader.ReadToEnd();
                stream.Close();
                wresp.Close();
            }
            catch (Exception ex)
            {
                Program.Log.Error("Error get http request: ", "");
                Program.Log.Error(uri + url);
                Program.Log.Error(ex);

                if (wresp != null)
                {
                    wresp.Close();
                    wresp = null;
                }
            }
            finally
            {
                wr = null;
            }
            return response;
        }

        static public string Post(string uri, string url, string cookie, string contentType, string payload)
        {
            string response = "";
            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(uri + url);

            wr.ContentType = contentType;
            wr.Method = "POST";
            wr.UserAgent = "c# app";
            wr.KeepAlive = true;
            wr.Credentials = System.Net.CredentialCache.DefaultCredentials;
            wr.AllowAutoRedirect = false;

            if (cookie != "")
            {
                var cookieContainer = new System.Net.CookieContainer();
                cookieContainer.SetCookies(new System.Uri(uri), cookie);
                wr.CookieContainer = cookieContainer;
            }
            Stream rs = wr.GetRequestStream();

            byte[] payloadBytes = System.Text.Encoding.UTF8.GetBytes(payload);
            rs.Write(payloadBytes, 0, payloadBytes.Length);
            rs.Close();
            HttpWebResponse wresp = null;
            try
            {
                wresp = (HttpWebResponse)wr.GetResponse();
                Stream stream = wresp.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                response = reader.ReadToEnd();
                stream.Close();
                wresp.Close();
            }
            catch (Exception ex)
            {
                Program.Log.Error("Error post http request:", "");
                Program.Log.Error(uri + url);
                Program.Log.Error(ex);

                if (wresp != null)
                {
                    wresp.Close();
                    wresp = null;
                }
            }
            finally
            {
                wr = null;
            }
            return response;
        }

        static public string Post(string uri, string url, out string [] cookie, string contentType, string payload)
        {
            cookie = default;
            string response = "";
            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(uri + url);

            wr.ContentType = contentType;
            wr.Method = "POST";
            wr.UserAgent = "c# app";
            wr.KeepAlive = true;
            wr.Credentials = System.Net.CredentialCache.DefaultCredentials;
            wr.AllowAutoRedirect = false;
            /*
            if (cookie != "")
            {
                var cookieContainer = new System.Net.CookieContainer();
                cookieContainer.SetCookies(new System.Uri(uri), cookie);// "PHPSESSID=" + Program.Params.IPhpSessId.Value);// ;
                wr.CookieContainer = cookieContainer;
            }*/
            Stream rs = wr.GetRequestStream();

            byte[] payloadBytes = System.Text.Encoding.UTF8.GetBytes(payload);
            rs.Write(payloadBytes, 0, payloadBytes.Length);
            rs.Close();
            HttpWebResponse wresp = null;
            try
            {
                wresp = (HttpWebResponse)wr.GetResponse();
                WebHeaderCollection headers = wresp.Headers;
                cookie = headers.GetValues("Set-Cookie");

                Stream stream = wresp.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                
                response = reader.ReadToEnd();
                stream.Close();
                wresp.Close();
            }
            catch (Exception ex)
            {
                Program.Log.Error("Error post http request:", "");
                Program.Log.Error(uri + url);
                Program.Log.Error(ex);

                if (wresp != null)
                {
                    wresp.Close();
                    wresp = null;
                }
            }
            finally
            {
                wr = null;
            }
            return response;
        }
    }
}
