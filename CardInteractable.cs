using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInteractable : MonoBehaviour, IInteractable {

    private Tween tween;
    private Vector3 startPosition;
    public GameObject shader;

    public void OnHover() {

        startPosition = transform.position;
        transform.position += new Vector3(0, .5f, -1);
        if (shader.activeInHierarchy) return;
        shader.SetActive(true);
    }

    public void OnHoverExit() {
        
        transform.position = startPosition;
        if (!shader.activeInHierarchy) return;
        shader.SetActive(false);
    }

    public void OnLeftClick() {
        Debug.Log("OnLeftClick!");
    }

    public void OnRightClick() {
        Debug.Log("OnRightClick");
    }
}
