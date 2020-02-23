using UnityEngine;

public class Command : ScriptableObject
{
    [Range(0, 100)]
    [SerializeField] protected int m_IntField;

    public virtual void Execute(Actor actor)
    {

    }
}