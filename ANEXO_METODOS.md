#### NOTA: *<u>ESTE ANEXO HA DE SER ACTUALIZADO</u>* Existen ciertas clases cuyos métodos se han actualizado en el lapso del 18 al 19 de Junio, por lo que pueden no ser del todo exactos.

# BACKEND. Scripts de Unity.

## PowerUpMovement.cs
**Descripción:** Controla el movimiento de los power-ups, incluyendo un movimiento en zigzag y rotación.  
**Métodos:**
- `Start()`: Inicializa la salud y rota el objeto de forma aleatoria al inicio.
- `Update()`: Mueve el objeto hacia la izquierda, realiza un movimiento en zigzag y rota el objeto.
- `TakeDamage(int damage)`: Reduce la salud del objeto cuando recibe daño.
- `PlayBreakSound()`: Reproduce un sonido cuando el objeto es destruido.

## StageManager.cs
**Descripción:** Administra las etapas del juego, incluyendo el avance de etapa, los períodos de descanso y la destrucción de la fortaleza voladora.  
**Métodos:**
- `Awake()`: Configura la instancia singleton y asegura que no se destruya al cargar una nueva escena.
- `Start()`: Inicializa el temporizador de la etapa.
- `Update()`: Actualiza el temporizador de la etapa y cambia de etapa si es necesario.
- `StartRestPeriod()`: Inicia un período de descanso.
- `EndRestPeriod()`: Termina el período de descanso y avanza a la siguiente etapa.
- `AdvanceStage()`: Avanza a la siguiente etapa del juego y actualiza los spawners.
- `UpdateStageSettings()`: Configura los ajustes para la etapa actual y reinicia los spawners.
- `FlyingFortressDestroyed()`: Maneja la destrucción de la fortaleza voladora.
- `LoadSummaryScene()`: Carga la escena de resumen después de un retraso.
- `StopAllSpawners()`: Detiene todos los spawners activos.
- `StopSpawner(MonoBehaviour spawner)`: Detiene un spawner específico.
- `StartSpawner(MonoBehaviour spawner)`: Inicia un spawner específico.

## GameManager.cs
**Descripción:** Proporciona métodos globales, como la carga de la primera escena del juego.  
**Métodos:**
- `LoadSceneZero()`: Carga la escena con índice 0.

## LoginManager.cs
**Descripción:** Gestiona la funcionalidad de inicio de sesión, registro y acceso como invitado en el juego, incluyendo la conexión a un servidor para autenticar y registrar usuarios.  
**Métodos:**
- `Start()`: Inicializa la imagen de fade como transparente y configura el campo de contraseña.
- `Update()`: Detecta la tecla Enter para iniciar sesión.
- `OnLoginButtonClicked()`: Inicia el proceso de login.
- `OnGuestButtonClicked()`: Establece el usuario y contraseña como "guest" y carga la escena siguiente.
- `Login()`: Envía los datos de login al servidor y maneja la respuesta.
- `OnRegisterButtonClicked()`: Inicia el proceso de registro.
- `Register()`: Envía los datos de registro al servidor y maneja la respuesta.
- `FadeOutAndChangeScene()`: Inicia el efecto de fade out y cambia de escena.
- `OnEnterKeyPressed()`: Maneja la tecla Enter para iniciar sesión.

## MainMenu.cs
**Descripción:** Administra las interacciones del menú principal, como iniciar el juego, cerrar la aplicación y cerrar sesión del usuario.  
**Métodos:**
- `Start()`: Muestra el nombre de usuario y cambia el texto del botón de logout si el usuario es "guest".
- `PlayGame()`: Carga la escena Map1.
- `QuitGame()`: Sale del juego.
- `Logout()`: Limpia los PlayerPrefs y carga la escena de login.

## ResumenPartida.cs
**Descripción:** Muestra el resumen de la partida, incluyendo estadísticas como la puntuación, precisión, tiempo completado, daño causado y recibido.  
**Métodos:**
- `Start()`: Muestra las estadísticas de la última partida desde PlayerPrefs.
- `OnNextButtonClicked()`: Carga la escena de créditos.

