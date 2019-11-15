using UnityEngine;

namespace DMX
{
    public abstract class DMXOutput : MonoBehaviour
    {
        #region Public Methods
        public abstract void Send(byte[] dmxData);
        #endregion
    }
}