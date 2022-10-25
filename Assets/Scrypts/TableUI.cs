using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class TableUI : MonoBehaviour
{
    public GameObject canvas;
    public TextMeshProUGUI PlaceInListTemplate;
    public GameObject Buttons;

    
    void Start()
    {
        DataHandler.Instance.Load();
        List<DataHandler.SaveData> scoreData = DataHandler.Instance.ScoreList;
        int len = scoreData.Count;
        int i;
        for (i = 0; i < len; i++)
        {
            TextMeshProUGUI PlaceInListClone = Instantiate(PlaceInListTemplate);

            PlaceInListClone.transform.SetParent(canvas.transform, false);
            Vector3 TemplatePosition = PlaceInListTemplate.GetComponent<RectTransform>().position;
            TemplatePosition.y += 60 - (60 * i);
            PlaceInListClone.GetComponent<RectTransform>().position = TemplatePosition;
            PlaceInListClone.text = $"{i + 1}. {scoreData[i].PlayerName}: {scoreData[i].HighScore}";
            PlaceInListClone.gameObject.SetActive(true);
        }
    }

    public void ResetScore()
    {
        DataHandler.Instance.ResetScore();
        SceneManager.LoadScene(2);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
