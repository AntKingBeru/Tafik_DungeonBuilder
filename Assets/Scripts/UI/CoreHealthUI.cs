using UnityEngine;
using UnityEngine.UI;

public class CoreHealthUI : MonoBehaviour
{
    [SerializeField] private Image fillerImage;
    
    private CoreHealth _core;

    private void Start()
    {
        var coreRoom = GridManager.Instance.GetCoreRoom();

        if (!coreRoom)
            return;
        
        _core = coreRoom.GetCoreHealth();

        if (!_core)
            return;

        _core.OnHealthChanged += UpdateUI;

        UpdateUI(1, 1);
    }

    private void OnDestroy()
    {
        if (_core)
            _core.OnHealthChanged -= UpdateUI;
    }

    private void UpdateUI(int current, int max)
    {
        fillerImage.fillAmount = (float) current / max;
    }
}