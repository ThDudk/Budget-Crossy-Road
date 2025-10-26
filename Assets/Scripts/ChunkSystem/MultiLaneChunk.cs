using UnityEngine;
using UnityEngine.Serialization;
using utility;

namespace ChunkSystem {
    public class MultiLaneChunk : Chunk
    {
        [SerializeField] private ProbabilityMap<int> lanes;
        [FormerlySerializedAs("roadTop")] [SerializeField] private GameObject top;
        [FormerlySerializedAs("roadMiddle")] [SerializeField] private GameObject middle;
        [FormerlySerializedAs("roadBottom")] [SerializeField] private GameObject bottom;
        [FormerlySerializedAs("roadSingle")] [SerializeField] private GameObject single;
        private int numLanes;
    
        public override int NumLanes() {
            return numLanes;
        }
    
        private void Awake() {
            numLanes = lanes.GetRandom();

            if (numLanes == 1) {
                AddLane(single, 0);
                return;
            }
        
            AddLane(bottom, 0);
            for (var i = 1; i < numLanes - 1; i++) AddLane(middle, i);
            AddLane(top, numLanes - 1);
        }

        private void AddLane(GameObject lane, int offset) {
            var laneObj = Instantiate(lane, transform);
            laneObj.transform.localPosition = Vector3.up * offset;
        }
    }
}
