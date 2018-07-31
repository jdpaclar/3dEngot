using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField] GameObject gameCanvasPrefab = null;
    [SerializeField] GameObject eventSystemPrefab = null;

    GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Instantiate(gameCanvasPrefab);
        Instantiate(eventSystemPrefab);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position;
    }

    #region Old Code For Reference
    //const string TAG_PLAYER = "Player";
    //GameObject _player;

    //// Use this for initialization
    //void Start()
    //{
    //    _player = GameObject.FindGameObjectWithTag(TAG_PLAYER);
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    transform.position = _player.transform.position;
    //}
    #endregion
}
