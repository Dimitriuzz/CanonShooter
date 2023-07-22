
using UnityEngine;


namespace BallistaShooter
{
    /// <summary>
    /// Летательный аппарат 2Д.
    /// NOTE: важно учесть соотношение сил и скоростей чтобы физический движок не выдал пакость.
    /// </summary>
   // [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructible
    {
        /// <summary>
        /// Масса для автоматической установки у ригида.
        /// </summary>
        [Header("Space ship")]
        [SerializeField] private float m_Mass;

        /// <summary>
        /// Толкающая вперед сила.
        /// </summary>
        [SerializeField] private float m_Thrust;

        /// <summary>
        /// Вращающая сила.
        /// </summary>
        [SerializeField] private float m_Mobility;
        public float Mobility => m_Mobility;

        private float m_MobilityBackup;

        /// <summary>
        /// Максимальная линейная скорость.
        /// </summary>
        [SerializeField] private float m_MaxLinearVelocity;

        /// <summary>
        /// Максимальная вращательная скорость. В градусах/сек
        /// </summary>
        [SerializeField] private float m_MaxAngularVelocity;

        /// <summary>
        /// Сохраненная ссылка на ригид.
        /// </summary>
        private Rigidbody m_Rigid;
        public Rigidbody Rigid => m_Rigid;

        public void StopMovement()
        {
            m_MobilityBackup = m_Mobility;
            m_Mobility = 0;

        }

        public void ContinueMovement()
        {
            m_Mobility = m_MobilityBackup;
        }

        #region Public API

        /// <summary>
        /// Управление линейной тягой. -1.0 до +1.0
        /// </summary>
        public float ThrustControl { get; set; }

        /// <summary>
        /// Управление вращательной тягой. -1.0 до +1.0
        /// </summary>
        public float TorqueControl { get; set; }

        #endregion

        #region Unity events

        protected override void Start()
        {
            base.Start();

            //m_Rigid = GetComponent<Rigidbody>();
           //m_Rigid.mass = m_Mass;

            // единичная инерция для того чтобы упростить баланс кораблей.
            // либо неравномерные коллайдеры будут портить вращение
            // решается домножением торка на момент инерции
            //m_Rigid.inertia = 1;

           // InitOffensive();
        }

        private void FixedUpdate()
        {
            UpdateRigidbody();
            //UpdateEnergyRegen();
        }

        #endregion

        /// <summary>
        /// Метод добавления сил кораблю для движения.
        /// </summary>
        private void UpdateRigidbody()
        {
           // прибавляем толкающую силу
           // m_Rigid.AddForce(m_Thrust * ThrustControl * transform.up * Time.fixedDeltaTime, ForceMode.Force);

            // линейное вязкое трение -V * C
           // m_Rigid.AddForce(-m_Rigid.velocity * (m_Thrust / m_MaxLinearVelocity) * Time.fixedDeltaTime, ForceMode.Force);

            // добавляем вращение
            //m_Rigid.AddTorque(m_Mobility * TorqueControl * Time.fixedDeltaTime, ForceMode.Force);

            // вязкое вращательное трение
            //m_Rigid.AddTorque(-m_Rigid.angularVelocity * (m_Mobility / m_MaxAngularVelocity) * Time.fixedDeltaTime, ForceMode.Force);
       
           
            }

        public bool DrawAmmo(int count)
        {
            return true;
        }

        /// <summary>
        /// Метод вычитания патронов из боезапаса корабля. Используется турелями.
        /// </summary>
        /// <param name="count"></param>
        /// <returns>true если патроны были скушаны</returns>
        public bool DrawEnergy(int count)
        {
            return true;
        }

        public void Fire(TurretMode mode)
        {
            
        }

        /*
        #region Offensive

        private const int StartingAmmoCount = 10;

        /// <summary>
        /// Ссылки на турели корабля. Турели класть отдельными геймобъектами.
        /// Каждая турель ест патроны и энергию.
        /// </summary>
        [SerializeField] private Turret[] m_Turrets;

        /// <summary>
        /// Максимум энергии на корабле.
        /// </summary>
        [SerializeField] private int m_MaxEnergy;

        /// <summary>
        /// Максимум патронов на корабле.
        /// </summary>
        [SerializeField] private int m_MaxAmmo;

        /// <summary>
        /// Скорость регенерации энергии в секунду.
        /// </summary>
        [SerializeField] private int m_EnergyRegenPerSecond;

        /// <summary>
        /// Кол-ыо энергии на корабле. float чтоб был смысл в свойстве реген в секунду.
        /// </summary>
        private float m_PrimaryEnergy;

        public void AddEnergy(int e)
        {
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy + e, 0, m_MaxEnergy);
        }

        /// <summary>
        /// Кол-во патронов.
        /// </summary>
        private int m_SecondaryAmmo;

        public void AddAmmo(int ammo)
        {
            m_SecondaryAmmo = Mathf.Clamp(m_SecondaryAmmo + ammo, 0, m_MaxAmmo);
        }

        /// <summary>
        /// Инициализация начальный свойств корабля.
        /// </summary>
        private void InitOffensive()
        {
            m_PrimaryEnergy = m_MaxEnergy;
            m_SecondaryAmmo = StartingAmmoCount;
        }

        /// <summary>
        /// Обновляем статы корабля. Можно воткнуть например автопочинку.
        /// </summary>
        private void UpdateEnergyRegen()
        {
            m_PrimaryEnergy += (float)m_EnergyRegenPerSecond * Time.fixedDeltaTime;
        }

        /// <summary>
        /// Метод вычитания патронов из боезапаса корабля. Используется турелями.
        /// </summary>
        /// <param name="count"></param>
        /// <returns>true если патроны были скушаны</returns>
        

        /// <summary>
        /// Стреляем первичкой или вторичкой.
        /// </summary>
        /// <param name="mode"></param>
       

        #endregion

        public void AssignWeapon(TurretProperties props)
        {
            foreach (var v in m_Turrets)
                v.AssignLoadout(props);
        }
        */

        public new void Use(EnemyAsset asset)
        {
            m_MaxLinearVelocity = asset.moveSpeed;
            base.Use(asset);
        }

    }

}