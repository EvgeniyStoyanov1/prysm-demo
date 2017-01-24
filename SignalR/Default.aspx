<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SignalR.Prysm.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <script src="<%: ResolveUrl("~/Scripts/jquery-1.6.4.js") %>"></script>
    <script src="<%: ResolveUrl("~/Scripts/jquery.signalR-2.2.1.js") %>"></script>
    <script>
        $(function () {
            "use strict";

            var transportPolling = function (connection, config) {
                var connection = connection;
                var transportPollingTimeout = null;
                var transportResponseTimeout = null;
         
                var transportPollingHandler = function () {
                    connection.send({ type: 'ping' });
                    transportResponseTimeout = window.setTimeout(reseponseWatchHandler, config.waitResponseInterval); // wait response
                }.bind(this);

                var reseponseWatchHandler = function () {
                    console.log("Ping response timeout.");

                    this.stop();
                    if (connection.transport.lostConnection) {
                        connection.transport.lostConnection(connection)
                    }
                }.bind(this);

                this.received = function (data) {
                    var response = connection.json.parse(data);
                    if (response.type === 'ping') {
                        console.log("Ping response received.");
                        clearTimeout(transportResponseTimeout);
                        transportPollingTimeout = window.setTimeout(transportPollingHandler, config.pollingInterval);
                    }
                }.bind(this);

                this.start = function () {
                    transportPollingHandler();
                }.bind(this);

                this.stop = function () {
                    clearTimeout(transportResponseTimeout);
                    clearTimeout(transportPollingTimeout);
                }.bind(this);
            }

            var connection = $.connection("../ping-connection");
           
            connection.logging = true;
            connection.polling = new transportPolling(connection, {
                pollingInterval: 2000,
                waitResponseInterval: 3000
            })

            connection.received(function (data) { connection.polling.received(data); });
            connection.start({ transport: 'webSockets' }).then(connection.polling.start);
        });
    </script>
    <form id="form1" runat="server">
    </form>
</body>
</html>
