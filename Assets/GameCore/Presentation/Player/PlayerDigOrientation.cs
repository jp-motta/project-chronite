using UnityEngine;
using UnityEngine.InputSystem;
using Domain.Entities;

public class PlayerDigOrientation : MonoBehaviour
{
  private Direction current = Direction.Right;

  private void Update()
  {
    if (Keyboard.current == null) return;

    if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) current = Direction.Right;
    else if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) current = Direction.Left;
    else if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) current = Direction.Up;
    else if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) current = Direction.Down;
  }

  public Direction GetDirection() => current;
}
