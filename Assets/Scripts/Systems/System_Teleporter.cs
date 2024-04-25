using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TeleportType
{
    Respawn,
    Lava
}
public class System_Teleporter : MonoBehaviour
{
    public static System_Teleporter Instance;
    [SerializeField] private List<GameObject> _tpPositionsRespawn;

    private bool _isRespawning = false;
    
    private void Awake()
    {
        Instance = this;
    }

    public void Teleport(TeleportType type)
    {
        if (_isRespawning)
            return;
        
        _isRespawning = true;
        if (type == TeleportType.Respawn)
        {
            if (_tpPositionsRespawn.Count <= 0)
            {
                Debug.Log("GameOVER");
                return;
            }

            StartCoroutine(nameof(TeleportingSequence_Respawn));
        }
    }
    
    private IEnumerator TeleportingSequence_Respawn()
    {
        PlayerController.Instance.Hide(true);
        yield return new WaitForSeconds(2);

        if (!System_GRU.Instance.IsSlumbers)
            System_GRU.Instance.Gru_Sleep();

        Transform newpos = _tpPositionsRespawn[0].transform;
        PlayerController.Instance.transform.position = newpos.position;
        yield return new WaitForSeconds(1);

        Destroy(_tpPositionsRespawn[0]);
        _tpPositionsRespawn.RemoveAt(0);
        PlayerController.Instance.Hide(false);
        PlayerController.Instance.Movement._isClimbing = false;
        PlayerController.Instance.Restore();
        _isRespawning = false;
    }
}
