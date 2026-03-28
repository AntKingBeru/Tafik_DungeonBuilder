using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

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
    private TrapData _trapData;
    private BuildMenuUI _menu;

    private ResourceData[] _cost;

    public void Initialize(RoomData data, BuildMenuUI menu)
    {
        _roomData = data;
        _trapData = null;
        _menu = menu;

        _cost = data.cost;

        nameText.text = data.roomName;
        costText.text = FormatCost(data.Cost);
        
        button.onClick.AddListener(OnClicked);
    }

    public void Initialize(TrapData data, BuildMenuUI menu)
    {
        _trapData = data;
        _roomData = null;
        _menu = menu;
        
        _cost = data.cost;

        nameText.text = data.trapName;
        costText.text = FormatCost(data.Cost);
        
        button.onClick.AddListener(OnClicked);
    }

    private string FormatCost(ResourceData[] cost)
    {
        if (cost == null || cost.Length == 0)
            return "Free";

        var sb = new StringBuilder();
        
        foreach (var c in cost)
            sb.Append($"{c.type}: {c.amount}");

        return sb.ToString();
    }

    private void OnClicked()
    {
        if (_roomData)
            _menu.SelectRoom(_roomData);
        else if (_trapData)
            _menu.SelectTrap(_trapData);
    }
    
    public void SetSelected(bool selected)
    {
        background.color = selected ? selectedColor : normalColor;
    }

    public void SetInteractable(bool canAfford)
    {
        button.interactable = canAfford;

        if (!canAfford)
            background.color = disabledColor;
    }
    
    public ResourceData[] GetCost() => _cost;
    public RoomData GetRoom() => _roomData;
    public TrapData GetTrap() => _trapData;
}