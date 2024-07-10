namespace EventManagement.Forms
{
    partial class InviteMeListForm
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
            dgvInviteMeList = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvInviteMeList).BeginInit();
            SuspendLayout();
            // 
            // lbTitle
            // 
            lbTitle.AutoSize = true;
            lbTitle.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbTitle.Location = new Point(269, 9);
            lbTitle.Name = "lbTitle";
            lbTitle.Size = new Size(254, 21);
            lbTitle.TabIndex = 0;
            lbTitle.Text = "Danh sách lời mời tham gia sự kiện";
            // 
            // dgvInviteMeList
            // 
            dgvInviteMeList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvInviteMeList.Location = new Point(12, 30);
            dgvInviteMeList.Name = "dgvInviteMeList";
            dgvInviteMeList.Size = new Size(776, 372);
            dgvInviteMeList.TabIndex = 1;
            // 
            // InviteMeListForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 412);
            Controls.Add(dgvInviteMeList);
            Controls.Add(lbTitle);
            Name = "InviteMeListForm";
            Text = "InviteMeListForm";
            ((System.ComponentModel.ISupportInitialize)dgvInviteMeList).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbTitle;
        private DataGridView dgvInviteMeList;
    }
}