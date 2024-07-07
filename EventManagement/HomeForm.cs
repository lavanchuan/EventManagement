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
    public partial class HomeForm : Form
    {
        DbContext dbContext = new DbContext();

        ValidService validService = new ValidService();
        EventService eventService = new EventService();

        public int accountId;
        public string name;

        public HomeForm()
        {
            InitializeComponent();

            Thread updateThread = new Thread(new ThreadStart(UpdateEvents));
            updateThread.Start();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void UpdateEvents()
        {
            while (true) {
                v_events.Items.Clear();
                dbContext.loadData();
                foreach (EventDTO e in dbContext.events)
                {
                    v_events.Items.Add(e.ToString());
                }

                Thread.Sleep(500);
            }
        }

        private void btnRequestEvent_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Int32.Parse(txtIdEvent.Text);
                if (id <= 0)
                {
                    MessageBox.Show("ID không hợp lệ");
                }
                else if (!eventService.ExistsById(id))
                {
                    MessageBox.Show("ID không tồn tại");
                }
                else
                {
                    Thread requestThread = new Thread(new ThreadStart(ThreadSendRequestEvent));
                    requestThread.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ID không hợp lệ");
            }
        }


        private void ThreadSendRequestEvent()
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
            int id = Int32.Parse(txtIdEvent.Text);
            RequestData request = new RequestData();
            request.eventId = id;
            request.userId = accountId;
            request.createAt = DateTime.Now;
            request.state = 0;

            result = socketService.SendRequestEvent(request, endPoint);

            result = 0;
            byte[] buffer = new byte[1024];
            EndPoint serverEndPoint = (EndPoint)endPoint;

            while (result == 0)
            {
                result = client.ReceiveFrom(buffer, 1024, 0, ref serverEndPoint);
            }

            string msg = Encoding.UTF8.GetString(buffer, 0, result);
            if (msg.Contains(SocketService.REQUEST_RESPONSE) && msg.Split("\t")[1] == "TRUE")
            {
                MessageBox.Show("Yêu cầu thành công");
            }
            else
            {

                MessageBox.Show("Yêu cầu thất bại!!!");
            }

            client.Close();

        }

        public void SetName(string name)
        {
            this.name = name;
            lbName.Text = name.ToUpper();
        }

        private void btnAddEvent_Click(object sender, EventArgs e)
        {
            CreateNewEvent form = new CreateNewEvent();
            form.ownerId = accountId;
            form.ShowDialog();
        }
    }

}
