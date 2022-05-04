using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
  PlayerInput _playerInput;
  CharacterController _characterController;
  Animator _animator;

  int _isWalkingHash;
  int _isJumpingHash;
  bool _requireNewJumpPress = false;
  Vector2 _currentMovementInput;
  Vector3 _currentMovement;
  Vector3 _appliedMovement;
  bool _isMovementPressed;
  float _rotationFactorPerFrame = 15f;
  float _groundedGravity = -0.05f;
  float _gravity = -9.8f;
  float _initialJumpVelocity;
  float _maxJumpTime = 0.5f;
  float _maxJumpHeight = 2f;

  bool _isJumpPressed = false;
  bool _isJumping;

  PlayerBaseState _currentState;
  PlayerStateFactory _states;

  public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
  public Animator Animator { get { return _animator; } }
  public CharacterController CharacterController { get { return _characterController; } }
  public float Gravity { get { return _gravity; } }
  public float InitialJumpVelocity { get { return _initialJumpVelocity; } }
  public bool IsJumpPressed { get { return _isJumpPressed; } }
  public int IsJumpingHash { get { return _isJumpingHash; } }
  public int IsWalkingHash { get { return _isWalkingHash; } }
  public bool IsMovementPressed { get { return _isMovementPressed; } }
  public bool RequireNewJumpPress { get { return _requireNewJumpPress; } set { _requireNewJumpPress = value; } }
  public bool IsJumping { set { _isJumping = value; } }
  public bool IsJumpingPressed { get { return _isJumpPressed; } }
  public float GroundedGravity { get { return _groundedGravity; } }
  public float CurrentMovementY { get { return _currentMovement.y; } set { _currentMovement.y = value; } }
  public Vector2 CurrentMovementInput { get { return _currentMovementInput; } set { _currentMovementInput = value; } }
  public float AppliedMovementY { get { return _appliedMovement.y; } set { _appliedMovement.y = value; } }
  public float AppliedMovementX { get { return _appliedMovement.x; } set { _appliedMovement.x = value; } }
  public float AppliedMovementZ { get { return _appliedMovement.z; } set { _appliedMovement.z = value; } }

  void Awake()
  {
    _playerInput = new PlayerInput();
    _characterController = GetComponent<CharacterController>();
    _animator = GetComponent<Animator>();

    _states = new PlayerStateFactory(this);
    _currentState = _states.Grounded();
    _currentState.Enter();

    _isWalkingHash = Animator.StringToHash("isWalking");
    _isJumpingHash = Animator.StringToHash("isJumping");

    _playerInput.CharacterControls.Move.started += onMovementInput;
    _playerInput.CharacterControls.Move.canceled += onMovementInput;
    _playerInput.CharacterControls.Move.performed += onMovementInput;
    _playerInput.CharacterControls.Jump.started += onJump;
    _playerInput.CharacterControls.Jump.canceled += onJump;

    setupJumpVariables();
  }

  // Update is called once per frame
  void Update()
  {
    handleRotation();
    _currentState.UpdateStates();
    _characterController.Move(_appliedMovement * Time.deltaTime);
  }

  void handleRotation()
  {
    Vector3 positionToLookAt;

    positionToLookAt.x = _currentMovement.x;
    positionToLookAt.y = 0.0f;
    positionToLookAt.z = _currentMovement.z;

    Quaternion currentRotation = transform.rotation;

    if (_isMovementPressed)
    {
      Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
      transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, _rotationFactorPerFrame * Time.deltaTime);
    }
  }


  private void setupJumpVariables()
  {
    float timeToApex = _maxJumpTime / 2;
    _gravity = (-2 * _maxJumpHeight) / Mathf.Pow(timeToApex, 2);
    _initialJumpVelocity = (2 * _maxJumpHeight) / timeToApex;
  }

  private void onJump(InputAction.CallbackContext context)
  {
    _isJumpPressed = context.ReadValueAsButton();
    _requireNewJumpPress = false;
  }

  void onMovementInput(InputAction.CallbackContext context)
  {
    _currentMovementInput = context.ReadValue<Vector2>();
    _currentMovement.x = _currentMovementInput.x;
    _currentMovement.z = _currentMovementInput.y;
    _isMovementPressed = _currentMovementInput.x != 0 || _currentMovementInput.y != 0;
  }

  private void OnEnable()
  {
    _playerInput.CharacterControls.Enable();
  }

  private void OnDisable()
  {
    _playerInput.CharacterControls.Disable();
  }
}
