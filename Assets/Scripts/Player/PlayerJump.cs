using System;
using UnityEngine;
using UnityEngine.Serialization;

/// THIS IS SO FUCKED
/// next ill try using a hidden cube with rb for jump
/// and separate cube with rb only for rotate
namespace Player
{
    public class PlayerJump : MonoBehaviour
    {
        public Rigidbody mainPlayerRigidbody;

        public int jumpForce;

        private void Update()
        {
            mousePressed = Input.GetMouseButton(0);
        }

        private float _highestJump = 0;

        private void FixedUpdate()
        {
            CheckJump();
            PlayerPhysics();
        }

        void CheckJump()
        {
            if (isGrounded && jumpTicket && mousePressed)
            {
                jumpTicket = false;

                // Jump.
                mainPlayerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                Debug.Log("Jumped!");

                // Rotate too.
                mainPlayerRigidbody.AddTorque(Vector3.right * (jumpForce * 2), ForceMode.Impulse);
            }
        }

        void PlayerPhysics()
        {
            mainPlayerRigidbody.AddForce(Vector3.down * (jumpForce * 3f), ForceMode.Acceleration);
        }

        // Check if grounded.
        public bool mousePressed = false;
        public bool isGrounded = false;
        public bool jumpTicket = true;

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("On collision!");
            isGrounded = true;
        }

        private void OnCollisionExit(Collision other)
        {
            Debug.Log("Collision exited!");
            isGrounded = false;
            jumpTicket = true;
        }
    }
}