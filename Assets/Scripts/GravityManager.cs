using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    public GameObject ball;
    public GameObject planets;
    public GameObject ui;
    private BallManager.States _lastBallState;
    private BallManager _ballManager;
    private Vector3 _ballVelocity;

	// Use this for initialization
	void Start ()
    {
        _ballManager = ball.GetComponent<BallManager>();
        SetLastBallState();
        _ballVelocity = new Vector3(0.0f, 0.0f);
	}

    void SetLastBallState()
    {
        _lastBallState = _ballManager.state;
    }

    bool TransitioningTo(BallManager.States state)
    {
        return _lastBallState != state && _ballManager.state == state;
    }

    void ShowWinUI()
    {
        foreach (Transform child in ui.transform)
        {
            child.gameObject.SetActive(child.name == "WinText" || child.name == "Restart");
        }
    }

    void ShowGameOverUI()
    {
        foreach (Transform child in ui.transform)
        {
            child.gameObject.SetActive(child.name == "GameOverText" || child.name == "Restart");
        }
    }

    void HideUI()
    {
        foreach (Transform child in ui.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        if (TransitioningTo(BallManager.States.Won))
        {
            ShowWinUI();
        }

        if (TransitioningTo(BallManager.States.Dead))
        {
            ShowGameOverUI();
        }

        if (TransitioningTo(BallManager.States.Idle))
        {
            HideUI();
        }

        if (_ballManager.state == BallManager.States.InPlay)
        {
            Vector3 force = new Vector3(0.0f, 0.0f);
            float g = 1.0f;
            float m1 = 8.0f;
            float m2 = 1.0f;

            foreach (Transform child in planets.transform)
            {
                Vector3 delta = child.position - ball.transform.position;
                float f = g * m1 * m2 / Mathf.Pow(delta.magnitude, 2);
                float theta = Mathf.Atan2(delta.y, delta.x);
                force += new Vector3(Mathf.Cos(theta) * f, Mathf.Sin(theta) * f);
            }

            _ballVelocity += force / m2 * Time.fixedDeltaTime;
            ball.transform.Translate(_ballVelocity * Time.fixedDeltaTime);
        }

        SetLastBallState();
    }

    public void Shoot(Vector3 velocity)
    {
        _ballVelocity = velocity;
        _ballManager.state = BallManager.States.InPlay;
    }

    public void Restart()
    {
        _ballManager.Restart();
        _ballVelocity = new Vector3(0.0f, 0.0f);
    }
}