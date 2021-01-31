using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Quantum_Decks.Audio;
using Shared.Scriptable_References;
using UnityEngine;

namespace Quantum_Decks.Card_System
{
    public class HandAnimations : MonoBehaviour
    {
        [SerializeField] private AnimationsReference _animationsReference;
        [SerializeField] private RectTransform Staple;
        [SerializeField] private RectTransform OtherStaple;

        [SerializeField] private Transform DropZone;
        [SerializeField] private RectTransform Void;

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

            RespawnCards(false);
        }

        public List<Tween> SelectIndex(int index)
        {
            AudioManager.Instance.PlaySfx(Random.Range(0, 1) > .5f ? SFX.PLAYING_A_CARD_1 : SFX.PLAYING_A_CARD_2);
            var tweens = Deselect();
            var currentTransform = _children[index].transform;
            // Debug.Log(_canvas.localScale);
            // var offset = CalculateOffset((RectTransform) currentTransform, SMALL_SCALE_FACTOR);
            tweens.Add(currentTransform.DOMove(DropZone.position, _animationsReference.Duration));
            tweens.Add(currentTransform.DOScale(new Vector3(_animationsReference.SmallScaleFactor, _animationsReference.SmallScaleFactor, _animationsReference.SmallScaleFactor), _animationsReference.Duration));

            return tweens;
        }

        public void Hover(int index, bool isHover)
        {
            var currentTransform = _children[index].transform;
            currentTransform.DOMoveY(_positions[index].y + (isHover ? _animationsReference.HoverAmount : 0), .2f);
        }

        public List<Tween> Deselect()
        {
            FeedbackManager.Instance.TriggerFeedback(Networking.Player.One, 10);
            var tweens = new List<Tween>();
            for (var i = 0; i < _children.Length; i++)
            {
                if (Vector3.Distance(_children[i].position, _positions[i]) > _animationsReference.HoverAmount)
                    tweens.Add(_children[i].DOMove(_positions[i], _animationsReference.Duration));
                tweens.Add(_children[i].DOScale(Vector3.one, _animationsReference.Duration));
                tweens.Add(_children[i].DORotate(Vector3.zero, _animationsReference.Duration));
            }

            return tweens;
        }

        public List<Tween> EndRound()
        {
            AudioManager.Instance.PlaySfx(SFX.MOVING_CARDS);
            return _children.Select((t, i) => t.DOMove(Vector3.Distance(t.position, _positions[i]) > _animationsReference.HoverAmount ? OtherStaple.position : Void.position, _animationsReference.Duration)).Cast<Tween>().ToList();
        }


        public List<Tween> RespawnCards(bool deselect = true)
        {
            AudioManager.Instance.PlaySfx(Random.Range(0, 1) > .5f ? SFX.DRAWING_CARD_1 : SFX.DRAWING_CARD_2);
            var tweens = new List<Tween>();

            foreach (var child in _children)
            {
                child.transform.position = Staple.position;
                child.transform.localScale = Vector3.one * _animationsReference.StapleScaleFactor;
                child.transform.rotation = Quaternion.Euler(0, 180, 180);
            }

            if (deselect) Deselect();

            return tweens;
        }
    }
}