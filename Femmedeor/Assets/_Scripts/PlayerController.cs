using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moving_speed = 50f;
    public float rotation_speed = 5f;

    private Vector3 movement_local;
    private Quaternion rotation;
    private Rigidbody playerRigidbody;


    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");   // moving left/right, keys "a"/"d"
        float v = Input.GetAxis("Vertical");    // moving forward/backward, keys "w"/"s"
        float u = Input.GetAxis("Up");          // moving up/down, keys "left shift"/"left ctrl"
        float r = Input.GetAxis("Rotation");    // rotations to the right/left, keys "e"/"q"
        Move(h, u, v, r);
        
    }

    void Move(float h, float u, float v, float r)
    {
        movement_local.Set(h, u, v);
        movement_local = movement_local.normalized * moving_speed * Time.deltaTime;

        // transformation from local to global coordinate frame due to keeping correct moving after rotation
        Vector3 movement__global = transform.TransformVector(movement_local);    

        rotation = Quaternion.Euler(new Vector3(0.0f, r, 0.0f).normalized * rotation_speed * Time.deltaTime);

        // applying mooving and rotation
        playerRigidbody.MovePosition(transform.position + movement__global);
        playerRigidbody.MoveRotation(playerRigidbody.rotation * rotation);
    }
}
