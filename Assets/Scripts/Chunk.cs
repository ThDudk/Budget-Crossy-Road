using System;
using System.Linq;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    private Chunk[] GetSubchunks() {
        try {
            return GetComponentsInChildren<Chunk>();
        } catch {
            return Array.Empty<Chunk>();
        }
    }
    
    public int NumLanes() {
        var subchunks = GetSubchunks();
        var count = subchunks.Sum(chunk => chunk.NumLanes());

        count += transform.childCount - subchunks.Length;
        return count;
    }
}
