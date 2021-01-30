using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

namespace Networking
{
    public class NetworkPlayer : NetworkBehaviour
    {
        private void Start()
        {
            if (!isLocalPlayer) return;
            NetworkManager.LocalPlayer = this;
            NetworkManager.OnSubmitChange = new OnSubmitChangeEvent();
        }

        void HandleData()
        {
            if (isLocalPlayer)
            {
                // Do local player stuff
            }
        }

        [Command]
        public void SetSubmitState(Player player, bool submitState)
        {
            SubmitStateChanged(player, submitState);
        }

        [ClientRpc]
        private void SubmitStateChanged(Player player, bool submitState)
        {
            NetworkManager.OnSubmitChange.Invoke(player, submitState);
        }

        // [Command]
        // public void SetSelectedCard(Player player, string cardId)
        // {
        //     Debug.Log($"Spieler {player} hat Karte {cardId} ausgewählt!!! (Server)");
        // }
        //
        // [ClientRpc]
        // public void ConfirmSelectedCard(Player player, string cardId)
        // {
        //     Debug.Log($"Spieler {player} hat Karte {cardId} erfolgreich ausgewählt. (Client)");
        // }


        private void Update()
        {
            HandleData();
        }
    }
}