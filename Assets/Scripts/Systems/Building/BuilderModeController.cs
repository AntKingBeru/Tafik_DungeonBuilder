using UnityEngine;
using UnityEngine.InputSystem;

public class BuilderModeController : MonoBehaviour
{
    private const string PlayerMap = "Player";
    private const string BuilderMap = "Builder";
    
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private BuildSystem buildSystem;

    private bool _buildModeActive;

    public void EnterBuildMode()
    {
        if (_buildModeActive) return;
        
        _buildModeActive = true;
        
        playerInput.SwitchCurrentActionMap(BuilderMap);
        buildSystem.enabled = true;
    }

    public void ExitBuildMode()
    {
        if (!_buildModeActive) return;

        _buildModeActive = false;
        
        playerInput.SwitchCurrentActionMap(PlayerMap);
        buildSystem.enabled = false;
    }
}