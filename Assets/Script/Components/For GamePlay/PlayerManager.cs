using CommandChoice.Data;
using CommandChoice.Model;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace CommandChoice.Component
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private int HP = DataGlobal.HpDefault;
        [field: SerializeField] public int Mail { get; private set; } = DataGlobal.MailDefault;
        [SerializeField] private LayerMask LayerStopMove;
        [Header("Fields Auto Set")]
        [SerializeField] private Text textHP;
        [SerializeField] private Text textMail;
        [SerializeField] private Vector3 startSpawn;

        void Awake()
        {
            textHP = GameObject.FindGameObjectWithTag(StaticText.TagHP).transform.GetComponentInChildren<Text>();
            textMail = GameObject.Find(StaticText.UiMail).transform.GetComponentInChildren<Text>();
        }

        void Start()
        {
            UpdateText();
            startSpawn = transform.position;
        }

        public void ResetGame()
        {
            HP = DataGlobal.HpDefault;
            Mail = DataGlobal.MailDefault;
            UpdateText();
            transform.position = startSpawn;
        }

        public void PlayerMoveUp()
        {
            Vector2 transformPlayer = transform.position;
            transformPlayer.y += 1;
            CheckMovement(transformPlayer);
        }

        public void PlayerMoveDown()
        {
            Vector2 transformPlayer = transform.position;
            transformPlayer.y -= 1;
            CheckMovement(transformPlayer);
        }

        public void PlayerMoveLeft()
        {
            Vector2 transformPlayer = transform.position;
            transformPlayer.x -= 1;
            CheckMovement(transformPlayer);
        }

        public void PlayerMoveRight()
        {
            Vector2 transformPlayer = transform.position;
            transformPlayer.x += 1;
            CheckMovement(transformPlayer);
        }

        private void CheckMovement(Vector2 newMovement)
        {
            if (Physics2D.OverlapCircle(newMovement, 0.2f, LayerStopMove)) return;
            transform.position = newMovement;
        }

        public void TakeDamage()
        {
            HP--;
            UpdateText();
        }

        public void Heal()
        {
            HP++;
            UpdateText();
        }

        public void GetMail(int getCountMail)
        {
            Mail += getCountMail;
            if (Mail > 3) Mail = 3;
            UpdateText();
        }
        public void DropMail(int dropCountMail)
        {
            Mail += dropCountMail;
            if (Mail < 0) Mail = 0;
            UpdateText();
        }

        void UpdateText()
        {
            textHP.text = $"x {HP}";
            textMail.text = $"x {Mail}";
        }

        // void OnTriggerEnter2D(Collider2D other)
        // {
        //     print(other.gameObject.name);
        // }
    }
}