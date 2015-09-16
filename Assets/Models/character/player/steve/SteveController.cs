using UnityEngine;
using System.Collections;

public class SteveController : MonoBehaviour {

    public float speed = 1f;

    private Animator animator;
    private Vector3 nextPos;

	void Start () {
        animator = GetComponent<Animator>();
        nextPos = transform.position;
	}

	void Update () 
    {
        if( Input.GetKeyDown(KeyCode.W))
        {
            
            animator.SetFloat(Parameters.speed, speed);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -90f);
            animator.SetFloat(Parameters.speed, speed);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.Rotate(Vector3.up, 180);
            animator.SetFloat(Parameters.speed, speed);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Rotate(Vector3.up, 90f );
            animator.SetFloat(Parameters.speed, speed);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetFloat(Parameters.speed, 0);
        }

	}

    public void Move( Vector3 nextPosition )
    {

        Vector3 directPosition = nextPosition;
        Vector3 myPos = transform.position;

        directPosition.y = 0;                   //evita problemi di angoli sbagliati
        myPos.y = 0;

        Vector3 direction = directPosition - myPos;
        transform.rotation = Quaternion.LookRotation(direction);
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, Time.deltaTime * speed);

        animator.SetFloat(Parameters.speed, speed);
    }

    internal void Stop()
    {

    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.gameObject.transform.position, Vector3.forward);
        Gizmos.DrawLine(this.gameObject.transform.position, Vector3.back);
    }
}

internal struct Parameters
{
    public const string speed = "speed";
}
