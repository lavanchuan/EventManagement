using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using EventManagement.Entities;
using EventManagement.Responses;

namespace EventManagement.Forms
{
    public partial class InviteMeListForm : Form
    {
        List<InviteDetail> dataSource;// thông tin người mời và thông tin sự kiện
        int userId;
        SocketService socketService;
        EndPoint serverEndpoint;

        public InviteMeListForm()
        {
            InitializeComponent();
        }

        public void SetDataSource(List<InviteDetail> dataSource)
        {
            this.dataSource = dataSource;
            dgvInviteMeList.DataSource = dataSource;

            // accept
            DataGridViewButtonColumn btnColAccept = new DataGridViewButtonColumn();
            btnColAccept.Text = "Chấp nhận";
            btnColAccept.UseColumnTextForButtonValue = true;

            dgvInviteMeList.Columns.Add(btnColAccept);
            dgvInviteMeList.CellContentClick += AcceptInvite;

            // reject
            DataGridViewButtonColumn btnColReject = new DataGridViewButtonColumn();
            btnColReject.Text = "Từ chối";
            btnColReject.UseColumnTextForButtonValue = true;

            dgvInviteMeList.Columns.Add(btnColReject);
            dgvInviteMeList.CellContentClick += RejectInvite;

        }

        private void AcceptInvite(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataSource.Count &&
                e.ColumnIndex == 0)
            {

                InviteDetail item = dataSource[e.RowIndex];

                Thread acceptInviteThread = new Thread(() =>
                {
                    RequestData request = new RequestData();
                    request.ownerId = item.ownerId;
                    request.userId = userId;
                    request.eventId = item.eventId;

                    Console.WriteLine($"REQUEST: {request.eventId}\t{request.ownerId}\t{request.userId}");

                    int result = socketService.SendAcceptInvite(request, serverEndpoint);
                    if (result == 0)
                    {
                        MessageBox.Show("Đang cập nhật, vui lòng thử lại sau !!!");
                        return;
                    }

                    // recv response from server
                    result = 0;
                    byte[] buffer = new byte[1024];
                    while (result == 0)
                    {
                        result = socketService.RecvFromServer(socketService.socket, ref buffer, 1024, 0, ref serverEndpoint);
                    }

                    string message = Encoding.UTF8.GetString(buffer, 0, result);
                    ServerResponse response = new ServerResponse(message);

                    if (response.type.Equals(SocketService.ACCEPT_INVITE_RESPONSE) &&
                    response.result.Equals(SocketService.TRUE))
                    {

                        MessageBox.Show("Chấp nhận thành công.");
                        return;
                    }
                    else MessageBox.Show("Chấp nhận thất bại !!!");
                });

                acceptInviteThread.Start();
            }
        }

        private void RejectInvite(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataSource.Count &&
                e.ColumnIndex == 1)
            {

                InviteDetail item = dataSource[e.RowIndex];

                Thread rejectInviteThread = new Thread(() =>
                {
                    RequestData request = new RequestData();
                    request.ownerId = item.ownerId;
                    request.userId = userId;
                    request.eventId = item.eventId;

                    //Console.WriteLine($"REQUEST: {request.eventId}\t{request.ownerId}\t{request.userId}");

                    int result = socketService.SendRejectInvite(request, serverEndpoint);
                    if (result == 0)
                    {
                        MessageBox.Show("Đang cập nhật, vui lòng thử lại sau !!!");
                        return;
                    }

                    // recv response from server
                    result = 0;
                    byte[] buffer = new byte[1024];
                    while (result == 0)
                    {
                        result = socketService.RecvFromServer(socketService.socket, ref buffer, 1024, 0, ref serverEndpoint);
                    }

                    string message = Encoding.UTF8.GetString(buffer, 0, result);
                    ServerResponse response = new ServerResponse(message);

                    if (response.type.Equals(SocketService.REJECT_INVITE_RESPONSE) &&
                    response.result.Equals(SocketService.TRUE))
                    {

                        MessageBox.Show("Chấp nhận thành công.");
                        return;
                    }
                    else MessageBox.Show("Chấp nhận thất bại !!!");
                });

                rejectInviteThread.Start();
            }
        }

        public void SetUserId(int userId)
        {
            this.userId = userId;
        }

        public void SetSocketService(SocketService socketService)
        {
            this.socketService = socketService;
        }

        public void SetServerEndpoint(EndPoint serverEndPoint) {
            this.serverEndpoint = serverEndPoint;
        }
    }
}
