using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace practice0CSharp
{
    public partial class ShowDownloadProgress : Form
    {
        public ShowDownloadProgress()
        {
            InitializeComponent();
        }
        public void setProgressValue(int value)
        {
            progressBar1.Value = value;
        }
        public void setDownloadProgressText(string text)
        {
            processText.Text += text + " 다운로드 중..." + Environment.NewLine;
        }
        public void setUploadProgressText(string text)
        {
            processText.Text += text + " 업로드 중..." + Environment.NewLine;
        }
    }
}
