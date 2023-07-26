using UnityEngine;
using UnityEngine.UI;

namespace CannonShooter
{
	public class Cannon : MonoSingleton<Cannon>
	{
		[Header("Balls")]
		public Rigidbody[] ballsPrefabs;
		public float ballVelocity = 500;
		
		private Rigidbody currentBall;
		
		[Space(10)]
		public float reloadTime = 2;
		public int damageBonus = 0;

		private Enemy[] enemies;

		[SerializeField] private Image progressBar;
		[SerializeField] Transform shootPoint;
		[SerializeField] Text currentDamage;
		[SerializeField] Text m_reloadTime;
		[SerializeField] Button m_reloadButton;

		[SerializeField] private float rotationSpeed;
		private float currentTime;
		private Enemy currentEnemy;

		private Vector3 targetVector;

		private LineRenderer aimLine;

		private bool canFire => currentTime >= reloadTime;

        private void Start()
        {
	        progressBar.fillAmount = 1;
	        aimLine = GetComponent<LineRenderer>();

			currentBall = ballsPrefabs[0];
		}
		private void Update()
		{
			if (Physics.Raycast(shootPoint.position,shootPoint.forward, out var hitInfo))
			{
				aimLine.SetPosition(0, shootPoint.position);              
				aimLine.SetPosition(1, hitInfo.point);
			}

			AimCannon(Input.GetAxis("Horizontal"));
			
			if (currentTime < reloadTime)
			{
				currentTime += Time.deltaTime;
				progressBar.fillAmount = currentTime / reloadTime;
				m_reloadTime.text = System.Math.Round(currentTime, 1).ToString() + " сек";
			}

			if (Input.GetKeyDown(KeyCode.Space) && canFire)
			{
				Fire();
				currentTime = 0;
			}


		}

		public void SetBall(int arrowType)
        {
			currentBall = ballsPrefabs[arrowType];
        }

		private void AimCannon(float direction)
        {
			var targetRotation = transform.rotation * Quaternion.Euler(0, direction * 90, 0);
			
			transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }

		private void Fire()
        {
			if (currentBall == null) return;

			var bullet = Instantiate(currentBall, shootPoint.position, shootPoint.rotation);
			bullet.velocity = bullet.transform.forward * ballVelocity;
			var p = bullet.GetComponent<Projectile>();
			p.m_Damage += damageBonus;
			currentDamage.text = p.m_Damage.ToString();


		}

		public void ChangeReloadTime(float mReloadBonus)
		{
			reloadTime -= mReloadBonus;

			if (reloadTime < 0.5f)
			{
				reloadTime = 0.5f;
				m_reloadButton.interactable = false;
			}
		}
	}
}