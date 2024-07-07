namespace EventManagement
{
    partial class LoginForm
    {
        LoginFormService loginFormService = new LoginFormService();

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
            lbUsername = new Label();
            txtUsername = new TextBox();
            lbPassword = new Label();
            txtPassword = new TextBox();
            btnLogin = new Button();
            btnRegister = new Button();
            lbName = new Label();
            txtName = new TextBox();
            SuspendLayout();
            // 
            // lbUsername
            // 
            lbUsername.AutoSize = true;
            lbUsername.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbUsername.Location = new Point(80, 65);
            lbUsername.Name = "lbUsername";
            lbUsername.Size = new Size(141, 25);
            lbUsername.TabIndex = 0;
            lbUsername.Text = "Tên đăng nhập:";
            // 
            // txtUsername
            // 
            txtUsername.Font = new Font("Segoe UI", 12F);
            txtUsername.Location = new Point(231, 65);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderText = "Tên đăng nhập";
            txtUsername.Size = new Size(214, 29);
            txtUsername.TabIndex = 1;
            txtUsername.Tag = "";
            // 
            // lbPassword
            // 
            lbPassword.AutoSize = true;
            lbPassword.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbPassword.Location = new Point(80, 113);
            lbPassword.Name = "lbPassword";
            lbPassword.Size = new Size(95, 25);
            lbPassword.TabIndex = 0;
            lbPassword.Text = "Mật khẩu:";
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 12F);
            txtPassword.Location = new Point(231, 113);
            txtPassword.Name = "txtPassword";
            txtPassword.PlaceholderText = "Mật khẩu";
            txtPassword.Size = new Size(214, 29);
            txtPassword.TabIndex = 1;
            txtPassword.Tag = "";
            txtPassword.UseSystemPasswordChar = true;
            // 
            // btnLogin
            // 
            btnLogin.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLogin.Location = new Point(80, 242);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(167, 44);
            btnLogin.TabIndex = 2;
            btnLogin.Text = "Đăng nhập";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnRegister
            // 
            btnRegister.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRegister.Location = new Point(278, 242);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(167, 44);
            btnRegister.TabIndex = 2;
            btnRegister.Text = "Đăng ký";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // lbName
            // 
            lbName.AutoSize = true;
            lbName.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbName.Location = new Point(80, 164);
            lbName.Name = "lbName";
            lbName.Size = new Size(45, 25);
            lbName.TabIndex = 0;
            lbName.Text = "Tên:";
            // 
            // txtName
            // 
            txtName.Font = new Font("Segoe UI", 12F);
            txtName.Location = new Point(231, 164);
            txtName.Name = "txtName";
            txtName.PlaceholderText = "Tên của bạn";
            txtName.Size = new Size(214, 29);
            txtName.TabIndex = 1;
            txtName.Tag = "";
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(538, 328);
            Controls.Add(btnRegister);
            Controls.Add(btnLogin);
            Controls.Add(txtPassword);
            Controls.Add(txtName);
            Controls.Add(txtUsername);
            Controls.Add(lbPassword);
            Controls.Add(lbName);
            Controls.Add(lbUsername);
            Name = "LoginForm";
            Text = "Login";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        // setup
        void setup() {
            // txtName
            if (loginFormService.login)
            {
                lbName.Hide();
                txtName.Hide();
            }
            else
            {
                lbName.Show();
                txtName.Show();
            }

        }

        private Label lbUsername;
        private TextBox txtUsername;
        private Label lbPassword;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnRegister;
        private Label lbName;
        private TextBox txtName;
    }
}