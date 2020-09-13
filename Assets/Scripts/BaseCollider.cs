using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCollider : MonoBehaviour {

    [SerializeField] GameObject baseExplosion;

    private BaseHealth baseHealth;
    

    private void Start()
    {
        baseHealth = GetComponentInParent<BaseHealth>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.gameObject.CompareTag("Enemy")) //Если это враг стрелляет
        baseHealth.UpdateHealth();
    }

    
}
