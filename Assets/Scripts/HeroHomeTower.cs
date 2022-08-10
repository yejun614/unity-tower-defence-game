using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroHomeTower : MonoBehaviour
{
    public GameManager gameManager;

    public void SetDamage(float attackPoint)
    {
        gameManager.heathPoint -= attackPoint;
    }
}
