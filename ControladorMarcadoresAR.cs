using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

// Esto asegura que el script no funcione si no está el gestor de AR
[RequireComponent(typeof(ARTrackedImageManager))]
public class ControladorMarcadoresAR : MonoBehaviour
{
    private ARTrackedImageManager gestorImagenesAR;

    void Awake()
    {
        // Enlaza la variable con el componente de Unity
        gestorImagenesAR = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        // Se suscribe al evento que avisa cuando la cámara ve algo
        gestorImagenesAR.trackedImagesChanged += AlCambiarImagenRastreada;
    }

    void OnDisable()
    {
        // Se desuscribe cuando la app se pausa o cierra
        gestorImagenesAR.trackedImagesChanged -= AlCambiarImagenRastreada;
    }

    void AlCambiarImagenRastreada(ARTrackedImagesChangedEventArgs evento)
    {
        // 1. Cuando la cámara detecta una imagen por primera vez
        foreach (var imagenDetectada in evento.added)
        {
            ActualizarEstadoImagen(imagenDetectada);
        }

        // 2. Cuando el usuario mueve el celular alrededor de la imagen
        foreach (var imagenActualizada in evento.updated)
        {
            ActualizarEstadoImagen(imagenActualizada);
        }

        // 3. Cuando la imagen sale del rango de visión
        foreach (var imagenRemovida in evento.removed)
        {
            imagenRemovida.gameObject.SetActive(false);
        }
    }

    void ActualizarEstadoImagen(ARTrackedImage imagenDectectada)
    {
        // Aquí validaremos qué imagen está viendo para saber qué mostrar
        // "Estatua_Precursor" será el nombre que le daremos a la foto en Unity más adelante
        if (imagenDectectada.referenceImage.name == "Estatua_Precursor" || imagenDectectada.referenceImage.name == "Acueducto")
        {
            // Si el motor AR confirma que está rastreando la imagen activamente
            if (imagenDectectada.trackingState == TrackingState.Tracking)
            {
                // Muestra el modelo 3D (el cubo placeholder por ahora)
                imagenDectectada.gameObject.SetActive(true);
                
                // NOTA FUTURA: Aquí agregaremos el código para sumar puntos al usuario en Firebase
                // o para reproducir el audio histórico.
            }
            else
            {
                // Si la cámara pierde enfoque o el usuario se aleja, oculta el modelo
                imagenDectectada.gameObject.SetActive(false);
            }
        }
    }
}