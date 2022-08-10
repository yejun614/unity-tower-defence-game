using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDestory : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(WaitAndDestroy());
    }

    private IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        
        Destroy(gameObject);
    }
}
