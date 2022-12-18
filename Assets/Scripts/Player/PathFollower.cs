using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PathFollower : MonoBehaviour
    {
        /// <summary>
        /// Gets all the waypoints positions (x and z) in the scene.
        /// </summary>
        private Vector3[] _targetWaypoints;

        private void Awake()
        {
            _targetWaypoints = GameObject.FindGameObjectsWithTag("Waypoint")
                .Select(x => x.transform.position)
                .ToArray();
        }

        public int areaPadding = 5;

        private int _currentWaypointIndex = 0;

        public bool lastWaypointReached;

        /// <summary>
        /// Gets the position the player will be moved to, it will be the position to be added on the player's
        /// transform.position.
        /// </summary>
        public Vector3 GetCurrentTargetPosition(Vector3 currentPlayerPosition)
        {
            if (!(_currentWaypointIndex < _targetWaypoints.Length))
            {
                lastWaypointReached = true;
                
                return _targetWaypoints.Last();
            }
            var currentWaypoint = _targetWaypoints[_currentWaypointIndex];


            var maxX = currentWaypoint.x + areaPadding;
            var minX = currentWaypoint.x - areaPadding;
            var maxZ = currentWaypoint.z + areaPadding;
            var minZ = currentWaypoint.z - areaPadding;

            var playerInsideMinX = currentPlayerPosition.x > minX;
            var playerInsideMaxX = currentPlayerPosition.x < maxX;
            var playerInsideMinZ = currentPlayerPosition.z > minZ;
            var playerInsideMaxZ = currentPlayerPosition.z < maxZ;

            // Check if already in the position.
            if (playerInsideMinX && playerInsideMaxX &&
                playerInsideMinZ && playerInsideMaxZ)
            {
                // Move to the next waypoint.
                _currentWaypointIndex++;
                
                Debug.Log($"Waypoint {_targetWaypoints.ToList().IndexOf(currentWaypoint)} reached! " +
                          $"Moving to waypoint at {(_currentWaypointIndex < _targetWaypoints.Length ? _targetWaypoints[_currentWaypointIndex] : "done.")}");
            }

            currentWaypoint.y = currentPlayerPosition.y;

            return currentWaypoint;
        }
    }
}