## BulletPool.cs
**Descripción:** Gestiona un pool de balas para optimizar la creación y destrucción de balas en el juego, permitiendo obtener y devolver balas de diferentes tipos (jugador, enemigo, especial).  
**Métodos:**
- `Awake()`: Inicializa la instancia singleton.
- `GetPlayerBullet()`: Obtiene una bala de jugador del pool o crea una nueva si el pool está vacío.
- `ReturnPlayerBullet(GameObject bullet)`: Devuelve una bala de jugador al pool.
- `GetEnemyBullet()`: Obtiene una bala de enemigo del pool o crea una nueva si el pool está vacío.
- `ReturnEnemyBullet(GameObject bullet)`: Devuelve una bala de enemigo al pool.
- `GetSpecialBullet()`: Obtiene una bala especial del pool o crea una nueva si el pool está vacío.
- `ReturnSpecialBullet(GameObject bullet)`: Devuelve una bala especial al pool.

## ScoreManager.cs
**Descripción:** Gestiona la puntuación del juego, registra estadísticas de los disparos y daños, y guarda estas estadísticas tanto localmente como en un servidor remoto.  
**Métodos:**
- `Awake()`: Inicializa la instancia singleton y asegura que no se destruya al cargar una nueva escena.
- `Start()`: Inicializa la puntuación y el tiempo de inicio.
- `AddScore(int amount)`: Añade puntos a la puntuación actual y actualiza el texto de la puntuación.
- `RegisterShot()`: Registra un disparo realizado.
- `RegisterHit()`: Registra un disparo acertado.
- `RegisterDamageDealt(int amount)`: Registra el daño causado.
- `RegisterDamageTaken(int amount)`: Registra el daño recibido.
- `SaveGameStatistics()`: Guarda las estadísticas del juego en PlayerPrefs y las envía al servidor si el usuario está logueado.
- `UpdateScoreText()`: Actualiza el texto de la puntuación en la interfaz.
- `SendStatisticsToServer(string json, string username, string password)`: Envía las estadísticas del juego al servidor.
- `GetScore()`: Retorna la puntuación actual.

## GameStatistics.cs
**Descripción:** Almacena las estadísticas de una partida, como la fecha, el nombre del mapa, la puntuación, la precisión, el tiempo completado, el daño causado y el daño recibido.  
**Métodos:**
- `GameStatistics(DateTime fecha, string nombreMapa, int puntuacion, float precision, float tiempoCompletado, int danoCausado, int danoRecibido)`: Inicializa los campos con los valores proporcionados.

## ScrollCredits.cs
**Descripción:** Desplaza los créditos en la pantalla.  
**Métodos:**
- `Start()`: Obtiene el componente RectTransform del objeto.
- `Update()`: Desplaza los créditos hacia arriba a una velocidad constante.

## TabNavigation.cs
**Descripción:** Permite la navegación entre campos de entrada utilizando la tecla Tab.  
**Métodos:**
- `Update()`: Detecta si se presiona la tecla Tab.
- `SelectNextInputField()`: Encuentra el campo de entrada que actualmente tiene el foco y selecciona el siguiente.

## AudioFader.cs
**Descripción:** Realiza un fade out de una fuente de audio.  
**Métodos:**
- `FadeOutAudio()`: Inicia la corrutina para hacer fade out del audio.
- `FadeOutCoroutine()`: Reduce gradualmente el volumen de la fuente de audio hasta silenciarla.

## CustomCursor.cs
**Descripción:** Personaliza el cursor del juego y lo oculta tras un período de inactividad.  
**Métodos:**
- `Start()`: Configura el cursor personalizado y registra la última actividad del mouse.
- `Update()`: Detecta el movimiento del mouse y actualiza el tiempo de la última actividad.
- `SetCursor()`: Establece la textura del cursor.
- `ShowCursor()`: Muestra el cursor.
- `HideCursor()`: Oculta el cursor.

## FadeInLogo.cs
**Descripción:** Realiza un efecto de fade-in en el logo al inicio del juego.  
**Métodos:**
- `Start()`: Obtén el componente Sprite Renderer del objeto y asegura que el logo sea transparente al inicio.
- `FadeIn()`: Realiza un efecto de fade-in en el logo.

## MovimientoFondo.cs
**Descripción:** Desplaza una imagen de fondo para crear un efecto de movimiento.  
**Métodos:**
- `Update()`: Calcula la nueva posición de la imagen de fondo y la aplica.

## OpenURL.cs
**Descripción:** Abre una URL específica en el navegador predeterminado del sistema.  
**Métodos:**
- `OpenRegistrationPage()`: Abre la URL en el navegador.

## PlaySoundOnType.cs
**Descripción:** Reproduce un sonido cuando el usuario escribe en un campo de entrada de texto.  
**Métodos:**
- `Start()`: Añadir listener para cada entrada de texto en los campos TMP_InputField.
- `PlaySound(string input)`: Reproduce un sonido cuando se escribe en un campo de entrada de texto.

