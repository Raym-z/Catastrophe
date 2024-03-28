using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] GameObject teleporterSource;
    [SerializeField] GameObject teleporterTarget;
    [SerializeField] float overlapRadius = 1f;

    [SerializeField] float teleportDelay = 1f;

    bool isTeleporting = false;

    private void Update()
    {
        Collider2D area = Physics2D.OverlapCircle(transform.position, 1f, LayerMask.GetMask("Player"));
        if (area != null && isTeleporting == false)
        {
            StartCoroutine(Teleport(area.gameObject));
        }
    }

    IEnumerator Teleport(GameObject objectToTeleport)
    {
        isTeleporting = true;
        yield return new WaitForSeconds(teleportDelay);

        if (teleporterTarget != null)
        {
            objectToTeleport.transform.position = teleporterTarget.transform.position;
            // Optionally, you might want to adjust rotation or other properties here
        }

        isTeleporting = false;
    }

    void OnDrawGizmosSelected()
    {
        // Set the color of the gizmo to cyan
        Gizmos.color = Color.red;

        // Draw the overlap circle in the Scene view
        Gizmos.DrawWireSphere(transform.position, overlapRadius);
    }
}
