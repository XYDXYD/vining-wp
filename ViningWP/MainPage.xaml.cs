﻿using System;
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
        private string serverURL = "ws://192.168.10.1:10888";

        public MainPage()
        {
            InitializeComponent();

            if (serverURL == "")
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
            websocket.Send("");
            System.Diagnostics.Debug.WriteLine("Opened!");
            System.Diagnostics.Debug.WriteLine(e.Message);
}