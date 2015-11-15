using UnityEngine;
using System.Collections;

public class MonsterController : MonoBehaviour {
    [SerializeField]
    private float distanceToWalk;
    [SerializeField]
    private float howlingDistance;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private AudioSource barkingAudio;
    [SerializeField]
    private AudioSource howlingAudio;

    private float distanceWalked;
    private GameObject player;
    private Rigidbody rigidBody;

    // Use this for initialization
    public void Start () {
        this.rigidBody = GetComponent<Rigidbody>();
        this.player = GameObject.FindGameObjectWithTag("Player");

        this.howlingDistance = (this.howlingDistance <= 0.0f) ? 100.0f : this.howlingDistance;
        this.movementSpeed = (this.movementSpeed <= 0.0f) ? 10.0f : this.movementSpeed;
        this.runSpeed = (this.runSpeed <= 0.0f) ? 10.0f : this.runSpeed;
        this.distanceToWalk = (this.distanceToWalk <= 0.0f) ? 5.0f : this.distanceToWalk;
    }

    // Update is called once per frame
    public void Update () {
        if (this.distanceWalked < this.distanceToWalk) {
            walk();
        } else {
            turn();
        }

        if (this.distanceToPlayer() < this.howlingDistance) {
            this.barkingAudio.mute = false;
            this.howlingAudio.mute = true;
        } else {
            this.barkingAudio.mute = true;
            this.howlingAudio.mute = false;
        }
    }

    private void walk() {
        rigidBody.MovePosition(this.transform.position + this.transform.forward * this.movementSpeed * Time.fixedDeltaTime);
        this.distanceWalked += movementSpeed * Time.fixedDeltaTime;
    }

    private void turn() {
        Vector3 rotateY = new Vector3(0.0f, Random.Range(-90, 90), 0.0f);
        this.transform.Rotate(rotateY);
        this.distanceWalked = 0;
    }

    private float distanceToPlayer() {
        return (this.transform.position - this.player.transform.position).sqrMagnitude;
    }
}