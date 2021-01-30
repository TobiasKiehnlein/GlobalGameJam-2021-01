﻿using System;
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
        }

        [Command]
        public void SetSubmitState(Player player, bool submitState)
        {
            SubmitStateChanged(player, submitState);
        }

        [ClientRpc]
        private static void SubmitStateChanged(Player player, bool submitState)
        {
            NetworkManager.OnSubmitChange.Invoke(player, submitState);
        }

        [Command]
        public void SetSelectedCard(Player player, string card)
        {
            SelectedCardChanged(player, card);
        }

        [ClientRpc]
        private static void SelectedCardChanged(Player player, string card)
        {
            NetworkManager.OnSelectedCardChanged.Invoke(player, card);
        }

        [Command]
        public void ChangeHand(Player player, string card1, string card2, string card3)
        {
            HandChanged(player, card1, card2, card3);
        }

        [ClientRpc]
        private static void HandChanged(Player player, string card1, string card2, string card3)
        {
            NetworkManager.OnHandChanged.Invoke(player, card1, card2, card3);
        }
        
        [Command]
        public void ChangeEnvironment(string[] cards)
        {
            EnvironmentChanged(cards);
        }

        [ClientRpc]
        private static void EnvironmentChanged(string[] cards)
        {
            NetworkManager.OnEnvironmentChanged.Invoke(cards);
        }
    }
}