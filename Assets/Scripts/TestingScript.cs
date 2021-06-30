using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TestingScript : MonoBehaviour
{
    //GameObject square;
    GameObject text;
    int QuestionsCount = 0;
    Testing ts;
    // Start is called before the first frame update
    void Start()
    {
        if (PublicBuf.PowerfulDisciple == "E")
        {
            ts = new Testing();
            ts.GetQuests(PublicBuf.ProfFile);
        }
        else if (PublicBuf.PowerfulDisciple != "E")
        {
            ts = new Testing();
            ts.GetQuests(PublicBuf.SpeckFile, PublicBuf.PowerfulDisciple);
        }
        text = GameObject.Find("MainText");
        LoadQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        if (QuestionsCount != 0 && QuestionsCount >= ts.Quests.Count)
        {
            try
            {
                text.GetComponent<Text>().text = "Тест завершён";
                GameObject go = GameObject.Find("ExitButton");
                go.transform.rotation = new Quaternion(0, 0, 0, 0);
                go = GameObject.Find("Somn");
                go.GetComponent<Button>().interactable = false;
                go = GameObject.Find("No");
                go.GetComponent<Button>().interactable = false;
                go = GameObject.Find("Yes");
                go.GetComponent<Button>().interactable = false;
                go = GameObject.Find("NNo");
                go.GetComponent<Button>().interactable = false;
                go = GameObject.Find("NYes");
                go.GetComponent<Button>().interactable = false;
                go = GameObject.Find("Back");
                go.GetComponent<Button>().interactable = false;
            }
            catch { }
        }
    }
    void LoadQuestion()
    {
        text.GetComponent<Text>().text = ts.Quests[QuestionsCount].Quest;
    }
    public void NoClicked()
    {
        ts.Quests[QuestionsCount].Weight = -2;
        QuestionsCount++;
        LoadQuestion();
    }
    public void NNoClicked()
    {
        ts.Quests[QuestionsCount].Weight = -1;
        QuestionsCount++;
        LoadQuestion();
    }
    public void SomnenieClicked()
    {
        ts.Quests[QuestionsCount].Weight = 0;
        QuestionsCount++;
        LoadQuestion();
    }
    public void NYesClicked()
    {
        ts.Quests[QuestionsCount].Weight = 1;
        QuestionsCount++;
        LoadQuestion();
    }
    public void YesClicked()
    {
        ts.Quests[QuestionsCount].Weight = 2;
        QuestionsCount++;
        LoadQuestion();
    }
    public void BackClicked()
    {
        if (QuestionsCount > 0)
            QuestionsCount--;
        else QuestionsCount = 0;
        LoadQuestion();
    }
    public void BackToScene()
    {
        ts.GetResultData();
        if (PublicBuf.ProhodProf == true && PublicBuf.ProhodSpeck == false)
        {
            PublicBuf.PowerfulDisciple = ts.GetBestResult();
            PublicBuf.Prof = ts.Result;
        }
        else if (PublicBuf.ProhodSpeck == true && PublicBuf.ProhodProf == true) PublicBuf.Speck = ts.Result;
        SceneManager.LoadScene("MainMenu");
    }
}
