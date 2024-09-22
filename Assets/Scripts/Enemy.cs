using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] int scorePerHit = 15;
    [SerializeField] float destroyDelay = 0.5f;

    ScoreBoard scoreBoard;
    GameObject parentGameObject;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");

        if (scoreBoard == null)
        {
            Debug.LogError("ScoreBoard not found in the scene!");
        }

        if (parentGameObject == null)
        {
            Debug.LogError("GameObject with tag 'SpawnAtRuntime' not found!");
        }

        AddRigidBody();
    }

    void AddRigidBody()
    {
        if (GetComponent<Rigidbody>() == null)
        {
            Rigidbody rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
        }
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        KillEnemy();
    }

    void ProcessHit()
    {
        if (scoreBoard != null)
        {
            scoreBoard.IncreaseScore(scorePerHit);
        }
    }

    void KillEnemy()
    {
        if (deathVFX != null && parentGameObject != null)
        {
            GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
            vfx.transform.SetParent(parentGameObject.transform);
            Destroy(vfx, destroyDelay);
        }

        Destroy(gameObject);
    }
}