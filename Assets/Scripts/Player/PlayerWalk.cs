using System;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// Smoothly move the player to a waypoint.
    /// </summary>
    public class PlayerWalk : MonoBehaviour
    {
        public Rigidbody mainPlayerRigidbody;

        public PathFollower pathFollower;

        public Transform follower;

        private PlayerJump _playerJump;

        public float playerSpeed;

        private Vector3 _targetWaypoint;

        private void FixedUpdate()
        {
            _targetWaypoint = pathFollower
                .GetCurrentTargetPosition(transform.position);

            MoveToWaypoint();

            SmoothLookAtWaypoint();

            // // OLD WAY
            // // Get position to move.
            // Vector3 positionStep = new Vector3(
            //     position.x > nearestWaypoint.x ? -playerSpeed : playerSpeed,
            //     0,
            //     position.z > nearestWaypoint.z ? -playerSpeed : playerSpeed
            // );
            //
            // mainPlayerRigidbody.MovePosition(position + positionStep * (Time.deltaTime));

            // Look at the object.
        }

        private void OnCollisionEnter(Collision collision)
        {
            transform.LookAt(_targetWaypoint);
        }

        void SmoothLookAtWaypoint()
        {
            var rotation = transform.rotation;
            var targetRotation = Quaternion.LookRotation(_targetWaypoint - transform.position);
            var position = transform.position;
            //
            // var targetLook = _targetWaypoint;
            //
            // Vector3 relativePos = targetLook - position;
            //
            // Quaternion rotation = Quaternion.LookRotation(relativePos, transform.up);
            //
            // transform.rotation = rotation;

            // // Doing this makes the object stop rotating...wtf.

            // transform.LookAt(_targetWaypoint);

            // Pathetic attempt to rotate smoothly.
            // Only rotate Y.
            // targetRotation.w = rotation.w;
            // targetRotation.x = rotation.x;
            // targetRotation.z = rotation.z;
            //
            // rotation = Quaternion.Slerp(rotation, targetRotation, (playerSpeed * 10f) * Time.deltaTime);
            // transform.rotation = rotation;
        }

        void MoveToWaypoint()
        {
            // var position = mainPlayerRigidbody.transform.position;
            var position = transform.position;

            // Retain our Y position.
            _targetWaypoint.y = position.y;

            // Move towards object.
            position = Vector3.MoveTowards(position, _targetWaypoint, playerSpeed);

            transform.position = position;
            follower.position = position;
        }
    }
}