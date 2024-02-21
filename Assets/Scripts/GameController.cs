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

    private Pet _newPet;

    private void Start()
    {
        UpdateButtonState();
        _nameInputField.onValueChanged.AddListener(delegate {UpdateButtonState();} );
    }

    public void OnClickAdoptButton()
    {
        var petName = _nameInputField.text;
        _newPet = new Pet(petName);
        _nameInputField.interactable = false;
        _namingText.SetActive(false);
        Debug.Log(_newPet.ToString());
    }

    private void UpdateButtonState()
    {
        _adoptButton.interactable = !string.IsNullOrEmpty(_nameInputField.text);
    }
}
