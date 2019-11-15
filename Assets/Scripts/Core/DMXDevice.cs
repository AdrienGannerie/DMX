using UnityEngine;

namespace DMX
{
    public abstract class DMXDevice : MonoBehaviour
    {
        #region Properties
        [Range(0, 255), SerializeField] int m_Address;
        public int Address
        {
            get
            {
                return m_Address;
            }
            set
            {
                m_Address = value;
            }
        }

        public abstract int Channels { get; }
        #endregion

        #region Public Methods
        public abstract void SetData(byte[] dmxData);
        public abstract byte[] GetData();
        #endregion
    }
}