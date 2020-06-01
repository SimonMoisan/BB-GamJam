using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    [Header("Associated objects :")]
    public GameLoop gameLoop;
    public GameManager gameManager;

    //Gordon
    public int moodIndex;
    public Image gordonActualImage;
    public Image[] gordonImages;

    void OnValidate()
    {
        gameLoop = FindObjectOfType<GameLoop>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
