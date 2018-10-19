using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class HorizontalSpawner : MonoBehaviour {

    private GameObject CircularEnemy;
    private GameObject Player;

    public float minimumRespawnTime = 3f;
    public float maximumRespawnTime = 6f;

    public float rotationFixer = 190f;

    public float offsetToPlayer = 0.22f;

#if UNITY_EDITOR
    // Draw Gizmos when gameobject is selected (Should represent the way the object moves around).
    public void OnDrawGizmosSelected() {
        Vector3 rotate = this.gameObject.transform.rotation.eulerAngles;
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(this.gameObject.transform.position, Quaternion.Euler(new Vector3(rotate.x, rotate.y, rotate.z - rotationFixer)), this.gameObject.transform.lossyScale);
        Gizmos.matrix = rotationMatrix;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.gameObject.transform.position, Vector3.back);
    }
#endif

    public void Start() {
        Player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(SpawnEnemy());
    }

    public void FixedUpdate() {
        // Update position of spawner in relation with player position on Y-axis
        //this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, Player.transform.position.y + offsetToPlayer, this.gameObject.transform.position.z);
    }

    // Spawn circulare enemy on a random range of time
    IEnumerator SpawnEnemy() {
        while(true) { 
            // Spawn enemy
            GameObject enemy = Instantiate(Resources.Load("horizontalEnemy")) as GameObject;
            enemy.transform.parent = GameObject.Find("Ennemies").transform;
            enemy.transform.position = this.gameObject.transform.position;
            enemy.transform.rotation = this.gameObject.transform.rotation;

        float randomSpawnTimer = Random.Range(minimumRespawnTime, maximumRespawnTime);
        yield return new WaitForSeconds(randomSpawnTimer);
        }
    }

}
