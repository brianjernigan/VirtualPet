using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private TMP_InputField _nameInputField;
    [SerializeField] private Button _adoptButton;
    [SerializeField] private GameObject _namingText;

    [Header("Sliders")] 
    [SerializeField] private Slider _hungerBarSlider;
    [SerializeField] private Slider _boredomBarSlider;
    [SerializeField] private Slider _fatigueBarSlider;

    private const int HungerPerSecond = 10;
    private const int BoredomPerSecond = 10;
    private const int FatiguePerSecond = 10;
    
    private float MaxHungerValue { get; set; }
    private float MaxBoredomValue { get; set; }
    private float MaxFatigueValue { get; set; }

    private Pet _newPet;

    private bool _isAdopted;

    private void SetMaxBarValues()
    {
        MaxHungerValue = _hungerBarSlider.maxValue;
        MaxBoredomValue = _hungerBarSlider.maxValue;
        MaxFatigueValue = _hungerBarSlider.maxValue;
    }
    
    private void UpdateButtonState()
    {
        _adoptButton.interactable = !string.IsNullOrEmpty(_nameInputField.text);
    }
    
    private void Start()
    {
        SetMaxBarValues();
        UpdateButtonState();
        _nameInputField.onValueChanged.AddListener(delegate {UpdateButtonState();} );
    }

    private void Update()
    {
        if (!_isAdopted) return;
        IncreasePetStatuses();
        Debug.Log(_newPet);
    }
    
    public void OnClickAdoptButton()
    {
        var petName = _nameInputField.text;
        _newPet = new Pet(petName);
        _isAdopted = true;
        _nameInputField.interactable = false;
        _namingText.SetActive(false);
    }

    private void IncreasePetStatuses()
    {
        _newPet.IncreaseHunger(HungerPerSecond, MaxHungerValue);
        _newPet.IncreaseBoredom(BoredomPerSecond, MaxBoredomValue);
        _newPet.IncreaseFatigue(FatiguePerSecond, MaxFatigueValue);

        UpdateStatusBars();
    }
    
    private void UpdateStatusBars()
    {
        _hungerBarSlider.value = _newPet.Hunger;
        _boredomBarSlider.value = _newPet.Boredom;
        _fatigueBarSlider.value = _newPet.Fatigue;
    }

    public void OnClickFeedButton()
    {
        _newPet.Feed(10);
    }

    public void OnClickPlayButton()
    {
        _newPet.Play(10);
    }

    public void OnClickRestButton()
    {
        _newPet.Rest(10);
    }
}
