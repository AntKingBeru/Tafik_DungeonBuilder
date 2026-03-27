using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerMode CurrentMode { get; private set; } = PlayerMode.Gameplay;
    
    public bool IsInGameplay => CurrentMode == PlayerMode.Gameplay;

    public void CycleBuildMode()
    {
        if (CurrentMode == PlayerMode.Gameplay)
            CurrentMode = PlayerMode.Clear;
        else
            CurrentMode = (PlayerMode)(((int)CurrentMode + 1) % 4);
        
        Debug.Log($"Player mode changed to {CurrentMode}");
    }

    public void ReturnToGameplay()
    {
        if (CurrentMode == PlayerMode.Gameplay)
            return;
        
        CurrentMode = PlayerMode.Gameplay;
        Debug.Log($"Player mode changed to {CurrentMode}");   
    }
}