## SceneFader.cs
**Descripción:** Realiza efectos de fade-in y fade-out entre transiciones de escenas.  
**Métodos:**
- `Start()`: Comienza con un fade in.
- `FadeToScene(string sceneName)`: Inicia el fade out y cambia de escena.
- `FadeIn()`: Realiza un fade in gradual.
- `FadeOut(string sceneName)`: Realiza un fade out gradual y luego carga la nueva escena.

## RockMovement.cs
**Descripción:** Controla el movimiento y la destrucción de las rocas en el juego.  
**Métodos:**
- `Update()`: Mueve y rota la roca, y la destruye si se mueve fuera de la pantalla.
- `Move()`: Mueve la roca hacia la izquierda y la rota alrededor del eje Y.

## RockSpawner.cs
**Descripción:** Genera rocas en el juego a intervalos aleatorios.  
**Métodos:**
- `StartSpawning()`: Inicia la generación de rocas.
- `StopSpawning()`: Detiene la generación de rocas.
- `SpawnRock()`: Genera una roca en una posición aleatoria.
- `SpawnRocksAtPosition(Vector3 position, int numberOfRocks)`: Genera múltiples rocas en una posición específica.
- `InstantiateRock(Vector3 position)`: Instancia una roca con una escala aleatoria y reproduce un sonido.
- `PlaySpawnSound()`: Reproduce el sonido de spawn si está configurado.

## RockHealth.cs
**Descripción:** Maneja la salud de las rocas, la división en rocas más pequeñas y la reproducción de efectos de sonido y explosión.  
**Métodos:**
- `Start()`: Inicializa la salud de la roca.
- `OnBecameVisible()`: Marca la roca como visible cuando entra en pantalla.
- `OnBecameInvisible()`: Destruye la roca si se vuelve invisible después de haber sido visible.
- `TakeDamage(int damage)`: Aplica daño a la roca y verifica si debe destruirse.
- `DamageRock(GameObject rock)`: Aplica daño a una roca y reproduce el sonido de impacto.
- `DamageEnemy(GameObject enemy)`: Aplica daño a un enemigo o jefe y reproduce el sonido de impacto.
- `PlayHitSound()`: Reproduce el sonido de impacto si está configurado.

## LaserShoot.cs
**Descripción:** Maneja el disparo de balas y balas especiales desde la nave del jugador.  
**Métodos:**
- `Start()`: Inicializa las referencias a los componentes necesarios.
- `Update()`: Dispara una bala normal o especial según las teclas presionadas.
- `Shoot()`: Dispara una bala normal y reproduce el sonido de disparo.
- `ShootSpecial()`: Dispara una bala especial y reproduce el sonido de disparo.
- `ResetIsShooting()`: Permite disparar nuevamente.

## MovimientoNave.cs
**Descripción:** Controla el movimiento de la nave del jugador y su interacción con otros objetos en el juego.  
**Métodos:**
- `Start()`: Inicializa la referencia al script VidaNave.
- `Update()`: Controla el movimiento y la rotación de la nave.
- `OnCollisionEnter2D(Collision2D collision)`: Aplica daño a la nave al colisionar con un enemigo si no es invulnerable.
- `ResetearMovimiento()`: Resetea la velocidad y rotación de la nave.

## BigBullet.cs
**Descripción:** Controla el comportamiento de las balas grandes en el juego.  
**Métodos:**
- `Start()`: Destruye la bala después de su vida útil y obtiene la referencia al audio source.
- `OnTriggerEnter2D(Collider2D other)`: Aplica daño según el tipo de objeto con el que colisiona.
- `DamageRock(GameObject rock)`: Aplica daño a una roca y reproduce el sonido de impacto.
- `DamageEnemy(GameObject enemy)`: Aplica daño a un enemigo o jefe y reproduce el sonido de impacto.
- `IncrementPowerBarScale()`: Incrementa la escala de la barra de poder.
- `PlayHitSound()`: Reproduce el sonido de impacto si está configurado.

