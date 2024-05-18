# **DESARROLLO DE VIDEOJUEGO CON SISTEMA DE ESTADISTICAS EN LA NUBE**

## PROYECTO INTEGRADO PARA EL CICLO DE DESARROLLO DE APLICACIONES MULTIPLATAFORMA

#### Autor: Sebastían Juan Melgar Marín

## Índice de Contenidos

- [1 - Sobre este proyecto](#1---sobre-este-proyecto)
  - [1.1 - Control de versiones](#11---control-de-versiones)
  - [1.2 - Licencia de uso](#12---licencia-de-uso)
- [2 - Análisis del problema](#2---análisis-del-problema)
  - [2.1 - Introducción al problema](#21---introducción-al-problema)
  - [2.2 - Antecedentes](#22---antecedentes)
  - [2.3 - Objetivos](#23---objetivos)
  - [2.4 - Requisitos](#24---requisitos)
    - [2.4.1 - Funcionales](#241---funcionales)
    - [2.4.2 - No funcionales](#242---no-funcionales)
  - [2.5 - Recursos](#25---recursos)
    - [2.5.1 - Software](#251---software)
    - [2.5.2 - Hardware](#252---hardware)
- [3 - Diseño de la solución software](#3---diseño-de-la-solución-software)
  - [3.1 - Modelados](#31---modelados)
    - [3.1.1 - Casos de uso](#311---casos-de-uso)
    - [3.1.2 - Interacción](#312---interacción)
    - [3.1.3 - Estado](#313---estado)
    - [3.1.4 - Actividad](#314---actividad)
  - [3.2 - Prototipado gráfico](#32---prototipado-gráfico)
    - [3.2.1 - Escritorio](#321---escritorio)
    - [3.2.2 - Tablets / Smartphones](#322---tablets--smartphones)
  - [3.3 - Base de datos](#33---base-de-datos)
    - [3.3.1 - Diseño Conceptual (ER)](#331---diseño-conceptual-er)
- [4 - Implementación](#4---implementación)
  - [4.1 - Codificación](#41---codificación)
    - [4.1.1 - Usabilidad](#411---usabilidad)
    - [4.1.2 - Backend](#412---backend)
    - [4.1.3 - Frontend](#413---frontend)
  - [4.2 - Pruebas](#42---pruebas)
- [5 - Documentación](#5---documentación)
  - [5.1 - Empaquetado / Distribución](#51---empaquetado--distribución)
  - [5.2 - Instalación](#52---instalación)
  - [5.3 - Manual de Usuario / Referencia](#53---manual-de-usuario--referencia)
- [6 - Conclusiones](#6---conclusiones)
- [7 - Bibliografía](#7---bibliografía)

## 1 - Sobre este proyecto

Este proyecto surge como una continuación del desarrollo parcial de un videojuego iniciado como parte del plan de estudios de la asignatura "Horas de libre configuración". En el marco de esta asignatura, enfocada en el desarrollo de videojuegos durante el presente curso escolar, me he propuesto llevar a cabo la expansión y mejora de un emocionante juego de estilo matamarcianos (Shoot'em up).

El videojuego en cuestión se caracteriza por su dinamismo y acción frenética, presentando al jugador un desafío constante mientras avanza en un entorno de scroll lateral. En este universo virtual, el jugador se enfrenta a una sucesión de obstáculos y enemigos que requieren habilidad y destreza para ser esquivados o destruidos, evitando así sufrir daños por colisión o por el impacto de proyectiles enemigos.

El objetivo principal de este proyecto es elevar la experiencia de juego a un nivel superior mediante la implementación de nuevas funcionalidades y la introducción de un sistema de estadísticas que permita al jugador medir su progreso y desempeño de manera significativa. Al abordar estas mejoras, busco no solo enriquecer la jugabilidad del videojuego, sino también explorar y aplicar conocimientos adquiridos en el ámbito del desarrollo de software y diseño de experiencias interactivas.

Imágenes del juego en su estado actual:

![Menu principal](img/image.png)

![Gameplay](img/image2.png)

[Probar Dam Warrior en el navegador](https://play.unity.com/mg/other/dam_warrior_web)

[Descargar la build previa del juego](Old_Build/DAM_WARRIOR_BUILD_06_03_2024.rar)

### 1.1 - Control de versiones

Se hará control de versiones al código de los scripts de Unity hechos para el videojuego, así como del código que se empleará en desarrollo de la web usando Flask.

### 1.2 - Licencia de uso

## 2 - Análisis del problema

### 2.1 - Introducción al problema

En el contexto del desarrollo de un videojuego, nos enfrentamos a dos áreas fundamentales que requieren atención: la mejora de la experiencia del jugador a través de la implementación de características básicas faltantes y la integración de un sistema de estadísticas para enriquecer la interacción del jugador con el juego.

Actualmente, el videojuego carece de ciertas funcionalidades que son esenciales para proporcionar una experiencia completa y satisfactoria. La ausencia de un menú de opciones, múltiples mapas, un final apropiado con créditos, transiciones fluidas entre escenarios y una interfaz visualmente atractiva limita el potencial del juego y puede afectar la percepción del jugador sobre su calidad y profesionalismo. Además, la falta de elementos como transiciones de sonido y sucesos coherentes contribuye a una experiencia de juego menos inmersiva.

Por otro lado, la implementación de un sistema de estadísticas permitirá recopilar datos relevantes sobre el desempeño del jugador durante sus partidas. Estadísticas como el daño recibido, el daño infligido, el número de enemigos derrotados, el tiempo total de juego y el porcentaje de aciertos proporcionarán información valiosa tanto para el jugador como para los desarrolladores. Estos datos no solo enriquecerán la experiencia del jugador al ofrecer una mayor comprensión de su progreso y habilidades, sino que también brindarán a los desarrolladores información clave para ajustar y mejorar el juego en futuras iteraciones.

Para abordar estos desafíos, proponemos la ampliación y mejora de las funcionalidades del videojuego, así como la implementación de un sistema de estadísticas basado en Flask que permita la recopilación y visualización de datos de manera eficiente y accesible.

### 2.2 - Antecedentes

El desarrollo de videojuegos es un campo en constante evolución, donde la atención a los detalles y la satisfacción del jugador son cruciales para el éxito de un título. La falta de ciertas funcionalidades básicas y la ausencia de un sistema de estadísticas pueden afectar negativamente la experiencia del jugador y limitar el potencial de un juego para destacarse en un mercado cada vez más competitivo. Reconociendo esta necesidad, mi proyecto se enfoca en abordar estas deficiencias y mejorar la experiencia del usuario.

### 2.3 - Objetivos

El objetivo principal es mejorar la experiencia del jugador mediante la implementación de características faltantes y la integración de un sistema de estadísticas en nuestro videojuego. Específicamente, se busca:

- Ampliar las funcionalidades del juego para incluir un menú de opciones, varios mapas, un final con créditos, transiciones entre mapas y sucesos más coherentes, así como una interfaz más vistosa y transiciones de sonido.

- Implementar un sistema de estadísticas que recoja datos sobre el desempeño del jugador en sus partidas, incluyendo daño recibido, daño infligido, número de enemigos destruidos, tiempo total de partida y porcentaje de aciertos.

### 2.4 - Requisitos

#### 2.4.1 - Funcionales

Los requisitos funcionales incluyen:

- Implementación de un menú de opciones para permitir al jugador personalizar la configuración del juego.
- Diseño y desarrollo de varios mapas con diferentes niveles de dificultad y desafíos.
- Creación de un final con créditos para proporcionar un cierre adecuado al juego.
- Integración de transiciones fluidas entre mapas y sucesos coherentes para mejorar la inmersión del jugador.
- Diseño de una interfaz de usuario atractiva y visualmente agradable.
- Incorporación de transiciones de sonido para enriquecer la experiencia auditiva del jugador.
- Implementación de un sistema de estadísticas al final de cada mapa para mostrar datos relevantes sobre el desempeño del jugador.
- Implementación de un sistema de consulta de estadísticas de partidas via web, con sistema de registro, sistema de login, registro de partidas, sistema de amigos, etc.


#### 2.4.2 - No funcionales

Los requisitos no funcionales incluyen:

- Compatibilidad con diferentes dispositivos y sistemas operativos.
- Eficiencia en el rendimiento del juego para garantizar una experiencia fluida.
- Seguridad en el almacenamiento y gestión de datos del jugador.

### 2.5 - Recursos

#### 2.5.1 - Software

Para el desarrollo de nuestro proyecto, utilizaremos:

- Python y C# como lenguajes de programación principales (para Flask y Unity respectivamente).
- Flask para la implementación del sistema de estadísticas y la interacción con la base de datos.
- Herramientas de desarrollo integradas (IDE) como Visual Studio Code o SublimeText.
- Git para el control de versiones del código fuente.

#### 2.5.2 - Hardware

Los requisitos de hardware para el desarrollo y la ejecución del juego son mínimos y estándar, incluyendo:

- Ordenador con capacidad suficiente para ejecutar el entorno de desarrollo y probar el juego.
- Dispositivos de prueba para garantizar la compatibilidad con diferentes plataformas y sistemas operativos.

## 3 - Diseño de la solución software

### 3.1 - Modelados

#### 3.1.1 - Casos de uso

#### 3.1.2 - Interacción

#### 3.1.3 - Estado

#### 3.1.4 - Actividad

### 3.2 - Prototipado gráfico

#### 3.2.1 - Escritorio

DISEÑO WIREFRAME DE LA INTERFAZ WEB:

![alt text](img/image5.png)

MOCKUPS DE LA INTERFAZ WEB:

![alt text](img/image6.png)

### 3.3 - Base de datos

#### 3.3.1 - Diseño Conceptual (ER)

![Entidad-Relacion](img/image3.png)

Las relaciones entre las tablas serían las siguientes:

    Usuario - Amistad:
        La tabla Usuario tiene una relación de uno a muchos (1, N) con la tabla Amistad. Esto significa que cada registro en Usuario puede estar vinculado a muchos registros en Amistad, pero cada registro en Amistad se refiere a exactamente dos usuarios (indicando una amistad).
        La tabla Amistad refleja una relación de muchos a muchos (M, N) entre usuarios porque puede haber muchas instancias de amistad, y cada instancia involucra dos usuarios.

    Usuario - Partida:
        La tabla Usuario tiene una relación de uno a muchos (1, N) con la tabla Partida. Cada usuario puede tener varias partidas asociadas, pero cada partida está asociada a un único usuario (indicado por usuario_id en la tabla Partida).

    Partida - Mapa:
        La tabla Partida tiene una relación de muchos a uno (N, 1) con la tabla Mapa. Muchas partidas pueden tener asociado el mismo mapa (como en diferentes sesiones de juego en el mismo entorno), pero cada partida está vinculada a un único mapa (indicado por mapa_id en la tabla Partida).

    La tabla Amistad actúa como una tabla de asociación para manejar la relación muchos a muchos entre Usuario y Usuario, mientras que la relación entre Partida y Mapa se gestiona directamente a través de la clave foránea mapa_id en la tabla Partida. Con esta configuración, puedes tener muchos usuarios que se hacen amigos entre sí, cada usuario puede jugar muchas partidas y cada partida se lleva a cabo en un solo mapa, que puede ser el escenario de muchas partidas diferentes.

#### 3.3.2 - Modelo relacional

![Relacional](img/image4.png)

## 4 - Implementación

### 4.1 - Codificación

#### 4.1.1 - Usabilidad

#### 4.1.2 - Backend

#### 4.1.3 - Frontend

### 4.2 - Pruebas

## 5 - Documentación

### 5.1 - Empaquetado / Distribución

### 5.2 - Instalación

### 5.3 - Manual de Usuario / Referencia

## 6 - Conclusiones

## 7 - Bibliografía
