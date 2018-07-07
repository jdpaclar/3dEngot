using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;
    float damageCaused;

    public void SetDamage(float damage)
    {
        damageCaused = damage;
    }

    private void OnTriggerEnter(Collider collider)
    {
        Component damageable = collider.gameObject.GetComponent(typeof(IDamageable));

        if (damageable)
        {
            (damageable as IDamageable).TakeDamage(damageCaused);
        }
    }
}
