using Mirror;
using UnityEngine;

namespace Shared.Scriptable_References
{
    public class QuantumNetworkIdentity : NetworkIdentity
    {
        [SerializeField] private NetworkSettingReference _networkSettingReference;
        
        public void OnDisable()
        {
            enabled = _networkSettingReference.IsOnline();
        }
    }
}