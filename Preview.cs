using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace practice0CSharp
{
    public partial class Preview : Form
    {
        private PrevInterface frm = null;
        
        public Preview(PrevInterface frm, byte[] fileBytes)
        {
            InitializeComponent();
            this.frm = frm;
            pictureBox1.Image = ByteToImage(fileBytes);
            if(pictureBox1.Image != null)
                pictureBox1.Location = new Point((456 - pictureBox1.Image.Width) / 2, (324 - pictureBox1.Image.Height) / 2);
        }

        public Preview(PrevInterface frm, string data)
        {
            InitializeComponent();
            this.frm = frm;
            textBox1.Text = data;
            textBox1.Visible = true;
        }

        public Bitmap ByteToImage(byte[] blob)
        {
            MemoryStream mStream = new MemoryStream();
            byte[] pData = blob;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));

            Bitmap bm = null;
            try
            {
                bm = new Bitmap(mStream, false);
            }
            catch (ArgumentException e)
            {
                pictureBox2.Visible = true;
                return null;
            }
            pictureBox2.Visible = false;
            mStream.Dispose();
            if (bm.Size.Width > 456 || bm.Size.Height > 324)
            {
                if (bm.Size.Width > bm.Size.Height)
                {
                    double per = (double)bm.Size.Width / (double)456;   //resize persent
                    Bitmap bmap = new Bitmap(bm, new Size(456, (int)((double)bm.Size.Height / per)));
                    return bmap;
                }
                else
                {
                    double per = (double)bm.Size.Height / (double)324;   //resize persent
                    Bitmap bmap = new Bitmap(bm, new Size((int)((double)bm.Size.Width / per), 324));
                    return bmap;
                }
            }
            return bm;

        }
        public void setImage(byte[] fileBytes)
        {
            textBox1.Visible = false;
            try
            {
                pictureBox1.Image = ByteToImage(fileBytes);
                pictureBox1.Location = new Point((456 - pictureBox1.Image.Width) / 2, (324 - pictureBox1.Image.Height) / 2);
            }
            catch
            {
                pictureBox2.Visible = true;
            }
        }

        public void setText(string data)
        {
            textBox1.Text = data;
            textBox1.Visible = true;
        }

        private void Preview_FormClosing(object sender, FormClosingEventArgs e)
        {
            frm.formClose();
            // Set Form1's pre = null
        }

        internal void setimageNull()
        {
            textBox1.Visible = false;
            pictureBox2.Visible = true;
        }
    }
}
