#define TEST

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
using System.Net.Json;
using System.Collections;

namespace practice0CSharp
{
    public interface IMyInterface
    {
        void getCookie(CookieContainer cookies);
    }

    public interface PrevInterface
    {
        void formClose();
    }

    public partial class Form1 : Form, IMyInterface, PrevInterface
    {
        private string ID = null;
        private string PW = null;
        private string IDX = null;
        private DirectoryInfo InitializePath = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\NaverCloud");
        private string DIR = "";
        private int pageIndex = 0;
        private List<string> uploadLinks = new List<string>();
        private List<FileInfo> files = new List<FileInfo>();
        private bool drop = false;
        private Hashtable resourceNo = new Hashtable();
        //private List<string> excepctionType = new List<string> { ".alz", ".zip", ".egg", ".exe", "mp4", "mkv", "avi", "mp3", "flv" };
        //private List<int> contentLength = new List<int>();

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
                cloudConnect();
                getList("");
                drop = true;
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
            resourceNo.Clear();
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

                string reNo = null;
                reNo = responseFromServer.Substring(responseFromServer.IndexOf("\"resourceno\":") + "\"resourceno\":".Length);
                reNo = reNo.Substring(0, reNo.IndexOf(","));
                resourceNo[parseStr] = reNo;

                //string conLength;
                //conLength = responseFromServer.Substring(responseFromServer.IndexOf("\"getcontentlength\":") + "\"getcontentlength\":".Length);
                //conLength = conLength.Substring(0, conLength.IndexOf(","));
                //contentLength.Add(int.Parse(conLength));

                responseFromServer = responseFromServer.Substring(responseFromServer.IndexOf("}") + 1);
                downloadList.Items.Add(checkType(parseStr));
            }

