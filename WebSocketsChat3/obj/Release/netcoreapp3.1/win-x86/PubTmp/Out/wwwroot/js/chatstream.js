(function () {
    let webSocket;
    let userName;


    var getWebSocketMessages = function (onMessageReceived) {
        if (userName != undefined) {
            let url = `wss://${location.host}/stream/get?username=${userName}`;
            webSocket = new WebSocket(url);

            webSocket.onmessage = onMessageReceived;

        }
    };

    let ulElement = document.getElementById('chatMessages');

    document.getElementById('joinGroupButton').onclick = function () {
        if (joinGroupText.value != undefined && joinGroupText.value != '' && joinGroupText.value != 'Enter your name') {
            userName = document.getElementById('joinGroupText').value;
            document.getElementById('userAuthorisation').hidden = true;
            getWebSocketMessages(function (message) {
                ulElement.innerHTML = ulElement.innerHTML += `<li>${message.data}</li>`
            });
        };

    };


    document.getElementById("sendmessage").addEventListener("click", function () {
        let textElement = document.getElementById("messageTextInput");
        let text = textElement.value;
        webSocket.send(text);
        textElement.value = '';
    });
}());