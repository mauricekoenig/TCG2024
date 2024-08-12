

using System;
using UnityEngine;

namespace Mk.Tcg.Gameplay {

    public class PlayerResources : MonoBehaviour {

        [SerializeField] private readonly int maxResources = 10;
        [SerializeField] private int currentResources;

        public void SetCurrentResources(int current) {
            if (current > maxResources) return;
            currentResources = current;
        }

        public void Increase () {
            if (currentResources >= maxResources) return;
            currentResources++;
        }
         
        public void Decrease () {
            if (currentResources == 0) return;
            currentResources--;
        }

        public event Action<int, int> OnResourcesChanged;

    }
}
