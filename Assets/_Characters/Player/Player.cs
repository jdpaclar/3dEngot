using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using RPG.CameraUI;
using RPG.Characters.Enemies;
using RPG.Core;
using RPG.Weapons;

namespace RPG.Characters.Player
{
    public class Player : MonoBehaviour, IDamageable
    {
        [SerializeField] float damagePerHit = 10;
        [SerializeField] int enemyLayer = 9;
        [SerializeField] float maxHealthPoints = 100f;
        [SerializeField] Weapon weaponInUse;
        [SerializeField] AnimatorOverrideController animatorOverrideController;

        Animator animator;
        CameraRaycaster cameraRaycaster;

        float currentHealthPoints;
        float lastHitTime = 0f;

        void Start()
        {
            RegisterForMouseClick();
            SetCurrentMaxHeatlh();
            PutWeaponInHand();
            SetupRuntimeAnimator();
        }

        private void SetCurrentMaxHeatlh()
        {
            currentHealthPoints = maxHealthPoints;
        }

        void SetupRuntimeAnimator()
        {
            animator = GetComponent<Animator>();
            animator.runtimeAnimatorController = animatorOverrideController;

            animatorOverrideController["DEFAULT ATTACK"] = weaponInUse.GetWeaponAttackAnimClip(); // remove string parameter
        }

        private void PutWeaponInHand()
        {
            var weaponPrefab = weaponInUse.GetWeaponPrefab();
            GameObject dominantHand = RequestDominantHand();
            var weapon = Instantiate(weaponPrefab, dominantHand.transform);
            weapon.transform.localPosition = weaponInUse.gripTransform.localPosition;
            weapon.transform.localRotation = weaponInUse.gripTransform.localRotation;
        }

        private GameObject RequestDominantHand()
        {
            var dominantHands = GetComponentsInChildren<DominantHand>();
            int numberOfDominantHands = dominantHands.Length;

            Assert.IsFalse(numberOfDominantHands <= 0, "No Dominant Hand Found on Player please add one!!!");
            Assert.IsFalse(numberOfDominantHands > 1, "Multiple Dominant Hand Scripts Found. Please Remove one!");

            return dominantHands[0].gameObject;
        }

        private void RegisterForMouseClick()
        {
            cameraRaycaster = FindObjectOfType<CameraRaycaster>();
            cameraRaycaster.notifyMouseClickObservers += OnMouseClick;
        }

        // on Attacking enemy
        void OnMouseClick(RaycastHit raycastHit, int layerHit)
        {
            if (layerHit == enemyLayer)
            {
                var enemy = raycastHit.collider.gameObject;

                // Check enemy is in range
                if (IsTargetInRange(enemy))
                {
                    AttackTarget(enemy);
                }
            }
        }

        private void AttackTarget(GameObject target)
        {
            var enemyComponent = target.GetComponent<Enemy>();
            if (Time.time - lastHitTime > weaponInUse.GetTimeBetweenHits())
            {
                animator.SetTrigger("Attack"); // TODO make const
                enemyComponent.TakeDamage(damagePerHit);
                lastHitTime = Time.time;
            }
        }

        private bool IsTargetInRange(GameObject target)
        {
            float distanceToTarget = (target.transform.position - transform.position).magnitude;
            return distanceToTarget <= weaponInUse.GetMaxAttackRange();
        }

        public float HealthAsPercentage
        {
            get
            {
                return currentHealthPoints / maxHealthPoints;
            }
        }

        public void TakeDamage(float damage)
        {
            currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);

            // Kill player
            //if (currentHealthPoints <= 0)
            //{
            //    Destroy(gameObject);
            //}
        }
    }

}