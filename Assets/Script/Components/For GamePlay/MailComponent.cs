using UnityEngine;

namespace CommandChoice.Component
{
    public class MailComponent : MonoBehaviour
    {
        public void ResetGame()
        {
            gameObject.SetActive(true);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                PlayerManager player = other.gameObject.GetComponent<PlayerManager>();
                player.GetMail();
                gameObject.SetActive(false);
            }
        }
    }
}