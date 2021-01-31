using System;
using UnityEngine;

namespace Shared.Scriptable_References
{
    [CreateAssetMenu(menuName = "Quantum/References/Network Settings")]
    public class NetworkSettingReference : ScriptableReference<NetworkSetting>
    {
        public string ServerIp;

        public bool IsLocal()
        {
            return Value == NetworkSetting.Local;
        }

        public bool IsOnline()
        {
            return Value == NetworkSetting.Online;
        }
    }

    public enum NetworkSetting
    {
        Local,
        Online
    }
}