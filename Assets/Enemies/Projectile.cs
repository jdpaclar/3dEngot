using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;
    public float damageCaused;

    private void OnTriggerEnter(Collider collider)
    {
        Component damageable = collider.gameObject.GetComponent(typeof(IDamageable));

        if (damageable)
        {
            (damageable as IDamageable).TakeDamage(damageCaused);
        }
    }
}
