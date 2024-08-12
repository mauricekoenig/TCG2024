using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CardRuntimeData))]
public class CardInteractable : MonoBehaviour, IInteractable {

    private CardRuntimeData data;
    public GameObject shader;
    public bool dragging;
    private bool animating;

    private Vector3 hoverStartPosition;
    private Vector3 dragStartPosition;

    private void Start() {
        data = GetComponent<CardRuntimeData>();
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

    public void OnHover() {

        switch (data.Location) {

            case CardLocation.InHand:

                hoverStartPosition = transform.position;
                transform.position += new Vector3(0, .5f, -1);
                EnableShader();
                break;

            case CardLocation.InPlay:
                EnableShader();
                break;
        }
    }
    public void OnHoverExit() {
        
        switch (data.Location) {

            case CardLocation.InHand:
                transform.position = hoverStartPosition;
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

    public void OnDrag () {

        dragStartPosition = transform.position;
        dragging = true;
    }

    public void OnDragExit() {

        if (dragging) {
            animating = true;
            transform.DOMove(dragStartPosition, 3f).OnComplete(() => {
                animating = false;
                dragging = false;
            });
        }
    }

    public bool IsMovable () {

        return (data.Location == CardLocation.InHand && data.IsPlayable);
    }

    public bool IsAnimating () {
        return animating;
    }

    private void Update() {

        if (!dragging) return;
        HandleDragging();
    }

    public void HandleDragging () {

        float zDistance = 5f;
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = zDistance;

        transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
