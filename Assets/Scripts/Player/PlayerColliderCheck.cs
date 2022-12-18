using System;
using UnityEngine;

namespace Player
{
    public class PlayerColliderCheck : MonoBehaviour
    {
        public GameObject player;
        public GameObject DeathFX;

        private void OnCollisionStay(Collision collisionInfo)
        {
            isGrounded = collisionInfo.contacts.Length > 0;
        }
        
        private void OnCollisionExit(Collision other)
        {
            isGrounded = false;
        }
        
        public bool isGrounded;

        //Detect collisions between the GameObjects with Colliders attached
        void OnCollisionEnter(Collision collision)
        {
            //Check for a match with the specific tag on any GameObject that collides with your GameObject
            if (collision.gameObject.tag == "Obstacle")
            {
                //If the GameObject has the same tag as specified, output this message in the console
                Debug.Log("Die");
                player.GetComponent<SimplePlayerController>().distanceTravelled = 0;
                UnityEngine.Object.Instantiate<GameObject>(this.DeathFX, base.transform.position, base.transform.rotation);
            }
        }
    }
}