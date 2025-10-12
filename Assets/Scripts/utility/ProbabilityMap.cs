using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;
using Random = UnityEngine.Random;

namespace utility {
    [Serializable]
    public class ProbabilityMap<T> : SerializedDictionary<T, float>
    {
        /// <summary>
        /// NOTE: Will return null if no item is below the random value. 
        /// </summary>
        /// <returns>a random item from the probability map or default (null for most objects) if the random value falls below any item (or this is empty)</returns>
        [CanBeNull]
        public T GetRandom() {
            switch (Count) {
                case 0: return default;
                case 1: return Keys.First();
            }

            var value = Random.Range(0f, 1f);
            
            var chosenTiles = new List<T>();
            
            foreach(var pair in this)
            {
                if (pair.Value > value) continue;

                if (chosenTiles.Count > 0 && pair.Value < this[chosenTiles.First()]) continue;
                
                if (chosenTiles.Count > 0 && pair.Value > this[chosenTiles.First()]) {
                    chosenTiles.Clear();
                }
                
                chosenTiles.Add(pair.Key);
            }

            if (chosenTiles.Count == 0) return default;
            return chosenTiles[Random.Range(0, chosenTiles.Count)];
        }
    }
}
