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
    public partial class showDownloadProgress : Form
    {
        public showDownloadProgress()
        {
            InitializeComponent();
        }
        public void setProgressValue(int value)
        {
            progressBar1.Value = value;
        }
        public void setProgressText(string text)
        {
            processText.Text += text + " 다운로드 중..." + Environment.NewLine;
        }
    }
}
