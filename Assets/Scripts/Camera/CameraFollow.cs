using System;
using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    private static CameraFollow instance;
    private GameObject player;
    private Transform target;
    public float smoothing = 5f;
    Vector3 offset;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
    }

    private void Start()
    {
        //Mendapatkan offset antara target dan camera
        offset = transform.position - target.position;
    }
    
    private void FixedUpdate()
    {
        //Menapatkan posisi untuk camera
        Vector3 targetCamPos = target.position + offset;

        //set posisi camera dengan smoothing
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}