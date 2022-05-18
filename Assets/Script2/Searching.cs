using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Searching : MonoBehaviour
{
    public Text searching;

    private void Start()
    {
           
    }

    public void OpenUrl()
    {
        Application.OpenURL($"https://search.naver.com/search.naver?where=nexearch&sm=top_hty&fbm=1&ie=utf8&query={searching.text}");
    }

    public void IsImposter()
    {
        if (searching.text == "BLUE" || searching.text == "ÆÄ¶û")
        {
            SceneManager.LoadScene("Victory");
        } 
        else
        {
            SceneManager.LoadScene("Kill");
        }
    }
}
