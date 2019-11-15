using ArtNet.Packets;
using ArtNet.Sockets;
using System.Net;
using UnityEngine;

public class DMXInput : MonoBehaviour
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

    ArtNetSocket m_Listener;
    byte[] m_DMXData;
    #endregion

    #region Public Methods
    public bool TryGetDMXData(out byte[] dmxData)
    {
        dmxData = m_DMXData;
        m_DMXData = null;
        return dmxData != null && dmxData.Length == 512;
    }
    #endregion

    #region Private Methods
    void OnEnable()
    {
        m_Listener = new ArtNetSocket();
        m_Listener.Open(IPAddress.Parse(LocalIP), null);
        m_Listener.NewPacket += OnInput;
    }
    void OnDisable()
    {
        m_DMXData = null;
        if (m_Listener.PortOpen) m_Listener.Close();
    }
    void OnInput(object sender, NewPacketEventArgs<ArtNetPacket> args)
    {
        if (args.Packet.OpCode == ArtNet.Enums.ArtNetOpCodes.Dmx)
        { 
            var dmxPacket = args.Packet as ArtNetDmxPacket;
            if(dmxPacket.Universe == ArtNetUniverse) m_DMXData = dmxPacket.DmxData;
        }
    }
    #endregion
}
