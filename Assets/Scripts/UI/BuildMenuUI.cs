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

        ResourceManager.Instance.OnResourcesChanged += UpdateButtonStates;
        UpdateButtonStates();
    }

    private void OnDisable()
    {
        if (ResourceManager.Instance)
            ResourceManager.Instance.OnResourcesChanged -= UpdateButtonStates;
    }

    private void Update()
    {
        buttonContainer.gameObject.SetActive(playerState.CurrentMode is PlayerMode.Build or PlayerMode.BuildTrap);
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

    private void GenerateTrapButtons(TrapData[] traps)
    {
        foreach (Transform child in buttonContainer)
            Destroy(child.gameObject);

        _buttons.Clear();

        foreach (var trap in traps)
        {
            var button = Instantiate(buttonPrefab, buttonContainer);
            button.Initialize(trap, this);

            _buttons.Add(button);
        }
    }

    private void UpdateButtonStates()
    {
        foreach (var button in _buttons)
        {
            var canAfford = ResourceManager.Instance.HasEnough(button.GetCost());
            button.SetInteractable(canAfford);
        }
    }

    public void SelectRoom(RoomData room)
    {
        player.SetSelectedRoom(room);

        foreach (var button in _buttons)
        {
            var isSelected = button.GetRoom() == room;
            button.SetSelected(isSelected);

            if (isSelected)
                _selectedButton = button;
        }
    }

    public void SelectTrap(TrapData trap)
    {
        player.SetSelectedTrap(trap);

        foreach (var button in _buttons)
        {
            var isSelected = button.GetTrap() == trap;
            button.SetSelected(isSelected);

            if (isSelected)
                _selectedButton = button;
        }
    }

    public void SetCategory(BuildCategory category)
    {
        currentCategory = category;

        switch (category)
        {
            case BuildCategory.Rooms:
                GenerateButtons(menuData.rooms);
                break;

            case BuildCategory.Traps:
                GenerateTrapButtons(menuData.traps);
                break;

            case BuildCategory.Upgrades:
                // Future
                break;
        }

        UpdateButtonStates();
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