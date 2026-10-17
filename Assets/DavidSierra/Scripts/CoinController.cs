using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CoinController : MonoBehaviour {

	[SerializeField] private CoinPool coinPool;


	private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.GetComponent<PlayerController>().PickUpCoin();
			coinPool.DevolverMoneda(gameObject);  // lo que hace este metodo es en vez de eliminar, "devuelve" la moneda y la manda para el array de CoinPool
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