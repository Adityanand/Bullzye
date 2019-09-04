using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathLogic : MonoBehaviour
{
    public bool Dead;
    public bool RotateG;
    public float time;
    public GameObject TheCity;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        time = 5;
    }
    private void Update()
    {
        if (Dead)
        {
            time = time - 1*Time.deltaTime;
            GetComponent<AIEnemy>().StopAllCoroutines();
            GetComponent<AIEnemy>().enabled = false;
            GetComponent<Animator>().SetBool("Walking", false);
            GetComponent<Animator>().SetBool("Fire", false);
            GetComponent<Animator>().SetBool("Death", true);
            
            //if (Physics.gravity == new Vector3(0, -9.81f, 0)&&RotateG)
            //{
            //    Physics.gravity = new Vector3(0, 9.81f, 0);
            //    TheCity.transform.Rotate(0, 0, 180);
            //    RotateG = false;
            //}
            //else if (Physics.gravity == new Vector3(0, 9.81f, 0)&&RotateG)
            //{
            //    Physics.gravity = new Vector3(0, -9.81f, 0);
            //    TheCity.transform.Rotate(0, 0, 180);
            //    RotateG = false;
            //}
        }
        if (time <= 0)
            Destroy(this.gameObject);
    }
    private void OnCollisionEnter(Collision col)
    {
        if(col.collider.gameObject.tag=="Bullet")
        {
            Dead = true;
            RotateG = true;
            GetComponent<BoxCollider>().enabled=false;
            GetComponent<Rigidbody>().useGravity = false;
        }
    }
}
