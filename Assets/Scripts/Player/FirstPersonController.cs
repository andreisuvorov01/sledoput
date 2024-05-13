using UnityEngine;
using UnityEngine.UI;

public class FirstPersonController : MonoBehaviour
{
	private Rigidbody rb;

	public Camera playerCamera;

	public float fov = 60f;

	public bool invertCamera;

	public bool cameraCanMove = true;

	public float mouseSensitivity = 2f;

	public float maxLookAngle = 50f;

	public bool lockCursor = true;

	public bool crosshair = true;

	public Sprite crosshairImage;

	public Color crosshairColor = Color.white;

	private float yaw;

	private float pitch;

	private Image crosshairObject;

	public bool enableZoom = true;

	public bool holdToZoom;

	public KeyCode zoomKey = KeyCode.Mouse1;

	public float zoomFOV = 30f;

	public float zoomStepTime = 5f;

	private bool isZoomed;

	public bool playerCanMove = true;

	public float walkSpeed = 5f;

	public float maxVelocityChange = 10f;

	private bool isWalking;

	public bool enableSprint = true;

	public bool unlimitedSprint;

	public KeyCode sprintKey = KeyCode.LeftShift;

	public float sprintSpeed = 7f;

	public float sprintDuration = 5f;

	public float sprintCooldown = 0.5f;

	public float sprintFOV = 80f;

	public float sprintFOVStepTime = 10f;

	public bool useSprintBar = true;

	public bool hideBarWhenFull = true;

	public Image sprintBarBG;

	public Image sprintBar;

	public float sprintBarWidthPercent = 0.3f;

	public float sprintBarHeightPercent = 0.015f;

	private CanvasGroup sprintBarCG;

	private bool isSprinting;

	private float sprintRemaining;

	private float sprintBarWidth;

	private float sprintBarHeight;

	private bool isSprintCooldown;

	private float sprintCooldownReset;

	public bool enableJump = true;

	public KeyCode jumpKey = KeyCode.Space;

	public float jumpPower = 5f;

	private bool isGrounded;

	public bool enableCrouch = true;

	public bool holdToCrouch = true;

	public KeyCode crouchKey = KeyCode.LeftControl;

	public float crouchHeight = 0.75f;

	public float speedReduction = 0.5f;

	private bool isCrouched;

	private Vector3 originalScale;

	public bool enableHeadBob = true;

	public Transform joint;

	public float bobSpeed = 10f;

	public Vector3 bobAmount = new Vector3(0.15f, 0.05f, 0f);

	private Vector3 jointOriginalPos;

	private float timer;

	private float old_pos;

	private float camRotation;

