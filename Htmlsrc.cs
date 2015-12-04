using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace practice0CSharp
{
    public partial class Htmlsrc : Form
    {
        public Htmlsrc(string src)
        {
            InitializeComponent();
            textBox1.Text = src;
        }
    }
}
