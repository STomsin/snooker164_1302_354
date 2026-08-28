using Unity.VisualScripting;
using UnityEngine;

public class Hole : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {

        Balls b = other.GetComponent<Balls>();

        if (b != null)
        {
            if (b.Point == 0)
            {
                GameManager.instance.ShowString();
                Time.timeScale = 0f;
                return;
            }

            GameManager.instance.ShowNotiText(b.Point);
            Destroy(b.gameObject);
            AudioManager.instance.PlaySFX(0);
        }
    }
}