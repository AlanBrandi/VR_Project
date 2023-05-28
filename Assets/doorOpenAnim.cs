using UnityEngine;

public class doorOpenAnim : MonoBehaviour
{
    public Transform portaTransform; // Referência ao transform da porta
    public Vector3 rotacaoFinal; // Rotação final da porta (definida no Inspector)
    public float velocidadeRotacao = 5f; // Velocidade de rotação da porta

    private Quaternion rotacaoInicial; // Rotação inicial da porta
    private bool portaAberta = false; // Indica se a porta está aberta ou fechada
    bool chaveDentro;

    private void Start()
    {
        rotacaoInicial = portaTransform.rotation; // Salva a rotação inicial da porta
    }

    private void Update()
    {
        if (chaveDentro)
        {
            if (portaAberta)
                return;

            // Verifica se a porta atingiu a rotação final
            if (Quaternion.Angle(portaTransform.rotation, Quaternion.Euler(rotacaoFinal)) < 0.1f)
            {
                portaAberta = true;
                Debug.Log("Porta aberta!");
            }
            else
            {
                // Rotaciona a porta em direção à rotação final
                portaTransform.rotation = Quaternion.RotateTowards(portaTransform.rotation, Quaternion.Euler(rotacaoFinal), velocidadeRotacao * Time.deltaTime);
            }
        }
    }

    public void ResetarPorta()
    {
        portaAberta = false;
        portaTransform.rotation = rotacaoInicial; // Retorna a porta à rotação inicial
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            other.gameObject.SetActive(false);
            chaveDentro = true;
        }
    }
}