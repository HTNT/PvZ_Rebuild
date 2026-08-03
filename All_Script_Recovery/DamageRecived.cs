using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageRecived : MonoBehaviour
{
    public float health;
    public virtual void DamageTaken(float dmg)
    {
        this.health -= dmg;
        if (this.health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
