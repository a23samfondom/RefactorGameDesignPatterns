using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerJump : MonoBehaviour {

    [Header("References")] 
    private Rigidbody2D rb;
    

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private bool isGrounded;

    [Header("Input")]
    [SerializeField] private InputAction jumpAction;
    [SerializeField] private CoyoteJump coyoteJump;
    bool jumpInput = false;
	private bool jumpRequested = false; //añadimos esta variable para saber cuando se ha pulsado la tecla 
	
	public static event Action PlayerJumped; //AGRERGO este evento para logro de saltos utilizado en OnJump()


    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        jumpAction.performed += OnJump;
		jumpAction.canceled += OnJump; // suscribimos el canceled de la tecla cuando suelta el espacio, que llame a OnJump 
	}

    private void OnEnable() {
        jumpAction.Enable();
       
    }
    private void OnDisable() {
        jumpAction.Disable();
   
    }
	
	void Update() {
        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
		bool jumped = coyoteJump.UpdateJump(rb, isGrounded, jumpRequested, jumpInput); // comprueba si esta saltando, osea que si estas en el aire, no puedes saltar

		jumpRequested = false; // reiniciamos esta variable a false, para que no entre en un bucle de que piense que esta todo el rato saltando, al ser false ya se indica que no ha pulsado la tecla
                                // osea que solo se activa cuando pulsas el espacio, depsues se consume en el update
		if (jumped)
		{
			PlayerJumped?.Invoke();
		}
	}

    void FixedUpdate() {                                                
        coyoteJump.FixedUpdateJump(rb, isGrounded, jumpInput);
    }
    void OnJump(InputAction.CallbackContext ctx) {
		if (ctx.performed)
		{
	
			jumpInput = true;
			jumpRequested = true;
		}
		else if (ctx.canceled)
		{

			jumpInput = false;
		}
	}
}

