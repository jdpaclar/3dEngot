using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Weapons
{

    public class Projectile : MonoBehaviour
    {
        [SerializeField] float projectileSpeed;
        [SerializeField] GameObject shooter; // Detect and Inspect when paused who the shooter is

        const float DESTROY_DELAY = 0.01f;
        float damageCaused;

        public void SetShooter(GameObject shooter)
        {
            this.shooter = shooter;
        }

        public void SetDamage(float damage)
        {
            damageCaused = damage;
        }

        public float GetDefaultLaunchSpeed()
        {
            return projectileSpeed;
        }

        private void OnCollisionEnter(Collision collision)
        {
            var layerCollidedWith = collision.gameObject.layer;
            if (shooter && layerCollidedWith != shooter.layer)
            {
                DamageIfDamagables(collision);
            }
        }

        private void DamageIfDamagables(Collision collision)
        {
            Component damageable = collision.gameObject.GetComponent(typeof(IDamageable));

            if (damageable)
            {
                (damageable as IDamageable).TakeDamage(damageCaused);
            }

            Destroy(gameObject, DESTROY_DELAY);
        }
    }

}