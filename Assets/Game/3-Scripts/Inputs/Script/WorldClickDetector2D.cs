using UnityEngine;

public class WorldClickDetector2D : MonoBehaviour
{
    [SerializeField] private Camera _worldCamera;
    [SerializeField] private LayerMask _clickableLayers;

    private void Start()
    {
        TouchInputManager.Instance.OnTap += HandleTap;
    }

    private void OnDestroy()
    {
        if (TouchInputManager.Instance != null)
            TouchInputManager.Instance.OnTap -= HandleTap;
    }

    private void HandleTap(Vector2 screenPosition)
    {
        if (_worldCamera == null)
            return;

        Vector3 worldPosition =
            _worldCamera.ScreenToWorldPoint(screenPosition);

        Vector2 point = new Vector2(
            worldPosition.x,
            worldPosition.y
        );

        Collider2D touchedCollider =
            Physics2D.OverlapPoint(point, _clickableLayers);

        if (touchedCollider == null)
            return;

        IClickable clickable =
            touchedCollider.GetComponentInParent<IClickable>();

        clickable?.OnClicked();
    }
}