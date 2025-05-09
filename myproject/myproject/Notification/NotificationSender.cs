using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public static class NotificationSender
{
    public static async Task SendNotificationAsync(int userId, string message, string type)
    {
        var notificationData = new
        {
            messageContent = message,
            type = type,
            status = "Unread",
            userId = userId
        };

        string json = JsonConvert.SerializeObject(notificationData);

        using (var client = new HttpClient())
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(
                "https://localhost:7177/Notification/CreateNotification", // Update with your actual address
                content
            );

            if (!response.IsSuccessStatusCode)
            {
                string error = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Failed to send notification: " + error);
            }
        }
    }
}
