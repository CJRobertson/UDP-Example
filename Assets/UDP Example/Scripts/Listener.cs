using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Listener : MonoBehaviour
{
    public int portNumber = 5000;

    Thread udpListeningThread;
    Thread udpSendingThread;
    UdpClient receivingClient;

    void Start()
    {
        InitializeListenerThread();
    }

    private void InitializeListenerThread()
    {
        // Create a new thread.
        udpListeningThread = new Thread(new ThreadStart(UDPListener));

        // Run in background
        udpListeningThread.IsBackground = true;
        udpListeningThread.Start();

        Debug.Log("Started on port number: " + portNumber.ToString());
    }

    public void UDPListener()
    {
        receivingClient = new UdpClient(portNumber);

        while (true)
        {
            // While Listening 
            try
            {
                IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Any, 0);

                // Blocks until a message is returned on this socket from a host.
                byte[] receiveBytes = receivingClient.Receive(ref remoteIPEndPoint);

                if (receiveBytes != null)
                {
                    string returnData = Encoding.ASCII.GetString(receiveBytes);
                    Debug.Log("Message Received: " + returnData.ToString());
                }
            }
            catch (Exception exception)
            {
                Debug.Log(exception.ToString());
            }
        }
    }
}
