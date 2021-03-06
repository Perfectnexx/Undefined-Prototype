﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_View : MonoBehaviour {
    [SerializeField] private LayerMask layerMask;
    private Mesh mesh;
    //private float fov;
    private Vector3 origin;
    //private float startingangle;

    public float fov;
    public int RayCount;
    public float angle;
    public float viewdistance;
    // public float x;

    public bool verbose = true;

    void Start() {
        origin = Vector3.zero;
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        layerMask = LayerMask.GetMask("Wall");

        Debug.Log(layerMask.value);

        fov = 20f;
        RayCount = 10;
        angle = 0f;

        viewdistance = 5f;
    }


    void Update() {
        if (Input.GetKey(KeyCode.LeftBracket)) {
            angle += 1.5f;
        }
        if (Input.GetKey(KeyCode.RightBracket)) {
            angle -= 1.5f;
        }

        float rayAngle = angle;
        float angleIncrease = fov / RayCount;

        Vector3[] vertices = new Vector3[RayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[RayCount * 3];

        vertices[0] = origin;


        int VertexIndex = 1;
        int TrianglesIndex = 0;
        for (int i = 0; i <= RayCount; i++) {
            Vector3 vertex;
            RaycastHit2D raycasthit2D = Physics2D.Raycast(origin, GetVectorFromAngle(rayAngle), viewdistance, layerMask);
            if (raycasthit2D.collider != null) {
                Debug.Log(raycasthit2D.collider.gameObject.name);
                vertex = raycasthit2D.point;
            } else {
                vertex = origin + GetVectorFromAngle(rayAngle) * viewdistance;
            }

            vertices[VertexIndex] = vertex;

            if (i > 0) {
                triangles[TrianglesIndex + 0] = 0;
                triangles[TrianglesIndex + 1] = VertexIndex - 1;
                triangles[TrianglesIndex + 2] = VertexIndex;

                TrianglesIndex += 3;
            }

            VertexIndex++;
            rayAngle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    public static Vector3 GetVectorFromAngle(float angle) {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    public static float GetAngleFromVectorFloat(Vector3 dir) {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }

    // private void SetOrgin(Vector3 origin) {
    //     this.origin = origin;    
    // }

    // private void SetAimDirection(Vector3 AimDirection) {
    //     startingangle = GetAngleFromVectorFloat(AimDirection) - fov / 2f; 
    // }
}