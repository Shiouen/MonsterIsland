using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
    [SerializeField]
    private bool cursorLocked;
    [SerializeField]
    private float pickupHeight;
    [SerializeField]
    private GameObject borderPrefab;
    [SerializeField]
    private GameObject monsterPrefab;
    [SerializeField]
    private GameObject pickupPrefab;
    [SerializeField]
    private int monsterAmount;
    [SerializeField]
    private int pickupAmount;

    private GameObject[] monsters;
    private GameObject[] pickups;
    private Terrain terrain;
    private Vector3 terrainPosition;
    private Vector3 terrainSize;

    // Use this for initialization
    public void Start () {
        // lock cursor
        Cursor.lockState = (this.cursorLocked) ? CursorLockMode.Locked : Cursor.lockState;
        Cursor.visible = !this.cursorLocked;

        // get terrain properties
        this.terrain = Terrain.activeTerrain;
        this.terrainPosition = new Vector3(this.terrain.GetPosition().x, this.terrain.GetPosition().y, this.terrain.GetPosition().z);
        this.terrainSize = new Vector3(this.terrain.terrainData.size.x, this.terrain.terrainData.size.y, this.terrain.terrainData.size.z);

        this.pickupAmount = (this.pickupAmount <= 0) ? 10 : this.pickupAmount;
        this.monsterAmount = (this.monsterAmount <= 0) ? 10 : this.monsterAmount;

        this.SpawnBorders();
        this.SpawnMonsters();
        this.SpawnPickups();
    }
	
	// Update is called once per frame
	public void Update () {
        Vector3 randomRotation = new Vector3(0, 1, 0);

        foreach (GameObject item in this.pickups) {
            item.transform.Rotate(randomRotation);
        }
    }

    private void SpawnMonsters() {
        // in case no prefab assigned
        if (this.monsterPrefab == null) { return; }

        float randomX;
        float randomZ;
        float y;

        this.pickups = new GameObject[this.monsterAmount];

        for (int i = 0; i < this.monsterAmount; i++) {
            randomX = Random.Range(this.terrainPosition.x + 80.0f, this.terrainSize.x - 80.0f);
            randomZ = Random.Range(this.terrainPosition.z + 120.0f, this.terrainSize.z - 120.0f);

            y = Terrain.activeTerrain.SampleHeight(new Vector3(randomX, 0, randomZ));

            this.transform.position = new Vector3(randomX, y, randomZ);

            GameObject monster = (GameObject) Instantiate(this.monsterPrefab, this.transform.position, Quaternion.identity);
            monster.transform.localScale += new Vector3(-2.0F, 0.0f, 0.0f);
            this.pickups[i] = monster;
        }
    }

    private void SpawnPickups() {
        // in case no prefab assigned
        if (this.pickupPrefab == null) { return; }

        float randomX;
        float randomZ;
        float y;

        this.pickups = new GameObject[ this.monsterAmount ];

        for (int i = 0; i < this.pickupAmount; i++) {
            randomX = Random.Range(this.terrainPosition.x + 80.0f, this.terrainSize.x - 80.0f);
            randomZ = Random.Range(this.terrainPosition.z + 120.0f, this.terrainSize.z - 120.0f);

            y = Terrain.activeTerrain.SampleHeight(new Vector3(randomX, 0, randomZ));
            y += this.pickupHeight;

            this.transform.position = new Vector3(randomX, y, randomZ);

            GameObject pickup = (GameObject) Instantiate(this.pickupPrefab, this.transform.position, Quaternion.identity);
            pickup.transform.localScale += new Vector3(-2.0F, 0.0f, 0.0f);
            this.pickups[ i ] = pickup;
        }
    }

    private void SpawnBorders() {
        GameObject border1 = (GameObject) Instantiate(this.borderPrefab, new Vector3(250, 100, 0), Quaternion.identity);
        GameObject border2 = (GameObject) Instantiate(this.borderPrefab, new Vector3(250, 100, 500), Quaternion.identity);
        border2.transform.Rotate(new Vector3(0, 180, 0));
        GameObject border3 = (GameObject) Instantiate(this.borderPrefab, new Vector3(500, 100, 250), Quaternion.identity);
        border3.transform.Rotate(new Vector3(0, -90, 0));
        GameObject border4 = (GameObject) Instantiate(this.borderPrefab, new Vector3(0, 100, 250), Quaternion.identity);
        border4.transform.Rotate(new Vector3(0, 90, 0));
    }
}