## Bullet.cs
**Descripción:** Controla el comportamiento de las balas normales en el juego.  
**Métodos:**
- `Start()`: Destruye la bala después de su vida útil y obtiene la referencia al audio source.
- `OnCollisionEnter2D(Collision2D collision)`: Aplica daño según el tipo de objeto con el que colisiona.
- `DamageRock(GameObject rock)`: Aplica daño a una roca y reproduce el sonido de impacto.
- `DamageEnemy(GameObject enemy)`: Aplica daño a un enemigo y reproduce el sonido de impacto.
- `DamageBoss(GameObject boss)`: Aplica daño a un jefe y reproduce el sonido de impacto.
- `PlayHitSound()`: Reproduce el sonido de impacto si está configurado.

## VidaNave.cs
**Descripción:** Maneja la salud y las vidas de la nave del jugador, así como la activación de la invulnerabilidad y los efectos visuales.  
**Métodos:**
- `Start()`: Inicializa referencias y verifica componentes.
- `OnCollisionEnter2D(Collision2D collision)`: Maneja las colisiones y aplica daño si no es invulnerable.
- `AplicarDanio(int cantidad)`: Aplica daño a la nave y activa la invulnerabilidad temporal.
- `ActivarInvulnerabilidad()`: Activa la invulnerabilidad temporalmente.
- `PerderSalud(int cantidad)`: Reduce la salud de la nave y verifica si debe perder una vida.
- `ActualizarBarraDeVida()`: Actualiza la barra de vida según la salud actual.
- `PerderVida()`: Reduce las vidas de la nave y maneja la lógica de reinicio o pérdida del juego.
- `RespawnAndBlink()`: Maneja el respawn de la nave y la animación de parpadeo de invulnerabilidad.
- `SpawnExplosion()`: Genera una explosión en la posición de la nave.
- `SetFieldAlpha(float alpha)`: Establece la transparencia del campo de fuerza.
- `FadeFieldIn(float duration)`: Realiza un fade in en el campo de fuerza.
- `FadeFieldOut(float duration)`: Realiza un fade out en el campo de fuerza.

## LaserShootEnemy.cs
**Descripción:** Maneja el disparo de balas enemigas desde la nave enemiga.  
**Métodos:**
- `Start()`: Inicializa la fuente de audio para el sonido de disparo.
- `Update()`: Incrementa el temporizador y dispara si es necesario.
- `Shoot()`: Dispara una bala enemiga desde el punto de disparo.

## MovimientoEnemigoZigZag.cs
**Descripción:** Controla el movimiento en zigzag de los enemigos.  
**Métodos:**
- `Start()`: Inicializa la posición y parámetros de movimiento vertical.
- `Update()`: Controla el movimiento y la rotación del enemigo.
- `Move()`: Mueve el enemigo lateralmente y en zigzag vertical.
- `Rotate()`: Ajusta la rotación del enemigo según su movimiento vertical.

## SpaceShipSpawner.cs
**Descripción:** Genera naves espaciales enemigas a intervalos aleatorios.  
**Métodos:**
- `StartSpawning()`: Inicia la generación de naves espaciales.
- `StopSpawning()`: Detiene la generación de naves espaciales.
- `SpawnSpaceShipWithRandomInterval()`: Genera una nave si el spawner está activo y programa el siguiente spawn.
- `InstantiateSpaceShip()`: Selecciona y genera un prefab de nave espacial.
- `PlaySpawnSound()`: Reproduce el sonido de spawn si está configurado.

## BulletEnemigo.cs
**Descripción:** Controla el comportamiento de las balas enemigas en el juego.  
**Métodos:**
- `Awake()`: Obtiene la referencia al componente AudioSource.
- `OnEnable()`: Registra un disparo.
- `ReturnToPool()`: Devuelve la bala al pool.
- `OnCollisionEnter2D(Collision2D collision)`: Aplica daño a la nave del jugador si colisiona con ella.
- `PlayHitSound()`: Reproduce el sonido de impacto si está configurado.

## EnemyHealth.cs
**Descripción:** Maneja la salud de los enemigos y su destrucción.  
**Métodos:**
- `Start()`: Inicializa referencias y verifica componentes.
- `TakeDamage(int damageAmount)`: Aplica daño al enemigo y maneja su muerte si la salud llega a cero.
- `Die()`: Maneja la muerte del enemigo.
- `SetFieldAlpha(float alpha)`: Establece la transparencia del campo de fuerza del enemigo.
- `FadeFieldIn(float duration)`: Realiza un fade in en el campo de fuerza.
- `FadeFieldOut(float duration)`: Realiza un fade out en el campo de fuerza.

