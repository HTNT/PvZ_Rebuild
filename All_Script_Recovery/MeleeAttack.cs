using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    protected AttackCtrl atkCtrl;
    public float damageDeal;
    // Start is called before the first frame update
    void Start()
    {
        atkCtrl = GetComponent<AttackCtrl>();
        DamageSender dmgSend = this.gameObject.AddComponent<DamageSender>();
        dmgSend.damageDeal = damageDeal;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Pee") return;
        else
        this.atkCtrl.despawn.Dead();
        
    }
}
