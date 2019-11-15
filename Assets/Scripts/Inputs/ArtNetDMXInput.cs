using ArtNet.Packets;
using ArtNet.Sockets;
using System.Net;
using UnityEngine;

namespace DMX.Inputs
{
    public class ArtNetDMXInput : DMXInput
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
        #endregion

        #region Private Methods
        void OnEnable()
        {
            m_Listener = new ArtNetSocket();
            m_Listener.Open(IPAddress.Parse(LocalIP), null);
            m_Listener.NewPacket += OnInput;
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            if (m_Listener.PortOpen) m_Listener.Close();
        }
        void OnInput(object sender, NewPacketEventArgs<ArtNetPacket> args)
        {
            if (args.Packet.OpCode == ArtNet.Enums.ArtNetOpCodes.Dmx)
            {
                var dmxPacket = args.Packet as ArtNetDmxPacket;
                if (dmxPacket.Universe == ArtNetUniverse) m_DMXData = dmxPacket.DmxData;
            }
        }
        #endregion
    }
}