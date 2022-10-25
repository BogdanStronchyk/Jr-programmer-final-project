using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class UIManager : MonoBehaviour
{

    public TMP_InputField InputField;
    //public TextMeshProUGUI BestScoreText;
    public GameObject NoNameText;

    private void Start()
    {
        DataHandler.Instance.Load();
        //BestScoreText.text = $"Best score: {DataHandler.Instance.Name} : {DataHandler.Instance.BestScore}";
    }

    public void StartNew()
    {
        if (InputField.text.Length > 0)
        {
            DataHandler.Instance.Name = InputField.text;

            SceneManager.LoadScene(1);
        }
        else
        {
            NoNameText.SetActive(true);
            StartCoroutine(Timer(3));
        }

    }

    public void ToScoreTable()
    {
        SceneManager.LoadScene(2);
    }

    IEnumerator Timer(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        NoNameText.SetActive(false);
    }


    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif 
    }
}
