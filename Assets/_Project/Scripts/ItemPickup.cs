using System;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private Transform _itemPlace;
    [SerializeField] private float _throwForce = 500f;

    private Rigidbody _objectRigidboby;

    public bool Interactable { get; private set; } = false;
    public static bool PickedUp { get; private set; } = false;

    public static event Action ItemPickedUp;
    public static event Action ItemThrow;

    private void OnEnable()
    {
        _objectRigidboby = GetComponent<Rigidbody>();

        ItemFinder.ItemFound += OnItemFound;
    }

    private void OnDisable()
    {
        ItemFinder.ItemFound -= OnItemFound;
    }

    private void OnItemFound(ItemPickup item)
    {
        if (item != null)
        {
            item.Interactable = true;
        }
        else
        {
            Interactable = false;
        }
    }

    private void Update()
    {
        if (Interactable)
        {
            if(Input.GetMouseButtonDown(0))
            {
                transform.position = _itemPlace.position;
                _objectRigidboby.useGravity = false;
                PickedUp = true;
                ItemPickedUp?.Invoke();
            }

            if (PickedUp)
            {
                transform.position = _itemPlace.position;

                if (Input.GetMouseButtonDown(1))
                {
                    _objectRigidboby.useGravity = true;
                    _objectRigidboby.velocity = _itemPlace.forward * _throwForce * Time.deltaTime;
                    PickedUp = false;
                    ItemThrow?.Invoke();
                }
            }
        }
    }
}
