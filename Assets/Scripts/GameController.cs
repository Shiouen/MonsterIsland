using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    [SerializeField]
    private bool cursorLocked;

	// Use this for initialization
	void Start () {
        // lock cursor
        Cursor.lockState = (this.cursorLocked) ? CursorLockMode.Locked : Cursor.lockState;
        Cursor.visible = !this.cursorLocked;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
