using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public float resetSpeend = 0.5f;
    public float cameraSpeed = 0.3f;

    public Bounds cameraBounds;

    private Transform target;

    private float offsetZ;
    private Vector3 lastTargetPosition;
    private Vector3 currentVelocity;

    private bool followsPlayer;

    private void Awake()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        collider.size = new Vector2(Camera.main.aspect * 2f * Camera.main.orthographicSize, 15f);
        cameraBounds = collider.bounds;
    }
    void Start()
    {
        target = GameObject.FindGameObjectWithTag(MyTags.PLAYER_TAG).transform;
        lastTargetPosition = target.position;
        offsetZ = (transform.position - target.position).z;
        followsPlayer = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (followsPlayer)
        {
            Vector3 aheadTargetPosition = target.position + Vector3.forward * offsetZ; 

            if(aheadTargetPosition.x >= transform.position.x)
            {
                Vector3 newCamerPosition = Vector3.SmoothDamp(transform.position, aheadTargetPosition, ref currentVelocity, cameraSpeed);

                transform.position = new Vector3(newCamerPosition.x, transform.position.y, newCamerPosition.z);

                lastTargetPosition = target.position;
            }
        }
    }
}
