using Unity.Netcode;
using UnityEngine;

public class Character : NetworkBehaviour
{
    [SerializeField] float moveSpeed = 4;
    [SerializeField] Transform sprites;

    Rigidbody2D rb;

    float moveH, moveV;
    Vector2 movement;

    int facingDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveH = Input.GetAxisRaw("Horizontal");
        moveV = Input.GetAxisRaw("Vertical");
        movement = new Vector2(moveH, moveV).normalized;
        rb.linearVelocity = movement;

        if(movement.x != 0)
        {
            facingDirection = movement.x > 0 ? 1 : -1;
        }
        sprites.localScale = new Vector2(facingDirection, 1); // change Direction
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SendPlayerDiedServerRpc();
    }

    [ServerRpc]
    void SendPlayerDiedServerRpc()
    {
        NotifyAllPlayersGameOverClientRpc();
    }

    [ClientRpc]
    void NotifyAllPlayersGameOverClientRpc()
    {
        GameOver();
    }

    // ���� ���� ���� ó��
    void GameOver()
    {
        Debug.Log("���� ����~!");
    }
}