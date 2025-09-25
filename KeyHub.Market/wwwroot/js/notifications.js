    let connection;

    window.onload = function () {
    connectToWebsockets();
}
    // TODO POPRAW
    function connectToWebsockets() {
    console.log("🔌 Trying to connect to SignalR...");

    connection = new signalR.HubConnectionBuilder()
    .withUrl("/purchaseNotificationHub") 
    .withAutomaticReconnect()
    .build();

    connection.on("ReceiveNotifications", (notification) => {
    console.log("📢 NOTIFICATIOn", notification);
});

    connection.start()
    .then(() => {
    console.log("✅ Connected to SignalR server");
})
    .catch(err => console.error("❌ SignalR connection error:", err));
}
