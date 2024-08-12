using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mk.Tcg.Gameplay {

    public class TurnController : MonoBehaviour {

        [SerializeField] private int totalTurns;
        [SerializeField] private TurnState currentState;
        [SerializeField] private Player activePlayer;
        [SerializeField] private GameObject humanPlayerPrefab;
        private Queue<Player> players = new();


        public event Action <TurnState> OnStateChanged;
        public event Action <Player> OnActivePlayerChanged;
        public event Action <Player> OnDrawEnter;
        public event Action <Player> OnMainEnter;
        public event Action <Player> OnBattleEnter;
        public event Action <Player> OnEndEnter;

        private void Start() {

            RegisterPlayer(CreatePlayer(1, "Maurice"));
            RegisterPlayer(CreatePlayer(2, "Demon"));
            activePlayer = players.Peek();

            SetState(TurnState.Draw);
        }

        private Player CreatePlayer (int id, string name) {

            Player player = Instantiate(humanPlayerPrefab).GetComponent<Player>();
            player.Init(id, name);
            player.gameObject.name = $"Player: {name}";
            return player;
        }

        public void RegisterPlayer (Player player) {

            players.Enqueue(player);
        }

        private void NextPlayer () {

            if (players.Count == 0) return;
            Player player = players.Dequeue();
            players.Enqueue(player);
            activePlayer = players.Peek();
            OnActivePlayerChanged?.Invoke(activePlayer);
        }

        public void SetState (TurnState state) {

            switch (state) {

                case TurnState.None:
                    break;

                case TurnState.Draw:
                    OnDrawEnter?.Invoke(activePlayer);
                    break;

                case TurnState.Main:
                    OnMainEnter?.Invoke(activePlayer);
                    break;
            }
        }

    }
}
