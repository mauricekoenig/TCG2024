


using Mk.Tcg.Core;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Mk.Tcg.Gameplay {

    [RequireComponent(typeof(PlayerCards))]
    [RequireComponent(typeof(PlayerResources))]

    public abstract class Player : MonoBehaviour {

        [SerializeField] protected int id;
        [SerializeField] protected string playerName;
        [SerializeField] protected PlayerCards cards;
        [SerializeField] protected PlayerResources resources;

        public void Init (int id, string name, List<CardData> deck, int startingResources) {


            cards = GetComponent<PlayerCards>();
            resources = GetComponent<PlayerResources>();

            this.id = id;
            playerName = name;
            cards.Init(deck);
            resources.SetCurrentResources(startingResources);
        }

        public void SetObjectName (string name) {
            gameObject.name = name;
        }
    }
}
