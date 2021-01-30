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

    public class NetworkManager : Mirror.NetworkManager
    {
        public static OnSubmitChangeEvent OnSubmitChange;
        public static NetworkPlayer LocalPlayer { get; set; }

        public override void OnStartServer()
        {
            base.OnStartServer();
            Debug.Log("Server started");
        }

        public override void OnClientConnect(NetworkConnection conn)
        {
            base.OnClientConnect(conn);
            Debug.Log("Connected to server");
        }

        // public override void OnServerAddPlayer(NetworkConnection conn)
        // {
        //     base.OnServerAddPlayer(conn, out var player);
        //     var networkPlayer = player.GetComponent<NetworkPlayer>();
        //     Debug.Log("Player added");
        //     if (networkPlayer != null)
        //     {
        //         OnSubmitChange.AddListener((p, s) => Debug.Log($"{p} - {s}"));
        //     }
        // }

        public override void OnClientDisconnect(NetworkConnection conn)
        {
            base.OnClientDisconnect(conn);
            Debug.Log("Disconnected from Server");
        }
    }
}