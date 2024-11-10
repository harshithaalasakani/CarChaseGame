using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CarController : MonoBehaviour
{
    [SerializeField]
    private float speed = 25f, rotationSpeed = 180f;

    [SerializeField]
    private bool playerCar = false;

    [SerializeField]
    private GameObject explosion;

    private Rigidbody myBody;

    private float translation, rotation;
    private GameObject target;
    private void Awake()
    {
        myBody = GetComponent<Rigidbody>();
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }
    private void Start()
    {
        target = GameObject.FindWithTag("Player");
        
    }
    
    private void Update()
    {
        if (!playerCar && !target)
        {
            return;
        }
        if(playerCar)
        {
            if(Mathf.Abs(Input.GetAxis("Vertical")) > 0)
            {
                rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
                translation = Input.GetAxis("Vertical") * speed;
            }
            else
            {
                rotation = 0f;
                translation = 0f;
            }
        }
        else
        {
            Vector3 targetDirection = transform.position - target.transform.position;
            targetDirection.Normalize();
            rotation = Vector3.Cross(targetDirection, transform.forward).y;
        }
        Invoke("Restart", 5f);
    }

    private void FixedUpdate()
    {
        if (playerCar)
        {
            myBody.velocity = transform.forward * translation;
            transform.Rotate(Vector3.up * rotation);
        }
        else
        {
            myBody.angularVelocity = rotationSpeed * rotation * new Vector3(0, 1, 0);
            myBody.velocity = transform.forward * speed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Instantiate(explosion, transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
            Destroy(collision.gameObject);
            Destroy(gameObject);
            
        }
    }
}
