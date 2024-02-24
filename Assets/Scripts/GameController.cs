//////////////////////////////////////////////
//Assignment/Lab/Project: Virtual Pet
//Name: Brian Jernigan
//Section: SGD.213.2172
//Instructor: Brian Sowers
//Date: 02/26/2024
/////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("On-Screen Elements")]
    [SerializeField] private TMP_InputField _nameInputField;
    [SerializeField] private Button _adoptButton;
    [SerializeField] private Button _playAgainButton;
    [SerializeField] private GameObject _namePetText;
    [SerializeField] private GameObject _loseText;

    [Header("Sliders")] 
    [SerializeField] private Slider _hungerBarSlider;
    [SerializeField] private Slider _boredomBarSlider;
    [SerializeField] private Slider _fatigueBarSlider;

    [Header("Bar Fills")] 
    [SerializeField] private Image _hungerBarFill;
    [SerializeField] private Image _boredomBarFill;
    [SerializeField] private Image _fatigueBarFill;

    [Header("Buttons")] 
    [SerializeField] private Button _feedButton;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _restButton;

    private const float HungerPerSecond = 5f;
    private const float BoredomPerSecond = 6.25f;
    private const float FatiguePerSecond = 3.75f;
    
    private float MaxHungerValue { get; set; }
    private float MaxBoredomValue { get; set; }
    private float MaxFatigueValue { get; set; }

    private Pet _newPet;
    private bool _isAdopted;
    
    // Max values are set by slider components in inspector
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
        // Enables button when something is entered into the input field
        _nameInputField.onValueChanged.AddListener(delegate {UpdateButtonState();} );
    }

    private void Update()
    {
        if (!_isAdopted) return;
        
        UpdatePetNeeds();
        UpdateActionButtons();
        UpdateStatusBars();
        
        
        if (CheckForLoss())
        {
            OnLoss();
        }
    }

    private void UpdatePetNeeds()
    {
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
    }

    // Disables individual action buttons when status reaches max
    private void UpdateActionButtons()
    {
        _feedButton.interactable = !StatusHasReachedMax(_newPet.Hunger, MaxHungerValue);
        _playButton.interactable = !StatusHasReachedMax(_newPet.Boredom, MaxBoredomValue);
        _restButton.interactable = !StatusHasReachedMax(_newPet.Fatigue, MaxFatigueValue);
    }

    private bool StatusHasReachedMax(float currentValue, float maxValue)
    {
        return Math.Abs(currentValue - maxValue) < Mathf.Epsilon;
    }
    
    // Relates slider value with pet status value
    private void UpdateStatusBars()
    {
        _hungerBarSlider.value = _newPet.Hunger;
        _boredomBarSlider.value = _newPet.Boredom;
        _fatigueBarSlider.value = _newPet.Fatigue;

        UpdateAllBarsColors();
    }

    // Changes colors of all bars according to their current values
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
    
    // Player loses if all 3 statuses have reached their max value
    private bool CheckForLoss()
    {
        return Math.Abs(_newPet.Hunger - MaxHungerValue) < Mathf.Epsilon &&
                   Math.Abs(_newPet.Boredom - MaxBoredomValue) < Mathf.Epsilon &&
                   Math.Abs(_newPet.Fatigue - MaxFatigueValue) < Mathf.Epsilon;
    }

    private void OnLoss()
    {
        _adoptButton.gameObject.SetActive(false);
        _playAgainButton.gameObject.SetActive(true);
        _loseText.SetActive(true);
    }
    
    public void OnClickAdoptButton()
    {
        var petName = _nameInputField.text;
        _newPet = new Pet(petName);
        OnAdoption();
    }

    public void OnClickPlayAgainButton()
    {
        SceneManager.LoadScene("VirtualPet");
    }

    private void OnAdoption()
    {
        _isAdopted = true;
        _nameInputField.interactable = false;
        _adoptButton.interactable = false;
        _namePetText.SetActive(false);
    }
    
    public void OnClickFeedButton()
    {
        _newPet.Feed(25);
        StartCoroutine(DelayStatusIncrease(status => _newPet.IsFed = status, 2.0f));
    }

    public void OnClickPlayButton()
    {
        _newPet.Play(30);
        StartCoroutine(DelayStatusIncrease(status => _newPet.HasPlayed = status, 1.0f));
    }

    public void OnClickRestButton()
    {
        _newPet.Rest(35);
        StartCoroutine(DelayStatusIncrease(status => _newPet.IsRested = status, 1.5f));
    }
    
    // Delays increase in status value after their corresponding buttons are pressed
    // Pets aren't satisfied for very long
    private IEnumerator DelayStatusIncrease(Action<bool> setStatus, float delay)
    {
        setStatus(true);
        yield return new WaitForSeconds(delay);
        setStatus(false);
    }
}
