using ArtNet.Sockets;
using ArtNet.Packets;
using System.Net;
using UnityEngine;

public class DMXOutput : MonoBehaviour
{
    #region Properties
    [SerializeField] short m_ArtNetUniverse;
    public short ArtNetUniverse
    {
        get
        {
            return m_ArtNetUniverse;
        }
        set
        {
            m_ArtNetUniverse = value;
        }
    }

    [SerializeField] string m_LocalIP = "127.0.0.1";
    public string LocalIP
    {
        get
        {
            return m_LocalIP;
        }
        set
        {
            m_LocalIP = value;
        }
    }

    [SerializeField] string m_RemoteIP = "127.0.0.1";
    public string RemoteIP
    {
        get
        {
            return m_RemoteIP;
        }
        set
        {
            m_RemoteIP = value;
        }
    }

    ArtNetSocket m_Sender;
    #endregion

    #region Public Methods
    public void Send(byte[] dmxData)
    {
        if(m_Sender != null && m_Sender.PortOpen)
        {
            var artNetPacket = new ArtNetDmxPacket { Universe = ArtNetUniverse, DmxData = dmxData };
            m_Sender.Send(artNetPacket, IPAddress.Parse(RemoteIP));
        }
    }
    #endregion

    #region Private Methods
    void OnEnable()
    {
        m_Sender = new ArtNetSocket();
        m_Sender.Open(IPAddress.Parse(LocalIP), null);
    }
    void OnDisable()
    {
        if (m_Sender.PortOpen) m_Sender.Close();
    }
    #endregion
}
