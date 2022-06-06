using Playground.Characters.Monsters;
using UnityEngine;
using UnityEngine.Events;

namespace Playground.Characters
{
	public class CharacterController2D : MonoBehaviour
	{
		[SerializeField] public float m_JumpForce = 1250f; // Amount of force added when the player jumps.

		[Range(0, 1)] [SerializeField]
		public float m_CrouchSpeed = .36f; // Amount of maxSpeed applied to crouching movement. 1 = 100%

		[Range(0, .3f)] [SerializeField]
		public float m_MovementSmoothing = .05f; // How much to smooth out the movement

		[SerializeField] public bool m_AirControl = true; // Whether or not a player can steer while jumping;
		[SerializeField] public LayerMask m_WhatIsGround; // A mask determining what is ground to the character
		[SerializeField] public LayerMask m_WhatIsCeil; // A mask determining what is ceil to the character
		[SerializeField] public Transform m_FootCheck; // A position marking where to check if the player is grounded.
		[SerializeField] public Transform m_HeadCheck; // A position marking where to check for ceilings
		[SerializeField] public Collider2D m_CrouchDisableCollider; // A collider that will be disabled when crouching

		const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
		private bool m_Grounded; // Whether or not the player is grounded.
		private bool m_NormalGravity = true; // Whether or not the gravity has been switched
		private bool m_Ceiled; // Whether or not the player is ceiled
		const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
		private Rigidbody2D m_Rigidbody2D;
		private bool m_FacingRight = true; // For determining which way the player is currently facing.
		private Vector3 m_Velocity = Vector3.zero;

		[Header("Events")] [Space] public UnityEvent OnLandEvent; // All event that have to be done when landing

		[System.Serializable]
		public class BoolEvent : UnityEvent<bool>
		{
		}

		public BoolEvent OnCrouchEvent;
		private bool m_wasCrouching = false;

		private void Awake()
		{
			m_Rigidbody2D = GetComponent<Rigidbody2D>();

			if (OnLandEvent == null)
				OnLandEvent = new UnityEvent();

			if (OnCrouchEvent == null)
				OnCrouchEvent = new BoolEvent();
			
			OnLandEvent.AddListener(gameObject.GetComponent<Character>().OnLanding);
			
			m_WhatIsCeil = LayerMask.GetMask("Ceiling");
			m_WhatIsGround = LayerMask.GetMask("Floor");
		}

		private void FixedUpdate()
		{
			bool wasGrounded = m_Grounded;
			m_Grounded = false;

			// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
			// This can be done using layers instead but Sample Assets will not overwrite your project settings.
			Collider2D[] collidersGround =
				Physics2D.OverlapCircleAll(m_FootCheck.position, k_GroundedRadius, m_WhatIsGround);
			for (int i = 0; i < collidersGround.Length; i++)
			{
				if (collidersGround[i].gameObject != gameObject)
				{
					m_Grounded = true;
					if (!wasGrounded)
					{
						OnLandEvent.Invoke();
					}
				}
			}



			m_Ceiled = false;
			// The player is ceiled if a circlecast to the ceilingcheck position hits anything designated as ceil
			// This can be done using layers instead but Sample Assets will not overwrite your project settings.
			Collider2D[] collidersCeil =
				Physics2D.OverlapCircleAll(m_FootCheck.position, k_CeilingRadius, m_WhatIsCeil);
			for (int i = 0; i < collidersCeil.Length; i++)
			{
				if (collidersCeil[i].gameObject != gameObject)
				{
					m_Ceiled = true;
				}
			}
		}


		public void Move(float move, bool crouch, bool jump)
		{
			// If crouching, check to see if the character can stand up
			if (!crouch)
			{
				// If the character has a ceiling preventing them from standing up, keep them crouching
				if (Physics2D.OverlapCircle(m_HeadCheck.position, k_CeilingRadius, m_WhatIsGround))
				{
					crouch = true;
				}
			}

			//only control the player if grounded or airControl is turned on
			if (m_Grounded || m_Ceiled || m_AirControl)
			{
				// If crouching
				if (crouch)
				{
					if (!m_wasCrouching)
					{
						m_wasCrouching = true;
						OnCrouchEvent.Invoke(true);
					}

					// Reduce the speed by the crouchSpeed multiplier
					move *= m_CrouchSpeed;

					// Disable one of the colliders when crouching
					if (m_CrouchDisableCollider != null)
						m_CrouchDisableCollider.enabled = false;
				} else
				{
					// Enable the collider when not crouching
					if (m_CrouchDisableCollider != null)
						m_CrouchDisableCollider.enabled = true;

					if (m_wasCrouching)
					{
						m_wasCrouching = false;
						OnCrouchEvent.Invoke(false);
					}
				}

				// Move the character by finding the target velocity
				Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
				// And then smoothing it out and applying it to the character
				m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

				// If the input is moving the player right and the player is facing left...
				if (move > 0 && !m_FacingRight)
				{
					// ... flip the player.
					Flip();
				}
				// Otherwise if the input is moving the player left and the player is facing right...
				else if (move < 0 && m_FacingRight)
				{
					// ... flip the player.
					Flip();
				}
			}
			
			// If the player should jump...
			if (jump)
			{
				if (m_Grounded && m_NormalGravity)
				{
					// Add a vertical force to the player.
					m_Grounded = false;
					m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
				}

				if (m_Grounded && !m_NormalGravity)
				{
					// Add an inverted vertical force to the player.
					m_Grounded = false;
					m_Rigidbody2D.AddForce(new Vector2(0f, -m_JumpForce));
				}
			}
		}


		public bool SwitchGravity()
		{
			if (m_Grounded || m_Ceiled)
			{
				Physics2D.gravity = -Physics2D.gravity;
				gameObject.transform.Rotate(0f, 180f, 180f);
				m_NormalGravity ^= true; // Switch the boolean
				
				//loop for monster
				GameObject[] SwitchMonster = GameObject.FindGameObjectsWithTag("Monsters");
				foreach (var monster in SwitchMonster)
				{
					monster.gameObject.transform.Rotate(0f, 180f, 180f);
				}
				return true;
			}
			
			return false;
		}

		private void Flip()
		{
			// Switch the way the player is labelled as facing.
			m_FacingRight = !m_FacingRight;

			// Multiply the player's x local scale by -1.
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
	}
}
