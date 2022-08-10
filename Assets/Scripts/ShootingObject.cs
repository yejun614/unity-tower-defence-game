using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingObject : MonoBehaviour
{
    public GameObject boomGameObject;
    public float boomTime = 1;
    
    public float attackPoint = 0;
    public float speed = 10;
    public Vector3 direction = Vector3.zero;
    public float timeLimit = 3;
    public String targetTagName;

    private float _timestamp = 0;

    private void Update()
    {
        _timestamp += Time.deltaTime;

        if (_timestamp >= timeLimit)
        {
            Destroy(gameObject);
        }
        
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTagName))
        {
            other.GetComponent<NPC>().SetDamage(attackPoint);
            StartCoroutine(BoomEffect());
        }
    }

    private IEnumerator BoomEffect()
    {
        yield return new WaitForSeconds(0.1f);
        
        GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        Instantiate(boomGameObject, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
