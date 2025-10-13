using System;
using UnityEngine;
using utility;

public class RoadChunk : Chunk
{
    [SerializeField] private ProbabilityMap<int> lanes;
    [SerializeField] private GameObject roadTop;
    [SerializeField] private GameObject roadMiddle;
    [SerializeField] private GameObject roadBottom;
    [SerializeField] private GameObject roadSingle;

    private void Start() {
        var numLanes = lanes.GetRandom();

        if (numLanes == 0) {
            AddLane(roadSingle);
            return;
        }
        
        for (var i = 0; i < numLanes; i++) {
            if(i == 0) {AddLane(roadBottom); continue; }
            if(i == numLanes - 1) {AddLane(roadTop); continue; }

            AddLane(roadMiddle);
        }
    }

    private void AddLane(GameObject lane) {
        var numLanes = NumLanes();
        
        var laneObj = Instantiate(lane, transform);
        laneObj.transform.position = transform.position + Vector3.up * numLanes;
    }
}
