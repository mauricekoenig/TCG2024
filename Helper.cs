using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Helper : MonoBehaviour
{
    public Transform T;
    public CardViewLayoutSettings Settings;
    public CardData TestData;
    public GameObject CardPrefab;
    public float xMod;

    public void Align (Transform parent) {

        if (parent.childCount == 0) return;
        for (int i = 0; i < parent.childCount; i++) {
            float n = parent.childCount;
            float t = (float)i / (n - 1);
            float x = Mathf.Lerp(-Settings.HandWidth / 2, Settings.HandWidth / 2, t) * (xMod * n);
            float z = Mathf.Lerp(0, -Settings.Depth, t);
            parent.GetChild(i).transform.DOMove(new Vector3(x, parent.position.y, z), Settings.AnimationSpeed);
        }
    }
    public void CreateCard (CardData data) {

        CardRuntimeData runtimeData = Instantiate(CardPrefab).GetComponent<CardRuntimeData>();
        runtimeData.Init(data, CardLocation.InPlay);
        runtimeData.BattlePosition = BattlePosition.Attack;
    }

    private void OnGUI() {

        if (GUILayout.Button("Align")){
            Align(T);
        }

        if (GUILayout.Button("Create")) {
            CreateCard(TestData);
        }

    }
}
