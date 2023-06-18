using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class eventColliderTrigger : MonoBehaviour
{
    public UnityEvent Collision;
    public float radius;
    bool Entered = false;
    // Start is called before the first frame update
    public void Update()
    {
        Collider2D[] overlapingGM = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D c in overlapingGM)
        {
            if (c.CompareTag("Player") && !Entered)
            {
                Collision.Invoke();
                Entered = true;
                return;
            }
        }
    }

    private void OnDrawGizmos()
    {
        //UnityEditor.Handles.color = Color.yellow;

        //UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.back, radius);
    }
}
