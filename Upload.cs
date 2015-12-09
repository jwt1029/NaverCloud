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
        private int cnt;
        private string DIR;
        private IMyInterface frm = null;
        public Upload(IMyInterface frm, int cnt, string DIR)
        {
            InitializeComponent();
            this.frm = frm;
            question.Text = Environment.NewLine + Environment.NewLine + cnt + "개의 파일을 " + DIR + "로 업로드 하시겠습니까?";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
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
            this.Close();
        }
    }
}
