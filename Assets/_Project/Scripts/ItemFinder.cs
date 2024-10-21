using System;
using UnityEngine;

public class ItemFinder : MonoBehaviour
{
    [SerializeField] private GameObject _crosshair1, _crosshair2;
    [SerializeField] private float _interactionDistance = 3f;

    private Camera _camera;

    public static event Action<ItemPickup> ItemFound;

    private void OnEnable()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        RaycastHit hit;

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (!ItemPickup.PickedUp)
        {
            if (Physics.Raycast(ray, out hit, _interactionDistance))
            {
                if (hit.collider.gameObject.TryGetComponent(out ItemPickup item))
                {
                    _crosshair1.SetActive(false);
                    _crosshair2.SetActive(true);
                    ItemFound?.Invoke(item);
                }
                else
                {
                    _crosshair1.SetActive(true);
                    _crosshair2.SetActive(false);
                    ItemFound?.Invoke(null);
                }
            }
        }
    }
}
