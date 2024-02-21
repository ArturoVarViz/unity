
# Camera Controller Readme

Este script de control de cámara proporciona funcionalidades para gestionar diferentes modos de visualización: movimiento de cámara en tercera persona y primera persona, así como rotación constante alrededor del jugador.

## Funciones Principales

### 1. Movimiento de Cámara en Tercera Persona

- **Descripción:** La cámara sigue al jugador desde una perspectiva externa.
- **Activación:** Esta función está activada por defecto.
- **Uso:**
  - Utiliza las teclas 'A' y 'D' para rotar horizontalmente la cámara alrededor del jugador.
  - Pulsa la tecla 'C' para cambiar al modo de primera persona.

```csharp
// Función para manejar el movimiento de la cámara en tercera persona
void ThirdPersonCameraMovement()
{
    float horizontalInput = 0.0f;
    if (Keyboard.current.aKey.isPressed)
    {
        horizontalInput = -1.0f;
    }
    else if (Keyboard.current.dKey.isPressed)
    {
        horizontalInput = 1.0f;
    }

    // Calcular la nueva posición de la cámara alrededor del jugador solo si hay entrada horizontal
    if (Mathf.Abs(horizontalInput) > 0.1f)
    {
        Quaternion rotation = Quaternion.AngleAxis(horizontalInput * rotationSpeed, Vector3.up);
        offset = rotation * offset;
    }

    // Aplicar la nueva posición de la cámara
    transform.position = player.transform.position + offset;
}```

### 2. Movimiento de Cámara en Primera Persona

    Descripción: Coloca la cámara en la posición del jugador, permitiendo al jugador mirar desde su perspectiva.
    Activación: Se activa al presionar la tecla 'C'.
    Uso:
        Utiliza las teclas 'A' y 'D' para rotar horizontalmente la cámara desde la perspectiva del jugador.
        Pulsa la tecla 'R' para cambiar al modo de rotación constante.


```

// Función para manejar el movimiento de la cámara en primera persona
void FirstPersonCameraMovement()
{
    transform.position = player.transform.position;
    transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

    float horizontalInput = 0.0f;
    if (Keyboard.current.aKey.isPressed)
    {
        Quaternion rotation = Quaternion.AngleAxis(-rotationSpeed, Vector3.up);
        offset = rotation * offset;
        horizontalInput = -1.0f;
    }
    else if (Keyboard.current.dKey.isPressed)
    {
        Quaternion rotation = Quaternion.AngleAxis(rotationSpeed, Vector3.up);
        offset = rotation * offset;
        horizontalInput = 1.0f;
    }

    // Calcular la nueva rotación de la cámara solo si hay entrada horizontal
    if (Mathf.Abs(horizontalInput) > 0.1f)
    {
        Quaternion rotation = Quaternion.AngleAxis(horizontalInput * rotationSpeed, Vector3.up);
        transform.rotation = rotation * transform.rotation;
    }
}```

### 3. Rotación Constante

    Descripción: La cámara gira continuamente alrededor del jugador.
    Activación: Se activa al presionar la tecla 'R'.
    Uso: La cámara girará automáticamente alrededor del jugador a una velocidad constante. Puedes usar las teclas 'A' y 'D' para ajustar la velocidad de rotación en cualquier modo de visualización.

```

// Función para manejar la rotación constante de la cámara alrededor del jugador
void RotateConstantlyAroundPlayer()
{
    Quaternion rotation = Quaternion.AngleAxis(rotationSpeed, Vector3.up);
    offset = rotation * offset;
}
```
## Controles Comunes

    A y D: Rotar horizontalmente la cámara alrededor del jugador.
    C: Cambiar entre modos de visualización en tercera y primera persona.
    R: Activar/desactivar la rotación constante alrededor del jugador.
