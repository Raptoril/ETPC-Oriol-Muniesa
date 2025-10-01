using System;
using UnityEngine;

public class SnakeFood : MonoBehaviour
{
    public GameObject food;
    public Vector2 gridsize = new Vector2(40, 30);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void SpawnFood()
    {
        //float x = UnityEngine.Random.Range(-spawnTrigger.bounds.min.x, spawnTrigger.bounds.max.x);
        //float y = UnityEngine.Random.Range(-spawnTrigger.bounds.min.y, spawnTrigger.bounds.max.y);

        float x = UnityEngine.Random.Range(-gridsize.x / 2, gridsize.x / 2);
        float y = UnityEngine.Random.Range(-gridsize.y / 2, gridsize.y / 2);
        GameObject foodObject = Instantiate(food, new Vector2(Mathf.Round(x), Mathf.Round(y)), Quaternion.identity);
        foodObject.name = "SnakeFood";
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("SnakeHead"))
        {
            SnakeController ctr = col.GetComponent<SnakeController>();
            ctr.Grow();

            SpawnFood();
            Destroy(gameObject);
        }
    }
}
