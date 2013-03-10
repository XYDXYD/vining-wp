using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using WebSocket4Net;
using SuperSocket.ClientEngine;

namespace ViningWP
{
    public partial class MainPage : PhoneApplicationPage
    {
        private string serverURL = "ws://192.168.10.1:10888";        private WebSocket websocket;        private bool is_connected = false;

        public MainPage()
        {
            InitializeComponent();

            if (serverURL == "")            {                            }            else            {                websocket = new WebSocket(serverURL);                websocket.Closed += new EventHandler(websocket_Closed);                websocket.Error += new EventHandler<SuperSocket.ClientEngine.ErrorEventArgs>(websocket_Error);                websocket.MessageReceived += new EventHandler<MessageReceivedEventArgs>(websocket_MessageReceived);                websocket.Opened += new EventHandler(websocket_Opened);                                websocket.Open();            }
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            
        }                      private void websocket_Opened(object sender, EventArgs e)        {            is_connected = true;
            websocket.Send("");
            System.Diagnostics.Debug.WriteLine("Opened!");        }        private void websocket_Error(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)        {                    }        private void websocket_Closed(object sender, EventArgs e)        {                    }        private void websocket_MessageReceived(object sender, MessageReceivedEventArgs e)        {
            System.Diagnostics.Debug.WriteLine(e.Message);        }        private void sendMessage()        {            if (is_connected)            {                websocket.Send("");            }        }    }
}