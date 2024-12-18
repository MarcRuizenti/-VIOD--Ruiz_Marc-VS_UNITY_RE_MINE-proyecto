using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementMetiorito : MonoBehaviour
{
    public GameObject objetivo;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minSpeed;
    private float useSpeed;
    private bool canUse = true;
    [SerializeField] private ParticleSystem particleSystem;


    void Update()
    {
        Vector3 direction = objetivo.transform.position - transform.position;

        useSpeed = Random.Range(minSpeed, maxSpeed);

        transform.position += direction * useSpeed * Time.deltaTime;
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
        ParticleSystem temp = Instantiate(particleSystem, transform.position, transform.rotation);

        Destroy(this.gameObject);
    }
}
