
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using DG.Tweening;

public class CardViewController : MonoBehaviour {

    public GameObject cardViewPrefab;
    public GameObject cardViewPlayPrefab;
    public CardViewLayoutSettings settings;

    #region Transforms
    public Transform player1Hand;
    public Transform player2Hand;

    public Transform player1Board;
    public Transform player2Board;

    public Transform localDeck;
    public Transform remoteDeck;
    #endregion

    public void SpawnCardOnDeck (int player) {

        Transform hand = GetHandTransform(player);
        if (hand.childCount == settings.MaxHandSize) return;

        Vector3 spawnPosition = player == 1 ? localDeck.transform.position : remoteDeck.transform.position;
        Quaternion spawnRotation = player == 1 ? localDeck.transform.rotation : remoteDeck.transform.rotation;

        GameObject cardObject = Instantiate(cardViewPrefab, spawnPosition,spawnRotation);
        CardView cardView = cardObject.GetComponent<CardView>();

        cardObject.transform.SetParent(hand);
        if (player == 1) cardView.ShowFront();
        if (player == 2) cardView.ShowBack();

        UpdateHandView(hand);
    }
    public void SpawnCardOnBoard (int player) {

        Transform board = GetBoardTransform(player);

        if (board.childCount == settings.MaxBoardSize) return;
        GameObject cardObject = Instantiate(cardViewPlayPrefab);
        cardObject.transform.SetParent(board, true);
        UpdateBoardView(board);
    }

    private void UpdateHandView (Transform hand) {

        if (hand == null) return;

        for (int i = 0; i < hand.childCount; i++) {

            int n = hand.childCount;
            float yOffset = hand == player1Hand ? -settings.BaseOffsetY : settings.BaseOffsetY;
            float t = n > 1 ? (float)i / (n - 1) : .5f;
            float x = Mathf.Lerp(-settings.HandWidth / 2, settings.HandWidth / 2, t) * (settings.OverlapHand * n);
            float y = yOffset;
            float z = Mathf.Lerp(0, -settings.Depth, t);

            Transform view = hand.GetChild(i);
            if (view == null) continue;
            view.DOMove(new Vector3(x, y, z), settings.AnimationSpeed);
            view.DORotate(new Vector3(0, 0, 0), settings.AnimationSpeed);
        }
    }
    private void UpdateBoardView (Transform board) {

        for (int i = 0; i < board.childCount; i++) {

            int n = board.childCount;
            float t = n > 1 ? (float)i / (n - 1) : .5f;
            float x = Mathf.Lerp(-board.localScale.x / 2, board.localScale.x / 2, t) * (settings.OverlapBoard * n);
            float y = board.position.y;
            board.GetChild(i).DOMove(new Vector3(x, y, 0), settings.AnimationSpeed);
        }
    }

    public void RemoveFromHandView (int player) {

        Transform hand = GetHandTransform(player);
        if (hand.childCount == 0) return;
        GameObject cardObject = hand.GetChild(hand.childCount - 1).gameObject;
        cardObject.transform.SetParent(cardObject.transform.parent.parent);
        Destroy(cardObject);
        UpdateHandView(hand);
    }

    public Transform GetHandTransform (int player) {
        return player == 1 ? player1Hand : player2Hand;
    }
    public Transform GetBoardTransform (int player) {
        return player == 1 ? player1Board : player2Board;
    }

    private void OnGUI() {

        if (GUILayout.Button("Add Local Hand")) SpawnCardOnDeck(1);
        if (GUILayout.Button("Add Local Board")) SpawnCardOnBoard(1);
        if (GUILayout.Button("Remove Local Hand")) RemoveFromHandView(1);

        GUILayout.Space(10);

        if (GUILayout.Button("Add Remote Hand")) SpawnCardOnDeck(2);
        if (GUILayout.Button("Add Remote Board")) SpawnCardOnBoard(2);
        if (GUILayout.Button("Remove Remote Hand")) RemoveFromHandView(2);

        GUILayout.Space(10);


        if (GUILayout.Button("Reset")) SceneManager.LoadScene(0);
    }

}
