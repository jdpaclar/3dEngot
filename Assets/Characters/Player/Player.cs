﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] float damagePerHit = 10;
    [SerializeField] float minTimeBetweenHits = 0.5f;
    [SerializeField] int enemyLayer = 9;
    [SerializeField] float maxHealthPoints = 100f;
    [SerializeField] float maxAttackRange = 2f;
    [SerializeField] Weapon weaponInUse;

    CameraRaycaster cameraRaycaster;

    float currentHealthPoints;
    float lastHitTime = 0f;
    
    void Start()
    {
        RegisterForMouseClick();
        currentHealthPoints = maxHealthPoints;
        PutWeaponInHand();
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
            if ((enemy.transform.position - transform.position).magnitude > maxAttackRange)
                return;

            var enemyComponent = enemy.GetComponent<Enemy>();

            if (Time.time - lastHitTime > minTimeBetweenHits)
            {
                enemyComponent.TakeDamage(damagePerHit);
                lastHitTime = Time.time;
            }
        }
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
