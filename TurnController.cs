using Mk.Tcg.Core;
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

        public void SetState (TurnState state) {

            switch (state) {

                case TurnState.None:
                    break;

                case TurnState.Draw:
                    currentState = TurnState.Draw;
                    OnDrawEnter?.Invoke(activePlayer);
                    break;

                case TurnState.Main:
                    OnMainEnter?.Invoke(activePlayer);
                    break;
            }
        }

        public Player CreatePlayer (int id, string name, List<CardData> deck, int startingResources) {

            Player player = Instantiate(humanPlayerPrefab).GetComponent<Player>();

            player.Init(id, name, deck, startingResources);
            player.SetObjectName($"Player: {name}");

            return player;
        }

        public void RegisterPlayer (Player player) {

            players.Enqueue(player);
            activePlayer = players.Peek();
        }

        private void NextPlayer () {

            if (players.Count == 0) return;
            Player player = players.Dequeue();
            players.Enqueue(player);
            activePlayer = players.Peek();
            OnActivePlayerChanged?.Invoke(activePlayer);
        }

    }
}
