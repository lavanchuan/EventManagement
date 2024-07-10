namespace EventManagement.Forms
{
    partial class RequestMeListForm
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
            lbTitle = new Label();
            dgvRequestMeList = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvRequestMeList).BeginInit();
            SuspendLayout();
            // 
            // lbTitle
            // 
            lbTitle.AutoSize = true;
            lbTitle.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbTitle.Location = new Point(271, 18);
            lbTitle.Name = "lbTitle";
            lbTitle.Size = new Size(279, 21);
            lbTitle.TabIndex = 0;
            lbTitle.Text = "Dánh sách lời yêu cầu tham gia sự kiện";
            // 
            // dgvRequestMeList
            // 
            dgvRequestMeList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRequestMeList.Location = new Point(12, 42);
            dgvRequestMeList.Name = "dgvRequestMeList";
            dgvRequestMeList.Size = new Size(776, 396);
            dgvRequestMeList.TabIndex = 1;
            // 
            // RequestMeListForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvRequestMeList);
            Controls.Add(lbTitle);
            Name = "RequestMeListForm";
            Text = "RequestMeListForm";
            ((System.ComponentModel.ISupportInitialize)dgvRequestMeList).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbTitle;
        private DataGridView dgvRequestMeList;
    }
}