using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieCtrl : MonoBehaviour
{
    public Despawn despawm;
    private void Awake()
    {
        despawm = GetComponent<Despawn>();
    }
    
}
