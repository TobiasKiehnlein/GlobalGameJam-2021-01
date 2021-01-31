﻿using System.Collections.Generic;
using DG.Tweening;
using Shared.Scriptable_References;
using UnityEngine;

namespace Quantum_Decks.Card_System
{
    public class HandAnimations : MonoBehaviour
    {
        [SerializeField] private AnimationsReference _animationsReference;

        [SerializeField] private Transform DropZone;

        private Transform[] _children;
        private Vector3[] _positions;
        private RectTransform _canvas;
        private RectTransform _staple;

        private void Start()
        {
            _canvas = GameObject.Find("MasterCanvas").GetComponent<RectTransform>();
            _staple = GameObject.Find("CardSpawn").GetComponent<RectTransform>();

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


        public List<Tween> RespawnCards()
        {
            var tweens = new List<Tween>();

            foreach (var child in _children)
            {
                child.transform.position = _staple.position;
                child.transform.localScale = Vector3.one * _animationsReference.StapleScaleFactor;
                child.transform.rotation = Quaternion.Euler(0, 180, 180);
            }

            return Deselect();
        }
    }
}