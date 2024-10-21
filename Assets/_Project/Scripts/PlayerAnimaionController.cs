using UnityEngine;

public class PlayerAnimaionController : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        ItemPickup.ItemPickedUp += PlayPickUpAnimation;
        ItemPickup.ItemThrow += PlayThrowAnitaion;
    }

    private void OnDisable()
    {
        ItemPickup.ItemPickedUp -= PlayPickUpAnimation;
        ItemPickup.ItemThrow -= PlayThrowAnitaion;
    }

    private void FixedUpdate()
    {
        if (_playerAnimator != null)
        {
            if (_rigidbody.velocity.magnitude > 0.2)
            {
                _playerAnimator.SetBool("isWalking", true);
            }
            else
            {
                _playerAnimator.SetBool("isWalking", false);
            }
        }

        if (ItemPickup.PickedUp)
        {
            _playerAnimator.SetBool("isCarrying", true);
        }
        else
        {
            _playerAnimator.SetBool("isCarrying", false);
        }
    }

    private void PlayPickUpAnimation()
    {
        _playerAnimator.SetTrigger("PickedUp");
    }

    private void PlayThrowAnitaion()
    {
        _playerAnimator.SetTrigger("ItemThrow");
    }
}
