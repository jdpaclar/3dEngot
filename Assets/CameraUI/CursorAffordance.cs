using UnityEngine;

[RequireComponent(typeof(CameraRaycaster))]
public class CursorAffordance : MonoBehaviour {

    [SerializeField] Texture2D walkCursor = null;
    [SerializeField] Texture2D unknownCursor = null;
    [SerializeField] Texture2D targetCursor = null;
    [SerializeField] Texture2D buttonCursor = null;

    [SerializeField] Vector2 cursorHotspot = new Vector2(0, 0);

    [SerializeField] const int walkableLayerNumber = 8;
    [SerializeField] const int enemyLayerNumber = 9;
    [SerializeField] const int uiLayerNumber = 5;

    CameraRaycaster cameraRaycaster;

    // Use this for initialization
    void Start()
    {
        cameraRaycaster = GetComponent<CameraRaycaster>();
        cameraRaycaster.notifyLayerChangeObservers += OnLayerChanged; // registering
    }

    void OnLayerChanged(int newLayer)
    {
        switch (newLayer)
        {
            case uiLayerNumber: // TODO make cameraRaycaster member variables
                Cursor.SetCursor(buttonCursor, cursorHotspot, CursorMode.Auto);
                break;
            case walkableLayerNumber:
                Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
                break;
            case enemyLayerNumber:
                Cursor.SetCursor(targetCursor, cursorHotspot, CursorMode.Auto);
                break;
            default:
                Cursor.SetCursor(unknownCursor, cursorHotspot, CursorMode.Auto);
                return;
        }
    }

    // TODO consider de-registering OnLayerChanged on leaving all game scenes

    #region Old Code for Reference
    //   [SerializeField] Texture2D walkCursor = null;
    //   [SerializeField] Texture2D attackCursor = null;
    //   [SerializeField] Texture2D unknownCursor = null;
    //   [SerializeField] Vector2 cursorHotspot = new Vector2(0, 0);

    //   CameraRaycaster cameraRaycaster;
    //// Use this for initialization
    //void Start () {
    //       cameraRaycaster = GetComponent<CameraRaycaster>();
    //       cameraRaycaster.LayerChangeObservers += OnLayerChange;
    //}

    //   void OnLayerChange(Layer currentLayer)
    //   {
    //       Texture2D renderCursor;

    //       switch (currentLayer)
    //       {
    //           case Layer.Walkable:
    //               renderCursor = walkCursor;
    //               break;
    //           case Layer.Enemy:
    //               renderCursor = attackCursor;
    //               break;
    //           case Layer.RaycastEndStop:
    //               renderCursor = unknownCursor;
    //               break;
    //           default:
    //               Debug.LogError("Cursor Error");
    //               return;

    //       }
    //       Cursor.SetCursor(renderCursor, cursorHotspot, CursorMode.Auto);
    //   }

    //// Update is called once per frame
    //void LateUpdate () {
    //       #region Notes, delete soon
    //       //if (Input.GetMouseButtonDown(0))
    //       //{
    //       //    print(cameraRaycaster.hit.collider.gameObject.tag.ToString());
    //       //}
    //       //print(cameraRaycaster.layerHit); 
    //       #endregion

    //   } 
    #endregion
}
