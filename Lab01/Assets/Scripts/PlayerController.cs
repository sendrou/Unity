using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private ContactFilter2D _platform;

    public UnityEvent Landed;
    public UnityEvent Dead;

    private Rigidbody2D _rigidbody;
    private bool _isOnPlatform => _rigidbody.IsTouching(_platform);
    private float speed = 5f;

    Vector2 move;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

    }
    // Update is called once per frame
    void Update()
    {
        move.x = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }
        Vector2 velocity = new Vector2(move.x * speed, _rigidbody.velocity.y);
        _rigidbody.velocity = velocity;
    }
    private void Jump()
    {
        if (_isOnPlatform == true)
        {
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.transform.parent != null)
        {
            if (collisionObject.transform.parent.TryGetComponent(out Platform platform))
                platform.StopMovement();
        }
        if(collisionObject.CompareTag("PlatformWall"))
            Dead?.Invoke();
        else if (collisionObject.CompareTag("Platform"))
        {
            collisionObject.tag = "Untagged";
            Landed?.Invoke();
        }
    }
}
