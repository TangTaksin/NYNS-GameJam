using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2 input;
    Rigidbody2D rb2D;
    Vector2 facingDirection;


    [SerializeField] bool isTopDown = true;
    [SerializeField] float charSpeed = 2f;

    [Header("Side Scroll Option")] // is side scroll mode necessery? probably not. but i'm doin' it anyway!
    [SerializeField] float gravityScale = 1f;
    [SerializeField] float jumpHeight = 1f;

    [Header("Pipe Placement")]
    [SerializeField] Grid grid;
    List<Pipe> currentPipes = new List<Pipe>();
    [SerializeField] Transform pipeHoldPos;
    [SerializeField] Transform projectionSprite;
    [SerializeField] LayerMask pipeLayer;

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
                            Input.GetAxisRaw("Vertical") * isTopDown.GetHashCode());
        input.Normalize();
    }

    //can move
    void Movement()
    {
        // if not in topdown mode then apply gravity
        rb2D.gravityScale = gravityScale * (!isTopDown).GetHashCode();

        var velY = input.y * charSpeed;
        var velX = input.x * charSpeed;

        rb2D.velocity = new Vector2(velX, 
            Mathf.Lerp(velY, rb2D.velocity.y, (!isTopDown).GetHashCode())); // Use velY if isTopDown is true,
                                                                            // else use rb's y velocity.
    }
    
    void UpdateDirection()
    {
        facingDirection = Vector2.Lerp(facingDirection, input, input.sqrMagnitude);
    }

    //can pickup pipe
    void PickUp_Place()
    {
        PlacementProjection();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentPipes.Count <= 0) //PickUp
            {
                //shoot raycast
                RaycastHit2D hit = Physics2D.Raycast(transform.position, facingDirection,1f,pipeLayer);

                print(hit.collider);

                if (hit)
                {
                    hit.collider.gameObject.TryGetComponent<Pipe>(out var pipe);
                    currentPipes.Add(pipe);

                    currentPipes[0].transform.SetParent(pipeHoldPos.transform);
                    currentPipes[0].SetCollider(false);

                    LeanTweenExt.LeanMove(currentPipes[0].gameObject, pipeHoldPos.position, .1f).setEaseInOutQuad();
                }
            }
            else if (Physics2D.OverlapBox(projectionSprite.position, grid.cellSize, 0f) == null)//Place
            {
                currentPipes[0].transform.SetParent(null);

                LeanTweenExt.LeanMove(currentPipes[0].gameObject, projectionSprite.position, .1f).setEaseInOutQuad();
                currentPipes[0].SetCollider(true);
                currentPipes.Clear();
                
            }
        }
    }

    void PlacementProjection()
    {
        projectionSprite.gameObject.SetActive(false);

        if (currentPipes.Count <= 0)
            return;

        //Project Placement
        projectionSprite.gameObject.SetActive(true);
        Vector3Int cellPosition = grid.WorldToCell(transform.position + (Vector3)facingDirection * 1.5f);
        projectionSprite.position = grid.GetCellCenterWorld(cellPosition);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + facingDirection * 1.5f);
    }
}
