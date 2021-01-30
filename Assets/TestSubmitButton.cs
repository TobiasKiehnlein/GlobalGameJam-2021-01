using Networking;
using UnityEngine;

public class TestSubmitButton : MonoBehaviour
{
    [SerializeField] private bool _submitState;
    private bool _listening;
    private Player _player = Player.Unset;

    public void Submit(int player)
    {
        _submitState = !_submitState;
        _player = (Player) player;
        NetworkManager.LocalPlayer.SetSubmitState(_player, _submitState);
        if (!_listening)
        {
            _listening = true;
            NetworkManager.OnSubmitChange.AddListener(OnSubmitChange);
        }
    }

    private void OnSubmitChange(Player playerId, bool state)
    {
        Debug.Log($"Local change from {playerId} to {state}");
        if (_player == playerId)
        {
            _submitState = state;
        }
        else
        {
            Debug.Log("other one changed");
        }
    }
}