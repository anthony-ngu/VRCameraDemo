using UnityEngine;
using System.Collections;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class SocketClient : MonoBehaviour {
	public string server = "127.0.0.1";
	public int port = 6969;
	private static Socket ConnectSocket(string server, int port)
	{
		Socket s = null;
		IPHostEntry hostEntry = null;

		// Get host related information.
		hostEntry = Dns.GetHostEntry(server);

		// Loop through the AddressList to obtain the supported AddressFamily. This is to avoid 
		// an exception that occurs when the host IP Address is not compatible with the address family 
		// (typical in the IPv6 case). 
		foreach(IPAddress address in hostEntry.AddressList)
		{
			IPEndPoint ipe = new IPEndPoint(address, port);
			Socket tempSocket = 
				new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			
			tempSocket.Connect(ipe);
			
			if(tempSocket.Connected)
			{
				s = tempSocket;
				break;
			}
			else
			{
				continue;
			}
		}
		return s;
	}

	// This method requests the home page content for the specified server. 
	private static string SocketSendReceive(string server, int port) 
	{
		// Create a socket connection with the specified server and port.
		Socket s = ConnectSocket(server, port);
		
		if (s == null)
			return ("Connection failed");
		
		// Send request to the server.
		s.Send(Encoding.ASCII.GetBytes("a"));  
		
		return "";
	}

	// Use this for initialization
	void Start () {
		string result = SocketSendReceive(server, port); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
