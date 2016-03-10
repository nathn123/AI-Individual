using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Controller : MonoBehaviour {

    public List<List<GameObject>> Panels;
    public GameObject PanelPrefab;
    public Canvas Base;
    public List<int> UsedValues;
    public AIBase Ai;
    public enum AiTypes
    {
        BreadthFirstSearch,
        AstarManhattan,
        AstarPattern
    };
	// Use this for initialization
	void Start () {

        Ai = new BFS();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Generate9()
    {
        if (Panels == null)
            Panels = new List<List<GameObject>>();
        foreach (var row in Panels)
        {
            foreach (var p in row)
            {
                Destroy(p);
            }
        }
        Panels.Clear();
		var centre = Base.pixelRect.center;
		var height = PanelPrefab.GetComponent<RectTransform>().rect.height;
		var width = PanelPrefab.GetComponent<RectTransform>().rect.width;
		
		for (int i = -1; i < 2; ++i)
		{
            List<GameObject> rows = new List<GameObject>();
			for (int j = -1; j < 2; ++j)
			{
				var position = new Vector3(centre.x + i * width, centre.y + j * height);
                rows.Add(GameObject.Instantiate(PanelPrefab, position, Quaternion.identity) as GameObject);
				int val;
				do
				{
					val = Random.Range(0, 9);
					
				} while (UsedValues.Contains(val));
				UsedValues.Add(val);

                if (val != 0)
                    rows[j + 1].GetComponent<Text>().text = val.ToString();
                else
                    rows[j + 1].GetComponent<Text>().text = "";

                rows[j+1].GetComponent<Text>().color = Color.green;
                rows[j+1].transform.SetParent(Base.transform);
			}
            Panels.Add(rows);
		}
		UsedValues.Clear ();
	}
	public void Generate15()
	{
        if (Panels == null)
            Panels = new List<List<GameObject>>();
        foreach (var row in Panels)
        {
            foreach (var p in row)
            {
                Destroy(p);
            }
        }
        Panels.Clear();
        var centre = Base.pixelRect.center;
        var height = PanelPrefab.GetComponent<RectTransform>().rect.height;
        var width = PanelPrefab.GetComponent<RectTransform>().rect.width;

        for (int i = -1; i < 3; ++i)
        {
            List<GameObject> rows = new List<GameObject>();
            for (int j = -1; j < 3; ++j)
            {
                var position = new Vector3(centre.x + i * width, centre.y + j * height);
                rows.Add(GameObject.Instantiate(PanelPrefab, position, Quaternion.identity) as GameObject);
                int val;
                do
                {
                    val = Random.Range(0, 16);

                } while (UsedValues.Contains(val));
                UsedValues.Add(val);

                if (val != 0)
                    rows[j + 1].GetComponent<Text>().text = val.ToString();
                else
                    rows[j + 1].GetComponent<Text>().text = "";

                rows[j + 1].GetComponent<Text>().color = Color.green;
                rows[j + 1].transform.SetParent(Base.transform);
            }
            Panels.Add(rows);
        }
        UsedValues.Clear();
	}

    public void ChangeAI(AiTypes type)
    {
        if (type == AiTypes.BreadthFirstSearch)
            Ai = new BFS();
        else if (type == AiTypes.AstarManhattan)
            return;
        else if (type == AiTypes.AstarPattern)
            return;
    }

	public void Solve()
	{
        if(Panels == null)
            return;
        Ai.Solve(Panels);
		// send the request to solve to the appropriate AI 
	}
	
}
