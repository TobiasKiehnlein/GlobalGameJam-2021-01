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
            QuantumNetworkManager.LocalPlayer = this;
        }

        [Command]
        public void SetSubmitState(Player player, bool submitState)
        {
            SubmitStateChanged(player, submitState);
        }

        [ClientRpc]
        private void SubmitStateChanged(Player player, bool submitState)
        {
            QuantumNetworkManager.OnSubmitChange.Invoke(player, submitState);
        }

        [Command]
        public void SetSelectedCard(Player player, string card)
        {
            SelectedCardChanged(player, card);
        }

        [ClientRpc]
        private void SelectedCardChanged(Player player, string card)
        {
            QuantumNetworkManager.OnSelectedCardChanged.Invoke(player, card);
        }

        [Command]
        public void ChangeHand(Player player, string card1, string card2, string card3)
        {
            HandChanged(player, card1, card2, card3);
        }

        [ClientRpc]
        private void HandChanged(Player player, string card1, string card2, string card3)
        {
            QuantumNetworkManager.OnHandChanged.Invoke(player, card1, card2, card3);
        }
        
        [Command]
        public void ChangeEnvironment(string[] cards)
        {
            EnvironmentChanged(cards);
        }

        [ClientRpc]
        private void EnvironmentChanged(string[] cards)
        {
            QuantumNetworkManager.OnEnvironmentChanged.Invoke(cards);
        }
    }
}