using ArtNet.Packets;
using ArtNet.Sockets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class DMXListener : MonoBehaviour
{
    public int Universe = 0;
    public byte[] Data = new byte[512];
    ArtNetSocket m_Listener = new ArtNetSocket();

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAddress = ipHostInfo.AddressList.First(a => a.AddressFamily == AddressFamily.InterNetwork);
        m_Listener.Open(ipAddress, null);
        m_Listener.NewPacket += OnReceive;
    }

    private void OnDestroy()
    {
        if(m_Listener.PortOpen)
        {
            m_Listener.Close();
        }
    }
    private void OnDisable()
    {
        if (m_Listener.PortOpen)
        {
            m_Listener.Close();
        }
    }

    private void OnReceive(object sender, NewPacketEventArgs<ArtNetPacket> e)
    {
        Debug.Log("Receive");
        if(e.Packet.OpCode == ArtNet.Enums.ArtNetOpCodes.Dmx)
        {
            var dmxPacket = (e.Packet as ArtNetDmxPacket);
            Data = dmxPacket.DmxData;
            Universe = dmxPacket.Universe;
        }

    }
}
