using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager current;

	public LayerMask draggable;
	public float dragSmoothing;

	[Space]

	public float energy;
	float energyForNextLevel = 100.0f;
	float energyLoseSpeed = 1.0f;
	int level = 1;

	[Space]

	public AudioClip pickupClip;
	public AudioClip dropClip;
	public AudioClip splitClip;
	public AudioClip absorbClip;
	public AudioClip depositClip;

	[Space]

	public Image energyBar;
	public TMP_Text energyText;
	public GameObject death;

	Vector2 mouseScreenPos;
	Vector2 mousePos;
	Vector2 cameraVelocity;

	Vector2 dragVelocity;
	Oblet dragging;

	Inputs inputs;
	bool dragStart;
	bool dragEnd;

	Camera cam;

	void Awake()
	{
		current = this;

		inputs = new Inputs();
		inputs.Enable();
		inputs.Game.Drag.started += _ => dragStart = true;
		inputs.Game.Drag.canceled += _ => dragEnd = true;
		inputs.Game.MousePosition.performed += x => mouseScreenPos = x.ReadValue<Vector2>();
		inputs.Game.Restart.started += _ => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

		cam = Camera.main;

		Time.timeScale = 1;
	}

	void FixedUpdate()
	{
		mousePos = cam.ScreenToWorldPoint(mouseScreenPos);

		if (dragStart)
		{
			dragging = Physics2D.OverlapPoint(mousePos, draggable)?.GetComponent<Oblet>();

			if (dragging)
			{
				PlaySound("Pickup");
			}
		}
		dragStart = false;

		if (dragEnd)
		{
			if (dragging)
			{
				dragging.velocity = dragVelocity;
				dragging = null;

				PlaySound("Drop");
			}
		}
		dragEnd = false;

		if (dragging)
		{
			var lastPos = dragging.rb.position;

			dragging.rb.position = Vector3.Lerp(dragging.rb.position, mousePos, dragSmoothing * Time.fixedDeltaTime);

			dragVelocity = dragging.rb.position - lastPos;
		}

		energy -= energyLoseSpeed * Time.fixedDeltaTime;

		if (energy < 0)
		{
			death.SetActive(true);
			Time.timeScale = 0;
		}
		else
		if (energy > energyForNextLevel)
		{
			level++;
			energyLoseSpeed = level + ((float)level * level / 22);
			energyForNextLevel = 100 + Mathf.Pow(10, level) / ((float)level / 2);
			Debug.Log($"{energyLoseSpeed} | {energyForNextLevel}");
		}
	}

	void Update()
	{
		energyBar.fillAmount = Mathf.Lerp(energyBar.fillAmount, energy / energyForNextLevel, 20f * Time.deltaTime);
		energyText.text = $"LVL {level}";
	}

	public void PlaySound(string name)
	{
		switch (name)
		{
			case "Split": PlaySound(splitClip); break;
			case "Absorb": PlaySound(absorbClip); break;
			case "Deposit": PlaySound(depositClip); break;
			case "Pickup": PlaySound(pickupClip); break;
			case "Drop": PlaySound(dropClip); break;
		}
	}

	public void PlaySound(AudioClip clip)
	{
		var s = gameObject.AddComponent<AudioSource>();
		s.clip = clip;
		s.pitch = Random.Range(0.9f, 1.1f);
		s.Play();
		Destroy(s, clip.length + 0.1f);
	}
}
