using UnityEngine;

public class King : MonoBehaviour
{
    private Vector3 targetPosition;
    private bool isSelected = false;
    private float moveSpeed = 10f;
    private Vector3 originalPosition;
    private bool isWhite;
    public GameObject GameManager;

    void Start()
    {
        targetPosition = transform.position;
        originalPosition = transform.position;
        isWhite = transform.position.z < 4; // Assuming white starts at bottom
        Debug.Log($"King initialized at position: {originalPosition}, isWhite: {isWhite}");
        GameManager = GameObject.Find("GameManager"); 
    }

    void Update()
    {
        // Smoothly move the piece to target position
        if (transform.position != targetPosition)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    if (!isSelected)
                    {
                        SelectPiece();
                    }
                }
                else if (isSelected)
                {
                    if (hit.collider.CompareTag("ChessSquare"))
                    {
                        if (IsValidKingMove(hit.point))
                        {
                            MovePiece(hit.point);
                        }
                        else
                        {
                            Debug.Log($"Invalid king move to {hit.point}. Current pos: {transform.position}");
                            DeselectPiece();
                        }
                    }
                    else
                    {
                        DeselectPiece();
                    }
                }
            }
        }
    }

    bool IsValidKingMove(Vector3 newPosition)
    {
        // Calculate the movement vector from current position
        Vector3 currentPos = new Vector3(
            Mathf.Floor(transform.position.x) + 0.5f,
            originalPosition.y,
            Mathf.Floor(transform.position.z) + 0.5f
        );

        Vector3 targetPos = new Vector3(
            Mathf.Floor(newPosition.x) + 0.5f,
            originalPosition.y,
            Mathf.Floor(newPosition.z) + 0.5f
        );

        Vector3 movement = targetPos - currentPos;

        // Get the absolute values of movement in both directions
        float absX = Mathf.Abs(movement.x);
        float absZ = Mathf.Abs(movement.z);

        // King can move one square in any direction (including diagonally)
        bool isValidKingMove = absX <= 1 && absZ <= 1 && !(absX == 0 && absZ == 0);

        if (!isValidKingMove)
        {
            Debug.Log($"Invalid king movement pattern - Can only move one square in any direction");
            return false;
        }

        return true;
    }

    void SelectPiece()
    {
        isSelected = true;
        GetComponent<Renderer>().material.color = Color.yellow;
        targetPosition = transform.position + Vector3.up * 0.5f;
    }

    void DeselectPiece()
    {
        isSelected = false;
        if (isWhite)
            GetComponent<Renderer>().material.color = Color.white;
        else
            GetComponent<Renderer>().material.color = Color.black;
        targetPosition = new Vector3(transform.position.x, originalPosition.y, transform.position.z);
    }

    void MovePiece(Vector3 newPosition)
    {
        transform.position = new Vector3(
            Mathf.Floor(newPosition.x) + 0.5f,
            originalPosition.y,
            Mathf.Floor(newPosition.z) + 0.5f
        );
        DeselectPiece();
    }
}