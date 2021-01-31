using System;
using Mirror;
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

    public class NetworkManager : Mirror.NetworkManager
    {
        public static OnSubmitChangeEvent OnSubmitChange;
        public static OnChangeHandEvent OnHandChanged;
        public static OnChangeEnvironmentEvent OnEnvironmentChanged;
        public static OnSelectCardEvent OnSelectedCardChanged;
        public static UnityEvent OnClientJoin;
        public static NetworkPlayer LocalPlayer { get; set; }

        public override void Start()
        {
            base.Start();

            OnSubmitChange = new OnSubmitChangeEvent();
            OnEnvironmentChanged = new OnChangeEnvironmentEvent();
            OnHandChanged = new OnChangeHandEvent();
            OnSelectedCardChanged = new OnSelectCardEvent();
            OnClientJoin = new UnityEvent();
        }

        public override void OnClientConnect(NetworkConnection conn)
        {
            base.OnClientConnect(conn);
            OnClientJoin.Invoke();
        }
    }
}