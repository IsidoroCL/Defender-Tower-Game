using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{

	#region Fields
	public int addMoney;
	#endregion

	#region Unity methods

	#endregion

	#region Private methods

	#endregion
	
	#region Public / Protected methods
	public void Touched()
    {
		Game.money += addMoney;
		gameObject.SetActive(false);
    }
	#endregion
}