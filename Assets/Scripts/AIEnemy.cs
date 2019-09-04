using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemy : MonoBehaviour
{
    [Header ("Path")]
    public GameObject[] waypoints;
    public GameObject player;
    [Header("Enemy Properties")]
    public Vector3 currentPosition;
    public float MovementSpeed;
    public float TurningSpeed;
    public bool ischasing;
    public Animator EnemyAnim;
    [Header("Enemy GunShoot")]
    public GameObject Bullet;
    public Transform BulletSpawnPositon;
    public float BulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
        EnemyAnim = GetComponent<Animator>();
        MovementSpeed = 10f;
        BulletSpeed = 200f;
        StartCoroutine(EnemyMovement());
        StartCoroutine(Fire());
        currentPosition = transform.position;

    }
    IEnumerator MoveToward(Transform target,GameObject[] Destination,float Speed)
    {
        foreach(var dest in Destination)
        {
            while (Vector3.Distance(dest.transform.position,target.position)>=1f)
            {
                currentPosition = target.position;
                if ((Vector3.Distance(player.transform.position, target.position) <80f )|| (ischasing))
                {
                    ischasing = true;
                    target.position = Vector3.MoveTowards(target.position, player.transform.position, MovementSpeed * Time.deltaTime);
                    var PlayerPosition = player.transform.position;
                    PlayerPosition.y = transform.position.y;
                    transform.LookAt(PlayerPosition);
                    EnemyAnim.SetBool("Walking", true);
                }
                if (Vector3.Distance(player.transform.position, target.position) < 50f)
                {
                    ischasing = false;
                    target.position = currentPosition;
                    EnemyAnim.SetBool("Walking", false);
                }
                else
                {
                    target.position = Vector3.MoveTowards(target.position, dest.transform.position, Speed * Time.deltaTime);
                    EnemyAnim.SetBool("Walking", true);
                }
                yield return new WaitForSeconds(.001f);
                var targetPosition =dest.transform.position;
                targetPosition.y = transform.position.y;
                transform.LookAt(targetPosition);
            }

        }
    }
    IEnumerator EnemyMovement()
    {
        yield return MoveToward(this.gameObject.transform,waypoints , MovementSpeed);
        if(GetComponent<DeathLogic>().Dead==false)
        StartCoroutine(EnemyMovement());
    }

    IEnumerator GunShoot(Transform Destination,Transform target)
    {
        while (Vector3.Distance(Destination.position, target.position) <= 51f)
        {
            var Shoot = Instantiate(Bullet, BulletSpawnPositon.transform.position, Quaternion.identity);
            Shoot.GetComponent<Rigidbody>().velocity = transform.forward * BulletSpeed;
            GetComponent<Animator>().SetBool("Fire", true);
            yield return new WaitForSeconds(.01f);
            GetComponent<Animator>().SetBool("Fire", false);
            yield return new WaitForSeconds(2f);
        }
    }
    IEnumerator Fire()
    {
        yield return GunShoot(player.transform, transform);
        StartCoroutine(Fire());
    }
}
