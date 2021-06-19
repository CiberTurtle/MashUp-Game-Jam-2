using System.Linq;
using NaughtyAttributes;
using UnityEngine;

public class Oblet : MonoBehaviour
{
	public Vector2 wanderRange;
	public Vector2 depositRange;

	[Space]

	public float friction;
	public float maxVelocity;

	public LayerMask obletsMask;

	[Space]

	public float moveTargetSpeed;
	public float moveAwaySpeed;
	public float nearbyObletsRange;

	[Space]

	public float speedToInteract;

	[Space]

	public GameObject oblet;
	public GameObject splitEffect;
	public GameObject mergeEffect;
	public GameObject depositEffect;

	[Space]

	public Rigidbody2D rb;
	public Transform bodySprite;
	public CircleCollider2D circleCollider;
	public ParticleSystem particle;
	public TrailRenderer trail;
	public Transform eyes;
	public GameObject normalEyes;
	public GameObject fullEyes;

	[Space]

	public float energy = 1;
	public float growSpeed = 0.05f;
	public float maxEnergy = 5f;

	[Space]

	[ReadOnly] public Vector2 velocity;
	[ReadOnly] public Vector2 move;
	[ReadOnly] public Vector2 targetPosition;

	public Vector2 position { get => rb.position; set => rb.position = value; }

	ParticleSystem.ShapeModule particleShape;

	void Awake()
	{
		if (Mathf.Approximately(transform.position.y, 20.0f))
			transform.position = new Vector2(Random.Range(-wanderRange.x, wanderRange.x), Random.Range(-wanderRange.y, wanderRange.y));

		if (Mathf.Approximately(transform.position.y, -20.0f))
		{
			transform.position = new Vector2(Random.Range(-wanderRange.x, wanderRange.x), Random.Range(-wanderRange.y, wanderRange.y));
			growSpeed = 0;
		}

		trail.Clear();
	}

	void Start()
	{
		particleShape = particle.shape;

		NewWanderTarget();
	}

	void FixedUpdate()
	{
		energy += growSpeed * Time.fixedDeltaTime;
		energy = Mathf.Clamp(energy, 0, maxEnergy);

		var betterSize = Mathf.Clamp(energy, 1.0f, float.PositiveInfinity);

		bodySprite.localScale = Vector3.Lerp(bodySprite.localScale, new Vector3(betterSize, betterSize, 1), 15 * Time.fixedDeltaTime);
		circleCollider.radius = betterSize / 2;
		particleShape.radius = betterSize / 2;
		trail.startWidth = betterSize;

		eyes.localPosition = new Vector3(0, energy / 8, 0);

		if (energy >= maxEnergy)
		{
			normalEyes.SetActive(false);
			fullEyes.SetActive(true);
		}
		else
		{
			normalEyes.SetActive(true);
			fullEyes.SetActive(false);
		}

		var nearbyOblets = Physics2D.OverlapCircleAll(position, nearbyObletsRange, obletsMask).Select(x => x.GetComponent<Oblet>()).ToArray();

		if (Vector2.Distance(position, targetPosition) < 1.0f)
		{
			NewWanderTarget();
		}

		move = (targetPosition - position).normalized * moveTargetSpeed;

		var moveAway = Vector2.zero;

		foreach (var oblet in nearbyOblets)
		{
			if (oblet == this) continue;

			moveAway += (rb.position - oblet.position) * (Vector2.Distance(rb.position, oblet.position) / nearbyObletsRange);
		}

		move += moveAway * moveAwaySpeed;

		if (velocity.sqrMagnitude > speedToInteract * speedToInteract)
		{
			var ob = Physics2D.OverlapCircleAll(transform.position, energy / 2 + 0.15f, obletsMask).FirstOrDefault(x => x.gameObject != gameObject && x.GetComponent<Oblet>().energy > 1)?.GetComponent<Oblet>();

			if (ob)
			{
				if (Mathf.Abs(energy - ob.energy) > 0.25f)
				{
					if (energy > ob.energy)
					{
						for (int i = 0; i < 2; i++)
						{
							var newOb = Instantiate(oblet, ob.position, Quaternion.identity).GetComponent<Oblet>();
							newOb.energy = ob.energy / 2;
							newOb.growSpeed = ob.growSpeed + Random.Range(-0.015f, 0.015f);
							newOb.maxEnergy = ob.maxEnergy + Random.Range(-0.1f, 0.1f);

							velocity = Vector2.zero;
						}

						GameManager.current.PlaySound("Split");
						Instantiate(splitEffect, ob.position, Quaternion.identity);

						Destroy(ob.gameObject);
					}
					else
					{
						ob.energy += energy;
						ob.growSpeed += growSpeed * Random.Range(0.0f, 0.25f);
						ob.maxEnergy += maxEnergy * Random.Range(0.0f, 0.25f);

						GameManager.current.PlaySound("Absorb");
						Instantiate(mergeEffect, ob.position, Quaternion.identity);

						Destroy(gameObject);
						return;
					}
				}
			}
		}

		velocity = Vector2.ClampMagnitude(velocity, maxVelocity);
		rb.position += velocity;
		rb.position += move;
		velocity *= friction;

		if (Mathf.Abs(rb.position.x) > depositRange.x || Mathf.Abs(rb.position.y) > depositRange.y)
		{
			GameManager.current.energy += energy * (energy * 0.25f);

			GameManager.current.PlaySound("Deposit");
			Instantiate(depositEffect, position, Quaternion.identity);

			Destroy(gameObject);
			return;
		}
	}

	void NewWanderTarget()
	{
		targetPosition = new Vector2(Random.Range(-wanderRange.x, wanderRange.x), Random.Range(-wanderRange.y, wanderRange.y));
	}

	void OnDrawGizmos()
	{
		Gizmos.color = new Color(1, 0, 0, 0.5f);
		Gizmos.DrawLine(transform.position, transform.position + (Vector3)move);
		Gizmos.color = new Color(0, 0, 1, 0.25f);
		Gizmos.DrawLine(transform.position, targetPosition);
	}
}
