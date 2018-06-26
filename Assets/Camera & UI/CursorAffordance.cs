using UnityEngine;

[RequireComponent(typeof(CameraRaycaster))]
public class CursorAffordance : MonoBehaviour {

    [SerializeField] Texture2D walkCursor = null;
    [SerializeField] Texture2D attackCursor = null;
    [SerializeField] Texture2D unknownCursor = null;
    [SerializeField] Vector2 cursorHotspot = new Vector2(0, 0);

    CameraRaycaster cameraRaycaster;
	// Use this for initialization
	void Start () {
        cameraRaycaster = GetComponent<CameraRaycaster>();
        cameraRaycaster.LayerChangeObservers += OnLayerChange;
	}
	
    void OnLayerChange(Layer currentLayer)
    {
        Texture2D renderCursor;

        switch (currentLayer)
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

	// Update is called once per frame
	void LateUpdate () {
        #region Notes, delete soon
        //if (Input.GetMouseButtonDown(0))
        //{
        //    print(cameraRaycaster.hit.collider.gameObject.tag.ToString());
        //}
        //print(cameraRaycaster.layerHit); 
        #endregion
        
    }
}
