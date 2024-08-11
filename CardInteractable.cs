using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardRuntimeData))]
public class CardInteractable : MonoBehaviour, IInteractable {

    private CardRuntimeData data;
    private Vector3 startPosition;
    public GameObject shader;
    public bool dragging;
    private bool animating;

    private void Start() {
        data = GetComponent<CardRuntimeData>();
    }

    public void OnHover() {

        switch (data.Location) {

            case CardLocation.InHand:

                startPosition = transform.position;
                transform.position += new Vector3(0, .5f, -1);
                EnableShader();
                break;

            case CardLocation.InPlay:
                EnableShader();
                break;
        }
    }

    public void EnableShader () {
        if (shader.activeInHierarchy) return;
        shader.SetActive(true);
    }
    public void DisableShader () {
        if (!shader.activeInHierarchy) return;
        shader.SetActive(false);
    }
    public void ChangeBattlePosition() {

        if (animating) return;
        animating = true;

        Vector3 cardRotation = data.BattlePosition == BattlePosition.Attack ? new Vector3(0, 0, 90) : new Vector3(0, 0, 0);
        BattlePosition battlePosition = data.BattlePosition == BattlePosition.Attack ? BattlePosition.Defense : BattlePosition.Attack;

        transform.DORotate(cardRotation, .5f).OnComplete(() => {
            data.BattlePosition = battlePosition;
            animating = false;
        });
    }
    public void OnHoverExit() {
        
        switch (data.Location) {

            case CardLocation.InHand:
                transform.position = startPosition;
                DisableShader();
                break;

            case CardLocation.InPlay:
                DisableShader();
                break;
        }
    }

    public void OnLeftClick() {

        switch (data.Location) {

            case CardLocation.InPlay:
                ChangeBattlePosition();
                break;
        }
    }

    public void OnRightClick() {

        Debug.Log("OnRightClick");
    }

    public void OnDrag() {

        dragging = true;
    }

    public void OnDragExit() {

        dragging = false;
    }
}
