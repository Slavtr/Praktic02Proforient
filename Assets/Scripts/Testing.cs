using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Testing
{
    public List<Test> Quests = new List<Test>();
    public List<List<string>> Result = new List<List<string>>();
    public void GetQuests(string path)
    {
        TextAsset QuestsTxt = (TextAsset)Resources.Load(path);
        string VEsFail = QuestsTxt.text;
        char[] chars = { '\r', '\n' };
        string[] line = VEsFail.Split(chars);
        foreach (string st in line)
        {
            if (st != "")
            {
                string[] data = st.Split(';');
                if (data[2] == "")
                {
                    Quests.Add(new Test(data[0], data[1], 0, null));
                }
                else
                {
                    Quests.Add(new Test(data[0], data[1], 0, data[2]));
                }
            }
        }
    }
    public void GetQuests(string path, string bestResult)
    {
        TextAsset QuestsTxt = (TextAsset)Resources.Load(path);
        string VEsFail = QuestsTxt.text;
        char[] chars = { '\r', '\n' };
        string[] line = VEsFail.Split(chars);
        foreach (string st in line)
        {
            if (st != "")
            {
                string[] data = st.Split(';');
                if (data[2] == bestResult)
                {
                    if (data[3] == "")
                    {
                        Quests.Add(new Test(data[0], data[1], 0, null));
                    }
                    else
                    {
                        Quests.Add(new Test(data[0], data[1], 0, data[3]));
                    }
                }
            }
        }
    }

    /*
     * Изменят результатирующую матрицу Result, которая состоит и двух массивов
     * 1-Массив направлений
     * 2-Массив подсчёта результатов по прохождению вопросов
     * 
     * {
     * {Биология, Математика}
     * {-8, 5}
     * }
     */
    public void GetResultData()
    {
        Result.Add(new List<string>());
        Result.Add(new List<string>());
        foreach (Test ts in Quests)
        {
            if (!Result[0].Contains(ts.Way))
            {
                Result[0].Add(ts.Way);
            }
        }
        for (int i = 0; i < Result[0].Count; i++)
        {
            Result[1].Add("0");
            foreach (Test ts in Quests.Where(x => x.Way == Result[0][i]))
            {
                Result[1][i] = Convert.ToString(Convert.ToInt32(Result[1][i]) + ts.Weight);
            }
        }
    }
    public string GetBestResult()
    {
        int index = 0;
        int max_weight = 0;
        for (int i = 0; i < Result[1].Count; i++)
        {
            if (Convert.ToInt32(Result[1][i]) > max_weight)
            {
                max_weight = Convert.ToInt32(Result[1][i]);
                index = i;
            }
        }
        return Result[0][index];
    }
    /*
     * Очищение результатов работает только для первой перегрузки GetQuests, 
     * поскольку вторая перегрузка выбирает вопросы на этапе загрузки в лист Quest.
     */
    public void ClearResult()
    {

        for (int i = 0; i < Quests.Count; i++)
        {
            Quests[i] = new Test(Quests[i].Quest, Quests[i].Way, 0, Quests[i].Photo);
        }

        for (int i = 0; i < Result[1].Count; i++)
        {
            Result[1][i] = "0";
        }
    }
}
public class Test
{
    public string Quest { get; set; }
    public int Weight { get; set; }
    public string Way { get; set; }
    public string Photo { get; set; }
    public Test(string Quest, string Way, int Weight, string Photo)
    {
        this.Quest = Quest;
        this.Way = Way;
        this.Weight = Weight;
        this.Photo = Photo;
    }
}
