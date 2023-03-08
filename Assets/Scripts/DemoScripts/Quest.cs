using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Date updated: " + DateTime.Now.ToString("MM/dd/yyyy")
[CreateAssetMenu(fileName = "Quest", menuName = "Quest")]
public class Quest : ScriptableObject
{
    public enum Status{
        NotYetComplete,
        Complete,
        Failed
    }
    public string questName;
    public List<Objective> objectives;
    [System.Serializable]
    public class Objective{
        public string name = "New Objective";
        public bool optional = false;
        public bool visible = true;
        public Status initialStatus = Status.NotYetComplete;
    }
}