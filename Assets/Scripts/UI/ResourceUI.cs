using UnityEngine;
using TMPro;

public class ResourceUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI stoneText;
    [SerializeField] private TextMeshProUGUI woodText;

    private void Start()
    {
        ResourceManager.Instance.OnResourcesChanged += UpdateUI;
        UpdateUI();
    }

    private void OnDestroy()
    {
        if (ResourceManager.Instance)
            ResourceManager.Instance.OnResourcesChanged -= UpdateUI;
    }

    private void UpdateUI()
    {
        stoneText.text = ResourceManager.Instance.Get(ResourceType.Stone).ToString();
        woodText.text = ResourceManager.Instance.Get(ResourceType.Wood).ToString();
    }
}