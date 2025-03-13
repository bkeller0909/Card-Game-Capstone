using System.Collections;
using UnityEngine;

//----------------------------------------------------------------
//  Author:         Keller
//  Co-Author:
//  Title:          PlayerCameraHandler
//  Date Created:   March 11, 2025
//  Instance:       No
//-----------------------------------------------------------------


/// <summary>
/// Handles the camera shake.
/// 
/// Attached to the player game object.
/// </summary>
public class PlayerCameraHandler : MonoBehaviour
{
    [HideInInspector] public CameraShake playerCameraShake;

    private void Awake()
    {
        playerCameraShake = GetComponentInChildren<CameraShake>();
    }

    /// <summary>
    /// Shakes the player camera.
    /// </summary>
    /// <param name="shakeIntensity">Intensity of the shake. Use a VERY LOW float value. Preferabley under 0.1f</param>
    /// <param name="shakeTimer">Duration of the camera shake.</param>
    public void CameraShake(float shakeIntensity, float shakeTimer)
    {
        StartCoroutine(DoCameraShake(shakeIntensity, shakeTimer, GameManager.Instance.player1, GameManager.Instance.player2));
    }

    private IEnumerator DoCameraShake(float shakeIntensity, float shakeTimer, PlayerManager player, PlayerManager enemy)
    {
        playerCameraShake.enabled = true;
        playerCameraShake.intensity = shakeIntensity;
        yield return new WaitForSeconds(shakeTimer);
        playerCameraShake.transform.position = playerCameraShake.initialPos;
        playerCameraShake.enabled = false;

        //if the total amout of spells for both players is over then send both to the default camera view after shake, otherwise keep reseting the correct player to the qte camera
        if (GameManager.Instance.totalSpellsPickedP1 == 0 && GameManager.Instance.totalSpellsPickedP2 == 0)
        {
            player.gameObject.GetComponentInChildren<CameraPositionChange>().GetInputForced(0);
            enemy.gameObject.GetComponentInChildren<CameraPositionChange>().GetInputForced(0);
        }
        else
        {
            gameObject.GetComponentInChildren<CameraPositionChange>().GetInputForced(3);
        }
    }
}
