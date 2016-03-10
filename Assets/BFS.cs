using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BFS : AIBase
{

    

    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void Solve(List<List<GameObject>> StartSpace)
    {
       CurSpace =  GetCurrentState(StartSpace);
       GoalSpace = GoalState(CurSpace);
       PossibleNodes = new List<Node>();
       ExpandedNodes = new List<Node>();
       Node BaseNode = new Node();
       BaseNode.State = CurSpace;
       BaseNode.Parent = -1;
       BaseNode.depth = 0;
       BaseNode.cost = 0;
       PossibleNodes.Add(BaseNode);
       do
       {
           //expand one node then test completion
           var node = PossibleNodes[0];
            PossibleNodes.Remove(node);
            ExpandedNodes.Add(node);
            PossibleNodes.AddRange(ExpandNode(node, ExpandedNodes.LastIndexOf(node)));

       } while (!HitGoal());

    }
    private List<Node> ExpandNode(Node CurNode, int parent)
    {
        List<Node> NewNodes = new List<Node>();
        // check what moves are possible for the 0
        // first we find the 0
        int x = -1;
        int y = -1;
        int max = CurNode.State.Count;
        for (int i = 0; i < max; ++i)
            for (int j = 0; j < max; ++j)
                if(CurNode.State[i][j].Value == 0)
                {
                    x = i;
                    y = j;
                    break;
                }
        // now we need to work out what moves are possible
        // we need to detect corners  == 2 moves
        // edges == 3 moves
        // central == 4 moves
        if (x == 0)
        {
            // on top row
            if(y == 0)
            {
                //top left
                NewNodes.Add(CreateNode(CurNode, new Vector2(x, y), new Vector2(x, y + 1), parent));
                NewNodes.Add(CreateNode(CurNode, new Vector2(x, y), new Vector2(x+1, y), parent));
            }
            else if (y == max-1)
            {
                // top right
                NewNodes.Add(CreateNode(CurNode, new Vector2(x, y), new Vector2(x, y - 1), parent));
                NewNodes.Add(CreateNode(CurNode, new Vector2(x, y), new Vector2(x + 1, y), parent));
            }
            else
            {
                //top edge
                NewNodes.Add(CreateNode(CurNode, new Vector2(x, y), new Vector2(x, y + 1), parent));
                NewNodes.Add(CreateNode(CurNode, new Vector2(x, y), new Vector2(x + 1, y), parent));
                NewNodes.Add(CreateNode(CurNode, new Vector2(x, y), new Vector2(x, y - 1), parent));
            }
        }
        else if (x == max-1)
        {
            //bottom row
            if (y == 0)
            {
                //bot left
                NewNodes.Add(CreateNode(CurNode, new Vector2(x, y), new Vector2(x, y + 1), parent));
                NewNodes.Add(CreateNode(CurNode, new Vector2(x, y), new Vector2(x - 1, y), parent));
            }
            else if (y == max-1)
            {
                // bot right
                NewNodes.Add(CreateNode(CurNode, new Vector2(x, y), new Vector2(x, y - 1), parent));
                NewNodes.Add(CreateNode(CurNode, new Vector2(x, y), new Vector2(x - 1, y), parent));
            }
            else
            {
                //bottom edge
                NewNodes.Add(CreateNode(CurNode, new Vector2(x, y), new Vector2(x, y + 1), parent));
                NewNodes.Add(CreateNode(CurNode, new Vector2(x, y), new Vector2(x - 1, y), parent));
                NewNodes.Add(CreateNode(CurNode, new Vector2(x, y), new Vector2(x, y - 1), parent));
            }
        }
        else if (y == 0)
        {
            //left edge
            NewNodes.Add(CreateNode(CurNode, new Vector2(x, y), new Vector2(x + 1, y), parent));
            NewNodes.Add(CreateNode(CurNode, new Vector2(x, y), new Vector2(x, y + 1), parent));
            NewNodes.Add(CreateNode(CurNode, new Vector2(x, y), new Vector2(x - 1, y), parent));
        }
        else if (y == max-1)
        {
            //right edge
            NewNodes.Add(CreateNode(CurNode, new Vector2(x, y), new Vector2(x + 1, y), parent));
            NewNodes.Add(CreateNode(CurNode, new Vector2(x, y), new Vector2(x, y - 1), parent));
            NewNodes.Add(CreateNode(CurNode, new Vector2(x, y), new Vector2(x - 1, y), parent));
        }
        else
        {
            //central case
            NewNodes.Add(CreateNode(CurNode, new Vector2(x, y), new Vector2(x, y + 1), parent));
            NewNodes.Add(CreateNode(CurNode, new Vector2(x, y), new Vector2(x + 1, y), parent));
            NewNodes.Add(CreateNode(CurNode, new Vector2(x, y), new Vector2(x, y - 1), parent));
            NewNodes.Add(CreateNode(CurNode, new Vector2(x, y), new Vector2(x - 1, y), parent));
        }
                // if move has not been done, add it to the list
        for (int j = 0; j < ExpandedNodes.Count;++j)
            for (int i = 0; i < NewNodes.Count; ++i)
            {
                if (CheckDuplicates(NewNodes[i],ExpandedNodes[j]))
                {
                    NewNodes.Remove(NewNodes[i]);
                    i =0;
                    j = 0;
                    // remove the conflict and reset the loop
                }
            }

                return NewNodes;
    }

    private bool CheckDuplicates(Node NewNode, Node OldNode)
    {
            for (int j = 0; j < NewNode.State.Count;++j)
            {
                for (int k = 0; k < NewNode.State.Count; ++k)
                {
                    if (NewNode.State[j][k].Value != OldNode.State[j][k].Value)
                       return false;
                }
            }
     return true;
    }
}
