using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StateIA", menuName = "IA/StateIA")]
public class StateIA : ScriptableObject
{
	[System.Serializable]
	public class Transition
	{
		public Decision decision;
		public StateIA trueState;
		public StateIA falseState;
	}

	#region Fields
	[SerializeField]
	Action[] actions;
	[SerializeField]
	Transition[] transitions;
	#endregion

	#region Private methods
	private void DoActions(Enemy enemy)
	{
		for (int i = 0; i < actions.Length; i++)
		{
			actions[i].Act(enemy);
		}
	}

	private void DoDecisions(Enemy enemy)
	{
		for (int i = 0; i < transitions.Length; i++)
		{
			bool result = transitions[i].decision.Evaluate(enemy);
			if (result)
            {
				enemy.state = transitions[i].trueState;
            }
			else
            {
				enemy.state = transitions[i].falseState;
			}
		}
	}
	#endregion

	#region Public / Protected methods
	public void GameUpdate(Enemy enemy)
    {
		DoActions(enemy);
		DoDecisions(enemy);
    }


	#endregion
}