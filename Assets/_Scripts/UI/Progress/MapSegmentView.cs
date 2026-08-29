#region Milestone 1 - MapSegmentView (exposes pre-placed level nodes)
//using UnityEngine;

//public class MapSegmentView : MonoBehaviour
//{
//    [SerializeField] LevelNode[] levelNodes;

//    public int NodeCount => levelNodes.Length;

//    public LevelNode GetNode(int index)
//    {
//        return levelNodes[index];
//    }
//}
#endregion

#region Milestone 1 - MapSegmentView (exposes pre-placed level nodes)
using UnityEngine;

public class MapSegmentView : MonoBehaviour
{
    [SerializeField] LevelNode[] levelNodes;

    public int NodeCount => levelNodes.Length;

    public LevelNode GetNode(int index)
    {
        return levelNodes[index];
    }
}
#endregion