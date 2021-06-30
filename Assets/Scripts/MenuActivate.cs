using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActivate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PublicBuf.PowerfulDisciple = "E";
        PublicBuf.ProhodProf = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ExitClicked()
    {
        Application.Quit();
    }
}
