﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {
    // [SerializeField] public Player_Light fieldofview;
    private Rigidbody2D rb;
    private Vector2 Movement;

    public float MoveSpeed;

    [Space()]
    [Header("Stamina")]

    public float maxStamina;
    public float stamina;
    public float staminaConsumption;
    public float staminaRegeneration;

    public Transform pointLight;

    void Start() {
        MoveSpeed = 3;

        maxStamina = 100;
        stamina = 100;
        staminaConsumption = 2;
        staminaRegeneration = 0.4f;

        rb = GetComponent<Rigidbody2D>();
    }


    void Update() {
        // input
        Movement.x = Input.GetAxisRaw("Horizontal");
        Movement.y = Input.GetAxisRaw("Vertical");
        Movement = Movement.normalized;

        if (Input.GetKey(KeyCode.LeftBracket)) {
            pointLight.Rotate(new Vector3(0, 0, 1.5f));
        }
        if (Input.GetKey(KeyCode.RightBracket)) {
            pointLight.Rotate(new Vector3(0, 0, -1.5f));
        }
    }

    void FixedUpdate() {
        // regen stamina
        float staminaSpeed = 1f;
        stamina = Mathf.Min(maxStamina, stamina + staminaRegeneration);
        if (Input.GetKey(KeyCode.LeftShift) && stamina >= staminaConsumption) {
            stamina -= staminaConsumption;
            staminaSpeed = 2;
        }

        // movement
        rb.MovePosition(rb.position + Movement * MoveSpeed * staminaSpeed * Time.fixedDeltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            Destroy(gameObject);
        }
    }
}