## LaserShootBoss.cs
**Descripción:** Maneja el disparo de balas desde el jefe, utilizando múltiples puntos de disparo.  
**Métodos:**
- `Start()`: Obtiene el componente AudioSource del GameObject "SFX_SHOOT".
- `Update()`: Incrementa el temporizador y dispara si es necesario.
- `Shoot()`: Dispara desde un punto aleatorio y reproduce el sonido de disparo.

## NotifyOnDestroy.cs
**Descripción:** Notifica al StageManager cuando el objeto es destruido, indicando la destrucción de la fortaleza voladora.  
**Métodos:**
- `OnDestroy()`: Notifica al StageManager que la fortaleza voladora ha sido destruida.

## BossHealth.cs
**Descripción:** Maneja la salud del jefe y su destrucción.  
**Métodos:**
- `Start()`: Inicializa referencias y verifica componentes.
- `TakeDamage(int damageAmount)`: Aplica daño al jefe y maneja su muerte si la salud llega a cero.
- `Die()`: Maneja la muerte del jefe.
- `ChangeToSummaryScene()`: Espera unos segundos y luego notifica al StageManager para cambiar de escena.

## BossMovement.cs
**Descripción:** Controla el movimiento del jefe, incluyendo avance inicial y oscilación.  
**Métodos:**
- `Start()`: Guarda la posición inicial del jefe.
- `Update()`: Controla el movimiento del jefe, alternando entre avanzar y oscilar.
- `Advance()`: Mueve el jefe hacia la izquierda hasta alcanzar la distancia de avance.
- `Sway()`: Oscila el jefe horizontal y verticalmente.

## BossSpawner.cs
**Descripción:** Maneja la generación del jefe en el juego.  
**Métodos:**
- `StartSpawning()`: Genera el jefe si no hay uno actualmente.
- `StopSpawning()`: Detiene cualquier lógica de spawneo continuo.
- `SpawnBoss()`: Genera el jefe en la posición del spawner.

## BulletBoss.cs
**Descripción:** Controla el comportamiento de las balas disparadas por el jefe.  
**Métodos:**
- `Start()`: Destruye la bala después de su vida útil.
- `OnCollisionEnter2D(Collision2D collision)`: Aplica daño a la nave del jugador si colisiona con ella.
- `PlayHitSound()`: Reproduce el sonido de impacto si está configurado.
- `SpawnExplosion()`: Genera la explosión en la posición actual.

## LaserBIGShootBoss.cs
**Descripción:** Maneja el disparo de balas grandes desde el jefe.  
**Métodos:**
- `Start()`: Obtiene el componente AudioSource del GameObject "SFX_SHOOT".
- `Update()`: Incrementa el temporizador y dispara si es necesario.
- `Shoot()`: Dispara desde el punto "BossDisparo4BIG" y reproduce el sonido de disparo.

# BACKEND. Aplicación flask.

### app.py
1. **jugar:**
    - Verifica si el usuario ha iniciado sesión y muestra la página para jugar.
2. **home:**
    - Redirige al dashboard si el usuario ha iniciado sesión, sino redirige a la página de login.
3. **perfil_usuario:**
    - Muestra el perfil del usuario seleccionado y sus partidas.
4. **register:**
    - Gestiona el registro de nuevos usuarios, verificando la disponibilidad del nombre de usuario y correo electrónico, y almacenando la nueva cuenta.
5. **login:**
    - Gestiona el inicio de sesión de los usuarios, verificando las credenciales.
6. **dashboard:**
    - Muestra el panel principal del usuario, incluyendo sus mejores partidas.
7. **partidas:**
    - Muestra todas las partidas del usuario.
8. **amigos:**
    - Gestiona la lista de amigos del usuario, permitiendo agregar y eliminar amigos.
9. **logout:**
    - Cierra la sesión del usuario.
10. **perfil:**
    - Permite al usuario actualizar su perfil, borrar partidas o eliminar su cuenta.
11. **guardar_estadisticas:**
    - Guarda las estadísticas de una partida enviada por una aplicación externa, después de verificar las credenciales del usuario.

## models.py
1. **Usuario:**
    - Modelo que representa un usuario en el sistema, incluyendo sus datos y relaciones con partidas y amistades.
2. **Mapa:**
    - Modelo que representa un mapa del juego, incluyendo su nombre y las partidas relacionadas.
3. **Partida:**
    - Modelo que representa una partida jugada, incluyendo datos como puntuación, precisión, y tiempo completado.
4. **Amistad:**
    - Modelo que representa una amistad entre dos usuarios, incluyendo la fecha en que se estableció la amistad.
