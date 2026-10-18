using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CoinController : MonoBehaviour {

	//[SerializeField] private CoinPool coinPool; --> Comento esta variable antes de borrartela pq ahora en vez de utilizar eso,
													//utilizamos la instancia para no arrastrar a cada moneda el pool


	private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.GetComponent<PlayerController>().PickUpCoin();
			CoinPool.Instance.DevolverMoneda(gameObject);  // lo que hace este metodo es en vez de eliminar, "devuelve" la moneda y la manda para el array de CoinPool
															//En vez de utilizar una variable, ahora utiliza la misma instancia compartida del pool
		}
    }
}


//Detecta cuándo el jugador recoge la moneda, suma la moneda al contador y la devuelve al pool. 

// codigo anterior :)

//private void OnTriggerEnter2D(Collider2D collision)
//{
//	if (collision.gameObject.CompareTag("Player"))
//	{
//		collision.gameObject.GetComponent<PlayerController>().PickUpCoin();
//		Destroy(gameObject);
//	}
//}



//Codigo original de Aitor antes de MODIFICACION de Samuel
//[SerializeField] private CoinPool coinPool;
/*	private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.GetComponent<PlayerController>().PickUpCoin();
			coinPool.DevolverMoneda(gameObject);  // lo que hace este metodo es en vez de eliminar, "devuelve" la moneda y la manda para el array de CoinPool
													
		}
    }
*/