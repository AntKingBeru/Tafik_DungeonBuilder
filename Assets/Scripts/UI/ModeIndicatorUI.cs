using UnityEngine;
using TMPro;

public class ModeIndicatorUI : MonoBehaviour
{
    [SerializeField] private PlayerStateMachine playerState;
    [SerializeField] private TextMeshProUGUI text;

    private void Update()
    {
        text.text = $"Mode: {playerState.CurrentMode}";
    }
}