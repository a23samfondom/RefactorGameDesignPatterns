using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthController : MonoBehaviour {
    [SerializeField] int maxLives = 3;
    [SerializeField] int lives = 3;

	[SerializeField] private EnemyFactoryPool enemyFactoryPool; // creamos la referencia a enemyFactoryPool, que controla el metodo de creación de enemigos
	public static event Action EnemyDied; // Evento que avisa cuando un enemigo muere.

    public void GetHit() {

        lives--;

        if (lives <= 0) {
            /*La idea es sencilla: cuando el enemigo pierda su última vida,
            pedimos una moneda al CoinPool y la colocamos donde ha muerto el enemigo.*/

            Vector3 posicionMuerte = transform.position; // Guardamos la posición donde ha muerto el enemigo.

            CoinPool.Instance.ObtenerMoneda(posicionMuerte); // Pedimos una moneda al pool.

            EnemyDied?.Invoke(); // Avisar a los sistemas interesados de que el enemigo ha muerto.

            FindFirstObjectByType<EnemySpawner>().CrearEnemigo(); //Avisar al spawner para que cree otro enemigo.

			GetComponent<EnemyPoolMember>().ReturnToPool(); // en vez de eliminar al enemigo, lo devolvemos a la pool
		}


    }

	public void ResetHealth() // este metodo reinicia la vida del enemigo
	{
		lives = maxLives;
	}




	//CODIGO ORIGINAL
	/*
    public void GetHit() {
        lives--;
        if (lives <= 0) {
            Destroy(gameObject);
        }
    }

     */
}
