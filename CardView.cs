using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardRuntimeData))]
public class CardView : MonoBehaviour {

    public CardRuntimeData data;
    public GameObject frontView;
    public GameObject backView;

    public void Start() {
    
        data = GetComponent<CardRuntimeData>();
        data.OnPropertyChanged += OnRuntimeDataChanged;
        data.OnBattlePositionChanged += OnBattlePositionChanged;
    }

    private void OnDestroy() {

        data.OnPropertyChanged -= OnRuntimeDataChanged;
        data.OnBattlePositionChanged -= OnBattlePositionChanged;
    }

    public void ShowFront () {
        if (backView.activeInHierarchy) backView.SetActive(false);
        frontView.SetActive(true);
    }

    public void ShowBack() {
        if (frontView.activeInHierarchy) frontView.SetActive(false);
        backView.SetActive(true);
    }

    public void OnRuntimeDataChanged () {
        Debug.Log("OnRuntimeDataChanged");
    }

    public void OnBattlePositionChanged () {

        Debug.Log("Switched BattlePosition!");
    }
}
