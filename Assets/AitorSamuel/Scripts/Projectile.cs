using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField] Rigidbody2D rb2D;
    [SerializeField] float projectileSpeed;

	private ProjectileFactoryPool projectilePool; // creamos la referencia a ProjectileFactoryPool
	public void SetPool(ProjectileFactoryPool pool) // este setter lo que hacemos es que el la variable projectilePool se guarde la referencia del pool
	{
		projectilePool = pool;
	}

	public void Init(Vector2 direction) {
        rb2D.linearVelocity = direction * projectileSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.CompareTag("Enemy")) {
            collision.gameObject.GetComponent<HealthController>().GetHit();
        }
		projectilePool.ReturnProjectile(gameObject); // ahora en vez de eliminar el proyectil, lo que hacemos es devolverlo a la pool
	}

}
