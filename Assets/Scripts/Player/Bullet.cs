using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _bulletForce = 500;
    [SerializeField] Rigidbody _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    public void Init(Vector3 position, Vector3 direction)
    {
        gameObject.transform.position = position;

        transform.rotation = Quaternion.LookRotation(direction);
        var rota = transform.localEulerAngles;
        rota.x = 90;
        transform.localEulerAngles = rota;

        _rb.velocity = Vector3.zero;
        gameObject.SetActive(true);
        _rb.AddForce(direction * _bulletForce);
        StartCoroutine(DisableSelf(2f));
    }
    private IEnumerator DisableSelf(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        IHitable hitable = other.GetComponent<IHitable>();
        if (hitable != null)
        {
            hitable.Execute();
            gameObject.SetActive(false);
        }
    }
}
