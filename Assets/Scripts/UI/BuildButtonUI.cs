using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildButtonUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private Button button;
    
    [Header("Colors")]
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color selectedColor = Color.green;
    [SerializeField] private Color disabledColor = Color.gray;

    private RoomData _roomData;
    private BuildMenuUI _menu;

    public void Initialize(RoomData data, BuildMenuUI menu)
    {
        _roomData = data;
        _menu = menu;

        nameText.text = data.roomName;
        costText.text = $"S: {data.stoneCost} W: {data.woodCost}";
        
        button.onClick.AddListener(OnClicked);
    }

    private void OnClicked()
    {
        _menu.SelectRoom(_roomData);
    }
    
    public void SetSelected(bool selected)
    {
        background.color = selected ? selectedColor : normalColor;
    }

    public void SetInteractable(bool canAfford)
    {
        button.interactable = canAfford;
        background.color = canAfford ? background.color : disabledColor;
    }
    
    public RoomData GetData() => _roomData;
}