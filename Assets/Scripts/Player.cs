using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Properties")]
    //public Camera playerHead;
    public float MovementSpeed;
    public float RunningSpeed;
    public float TurningSpeed;
    public float SideMovement;
    public float jumpforce;
    public bool isjumping;
    public bool isCrouching;
    public int Health;
    public Animator PlayerAnim;
    //public float speedH = 2.0f;
    //public float speedV = 2.0f;

    //private float yaw = 0.0f;
    //private float pitch = 0.0f;

    [Header("Gun Properties")]
    public GameObject bullet;
    public GameObject spawnPosition;
    public float bulletSpeed;


    // Start is called before the first frame update
    void Start()
    {
        MovementSpeed = 10f;
        RunningSpeed = 20f;
        TurningSpeed = 30f;
        SideMovement = 10f;
        jumpforce = 500f;
        Health = 100;
        StartCoroutine(GunShoot());
        bulletSpeed = 200f;
        PlayerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) && (Input.GetKey(KeyCode.LeftShift)))
        {
            transform.Translate(Vector3.forward * RunningSpeed * Time.deltaTime);
            PlayerAnim.SetBool("Running", true);
            PlayerAnim.SetBool("Walking", false);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * MovementSpeed * Time.deltaTime);
            PlayerAnim.SetBool("Running", false);
            PlayerAnim.SetBool("Walking", true);
        }
        else
        {
            PlayerAnim.SetBool("Running", false);
            PlayerAnim.SetBool("Walking", false);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-Vector3.forward * MovementSpeed * Time.deltaTime);
            PlayerAnim.SetBool("Backward Walking", true);
        }
        else
        {
            PlayerAnim.SetBool("Backward Walking", false);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.down * TurningSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * TurningSpeed * Time.deltaTime);
        }
        //if (isCrouching)
        //{
        //    this.gameObject.GetComponent<Animator>().SetBool("Crouching", true);
        //}
        if (Input.GetKeyDown(KeyCode.Space) && !isjumping)
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1, 0) * jumpforce);
            isjumping = true;
            PlayerAnim.SetBool("Jump", true);
        }
        else
            PlayerAnim.SetBool("Jump", false);
        //yaw += speedH * Input.GetAxis("Mouse X");
        //pitch -= speedV * Input.GetAxis("Mouse Y");

        //playerHead.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        //var CharacterRotation = playerHead.transform.rotation;
        //CharacterRotation.x = 0;
        //CharacterRotation.z = 0;

        //transform.rotation = CharacterRotation;

    }
    IEnumerator GunShoot()
    {
        var Shoot=Instantiate(bullet, spawnPosition.transform.position, Quaternion.identity);
        Shoot.GetComponent<Rigidbody>().velocity=transform.forward*bulletSpeed;
        GetComponent<Animator>().SetBool("Fire", true);
        yield return new WaitForSeconds(.01f);
        GetComponent<Animator>().SetBool("Fire", false);
        yield return new WaitForSeconds(2f);
        StartCoroutine(GunShoot());
    }
    public void OnCollisionEnter(Collision col)
    {
        if (col.collider.gameObject.tag == "Ground")
        {
            isjumping = false;
        }
    }
}
