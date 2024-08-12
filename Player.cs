


using UnityEngine;

namespace Mk.Tcg.Gameplay {

    public abstract class Player : MonoBehaviour {

        [SerializeField] protected int id;
        [SerializeField] protected string playerName;

        public void Init (int id, string name) {

            this.id = id;
            playerName = name;
        }
    }
}
