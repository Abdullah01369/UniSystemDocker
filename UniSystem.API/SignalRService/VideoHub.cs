using Microsoft.AspNetCore.SignalR;

namespace UniSystem.API.SignalRService
{
    public class VideoHub : Hub
    {
        private static Dictionary<string, string> ConnectedUsers = new Dictionary<string, string>();

        // Kullanıcı bağlandığında bağlantı ID'sini kaydet
        public override Task OnConnectedAsync()
        {
            ConnectedUsers[Context.ConnectionId] = Context.UserIdentifier ?? Context.ConnectionId;
            return base.OnConnectedAsync();
        }

        // Kullanıcı ayrıldığında bağlantı ID'sini sil
        public override Task OnDisconnectedAsync(Exception exception)
        {
            ConnectedUsers.Remove(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

        // Konferans başlatma ve kullanıcılara davet gönderme
        public async Task StartConference(string[] userIds)
        {
            foreach (var userId in userIds)
            {
                if (ConnectedUsers.ContainsValue(userId))
                {
                    var connectionId = ConnectedUsers.FirstOrDefault(x => x.Value == userId).Key;
                    await Clients.Client(connectionId).SendAsync("ReceiveConferenceInvite", Context.UserIdentifier);
                }
            }
        }

        // Daveti kabul etme ve katılma
        public async Task AcceptInvite(string inviterUserId)
        {
            await Clients.User(inviterUserId).SendAsync("UserAcceptedInvite", Context.UserIdentifier);
        }
    }
}
