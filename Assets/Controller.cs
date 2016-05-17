using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Controller : MonoBehaviour {

    public List<List<GameObject>> Panels;
    public GameObject PanelPrefab;
    public Canvas Base;
    public bool SolutionPlaying, nextmove;
    public List<int> UsedValues;
    public List<AIBase.Node> Solution;
    public Vector2 Move1, Move2 , Move1dir,Move2dir;
    public GameObject Moving1, Moving2;
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
        if(SolutionPlaying)
        {
            if (nextmove)
            {
                // do the next visual change
                // calculate the change between nodes

                var changes = DetectChange(Solution[Solution.Count - 1], Solution[Solution.Count - 2]);

                var pos1 = DetectPosition((int)changes.x, Solution[Solution.Count - 1]);
                var pos2 = DetectPosition((int)changes.y, Solution[Solution.Count - 1]);
                Moving1 = Panels[(int)pos1.x][(int)pos1.y];
                Moving2 = Panels[(int)pos2.x][(int)pos2.y];
                Solution.RemoveAt(Solution.Count - 1);

                Move1 = Moving2.transform.position;
                Move2 = Moving1.transform.position;
            }
            
            // ;
           //  ;
            if (new Vector3(Move1.x, Move1.y, 0) == Moving1.transform.position ||
                new Vector3(Move2.x, Move2.y, 0) == Moving2.transform.position)
                nextmove = true;
            
            if (Solution.Count == 0)
                SolutionPlaying = false;
        }
	
	}
    public void GenTest9()
    { 
        // REMOVE OLD PANELS
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
                int val = -1; // so we can see if it works properly
                if (i == -1) // LEFT
                {
                    if(j== 1) //TOP
                    {
                        val = 8;
                    }
                    else if(j == 0) // CENTRE
                    {
                        val = 2;
                    }
                    else if(j == -1)// BOT;
                    {
                        val = 3;
                    }
                }
                else if (i == 0) // CENTRE
                {
                    if(j== 1) //TOP
                    {
                        val = 6;
                    }
                    else if(j == 0) // CENTRE
                    {
                        val = 5;
                    }
                    else if(j == -1)// BOT;
                    {
                        val = 0;
                    }
                }
                else if (i == 1) //RIGHT
                {
                    if(j== 1) //TOP
                    {
                        val = 7;
                    }
                    else if(j == 0) // CENTRE
                    {
                        val = 4;
                    }
                    else if(j == -1)// BOT;
                    {
                        val = 1;
                    }
                }

                if (val == 0)
                    rows[j + 1].GetComponent<Text>().text = "";
                else
                    rows[j + 1].GetComponent<Text>().text = val.ToString();

                rows[j + 1].GetComponent<Text>().color = Color.green;
                rows[j + 1].transform.SetParent(Base.transform);
            }
            Panels.Add(rows);
        }
        UsedValues.Clear();
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
        StartCoroutine( Ai.Solve(Panels));// send the request to solve to the appropriate AI 
        Solution = Ai.GetTheSequence();
        Debug.Log("solution complete");
        //on completion get the correct iteration
	}

    Vector2 DetectPosition(int number, AIBase.Node curNode)
    {

        for (int i = 0; i < curNode.State.Count; ++i)
            for (int j = 0; j < curNode.State[i].Count; ++i)
                if (curNode.State[i][j].Value == number)
                    return new Vector2(i, j);
        return new Vector2(-1, -1);
    }
    Vector2 DetectChange(AIBase.Node oldNode,AIBase.Node newNode)
    {
        int pos = 0;
        Vector2 changes = new Vector2(-1,-1);
        //detect difference between the nodes
        for(int i = 0; i < oldNode.State.Count;++i)
            for(int j = 0; j < oldNode.State[i].Count;++j)
                if (oldNode.State[i][j].Value != newNode.State[i][j].Value)
                    changes = new Vector2(oldNode.State[i][j].Value, newNode.State[i][j].Value);
        return changes;
    }
    public void PlaySolution()
    {
        if (Solution == null)
            return;
        SolutionPlaying = true;
        nextmove = true;
    }
	
}
