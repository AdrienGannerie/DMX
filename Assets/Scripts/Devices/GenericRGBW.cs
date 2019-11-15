using UnityEngine;

namespace DMX.Devices
{
    [RequireComponent(typeof(Light)), ExecuteInEditMode]
    public class GenericRGBW : DMXDevice
    {
        #region Properties
        [SerializeField, HideInInspector] Light m_Light;
        public override int Channels { get { return 4; } }

        [Range(0, 255), SerializeField] byte m_Red;
        public byte Red
        {
            get
            {
                return m_Red;
            }
            set
            {
                m_Red = value;
                UpdateColor();
            }
        }

        [Range(0, 255), SerializeField] byte m_Green;
        public byte Green
        {
            get
            {
                return m_Green;
            }
            set
            {
                m_Green = value;
                UpdateColor();
            }
        }

        [Range(0, 255), SerializeField] byte m_Blue;
        public byte Blue
        {
            get
            {
                return m_Blue;
            }
            set
            {
                m_Blue = value;
                UpdateColor();
            }
        }

        [Range(0, 255), SerializeField] byte m_White;
        public byte White
        {
            get
            {
                return m_White;
            }
            set
            {
                m_White = value;
                UpdateColor();
            }
        }
        #endregion

        #region Public Methods
        public override void SetData(byte[] channels)
        {
            if (channels.Length == Channels)
            {
                m_Red = channels[0];
                m_Green = channels[1];
                m_Blue = channels[2];
                m_White = channels[3];
                UpdateColor();
            }
            else
            {
                Debug.LogWarning("Wrong number of channels");
            }
        }
        public override byte[] GetData()
        {
            var result = new byte[Channels];
            result[0] = Red;
            result[1] = Green;
            result[2] = Blue;
            result[3] = White;
            return result;
        }
        #endregion

        #region Private Methods
        void Start()
        {
            m_Light = GetComponent<Light>();
        }
        void OnValidate()
        {
            UpdateColor();
        }
        void UpdateColor()
        {
            var color = m_Light.color;
            color.r = m_Red / 256f;
            color.g = m_Green / 256f;
            color.b = m_Blue / 256f;
            color += Color.white * 0.5f * m_White / 256f;
            m_Light.color = color;
        }
        #endregion
    }
}
