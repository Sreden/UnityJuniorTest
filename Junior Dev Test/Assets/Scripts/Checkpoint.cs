using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public List<Checkpoint> nextCheckpoints = new List<Checkpoint>();

    /// <summary>
    /// returns random accessible checkpoint or null when there's no next
    /// </summary>
    public Checkpoint PickRandomNext()
    {
        return nextCheckpoints.Count == 0 ? null : nextCheckpoints[Random.Range(0, nextCheckpoints.Count)];
    }

    private void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = new Color(1f,0f,0.12f);
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}
