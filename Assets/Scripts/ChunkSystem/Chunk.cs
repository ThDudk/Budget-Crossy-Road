using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    private List<Chunk> GetSubchunks() {
        List<Chunk> chunks = new();
        
        for(var i = 0; i < transform.childCount; ++i) {
            if (!transform.GetChild(i).TryGetComponent(out Chunk chunk)) continue;
            
            chunks.Add(chunk);
        }

        return chunks;
    }
    
    public int NumLanes() {
        var subchunks = GetSubchunks();
        var count = subchunks.Sum(chunk => chunk.NumLanes());

        count += transform.childCount - subchunks.Count;
        return count;
    }
}
