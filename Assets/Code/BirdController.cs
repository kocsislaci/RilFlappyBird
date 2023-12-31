using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

// This Class is responsible to
// - Listen to the user input
// - Make the bird jump
// - Listen to collisions
// - Send dying event to listeners

public class BirdController : MonoBehaviour
{
  [SerializeField] private float jumpVelocity;
  private Rigidbody2D _rigidbody2D;
  private PlayerInput _playerInput;
  private Animator animator;
  public UnityEvent die = new();



  private void Awake()
  {
    _rigidbody2D = GetComponent<Rigidbody2D>();
    _playerInput = GetComponent<PlayerInput>();
    _playerInput.actions["Jump"].performed += OnJump;
    animator = this.GetComponent<Animator>();
    animator.speed = 0.5f;
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
    {
      die.Invoke();
    }
  }

  private void OnJump(InputAction.CallbackContext context)
  {
    _rigidbody2D.velocity = Vector2.up * jumpVelocity;
  }

  void update()
  {
    if (Input.GetButtonDown("space"))
    {
      animator.Play("fasz");
    }
  }
}
