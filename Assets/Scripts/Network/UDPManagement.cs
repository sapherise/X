using UnityEngine;
 
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

// 向服务器发送请求
 
public class UDPManagement {
    private string IP = "192.168.0.200";  
    private int port = 8001;
    IPEndPoint remoteEndPoint;
    UdpClient client;

    public UdpClient Start(){
        remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);
        client = new UdpClient(20001);
        return client;
    }

    // 发送消息
    public void SASendMessage(string content) {
        try {
            byte[] data = Encoding.UTF8.GetBytes(content);
            client.Send(data, data.Length, remoteEndPoint);
        } catch (Exception err){
            Debug.Log(err.ToString());
        }
    }
}