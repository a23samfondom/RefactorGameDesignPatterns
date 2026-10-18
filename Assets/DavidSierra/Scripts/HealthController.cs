using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {
    [SerializeField] int maxLives = 3;
    [SerializeField] int lives = 3;
    public void GetHit() {
        lives--;
        if (lives <= 0) {

            /*La idea es sencilla: cuando el enemigo pierda su última vida,
            pedimos una moneda al CoinPool y la colocamos donde ha muerto el enemigo.*/

            Vector3 posicionMuerte = transform.position; // Guardamos la posición donde ha muerto el enemigo.

            CoinPool.Instance.ObtenerMoneda(posicionMuerte); // Pedimos una moneda al pool.

            FindFirstObjectByType<EnemySpawner>().CrearEnemigo(); //Avisar al spawner para que cree otro enemigo.

            Destroy(gameObject); //Enemigo se destruye
        }
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
