using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player
{
    public class PlayerController : MonoBehaviour {

        public MouseCamera mouseCamera = new MouseCamera ();
        public float impulse_rate = 30f;
      
        private Vector3 impulse_local;
        private Rigidbody playerRigidbody;


        void Start()
        {
            playerRigidbody = GetComponent<Rigidbody>();
            mouseCamera.Init(transform);
        }

        void FixedUpdate()
        {
            float h = Input.GetAxis("Horizontal");   // moving left/right, keys "a"/"d"
            float v = Input.GetAxis("Vertical");    // moving forward/backward, keys "w"/"s"
            float u = Input.GetAxis("Up");          // moving up/down, keys "q"/"e"
            Move(h, u, v);

        }

        void Move(float h, float u, float v)
        {
            impulse_local.Set(h, 0f, v);
            impulse_local += transform.InverseTransformVector(new Vector3(0f, u, 0f));    // up/down direction should be independent of local frame's orientation
            impulse_local = impulse_local.normalized * impulse_rate * Time.deltaTime;

            // transformation from local to global coordinate frame due to keeping correct moving after rotation
            Vector3 impulse__global = transform.TransformVector(impulse_local);
            
            // applying mooving and rotation
            playerRigidbody.AddForce(impulse__global, ForceMode.Impulse);
            mouseCamera.LookRotation(transform);
        }
    }

}