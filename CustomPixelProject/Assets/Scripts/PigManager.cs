using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PigManager : MonoBehaviour
{

    [Header("Pig Settings")]
    public GameObject pigPrefab;
    public Transform spawnPoint;
    public Transform conveyorEndPoint;
    public Transform offScreenPoint;

    [Header("Man-Made Items")]
    public List<Sprite> manMadeItems;

    private Queue<GameObject> pigQueue = new Queue<GameObject>();
    private GameObject currentPig;



    // Start is called before the first frame update
    void Start()
    {
        manMadeItems = manMadeItems.OrderBy(x => Random.value).ToList();

        //Initialize pigs
        foreach (Sprite item in manMadeItems)
        {
            Debug.Log("Shuffled Item: " + item.name);

            GameObject pig =Instantiate(pigPrefab, spawnPoint.position, Quaternion.identity);
            pigPrefab.GetComponent<PigBehavior>().AssignItem(item);
            pigQueue.Enqueue(pig);
        }

        //Call function to bring out the pig
        MoveNextPig();
        
    }

    public void MoveNextPig()
    {
        if(pigQueue.Count > 0)
        {
            currentPig = pigQueue.Dequeue();
            StartCoroutine(MovePigToPosition(currentPig, conveyorEndPoint.position));
        }
        else
        {
            Debug.Log("All pigs processed!");
        }
    }

    public void RemoveCurrentPig()
    {
        if(currentPig != null)
        {
            StartCoroutine(MovePigToPosition(currentPig, offScreenPoint.position, () =>
            { 
            Destroy(currentPig);
            MoveNextPig();
            }));
        }
    }

    private IEnumerator MovePigToPosition(GameObject pig, Vector3 targetPosition, System.Action onComplete = null)
    {
        float speed = 2f;
        while(Vector3.Distance(pig.transform.position, targetPosition) > 0.1f)
        {
            pig.transform.position = Vector3.MoveTowards(pig.transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

        onComplete?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
