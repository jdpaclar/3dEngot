﻿using UnityEngine;

namespace RPG.Characters.Enemies
{
    // Add a UI Socket transform to your enemy
    // Attack this script to the socket
    // Link to a canvas prefab that contains NPC UI
    public class EnemyUI : MonoBehaviour
    {

        // Works around Unity 5.5's lack of nested prefabs
        [Tooltip("The UI canvas prefab")]
        [SerializeField]
        GameObject enemyCanvasPrefab = null;

        Camera cameraToLookAt;

        // Use this for initialization 
        void Start()
        {
            cameraToLookAt = Camera.main;
            //Instantiate(enemyCanvasPrefab, transform.position, Quaternion.identity, transform); // OLD code that seems to rotate enemy healthbar
            Instantiate(enemyCanvasPrefab, transform.position, transform.rotation, transform); // TODO: verify if this is the correct solution
        }

        // Update is called once per frame 
        void LateUpdate()
        {
            transform.LookAt(cameraToLookAt.transform);
            transform.rotation = Quaternion.LookRotation(cameraToLookAt.transform.forward);
        }
    }
}