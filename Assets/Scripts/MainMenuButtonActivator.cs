using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtonActivator : MonoBehaviour
{
    GameObject Speckbutton;
    GameObject ProfButton;
    // Start is called before the first frame update
    void Start()
    {
        Speckbutton = GameObject.Find("Speck");
        ProfButton = GameObject.Find("Prof");
        if (PublicBuf.ProhodProf == false)
        {
            Speckbutton.GetComponent<Button>().interactable = false;
        }
        else
        {
            ProfButton.GetComponent<Button>().interactable = false;
            Speckbutton.GetComponent<Button>().interactable = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ProfileTestClick()
    {
        PublicBuf.ProhodProf = true;
        SceneManager.LoadScene("SceneQuests");
    }
    public void SpeckTestClick()
    {
        PublicBuf.ProhodSpeck = true;
        ProfButton.GetComponent<Button>().interactable = false;
        SceneManager.LoadScene("SceneQuests");
    }
    public void ExitClicked()
    {
        Application.Quit();
    }
    public void ClearClicked()
    {
        Speckbutton.GetComponent<Button>().interactable = false;
        ProfButton.GetComponent<Button>().interactable = true;
        PublicBuf.ProhodProf = false;
        PublicBuf.PowerfulDisciple = "E";
        PublicBuf.ProfFile = "Prof";
        PublicBuf.SpeckFile = "Speck";
        PublicBuf.Prof = new List<List<string>>();
        PublicBuf.Speck = new List<List<string>>();
        PublicBuf.Path = "Water";
        PublicBuf.ProhodProf = false;
        PublicBuf.ProhodSpeck = false;
        PublicBuf.PowerfulDisciple = "E";
        PublicBuf.ProfFile = "Prof";
        PublicBuf.SpeckFile = "Speck";
        PublicBuf.Species = new List<string>();
        PublicBuf.Prof = new List<List<string>>();
        PublicBuf.Speck = new List<List<string>>();
    }
    public void ResultClicked()
    {
        SceneManager.LoadScene("RezultScene");
    }
}
