﻿namespace ServerEvent
{
    partial class formServer
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lbServer = new Label();
            btnStart = new Button();
            btnStop = new Button();
            listView1 = new ListView();
            SuspendLayout();
            // 
            // lbServer
            // 
            lbServer.AutoSize = true;
            lbServer.Font = new Font("Segoe UI", 12F);
            lbServer.Location = new Point(156, 9);
            lbServer.Name = "lbServer";
            lbServer.Size = new Size(187, 21);
            lbServer.TabIndex = 0;
            lbServer.Text = "Ứng dụng quản lý sự kiện";
            // 
            // btnStart
            // 
            btnStart.Font = new Font("Segoe UI", 12F);
            btnStart.Location = new Point(71, 46);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(123, 45);
            btnStart.TabIndex = 1;
            btnStart.Text = "Chạy";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // btnStop
            // 
            btnStop.Font = new Font("Segoe UI", 12F);
            btnStop.Location = new Point(298, 46);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(123, 45);
            btnStop.TabIndex = 1;
            btnStop.Text = "Dừng";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // listView1
            // 
            listView1.Location = new Point(29, 107);
            listView1.Name = "listView1";
            listView1.Size = new Size(424, 173);
            listView1.TabIndex = 3;
            listView1.UseCompatibleStateImageBehavior = false;
            // 
            // formServer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(480, 307);
            Controls.Add(listView1);
            Controls.Add(btnStop);
            Controls.Add(btnStart);
            Controls.Add(lbServer);
            Name = "formServer";
            Text = "Server";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbServer;
        private Button btnStart;
        private Button btnStop;
        private ListView listView1;
    }
}
