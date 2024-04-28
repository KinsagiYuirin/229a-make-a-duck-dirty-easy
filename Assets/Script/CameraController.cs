using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public bool isPlayerGrounded = false;

    [SerializeField] private float cameraUpGrounded;
    
    public static CameraController instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }
    
    void FollowPlayer()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

        //Need more smooth move camera
        /*
        if (isPlayerGrounded) 
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y + cameraUpGrounded * Time.timeScale, transform.position.z);
        */
    }
}
