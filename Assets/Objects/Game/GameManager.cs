using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
	public static GameManager current;

	public LayerMask draggable;
	public float dragSmoothing;

	[Space]

	public GameObject[] tutSegments;
	public float tutSegmentTime = 8.0f;

	[Space]

	public AudioClip pickupClip;
	public AudioClip dropClip;
	public AudioClip splitClip;
	public AudioClip absorbClip;
	public AudioClip absorbSlowClip;
	public AudioClip depositClip;
	public AudioClip depositLargeClip;
	public AudioClip maxClip;
	public AudioClip nextLevelClip;
	public AudioClip warningClip;

	[Space]

	public Image energyBar;
	public TMP_Text energyText;
	public CanvasGroup deathScreen;
	public TMP_Text deathMessage;
	public TMP_Text deathScore;

	[System.NonSerialized] public int numberOfOblets;

	ulong score;
	float energy;
	float energyForNextLevel = 100.0f;
	float energyLoseSpeed = 1.0f;
	int level = 1;

	int currentTutSegment;
	float currentTutTimeAlive;

	bool inDanger;

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
				dragging.isBeingPickedup = true;

				PlaySound("Pickup");
			}
		}
		dragStart = false;

		if (dragEnd)
		{
			if (dragging)
			{
				dragging.velocity = dragVelocity;
				dragging.isBeingPickedup = false;
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

		if (currentTutSegment < tutSegments.Length)
		{
			currentTutTimeAlive += Time.deltaTime;
			if (currentTutTimeAlive > tutSegmentTime)
			{
				currentTutTimeAlive = 0;

				tutSegments[currentTutSegment].GetComponent<CanvasGroup>().DOFade(0, 0.25f);
				currentTutSegment++;

				if (currentTutSegment < tutSegments.Length)
				{
					tutSegments[currentTutSegment].GetComponent<CanvasGroup>().DOFade(1, 0.25f);
				}
				else
				{
					energy = 50;
					energyText.transform.parent.GetComponent<CanvasGroup>().DOFade(1, 0.25f);
				}
			}
		}
		else
		{
			energy -= energyLoseSpeed * Time.fixedDeltaTime;

			if (energy < energyLoseSpeed * 10)
			{
				if (!inDanger)
					PlaySound("Warning");
				inDanger = true;
			}

			if (energy < 0)
			{
				Lose("Obletopia ran out of energy");
			}
			else
			if (energy > energyForNextLevel)
			{
				level++;
				energyLoseSpeed = 1 + ((float)(level - 1) / 2);
				energyForNextLevel = 100 + Mathf.Pow(10, level) / ((float)level / 2);
				Debug.Log($"Loss: {energyLoseSpeed} - Next: {energyForNextLevel}");

				PlaySound("Next Level");
			}
		}

		if (numberOfOblets < 1 && Time.timeScale != 0 && Time.timeSinceLevelLoad > 3)
		{
			Lose("Everyone died in Obletopia");
		}
	}

	public void AddEnergy(float energy)
	{
		this.energy += energy;
		score += (ulong)Mathf.FloorToInt(energy);
	}

	public void Lose(string msg)
	{
		deathMessage.text = msg;
		deathScore.text = $"Score: {score}";
		deathScreen.gameObject.SetActive(true);
		deathScreen.alpha = 0;
		deathScreen.transform.localScale = new Vector3(1.25f, 1.25f, 1);
		deathScreen.DOFade(1, 0.5f).SetUpdate(true);
		deathScreen.transform.DOScale(Vector3.one, 0.67f).SetEase(Ease.OutBounce).SetUpdate(true);
		Time.timeScale = 0;
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
			case "Absorb Slow": PlaySound(absorbSlowClip); break;
			case "Deposit": PlaySound(depositClip); break;
			case "Deposit Large": PlaySound(depositLargeClip, false); break;
			case "Pickup": PlaySound(pickupClip); break;
			case "Drop": PlaySound(dropClip); break;
			case "Max": PlaySound(maxClip); break;
			case "Next Level": PlaySound(nextLevelClip); break;
			case "Warning": PlaySound(warningClip); break;
		}
	}

	public void PlaySound(AudioClip clip, bool randomPitch = true)
	{
		var s = gameObject.AddComponent<AudioSource>();
		s.clip = clip;
		if (randomPitch) s.pitch = Random.Range(0.9f, 1.1f);
		s.Play();
		Destroy(s, clip.length + 0.1f);
	}
}
