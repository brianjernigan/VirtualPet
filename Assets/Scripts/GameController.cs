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
    private const int MaxStatusValue = 100;
    
    private Pet _newPet;

    private bool _petAdopted;

    private void Start()
    {
        UpdateButtonState();
        _nameInputField.onValueChanged.AddListener(delegate {UpdateButtonState();} );
    }

    private void Update()
    {
        if (_petAdopted)
        {
            IncrementPetStatus();
            Debug.Log(_newPet.Hunger);
        }
    }
    
    private void UpdateButtonState()
    {
        _adoptButton.interactable = !string.IsNullOrEmpty(_nameInputField.text);
    }
    
    public void OnClickAdoptButton()
    {
        var petName = _nameInputField.text;
        _newPet = new Pet(petName);
        _petAdopted = true;
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
        if (_newPet.Hunger + (HungerPerSecond * Time.deltaTime) <= MaxStatusValue)
        {
            _newPet.Hunger += HungerPerSecond * Time.deltaTime;
        }

        if (_newPet.Boredom + (BoredomPerSecond * Time.deltaTime) <= MaxStatusValue)
        {
            _newPet.Boredom += BoredomPerSecond * Time.deltaTime;
        }

        if (_newPet.Fatigue + (FatiguePerSecond * Time.deltaTime) <= MaxStatusValue)
        {
            _newPet.Fatigue += FatiguePerSecond * Time.deltaTime;
        }
        
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
