using UnityEngine;
using TMPro;

public class ResourceUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI stoneText;
    [SerializeField] private TextMeshProUGUI woodText;

    private void Start()
    {
        ResourceManager.Instance.OnResourceChanged += UpdateUI;
        UpdateUI(ResourceManager.Instance.Stone, ResourceManager.Instance.Wood);
    }

    private void OnDestroy()
    {
        ResourceManager.Instance.OnResourceChanged -= UpdateUI;
    }

    private void UpdateUI(int stone, int wood)
    {
        stoneText.text = stone.ToString();
        woodText.text = wood.ToString();
    }
}