[System.Serializable]
public class CoyoteJump { 
    public float jumpForce = 14f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float coyoteTime = 0.1f; // Time after leaving ground where jump is still allowed
    public float jumpBufferTime = 0.1f; // Buffer time for jump input
    private float coyoteTimeCounter;
    private float jumpBufferCounter;

	
	public void FixedUpdateJump(Rigidbody2D rigidbody2D, bool isGrounded, bool jumpInput) {
        // Better gravity
        if (rigidbody2D.linearVelocity.y < 0) {
            rigidbody2D.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        } else if (rigidbody2D.linearVelocity.y > 0 && !jumpInput) {
            rigidbody2D.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    public bool UpdateJump(Rigidbody2D rigidbody2D, bool isGrounded, bool jumpRequested, bool jumpInput) {   // modifico el metodo, ahora es bool, con el objetivo de que pueda informar si se ha eralizado
                                                                                                // el salto o no, si es true es que se ha eralizado y false que no
		bool jumped = false; // creamos el bool para saber si esta saltando

		// Update coyote timer
		if (isGrounded)
            coyoteTimeCounter = coyoteTime;
        else
            coyoteTimeCounter -= Time.deltaTime;

        // Jump buffer
        if (jumpRequested) 
            jumpBufferCounter = jumpBufferTime;
        else
            jumpBufferCounter -= Time.deltaTime;

		// Jump

		if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
		{
			rigidbody2D.linearVelocity = new Vector2( rigidbody2D.linearVelocity.x, jumpForce);

			jumpBufferCounter = 0f;
			coyoteTimeCounter = 0f; // esto evita la oportunidad de poder saltar despues del salto, evita que el mismo salto se vuelva a ejecutar
			
			jumped = true;
		}   
		// Variable jump height
		if (!jumpInput && rigidbody2D.linearVelocity.y > 0f) {
			rigidbody2D.linearVelocity = new Vector2(rigidbody2D.linearVelocity.x, rigidbody2D.linearVelocity.y * 0.5f);
			
		    coyoteTimeCounter = 0f; // prevent double use
        }
		return jumped;
	}
	
}




// codigo "antiguo"

//public class PlayerJump : MonoBehaviour
//{

//	[Header("References")]
//	[SerializeField] GameObject projectilePrefab;
//	[SerializeField] NotificationManager notificationManager;
//	private Rigidbody2D rb;
//	int coins = 0;

//	[Header("Ground Check")]
//	public Transform groundCheck;
//	public float groundCheckRadius = 0.2f;
//	public LayerMask groundLayer;


//	private bool isGrounded;

//	[Header("Input")]
//	[SerializeField] InputAction jumpAction;
//	[SerializeField] InputAction movementAction;
//	[SerializeField] InputAction shootAction;

//	[SerializeField] CoyoteJump coyoteJump;
//	bool jumpInput = false;

//	[Header("Movement")]
//	[SerializeField] float horizontalMovementSpeed = 5.0f;
//	float horizontalVelocity;
//	float lastHorizontal = 1.0f;

//	public static event Action CoinCollected; //AGREGO este evento para utilizarlo en PickUpCoin()
//	public static event Action PlayerJumped; //AGRERGO este evento para logro de saltos utilizado en OnJump()



//	void Awake()
//	{
//		rb = GetComponent<Rigidbody2D>();
//		jumpAction.performed += OnJump;
//		movementAction.performed += OnMovement;
//		shootAction.performed += OnShoot;
//		lastHorizontal = 1.0f;
//	}

//	private void OnEnable()
//	{
//		jumpAction.Enable();
//		movementAction.Enable();
//		shootAction.Enable();
//	}
//	private void OnDisable()
//	{
//		jumpAction.Disable();
//		movementAction.Disable();
//		shootAction.Disable();
//	}

//	void Update()
//	{
//		// Ground check
//		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
//		coyoteJump.UpdateJump(rb, isGrounded, jumpInput);
//		rb.linearVelocity = new Vector2(horizontalVelocity, rb.linearVelocity.y);
//	}

//	void FixedUpdate()
//	{
//		coyoteJump.FixedUpdateJump(rb, isGrounded, jumpInput);
//	}
//	void OnJump(InputAction.CallbackContext ctx)
//	{

//		if (ctx.ReadValue<float>() == 1)
//		{

//			jumpInput = true;
//			PlayerJumped?.Invoke(); //AVISA a AchievementManager de que el jugador ha saltado

//		}
//		else
//		{
//			jumpInput = false;
//		}
//	}

//	void OnMovement(InputAction.CallbackContext ctx)
//	{
//		horizontalVelocity = ctx.ReadValue<float>() * horizontalMovementSpeed;
//		if (Mathf.Abs(ctx.ReadValue<float>()) > 0) lastHorizontal = horizontalVelocity;
//	}
//	void OnShoot(InputAction.CallbackContext ctx)
//	{
//		if (ctx.ReadValue<float>() == 1.0f)
//		{
//			Shoot();
//		}
//	}

//	void Shoot()
//	{
//		GameObject projectileInstance = Instantiate(projectilePrefab);
//		Projectile projectile = projectileInstance.GetComponent<Projectile>();
//		projectile.transform.position = transform.position + (Vector3.right * Mathf.Clamp(lastHorizontal, -1, 1));
//		projectile.Init(new Vector2(lastHorizontal, 0));

//	}

//	public void PickUpCoin()
//	{
//		coins++;

//		CoinCollected?.Invoke();

//		//notificationManager.AddNotification("Player now has " + coins + " coin" + (coins > 1 ? "s" : ""), 1.5f);
//		//Comento el mensaje para verificar que se muestra el logro de las 5 monedas
//	}


//}

//[System.Serializable]
//public class CoyoteJump
//{
//	public float jumpForce = 14f;
//	public float fallMultiplier = 2.5f;
//	public float lowJumpMultiplier = 2f;
//	public float coyoteTime = 0.1f; // Time after leaving ground where jump is still allowed
//	public float jumpBufferTime = 0.1f; // Buffer time for jump input
//	private float coyoteTimeCounter;
//	private float jumpBufferCounter;

//	public void FixedUpdateJump(Rigidbody2D rigidbody2D, bool isGrounded, bool jumpInput)
//	{
//		// Better gravity
//		if (rigidbody2D.linearVelocity.y < 0)
//		{
//			rigidbody2D.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
//		}
//		else if (rigidbody2D.linearVelocity.y > 0 && !jumpInput)
//		{
//			rigidbody2D.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
//		}
//	}

//	public void UpdateJump(Rigidbody2D rigidbody2D, bool isGrounded, bool jumpInputPressed)
//	{

//		// Update coyote timer
//		if (isGrounded)
//			coyoteTimeCounter = coyoteTime;
//		else
//			coyoteTimeCounter -= Time.deltaTime;

//		// Jump buffer
//		if (jumpInputPressed)
//			jumpBufferCounter = jumpBufferTime;
//		else
//			jumpBufferCounter -= Time.deltaTime;

//		// Jump
//		if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
//		{
//			rigidbody2D.linearVelocity = new Vector2(rigidbody2D.linearVelocity.x, jumpForce);
//			jumpBufferCounter = 0f; // consume buffer
//		}

//		// Variable jump height
//		if (!jumpInputPressed && rigidbody2D.linearVelocity.y > 0f)
//		{
//			rigidbody2D.linearVelocity = new Vector2(rigidbody2D.linearVelocity.x, rigidbody2D.linearVelocity.y * 0.5f);
//			coyoteTimeCounter = 0f; // prevent double use
//		}
//	}

//}
