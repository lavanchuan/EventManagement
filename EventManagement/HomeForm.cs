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
using EventManagement.Forms;
using EventManagement.Responses;
using EventManagement.Entities;

namespace EventManagement
{
    public partial class HomeForm : Form
    {
        DbContext dbContext = new DbContext();

        ValidService validService = new ValidService();
        EventService eventService = new EventService();

        public int accountId;
        public string name;

        Socket client;

        EndPoint serverEndPoint;

        public HomeForm()
        {
            InitializeComponent();

            serverEndPoint = (EndPoint)(new IPEndPoint(IPAddress.Parse(AppData.SERVER_HOST), AppData.PORT));

            client = new Socket(IPAddress.Parse(AppData.SERVER_HOST).AddressFamily,
                SocketType.Dgram, ProtocolType.Udp);

            Thread updateThread = new Thread(new ThreadStart(UpdateEvents));
            updateThread.Start();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void UpdateEvents()
        {
            while (true)
            {
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
            IPAddress ipAddress = IPAddress.Parse(AppData.SERVER_HOST);
            IPEndPoint endPoint = new IPEndPoint(ipAddress, AppData.PORT);

            //Socket client = new Socket(ipAddress.AddressFamily,
            //    SocketType.Dgram,
            //    ProtocolType.Udp);

            SocketService socketService = new SocketService();
            socketService.socket = client;
            int id = Int32.Parse(txtIdEvent.Text);
            RequestData request = new RequestData();
            request.eventId = id;
            request.userId = accountId;
            request.createAt = DateTime.Now;

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
                MessageBox.Show("Yêu cầu thành công.");
            }
            else if (msg.Contains(SocketService.REQUEST_RESPONSE) && msg.Split("\t")[1] == "FALSE")
            {
                if (msg.Contains(SocketService.REQUEST_RESPONSE) && msg.Split("\t")[2] == "NOT_FOUND")
                    MessageBox.Show("Id không tồn tại!!!");
                else MessageBox.Show("Yêu cầu thất bại!!!");
            }
            else
            {

                MessageBox.Show("Yêu cầu thất bại!!!");
            }

            client.Close();

        }

        private void ThreadGetMyEventList(ref List<EventDTO> events)
        {
            int result;
            const int BufferSizeRecv = 1000000;
            byte[] buffer = new byte[BufferSizeRecv];
            string message = "";
            ServerResponse response;
            IPAddress ipAddress = IPAddress.Parse(AppData.SERVER_HOST);
            IPEndPoint endPoint = new IPEndPoint(ipAddress, AppData.PORT);

            EndPoint serverEndPoint = (EndPoint)endPoint;

            //Socket client = new Socket(ipAddress.AddressFamily,
            //    SocketType.Dgram,
            //    ProtocolType.Udp);

            SocketService socketService = new SocketService();
            socketService.socket = client;
            // send
            result = socketService.SendGetMyEventList(accountId, endPoint);
            if (result == 0)
            {
                MessageBox.Show("Thất bại, vui lòng thử lại!!!");
                return;
            }
            // recv
            result = 0;
            buffer = new byte[BufferSizeRecv];
            while (result == 0)
            {
                result = socketService.RecvFromServer(client, ref buffer, BufferSizeRecv, 0, ref serverEndPoint);
            }
            // 
            message = Encoding.UTF8.GetString(buffer, 0, result);
            response = new ServerResponse(message);
            if (response.type.Equals(SocketService.GET_MY_EVENT_LIST_RESPONSE))
            {
                if (response.result.Equals(SocketService.TRUE))
                {
                    events = EventDTO.ExtractEventList(response.msg);
                }
                else
                {
                    MessageBox.Show("Thất bại, vui lòng thử lại!!!");
                }
            }
            else
            {
                MessageBox.Show("SERVER ERROR!!!");
            }
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

        private void myEventList(object sender, EventArgs e)
        {
            List<EventDTO> myEvents = new List<EventDTO>();

            Thread thread = new Thread(() => ThreadGetMyEventList(ref myEvents));
            thread.Start();
            thread.Join();

            MyEventForm eventForm = new MyEventForm();
            eventForm.SetDataView(myEvents);
            eventForm.SetAccountId(accountId);
            eventForm.SetSocket(client);
            eventForm.Show();
        }

        private void btnIviteList_Click(object sender, EventArgs e)
        {
            List<InviteDetail> inviteDetails = new List<InviteDetail>();

            Thread getInviteMeListThread = new Thread(() => ThreadInviteMeList(ref inviteDetails));
            getInviteMeListThread.Start();
            getInviteMeListThread.Join();

            InviteMeListForm form = new InviteMeListForm();

            form.SetUserId(accountId);
            SocketService socketService = new SocketService();
            socketService.socket = client;
            form.SetSocketService(socketService);
            form.SetServerEndpoint(serverEndPoint);
            form.SetDataSource(inviteDetails);

            form.Show();
        }

        private void ThreadInviteMeList(ref List<InviteDetail> inviteDetails)
        {
            int result;
            RequestData request = new RequestData();
            SocketService socketService = new SocketService();
            socketService.socket = client;

            // send request
            request.userId = accountId;
            result = socketService.SendGetInviteMeList(request, serverEndPoint);
            if (result == 0)
            {
                MessageBox.Show("Đang sử lý, vui lòng thử lại sau !!!");
                return;
            }
            // recv msg
            result = 0;
            const int BufferRecvSize = 4096;
            byte[] buffer = new byte[BufferRecvSize];
            ServerResponse response;

            while (result == 0)
            {
                result = socketService.RecvFromServer(client, ref buffer, BufferRecvSize, 0, ref serverEndPoint);
            }

            response = new ServerResponse(Encoding.UTF8.GetString(buffer, 0, result));
            if (response.type.Equals(SocketService.GET_INVITE_ME_LIST_RESPONSE) &&
                response.result.Equals(SocketService.TRUE))
            {

                Console.WriteLine($"Test: {response.msg}");

                inviteDetails = InviteDetail.ExtractInviteList(response.msg);
                return;
            }
            else
            {
                MessageBox.Show("Đang sử lỹ, vui lòng thử lại sau !!!");
                return;
            }
        }

        private void btnRequestList_Click(object sender, EventArgs e)
        {
            List<RequestDetail> requestDetails = new List<RequestDetail>();

            Thread getQuestMeList = new Thread(() => ThreadRequestMeList(ref requestDetails));
            getQuestMeList.Start();
            getQuestMeList.Join();

            RequestMeListForm form = new RequestMeListForm();

            form.SetUserId(accountId);
            SocketService socketService = new SocketService();
            socketService.socket = client;
            form.SetSocketService(socketService);
            form.SetServerEndpoin(serverEndPoint);
            form.SetDataSource(requestDetails);

            form.Show();
        }

        private void ThreadRequestMeList(ref List<RequestDetail> requestDetails) {
            int result;
            RequestData request = new RequestData();
            SocketService socketService = new SocketService();
            socketService.socket = client;

            // send request
            request.userId = accountId;
            result = socketService.SendGetRequestMeList(request, serverEndPoint);
            if (result == 0)
            {
                MessageBox.Show("Đang sử lý, vui lòng thử lại sau !!!");
                return;
            }
            // recv msg
            result = 0;
            const int BufferRecvSize = 4096;
            byte[] buffer = new byte[BufferRecvSize];
            ServerResponse response;

            while (result == 0)
            {
                result = socketService.RecvFromServer(client, ref buffer, BufferRecvSize, 0, ref serverEndPoint);
            }

            response = new ServerResponse(Encoding.UTF8.GetString(buffer, 0, result));
            if (response.type.Equals(SocketService.GET_REQUEST_ME_LIST_RESPONSE) &&
                response.result.Equals(SocketService.TRUE))
            {

                requestDetails = RequestDetail.ExtractRequestList(response.msg);
                return;
            }
            else
            {
                MessageBox.Show("Đang sử lỹ, vui lòng thử lại sau !!!");
                return;
            }
        }
    }

}
