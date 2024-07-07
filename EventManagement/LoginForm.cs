using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace EventManagement
{
    public partial class LoginForm : Form
    {
        DbContext dbContext = new DbContext();
        AuthenticationService authenticationService = new AuthenticationService();

        ValidService validService = new ValidService();

        public LoginForm()
        {
            InitializeComponent();

            setup();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!loginFormService.login) { 
                loginFormService.login = true;
                setup();
                return;
            }

            Thread loginThread = new Thread(new ThreadStart(ThreadLogin));
            loginThread.Start();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (loginFormService.login) {
                loginFormService.login = false;
                setup();
                return;
            }

            Thread registerThread = new Thread(new ThreadStart(ThreadRegister));
            registerThread.Start();
        }

        private void ThreadLogin()
        {
            RequestData request = new RequestData();
            request.username = txtUsername.Text;
            request.password = txtPassword.Text;

            if (validService.IsValidRequestLogin(request))
            {
                int result = 0;
                IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
                int port = 6666;
                IPEndPoint endPoint = new IPEndPoint(ipAddress, port);

                Socket client = new Socket(ipAddress.AddressFamily,
                    SocketType.Dgram,
                    ProtocolType.Udp);

                SocketService socketService = new SocketService();
                socketService.socket = client;

                result = socketService.SendLogin(new AccountDTO(0, "", request.username, request.password),
                    endPoint);

                if (result > 0) Console.WriteLine("Sent Login message");
                else
                {
                    Console.WriteLine("Failed sent login message.");
                    MessageBox.Show("Đăng nhập không thành công!!!");
                    client.Close();
                    return;
                }

                result = 0;
                byte[] buffer = new byte[1024];
                EndPoint serverEndPoint = (EndPoint)endPoint;

                while (result == 0)
                {
                    result = client.ReceiveFrom(buffer, 1024, 0, ref serverEndPoint);
                }

                string msg = Encoding.UTF8.GetString(buffer, 0, result);
                if (msg.Contains(SocketService.LOGIN_RESPONSE) && msg.Split("\t")[1] == "TRUE")
                {
                    MessageBox.Show("Đăng nhập thành công");
                    int id = Int32.Parse(msg.Split("\t")[2]);
                    string name = msg.Split("\t")[3];
                    NavigateHomeForm(id, name);
                }
                else
                {

                    MessageBox.Show("Đăng nhập không thành công!!!");
                }

                client.Close();

            }
            else
            {
                MessageBox.Show("Đăng nhập không thành công!!!");
            }
        }

        private void ThreadRegister()
        {
            RequestData request = new RequestData();
            request.username = txtUsername.Text;
            request.password = txtPassword.Text;
            request.name = txtName.Text;

            if (validService.IsValidRequestRegister(request))
            {
                int result = 0;
                IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
                int port = 6666;
                IPEndPoint endPoint = new IPEndPoint(ipAddress, port);

                Socket client = new Socket(ipAddress.AddressFamily,
                    SocketType.Dgram,
                    ProtocolType.Udp);

                SocketService socketService = new SocketService();
                socketService.socket = client;

                result = socketService.SendRegister(new AccountDTO(0, request.name, request.username, request.password),
                    endPoint);

                if (result > 0) Console.WriteLine("Sent register message");
                else
                {
                    Console.WriteLine("Failed sent register message.");
                    MessageBox.Show("Đăng ký không thành công!!!");
                    client.Close();
                    return;
                }

                result = 0;
                byte[] buffer = new byte[1024];
                EndPoint serverEndPoint = (EndPoint)endPoint;

                while (result == 0)
                {
                    result = client.ReceiveFrom(buffer, 1024, 0, ref serverEndPoint);
                }

                string msg = Encoding.UTF8.GetString(buffer, 0, result);
                if (msg.Contains(SocketService.REGISTER_RESPONSE) && msg.Split("\t")[1] == "TRUE")
                {
                    MessageBox.Show("Đăng ký thành công");
                    int id = Int32.Parse(msg.Split("\t")[2]);
                    NavigateHomeForm(id, txtName.Text);
                }
                else
                {

                    MessageBox.Show("Đăng ký không thành công!!!");
                }

                client.Close();
            }
            else
            {
                MessageBox.Show("Các trường không được để trống");
            }
        }

        private void NavigateHomeForm(int accountId, string name) {
            Console.WriteLine("Navigating Home Form");
            //this.Hide();
            //this.Size = new Size(0, 0);
            this.Opacity = 0.0;

            HomeForm homeForm = new HomeForm();
            homeForm.accountId = accountId;
            homeForm.SetName(name);
            homeForm.ShowDialog();
        }
    }
}
