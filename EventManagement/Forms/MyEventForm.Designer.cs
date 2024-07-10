namespace EventManagement.Forms
{
    partial class MyEventForm
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
            dgvEventList = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvEventList).BeginInit();
            SuspendLayout();
            // 
            // lbTitle
            // 
            lbTitle.AutoSize = true;
            lbTitle.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbTitle.Location = new Point(326, 9);
            lbTitle.Name = "lbTitle";
            lbTitle.Size = new Size(149, 30);
            lbTitle.TabIndex = 1;
            lbTitle.Text = "Sự kiện của tôi";
            // 
            // dgvEventList
            // 
            dgvEventList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEventList.Location = new Point(12, 42);
            dgvEventList.Name = "dgvEventList";
            dgvEventList.Size = new Size(791, 320);
            dgvEventList.TabIndex = 2;
            // 
            // MyEventForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(815, 378);
            Controls.Add(dgvEventList);
            Controls.Add(lbTitle);
            Name = "MyEventForm";
            Text = "MyEventForm";
            ((System.ComponentModel.ISupportInitialize)dgvEventList).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lbTitle;
        private DataGridView dgvEventList;
    }
}