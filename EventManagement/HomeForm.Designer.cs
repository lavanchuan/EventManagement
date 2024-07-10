namespace EventManagement
{
    partial class HomeForm
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
            lbName = new Label();
            btnAddEvent = new Button();
            btnIviteList = new Button();
            btnRequestList = new Button();
            v_events = new ListBox();
            lbEventList = new Label();
            panel1 = new Panel();
            txtIdEvent = new TextBox();
            btnRequestEvent = new Button();
            label1 = new Label();
            btnMyEventList = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // lbName
            // 
            lbName.AutoSize = true;
            lbName.Font = new Font("Segoe UI", 18F);
            lbName.Location = new Point(12, 9);
            lbName.Name = "lbName";
            lbName.Size = new Size(78, 32);
            lbName.TabIndex = 0;
            lbName.Text = "Name";
            // 
            // btnAddEvent
            // 
            btnAddEvent.Font = new Font("Segoe UI", 9.75F);
            btnAddEvent.Location = new Point(12, 246);
            btnAddEvent.Name = "btnAddEvent";
            btnAddEvent.Size = new Size(206, 50);
            btnAddEvent.TabIndex = 1;
            btnAddEvent.Text = "Thêm sự kiện mới";
            btnAddEvent.UseVisualStyleBackColor = true;
            btnAddEvent.Click += btnAddEvent_Click;
            // 
            // btnIviteList
            // 
            btnIviteList.Font = new Font("Segoe UI", 9.75F);
            btnIviteList.Location = new Point(12, 301);
            btnIviteList.Name = "btnIviteList";
            btnIviteList.Size = new Size(206, 50);
            btnIviteList.TabIndex = 1;
            btnIviteList.Text = "Danh sách lời mời";
            btnIviteList.UseVisualStyleBackColor = true;
            btnIviteList.Click += btnIviteList_Click;
            // 
            // btnRequestList
            // 
            btnRequestList.Font = new Font("Segoe UI", 9.75F);
            btnRequestList.Location = new Point(12, 357);
            btnRequestList.Name = "btnRequestList";
            btnRequestList.Size = new Size(206, 50);
            btnRequestList.TabIndex = 1;
            btnRequestList.Text = "Danh sách yêu cầu tham";
            btnRequestList.UseVisualStyleBackColor = true;
            btnRequestList.Click += btnRequestList_Click;
            // 
            // v_events
            // 
            v_events.FormattingEnabled = true;
            v_events.ItemHeight = 15;
            v_events.Location = new Point(237, 57);
            v_events.Name = "v_events";
            v_events.Size = new Size(511, 349);
            v_events.TabIndex = 2;
            // 
            // lbEventList
            // 
            lbEventList.AutoSize = true;
            lbEventList.Font = new Font("Segoe UI", 18F);
            lbEventList.Location = new Point(294, 9);
            lbEventList.Name = "lbEventList";
            lbEventList.Size = new Size(391, 32);
            lbEventList.TabIndex = 0;
            lbEventList.Text = "Danh sách các sự kiện đang diễn ra";
            // 
            // panel1
            // 
            panel1.Controls.Add(txtIdEvent);
            panel1.Controls.Add(btnRequestEvent);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(12, 57);
            panel1.Name = "panel1";
            panel1.Size = new Size(206, 117);
            panel1.TabIndex = 3;
            // 
            // txtIdEvent
            // 
            txtIdEvent.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtIdEvent.Location = new Point(24, 32);
            txtIdEvent.Name = "txtIdEvent";
            txtIdEvent.PlaceholderText = "Nhập vào id của sự kiện";
            txtIdEvent.Size = new Size(156, 25);
            txtIdEvent.TabIndex = 2;
            // 
            // btnRequestEvent
            // 
            btnRequestEvent.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRequestEvent.Location = new Point(24, 75);
            btnRequestEvent.Name = "btnRequestEvent";
            btnRequestEvent.Size = new Size(156, 27);
            btnRequestEvent.TabIndex = 1;
            btnRequestEvent.Text = "Yêu cầu tham gia";
            btnRequestEvent.UseVisualStyleBackColor = true;
            btnRequestEvent.Click += btnRequestEvent_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(75, 3);
            label1.Name = "label1";
            label1.Size = new Size(76, 21);
            label1.TabIndex = 0;
            label1.Text = "Id sự kiện";
            label1.Click += label1_Click;
            // 
            // btnMyEventList
            // 
            btnMyEventList.Font = new Font("Segoe UI", 9.75F);
            btnMyEventList.Location = new Point(12, 186);
            btnMyEventList.Name = "btnMyEventList";
            btnMyEventList.Size = new Size(206, 50);
            btnMyEventList.TabIndex = 1;
            btnMyEventList.Text = "Sự kiện của tôi";
            btnMyEventList.UseVisualStyleBackColor = true;
            btnMyEventList.Click += myEventList;
            // 
            // HomeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Controls.Add(v_events);
            Controls.Add(btnRequestList);
            Controls.Add(btnIviteList);
            Controls.Add(btnMyEventList);
            Controls.Add(btnAddEvent);
            Controls.Add(lbEventList);
            Controls.Add(lbName);
            Name = "HomeForm";
            Text = "HomeForm";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbName;
        private Button btnAddEvent;
        private Button btnIviteList;
        private Button btnRequestList;
        private ListBox v_events;
        private Label lbEventList;
        private Panel panel1;
        private Button btnRequestEvent;
        private Label label1;
        private TextBox txtIdEvent;
        private Button btnMyEventList;
    }
}