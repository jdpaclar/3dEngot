using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour {

    CameraRaycaster cameraRaycaster;
	// Use this for initialization
	void Start () {
        cameraRaycaster = GetComponent<CameraRaycaster>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            if (cameraRaycaster.gameObject.layer == (int)Layer.Enemy)
            {
                print("Clicked on an Enemy");
            }
        }
        print(cameraRaycaster.layerHit);
	}
}
