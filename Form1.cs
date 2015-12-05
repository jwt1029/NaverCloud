using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
// User Defined.
using System.Net;
using System.IO;
using System.Web;

namespace practice0CSharp
{
    public interface IMyInterface
    {
        void getCookie(CookieContainer cookies);
    }

    public partial class Form1 : Form, IMyInterface
    {
        private string ID = null;
        private string PW = null;
        private string IDX = null;
        private DirectoryInfo InitializePath = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\NaverCloud");
        private string DIR = "";
        private int pageIndex = 0;
        private List<string> uploadLinks = new List<string>();
        private List<FileInfo> files = new List<FileInfo>();

        private CookieContainer cookie;
        public Form1()
        {
            InitializeComponent();
            uploadList.AllowDrop = true;
        }
        // http://javascriptdotnet.codeplex.com/
        private void button1_Click(object sender, EventArgs e)
        {
            NLoginCLS NL = new NLoginCLS(this as IMyInterface);
            if (NL.Login(textBox1.Text, textBox2.Text))
            {
                MessageBox.Show("Logged In!\n");
                ID = textBox1.Text;
                PW = textBox2.Text;
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                button1.Enabled = false;
                uploadbt.Enabled = true;
                cloudConnect();
                getList("");
                uploadList.AllowDrop = true;
            }
            else
            {
                MessageBox.Show("FAILED!");
            }
        }

        private void cloudConnect()
        {
            string responseFromServer = "";
            HttpWebRequest Hwr2 = (HttpWebRequest)WebRequest.Create("http://cloud.naver.com/");
            Hwr2.Method = "GET";

            Hwr2.CookieContainer = cookie;

            HttpWebResponse response = (HttpWebResponse)Hwr2.GetResponse();

            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream, Encoding.UTF8);

            responseFromServer = reader.ReadToEnd();

            IDX = responseFromServer.Substring(responseFromServer.IndexOf("idx: \"") + "idx: \"".Length);
            IDX = IDX.Substring(0, IDX.IndexOf("\""));
            //MessageBox.Show(IDX);

        }

        private void getList(string dir)
        {
            downloadList.Items.Clear();
            if (pageIndex != 0)
                downloadList.Items.Add("...");
            string responseFromServer = "";
            HttpWebRequest Hwr2 = (HttpWebRequest)WebRequest.Create("http://files.cloud.naver.com/GetList.ndrive");
            Hwr2.Method = "POST";
            Hwr2.Referer = "http://cloud.naver.com/";
            Hwr2.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.86 Safari/537.36";
            Hwr2.CookieContainer = cookie;

            System.IO.Stream str = Hwr2.GetRequestStream();
            System.IO.StreamWriter stwr = new System.IO.StreamWriter(str, new UTF8Encoding(false));
            DIR += dir + "/";
            stwr.Write("userid=" + ID + "&useridx=" + IDX + "&orgresource=" + DIR + "&type=3&depth=0&sort=credate&order=desc&startnum=0&pagingrow=100");
            //MessageBox.Show("Success!");
            stwr.Flush(); stwr.Close(); stwr.Dispose();
            str.Flush(); str.Close(); str.Dispose();


            HttpWebResponse response = (HttpWebResponse)Hwr2.GetResponse();

            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream, Encoding.UTF8);

            responseFromServer = reader.ReadToEnd();

            dir = DIR.Substring(0, DIR.LastIndexOf("/"));
            while (responseFromServer.IndexOf("\"href\":") != -1)
            {
                string parseStr = null;
                parseStr = substr(responseFromServer, responseFromServer.IndexOf("\"href\":\"") + "\"href\":\"".Length, responseFromServer.IndexOf("\"priority\":") - 1);
                parseStr = parseStr.Substring(parseStr.IndexOf(dir) + dir.Length);
                parseStr = parseStr.Substring(parseStr.IndexOf("\\/") + "\\/".Length);
                parseStr = parseStr.Substring(0, parseStr.LastIndexOf("\""));
                responseFromServer = responseFromServer.Substring(responseFromServer.IndexOf("\"priority\":") + "\"priority\":".Length);
                downloadList.Items.Add(checkType(parseStr));
            }

