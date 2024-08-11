


using UnityEngine;

public class InputController : MonoBehaviour {

    private Camera mainCamera;
    public LayerMask HoverMask;

    private IInteractable currentHoveredObject;
    public bool Foo;

    private void Start() {

        mainCamera = Camera.main;
        currentHoveredObject = null;
    }

    public void Update() {

        HandleInput();
        Foo = (currentHoveredObject != null);
    }

    private void HandleInput () {

        OnLeftClick();
        OnRightClick();
        OnHovering();
    }
    private void OnLeftClick () {

        if (Input.GetMouseButtonDown(0)) {

            Ray ray = GetRay();

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, HoverMask)) {

                if (!hit.collider.TryGetComponent<IInteractable>(out IInteractable interactable)) return;
                if (currentHoveredObject != null && currentHoveredObject != interactable) currentHoveredObject.OnHoverExit();
                currentHoveredObject = interactable;

                Debug.Log("OnLeftClick!");
            }


        }
    }
    private void OnRightClick() {

    }
    private void OnHovering () {

        Ray ray = GetRay();
        Debug.DrawRay(ray.origin, ray.direction * 20f, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, HoverMask)) {

            if (!hit.collider.TryGetComponent(out IInteractable interactable)) return;
            if (currentHoveredObject == interactable) return; // if its the same object.

            if (currentHoveredObject != null) currentHoveredObject.OnHoverExit();
            currentHoveredObject = interactable;
            interactable.OnHover();
        }

        else {
            if (currentHoveredObject == null) return;
            currentHoveredObject.OnHoverExit();
            currentHoveredObject = null;
        }
    }

    private Ray GetRay () {
        return mainCamera.ScreenPointToRay(Input.mousePosition);
    }
}
