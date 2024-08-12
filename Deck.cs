
using System.Collections.Generic;

namespace Mk.Tcg.Core {

    public class Deck {

        public List<DeckEntry> entries;
        public int size;

        public Deck() {
            entries = new();
        }

        public void Add(DeckEntry entry) {

            entries.Add(entry);
            size += entry.Quantity;
        }

        public void AddAllTest (IReadOnlyList<CardData> cards, int quantity) {
            foreach (CardData card in cards) {
                entries.Add(new DeckEntry(card.Name, quantity));
            }
        }
    }
}
