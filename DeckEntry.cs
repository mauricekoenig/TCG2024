

using System.Collections.Generic;

namespace Mk.Tcg.Core {

    public class DeckEntry {

        public string Name { get; set; }
        public int Quantity { get; set; }

        public DeckEntry (string name, int quantity) {

            Name = name;
            Quantity = quantity;
        }
    }

}
