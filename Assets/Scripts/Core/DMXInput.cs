using UnityEngine;

namespace DMX
{
    public abstract class DMXInput : MonoBehaviour
    {
        #region Properties
        protected byte[] m_DMXData;
        #endregion

        #region Public Methods
        public virtual bool TryGetDMXData(out byte[] dmxData)
        {
            dmxData = m_DMXData;
            m_DMXData = null;
            return dmxData != null && dmxData.Length == 512;
        }
        #endregion

        #region Private Methods
        protected virtual void OnDisable()
        {
            m_DMXData = null;
        }
        #endregion
    }
}