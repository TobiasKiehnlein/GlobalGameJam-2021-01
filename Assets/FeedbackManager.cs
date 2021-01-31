using Networking;
using UnityEngine;

public class FeedbackManager : MonoBehaviour
{
    private static FeedbackManager _instance;

    public static FeedbackManager Instance => _instance;

    [SerializeField] private Feedback _feedbackLeft;
    [SerializeField] private Feedback _feedbackRight;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void TriggerFeedback(Player playerId, int amount)
    {
        if (playerId == Player.One)
        {
            _feedbackLeft.TriggerFeedback(amount);
        }
        else if (playerId == Player.Two)
        {
            _feedbackRight.TriggerFeedback(amount);
        }
    }
}