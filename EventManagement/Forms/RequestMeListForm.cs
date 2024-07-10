using EventManagement.Entities;
using EventManagement.Responses;
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

namespace EventManagement.Forms
{
    public partial class RequestMeListForm : Form
    {

        int userId;
        EndPoint serverEndpoind;
        SocketService socketService;

        List<RequestDetail> dataSource;

        public RequestMeListForm()
        {
            InitializeComponent();
        }

        public void SetUserId(int userId) { this.userId = userId; }

        public void SetSocketService(SocketService socketService) { this.socketService = socketService; }

        public void SetServerEndpoin(EndPoint serverEndpoin) { this.serverEndpoind = serverEndpoin; }

        public void SetDataSource(List<RequestDetail> dataSource) {
            this.dataSource = dataSource;
            dgvRequestMeList.DataSource = dataSource;

            // accept
            DataGridViewButtonColumn btnColAccept= new DataGridViewButtonColumn();
            btnColAccept.Text = "Chấp nhận";
            btnColAccept.UseColumnTextForButtonValue = true;

            dgvRequestMeList.Columns.Add(btnColAccept);
            dgvRequestMeList.CellContentClick += RequestAccept;
            
            // reject
            DataGridViewButtonColumn btnColReject= new DataGridViewButtonColumn();
            btnColReject.Text = "Từ chối";
            btnColReject.UseColumnTextForButtonValue = true;

            dgvRequestMeList.Columns.Add(btnColReject);
            dgvRequestMeList.CellContentClick += RequestReject;
        }

        private void RequestAccept(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex >= 0 && e.RowIndex < dataSource.Count &&
                e.ColumnIndex == 0) { 
                
                RequestDetail item = dataSource[e.RowIndex];

                Thread acceptRequestThread = new Thread(() => {
                    RequestData request = new RequestData();
                    request.ownerId = userId;
                    request.userId = item.userId;
                    request.eventId = item.eventId;

                    int result = socketService.SendAcceptRequest(request, serverEndpoind);
                    if (result == 0) {
                        MessageBox.Show("Đang cập nhật, vui lòng thử lại sau !!!");
                        return;
                    }

                    // recv response from server
                    result = 0;
                    byte[] buffer = new byte[1024];
                    while (result == 0) {
                        result = socketService.RecvFromServer(socketService.socket, ref buffer, 1024, 0, ref serverEndpoind);
                    }

                    string message = Encoding.UTF8.GetString(buffer, 0, result);
                    ServerResponse response = new ServerResponse(message);

                    if (response.type.Equals(SocketService.ACCEPT_REQUEST_RESPONSE) &&
                    response.result.Equals(SocketService.TRUE)) {

                        MessageBox.Show("Chấp nhận thành công.");
                        return;
                    } else MessageBox.Show("Chấp nhận thất bại !!!");
                });

                acceptRequestThread.Start();
            }
        }
        private void RequestReject(object sender, DataGridViewCellEventArgs e) {

            if (e.RowIndex >= 0 && e.RowIndex < dataSource.Count &&
                e.ColumnIndex == 1)
            {

                RequestDetail item = dataSource[e.RowIndex];

                Thread rejectRequestThread = new Thread(() => {
                    RequestData request = new RequestData();
                    request.ownerId = userId;
                    request.userId = item.userId;
                    request.eventId = item.eventId;

                    int result = socketService.SendRejectRequest(request, serverEndpoind);
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
                        result = socketService.RecvFromServer(socketService.socket, ref buffer, 1024, 0, ref serverEndpoind);
                    }

                    string message = Encoding.UTF8.GetString(buffer, 0, result);
                    ServerResponse response = new ServerResponse(message);

                    if (response.type.Equals(SocketService.REJECT_REQUEST_RESPONSE) &&
                    response.result.Equals(SocketService.TRUE))
                    {

                        MessageBox.Show("Từ chối thành công.");
                        return;
                    }
                    else MessageBox.Show("Từ chối thất bại !!!");
                });

                rejectRequestThread.Start();

            }
        }
    }
}
