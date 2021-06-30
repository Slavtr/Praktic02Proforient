using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GetSpeck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
    public void InfoClickHolder()
    {
        GameObject go = transform.parent.gameObject;
        PublicBuf.CurSpeck = go.transform.GetChild(0).GetComponent<Text>().text;
        SceneManager.LoadScene("SpeckInfoScene");
    }
}
