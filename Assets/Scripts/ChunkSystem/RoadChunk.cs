using UnityEngine;
using utility;

namespace ChunkSystem {
    public class RoadChunk : Chunk
    {
        [SerializeField] private ProbabilityMap<int> lanes;
        [SerializeField] private GameObject roadTop;
        [SerializeField] private GameObject roadMiddle;
        [SerializeField] private GameObject roadBottom;
        [SerializeField] private GameObject roadSingle;
        private int numLanes;
    
        public override int NumLanes() {
            return numLanes;
        }
    
        private void Awake() {
            numLanes = lanes.GetRandom();

            if (numLanes == 1) {
                AddLane(roadSingle, 0);
                return;
            }
        
            AddLane(roadBottom, 0);
            for (var i = 1; i < numLanes - 1; i++) AddLane(roadMiddle, i);
            AddLane(roadTop, numLanes - 1);
        }

        private void AddLane(GameObject lane, int offset) {
            var laneObj = Instantiate(lane, transform);
            laneObj.transform.localPosition = Vector3.up * offset;
        }
    }
}
