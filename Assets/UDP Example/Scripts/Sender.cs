using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sender : MonoBehaviour
{
    public string messageToSend = "Hello World!";

    private void Start()
    {
        // This is here just because I wanted a delay before the script attempted to send a message.
        Invoke("SendMessage", 3f);
    }

    public void SendMessage()
    {
        MessageSender(messageToSend);
    }

    static void MessageSender(string message)
    {
        Socket socketToUse = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");

        byte[] sendBuffer = Encoding.ASCII.GetBytes(message);
        IPEndPoint endPoint = new IPEndPoint(ipAddress, 5000);

        socketToUse.SendTo(sendBuffer, endPoint);

        Debug.Log("Message sent to the required IP address.");
    }
}
