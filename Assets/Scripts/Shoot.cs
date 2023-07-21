
using UnityEngine;

using UnityEngine.UI;

namespace BallistaShooter
{
	public class Shoot : MonoBehaviour
	{

		[Header("Arrows")]
		public GameObject [] arrowPrefabs;
		public float arrowVelocity=500;
		public float arrowmaxVelocity=200;
		private float fillAmountStep;
		private GameObject currentArrow;
		

		[Space(10)]
		public float reloadTime=2;

		private Enemy[] enemies;

		[SerializeField] private Image progressBar;
		[SerializeField] Transform shootPoint;

		[SerializeField] private float rotationSpeed;
		private float currentTime=0;
		private float lastTime=0;
		private Enemy currentEnemy;
		private int currentEnemyIndex=0;

		float time;
		private Vector3 targetVector;
		private bool firePressed=false;
		private bool leftPressed=false;
		private bool rightPressed=false;
		private int enemyCount=0;

		private LineRenderer aimLine;

        private void Start()
        {
			//currentEnemy = enemies[currentEnemyIndex];
			progressBar.fillAmount = 1;
			fillAmountStep = reloadTime*0.01f;
			aimLine = GetComponent<LineRenderer>();
			//FindEnemies();
			//EnemyWavesManager.EnemySpawned += FindEnemies;
			//EnemyWavesManager.OnAllWavesDead += Unsubscribe;
			currentArrow = arrowPrefabs[0];
		}
		private void Update()
		{

			
				//targetVector = enemies[currentEnemyIndex].Aim.transform.position - transform.position;
				time += Time.deltaTime;

			if (Physics.Raycast(shootPoint.position,shootPoint.forward, out var hitInfo))
			{
				
				aimLine.SetPosition(0, shootPoint.position);              
				aimLine.SetPosition(1, hitInfo.point);              

            }
				//AImBallista();
			   AImBallista(Input.GetAxis("Horizontal"));
				if (progressBar.fillAmount < 1)
				{
					currentTime += Time.deltaTime;
					if (currentTime >= (lastTime + fillAmountStep))
					{
						progressBar.fillAmount += 0.01f;
						lastTime = currentTime;
					}
				}

				if (Input.GetKeyDown(KeyCode.Space) && progressBar.fillAmount == 1)
				{
					Fire();
					progressBar.fillAmount = 0;
					currentTime = 0;
					lastTime = 0;
					//firePressed = true;

					/*if (time >= Скорострельность)
					{
						Fire();
					}*/
				}
			
		}
				/*if (firePressed)
				{
					if (arrowVelocity <= arrowmaxVelocity)
					{
						arrowVelocity += Time.deltaTime * 50;
						progressBar.fillAmount = arrowVelocity / arrowmaxVelocity;

					}
					//Debug.Log("Velocity" + arrowVelocity);
				}

				if (Input.GetKeyUp(KeyCode.Space))
				{
					Fire();
					progressBar.fillAmount = 0;
					/*if (time >= Скорострельность)
					{

					}
					arrowVelocity = 0;
					firePressed = false;
				}

				/*
				if (Input.GetKeyUp(KeyCode.A))
				{
					if (time >= Скорострельность)
					{
						SelectEnemy(-1);
					}

				}
				if (Input.GetKeyUp(KeyCode.D))
				{
					if (time >= Скорострельность)
					{
						SelectEnemy(1);
					}

				}

				

				if (Input.GetKeyDown(KeyCode.A))leftPressed = true;

				if (leftPressed) AImBallista(0);

				if (Input.GetKeyUp(KeyCode.A)) leftPressed = false;


				if (Input.GetKeyDown(KeyCode.D)) rightPressed = true;

				if (rightPressed) AImBallista(1);

				if (Input.GetKeyUp(KeyCode.D)) rightPressed = false;

				foreach (var en in enemies)
                {
					if (en.isHit) enemyCount++;
                }
				if (enemyCount == enemies.Length)
				{
					//LevelSequenceController.Instance.FinishCurrentLevel(true);
					LevelResultController.Instance.Show(true);

				}
				else enemyCount = 0;
			}

		}

		private void Unsubscribe()
        {
			EnemyWavesManager.EnemySpawned -= FindEnemies;
			EnemyWavesManager.OnAllWavesDead -= Unsubscribe;
		}
		private void FindEnemies()
        {
			enemies = FindObjectsOfType<Enemy>();
			currentEnemy = enemies[currentEnemyIndex];

		}
				*/

		public void SetArrow(int arrowtype)
        {
			currentArrow = arrowPrefabs[arrowtype];

        }

		public void AImBallista(float direction)
        {
			var rotation = transform.rotation * Quaternion.Euler(0, direction * 90, 0);
			transform.rotation= Quaternion.Lerp(transform.rotation, rotation, rotationSpeed* Time.fixedDeltaTime);

			//if (direction == 1) transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 90, 0), Time.deltaTime);
			//Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetVector, Time.deltaTime, 0.0f);

			// Draw a ray pointing at our target in
			//Debug.DrawRay(transform.position, newDirection, Color.red);

			// Calculate a rotation a step closer to the target and applies rotation to this object
			//transform.rotation = Quaternion.LookRotation(newDirection);
		}

		private void Fire()
        {
			if (currentArrow == null) return;

			//Debug.Log(transform.position);
			//Debug.Log("Current Aim" + currentEnemy.transform.name);
			var bullet = Instantiate(currentArrow, shootPoint.position, shootPoint.rotation);//.GetComponentInChildren<Rigidbody>();
			var arrow=bullet.GetComponentInChildren<Rigidbody>();
			arrow.velocity = transform.TransformDirection(Vector3.forward * arrowVelocity);//transform.TransformDirection(Vector3.forward * arrowVelocity);
			Debug.Log("Arrow "+bullet.transform.position);
			time = 0;

		}

		private void SelectEnemy(int selection)
        {
			

				
				
			do
			{
				Debug.Log(currentEnemy.transform.name);
				currentEnemy.Selection.gameObject.SetActive(false);
				currentEnemyIndex += selection;
				if (currentEnemyIndex >= enemies.Length) currentEnemyIndex = 0;
				if (currentEnemyIndex < 0) currentEnemyIndex = enemies.Length - 1;
				currentEnemy = enemies[currentEnemyIndex];

				Debug.Log(currentEnemy.transform.name);
				currentEnemy.Selection.gameObject.SetActive(true);
			} while (currentEnemy.isHit!);
			

		}
	}
}