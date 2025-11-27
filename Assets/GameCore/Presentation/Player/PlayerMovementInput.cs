using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementInput : MonoBehaviour
{
  [SerializeField] private float moveSpeed = 5f;

  private Rigidbody2D rb;
  private float inputX;

  void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
  }

  void Update()
  {
    inputX = 0f;
    if (Keyboard.current == null) return;

    if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
      inputX -= 1f;
    if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
      inputX += 1f;
  }

  void FixedUpdate()
  {
    rb.linearVelocity = new Vector2(inputX * moveSpeed, rb.linearVelocity.y);
  }
}
