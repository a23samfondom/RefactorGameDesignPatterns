
using UnityEngine;

public class EnemyPoolMember : MonoBehaviour
{
	private EnemyFactoryPool enemyPool;

	public void SetPool(EnemyFactoryPool pool) // este set guarda la referencia al pool del enemigo en la variable enemyPool, como pasa tambien con los proyectiles
	{
		enemyPool = pool;
	}

	public void ReturnToPool() // este metodo tiene como objetivo devolver el enemigo al pool, llamamos al metodo returnEnemy que tiene esa función
	{
		enemyPool.ReturnEnemy(gameObject);
	}

	public void ResetEnemy() // este metodo simplemente resetea lam vida del enemigo
	{
		GetComponent<HealthController>().ResetHealth();
	}
}
