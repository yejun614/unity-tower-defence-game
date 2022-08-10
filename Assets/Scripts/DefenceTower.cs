using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DefenceTower : MonoBehaviour
{
    public TowerData data;
    public GameManager gameManager;
    public Tilemap tilemap;
    public bool isMovable = false;

    private float _timestamp = 0;
    private List<GameObject> _enemies = new List<GameObject>();

    private void Awake()
    {
        _timestamp = gameManager.playTime;
    }
    
    private void Update()
    {
        if (gameManager.playTime - _timestamp >= data.attackSpeed)
        {
            AttackEnemy();
            _timestamp = gameManager.playTime;
        }

        if (isMovable)
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = tilemap.WorldToCell(position);
            Vector3 worldPosition = tilemap.CellToWorld(cellPosition);
            worldPosition.z = 0;

            transform.position = worldPosition;
        }
    }

    private void AttackEnemy()
    {
        if (_enemies.Count == 0) return;
        
        GameObject enemyGameObject = _enemies[0];
        float minDistance = float.MaxValue;

        foreach (GameObject enemy in _enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            
            if (distance < minDistance)
            {
                minDistance = distance;
                enemyGameObject = enemy;
            }
        }
        
        GameObject effect = Instantiate(data.attackMotion, transform.position, Quaternion.identity);

        effect.transform.right = transform.position - enemyGameObject.transform.position;

        ShootingObject shootingObject = effect.GetComponent<ShootingObject>();
        shootingObject.attackPoint = data.attackPoint;
        shootingObject.targetTagName = data.enemy;
        shootingObject.direction = Vector3.left;
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
