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

    [Header("Bars")] 
    [SerializeField] private GameObject _hungerBar;
    [SerializeField] private GameObject _boredomBar;
    [SerializeField] private GameObject _fatigueBar;

    private const int HungerPerSecond = 10;
    private const int BoredomPerSecond = 10;
    private const int FatiguePerSecond = 10;
    
    private float MaxHungerValue { get; set; }
    private float MaxBoredomValue { get; set; }
    private float MaxFatigueValue { get; set; }

    private Pet _newPet;

    private bool _isAdopted;

    private void Start()
    {
        SetMaxBarValues();
        UpdateButtonState();
        _nameInputField.onValueChanged.AddListener(delegate {UpdateButtonState();} );
    }

    private void Update()
    {
        if (!_isAdopted) return;
        IncrementPetStatus();
        Debug.Log(_newPet);
    }

    private void SetMaxBarValues()
    {
        MaxHungerValue = _hungerBar.GetComponent<Slider>().maxValue;
        MaxBoredomValue = _boredomBar.GetComponent<Slider>().maxValue;
        MaxFatigueValue = _fatigueBar.GetComponent<Slider>().maxValue;
    }
    
    private void UpdateButtonState()
    {
        _adoptButton.interactable = !string.IsNullOrEmpty(_nameInputField.text);
    }
    
    public void OnClickAdoptButton()
    {
        var petName = _nameInputField.text;
        _newPet = new Pet(petName);
        _isAdopted = true;
        _nameInputField.interactable = false;
        _namingText.SetActive(false);
        Debug.Log(_newPet.ToString());
    }

    private void UpdateBars()
    {
        _hungerBar.GetComponent<Slider>().value = _newPet.Hunger;
        _boredomBar.GetComponent<Slider>().value = _newPet.Boredom;
        _fatigueBar.GetComponent<Slider>().value = _newPet.Fatigue;
    }

    private void IncrementPetStatus()
    {
        _newPet.GainHunger(HungerPerSecond, MaxHungerValue);
        _newPet.GainBoredom(BoredomPerSecond, MaxBoredomValue);
        _newPet.GainFatigue(FatiguePerSecond, MaxFatigueValue);

        UpdateBars();
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
