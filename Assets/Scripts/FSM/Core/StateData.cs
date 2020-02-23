using System.Collections.Generic;
using UnityEngine;

public class StateData : ScriptableObject
{
    public string displayName;
    public List<Command> commands = new List<Command>(); 
}