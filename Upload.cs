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
    public partial class Upload : Form
    {
        private string front;
        private string back;
        private IMyInterface frm = null;
        private string[] invList = new string[] { "<", ">", ":", "\"", "/", "|", "?", "*" };

        public Upload(IMyInterface frm, int cnt, string DIR)
        {
            InitializeComponent();
            this.frm = frm;
            front = Environment.NewLine + Environment.NewLine + cnt + "개의 파일을 " + DIR;
            question.Text = front + "로 업로드 하시겠습니까?";
        }

        public Upload(IMyInterface frm, int cnt, int dircnt, string DIR)
        {
            InitializeComponent();
            this.frm = frm;
            front = Environment.NewLine + Environment.NewLine + cnt + "개의 파일과 " + dircnt + "개의 폴더를 " + DIR;
            question.Text = front + "로 업로드 하시겠습니까?";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                question.Text = front + folderName.Text + "로 업로드 하시겠습니까?";
                for (int i = 0; i < 20; i++)
                    this.Size = new Size(this.Size.Width, this.Size.Height + 1);
                for (int i = 0; i < 10; i++)
                {
                    yes.Location = new Point(yes.Location.X, yes.Location.Y + 2);
                    no.Location = new Point(no.Location.X, no.Location.Y + 2);
                }
                label1.Visible = true;
                folderName.Visible = true;
            }
            else
            {
                question.Text = front + "로 업로드 하시겠습니까?";
                label1.Visible = false;
                folderName.Visible = false;
                for (int i = 0; i < 20; i++)
                {
                    this.Size = new Size(this.Size.Width, this.Size.Height - 1);
                    yes.Location = new Point(yes.Location.X, yes.Location.Y - 1);
                    no.Location = new Point(no.Location.X, no.Location.Y - 1);
                }
            }
        }

        private void no_Click(object sender, EventArgs e)
        {
            frm.getUploadState(false);
            this.Close();
        }

        private void yes_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                if (folderName.Text == "")
                    MessageBox.Show("폴더 이름을 입력해주세요!");
                else
                    frm.getUploadState(folderName.Text);
            else
                frm.getUploadState(true);
        }

        private void folderName_TextChanged(object sender, EventArgs e)
        {
            foreach (string str in invList)
                if (folderName.Text.IndexOf(str) != -1)
                {
                    folderName.Text = folderName.Text.Substring(0, folderName.Text.Length - 1);
                    MessageBox.Show("<>:\"/|?* 는 폴더이름으로 사용할 수 없습니다");
                }
            question.Text = front + folderName.Text + "로 업로드 하시겠습니까?";
        }
    }
}
