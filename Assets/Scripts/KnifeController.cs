using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KnifeController : MonoBehaviour
{
    private KnifeManager knifeManager;

    private Rigidbody2D _knifeRigidbody;

    private bool _canShoot;

    [SerializeField]
    private float _moveSpeed;

    void Start()
    {
        GetComponentValues();
    }


    void Update()
    {
        HandleShootInput();
    }

    private void FixedUpdate()
    {
        Shoot();
    }


    private void HandleShootInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            knifeManager.SetDisableKnifeIconColor();
            _canShoot = true;
        }

    }



    private void GetComponentValues()
    {
        _knifeRigidbody = GetComponent<Rigidbody2D>();
        knifeManager = GameObject.FindObjectOfType<KnifeManager>();
    }

    private void Shoot()
    {
        if (_canShoot)
        {
            _knifeRigidbody.AddForce(Vector2.up * _moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Circle"))
        {
            knifeManager.SetActiveKnife();
            _canShoot = false;
            _knifeRigidbody.isKinematic = true;
            transform.SetParent(collision.gameObject.transform);
        }

        if (collision.gameObject.CompareTag("Knife"))
        {
            SceneManager.LoadScene(0);
        }

    }

}
