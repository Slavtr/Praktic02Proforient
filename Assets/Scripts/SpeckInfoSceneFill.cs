using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpeckInfoSceneFill : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TextAsset SpeckText = Resources.Load<TextAsset>("SpeckInfo");
        string VEsFail = SpeckText.text;
        char[] chars = { '\r', '\n' };
        string[] line = VEsFail.Split(chars);
        foreach (string st in line)
        {
            if (st != "")
            {
                string[] st1 = st.Split(';');
                if (st1[0] == PublicBuf.CurSpeck)
                {
                    GameObject A = GameObject.Find("Canvas");
                    A.transform.GetChild(3).transform.GetComponent<Text>().text = st1[0];
                    A.transform.GetChild(2).transform.GetComponent<Text>().text = st1[1];
                    A.transform.GetChild(1).transform.GetComponent<Image>().sprite = Resources.Load<Sprite>(st1[0]);
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void Back_clicked()
    {
        PublicBuf.UrSpecies = new List<string>();
        PublicBuf.Species = new List<string>();
        SceneManager.LoadScene("RezultScene");
    }
}
