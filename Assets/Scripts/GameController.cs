//////////////////////////////////////////////
//Assignment/Lab/Project: Virtual Pet
//Name: Brian Jernigan
//Section: SGD.213.2172
//Instructor: Brian Sowers
//Date: 02/26/2024
/////////////////////////////////////////////

// All values based on my own experience owning a puppy lol

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("On-Screen Elements")]
    [SerializeField] private TMP_InputField _nameInputField;
    [SerializeField] private Button _adoptButton;
    [SerializeField] private GameObject _namePetText;

    [Header("Sliders")] 
    [SerializeField] private Slider _hungerBarSlider;
    [SerializeField] private Slider _boredomBarSlider;
    [SerializeField] private Slider _fatigueBarSlider;

    [Header("Bar Fills")] 
    [SerializeField] private Image _hungerBarFill;
    [SerializeField] private Image _boredomBarFill;
    [SerializeField] private Image _fatigueBarFill;

    private const float HungerPerSecond = 5f;
    private const float BoredomPerSecond = 7.5f;
    private const float FatiguePerSecond = 2.5f;
    
    private float MaxHungerValue { get; set; }
    private float MaxBoredomValue { get; set; }
    private float MaxFatigueValue { get; set; }

    private Pet _newPet;
    private bool _isAdopted;

    private void SetMaxBarValues()
    {
        MaxHungerValue = _hungerBarSlider.maxValue;
        MaxBoredomValue = _boredomBarSlider.maxValue;
        MaxFatigueValue = _fatigueBarSlider.maxValue;
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
        
        if (!_newPet.IsFed)
        {
            _newPet.IncreaseHunger(HungerPerSecond, MaxHungerValue);
        }

        if (!_newPet.HasPlayed)
        {
            _newPet.IncreaseBoredom(BoredomPerSecond, MaxBoredomValue);
        }

        if (!_newPet.IsRested)
        {
            _newPet.IncreaseFatigue(FatiguePerSecond, MaxFatigueValue);
        }

        UpdateStatusBars();
    }
    
    public void OnClickAdoptButton()
    {
        var petName = _nameInputField.text;
        _newPet = new Pet(petName);
        OnAdoption();
    }

    private void OnAdoption()
    {
        _isAdopted = true;
        _nameInputField.interactable = false;
        _adoptButton.interactable = false;
        _namePetText.SetActive(false);
    }
    
    private void UpdateStatusBars()
    {
        _hungerBarSlider.value = _newPet.Hunger;
        _boredomBarSlider.value = _newPet.Boredom;
        _fatigueBarSlider.value = _newPet.Fatigue;

        UpdateAllBarsColors();
    }

    private void UpdateAllBarsColors()
    {
        UpdateBarColor(_hungerBarSlider.value, _hungerBarFill);
        UpdateBarColor(_boredomBarSlider.value, _boredomBarFill);
        UpdateBarColor(_fatigueBarSlider.value, _fatigueBarFill);
    }

    private void UpdateBarColor(float statusValue, Image barFill)
    {
        barFill.color = statusValue switch
        {
            <= 33.3f => Color.green,
            <= 66.6f => Color.yellow,
            _ => Color.red
        };
    }

    public void OnClickFeedButton()
    {
        _newPet.Feed(25);
        StartCoroutine(DelayHunger());
    }

    private IEnumerator DelayHunger()
    {
        _newPet.IsFed = true;
        yield return new WaitForSeconds(2.0f);
        _newPet.IsFed = false;
    }

    public void OnClickPlayButton()
    {
        _newPet.Play(30);
        StartCoroutine(DelayBoredom());
    }
    
    private IEnumerator DelayBoredom()
    {
        _newPet.HasPlayed = true;
        yield return new WaitForSeconds(1.0f);
        _newPet.HasPlayed = false;
    }

    public void OnClickRestButton()
    {
        _newPet.Rest(35);
        StartCoroutine(DelayFatigue());
    }
    
    private IEnumerator DelayFatigue()
    {
        _newPet.IsRested = true;
        yield return new WaitForSeconds(1.5f);
        _newPet.IsRested = false;
    }
}
