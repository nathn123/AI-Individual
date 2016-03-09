using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Controller : MonoBehaviour {

    public List<GameObject> Panels;
    public GameObject PanelPrefab;
    public Canvas Base;
    public List<int> UsedValues;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Generate9()
    {
		foreach (var p in Panels)
			DestroyImmediate(p);
		var centre = Base.pixelRect.center;
		var height = PanelPrefab.GetComponent<RectTransform>().rect.height;
		var width = PanelPrefab.GetComponent<RectTransform>().rect.width;
		
		for (int i = -1; i < 2; ++i)
		{
			for (int j = -1; j < 2; ++j)
			{
				var position = new Vector3(centre.x + i * width, centre.y + j * height);
				Panels.Add(GameObject.Instantiate(PanelPrefab, position, Quaternion.identity) as GameObject);
				int val;
				do
				{
					val = Random.Range(0, 9);
					
				} while (UsedValues.Contains(val));
				UsedValues.Add(val);
				if (val == 0)
					continue;
				Panels[Panels.Count - 1].GetComponent<Text>().text = val.ToString();
				Panels[Panels.Count - 1].GetComponent<Text>().color = Color.green;
				Panels[Panels.Count - 1].transform.SetParent(Base.transform);
			}
		}
		UsedValues.Clear ();
	}
	public void Generate15()
	{
		foreach (var p in Panels)
			DestroyImmediate(p);
		var centre = Base.pixelRect.center;
		var height = PanelPrefab.GetComponent<RectTransform>().rect.height;
		var width = PanelPrefab.GetComponent<RectTransform>().rect.width;
		
		for (int i = -2; i < 2; ++i)
		{
			for (int j = -2; j < 2; ++j)
			{
				var position = new Vector3(centre.x + i * width, centre.y + j * height);
				Panels.Add(GameObject.Instantiate(PanelPrefab, position, Quaternion.identity) as GameObject);
				int val;
				do
				{
					val = Random.Range(0, 16);
					
				} while (UsedValues.Contains(val));
				UsedValues.Add(val);
				if (val == 0)
					continue;
				Panels[Panels.Count - 1].GetComponent<Text>().text = val.ToString();
				Panels[Panels.Count - 1].GetComponent<Text>().color = Color.green;
				Panels[Panels.Count - 1].transform.SetParent(Base.transform);
			}
		}
		UsedValues.Clear ();
	}

	public void Solve()
	{

		// send the request to solve to the appropriate AI 
	}
	
}
