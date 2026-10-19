
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactoryPool : MonoBehaviour
{
	[SerializeField] private GameObject enemyPrefab;
	[SerializeField] private int initialAmount = 3;

	private List<GameObject> pool = new List<GameObject>();

	private void Awake()
	{
		for (int i = 0; i < initialAmount; i++)
		{
			GameObject enemy = Instantiate(enemyPrefab);
			enemy.SetActive(false);
			pool.Add(enemy);
		}
	}

	public GameObject CreateEnemy(Vector3 position, Quaternion rotation)
	{
		GameObject enemy;

		if (pool.Count > 0)
		{
			enemy = pool[0];
			pool.RemoveAt(0);
		}
		else
		{
			enemy = Instantiate(enemyPrefab);
		}

		enemy.transform.position = position;
		enemy.transform.rotation = rotation;

		EnemyPoolMember enemyScript = enemy.GetComponent<EnemyPoolMember>();
		enemyScript.SetPool(this);
		enemyScript.ResetEnemy();

		enemy.SetActive(true);

		return enemy;
	}

	public void ReturnEnemy(GameObject enemy)
	{
		enemy.SetActive(false);
		pool.Add(enemy);
	}
}
