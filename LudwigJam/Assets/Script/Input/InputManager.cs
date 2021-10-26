using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [HideInInspector]
    public PlayerControls controls;

    public Vector2 movement;
    public float jump, finish;
    public Vector2 lookValue;
    public float leftClick, rightClick;
    public float escape;
    public float bugReload;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Movement.performed += ctx => movement = ctx.ReadValue<Vector2>();
        controls.Player.Movement.canceled += ctx => movement = Vector2.zero;

        controls.Player.Jump.performed += ctx => jump = ctx.ReadValue<float>();
        controls.Player.Jump.canceled += ctx => jump = 0;

        //controls.Player.Dash.performed += ctx => dash = ctx.ReadValue<float>();
        //controls.Player.Dash.canceled += ctx => dash = 0;

        controls.Player.MouseLook.performed += ctx => lookValue = ctx.ReadValue<Vector2>();
        controls.Player.MouseLook.canceled += ctx => lookValue = Vector2.zero;

        controls.Player.LeftClick.performed += ctx => leftClick = ctx.ReadValue<float>();
        controls.Player.LeftClick.canceled += ctx => leftClick = 0;

        controls.Player.RighClick.performed += ctx => rightClick = ctx.ReadValue<float>();
        controls.Player.RighClick.canceled += ctx => rightClick = 0;

        controls.Player.Escape.performed += ctx => escape = ctx.ReadValue<float>();
        controls.Player.Escape.canceled += ctx => escape = 0;

        controls.Player.Finish.performed += ctx => finish = ctx.ReadValue<float>();
        controls.Player.Finish.canceled += ctx => finish = 0;

        controls.Player.UnBug.performed += ctx => bugReload = ctx.ReadValue<float>();
        controls.Player.UnBug.canceled += ctx => bugReload = 0;
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

}
