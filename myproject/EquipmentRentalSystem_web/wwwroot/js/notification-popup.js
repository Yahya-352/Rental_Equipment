const connection = new signalR.HubConnectionBuilder()
    .withUrl("/notificationHub")
    .build();

const userId = 1; // Change this to your dynamic user ID

connection.start()
    .then(() => connection.invoke("JoinGroup", `user_${userId}`))
    .catch(err => console.error(err));

connection.on("ReceiveNotification", function (payload) {
    const popup = document.getElementById("popup");
    const typeEl = document.getElementById("popup-type");
    const textEl = document.getElementById("popup-text");
    const audio = document.getElementById("notif-sound");

    const typeKeys = ["type", "Type", "notificationType", "NotificationType"];
    const messageKeys = ["message", "Message", "messageContent", "MessageContent", "content", "Content"];
    const idKeys = ["id", "Id", "notificationId", "NotificationId"];

    const notificationTypeKey = typeKeys.find(key => payload[key]);
    const messageKey = messageKeys.find(key => payload[key]);
    const idKey = idKeys.find(key => payload[key]);

    const notificationType = notificationTypeKey ? payload[notificationTypeKey] : "Info";
    const fullMessage = messageKey ? payload[messageKey] : "No message provided.";
    const notificationId = idKey ? payload[idKey] : 0;

    typeEl.textContent = `${notificationType}: `;
    textEl.textContent = fullMessage;

    popup.classList.add('show');

    audio.currentTime = 0;
    audio.play();

    popup.onclick = function (e) {
        if (e.target !== document.getElementById('popup-close')) {
            window.location.href = `/Notification?selectedId=${notificationId}`;
        }
    };

    document.getElementById('popup-close').onclick = function (e) {
        e.stopPropagation();
        popup.classList.remove('show');
    };
});
