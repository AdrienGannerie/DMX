using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using UnityEngine;
using ArtNet.Packets;

public class DmxController : MonoBehaviour
{
    [Header("dmx devices")]
    public UniverseDevices[] m_Universes;
    Dictionary<int, byte[]> m_dmxDataByUniverse;

    private void OnValidate()
    {
        foreach (var universe in m_Universes)
            universe.Initialize();
    }

    void Start()
    {

        m_dmxDataByUniverse = new Dictionary<int, byte[]>();
    }

    public void Set(ArtNetDmxPacket dmxPacket)
    {
        m_dmxDataByUniverse[dmxPacket.Universe] = dmxPacket.DmxData;
    }

    private void Update()
    {
        var universes = m_dmxDataByUniverse.Keys.ToArray();
        for (var i = 0; i < universes.Length; i++)
        {
            var universe = universes[i];
            var dmxData = m_dmxDataByUniverse[universe];
            if (dmxData == null)
                continue;

            var universeDevices = m_Universes.Where(u => u.universe == universe).FirstOrDefault();
            if (universeDevices != null)
                foreach (var d in universeDevices.devices)
                    d.SetData(dmxData.Skip(d.startChannel).Take(d.NumChannels).ToArray());

            m_dmxDataByUniverse[universe] = null;
        }
    }

    static IPAddress FindFromHostName(string hostname)
    {
        var address = IPAddress.None;
        try
        {
            if (IPAddress.TryParse(hostname, out address))
                return address;

            var addresses = Dns.GetHostAddresses(hostname);
            for (var i = 0; i < addresses.Length; i++)
            {
                if (addresses[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    address = addresses[i];
                    break;
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogErrorFormat(
                "Failed to find IP for :\n host name = {0}\n exception={1}",
                hostname, e);
        }
        return address;
    }

    [System.Serializable]
    public class UniverseDevices
    {
        public string universeName;
        public int universe;
        public DMXDevice[] devices;

        public void Initialize()
        {
            var startChannel = 0;
            foreach (var d in devices)
                if (d != null)
                {
                    d.startChannel = startChannel;
                    startChannel += d.NumChannels;
                    d.name = string.Format("{0}:({1},{2:d3}-{3:d3})", d.GetType().ToString(), universe, d.startChannel, startChannel - 1);
                }
            if (512 < startChannel)
                Debug.LogErrorFormat("The number({0}) of channels of the universe {1} exceeds the upper limit(512 channels)!", startChannel, universe);
        }
    }
}
