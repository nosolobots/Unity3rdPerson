using System;
using UnityEngine;

public class PlayerTestState : PlayerBaseState
{
    float _time;

    public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        Debug.Log("Test State Enter: init timer");

        _stateMachine.InputReader.JumpEvent += OnJump;
    }

    private void OnJump()
    {
        _stateMachine.SwitchState(new PlayerTestState(_stateMachine));
    }

    public override void Tick(float deltaTime)
    {
        _time += deltaTime;

        Debug.Log("Test State Tick: " + Mathf.RoundToInt(_time));
    }

    public override void Exit()
    {
        Debug.Log("Test State Exit");

        _stateMachine.InputReader.JumpEvent -= OnJump;
    }
}
