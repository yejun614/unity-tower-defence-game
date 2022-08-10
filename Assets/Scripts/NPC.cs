using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public NPCData data;
    public List<Vector3> path;

    private int _pathIndex = 0;
    private float _heathPoint;
    private List<GameObject> _enemies = new List<GameObject>();
    
    private Animator _animator;

    private void Awake()
    {
        _heathPoint = data.maxHeathPoint;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        DeathCheck();

        if (_enemies.Count == 0)
        {
            Movement();
        }
        else
        {
            Attack();
        }
    }

    private void DeathCheck()
    {
        if (_heathPoint <= 0)
        {
            _animator.SetTrigger("death");
            Destroy(gameObject);
        }
    }

    private void Movement()
    {
        if (_pathIndex < path.Count)
        {
            Vector3 direction = path[_pathIndex] - transform.position;
            transform.Translate(direction * data.speed * Time.deltaTime);
            
            _animator.SetTrigger("walk");
            
            if (direction.x <= data.movementAccuracy &&
                direction.y <= data.movementAccuracy &&
                direction.z <= data.movementAccuracy)
            {
                _pathIndex ++;
            }
        }
    }

    private void Attack()
    {
        if (_enemies.Count == 0)
            throw new Exception("The list of enemy is empty");

        GameObject target = _enemies[0];
        float minDistance = float.MaxValue;

        foreach (GameObject enemy in _enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            
            if (distance < minDistance)
            {
                target = enemy;
                minDistance = distance;
            }
        }

        _animator.SetTrigger("walk");
        // target.GetComponent<NPC>().SetDamage(data.attackPoint);
        target.GetComponent<HeroHomeTower>().SetDamage(data.attackPoint);
        Destroy(gameObject);
    }

    public void SetDamage(float damage)
    {
        _animator.SetTrigger("hurt");
        _heathPoint -= damage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(data.enemy))
        {
            _enemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(data.enemy))
        {
            _enemies.Remove(other.gameObject);
        }
    }
}
