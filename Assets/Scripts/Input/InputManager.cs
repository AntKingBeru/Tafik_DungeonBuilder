using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private InputActionMap playerMap;
    [SerializeField] private InputActionMap buildMap;
    
    public InputActionAsset Actions => inputActions;

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        EnablePlayer();
    }

    public void EnablePlayer()
    {
        playerMap.Enable();
        buildMap.Disable();
    }
    
    public void EnableBuild()
    {
        buildMap.Enable();
        playerMap.Disable();
    }

    public InputAction GetBuildAction(string action)
    {
        return buildMap.FindAction(action);
    }

    public InputAction GetPlayerAction(string action)
    {
        return playerMap.FindAction(action);
    }
}