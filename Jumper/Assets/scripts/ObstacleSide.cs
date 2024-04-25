using UnityEngine;

public class ObstacleSide : MonoBehaviour
{
    public float Movespeed = 3.5f;


    private void Update()
    {

        this.transform.Translate(Vector3.left * Movespeed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("wallend") == true)
        {
            Destroy(this.gameObject);
        }
    }
}