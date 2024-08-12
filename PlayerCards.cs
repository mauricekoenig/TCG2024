


using Mk.Tcg.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Mk.Tcg.Gameplay {

    public class PlayerCards : MonoBehaviour {

        [SerializeField] private List<CardData> deck = new();
        [SerializeField] private List<CardData> hand = new();
        [SerializeField] private List<CardData> board = new();
        [SerializeField] private List<CardData> graveyard = new();
        [SerializeField] private List<CardData> removed = new();

        public void Init (List<CardData> deck) {

            this.deck = deck;
        }

        public void DrawCard () {

            if (deck.Count <= 0) return;
            CardData data = deck[deck.Count - 1];
            deck.RemoveAt(deck.Count - 1);
            hand.Add(data);
        }
    }
}
