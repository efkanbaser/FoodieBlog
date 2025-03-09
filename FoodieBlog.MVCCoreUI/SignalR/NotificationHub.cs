using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class NotificationHub : Hub
{
    // Dictionary to map user IDs to their connection IDs
    private static readonly Dictionary<string, List<string>> UserConnectionMap =
        new Dictionary<string, List<string>>();

    public async Task RegisterUser(string userId)
    {
        if (!UserConnectionMap.ContainsKey(userId))
        {
            UserConnectionMap[userId] = new List<string>();
        }

        // Add this connection to the user's list of connections
        if (!UserConnectionMap[userId].Contains(Context.ConnectionId))
        {
            UserConnectionMap[userId].Add(Context.ConnectionId);
        }

        await Task.CompletedTask;
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        // Find and remove the connection from the user mapping
        foreach (var userId in UserConnectionMap.Keys.ToList())
        {
            if (UserConnectionMap[userId].Contains(Context.ConnectionId))
            {
                UserConnectionMap[userId].Remove(Context.ConnectionId);

                // Remove the user entry if no connections remain
                if (UserConnectionMap[userId].Count == 0)
                {
                    UserConnectionMap.Remove(userId);
                }

                break;
            }
        }

        return base.OnDisconnectedAsync(exception);
    }

    // Method to get a user's connection IDs
    public static List<string> GetConnectionsForUser(string userId)
    {
        if (UserConnectionMap.ContainsKey(userId))
        {
            return UserConnectionMap[userId];
        }

        return new List<string>();
    }
}