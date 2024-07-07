namespace EventManagement
{
    partial class CreateNewEvent
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
            txtEventName = new TextBox();
            label1 = new Label();
            txtTime = new DateTimePicker();
            label2 = new Label();
            txtAddress = new TextBox();
            label3 = new Label();
            txtDescription = new TextBox();
            mySqlCommand1 = new MySql.Data.MySqlClient.MySqlCommand();
            btnOK = new Button();
            tbnCancel = new Button();
            SuspendLayout();
            // 
            // lbName
            // 
            lbName.AutoSize = true;
            lbName.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbName.Location = new Point(12, 65);
            lbName.Name = "lbName";
            lbName.Size = new Size(89, 21);
            lbName.TabIndex = 0;
            lbName.Text = "Tên sự kiện:";
            // 
            // txtEventName
            // 
            txtEventName.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtEventName.Location = new Point(131, 62);
            txtEventName.Name = "txtEventName";
            txtEventName.PlaceholderText = "Nhập tên sự kiện";
            txtEventName.Size = new Size(222, 29);
            txtEventName.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 111);
            label1.Name = "label1";
            label1.Size = new Size(78, 21);
            label1.TabIndex = 0;
            label1.Text = "Thời gian:";
            // 
            // txtTime
            // 
            txtTime.Location = new Point(131, 111);
            txtTime.Name = "txtTime";
            txtTime.Size = new Size(222, 23);
            txtTime.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(12, 160);
            label2.Name = "label2";
            label2.Size = new Size(60, 21);
            label2.TabIndex = 0;
            label2.Text = "Địa chỉ:";
            // 
            // txtAddress
            // 
            txtAddress.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtAddress.Location = new Point(131, 157);
            txtAddress.Name = "txtAddress";
            txtAddress.PlaceholderText = "Địa chỉ";
            txtAddress.Size = new Size(222, 29);
            txtAddress.TabIndex = 1;
            txtAddress.TextChanged += textBox1_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(12, 212);
            label3.Name = "label3";
            label3.Size = new Size(53, 21);
            label3.TabIndex = 0;
            label3.Text = "Mô tả:";
            // 
            // txtDescription
            // 
            txtDescription.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDescription.Location = new Point(131, 209);
            txtDescription.Name = "txtDescription";
            txtDescription.PlaceholderText = "Mô tả sự kiện";
            txtDescription.Size = new Size(222, 29);
            txtDescription.TabIndex = 1;
            // 
            // mySqlCommand1
            // 
            mySqlCommand1.CacheAge = 0;
            mySqlCommand1.Connection = null;
            mySqlCommand1.EnableCaching = false;
            mySqlCommand1.Transaction = null;
            // 
            // btnOK
            // 
            btnOK.Font = new Font("Segoe UI", 12F);
            btnOK.Location = new Point(37, 277);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(126, 40);
            btnOK.TabIndex = 3;
            btnOK.Text = "Tạo sự kiện";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // tbnCancel
            // 
            tbnCancel.Font = new Font("Segoe UI", 12F);
            tbnCancel.Location = new Point(216, 277);
            tbnCancel.Name = "tbnCancel";
            tbnCancel.Size = new Size(126, 40);
            tbnCancel.TabIndex = 3;
            tbnCancel.Text = "Hủy";
            tbnCancel.UseVisualStyleBackColor = true;
            tbnCancel.Click += tbnCancel_Click;
            // 
            // CreateNewEvent
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(374, 344);
            Controls.Add(tbnCancel);
            Controls.Add(btnOK);
            Controls.Add(txtTime);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtAddress);
            Controls.Add(txtDescription);
            Controls.Add(txtEventName);
            Controls.Add(lbName);
            Name = "CreateNewEvent";
            Text = "CreateNewEvent";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbName;
        private TextBox txtEventName;
        private Label label1;
        private DateTimePicker txtTime;
        private Label label2;
        private TextBox txtAddress;
        private Label label3;
        private TextBox txtDescription;
        private MySql.Data.MySqlClient.MySqlCommand mySqlCommand1;
        private Button btnOK;
        private Button tbnCancel;
    }
}