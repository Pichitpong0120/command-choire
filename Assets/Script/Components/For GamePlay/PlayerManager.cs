using CommandChoice.Data;
using UnityEngine;
using UnityEngine.UI;

namespace CommandChoice.Component
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private int HP = DataGlobal.HpDefault;
        [field: SerializeField] public int Mail { get; private set; } = DataGlobal.MailDefault;
        [SerializeField] private Text textHP;
        [SerializeField] private Text textMail;
        [SerializeField] private Vector3 startSpawn;

        void Awake()
        {
            textHP = GameObject.FindGameObjectWithTag("HP").transform.GetComponentInChildren<Text>();
            textMail = GameObject.Find("Ui Mail").transform.GetComponentInChildren<Text>();
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
            transform.position = transformPlayer;
        }

        public void PlayerMoveDown()
        {
            Vector2 transformPlayer = transform.position;
            transformPlayer.y -= 1;
            transform.position = transformPlayer;
        }

        public void PlayerMoveLeft()
        {
            Vector2 transformPlayer = transform.position;
            transformPlayer.x -= 1;
            transform.position = transformPlayer;
        }

        public void PlayerMoveRight()
        {
            Vector2 transformPlayer = transform.position;
            transformPlayer.x += 1;
            transform.position = transformPlayer;
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

        public void GetMail()
        {
            Mail++;
            UpdateText();
        }
        public void DropMail()
        {
            Mail--;
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