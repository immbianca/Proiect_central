using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Cinemachine;

public class Level1Script : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cameraPlayer;
    [SerializeField] private CinemachineVirtualCamera cameraEnemy;

    private void Awake()
    {
        if (CaracterManager.instance != null)
        {
            CaracterManager.instance.ApplyCaracterTo(gameObject);
        }
        else
        {
            Debug.LogWarning("CaracterManager instance is not set. Ensure it is initialized before using Level1Script.");
        }
    }

    private async void Start()
    {
        if (cameraPlayer != null && cameraEnemy != null)
        {
            await Task.Delay(4000);

            cameraPlayer.Priority = 10;
            cameraEnemy.Priority = 11;

            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlaySFX(AudioManager.instance.enemySound);
            }

            await Task.Delay(3000);

            cameraPlayer.Priority = 11;
            cameraEnemy.Priority = 10;
        }
    }
}
