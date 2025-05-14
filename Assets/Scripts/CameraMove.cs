using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;

    // Update is called once per frame
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - player.transform.position;
    }
    void Update()
    {
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        transform.position = player.transform.position + offset;
    }
}
