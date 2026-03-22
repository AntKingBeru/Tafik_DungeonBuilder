using UnityEngine;
using System;

public class PlayerModeController : MonoBehaviour
{
    public PlayerMode CurrentMode { get; private set; } = PlayerMode.None;
    
    public event Action<PlayerMode> OnModeChanged;

    public void SetMode(PlayerMode mode)
    {
        if (CurrentMode == mode)
            return;
        
        CurrentMode = mode;
        OnModeChanged?.Invoke(mode);
    }
    
    // TEMP: debug switching
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SetMode(PlayerMode.Clear);
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SetMode(PlayerMode.Build);
        
        if (Input.GetKeyDown(KeyCode.Escape))
            SetMode(PlayerMode.None);
    }
}