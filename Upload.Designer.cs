namespace practice0CSharp
{
    partial class Upload
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.no = new System.Windows.Forms.Button();
            this.question = new System.Windows.Forms.TextBox();
            this.yes = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.folderName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // no
            // 
            this.no.Location = new System.Drawing.Point(213, 117);
            this.no.Name = "no";
            this.no.Size = new System.Drawing.Size(84, 26);
            this.no.TabIndex = 1;
            this.no.Text = "아니오(N)";
            this.no.UseVisualStyleBackColor = true;
            this.no.Click += new System.EventHandler(this.no_Click);
            // 
            // question
            // 
            this.question.BackColor = System.Drawing.Color.White;
            this.question.Location = new System.Drawing.Point(-1, -1);
            this.question.Multiline = true;
            this.question.Name = "question";
            this.question.ReadOnly = true;
            this.question.Size = new System.Drawing.Size(311, 79);
            this.question.TabIndex = 2;
            this.question.Text = "\r\n\r\n";
            this.question.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // yes
            // 
            this.yes.Location = new System.Drawing.Point(123, 117);
            this.yes.Name = "yes";
            this.yes.Size = new System.Drawing.Size(84, 26);
            this.yes.TabIndex = 1;
            this.yes.Text = "예(Y)";
            this.yes.UseVisualStyleBackColor = true;
            this.yes.Click += new System.EventHandler(this.yes_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(145, 90);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(127, 16);
            this.radioButton1.TabIndex = 3;
            this.radioButton1.Text = "폴더 생성후 올리기";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(12, 90);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(127, 16);
            this.radioButton2.TabIndex = 3;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "현재 위치에 올리기";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // folderName
            // 
            this.folderName.Location = new System.Drawing.Point(78, 112);
            this.folderName.Name = "folderName";
            this.folderName.Size = new System.Drawing.Size(219, 21);
            this.folderName.TabIndex = 4;
            this.folderName.Visible = false;
            this.folderName.TextChanged += new System.EventHandler(this.folderName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "폴더 이름 :";
            this.label1.Visible = false;
            // 
            // Upload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 155);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.folderName);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.question);
            this.Controls.Add(this.yes);
            this.Controls.Add(this.no);
            this.Name = "Upload";
            this.Text = "Upload";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button no;
        private System.Windows.Forms.TextBox question;
        private System.Windows.Forms.Button yes;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.TextBox folderName;
        private System.Windows.Forms.Label label1;
    }
}