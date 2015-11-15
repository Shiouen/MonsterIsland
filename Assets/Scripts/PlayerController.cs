using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    private float mouseSensitivity;
    [SerializeField]
    private float movementSpeed;

    CharacterController characterController;

	// Use this for initialization
	void Start () {
        this.characterController = GetComponent<CharacterController>();

        this.mouseSensitivity = 5f;
        this.movementSpeed = 10f;
	}
	
	// Update is called once per frame
	void Update () {
        // Rotation
        // horizontal view change
        float rotateY = Input.GetAxis("Mouse X");
        this.transform.Rotate(0, rotateY, 0);
        // vertical view change
        float rotateX = Input.GetAxis("Mouse Y");
        Camera.main.transform.Rotate(-rotateX, 0, 0);


        // Movement
        // z - move front/backwards
        float z = Input.GetAxis("Vertical") * movementSpeed;
        // x - move left/right
        float x = Input.GetAxis("Horizontal") * movementSpeed * 0.80f;

        // need to multiply with rotation, otherwise we keep going 1 way (even if we rotate)
        Vector3 speed = this.transform.rotation * new Vector3(x, 0f, z);
        
        this.characterController.SimpleMove(speed);
	}
}
