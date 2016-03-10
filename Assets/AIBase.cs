using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public abstract class AIBase  {

    public struct PanelVal
    {
        public int Value;
        public GameObject Panel;

        public PanelVal(PanelVal Copy)
        {
            this.Value = Copy.Value;
            this.Panel = Copy.Panel;
        }
    }
    public struct Node
    {
        public int Parent; // value points to position in removed list
        //public List<Node> Children;
        public int depth;
        public int cost;
        public List<List<PanelVal>> State;
    }
    public List<List<PanelVal>> CurSpace, GoalSpace;
    public List<Node> PossibleNodes, ExpandedNodes;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public abstract void Solve(List<List<GameObject>> StartSpace);

    public virtual List<List<PanelVal>> GetCurrentState(List<List<GameObject>> StartSpace)
    {
        List<List<PanelVal>> current = new List<List<PanelVal>>();

        for (int i = 0; i < StartSpace.Count; ++i)
        {
            List<PanelVal> prerow = new List<PanelVal>();
            for (int j = 0; j < StartSpace[i].Count; ++j)
            {
                PanelVal pan = new PanelVal();
                var text = StartSpace[i][j].GetComponent<Text>().text;
                if (StartSpace[i][j].GetComponent<Text>().text == "")
                    pan.Value = 0;
                else
                    pan.Value = Int32.Parse(StartSpace[i][j].GetComponent<Text>().text);
                pan.Panel = StartSpace[i][j];
                prerow.Add(pan);
            }
            current.Add(prerow);
        }
                return current;
    }

    public virtual List<List<PanelVal>> GoalState(List<List<PanelVal>> statespace)
    {
        List<PanelVal> prelist = new List<PanelVal>();
        List<List<PanelVal>> goal = new List<List<PanelVal>>();
        //goal is 1 at TL and N is BCR / BC and 0 is BR
        foreach (var row in statespace)
            foreach (var p in row)
                if(p.Value != 0)
                    prelist.Add(p);
        prelist.Sort(CompareByValue);
        var height = statespace.Count;

        for (int i = 0; i < height; ++i)
        {
            List<PanelVal> PreGoal = new List<PanelVal>();
            for (int j = 0; j < height; ++j)
            {
                if ((i * height + j) == ((height-1)*height + (height-1)))
                    break;
                PreGoal.Add(prelist[i * height + j]);
            }
            goal.Add(PreGoal);
        }
        foreach (var row in statespace)
            foreach (var p in row)
                if (p.Value == 0)
                    goal[height-1].Add(p);
        return goal;
    }

    public virtual bool HitGoal()
    {
        foreach(var CurNode in PossibleNodes)
            if (CurNode.State == GoalSpace)
                return true;
        return false;
    }

    public virtual Node CreateNode(Node CurNode,Vector2 cur,Vector2 next, int parent)
    {
        Node NewNode = new Node();
        // we need to copy the node then switch the values at the x and y
        NewNode.cost = CurNode.cost + 1;
        NewNode.depth = CurNode.depth + 1;
        NewNode.Parent = parent;
        NewNode.State = new List<List<PanelVal>>();

        foreach(var col in CurNode.State)
            NewNode.State.Add(new List<PanelVal>(col));

        PanelVal tempstorezero = CurNode.State[(int)cur.x][(int)cur.y];
        PanelVal tempstoreother = CurNode.State[(int)next.x][(int)next.y];


        NewNode.State[(int)cur.x][(int)cur.y] = tempstoreother;
        NewNode.State[(int)next.x][(int)next.y] = tempstorezero;

        return NewNode;
    }

    int CompareByValue(PanelVal valA, PanelVal valB)
    {
        if (valA.Value > valB.Value)
            return 1;
        else if (valA.Value == valB.Value)
            return 0;
        else
            return -1;
    }
}
