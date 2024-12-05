using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cut_Action : MonoBehaviour
{
    public GameObject cutPrefab;
    public List<Sprite> cutSprites; // List of sprites to choose from
    public float spacing = 0.1f;
    public GameObject painOverlay;
    public GameObject deadOverlay;    
    public Transform parentTransform;
    public AudioSource pigPain;
    private List<Vector3> cutPath = new List<Vector3>();
    private Vector3 lastPosition;
    public Image healthBar;
    public double healthAmount = 100f;
    public float dwellIncrement = 0.1f;
    public float maxScaleFactor = 1.5f;
    public PigBehavior pigBehavior;
    private Dictionary<Vector3, float> dwellTimes = new Dictionary<Vector3, float>(); // To track dwell times
    public CursorScript cursorScript;
    public PigManager pigManager;


    void Start()
    {
        // Turn face off
        painOverlay.SetActive(false);
        deadOverlay.SetActive(false);
        
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        // Reference PigBehavior script
        pigBehavior = GetComponent<PigBehavior>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            cutPath.Clear();
            lastPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lastPosition.z = 0;
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPosition.z = 0;

            // Raycast to detect if the mouse is inside pig sprite
            RaycastHit2D hit = Physics2D.Raycast(currentPosition, Vector2.zero);

            // If cursor is inside pig sprite
            if (hit.collider != null && hit.collider.gameObject == this.gameObject && cursorScript.getMode() == "KNIFEMODE" && pigManager.getIfPigInPos() )
            {
                // Check the position of sprites and spacing of sprites (defined in inspector)
                if (Vector3.Distance(currentPosition, lastPosition) >= spacing)
                {
                    cutPath.Add(currentPosition);
                    lastPosition = currentPosition;

                    // Generate a random rotation
                    Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));

                    // Instantiate the cut prefab at the current position
                    GameObject newCut = Instantiate(cutPrefab, currentPosition, randomRotation, parentTransform);

                    // Assign a random sprite
                    SpriteRenderer spriteRenderer = newCut.GetComponent<SpriteRenderer>();
                    if (spriteRenderer != null && cutSprites.Count > 0)
                    {
                        spriteRenderer.sprite = cutSprites[Random.Range(0, cutSprites.Count)];
                    }

                    // Track dwell time
                    if (!dwellTimes.ContainsKey(currentPosition))
                        dwellTimes[currentPosition] = 0;

                    // Start coroutine to increase size based on dwell time
                    StartCoroutine(ScaleOverTime(newCut.transform, currentPosition));

                    // Take damage amount
                    TakeDamage(10);
                }
            }
        }
    }

    public void TakeDamage(double damage)
    {
        pigBehavior.happyOverlay.SetActive(false);
        // Show pig sad face
        painOverlay.SetActive(true);
        // Play audio
        pigPain.Play();
        //Subtracts from health amount
        healthAmount -= damage;
        //Removes green bar from health bar UI
        healthBar.fillAmount = (float)(healthAmount / 100f);
        //When health is at zero
        if (healthAmount == 0)
        {
            painOverlay.SetActive(false);
            deadOverlay.SetActive(true);            

            //Trigger particle explosion from pigBehavior script
            pigBehavior.TriggerExplosion();
        }

        // Remove green bar from health bar UI
        healthBar.fillAmount = (float)(healthAmount / 100f);
    }



    //Cut sprites
    private IEnumerator ScaleOverTime(Transform cutTransform, Vector3 position)
        {
            SpriteRenderer spriteRenderer = cutTransform.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null) yield break; // Ensure the prefab has a SpriteRenderer

            // Initialize base size
            Vector3 baseScale = cutTransform.localScale;



            while (dwellTimes.ContainsKey(position))
            {
                dwellTimes[position] += Time.deltaTime;

                // Incrementally grow the sprite
                float scaleFactor = Mathf.Clamp(1 + dwellTimes[position] * dwellIncrement, 1, maxScaleFactor);

                // Apply constrained scaling
                cutTransform.localScale = baseScale * scaleFactor;

                // Stop scaling when max size is reached
                if (scaleFactor >= maxScaleFactor) break;

                yield return null;
            }
        }

}
