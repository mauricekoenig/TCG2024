


using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class InputController : MonoBehaviour {

    private Camera mainCamera;
    public LayerMask HoverMask;
    private IInteractable currentSelectedObject;
    [SerializeField] private InputState currentState;

    private void Start() {

        mainCamera = Camera.main;
        currentSelectedObject = null;
    }

    public void Update() {

        HandleInput();
    }

    private void HandleInput () {

        LeftMouseDown();
        RightButtonUp();
        Hovering();
    }
    private void LeftMouseDown () {

        if (Input.GetMouseButtonDown(0)) {

            if (currentState == InputState.Dragging) return;

            Ray ray = GetRayFromCamera();

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, HoverMask)) {

                if (!hit.collider.TryGetComponent<IInteractable>(out IInteractable interactable)) return;
                if (interactable.IsAnimating()) return;

                if (currentSelectedObject != null && currentState == InputState.Hovering) {
                    currentSelectedObject.OnHoverExit();
                    currentState = InputState.None;
                }

                currentSelectedObject = interactable;

                if (interactable.IsMovable()) { // If card can be played. In Hand & enough Resources.
                    currentState = InputState.Dragging;
                    interactable.OnDrag();
                    return;
                }

                interactable.OnLeftClick();
            }

        }
    }
    private void RightButtonUp() {

        if (Input.GetMouseButtonUp(1)) {

            if (currentState == InputState.Dragging) {
                if (currentSelectedObject == null) return;
                if (currentSelectedObject.IsAnimating()) return;
                currentSelectedObject.OnDragExit();
                currentState = InputState.None;
            }
        }
    }
    private void Hovering () {

        if (currentState == InputState.Dragging) return;

        Ray ray = GetRayFromCamera();
        Debug.DrawRay(ray.origin, ray.direction * 20f, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, HoverMask)) {

            if (!hit.collider.TryGetComponent(out IInteractable interactable)) return; // is not a IInteractable.
            if (interactable.IsAnimating()) return;
            if (currentSelectedObject == interactable) return; // if its the same object.

            if (currentSelectedObject != null) currentSelectedObject.OnHoverExit(); // if there is a preview already.

            currentState = InputState.Hovering;
            currentSelectedObject = interactable;
            interactable.OnHover();
            Debug.Log("Hovering");
        }

        else { // if we dont hit a Hoverable Object.
            if (currentSelectedObject == null) return;
            currentSelectedObject.OnHoverExit(); // and there is a previous one, Unhover it and reset.
            currentSelectedObject = null;
            currentState = InputState.None;
        }
    }

    private Ray GetRayFromCamera () {
        return mainCamera.ScreenPointToRay(Input.mousePosition);
    }
}
