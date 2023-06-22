using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int deadVal = 0;
    public bool isLast = false;
    public bool isFirst = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            // Transforma ésta en la posición de respawn del jugador
            // y se encarga de evitar que revivan los enemigos anteriores
            GameManager.instance.checkpoint = transform.position;
            if (deadVal > GameManager.instance.deadVal)
            {
                if (!isFirst)
                    Tracker.Instance.AddEvent(new FinSalaEvent(deadVal));
                if (!isLast)
                    Tracker.Instance.AddEvent(new InicioSalaEvent(deadVal+1));
                GameManager.instance.deadVal = deadVal;
            }
        }
    }
}