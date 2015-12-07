namespace practice0CSharp
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.DirPath = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.downloadList = new System.Windows.Forms.ListBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFolderbt = new System.Windows.Forms.Button();
            this.uploadList = new System.Windows.Forms.ListBox();
            this.searchFilebt = new System.Windows.Forms.Button();
            this.uploadbt = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.removebt = new System.Windows.Forms.Button();
            this.removeFilebt = new System.Windows.Forms.Button();
            this.downloadbt = new System.Windows.Forms.Button();
            this.downloadlb = new System.Windows.Forms.Label();
            this.uploadlb = new System.Windows.Forms.Label();
            this.removelb = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.timer4 = new System.Windows.Forms.Timer(this.components);
            this.timer5 = new System.Windows.Forms.Timer(this.components);
            this.timer6 = new System.Windows.Forms.Timer(this.components);
            this.previewbt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "아이디 ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "비밀번호";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(77, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(209, 21);
            this.textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(77, 33);
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '*';
            this.textBox2.Size = new System.Drawing.Size(209, 21);
            this.textBox2.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(292, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 48);
            this.button1.TabIndex = 2;
            this.button1.Text = "로그인";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 296);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "Path";
            // 
            // DirPath
            // 
            this.DirPath.Enabled = false;
            this.DirPath.Location = new System.Drawing.Point(48, 292);
            this.DirPath.Name = "DirPath";
            this.DirPath.Size = new System.Drawing.Size(393, 21);
            this.DirPath.TabIndex = 4;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(447, 292);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(24, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "..";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // downloadList
            // 
            this.downloadList.FormattingEnabled = true;
            this.downloadList.HorizontalScrollbar = true;
            this.downloadList.ItemHeight = 12;
            this.downloadList.Location = new System.Drawing.Point(14, 60);
            this.downloadList.Name = "downloadList";
            this.downloadList.Size = new System.Drawing.Size(256, 196);
            this.downloadList.TabIndex = 6;
            this.downloadList.SelectedIndexChanged += new System.EventHandler(this.downloadList_SelectedIndexChanged);
            this.downloadList.DoubleClick += new System.EventHandler(this.listBox1_DoubleClick);
            // 
            // openFolderbt
            // 
            this.openFolderbt.Location = new System.Drawing.Point(477, 292);
            this.openFolderbt.Name = "openFolderbt";
            this.openFolderbt.Size = new System.Drawing.Size(66, 23);
            this.openFolderbt.TabIndex = 7;
            this.openFolderbt.Text = "폴더 열기";
            this.openFolderbt.UseVisualStyleBackColor = true;
            this.openFolderbt.Click += new System.EventHandler(this.openFolderbt_Click);
            // 
            // uploadList
            // 
            this.uploadList.AllowDrop = true;
            this.uploadList.FormattingEnabled = true;
            this.uploadList.HorizontalScrollbar = true;
            this.uploadList.ItemHeight = 12;
            this.uploadList.Location = new System.Drawing.Point(284, 60);
            this.uploadList.Name = "uploadList";
            this.uploadList.Size = new System.Drawing.Size(259, 196);
            this.uploadList.TabIndex = 8;
            this.uploadList.SelectedIndexChanged += new System.EventHandler(this.uploadList_SelectedIndexChanged);
            this.uploadList.DragDrop += new System.Windows.Forms.DragEventHandler(this.uploadList_DragDrop);
            this.uploadList.DragEnter += new System.Windows.Forms.DragEventHandler(this.uploadList_DragEnter);
            // 
            // searchFilebt
            // 
            this.searchFilebt.Location = new System.Drawing.Point(466, 262);
            this.searchFilebt.Name = "searchFilebt";
            this.searchFilebt.Size = new System.Drawing.Size(77, 23);
            this.searchFilebt.TabIndex = 9;
            this.searchFilebt.Text = "파일 선택";
            this.searchFilebt.UseVisualStyleBackColor = true;
            this.searchFilebt.Click += new System.EventHandler(this.searchFilebt_Click);
            // 
            // uploadbt
            // 
            this.uploadbt.Enabled = false;
            this.uploadbt.Location = new System.Drawing.Point(406, 262);
            this.uploadbt.Name = "uploadbt";
            this.uploadbt.Size = new System.Drawing.Size(54, 23);
            this.uploadbt.TabIndex = 10;
            this.uploadbt.Text = "올리기";
            this.uploadbt.UseVisualStyleBackColor = true;
            this.uploadbt.Click += new System.EventHandler(this.uploadbt_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // removebt
            // 
            this.removebt.Enabled = false;
            this.removebt.Location = new System.Drawing.Point(376, 262);
            this.removebt.Name = "removebt";
            this.removebt.Size = new System.Drawing.Size(24, 23);
            this.removebt.TabIndex = 11;
            this.removebt.Text = "-";
            this.removebt.UseVisualStyleBackColor = true;
            this.removebt.Click += new System.EventHandler(this.removebt_Click);
            // 
            // removeFilebt
            // 
            this.removeFilebt.Enabled = false;
            this.removeFilebt.Location = new System.Drawing.Point(210, 262);
            this.removeFilebt.Name = "removeFilebt";
            this.removeFilebt.Size = new System.Drawing.Size(60, 23);
            this.removeFilebt.TabIndex = 12;
            this.removeFilebt.Text = "삭제";
            this.removeFilebt.UseVisualStyleBackColor = true;
            this.removeFilebt.Click += new System.EventHandler(this.removeFilebt_Click);
            // 
            // downloadbt
            // 
            this.downloadbt.Enabled = false;
            this.downloadbt.Location = new System.Drawing.Point(129, 262);
            this.downloadbt.Name = "downloadbt";
            this.downloadbt.Size = new System.Drawing.Size(75, 23);
            this.downloadbt.TabIndex = 14;
            this.downloadbt.Text = "내려받기";
            this.downloadbt.UseVisualStyleBackColor = true;
            this.downloadbt.Click += new System.EventHandler(this.downloadbt_Click);
            // 
            // downloadlb
            // 
            this.downloadlb.AutoSize = true;
            this.downloadlb.Location = new System.Drawing.Point(558, 24);
            this.downloadlb.Name = "downloadlb";
            this.downloadlb.Size = new System.Drawing.Size(123, 12);
            this.downloadlb.TabIndex = 15;
            this.downloadlb.Text = "Download Complete!";
            // 
            // uploadlb
            // 
            this.uploadlb.AutoSize = true;
            this.uploadlb.Location = new System.Drawing.Point(558, 24);
            this.uploadlb.Name = "uploadlb";
            this.uploadlb.Size = new System.Drawing.Size(106, 12);
            this.uploadlb.TabIndex = 15;
            this.uploadlb.Text = "Upload Complete!";
            // 
            // removelb
            // 
            this.removelb.AutoSize = true;
            this.removelb.Location = new System.Drawing.Point(558, 24);
            this.removelb.Name = "removelb";
            this.removelb.Size = new System.Drawing.Size(113, 12);
            this.removelb.TabIndex = 15;
            this.removelb.Text = "Remove Complete!";
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 1;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // timer3
            // 
            this.timer3.Interval = 1;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // timer4
            // 
            this.timer4.Interval = 1;
            this.timer4.Tick += new System.EventHandler(this.timer4_Tick);
            // 
            // timer5
            // 
            this.timer5.Interval = 1;
            this.timer5.Tick += new System.EventHandler(this.timer5_Tick);
            // 
            // timer6
            // 
            this.timer6.Interval = 1;
            this.timer6.Tick += new System.EventHandler(this.timer6_Tick);
            // 
            // previewbt
            // 
            this.previewbt.Enabled = false;
            this.previewbt.Image = ((System.Drawing.Image)(resources.GetObject("previewbt.Image")));
            this.previewbt.Location = new System.Drawing.Point(543, 132);
            this.previewbt.Name = "previewbt";
            this.previewbt.Size = new System.Drawing.Size(17, 75);
            this.previewbt.TabIndex = 16;
            this.previewbt.UseVisualStyleBackColor = true;
            this.previewbt.Click += new System.EventHandler(this.previewbt_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 326);
            this.Controls.Add(this.previewbt);
            this.Controls.Add(this.removelb);
            this.Controls.Add(this.uploadlb);
            this.Controls.Add(this.downloadlb);
            this.Controls.Add(this.downloadbt);
            this.Controls.Add(this.removeFilebt);
            this.Controls.Add(this.removebt);
            this.Controls.Add(this.uploadbt);
            this.Controls.Add(this.searchFilebt);
            this.Controls.Add(this.uploadList);
            this.Controls.Add(this.openFolderbt);
            this.Controls.Add(this.downloadList);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.DirPath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.LocationChanged += new System.EventHandler(this.Form1_LocationChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox DirPath;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox downloadList;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button openFolderbt;
        private System.Windows.Forms.ListBox uploadList;
        private System.Windows.Forms.Button searchFilebt;
        private System.Windows.Forms.Button uploadbt;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button removebt;
        private System.Windows.Forms.Button removeFilebt;
        private System.Windows.Forms.Button downloadbt;
        private System.Windows.Forms.Label downloadlb;
        private System.Windows.Forms.Label uploadlb;
        private System.Windows.Forms.Label removelb;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Timer timer4;
        private System.Windows.Forms.Timer timer5;
        private System.Windows.Forms.Timer timer6;
        private System.Windows.Forms.Button previewbt;
    }
}

