using UnityEngine;

[CreateAssetMenu(fileName = "EnemyFactory", menuName = "Factory/EnemyFactory")]
public class EnemyFactory : GameObjectFactory
{
    
	#region Fields
	[SerializeField]
	EnemyConfig[] configs;
	#endregion

	#region Unity methods

	#endregion

	#region Private methods
	private EnemyConfig GetConfig(EnemyType type)
    {
        switch (type)
        {
			case EnemyType.Normal: return configs[0];
			case EnemyType.Swift: return configs[1];
			case EnemyType.Armoured: return configs[2];
			default: return configs[0];
        }
    }
	#endregion
	
	#region Public / Protected methods
	public Enemy GetEnemy(EnemyType type)
    {
		EnemyConfig config = GetConfig(type);
		Enemy enemy = CreateGameObjectInstance<Enemy>(config.prefab);
		enemy.Config(config.speed, config.health, this);
		return enemy;
    }

	public void Reclaim(Enemy enemy)
	{
		Destroy(enemy.gameObject);
	}
	#endregion
}