using UnityEngine;
using UnityEngine.UI;

namespace CannonShooter
{
	public class CannonRandom : MonoSingleton<Cannon>
	{
		[Header("Balls")]
		public Rigidbody[] ballsPrefabs;
		public float ballVelocity = 500;
		
		private Rigidbody currentBall;
		
		[Space(10)]
		public float reloadTime = 2;
		public int damageBonus = 0;

		private Enemy[] enemies;

		
		[SerializeField] Transform shootPoint;
		
		[SerializeField] Text m_reloadTime;
	

		[SerializeField] private float rotationSpeed;
		private float currentTime;


		private Vector3 targetVector;



		private bool canFire => currentTime >= reloadTime;

        private void Start()
        {


			currentBall = ballsPrefabs[0];
		}
		private void Update()
		{

			AimCannon();

			if (!canFire)
			{
				currentTime += Time.deltaTime;

			}
			else
			{
				SetBall(Random.Range(0, ballsPrefabs.Length));
				Fire();

			
				currentTime = 0;
			}


		}

		public void SetBall(int arrowType)
        {
			currentBall = ballsPrefabs[arrowType];
        }

		private void AimCannon()
        {
			var targetRotation = transform.rotation * Quaternion.Euler(0, Random.Range(-60, 60), 0);
			Debug.Log(targetRotation);
			transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
			//transform.rotation = Quaternion.Euler(0, Random.Range(-90,90), 0);
        }

		private void Fire()
        {
			if (currentBall == null) return;

			var bullet = Instantiate(currentBall, shootPoint.position, shootPoint.rotation);
			bullet.velocity = bullet.transform.forward * ballVelocity;
		


		}

		
	}
}