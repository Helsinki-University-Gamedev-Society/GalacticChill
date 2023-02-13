using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public int gameStartScene = 1;
    public Slider volumeSlider;
       
    public void StartGame()
    {
        SceneManager.LoadScene(gameStartScene);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void SetVolume()
    {
        PlayerPrefs.SetFloat("volume",volumeSlider.value / 100f);
    }
    
    public void ContinueGame()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene(gameStartScene);
    }
}
