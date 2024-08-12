using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Mk.Tcg.Core;

namespace Mk.Tcg.Gameplay {

    public class HelperTurnControllerScene : MonoBehaviour {

        public List<CardData> testDeserializedDeck;

        TurnController turnController;
        CardDataBase database;

        [TextArea(4, 10)]
        public string jsonDeck;

        private void Start() {
            turnController = FindFirstObjectByType<TurnController>();
            database = FindFirstObjectByType<CardDataBase>();
        }
        private void CreateDeck () {

            Deck deck = new Deck();
            deck.AddAllTest(database.GetAll(), 2);
            jsonDeck = database.SerializeDeck(deck);
            testDeserializedDeck = database.Deserialize(jsonDeck);
        }

        private void OnGUI() {

            if (GUILayout.Button("Create Player 1")) {
                var p1 = turnController.CreatePlayer(1, "Human", testDeserializedDeck, 10);
                turnController.RegisterPlayer(p1);
            }
            if (GUILayout.Button("Create Player 2")) {
                var p2 = turnController.CreatePlayer(1, "Demon", testDeserializedDeck, 10);
                turnController.RegisterPlayer(p2);
            }

            GUILayout.Space(50);

            if (GUILayout.Button("Load Resources")) {
                database.LoadCardDataFromResources();
            }

            if (GUILayout.Button("Create Deck JSON")) {
                CreateDeck();
            }

            GUILayout.Space(50);

            if (GUILayout.Button("Draw State")) {
                turnController.SetState(TurnState.Draw);
            }
        }
    }
}
