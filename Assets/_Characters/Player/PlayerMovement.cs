using System;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
using RPG.CameraUI;

namespace RPG.Characters.Player
{

    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(AICharacterControl))]
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class PlayerMovement : MonoBehaviour
    {
        ThirdPersonCharacter thirdPersonCharacter = null;   // A reference to the ThirdPersonCharacter on the object
        CameraRaycaster cameraRaycaster = null;
        Vector3 clickPoint;

        [SerializeField] const int walkableLayerNumber = 8;
        [SerializeField] const int enemyLayerNumber = 9;
        [SerializeField] const int uiLayerNumber = 5;
        GameObject walkTarget = null;

        AICharacterControl aiCharacterControl = null;

        bool isInDirectMode = false;  // TODO consider making serailize field later

        private void Start()
        {
            cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
            thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
            aiCharacterControl = GetComponent<AICharacterControl>();
            walkTarget = new GameObject("walkTarget");

            cameraRaycaster.notifyMouseClickObservers += ProcessMouseClick;
        }

        void ProcessMouseClick(RaycastHit raycastHit, int layerHit)
        {
            switch (layerHit)
            {
                case enemyLayerNumber:
                    GameObject enemy = raycastHit.collider.gameObject;
                    aiCharacterControl.SetTarget(enemy.transform);
                    break;
                case walkableLayerNumber:
                    walkTarget.transform.position = raycastHit.point;
                    aiCharacterControl.SetTarget(walkTarget.transform);
                    break;
                default:
                    Debug.Log("Click Unhandled");
                    return;
            }
        }

        #region Old Code For Reference
        //private void ProcessMouseMovement()
        //{
        //    if (Input.GetMouseButton(0))
        //    {
        //        clickPoint = cameraRaycaster.hit.point;  // So not set in default case
        //        switch (cameraRaycaster.currentLayerHit)
        //        {
        //            case Layer.Walkable:
        //                currentDestination = ShortDestination(clickPoint, walkMoveStopRadius);
        //                break;
        //            case Layer.Enemy:
        //                currentDestination = ShortDestination(clickPoint, attackMoveStopRadius);
        //                break;
        //            default:
        //                print("Unexpected!");
        //                return;
        //        }
        //    }

        //    WalkToDestination();
        //} 
        #endregion

        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.G)) // TODO can be placed into game menu and map to specific controller
            {
                isInDirectMode = !isInDirectMode;
            }

            if (isInDirectMode)
            {
                ProcessDirectMode();
            }
            else
            {
                //ProcessMouseMovement();
            }
        }

        // TODO make this get called again
        private void ProcessDirectMode()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 movement = v * cameraForward + h * Camera.main.transform.right;

            thirdPersonCharacter.Move(movement, false, false);
        }

        #region Old Code For Reference
        //private void WalkToDestination()
        //{
        //    var playerToClickPoint = currentDestination - transform.position;

        //    if (playerToClickPoint.magnitude >= 0)
        //    {
        //        thirdPersonCharacter.Move(currentDestination - transform.position, false, false);
        //    }
        //    else
        //    {
        //        thirdPersonCharacter.Move(Vector3.zero, false, false);
        //    }
        //}

        //private Vector3 ShortDestination(Vector3 destination, float shortening)
        //{
        //    Vector3 reductionVector = (destination - transform.position).normalized * shortening;
        //    return destination - reductionVector;
        //}

        //private void OnDrawGizmos()
        //{
        //    Gizmos.color = Color.black;
        //    // Transform is player
        //    // Current Click Target is the destination
        //    Gizmos.DrawLine(transform.position, clickPoint);
        //    Gizmos.DrawSphere(currentDestination, 0.1f);
        //    Gizmos.DrawSphere(clickPoint, 0.13f);

        //    // attack sphere gizmos
        //    Gizmos.color = new Color(255f, 0f, 0, .5f);
        //    Gizmos.DrawWireSphere(transform.position, attackMoveStopRadius);
        //} 
        #endregion
    }


}