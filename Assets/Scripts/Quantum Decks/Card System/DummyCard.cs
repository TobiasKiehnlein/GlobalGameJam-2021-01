using System.Collections;
using DG.Tweening;
using UnityEngine;

public class DummyCard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOScale(Vector3.zero, .5f);
        StartCoroutine(ClosingCoroutine());
    }

    IEnumerator ClosingCoroutine()
    {
        yield return new WaitForSeconds(.5f);
        // Destroy(gameObject);
    }
}