using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PigManager : MonoBehaviour
{
    [Header("Pig Settings")]
    public GameObject pigPrefab;
    public Transform spawnPoint;
    public Transform conveyorEndPoint;
    public Transform offScreenPoint;
    public Transform parentTransform;
    public GameObject painOverlay;
    public cursorUI cursorButton;

    [Header("Man-Made Items")]
    public List<Sprite> manMadeItems;
    public GameObject itemSpawn;
    
    private Queue<GameObject> pigQueue = new Queue<GameObject>();
    private GameObject currentPig;    
    public Sprite assignedItem;

    public spriteSwitch spriteSwitch;


    private bool isPigInPos = false;

    public string nextSceneName = "End_Cutscene";


    // Start is called before the first frame update
    void Start()
    {
        //Randomize the list of items
        manMadeItems = manMadeItems.OrderBy(x => Random.value).ToList();        


        //spriteSwitch.PassingList(manMadeItems);

        //Initialize pigs
        foreach (Sprite item in manMadeItems)
        {
            int index = manMadeItems.IndexOf(item);            

            //Instantiate pig at spawnpoint. pig clones go under pig parent
            GameObject pig = Instantiate(pigPrefab, spawnPoint.position, Quaternion.identity, parentTransform);

            PigBehavior pigBehavior = pig.GetComponent<PigBehavior>();
            pigBehavior.assignedItem = item;            

            pigQueue.Enqueue(pig);
        }

        //Call function to bring out the pig from right side of screen
        MoveNextPig();        
    }    

    public void MoveNextPig()
    {
        cursorButton.resetDecisionMade();
        if(pigQueue.Count > 0)
        {
            currentPig = pigQueue.Dequeue();
            StartCoroutine(MovePigToPosition(currentPig, conveyorEndPoint.position));
        }
        else
        {
            Debug.Log("All pigs processed!");
            SceneManager.LoadScene(nextSceneName);
        }
    }

    // Moves pig from center >> out of screen
    public void RemoveCurrentPig()
    {
        if(currentPig != null)
        {
            isPigInPos = false;
            StartCoroutine(MovePigToPosition(currentPig, offScreenPoint.position, () =>
            { 
                //Destroys asset
            Destroy(currentPig);
                //Brings out new pig
            MoveNextPig();
            }));
        }
    }

    // Creates pig movement
    private IEnumerator MovePigToPosition(GameObject pig, Vector3 targetPosition, System.Action onComplete = null)
    {
        //Set speed pig moves on conveyor belt
        float speed = 8f;


        while(Vector3.Distance(pig.transform.position, targetPosition) > 0.1f)
        {
            pig.transform.position = Vector3.MoveTowards(pig.transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

        if (targetPosition == conveyorEndPoint.position)
        {
            isPigInPos = true;
        }

        onComplete?.Invoke();
    }

    public bool getIfPigInPos()
    {
        return isPigInPos;
    }
}
