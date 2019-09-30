using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuView : MonoBehaviour
{
    public static MenuView Instance { get; set; }

    public Button PlayButton;
    
    private void Awake()
    {
        Instance = this;
        
        PlayButton.onClick.AddListener(OnPlayButtonClick);
    }

    private void OnPlayButtonClick()
    {
        
    }
}
