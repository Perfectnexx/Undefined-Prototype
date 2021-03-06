using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Script : MonoBehaviour {
    private Rigidbody2D rb;
    private Vector2 movement;
    public float moveSpeed;
    public Transform target;
    // public bool overlapping;

    public float degrees;
    public int rays;
    public float facingDegree;


    public bool detected = false;

    private GameObject player;

    void Start() {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        rb = gameObject.GetComponent<Rigidbody2D>();
        moveSpeed = 3;

        degrees = 1;
        rays = 1;
        facingDegree = 0;

        player = GameObject.Find("Player_Prototype");
        Debug.Log(player.name);
    }

    void Update() {
        // ah yes, broken code
        //movement = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        //rb.MovePosition(rb.position + movement);

        // raycast!
        // shoot rays to detect walls or the player, whichever it hits first
        Vector3 playerPos = player.transform.position;
        Vector2 enemyToPlayer = playerPos - transform.position;

        Debug.DrawRay(transform.position, enemyToPlayer, Color.red);


        float direction = facingDegree;
        float raySpacing = degrees / rays;
        for (int i = 0; i < rays; i++) {

            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.right);
            

            if (hit.collider != null) {
                detected = true;
            } else {
                detected = false;
            }

            direction += raySpacing;
        }

        // move towards the player pos
        float tilesize = 20f;
        if (Vector3.Distance(target.position, transform.position) <= 5 * tilesize) {
            
            //transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime) ;
        }
    }
}
