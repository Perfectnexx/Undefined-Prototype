using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Script : MonoBehaviour {
    public float moveSpeed;

    public float degrees;
    public float facingDegree;
    public float distance;

    public bool detected;
    private int layerMask;

    private GameObject player;
    private Transform target;

    

    void Start() {
        moveSpeed = 3;

        degrees = 90;
        facingDegree = 0;
        distance = 3f;

        detected = false;
        layerMask = (LayerMask.GetMask("Player", "Wall"));

        player = GameObject.Find("Player_Prototype");
        target = player.GetComponent<Transform>();
    }

    void Update() {
        // raycast!
        // shoot ray to detect walls or the player, whichever it hits first
        Vector3 enemyPos = transform.position;
        Vector3 playerPos = target.position;
        Vector3 enemyToPlayer = playerPos - enemyPos;

        Debug.Log(Quaternion.FromToRotation(Vector3.right, enemyPos - playerPos).eulerAngles.z);

        Debug.DrawRay(enemyPos, enemyToPlayer.normalized * distance, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(enemyPos, enemyToPlayer, distance, layerMask);
        
        if (hit.collider != null) {
            // Debug.Log(hit.collider.gameObject.name);
            detected = true;
        } else {
            detected = false;
        }

        // move towards the player pos
        float tilesize = 20f;
        if (Vector3.Distance(target.position, transform.position) <= 5 * tilesize) {
            
            //transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime) ;
        }
    }
}
