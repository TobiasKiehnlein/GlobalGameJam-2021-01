using System.Collections;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    public float Seconds;
    
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(Seconds);
        Destroy(gameObject);
    }
}