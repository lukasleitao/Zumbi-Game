using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoJogador : MovimentaPersonagem
{
  public void RotacaoJogador(LayerMask MascaraChao)
    {
        // Raio da origem da câmera até onde o mouse tá apontando
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Pega a posição do raio que ele toca em algo ( já tem valor implícito )
        RaycastHit impacto;

        // O "out" desse if é só para não dar erro porque impacto não tem valor nenhum explícito
        // A mascara no chao é pra contar só o chão como algo que o raio pode tocar
        if (Physics.Raycast(raio, out impacto, 100, MascaraChao))
        {
            // "- transform.position" para ser relativo ao jogador
            Vector3 posicaoMiraJogador = impacto.point - transform.position;

            // Para ficar na altura do jogador
            posicaoMiraJogador.y = transform.position.y;

            // Pensou em rotação pensou na porra do "Quaternion"
            Quaternion novaRotacao = Quaternion.LookRotation(posicaoMiraJogador);

            GetComponent<Rigidbody>().MoveRotation(novaRotacao);
        }
    }
}
