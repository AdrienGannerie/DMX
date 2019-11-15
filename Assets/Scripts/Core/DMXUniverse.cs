using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DMXUniverse : MonoBehaviour
{
    #region Properties
    [SerializeField] int m_Universe = 0;
    public int Universe
    {
        get
        {
            return m_Universe;
        }
        set
        {
            m_Universe = value;
        }
    }

    [SerializeField] DMXInput m_Input;
    public DMXInput Input
    {
        get
        {
            return m_Input;
        }
        set
        {
            m_Input = value;
        }
    }

    [SerializeField] DMXOutput m_Output;
    public DMXOutput Output
    {
        get
        {
            return m_Output;
        }
        set
        {
            m_Output = value;
        }
    }

    [SerializeField] List<DMXDevice> m_Devices = new List<DMXDevice>();
    public List<DMXDevice> Devices
    {
        get
        {
            return m_Devices;
        }
        set
        {
            m_Devices = value;
        }
    }
    #endregion

    #region Public Methods
    public void SetData(byte[] data)
    {
        foreach (var device in m_Devices)
        {
            device.SetData(data.Skip(device.Address).Take(device.Channels).ToArray());
        }
    }
    public byte[] GetData()
    {
        var result = new byte[512];
        foreach (var device in m_Devices)
        {
            Array.Copy(device.GetData(), 0, result, device.Address, device.Channels);
        }
        return result;
    }
    #endregion

    #region Private Methods
    void Update()
    {
        if (Input != null && Input.TryGetDMXData(out byte[] inputData)) SetData(inputData);
        if (Output != null) Output.Send(GetData());
    }
    #endregion
}