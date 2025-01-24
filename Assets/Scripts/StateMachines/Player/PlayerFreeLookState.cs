using System;
using Unity.Mathematics;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    readonly int _freeLookSpeed = Animator.StringToHash("FreeLookSpeed");
    const float _animatorDumpTime = 0.1f;

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        Debug.Log("Test State Enter: init timer");

        
    }

    private void OnJump()
    {
        
    }

    public override void Tick(float deltaTime)
    {
        //Vector3 movement = new Vector3();
        //movement.x = _stateMachine.InputReader.MovementValue.x;
        //movement.y = 0;
        //movement.z = _stateMachine.InputReader.MovementValue.y;
        //_stateMachine.transform.Translate(movement * deltaTime);

        Vector3 movement = CalculateMovement();

        _stateMachine.characterController.Move(_stateMachine.FreeLookMovementSpeed * deltaTime * movement);

        if (_stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            _stateMachine.Animator.SetFloat(_freeLookSpeed, 0, _animatorDumpTime, deltaTime);
            return;
        }
        _stateMachine.Animator.SetFloat(_freeLookSpeed, 1, _animatorDumpTime, deltaTime);
        
        //_stateMachine.transform.10rotation = Quaternion.LookRotation(movement);
        FaceMovementDirection(movement, deltaTime);
    }

    public override void Exit()
    {
               
    }

    Vector3 CalculateMovement()
    {
        Vector3 forward = _stateMachine.MainCameraTransform.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 right = _stateMachine.MainCameraTransform.right;
        right.y = 0;
        right.Normalize();

        Vector3 movement = right * _stateMachine.InputReader.MovementValue.x + 
            forward * _stateMachine.InputReader.MovementValue.y;
        
        return movement;
    }

    void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        _stateMachine.transform.rotation = Quaternion.Lerp(
            _stateMachine.transform.rotation,
            Quaternion.LookRotation(movement),
            deltaTime * _stateMachine.RotationDamping);
    }
}
