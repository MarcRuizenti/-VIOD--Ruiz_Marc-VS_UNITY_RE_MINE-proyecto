using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementMetiorito : MonoBehaviour
{
    public GameObject objetivo;
    [SerializeField] private float speed;
    private bool canUse = true;
    void Update()
    {
        Vector3 direction = objetivo.transform.position - transform.position;

        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CubeLogic>() && canUse)
        {
            canUse = false;
            if (other.GetComponent<CubeLogic>().IAmclick) GameManager.Instance.timerCounter -= 20;
            else
            {
                other.GetComponent<CubeLogic>().Click();
            }
        }

        Destroy(this.gameObject);
    }
}
