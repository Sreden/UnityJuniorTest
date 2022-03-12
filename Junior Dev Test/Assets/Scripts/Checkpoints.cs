using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    public List<Transform> nextCheckpoints = new List<Transform>();

    /// <summary>
    /// returns random accessible checkpoint or null bc there's no next
    /// </summary>
    public Transform PickRandomNext()
    {
        return nextCheckpoints.Count == 0 ? null : nextCheckpoints[Random.Range(0, nextCheckpoints.Count)];
    }
}
