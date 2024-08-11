

using System;
using UnityEngine;

public class CardRuntimeData : MonoBehaviour {

    [SerializeField] private CardData data;
    [SerializeField] private string cardName;
    [SerializeField] private int cost;
    [SerializeField] private int attack;
    [SerializeField] private int defense;
    [SerializeField] private BattlePosition battlePosition;
    [SerializeField] private CardLocation location;

    [SerializeField] private bool isPlayable;

    public event Action OnPropertyChanged;
    public event Action OnBattlePositionChanged;

    public void OnPlayerResourcesChanged (int currentResources) {

        isPlayable  = (cost <= currentResources);
        NotifyPropertyChanged();
    }

    public void NotifyPropertyChanged() {

        OnPropertyChanged?.Invoke();
    }

    public CardData Data {
        get { return data; }
    }

    public CardLocation Location {
        get { return location; }
        set {
            location = value;
        }
    }

    public string Name {
        get { return cardName; }
        set {
            cardName = value;
            NotifyPropertyChanged();
        }
    }

    public int Cost {
        get { return cost; }
        set {
            cost = value;
            NotifyPropertyChanged();
        }
    }

    public int Attack {
        get { return attack; }
        set {
            attack = value;
            NotifyPropertyChanged();
        }
    }

    public int Defense {
        get { return defense; }
        set {
            defense = value;
            NotifyPropertyChanged();
        }
    }

    public BattlePosition BattlePosition {
        get { return battlePosition; }
        set {
            battlePosition = value;
            OnBattlePositionChanged?.Invoke();
        }
    }

    public void Init (CardData data, CardLocation location = CardLocation.InHand) {

        this.data = data;
        name = data.name;
        cost = data.Cost;
        attack = data.Attack;
        defense = data.Defense;
        battlePosition = BattlePosition.None;
        this.location = location;
        OnPropertyChanged?.Invoke();
    }

} 
