using UnityEngine;

public class CameraFollow : MonoBehaviour {

    const string TAG_PLAYER = "Player";
    GameObject _player;

	// Use this for initialization
	void Start () {
        _player = GameObject.FindGameObjectWithTag(TAG_PLAYER);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = _player.transform.position;
	}
}
