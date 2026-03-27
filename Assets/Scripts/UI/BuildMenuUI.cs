using UnityEngine;
using System.Collections.Generic;

public class BuildMenuUI : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private BuildMenuData menuData;
    
    [Header("UI Elements")]
    [SerializeField] private Transform buttonContainer;
    [SerializeField] private BuildButtonUI buttonPrefab;
    
    [SerializeField] private BuildCategory currentCategory;

    [SerializeField] private PlayerController player;
    [SerializeField] private PlayerStateMachine playerState;
    
    private readonly List<BuildButtonUI> _buttons = new();
    private BuildButtonUI _selectedButton;

    private void OnEnable()
    {
        ShowRooms();

        ResourceManager.Instance.OnResourceChanged += UpdateButtonStates;
        UpdateButtonStates(ResourceManager.Instance.Stone, ResourceManager.Instance.Wood);
    }

    private void Update()
    {
        buttonContainer.gameObject.SetActive(playerState.CurrentMode == PlayerMode.Build);
    }

    private void GenerateButtons(RoomData[] data)
    {
        foreach (Transform child in buttonContainer)
            Destroy(child.gameObject);
        
        _buttons.Clear();
        
        foreach (var room in data)
        {
            var button = Instantiate(buttonPrefab, buttonContainer);
            button.Initialize(room, this);
            
            _buttons.Add(button);
        }
    }

    private void UpdateButtonStates(int stone, int wood)
    {
        foreach (var button in _buttons)
        {
            var data = button.GetData();
            var canAfford = stone >= data.stoneCost && wood >= data.woodCost;
            
            button.SetInteractable(canAfford);
        }
    }

    public void SelectRoom(RoomData room)
    {
        player.SetSelectedRoom(room);

        foreach (var button in _buttons)
        {
            var isSelected = button.GetData() == room;
            button.SetSelected(isSelected);
            
            if (isSelected)
                _selectedButton = button;
        }
    }

    public void SetCategory(BuildCategory category)
    {
        currentCategory = category;

        var data = category switch
        {
            BuildCategory.Rooms => menuData.rooms,
            BuildCategory.Traps => menuData.traps,
            BuildCategory.Upgrades => menuData.upgrades,
            _ => null
        };
        
        GenerateButtons(data);
        
        UpdateButtonStates(ResourceManager.Instance.Stone, ResourceManager.Instance.Wood);
    }

    public void ShowRooms()
    {
        SetCategory(BuildCategory.Rooms);
    }
    
    public void ShowTraps()
    {
        SetCategory(BuildCategory.Traps);
    }
    
    public void ShowUpgrades()
    {
        SetCategory(BuildCategory.Upgrades);
    }
}