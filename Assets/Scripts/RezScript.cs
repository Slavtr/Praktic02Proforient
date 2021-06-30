using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class RezScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject A = new GameObject();
        GameObject SpeckPrefab = Resources.Load("SpeckPref") as GameObject;
        GameObject ScalePrefab = Resources.Load("GameObject") as GameObject;
        GameObject C = GameObject.Find("ContentGraph");
        if (PublicBuf.ProhodProf && !PublicBuf.ProhodSpeck)
        {
            materialise_scales(A, ScalePrefab, C, PublicBuf.Prof);
            A = GameObject.Find("PredmProf");
            Predm_zap(PublicBuf.Prof, A);
            Water_zap(PublicBuf.Path, A.transform.GetComponent<Text>().text, "WaterProf");
            A = GameObject.Find("U");
            Texture tx = CreateDiag(PublicBuf.Prof[1]);
            A.transform.GetComponent<Image>().sprite = Sprite.Create((Texture2D)tx, new Rect(0f, 0f, tx.width, tx.height), Vector2.zero);
            LabelDiag(A, Resources.Load("DiagLabel") as GameObject, PublicBuf.Prof);
        }
        else if (PublicBuf.ProhodProf && PublicBuf.ProhodSpeck)
        {
            materialise_scales(A, ScalePrefab, C, PublicBuf.Speck);
            A = GameObject.Find("PredmProf");
            Predm_zap(PublicBuf.Prof, A);
            Water_zap(PublicBuf.Path, A.transform.GetComponent<Text>().text, "WaterProf");
            A = GameObject.Find("PredmSpeck");
            A.transform.rotation = new Quaternion(0, 0, 0, 0);
            Predm_zap(PublicBuf.Speck, A);
            Water_zap(PublicBuf.Path, A.transform.GetComponent<Text>().text, "WaterSpeck");
            A = GameObject.Find("WaterSpeck");
            A.transform.rotation = new Quaternion(0, 0, 0, 0);
            A = GameObject.Find("U");
            Texture tx = CreateDiag(PublicBuf.Speck[1]);
            A.transform.GetComponent<Image>().sprite = Sprite.Create((Texture2D)tx, new Rect(0f, 0f, tx.width, tx.height), Vector2.zero);
            LabelDiag(A, Resources.Load("DiagLabel") as GameObject, PublicBuf.Speck);
        }
        C = GameObject.Find("ContentSpeckThem");
        materialise_species(SpeckPrefab, C, PublicBuf.Species);
        C = GameObject.Find("ContentSpeckUs");
        materialise_species(SpeckPrefab, C, PublicBuf.UrSpecies);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void materialise_scales(GameObject A, GameObject B, GameObject C, List<List<string>> ls)
    {
        int i = 0;
        foreach (string st in ls[0])
        {
            A = Instantiate(B);
            A.transform.SetParent(C.transform, false);
            int bf = Convert.ToInt32(ls[1][i]);
            if (bf > 0)
            {
                for (int j = 0; j < bf; j++)
                {
                    A.transform.GetChild(11 + j).GetComponent<Image>().color = Color.green;
                }
            }
            else
            {
                for (int j = 0; j > bf; j--)
                {
                    A.transform.GetChild(10 + j).GetComponent<Image>().color = Color.red;
                }
            }
            A.transform.GetChild(0).GetComponent<Text>().text = st;
            i++;
        }
    }
    void materialise_species(GameObject B, GameObject C, List<string> ls)
    {
        int i = 0;
        foreach (string st in ls)
        {
            if (st != "")
            {
                GameObject A = Instantiate(B);
                A.transform.SetParent(C.transform, false);
                A.transform.GetChild(0).GetComponent<Text>().text = st;
                A.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(st);
                i++;
            }
        }
    }
    void Predm_zap(List<List<string>> ls, GameObject A)
    {
        int max = -1000, maxind = -1000;
        List<int> maxcount = new List<int>();
        foreach (string st in ls[1])
        {
            if (Convert.ToInt32(st) >= max)
            {
                max = Convert.ToInt32(st);
                maxind = ls[1].IndexOf(st);
            }
        }
        string ret = "";
        for (int i = 0; i < ls[1].Count; i++) 
        {
            if(Convert.ToInt32(ls[1][i])==max)
            {
                maxcount.Add(i);
            }
        }
        foreach(int sn in maxcount)
        {
            ret += ls[0][sn] + ";\t";
        }
        A.GetComponent<Text>().text = ret;
    }
    void Water_zap(string path, string name, string water)
    {
        GameObject A = GameObject.Find(water);
        char[] chars = { ';', '\t' };
        string[] st = name.Split(chars);
        if(st.Length>=1)
        {
            string ret = "";
            foreach(string st1 in st)
            {
                if (st1 != "" && st1 != " ")
                    ret += GetText.GetTextAbout(st1, path) + "\n";
            }
            A.GetComponent<Text>().text = ret;
        }
        else A.GetComponent<Text>().text = GetText.GetTextAbout(name, path);
    }
    public void ExitClicked()
    {
        PublicBuf.Species = new List<string>();
        SceneManager.LoadScene("MainMenu");
    }
    Texture2D CreateDiag(List<string> CirclEl)
    {
        Texture2D Diag = new Texture2D(1500, 1500);
        for (int i = 0; i <= 1500; i++)
            for (int j = 0; j <= 1500; j++)
                Diag.SetPixel(i, j, Color.clear);
        int ElCount = CirclEl.Count;
        double alf = 360 / ElCount, ugl = 0;
        int R = Diag.width / 2 - 10;
        int xc = Diag.width / 2, yc = Diag.height / 2;
        Color cl = Color.blue;
        for (int i = 1; i < ElCount; i++)
        {
            if (Convert.ToInt32(CirclEl[i]) >= 0)
            {
                for (int j = 1; j <= (R / 10 * Convert.ToInt32(CirclEl[i - 1])); j++)
                    Brezenham((int)(xc + j * Math.Cos(ugl)), (int)(yc + j * Math.Sin(ugl)), (int)(xc + (R / 10 * Convert.ToInt32(CirclEl[i])) * Math.Cos(ugl + alf)), (int)(yc + (R / 10 * Convert.ToInt32(CirclEl[i])) * Math.Sin(ugl + alf)), Diag, cl);
                for (int j = 1; j <= (R / 10 * Convert.ToInt32(CirclEl[i])); j++)
                    Brezenham((int)(xc + (R / 10 * Convert.ToInt32(CirclEl[i - 1])) * Math.Cos(ugl)), (int)(yc + (R / 10 * Convert.ToInt32(CirclEl[i - 1])) * Math.Sin(ugl)), (int)(xc + j * Math.Cos(ugl + alf)), (int)(yc + (j * Math.Sin(ugl + alf))), Diag, cl);
            }
            ugl += alf;
        }
        cl = Color.black;
        Brezenham(xc, 0, xc, Diag.height, Diag, cl);
        Brezenham(0, yc, Diag.width, yc, Diag, cl);
        Brezenham(1, yc + 1, Diag.width, yc + 1, Diag, cl);
        Diag.Apply();
        return Diag;
    }
    void Brezenham(int x0, int y0, int x1, int y1, Texture2D Diag, Color clr)
    {
        //Изменения координат
        int dx = (x1 > x0) ? (x1 - x0) : (x0 - x1);
        int dy = (y1 > y0) ? (y1 - y0) : (y0 - y1);
        //Направление приращения
        int sx = (x1 >= x0) ? (1) : (-1);
        int sy = (y1 >= y0) ? (1) : (-1);

        if (dy < dx)
        {
            int d = (dy << 1) - dx;
            int d1 = dy << 1;
            int d2 = (dy - dx) << 1;
            Diag.SetPixel(x0, y0, clr);
            int x = x0 + sx;
            int y = y0;
            for (int i = 1; i <= dx; i++)
            {
                if (d > 0)
                {
                    d += d2;
                    y += sy;
                }
                else
                    d += d1;
                Diag.SetPixel(x, y, clr);
                x += sx;
            }
        }
        else
        {
            int d = (dx << 1) - dy;
            int d1 = dx << 1;
            int d2 = (dx - dy) << 1;
            Diag.SetPixel(x0, y0, clr);
            int x = x0;
            int y = y0 + sy;
            for (int i = 1; i <= dy; i++)
            {
                if (d > 0)
                {
                    d += d2;
                    x += sx;
                }
                else
                    d += d1;
                Diag.SetPixel(x, y, clr);
                y += sy;
            }
        }
    }
    void LabelDiag(GameObject Diag, GameObject Prefab, List<List<string>> ls)
    {
        float alf = 360 / ls[0].Count;
        float ugl = 0;
        float r = Diag.GetComponent<RectTransform>().rect.width / 2 - 10;
        for (int i = 0; i < ls[1].Count; i++)
        {
            GameObject A = Instantiate<GameObject>(Prefab, Diag.transform);
            A.transform.SetParent(Diag.transform, false);
            A.transform.localPosition = new Vector3(x: -((float)(r * Math.Cos(ugl))), y: -((float)(r * Math.Sin(ugl))));
            A.transform.GetChild(0).GetComponent<Text>().text = ls[0][i];
            ugl += alf;
        }
    }
}
static class GetText
{
    static public string GetTextAbout(string name, string path)
    {
        TextAsset QuestsTxt = (TextAsset)Resources.Load(path);
        string VEsFail = QuestsTxt.text;
        char[] chars = { '\r', '\n' };
        string[] line = VEsFail.Split(chars);
        foreach (string st in line)
        {
            if (st != "")
            {
                var data = st.Split(';');
                if (data[0] == name)
                {
                    PublicBuf.Species.AddRange(data[2].Split(','));
                    PublicBuf.UrSpecies.AddRange(data[3].Split(','));
                    return data[1];
                }
            }
        }
        return "Нет информации...";
    }
}
