using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerLevelOptions : MonoBehaviour
    {
        [FormerlySerializedAs("startingSpeed")] public float currentSpeed = 5.0f;

        public float jumpForce = 6f;
    }
}