using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventManagement
{
    public partial class CreateNewEvent : Form
    {
        ValidService validService = new ValidService();

        public int ownerId;
        public CreateNewEvent()
        {
            InitializeComponent();

            txtTime.Value = DateTime.Now;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            
            Thread loginThread = new Thread(new ThreadStart(ThreadCreateNewEvent));
            loginThread.Start();
            
        }

        private void ThreadCreateNewEvent()
        {

            Console.WriteLine("UserId: " + ownerId);

            RequestData request = new RequestData();
            request.name = txtEventName.Text;
            request.address = txtAddress.Text;
            request.description = txtDescription.Text;
            request.ownerId = ownerId;
            request.time = txtTime.Value;

            if (!validService.IsValidRequestCreateNewEvent(request))
            {
                MessageBox.Show("Các trường không được để trống");
                return;
            }
            

            int result = 0;
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            int port = 6666;
            IPEndPoint endPoint = new IPEndPoint(ipAddress, port);

            Socket client = new Socket(ipAddress.AddressFamily,
                SocketType.Dgram,
                ProtocolType.Udp);

            SocketService socketService = new SocketService();
            socketService.socket = client;

            result = socketService.SendCreateNewEvent(request, endPoint);

            if (result == 0) {
                MessageBox.Show("Tạo mới sự kiện không thành công");
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
            if (msg.Contains(SocketService.CREATE_EVENT_RESPONSE) && msg.Split("\t")[1] == "TRUE")
            {
                MessageBox.Show("Tạo mới sự kiện thành công");

                this.Hide();
            }
            else
            {

                MessageBox.Show("Tạo mới sự kiện không thành công!!!");
            }

            client.Close();
        }
    }
}
