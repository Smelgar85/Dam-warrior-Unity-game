# PROPUESTA DE PROYECTO INTEGRADO (PI)

## INFORMACIÓN GENERAL

- **Alumno/a:** Sebastián Juan Melgar Marín
- **Título para el PI:** Videojuego con datos persistentes almacenados en la nube, consultables vía web.

## INTRODUCCIÓN

### Contextualización del problema a resolver

Este trabajo se basa en el proyecto previo desarrollado en Unity para la asignatura de Horas de libre configuración. El proyecto, aunque iba bien encaminado, necesitaba mejorarse. El presente TFG se centrará en incorporar un sistema de puntuación y estadísticas, y de la mano de este, de un registro de usuarios visible de manera externa, desde una web.

El sistema permitirá el logeado de estadísticas para los jugadores, donde poder ver el total de puntos por partida, un histórico de partidas con las puntuaciones, el porcentaje de acierto por partida, el tiempo de completado de nivel, un nivel de jugador, etc.

## OBJETIVOS

La aplicación buscará:

1. Conseguir que el videojuego tenga un cierto nivel de calidad, con una presentación, desarrollo, final y créditos, y el menor número de bugs posible.
2. Implementar un sistema por el cual un usuario pueda jugar registrándose, pudiendo en ese caso ver su progreso y estadísticas vía web.
3. Almacenamiento permanente de los datos de las partidas del usuario usando recursos remotos.
4. Permitir a cada usuario modificar sus datos de acceso o darse de baja.
5. Poder ver el progreso de otros usuarios e implantar un sistema de amigos.
6. Compilar una versión adaptada a Android.

### FUNCIONALIDADES

- Inicio de registro desde el propio juego o redirección al usuario a la página web.
- Sistema de registro de usuarios en una web generada con Flask.
- Registro de progreso desde la página web para ver estadísticas del juego, tanto por partidas como por usuarios.
- Logueo dentro del juego para cargar el progreso del jugador.

## ENTORNO TECNOLÓGICO

### TECNOLOGÍAS EMPLEADAS

- Unity para continuar el desarrollo del videojuego (C#).
- Flask para la web de registro de usuarios.
- MySQL para la base de datos.

### RECURSOS SOFTWARE Y HARDWARE

- Visual Studio Code para el desarrollo en C# y Python.
- [https://app.diagrams.net](https://app.diagrams.net) para la elaboración de diagramas.
- [https://eu.pythonanywhere.com/](https://eu.pythonanywhere.com/) para el alojamiento de la página y la base de datos.
- Notepad++ para anotaciones y varios.
- Trello para organizar el progreso del proyecto
- Git para el control de versiones (tengo pendiente estudiar si es viable usarlo para Unity).
