using System.Collections.Generic;
using UnityEngine;

public class GestorGamificacion : MonoBehaviour
{
    [Header("Datos del Jugador")]
    public int puntajeTotal = 0;
    
    // Esta lista guarda los nombres de los monumentos que el turista ya encontró
    private List<string> monumentosDescubiertos = new List<string>();

    // Este método será llamado por el script de Realidad Aumentada cuando vea una imagen
    public void RegistrarDescubrimiento(string nombreMonumento)
    {
        // 1. Verificamos si la lista ya contiene este monumento
        if (!monumentosDescubiertos.Contains(nombreMonumento))
        {
            // 2. Si es nuevo, lo agregamos a la lista
            monumentosDescubiertos.Add(nombreMonumento);
            
            // 3. Sumamos 50 puntos (puedes cambiar este valor después)
            puntajeTotal += 50; 
            
            // Debug.Log sirve para imprimir mensajes en la consola de Unity y probar que funciona
            Debug.Log("¡Éxito! Has descubierto: " + nombreMonumento);
            Debug.Log("Tu puntaje actual es: " + puntajeTotal + " EXP");

            // NOTA FUTURA: Aquí enlazaremos la interfaz gráfica (UI) para que salga 
            // un texto en pantalla diciendo "+50 Puntos" y guardaremos esto en Firebase.
        }
        else
        {
            // Si ya lo había escaneado antes, no sumamos puntos.
            Debug.Log("Ya descubriste " + nombreMonumento + " anteriormente. Ve al siguiente punto.");
        }
    }
}