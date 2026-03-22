using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputReader : MonoBehaviour
{
    [Header("Input Actions")]
    [SerializeField] private InputActionReference pointAction;
    [SerializeField] private InputActionReference clickAction;

    public Vector2 MousePosition { get; private set; }

    public event Action OnClick;

    private void OnEnable()
    {
        pointAction.action.Enable();
        clickAction.action.Enable();

        pointAction.action.performed += OnPoint;
        clickAction.action.performed += OnClickPerformed;
    }
    
    private void OnDisable()
    {
        pointAction.action.performed -= OnPoint;
        clickAction.action.performed -= OnClickPerformed;
        
        pointAction.action.Disable();
        clickAction.action.Disable();
    }

    private void OnPoint(InputAction.CallbackContext context)
    {
        MousePosition = context.ReadValue<Vector2>();
    }

    private void OnClickPerformed(InputAction.CallbackContext context)
    {
        OnClick?.Invoke();
    }
}