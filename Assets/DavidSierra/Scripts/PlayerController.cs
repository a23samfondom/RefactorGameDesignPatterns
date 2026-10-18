using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerController : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private GameObject projectilePrefab;

	private Rigidbody2D rb;
	private int coins = 0;

	[Header("Input")]
	[SerializeField] private InputAction movementAction;
	[SerializeField] private InputAction shootAction;

	[Header("Movement")]
	[SerializeField] private float horizontalMovementSpeed = 5.0f;

	private float horizontalVelocity;
	private float lastHorizontal = 1.0f;

	public static event Action CoinCollected; //AGREGO este evento para utilizarlo en PickUpCoin()

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();

		movementAction.performed += OnMovement;
		shootAction.performed += OnShoot;
		movementAction.canceled += OnMovement;
	}

	private void OnEnable()
	{
		movementAction.Enable();
		shootAction.Enable();
	}

	private void OnDisable()
	{
		movementAction.Disable();
		shootAction.Disable();
	}

	private void OnMovement(InputAction.CallbackContext ctx)
	{
		float input = ctx.ReadValue<float>();

		horizontalVelocity = input * horizontalMovementSpeed;

		if (Mathf.Abs(input) > 0)
			lastHorizontal = input;
	}

	private void Update()
	{
		rb.linearVelocity = new Vector2(
			horizontalVelocity,
			rb.linearVelocity.y
		);
	}

	private void OnShoot(InputAction.CallbackContext ctx)
	{
		if (ctx.ReadValue<float>() == 1.0f)
		{
			Shoot();
		}
	}

	private void Shoot()
	{
		GameObject projectileInstance = Instantiate(projectilePrefab);

		Projectile projectile =
			projectileInstance.GetComponent<Projectile>();

		projectile.transform.position =
			transform.position + Vector3.right * lastHorizontal;

		projectile.Init(new Vector2(lastHorizontal, 0));
	}

	public void PickUpCoin()
	{
		coins++;

		CoinCollected?.Invoke();
		
	}
	

   

}
