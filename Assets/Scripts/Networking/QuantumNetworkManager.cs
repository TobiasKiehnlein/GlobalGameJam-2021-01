using System;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

namespace Networking
{
    [Serializable]
    public class OnSubmitChangeEvent : UnityEvent<Player, bool>
    {
    }

    [Serializable]
    public class OnChangeHandEvent : UnityEvent<Player, string, string, string>
    {
    }

    [Serializable]
    public class OnChangeEnvironmentEvent : UnityEvent<string[]>
    {
    }

    [Serializable]
    public class OnSelectCardEvent : UnityEvent<Player, string>
    {
    }

    public class QuantumNetworkManager : NetworkManager
    {
        public static OnSubmitChangeEvent OnSubmitChange;
        public static OnChangeHandEvent OnHandChanged;
        public static OnChangeEnvironmentEvent OnEnvironmentChanged;
        public static OnSelectCardEvent OnSelectedCardChanged;
        public static UnityEvent OnClientJoin;
        private static NetworkPlayer s_localPlayer;

        public static NetworkPlayer LocalPlayer
        {
            get => s_localPlayer;
            set
            {
                s_localPlayer = value;
                OnClientJoin.Invoke();
            }
        }

        public override void Awake()
        {
            base.Awake();

            OnSubmitChange = new OnSubmitChangeEvent();
            OnEnvironmentChanged = new OnChangeEnvironmentEvent();
            OnHandChanged = new OnChangeHandEvent();
            OnSelectedCardChanged = new OnSelectCardEvent();
            OnClientJoin = new UnityEvent();
        }
    }
}