using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HintManager : MonoBehaviour
{
    public TextMeshProUGUI[] textMeshProUGUIs;
    public TextMeshPro hint1;
    public TextMeshPro hint2;
    public TextMeshPro hint3;
    public TextMeshPro hint4;

    bool sound = false;
    // Start is called before the first frame update
    void Start()
    {
        hint1.enabled = false;
        hint2.enabled = false;
        hint3.enabled = false;
        hint4.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < textMeshProUGUIs.Length; i++)
        {
            if (textMeshProUGUIs[i].color == Color.green)
            {
                if (textMeshProUGUIs[i].name.Contains("Weapons"))
                {
                    hint1.text = "B";
                    hint1.enabled = true;
                }
                else if (textMeshProUGUIs[i].name.Contains("Admin"))
                {
                    hint2.text = "L";
                    hint2.enabled = true;
                }
                else if (textMeshProUGUIs[i].name.Contains("KillBug"))
                {
                    hint3.text = "U";
                    hint3.enabled = true;
                }
                else if (textMeshProUGUIs[i].name.Contains("Carry"))
                {
                    hint4.text = "E";
                    hint4.enabled = true;
                }
            }
        }

        //// TODO : 힌트 다 나오면 scene 이동
        //if (hint1.text == "B" && hint2.text == "L")
        //{
        //    StartCoroutine(NextScene());
        //}
    }

    IEnumerator NextScene()
    { 
        yield return new WaitForSeconds(1);
    }
}
