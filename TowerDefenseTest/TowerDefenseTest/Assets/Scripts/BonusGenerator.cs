using System.Collections.Generic;
using UnityEngine;

public class BonusGenerator : MonoBehaviour
{

	#region Fields
	private float timeProgress;
    private ObjectPool pool;
    private Tile tile;
	public float frequencyBetweenBonus;
    private GridScenario gridScenario;
    #endregion

    #region Unity methods
    private void Awake()
    {
        pool = GetComponent<ObjectPool>();
    }

    private void Start()
    {
        gridScenario = GameObject.FindGameObjectWithTag("Scenario").GetComponent<GridScenario>();
        tile = GetComponent<TileContentType>().tileContainer;
    }

    private void Update()
    {
        timeProgress += Time.deltaTime;
        if (timeProgress > frequencyBetweenBonus)
        {
            timeProgress -= frequencyBetweenBonus;
            GameObject bonus = pool.GetPooledObject() as GameObject;
            Vector3 positionInScenario = GetRandomPositionNeighbor();
            bonus.transform.position = new Vector3(positionInScenario.x + Random.Range(-0.4f, 0.4f),
                positionInScenario.y + Random.Range(-0.4f, 0.4f),
                -7);
            bonus.SetActive(true);
        }
    }
    #endregion

    #region Private methods
    private Vector3 GetRandomPositionInScenario()
    {
        int x = Random.Range(0, gridScenario.scenarioSize.x);
        int y = Random.Range(0, gridScenario.scenarioSize.y);
        Vector3 position = gridScenario.scenarioTiles[x, y].transform.position;
        return position;
    }

    private Vector3 GetRandomPositionNeighbor()
    {
        List<Tile> neighbors = gridScenario.GetNeighbor(tile);
        Vector3 position = neighbors[Random.Range(0, neighbors.Count)].transform.position;
        return position;
    }
    #endregion

    #region Public / Protected methods

    #endregion
}