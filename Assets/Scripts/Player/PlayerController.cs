using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        #region Setters

        public Transform parentTransform;

        public Transform mainPlayerPosition;

        public PlayerFlags playerFlags;

        public PlayerLevelOptions playerLevelOptions;

        public PlayerColliderCheck playerColliderCheck;

        public PathFollower pathFollower;

        public Transform cameraFocus;

        #endregion

        private bool _mouseDown;

        void Update()
        {
            _mouseDown = Input.GetMouseButton(0);
        }

        private void FixedUpdate()
        {
            CheckAndJump();

            //SetPhysics();
        }

        private bool _jumpTicket = true;

        private void CheckAndJump()
        {
            if (_mouseDown && playerColliderCheck.isGrounded && _jumpTicket)
            {
                _jumpTicket = false;

                playerFlags.playerPhysicsBody.AddForce(playerFlags.playerTransform.up *
                                                       playerLevelOptions.jumpForce, ForceMode.Impulse);

                playerFlags.playerPhysicsBody.AddTorque(playerFlags.playerTransform.right,
                    ForceMode.Impulse);
            }
            else if (playerColliderCheck.isGrounded)
                _jumpTicket = true;
        }

        private void SetPhysics()
        {
            // Make the player go down faster.
            playerFlags.playerPhysicsBody.AddForce(
                Vector3.down * (playerLevelOptions.jumpForce * 3f),
                ForceMode.Acceleration);

            var movePosition = pathFollower.GetCurrentTargetPosition(playerFlags.playerPhysicsBody.position);

            // Lock player rotation.
            var localEulerAngles = mainPlayerPosition.localEulerAngles;
            // mainPlayerPosition.localEulerAngles = new Vector3(localEulerAngles.x, 0, 0);

            // Make the player's forward face the waypoint.
            playerFlags.playerTransform.LookAt(movePosition);

            if (pathFollower.lastWaypointReached)
            {
                // playerFlags.playerPhysicsBody.constraints =
                //     RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;

                return;
            }

            playerFlags.playerTransform.position =
                Vector3.MoveTowards(playerFlags.playerTransform.position,
                    movePosition,
                    Time.deltaTime * playerLevelOptions.currentSpeed);

            cameraFocus.transform.position = playerFlags.playerTransform.position;
        }
    }
}