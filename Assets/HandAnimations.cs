using System.IO.IsolatedStorage;
using DG.Tweening;
using Networking;
using Shared.Scriptable_References;
using UnityEngine;

public class HandAnimations : MonoBehaviour
{
    [SerializeField] private Transform DropZone;
    [SerializeField] private Player ThePlayer;
    [SerializeField] private PlayerCollection PlayerCollection;

    private Transform[] _children;
    private Vector3[] _positions;
    private Quantum_Decks.Player.Player _player;
    private RectTransform _canvas;

    private void Start()
    {
        _canvas = GameObject.Find("MasterCanvas").GetComponent<RectTransform>();

        _children = new Transform[transform.childCount];
        _positions = new Vector3[_children.Length];

        var count = 0;
        foreach (Transform child in transform)
        {
            _children[count] = child;
            _positions[count] = child.transform.position;

            count++;
        }

        _player = PlayerCollection.GetPlayer(ThePlayer);
    }

    public void SelectIndex(int index)
    {
        Deselect();
        var currentTransform = _children[index].transform;
        // Debug.Log(_canvas.localScale);
        var offset = (Vector3) (((RectTransform) currentTransform).rect.size * _canvas.localScale) / 2;
        currentTransform.DOMove(DropZone.position - offset, .5f);
        currentTransform.DOScale(new Vector3(.7f, .7f, .7f), .5f);
    }

    public void Deselect()
    {
        for (int i = 0; i < _children.Length; i++)
        {
            _children[i].DOMove(_positions[i], .5f);
            _children[i].DOScale(Vector3.one, .5f);
        }
    }

    public Quantum_Decks.Player.Player GetPlayer()
    {
        return _player;
    }
}