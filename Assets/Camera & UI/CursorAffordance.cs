using UnityEngine;

public class CursorAffordance : MonoBehaviour {

    [SerializeField] Texture2D walkCursor = null;
    [SerializeField] Texture2D attackCursor = null;
    [SerializeField] Texture2D unknownCursor = null;
    // 96 is the size of the cursor assets PSD
    [SerializeField] Vector2 cursorHotspot = new Vector2(96, 96);

    CameraRaycaster cameraRaycaster;
	// Use this for initialization
	void Start () {
        cameraRaycaster = GetComponent<CameraRaycaster>();
	}
	
	// Update is called once per frame
	void Update () {
        #region Notes, delete soon
        //if (Input.GetMouseButtonDown(0))
        //{
        //    print(cameraRaycaster.hit.collider.gameObject.tag.ToString());
        //}
        //print(cameraRaycaster.layerHit); 
        #endregion
        Texture2D renderCursor;

        switch(cameraRaycaster.layerHit)
        {
            case Layer.Walkable:
                renderCursor = walkCursor;
                break;
            case Layer.Enemy:
                renderCursor = attackCursor;
                break;
            case Layer.RaycastEndStop:
                renderCursor = unknownCursor;
                break;
            default:
                Debug.LogError("Cursor Error");
                return;

        }
        Cursor.SetCursor(renderCursor, cursorHotspot, CursorMode.Auto);
    }
}
