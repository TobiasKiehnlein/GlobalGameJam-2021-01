using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Feedback : MonoBehaviour
{
    private TMP_Text _text;
    private Vector3 root;
    private const float LENGTH = 3;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TMP_Text>();
        _text.text = string.Empty;
        _text.enabled = false;
        root = transform.position;
    }

    public void TriggerFeedback(int value)
    {
        _text.enabled = true;
        _text.text = (value > 0 ? "+" : "-") + Math.Abs(value);
        var up = root;
        up.y += 2;
        transform.position = root;
        transform.DOPath(new[] { root, up }, LENGTH, PathType.CatmullRom);

        StartCoroutine(DisableRoutine());
    }

    IEnumerator DisableRoutine()
    {
        yield return new WaitForSeconds(LENGTH);

        _text.enabled = false;
    }
}