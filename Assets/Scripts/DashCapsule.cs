using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashCapsule : MonoBehaviour
{
    [SerializeField] private GameObject capsule;
    private bool respawnBool;
    private float respawnTimer;
    private float respawnTimerInit = 3.0f;


    // Start is called before the first frame update
    void Start()
    {
        respawnTimer = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (respawnBool)
        {
            if(respawnTimer>0)
            {
                respawnTimer -= Time.deltaTime;
            }
            if(respawnTimer<=0)
            {
                MakeInvisible(true);
                respawnBool = false;
                respawnTimer = respawnTimerInit; 
            }

        }
    }

    private void FixedUpdate()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            respawnBool = true;
            MakeInvisible(false);
            Debug.Log("dashcapsuletriggered");
        }
    }

    private void MakeInvisible(bool active)
    {
        if(active)
        {
            GetComponent<Renderer>().enabled = true;
            GetComponent<Collider>().enabled = true;
        }
        else
        {
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
        }
    }
}
