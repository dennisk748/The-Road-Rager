using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] List<CharacterScriptableObject> charObjects;
     [SerializeField] TextMeshProUGUI charDescription;
     [SerializeField] TextMeshProUGUI charName;
     [SerializeField] TextMeshProUGUI charSpeed;
     [SerializeField] TextMeshProUGUI charStrength;
      [SerializeField] Image charImg;
    [SerializeField] int charIndex;

    public delegate void ShowSOEventHandler();
    public event ShowSOEventHandler OnShowSO;
    

    [SerializeField]private Button nextButton;
    [SerializeField]private Button prevButton;


    void Start()
    {
        OnShowSO += ShowSO;
        
        CheckButtonAvailability();
        OnShowSO?.Invoke();
        
    }

    public void NextCharacter()
    {
        if (charIndex < charObjects.Count - 1)
        {
            charIndex++;
            CheckButtonAvailability();
            OnShowSO?.Invoke();
        }
    }

    public void PreviousCharacter()
    {
        if (charIndex > 0)
        {
            charIndex--;
            CheckButtonAvailability();
            OnShowSO?.Invoke();
        }
    }


    public void SelectCharacter()
    {
        PlayerPrefs.SetInt("playerIndex", charIndex);
    }

    private void ShowSO()
    {
        charDescription.text = "<b>Description</b>\n"+ charObjects[charIndex].Description;
        charImg.sprite = charObjects[charIndex].Img;
        charName.text =charObjects[charIndex].Name;
        charSpeed.text = "<b>Speed</b>: "+charObjects[charIndex].Speed.ToString();
        charStrength.text = "<b>Strength</b>:"+charObjects[charIndex].Strength.ToString();
    }

    private void CheckButtonAvailability()
    {
        if (charIndex == 0)
        {
            prevButton.interactable = false;
            
        }
        else if (charIndex == charObjects.Count - 1)
        {
            nextButton.interactable = false;
            }
        else
        {
            prevButton.interactable = true;
            nextButton.interactable = true;
        }
    }
}
