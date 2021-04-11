using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class PlayerAttackController : NetworkBehaviour
{
    [SerializeField] private float rangeInteract = 30f;
    [SerializeField] private Camera cameraPlayer = null;
    [SerializeField] private Healable heal = null;
    [SerializeField] private Text textHealthPack = null;
    private int healthPack = 0;
    public override void OnStartAuthority()
    {
        enabled = true;
    }

    public void Update()
    {
        if (!hasAuthority) { return; }
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 rayOrigin = cameraPlayer.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
            Debug.DrawRay(transform.position, transform.forward * rangeInteract);
            if (Physics.Raycast(rayOrigin, cameraPlayer.transform.forward, out hit, rangeInteract))
            {
                Debug.Log(hit.transform.gameObject.tag);
                if (hit.transform.gameObject.tag == "PlayerVisual")
                {
                    hit.transform.gameObject.GetComponentInParent<Damageable>().SendMessage("TakeDamage", 25);
                }
                else if (hit.transform.gameObject.tag == "HealthPack")
                {
                    TellServerToDestroyObject(hit.transform.gameObject);
                    healthPack++;
                    textHealthPack.text = healthPack.ToString();
                }
            }
            else
            {
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && healthPack != 0)
        {
            heal.HealDamage(25);
            healthPack--;
            textHealthPack.text = healthPack.ToString();
        }
    }

    [Client]
    public void TellServerToDestroyObject(GameObject obj)
    {
        CmdDestroyObject(obj);
    }

    [Command]
    private void CmdDestroyObject(GameObject obj)
    {
        if (!obj) return;
        NetworkServer.Destroy(obj);
    }
}
