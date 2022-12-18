using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerFlags : MonoBehaviour
    {
        #region Player Physics Flags

        /// <summary>
        /// The transform to check for collisions.
        /// </summary>
        [FormerlySerializedAs("playerColliderTransform")]
        public Transform playerTransform;

        public Collider playerCollider;

        public Rigidbody playerPhysicsBody;
        
        #endregion

        private float _distanceToGround;

        private void Awake()
        {
            _distanceToGround = playerCollider.bounds.extents.y;
        }

        private void FixedUpdate()
        {
            
        }

        #region Auto Properties

        #endregion
    }
}