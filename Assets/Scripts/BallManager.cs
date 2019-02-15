using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public enum States
    {
        Idle,
        InPlay,
        Died,
        Dead,
        Win,
        Won
    };

    public States state;
    public Animator animator;

    private Vector3 _startPosition;

	// Use this for initialization
	void Start ()
    {
        state = States.Idle;
        animator = GetComponent<Animator>();
        _startPosition = transform.position;
	}

    void Die ()
    {
        state = States.Died;
        animator.SetTrigger("Die");
    }

    void Dead ()
    {
        state = States.Dead;
        gameObject.SetActive(false);
    }

    void Win ()
    {
        state = States.Win;
        animator.SetTrigger("Win");
    }

    void Won ()
    {
        state = States.Won;
        transform.localScale = new Vector3(1f, 1f, 1f);
        gameObject.SetActive(false);
    }

    public void Restart ()
    {
        state = States.Idle;
        gameObject.SetActive(true);
        transform.position = this._startPosition;
    }

    // Update is called once per frame
    void Update ()
    {

	}

    void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.CompareTag("Planet"))
        {
            Die();
        }
        if (collision.CompareTag("Goal"))
        {
            Win();
        }
    }
}
