using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int health = 100;

    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        if (health < 0) 
        {
            health = 0;
        }
        if (health == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        throw new NotImplementedException();
    }
}