            //Htmlsrc html = new Htmlsrc(responseFromServer);
            //html.Show();
        }

        private void getList()
        {
            downloadList.Items.Clear();
            if (pageIndex != 0)
                downloadList.Items.Add("...");
            string responseFromServer = "";
            HttpWebRequest Hwr2 = (HttpWebRequest)WebRequest.Create("http://files.cloud.naver.com/GetList.ndrive");
            Hwr2.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            Hwr2.KeepAlive = true;
            Hwr2.Method = "POST";
            Hwr2.Referer = "http://cloud.naver.com/";
            Hwr2.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.86 Safari/537.36";
            Hwr2.CookieContainer = cookie;
            Hwr2.Headers.Add("NDriveSvcType", "NHN/ND-WEB Ver");
            Hwr2.Headers.Add("X-Requested-With", "XMLHttpRequest");
            Hwr2.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            StringBuilder strBD = new StringBuilder();
            strBD.Append("userid=" + ID + "&useridx=" + IDX + "&orgresource=" + DIR + "&type=3&depth=0&sort=credate&order=desc&startnum=0&pagingrow=100");
            byte[] FormData = System.Text.Encoding.UTF8.GetBytes(strBD.ToString());
            System.IO.Stream str = Hwr2.GetRequestStream();
            str.Write(FormData, 0, FormData.Length);
            str.Close();
           // System.IO.StreamWriter stwr = new System.IO.StreamWriter(str, Encoding.Default);
           // stwr.Write("userid=" + ID + "&useridx=" + IDX + "&orgresource=" + DIR + "&type=3&depth=0&sort=credate&order=desc&startnum=0&pagingrow=100");
           // MessageBox.Show("Success!");
           // stwr.Flush(); stwr.Close(); stwr.Dispose();
           // str.Flush(); str.Close(); str.Dispose();


            using (HttpWebResponse response = (HttpWebResponse)Hwr2.GetResponse())
            {

                using (Stream dataStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(dataStream, Encoding.UTF8))
                    {

                        responseFromServer = reader.ReadToEnd();

                        string dir = DIR.Substring(0, DIR.LastIndexOf("/"));
                        while (responseFromServer.IndexOf("\"href\":") != -1)
                        {
                            string parseStr = null;
                            parseStr = substr(responseFromServer, responseFromServer.IndexOf("\"href\":\"") + "\"href\":\"".Length, responseFromServer.IndexOf("\"priority\":") - 1);
                            parseStr = parseStr.Substring(parseStr.IndexOf(dir) + dir.Length);
                            parseStr = parseStr.Substring(parseStr.IndexOf("\\/") + "\\/".Length);
                            parseStr = parseStr.Substring(0, parseStr.LastIndexOf("\""));
                            responseFromServer = responseFromServer.Substring(responseFromServer.IndexOf("\"priority\":") + "\"priority\":".Length);
                            downloadList.Items.Add(checkType(parseStr));
                        }
                    }
                }
            }
            //Htmlsrc html = new Htmlsrc(responseFromServer);
            //html.Show();
        }

        public void getCookie(CookieContainer cookie)
        {
            this.cookie = cookie;
        }

        private string substr(string str, int startIndex, int lastIndex)
        {
            return str.Substring(startIndex, lastIndex - startIndex + 1);
        }

        private string checkType(string str)
        {
            if (str.IndexOf("\\/") != -1)
                if (str[str.Length - 1] == '/' && str[str.Length - 2] == '\\')
                    return "[" + str.Substring(0, str.Length - 2) + "]";
            return str;

        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            string select = downloadList.SelectedItem.ToString();
            if (select == "...")
            {
                pageIndex--;
                DIR = DIR.Substring(0, DIR.Length - 1);
                DIR = substr(DIR, 0, DIR.LastIndexOf("/"));
                
                getList();
            }
            else if (select[0] == '[' && select[select.Length - 1] == ']')
            {
                pageIndex++;
                getList(select.Substring(1, select.Length - 2));
            }
            else
            {
                downloadFile(select.Substring(0, select.Length));
            }
        }

        private void downloadFile(string fileName)
        {
            string key = getGenerateKey();
            string filekey = getFileKey(key);
            string n = HttpUtility.UrlEncode(fileName);
            HttpWebRequest Hwr2 = (HttpWebRequest)WebRequest.Create("http://files.cloud.naver.com" + DIR + n + "?attachment=2&userid=" + ID + "&useridx=" + IDX + "&NDriveSvcType=NHN/ND-WEB%20Ver"); //&key=" + key + "&filekey=" + filekey);
            Hwr2.Method = "GET";
            Hwr2.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            Hwr2.CookieContainer = cookie;

            HttpWebResponse response = (HttpWebResponse)Hwr2.GetResponse();

            Stream dataStream = response.GetResponseStream();

            byte[] data = ReadFully(dataStream);
            if (!InitializePath.Exists)
                InitializePath.Create();
            File.WriteAllBytes(InitializePath.ToString() + "\\" + fileName, data);
            dataStream.Flush();
            MessageBox.Show("Download Complete!");
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        private string getFileKey(string key)
        {
            string responseFromServer = "";
            HttpWebRequest Hwr2 = (HttpWebRequest)WebRequest.Create("http://files.cloud.naver.com/Status/Get.ndrive");
            Hwr2.Method = "POST";
            Hwr2.Referer = "http://cloud.naver.com/";
            Hwr2.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.86 Safari/537.36";
            Hwr2.CookieContainer = cookie;

            System.IO.Stream str = Hwr2.GetRequestStream();
            System.IO.StreamWriter stwr = new System.IO.StreamWriter(str, Encoding.Default);
            stwr.Write("userid=" + ID + "&useridx=" + IDX + "&key=" + key);

            stwr.Flush(); stwr.Close(); stwr.Dispose();
            str.Flush(); str.Close(); str.Dispose();


            HttpWebResponse response = (HttpWebResponse)Hwr2.GetResponse();

            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream, Encoding.UTF8);

            responseFromServer = reader.ReadToEnd();

            string filekey = responseFromServer.Substring(responseFromServer.IndexOf("\"url\":\"") + "\"url\":\"".Length);
            filekey = filekey.Substring(0, filekey.IndexOf("\""));

            return filekey;

        }

        private string getGenerateKey()
        {
            string responseFromServer = "";
            HttpWebRequest Hwr2 = (HttpWebRequest)WebRequest.Create("http://files.cloud.naver.com/Status/GenerateKey.ndrive");
            Hwr2.Method = "POST";
            Hwr2.Referer = "http://cloud.naver.com/";
            Hwr2.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.86 Safari/537.36";
            Hwr2.CookieContainer = cookie;

            System.IO.Stream str = Hwr2.GetRequestStream();
            System.IO.StreamWriter stwr = new System.IO.StreamWriter(str, Encoding.Default);
            stwr.Write("userid=" + ID + "&useridx=" + IDX + "&action=down");

            stwr.Flush(); stwr.Close(); stwr.Dispose();
            str.Flush(); str.Close(); str.Dispose();


            HttpWebResponse response = (HttpWebResponse)Hwr2.GetResponse();

            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream, Encoding.UTF8);

            responseFromServer = reader.ReadToEnd();
            string key = responseFromServer.Substring(responseFromServer.IndexOf("\"key\":\"") + "\"key\":\"".Length);
            key = key.Substring(0, key.IndexOf("\""));

            return key;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                DirPath.Text = folderBrowserDialog1.SelectedPath;
                InitializePath = new DirectoryInfo(DirPath.Text);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DirPath.Text = InitializePath.ToString();
        }

        private void openFolderbt_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", DirPath.Text);

        }

        private void uploadList_DragDrop(object sender, DragEventArgs e)
        {
            string[] directory = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string fileName in directory)
            {
                files.Add(new FileInfo(fileName));
                uploadLinks.Add(fileName);
                uploadList.Items.Add(fileName.Substring(fileName.LastIndexOf('\\') + 1));
            }
        }

        private void uploadbt_Click(object sender, EventArgs e)
        {

            switch (MessageBox.Show(uploadList.Items.Count + "개의 파일을 " + DIR + "로 업로드 하시겠습니까?", "업로드", MessageBoxButtons.YesNo))
            {
                case System.Windows.Forms.DialogResult.Yes:
                    uploadFiles();
                    getList();
                    MessageBox.Show("Upload Complete!");
                    break;
                case System.Windows.Forms.DialogResult.No:
                    break;

            }
        }

        private void uploadFiles()
        {
            byte[] reqBody = setRequestBody();
            string responseFromServer = "";
            string name = uploadList.Items[0].ToString();
            HttpWebRequest Hwr2 = (HttpWebRequest)WebRequest.Create("http://files.cloud.naver.com/" + name.Substring(name.IndexOf("\\") + 1));
            Hwr2.Method = "POST";
            Hwr2.ContentType = "multipart/form-data; boundary=----WebKitFormBoundary7fmHxHYr8HlMFKWY";
            Hwr2.Referer = "http://cloud.naver.com/";
            Hwr2.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.86 Safari/537.36";
            Hwr2.CookieContainer = cookie;
            Hwr2.KeepAlive = false;
            //byte[] buffer = System.Text.Encoding.UTF8.GetBytes(reqBody);
            //파일 이어붙이기 StringA.ToByte + FileByte + StringB.ToByte
            System.IO.Stream str = Hwr2.GetRequestStream();
            str.Write(reqBody, 0, reqBody.Length);
            using (System.IO.StreamWriter stwr = new System.IO.StreamWriter(str, Encoding.UTF8))
            {
                //stwr.Write(reqBody);

                stwr.Flush(); stwr.Close(); stwr.Dispose();
                str.Flush(); str.Close(); str.Dispose();



                using (HttpWebResponse response = (HttpWebResponse)Hwr2.GetResponse())
                {

                    using (Stream dataStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(dataStream, Encoding.UTF8))
                        {

                            responseFromServer = reader.ReadToEnd();
                        }
                    }
                }
            }
            //Htmlsrc html = new Htmlsrc(responseFromServer);
            //html.Show();
            uploadList.Items.Clear();
        }

        private byte[] setRequestBody()
        {
            byte[] buffer;
            string boundary = "------WebKitFormBoundary7fmHxHYr8HlMFKWY";
            string[] ReqBody = new string[28];
            ReqBody[0] = boundary;
            ReqBody[1] = "Content-Disposition: form-data; name=\"userid\"";
            ReqBody[2] = "";
            ReqBody[3] = ID;
            ReqBody[4] = boundary;
            ReqBody[5] = "Content-Disposition: form-data; name=\"useridx\"";
            ReqBody[6] = "";
            ReqBody[7] = IDX;
            ReqBody[8] = boundary;
            ReqBody[9] = "Content-Disposition: form-data; name=\"Filename\"";
            ReqBody[10] = "";
            ReqBody[11] = uploadList.Items[0].ToString();
            ReqBody[12] = boundary;
            ReqBody[13] = "Content-Disposition: form-data; name=\"overwrite\"";
            ReqBody[14] = "";
            ReqBody[15] = "F";
            ReqBody[16] = boundary;
            ReqBody[17] = "Content-Disposition: form-data; name=\"filesize\"";
            ReqBody[18] = "";
            ReqBody[19] = files[0].Length.ToString();
            ReqBody[20] = boundary;
            ReqBody[21] = "Content-Disposition: form-data; name=\"getlastmodified\"";
            ReqBody[22] = "";
            System.Globalization.DateTimeFormatInfo fmt = (new System.Globalization.CultureInfo("en-US")).DateTimeFormat;
            ReqBody[23] = files[0].LastWriteTime.ToString("s") + "+09:00";
            ReqBody[24] = boundary;
            ReqBody[25] = "Content-Disposition: form-data; name=\"Filedata\"; filename=\"" + files[0].Name + "\"";
            ReqBody[26] = "Content-Type: " + MimeMapping.GetMimeMapping(uploadList.Items[0].ToString());
            ReqBody[27] = "";
            
            string dataA = String.Join("\r\n", ReqBody) + "\r\n";
            buffer = System.Text.Encoding.UTF8.GetBytes(dataA);

            byte[] b3 = new byte[buffer.Length + File.ReadAllBytes(uploadLinks[0]).Length + System.Text.Encoding.UTF8.GetBytes("\r\n" + boundary).Length];
            Array.Copy(buffer, 0, b3, 0, buffer.Length);
            Array.Copy(File.ReadAllBytes(uploadLinks[0]), 0, b3, buffer.Length, File.ReadAllBytes(uploadLinks[0]).Length);
            Array.Copy(System.Text.Encoding.UTF8.GetBytes("\r\n" + boundary), 0, b3, buffer.Length + File.ReadAllBytes(uploadLinks[0]).Length, System.Text.Encoding.UTF8.GetBytes("\r\n" + boundary).Length);
            // buffer + File.ReadAllBytes(uploadLinks[0]) + System.Text.Encoding.UTF8.GetBytes("\r\n" + boundary);
            //ReqBody[28] = System.Text.Encoding.UTF8.GetString(File.ReadAllBytes(uploadLinks[0]));
            //ReqBody[29] = boundary;


            return b3;
        }

        private void uploadList_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void searchFilebt_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "모든 파일(*.*)|*.*";
            openFileDialog1.InitialDirectory = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)).ToString();
            openFileDialog1.Title = "파일 탐색";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileLink = openFileDialog1.FileName;
                string fileName = fileLink.Substring(fileLink.LastIndexOf('\\') + 1);
                uploadList.Items.Add(fileName);
            }
        }

        private void uploadList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (uploadList.SelectedIndex != -1)
                removebt.Enabled = true;
            else
                removebt.Enabled = true;
        }

        private void removebt_Click(object sender, EventArgs e)
        {
            files.Remove(new FileInfo(uploadList.Items[uploadList.SelectedIndex].ToString()));
            uploadLinks.RemoveAt(uploadList.SelectedIndex);
            uploadList.Items.Remove(uploadList.Items[uploadList.SelectedIndex]);
            removebt.Enabled = false;
        }

    }
}