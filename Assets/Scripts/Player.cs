using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2 input;
    Rigidbody2D rb2D;
    Vector2 facingDirection;

    [SerializeField] float charSpeed = 2f;

    [Header("Pipe Placement")]
    [SerializeField] Grid grid;
    [SerializeField] float grabRange = 1.5f;
    List<Pipe> currentPipes = new List<Pipe>();

    [SerializeField] Transform pipeHoldPos;
    [SerializeField] SpriteRenderer projectionSprite;
    [SerializeField] Color placableColor = Color.white, unplacableColor = Color.red;
    Vector3 projectionTarget;
    bool canPlace;

    [SerializeField] LayerMask pipeLayer;
    [SerializeField] LayerMask placementCheckLayer;

    [Header("Visual")]
    [SerializeField] SpriteRenderer playerSprite;

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent<Rigidbody2D>(out rb2D);

    }

    // Update is called once per frame
    void Update()
    {
        ReceiveInput();
        UpdateDirection();

        PickUp_Place();
        Movement();
    }

    void ReceiveInput()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"),
                            Input.GetAxisRaw("Vertical"));
        input.Normalize();

        if (Input.GetKeyDown(KeyCode.Q) && (currentPipes.Count > 0))
        {
            currentPipes[0].Rotate();
        }

    }

    //can move
    void Movement()
    {
        // if not in topdown mode then apply gravity
        var velY = input.y * charSpeed;
        var velX = input.x * charSpeed;

        rb2D.velocity = new Vector2(velX, velY); // Use velY if isTopDown is true,
                                                                            // else use rb's y velocity.
    }
    
    void UpdateDirection()
    {
        facingDirection = Vector2.Lerp(facingDirection, input, input.sqrMagnitude);
        var rotation = Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0, 0, 90) * facingDirection);
        playerSprite.transform.rotation = Quaternion.Lerp(playerSprite.transform.rotation, rotation, Time.deltaTime * 15);
    }

    //can pickup pipe
    void PickUp_Place()
    {
        PlacementProjection();

        if (currentPipes.Count > 0)
        {
            currentPipes[0].transform.position = Vector3.Lerp(currentPipes[0].transform.position, pipeHoldPos.position, Time.deltaTime * 15);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentPipes.Count <= 0) //PickUp
            {
                //shoot raycast
                RaycastHit2D hit = Physics2D.Raycast(transform.position, facingDirection, grabRange, pipeLayer);

                print(hit.collider);

                if (hit)
                {
                    hit.collider.gameObject.TryGetComponent<Pipe>(out var pipe);
                    currentPipes.Add(pipe);

                    currentPipes[0].SetLiftState(true);
                }
            }
            else if (canPlace) //Place
            {
                currentPipes[0].transform.SetParent(null);
                LeanTweenExt.LeanMove(currentPipes[0].gameObject, projectionTarget, .1f).setEaseInOutQuad();
                currentPipes[0].SetLiftState(false);

                currentPipes.Clear();
                
            }
        }
    }

    void PlacementProjection()
    {
        projectionSprite.gameObject.SetActive(false);

        if (currentPipes.Count <= 0)
            return;

        canPlace = (Physics2D.OverlapBox(projectionTarget, grid.cellSize, 0f, placementCheckLayer) == null);

        //Projection Placement
        projectionSprite.gameObject.SetActive(true);

        Vector3Int cellPosition = grid.WorldToCell(transform.position + (Vector3)facingDirection * grabRange);
        projectionTarget = grid.GetCellCenterWorld(cellPosition);

        projectionSprite.transform.position = Vector3.MoveTowards(projectionSprite.transform.position, projectionTarget, Time.deltaTime * 20);

        //Projection Color
        if (canPlace)
            projectionSprite.color = placableColor;
        else
            projectionSprite.color = unplacableColor;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + facingDirection * grabRange);
    }
}
