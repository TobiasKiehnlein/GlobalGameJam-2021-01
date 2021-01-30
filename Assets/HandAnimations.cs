using DG.Tweening;
using Shared.Scriptable_References;
using UnityEngine;

public class HandAnimations : MonoBehaviour
{
    [SerializeField] private Transform DropZone;
    [SerializeField] private PlayerCollection PlayerCollection;

    private Transform[] _children;
    private Vector3[] _positions;
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
    }

    public List<Tween> SelectIndex(int index)
    {
        var tweens = Deselect();
        var currentTransform = _children[index].transform;
        // Debug.Log(_canvas.localScale);
        var offset = (Vector3) (((RectTransform) currentTransform).rect.size * _canvas.localScale) / 2;
        tweens.Add(currentTransform.DOMove(DropZone.position - offset, .5f));
        tweens.Add(currentTransform.DOScale(new Vector3(.7f, .7f, .7f), .5f));

        return tweens;
    }

    public List<Tween> Deselect()
    {
        var tweens = new List<Tween>();
        for (var i = 0; i < _children.Length; i++)
        {
            tweens.Add(_children[i].DOMove(_positions[i], .5f));
            tweens.Add(_children[i].DOScale(Vector3.one, .5f));
        }

        return tweens;
    }
}