            //Htmlsrc html = new Htmlsrc(responseFromServer);
            //html.Show();
        }

        private void getList()
        {
            downloadList.Items.Clear();
            resourceNo.Clear();
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

                            string reNo = null;
                            reNo = substr(responseFromServer, responseFromServer.IndexOf("\"resourceno\":\"") + "\"resourceno\":\"".Length, responseFromServer.IndexOf("\"resourcetype\":") - 1);
                            resourceNo[parseStr] = reNo;

                            //string conLength;
                            //conLength = responseFromServer.Substring(responseFromServer.IndexOf("\"getcontentlength\":") + "\"getcontentlength\":".Length);
                            //conLength = conLength.Substring(0, conLength.IndexOf(","));
                            //contentLength.Add(int.Parse(conLength));

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
            downloadbt.Enabled = false;
            removeFilebt.Enabled = false;

        }

        private void downloadFile(string fileName)
        {
            string key = getGenerateKey();
            string filekey = getFileKey(key);
            string encodestr = HttpUtility.UrlEncode(fileName);
            HttpWebRequest Hwr2 = (HttpWebRequest)WebRequest.Create("http://files.cloud.naver.com" + DIR + encodestr + "?attachment=2&userid=" + ID + "&useridx=" + IDX + "&NDriveSvcType=NHN/ND-WEB%20Ver"); //&key=" + key + "&filekey=" + filekey);
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
            timer1.Start();
            //MessageBox.Show("Download Complete!");
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
            uploadbt.Enabled = true;
        }

        private void uploadbt_Click(object sender, EventArgs e)
        {

            switch (MessageBox.Show(uploadList.Items.Count + "개의 파일을 " + DIR + "로 업로드 하시겠습니까?", "업로드", MessageBoxButtons.YesNo))
            {
                case System.Windows.Forms.DialogResult.Yes:
                    if (!uploadcheck())
                        break;
                    getList();
                    downloadbt.Enabled = false;
                    files.Clear();
                    uploadLinks.Clear();
                    timer3.Start();
                    break;
                case System.Windows.Forms.DialogResult.No:
                    break;

            }
        }

        private bool uploadcheck()
        {
            string responseFromServer = "";
            HttpWebRequest Hwr2 = (HttpWebRequest)WebRequest.Create("http://files.cloud.naver.com/CheckUpload.ndrive");
            Hwr2.Method = "POST";
            Hwr2.Referer = "http://cloud.naver.com/";
            Hwr2.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.86 Safari/537.36";
            Hwr2.CookieContainer = cookie;

            System.IO.Stream str = Hwr2.GetRequestStream();
            System.IO.StreamWriter stwr = new System.IO.StreamWriter(str, new UTF8Encoding(false));
            string encodestr = HttpUtility.UrlEncode(DIR + uploadList.Items[0].ToString());
            string dateEncode = HttpUtility.UrlEncode(files[0].LastWriteTime.ToString("s") + "+09:00");
            stwr.Write("userid=" + ID + "&useridx=" + IDX + "&uploadsize=" + files[0].Length.ToString() + "&overwrite=F&getlastmodified=" + dateEncode + "&dstresource=" + encodestr);
            stwr.Flush(); stwr.Close(); stwr.Dispose();
            str.Flush(); str.Close(); str.Dispose();


            HttpWebResponse response = (HttpWebResponse)Hwr2.GetResponse();

            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream, Encoding.UTF8);

            responseFromServer = reader.ReadToEnd();

            JsonTextParser parser = new JsonTextParser();
            JsonObject obj = parser.Parse(responseFromServer);
            JsonObjectCollection col = (JsonObjectCollection)obj;
            if ((string)col["message"].GetValue() == "Duplicated File Exist")
            {
                string filename = ((JsonStringValue)((JsonObjectCollection)col["resultvalue"])[0]).Value;
                filename = filename.Substring(filename.LastIndexOf("/") + 1);
                string fileAtime = files[0].LastWriteTime.ToString("s") + "+09:00";         // Existing File LastModified
                string fileBtime = ((JsonStringValue)((JsonObjectCollection)col["resultvalue"])[1]).Value;          // New File LastModified
                switch (MessageBox.Show("이 위치에 같은 이름의 파일이 있습니다." + Environment.NewLine + "기존 파일을 덮어쓰시겠습니까?" + Environment.NewLine + "이름 : " + filename + Environment.NewLine + "신규 : " + fileAtime + Environment.NewLine + "기존 : " + fileBtime, "업로드", MessageBoxButtons.YesNo))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        uploadFiles(true);     //overwrite = T
                        return true;
                    case System.Windows.Forms.DialogResult.No:
                        return false;

                }
            }
            uploadFiles(false);     //overwrite = F
            return true;
        }

        private void uploadFiles(bool overwrite)
        {
            byte[] reqBody = setRequestBody(overwrite);
            string responseFromServer = "";
            string name = uploadList.Items[0].ToString();
            //string encodestr = HttpUtility.UrlEncode(DIR + name);
            HttpWebRequest Hwr2 = (HttpWebRequest)WebRequest.Create("http://files.cloud.naver.com" + DIR + name);
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

        private byte[] setRequestBody(bool overwrite)
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
            if (overwrite)
                ReqBody[15] = "T";
            else
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
            if (drop)
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                    e.Effect = DragDropEffects.All;
                else
                    e.Effect = DragDropEffects.None;
            }
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
            files.RemoveAt(uploadList.SelectedIndex);
            uploadLinks.RemoveAt(uploadList.SelectedIndex);
            uploadList.Items.Remove(uploadList.Items[uploadList.SelectedIndex]);
            if (uploadList.Items.Count == 0)
                uploadbt.Enabled = false;
            removebt.Enabled = false;
        }

        private void downloadList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (downloadList.SelectedIndex != -1 && downloadList.SelectedItem.ToString() != "...")
            {
                downloadbt.Enabled = true;
                removeFilebt.Enabled = true;
                string getstring = downloadList.SelectedItem.ToString();
                if (getstring[0] == '[' && getstring[getstring.Length - 1] == ']')
                    previewbt.Enabled = false;
                else
                    previewbt.Enabled = true;
                if (prev != null)
                    if (getstring[0] == '[' && getstring[getstring.Length - 1] == ']')
                        prev.setimageNull();
                    else
                    {
                        string select = downloadList.SelectedItem.ToString();
                        if (MimeMapping.GetMimeMapping(select).IndexOf("image") != -1)
                            previewFile(0);
                        else if (MimeMapping.GetMimeMapping(select).IndexOf("text") != -1)
                            previewFile(1);
                        else
                            prev.setimageNull();
                    }
            }
            else
            {
                downloadbt.Enabled = false;
                removeFilebt.Enabled = false;
            }
        }
        private void downloadbt_Click(object sender, EventArgs e)
        {
            listBox1_DoubleClick(new object(), new EventArgs());
            getList();
        }

        private void removeFilebt_Click(object sender, EventArgs e)
        {
            string responseFromServer = "";
            HttpWebRequest Hwr2 = (HttpWebRequest)WebRequest.Create("http://files.cloud.naver.com/DoDelete.ndrive");
            Hwr2.Method = "POST";
            Hwr2.Referer = "http://cloud.naver.com/";
            Hwr2.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.86 Safari/537.36";
            Hwr2.CookieContainer = cookie;

            System.IO.Stream str = Hwr2.GetRequestStream();
            System.IO.StreamWriter stwr = new System.IO.StreamWriter(str, new UTF8Encoding(false));
            string encodestr = HttpUtility.UrlEncode(DIR + downloadList.SelectedItem.ToString());
            stwr.Write("userid=" + ID + "&useridx=" + IDX + "&ownerid=&owneridx=&owneridcnum=&orgresource=" + encodestr + "&forcedelete=F");
            stwr.Flush(); stwr.Close(); stwr.Dispose();
            str.Flush(); str.Close(); str.Dispose();


            HttpWebResponse response = (HttpWebResponse)Hwr2.GetResponse();

            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream, Encoding.UTF8);

            responseFromServer = reader.ReadToEnd();
            timer5.Start();
            getList();
        }
        int cntA;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (downloadlb.Location.X <= 428)
            {
                if (cntA == 40)
                {
                    cntA = 0;
                    timer1.Stop();
                    timer2.Start();
                }
                cntA++;
            }
            else
                downloadlb.Location = new Point(downloadlb.Location.X - 2, downloadlb.Location.Y);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (downloadlb.Location.X >= 558)
                timer2.Stop();
            downloadlb.Location = new Point(downloadlb.Location.X + 1, downloadlb.Location.Y);
        }

        int cntB;
        private void timer3_Tick(object sender, EventArgs e)
        {
            if (uploadlb.Location.X <= 428)
            {
                if (cntB == 40)
                {
                    cntB = 0;
                    timer3.Stop();
                    timer4.Start();
                }
                cntB++;
            }
            else
                uploadlb.Location = new Point(uploadlb.Location.X - 2, uploadlb.Location.Y);
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            if (uploadlb.Location.X >= 558)
                timer4.Stop();
            uploadlb.Location = new Point(uploadlb.Location.X + 1, uploadlb.Location.Y);
        }

        int cntC;
        private void timer5_Tick(object sender, EventArgs e)
        {
            if (removelb.Location.X <= 428)
            {
                if (cntC == 40)
                {
                    cntC = 0;
                    timer5.Stop();
                    timer6.Start();
                }
                cntC++;
            }
            else
                removelb.Location = new Point(removelb.Location.X - 2, removelb.Location.Y);
        }

        private void timer6_Tick(object sender, EventArgs e)
        {
            if (removelb.Location.X >= 558)
                timer6.Stop();
            removelb.Location = new Point(removelb.Location.X + 1, removelb.Location.Y);
        }
        Preview prev;
        private void previewbt_Click(object sender, EventArgs e)
        {
            string select = downloadList.SelectedItem.ToString();
            if (MimeMapping.GetMimeMapping(select).IndexOf("image") != -1)
                previewFile(0);
            else if (MimeMapping.GetMimeMapping(select).IndexOf("text") != -1)
                previewFile(1);
        }
        private void previewFile(int type)  //0 - image, 1 - text, -1 - error
        {
            string key = getGenerateKey();
            string filekey = getFileKey(key);
            string encodestr = HttpUtility.UrlEncode(downloadList.SelectedItem.ToString());
            HttpWebRequest Hwr2 = (HttpWebRequest)WebRequest.Create("http://files.cloud.naver.com" + DIR + encodestr + "?attachment=2&userid=" + ID + "&useridx=" + IDX + "&NDriveSvcType=NHN/ND-WEB%20Ver"); //&key=" + key + "&filekey=" + filekey);
            Hwr2.Method = "GET";
            Hwr2.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            Hwr2.CookieContainer = cookie;

            HttpWebResponse response = (HttpWebResponse)Hwr2.GetResponse();
            //MessageBox.Show(response.ContentType);

            Stream dataStream = response.GetResponseStream();

            if (type == 0)
            {
                byte[] data = ReadFully(dataStream);
                if (prev == null)
                    prev = new Preview(this as PrevInterface, data);    //get File Link
                else
                    prev.setImage(data);
            }
            else if(type == 1)
            {
                StreamReader reader = new StreamReader(dataStream, Encoding.UTF8);
                string data = reader.ReadToEnd();
                if (prev == null)
                    prev = new Preview(this as PrevInterface, data);
                else
                    prev.setText(data);
            }
            prev.Show();
            prev.Location = new Point(this.Location.X + 574, this.Location.Y);

            
        }
        private void Form1_LocationChanged(object sender, EventArgs e)
        {
            if (prev != null)
                prev.Location = new Point(this.Location.X + 574, this.Location.Y);
        }

        public void formClose()
        {
            prev = null;
        }


    }
}