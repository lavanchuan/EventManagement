using EventManagement.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventManagement.Forms
{
    public partial class MyEventForm : Form
    {

        List<EventDTO> events;
        Socket socket;
        int accountId;

        public MyEventForm()
        {
            InitializeComponent();
        }

        private void InviteAll(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 0) return;
            EventDTO item = events[e.RowIndex];
            //MessageBox.Show($"{item.id}\n{item.name}\n{item.time}\n{item.address}\n{item.description}");

            RequestData request = new RequestData();
            request.eventId = item.id;
            request.ownerId = accountId;

            IPAddress ipaddress = IPAddress.Parse(AppData.SERVER_HOST);
            IPEndPoint ipEndPoint = new IPEndPoint(ipaddress, AppData.PORT);
            EndPoint server = (EndPoint)ipEndPoint;

            Thread sendInviteAllNewPeople = new Thread(() => ThreadSendInviteAllNewPeople(request, server));
            sendInviteAllNewPeople.Start();
        }

        public void SetDataView(List<EventDTO> events) {
            this.events = events;
            dgvEventList.DataSource = events;


            DataGridViewButtonColumn btnCol = new DataGridViewButtonColumn();
            btnCol.Text = "Mời tham gia";
            btnCol.UseColumnTextForButtonValue = true;
            dgvEventList.Columns.Add(btnCol);

            dgvEventList.CellContentClick += InviteAll;
        }

        public void SetSocket(Socket socket) {
            this.socket = socket;
        }

        public void SetAccountId(int accountId) {
            this.accountId = accountId;
        }

        private void ThreadSendInviteAllNewPeople(RequestData request, EndPoint server) {
            SocketService socketService = new SocketService();
            socketService.socket = socket;

            string message;
            byte[] buffer = new byte[1024];

            int result = socketService.SendInviteAllNewPeople(request, server);
            if (result == 0) {
                MessageBox.Show("Mời tham gia thất bại!!!");
                return;
            }

            result = 0;
            while (result == 0) {
                result = socketService.RecvFromServer(socket, ref buffer, 1024, 0, ref server);
            }

            message = Encoding.UTF8.GetString(buffer, 0, result);
            ServerResponse response = new ServerResponse(message);

            if (response.type.Equals(SocketService.INVITE_ALL_NEW_PEOPLE_RESPONSE) &&
                response.result.Equals(SocketService.TRUE)) {

                int quantity = Int32.Parse(response.msg.Trim());
                MessageBox.Show($"Đã mời {quantity} người mới.");
            } else
            {
                MessageBox.Show("Mời tham gia thất bại!!!");
            }
        }
    }
}
