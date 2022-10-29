using UnityEngine;
using UnityEngine.UI;

public class HealthBarPlayer : MonoBehaviour
{
    [SerializeField] private PlayerHealth[] playerHealths;
    [SerializeField] private Image[] totalHealthBarImage;
    [SerializeField] private Image[] currentHealthBarImage;
    
    private void Start()
    {
        for (int i = 0; i < playerHealths.Length; i++)
        {
            if (playerHealths[i].gameObject.activeInHierarchy)
            {
                totalHealthBarImage[i].fillAmount = playerHealths[i].CurrentHealth / 10;
                playerHealths[i].gameObject.SetActive(true);
            }
            else
            {
                playerHealths[i].enabled = false;
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < playerHealths.Length; i++)
        {
            if (playerHealths[i].gameObject.activeInHierarchy)
            {
                currentHealthBarImage[i].fillAmount = playerHealths[i].CurrentHealth / 10;
                playerHealths[i].gameObject.SetActive(true);
            }
            else
            {
                playerHealths[i].enabled = false;
            }
        }
    }
}