	[SerializeField] private AudioSource goPlayerAudio;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
		crosshairObject = GetComponentInChildren<Image>();
		playerCamera.fieldOfView = fov;
		originalScale = base.transform.localScale;
		jointOriginalPos = joint.localPosition;
		if (!unlimitedSprint)
		{
			sprintRemaining = sprintDuration;
			sprintCooldownReset = sprintCooldown;
		}
	}

	private void Start()
	{
		old_pos = base.transform.position.y;
		if (lockCursor)
		{
			Cursor.lockState = CursorLockMode.Locked;
		}
		/*if (crosshair)
		{
			crosshairObject.sprite = crosshairImage;
			crosshairObject.color = crosshairColor;
		}*/
		else
		{
			crosshairObject.gameObject.SetActive(value: false);
		}
		sprintBarCG = GetComponentInChildren<CanvasGroup>();
		if (useSprintBar)
		{
			sprintBarBG.gameObject.SetActive(value: true);
			sprintBar.gameObject.SetActive(value: true);
			float num = Screen.width;
			float num2 = Screen.height;
			sprintBarWidth = num * sprintBarWidthPercent;
			sprintBarHeight = num2 * sprintBarHeightPercent;
			sprintBarBG.rectTransform.sizeDelta = new Vector3(sprintBarWidth, sprintBarHeight, 0f);
			sprintBar.rectTransform.sizeDelta = new Vector3(sprintBarWidth - 2f, sprintBarHeight - 2f, 0f);
			/*if (hideBarWhenFull)
			{
				sprintBarCG.alpha = 0f;
			}*/
		}
		else
		{
			sprintBarBG.gameObject.SetActive(value: false);
			sprintBar.gameObject.SetActive(value: false);
		}
	}

	private void Update()
	{
		if (cameraCanMove)
		{
			yaw = base.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity;
			if (!invertCamera)
			{
				pitch -= mouseSensitivity * Input.GetAxis("Mouse Y");
			}
			else
			{
				pitch += mouseSensitivity * Input.GetAxis("Mouse Y");
			}
			pitch = Mathf.Clamp(pitch, 0f - maxLookAngle, maxLookAngle);
			base.transform.localEulerAngles = new Vector3(0f, yaw, 0f);
			playerCamera.transform.localEulerAngles = new Vector3(pitch, 0f, 0f);
		}
		if (Input.GetKeyDown(KeyCode.Space) )
		{
			Jump();
		}
		/*if (enableZoom)
		{
			if (Input.GetKeyDown(zoomKey) && !holdToZoom && !isSprinting)
			{
				if (!isZoomed)
				{
					isZoomed = true;
				}
				else
				{
					isZoomed = false;
				}
			}
			if (holdToZoom && !isSprinting)
			{
				if (Input.GetKeyDown(zoomKey))
				{
					isZoomed = true;
				}
				else if (Input.GetKeyUp(zoomKey))
				{
					isZoomed = false;
				}
			}
			if (isZoomed)
			{
				playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, zoomFOV, zoomStepTime * Time.deltaTime);
			}
			else if (!isZoomed && !isSprinting)
			{
				playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, fov, zoomStepTime * Time.deltaTime);
			}
		}*/
		/*if (enableSprint)
		{
			if (isSprinting)
			{
				isZoomed = false;
				playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, sprintFOV, sprintFOVStepTime * Time.deltaTime);
				if (!unlimitedSprint)
				{
					sprintRemaining -= 1f * Time.deltaTime;
					if (sprintRemaining <= 0f)
					{
						isSprinting = false;
						isSprintCooldown = true;
					}
				}
			}
			else
			{
				sprintRemaining = Mathf.Clamp(sprintRemaining += 1f * Time.deltaTime, 0f, sprintDuration);
			}
			if (isSprintCooldown)
			{
				sprintCooldown -= 1f * Time.deltaTime;
				if (sprintCooldown <= 0f)
				{
					isSprintCooldown = false;
				}
			}
			else
			{
				sprintCooldown = sprintCooldownReset;
			}
			if (useSprintBar && !unlimitedSprint)
			{
				float x = sprintRemaining / sprintDuration;
				sprintBar.transform.localScale = new Vector3(x, 1f, 1f);
			}
		}
		if (enableCrouch)
		{
			if (Input.GetKeyDown(crouchKey) && !holdToCrouch)
			{
				Crouch();
			}
			if (Input.GetKeyDown(crouchKey) && holdToCrouch)
			{
				isCrouched = false;
				Crouch();
			}
			else if (Input.GetKeyUp(crouchKey) && holdToCrouch)
			{
				isCrouched = true;
				Crouch();
			}
		}*/
		CheckGround();
		if (enableHeadBob)
		{
			HeadBob();
		}
	}

	private void FixedUpdate()
	{
		if (!playerCanMove)
		{
			return;
		}
		Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
		if (direction.x != 0f || (direction.z != 0f && isGrounded))
		{
            isWalking = true;
		}
		else
		{
            isWalking = false;
		}
		if (enableSprint && Input.GetKey(sprintKey) && sprintRemaining > 0f && !isSprintCooldown)
		{
			direction = base.transform.TransformDirection(direction) * sprintSpeed;
			Vector3 velocity = rb.velocity;
			Vector3 force = direction - velocity;
			force.x = Mathf.Clamp(force.x, 0f - maxVelocityChange, maxVelocityChange);
			force.z = Mathf.Clamp(force.z, 0f - maxVelocityChange, maxVelocityChange);
			force.y = 0f;
			if (force.x != 0f || force.z != 0f)
			{
				isSprinting = true;
				if (isCrouched)
				{
					Crouch();
				}
				if (hideBarWhenFull && !unlimitedSprint)
				{
					sprintBarCG.alpha += 5f * Time.deltaTime;
				}
			}
			rb.AddForce(force, ForceMode.VelocityChange);
		}
		else
		{
			isSprinting = false;
			/*if (hideBarWhenFull && sprintRemaining == sprintDuration)
			{
				sprintBarCG.alpha -= 3f * Time.deltaTime;
			}*/
			direction = base.transform.TransformDirection(direction) * walkSpeed;
			Vector3 velocity2 = rb.velocity;
			Vector3 force2 = direction - velocity2;
			force2.x = Mathf.Clamp(force2.x, 0f - maxVelocityChange, maxVelocityChange);
			force2.z = Mathf.Clamp(force2.z, 0f - maxVelocityChange, maxVelocityChange);
			force2.y = 0f;
			rb.AddForce(force2, ForceMode.VelocityChange);
		}
	}

	private void CheckGround()
	{
		Vector3 vector = new Vector3(base.transform.position.x, base.transform.position.y - base.transform.localScale.y * 0.5f, base.transform.position.z);
		Vector3 vector2 = base.transform.TransformDirection(Vector3.down);
		float num = 0.75f;
		if (Physics.Raycast(vector, vector2, out var _, num))
		{
			Debug.DrawRay(vector, vector2 * num, Color.red);
			isGrounded = true;
		}
		else
		{
			isGrounded = false;
		}
	}

	private void Jump()
	{
		if (isGrounded)
		{
			rb.AddForce(0f, jumpPower, 0f, ForceMode.Impulse);
			isGrounded = false;
		}
		if (isCrouched && !holdToCrouch)
		{
			Crouch();
		}
	}

	private void Crouch()
	{
		if (isCrouched)
		{
			base.transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
			walkSpeed /= speedReduction;
			isCrouched = false;
		}
		else
		{
			base.transform.localScale = new Vector3(originalScale.x, crouchHeight, originalScale.z);
			walkSpeed *= speedReduction;
			isCrouched = true;
		}
	}

	private void HeadBob()
	{
		if (isWalking)
		{

            if (isSprinting)
			{
				timer += Time.deltaTime * (bobSpeed + sprintSpeed);
			}
			else if (isCrouched)
			{
				timer += Time.deltaTime * (bobSpeed * speedReduction);
			}
			else
			{
				timer += Time.deltaTime * bobSpeed;
			}
			joint.localPosition = new Vector3(jointOriginalPos.x + Mathf.Sin(timer) * bobAmount.x, jointOriginalPos.y + Mathf.Sin(timer) * bobAmount.y, jointOriginalPos.z + Mathf.Sin(timer) * bobAmount.z);
		}
		else
		{
            goPlayerAudio.Play();
            timer = 0f;
			joint.localPosition = new Vector3(Mathf.Lerp(joint.localPosition.x, jointOriginalPos.x, Time.deltaTime * bobSpeed), Mathf.Lerp(joint.localPosition.y, jointOriginalPos.y, Time.deltaTime * bobSpeed), Mathf.Lerp(joint.localPosition.z, jointOriginalPos.z, Time.deltaTime * bobSpeed));
		}
	}
}
