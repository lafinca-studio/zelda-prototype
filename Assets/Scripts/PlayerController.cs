using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
  PlayerInput playerInput;
  CharacterController characterController;
  Animator animator;

  int isWalkingHash;
  int isJumpingHash;
  bool isJumpingAnimating = false;
  Vector2 currentMovementInput;
  Vector3 currentMovement;
  bool isMovementPressed;
  float rotationFactorPerFrame = 15f;
  float groundedGravity = -0.05f;
  float gravity = -9.8f;
  float initialJumpVelocity;
  float maxJumpTime = 0.5f;
  float maxJumpHeight = 4f;

  bool isJumpPressed = false;
  bool isJumping;

  void Awake()
  {
    playerInput = new PlayerInput();
    characterController = GetComponent<CharacterController>();
    animator = GetComponent<Animator>();

    isWalkingHash = Animator.StringToHash("isWalking");
    isJumpingHash = Animator.StringToHash("isJumping");

    playerInput.CharacterControls.Move.started += onMovementInput;
    playerInput.CharacterControls.Move.canceled += onMovementInput;
    playerInput.CharacterControls.Move.performed += onMovementInput;
    playerInput.CharacterControls.Jump.started += onJump;
    playerInput.CharacterControls.Jump.canceled += onJump;

    setupJumpVariables();
  }

  private void setupJumpVariables()
  {
    float timeToApex = maxJumpTime / 2;
    gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
    initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
  }

  void handleJump()
  {
    if (!isJumping && characterController.isGrounded && isJumpPressed)
    {
      isJumping = true;
      animator.SetBool(isJumpingHash, true);
      isJumpingAnimating = true;
      currentMovement.y = initialJumpVelocity * 0.5f;
    }
    else if (!isJumpPressed && isJumping && characterController.isGrounded)
    {
      isJumping = false;
    }
  }

  private void onJump(InputAction.CallbackContext context)
  {
    isJumpPressed = context.ReadValueAsButton();
  }

  void onMovementInput(InputAction.CallbackContext context)
  {
    currentMovementInput = context.ReadValue<Vector2>();
    currentMovement.x = currentMovementInput.x;
    currentMovement.z = currentMovementInput.y;
    isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
  }

  void handleAnimation()
  {
    bool isWalking = animator.GetBool(isWalkingHash);

    if (isMovementPressed && !isWalking)
    {
      animator.SetBool(isWalkingHash, true);
    }
    else if (!isMovementPressed && isWalking)
    {
      animator.SetBool(isWalkingHash, false);
    }
  }

  void handleGravity()
  {
    bool isFalling = currentMovement.y <= 0.0f || !isJumpPressed;
    float fallMultiplier = 2.0f;
    if (characterController.isGrounded)
    {
      if (isJumpingAnimating) {
        animator.SetBool(isJumpingHash, false);
        isJumpingAnimating = false;
      }
      currentMovement.y = groundedGravity;
    } else if (isFalling) {
      float previousYVelocity = currentMovement.y;
      float newYVelocity = currentMovement.y + (gravity * fallMultiplier * Time.deltaTime);
      float nextYVelocity = (previousYVelocity + newYVelocity) * 0.5f;
      currentMovement.y = nextYVelocity;    }
    else
    {
      float previousYVelocity = currentMovement.y;
      float newYVelocity = currentMovement.y + (gravity * Time.deltaTime);
      float nextYVelocity = (previousYVelocity + newYVelocity) * 0.5f;
      currentMovement.y = nextYVelocity;
    }
  }

  void handleRotation()
  {
    Vector3 positionToLookAt;

    positionToLookAt.x = currentMovement.x;
    positionToLookAt.y = 0.0f;
    positionToLookAt.z = currentMovement.z;

    Quaternion currentRotation = transform.rotation;

    if (isMovementPressed)
    {
      Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
      transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
    }

  }

  void Update()
  {
    handleRotation();
    handleAnimation();
    characterController.Move(currentMovement * Time.deltaTime);
    handleGravity();
    handleJump();
  }

  private void OnEnable()
  {
    playerInput.CharacterControls.Enable();
  }

  private void OnDisable()
  {
    playerInput.CharacterControls.Disable();
  }
}
