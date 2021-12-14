using UnityEngine;

[CreateAssetMenu(fileName = "Decision", menuName = "IA/Decision")]
public abstract class Decision : ScriptableObject
{
    public abstract bool Evaluate(Enemy enemy);
}