using System;
using System.Collections;
using System.Collections.Generic;
using BallistaShooter;
using UnityEditor;
using UnityEngine;

namespace BallistaShooter
{
    /// <summary>
    /// Скрипт управления AI. Цепляется на префаб корабля.
    /// Реализует управление используя набор примитивных действий.
    /// </summary>
    [RequireComponent(typeof(SpaceShip))]
    public class AIController : MonoBehaviour
    {
        /// <summary>
        /// Типы поведений.
        /// </summary>
        public enum AIBehaviour
        {
            /// <summary>
            /// Ничего не делаем.
            /// </summary>
            Null,

            /// <summary>
            /// Патрулируем и атакуем врагов.
            /// </summary>
            Patrol
        }

        [SerializeField] private CharacterController controller;

        [SerializeField] private AIBehaviour m_AIBehaviour;

        /// <summary>
        /// Как быстро будем летать.
        /// </summary>
        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationLinear;

        /// <summary>
        /// Как быстро будет бот поворачиваться.
        /// </summary>
        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationAngular;

        /// <summary>
        /// Текущая точка патрулирования. Впринципе может быть размером со всю игровую область.
        /// </summary>
        [SerializeField] private AIPointPatrol m_PatrolPoint;



        /// <summary>
        /// Время рандомизации выбора новой точки движения.
        /// Задает значение таймера ActionTimerType.RandomizeDirection
        /// </summary>
        [SerializeField] private float m_RandomSelectMovePointTime;

        /// <summary>
        /// Время между поисками целей. Минимальное внутри реализации 1сек.
        /// </summary>
        [SerializeField] private float m_FindNewTargetTime;

        [SerializeField] private float m_RotationSpeed=5f;

        /// <summary>
        /// Рандомное время между выстрелами.
        /// </summary>
        [SerializeField] private float m_ShootDelay;

        /// <summary>
        /// Дальность обзора для рейкаста вперед.
        /// </summary>
        [SerializeField] private float m_EvadeRayLength;

        /// <summary>
        /// Кеш ссылка на корабль.
        /// </summary>
        private SpaceShip m_SpaceShip;

        /// <summary>
        /// Текущая точка куда бот должен лететь. Может являтся как статичной, так и какой то динамической.
        /// </summary>
        public Vector3 m_MovePosition;

        /// <summary>
        /// Выбранная ботом цель.
        /// </summary>
        private Destructible m_SelectedTarget;

        [SerializeField] private float m_EvadeTime;
        [SerializeField] private LayerMask m_EvadeLayers;
        private bool isEvade;
        private Ray evadeRay;

        #region Unity events

        private void Start()
        {
            m_SpaceShip = GetComponent<SpaceShip>();
            controller = gameObject.GetComponent<CharacterController>();

            InitActionTimers();
        }

        private void Update()
        {
            UpdateActionTimers();
            UpdateAI();
        }

        #endregion


        /// <summary>
        /// Метод обновления логики AI.
        /// </summary>
        private void UpdateAI()
        {
            switch (m_AIBehaviour)
            {
                case AIBehaviour.Null:
                    break;

                case AIBehaviour.Patrol:
                    UpdateBehaviourPatrol();
                    break;
            }
        }

        /// <summary>
        /// Метод поведения патрулирования.
        /// </summary>
        private void UpdateBehaviourPatrol()
        {
            ActionFindNewMovePosition();
            ActionControlShip();
            ActionEvadeCollision();
        }

        /// <summary>
        /// Действие управления кораблем.
        /// </summary>
        protected virtual void ActionControlShip()
        {
            var moveDirection = m_MovePosition - transform.position;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDirection), m_RotationSpeed*Time.deltaTime);
            controller.Move(moveDirection.normalized*Time.deltaTime*m_SpaceShip.Mobility);
        }

        private void ActionFindNewMovePosition()
        {
            if(isEvade) return;
            if (m_AIBehaviour == AIBehaviour.Patrol)
            {
                // данное условие появится в юните стрельбы, корабль будет лететь до цели.
                if (m_SelectedTarget != null)
                {
                    m_MovePosition = m_SelectedTarget.transform.position;
                }
                else
                if (m_PatrolPoint != null)
                {
                    bool isInsidePatrolZone = (m_PatrolPoint.transform.position - transform.position).sqrMagnitude < m_PatrolPoint.Radius * m_PatrolPoint.Radius;

                    if (isInsidePatrolZone)
                    {
                        GetNewPoint();
                    }
                    else
                    {
                        // если мы не в зоне патруля то едем до нее.
                        m_MovePosition = m_PatrolPoint.transform.position;
                    }
                }

            }

        }

        protected virtual void GetNewPoint()
        {
            // если катаемся внутри зоны патрулирования то выбираем случайную точки внутри.
            if (IsActionTimerFinished(ActionTimerType.RandomizeDirection))
            {
                Vector2 newPoint = UnityEngine.Random.onUnitSphere * m_PatrolPoint.Radius + m_PatrolPoint.transform.position;
                m_MovePosition = newPoint;


                SetActionTimer(ActionTimerType.RandomizeDirection, m_RandomSelectMovePointTime);
            }
        }

        #region Action timers

        /// <summary>
        /// Типы таймеров.
        /// </summary>
        private enum ActionTimerType
        {
            Null,
            RandomizeDirection,
            EvadeTimer,
            MaxValues
        }

        private float[] m_ActionTimers;

        /// <summary>
        /// Инициализируем таймеры. Впринципе можно унести в отдельный класс.
        /// </summary>
        private void InitActionTimers()
        {
            m_ActionTimers = new float[(int)ActionTimerType.MaxValues];
        }

        private void UpdateActionTimers()
        {
            for (int i = 0; i < m_ActionTimers.Length; i++)
            {
                if (m_ActionTimers[i] > 0)
                    m_ActionTimers[i] -= Time.deltaTime;
            }
        }

        private void SetActionTimer(ActionTimerType e, float time)
        {
            m_ActionTimers[(int)e] = time;
        }

        private bool IsActionTimerFinished(ActionTimerType e)
        {
            return m_ActionTimers[(int)e] <= 0; // ВАЖНО: с нулем сравнивать так потому что юнити может влепить таймер в 0
        }

        #endregion
        

        public void SetPatrolBehaviour(AIPointPatrol point)
        {
            m_AIBehaviour = AIBehaviour.Patrol;
            m_PatrolPoint = point;
        }

        #region AI collision evade

        private void ActionEvadeCollision()
        {
            if(Physics.SphereCast(transform.position + transform.up * controller.height,
                   1f, transform.forward, out var hitInfo, m_EvadeRayLength, m_EvadeLayers))
            {
                float angle = Vector3.SignedAngle(hitInfo.point - transform.position, transform.forward, Vector3.up);
                if (angle >= 0)
                {
                    m_MovePosition = transform.position + transform.right;
                }
                else
                {
                    m_MovePosition = transform.position - transform.right;
                }
                
                isEvade = true;
                SetActionTimer(ActionTimerType.EvadeTimer, m_EvadeTime);
            }
            
            if (IsActionTimerFinished(ActionTimerType.EvadeTimer))
            {
                isEvade = false;
            }
        }

        #endregion

        private void OnDrawGizmos()
        {
            if(isEvade)
                Gizmos.color = Color.red;
            else
            {
                Gizmos.color = Color.green;
            }
            Gizmos.DrawRay(transform.position + transform.up * controller.height, transform.forward * m_EvadeRayLength);
            Gizmos.DrawSphere(transform.position + transform.up * controller.height,
                1f);
        }
    }
}