using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private string playerMapName = "Player";
    [SerializeField] private string buildMapName = "Build";
    
    private InputActionMap _playerMap;
    private InputActionMap _buildMap;
    
    public InputActionAsset Actions => inputActions;

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
        _playerMap = inputActions.FindActionMap(playerMapName);
        _buildMap = inputActions.FindActionMap(buildMapName);

        EnablePlayer();
    }

    public void EnablePlayer()
    {
        _playerMap.Enable();
        _buildMap.Disable();
    }
    
    public void EnableBuild()
    {
        _buildMap.Enable();
        _playerMap.Disable();
    }

    public InputAction GetBuildAction(string action)
    {
        return _buildMap.FindAction(action);
    }

    public InputAction GetPlayerAction(string action)
    {
        return _playerMap.FindAction(action);
    }
}