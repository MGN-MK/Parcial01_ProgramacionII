# Combat System

Sistema de Combate - Montserrat Godinez Núñez - Parcial 01

Este sistema de combate es al estilo de Pokemón: Combate por turnos, en equipos y con varias habilidades con diferentes resultados, tanto en el enemigo como en uno mismo y el equipo.

# Índice

# Contenido

- Administrador de Combate (Combat Manager).
- Interfaz de Combate (Paneles de Estatus, Panel de logs, Interfaz de Skills, Interfaz de Personajes).
- Interfaz de Estatus actualizado después de cada turno.
- Combate por Turnos.
- Combate en Equipos (Sin límite).
- Habilidades que afectan la salud de tres formas diferentes: En base a las estadísticas, en base a porcentaje, y de cantidad fija.
- Habilidades que le generan cambios a las estadísticas conforme pasen los turnos.
- Todas las habilidades tienen cinco formas de afectar a los personajes: Autoinfligido, todos los aliados, todos los enemigos, a un solo aliado, o a un solo enemigo.

# Instalación

- Importa el paquete de Assets “CombatSystemAssets”, este generará los archivos necesarios para poder utilizarlo, incluyendo los scripts organizados en carpetas, algunos prefabs de prueba, y efectos de partículas de prueba.
- Puedes utilizar y personalizar la escena “CombatSystem” que viene precargada, o crea una nueva escena donde se dará lugar al combate.

## Configurar una nueva escena

Si estás utilizando la escena que viene por defecto puedes saltarte estos pasos.

1. Crea un objeto vacío, el cual tendrá por nombre “CombatManager” (por organización), y agrégale como componente el script “CombatManager” (Scripts > Combat > CombatManager).
2. Agrega a la escena el prefab “UI” (Prefabs > UI), es un canvas predeterminado para la interfaz de combate. En caso de que ocupes más elementos del interfaz, dentro de la carpeta “Prefabs” se encuentras los diferentes paneles, listos para ser integrados en el canvas.
3. Agrega a tus jugadores. A estos agrégales el script “PlayerCharacter” (Scripts > Characters > PlayerCharacter), y rellena los campos con sus respectivos componentes.
4. Agrega a tus enemigos. A estos agrégales el script “EnemyCharacter” (Scripts > Characters > EnemyCharacter), y rellena los campos con sus respectivos componentes.
5. Como hijos de cada personaje crea un objeto vacío llamado “Skills”, este contendrá, a su vez, objetos vacíos con los nombres de cada habilidad que posee cada personaje.

## Configurar las Skills de cada personaje

### Habilidades de Salud

Al objeto vacío hijo del objeto “Skills” (hijo propiamente del personaje) agrégale como componente el script de “HealthModSkill” (Scripts > Skills > HealthModSkill), y rellena los campos con sus respectivos componentes.

- Skill Name. Escribe el nombre de la habilidad
- Animation Duration. Escribe cuántos segundos durará la animación de la habilidad.
- Targeting. Selecciona el tipo de víctima de la habilidad.
    - AUTO. El mismo personaje.
    - ALL_ALLIES. Todos los personajes dentro del mismo equipo.
    - ALL_OPPONENTS. Todos los personajes dentro del equipo enemigo.
    - SINGLE_ALLY. Un personaje dentro del mismo equipo.
    - SINGLE_OPPONENT. Un personaje dentro del equipo enemigo.
- Effect Prfb. Asigna el prefab del efecto que se va a crear en la misma posición de la víctima.
- Amount. Escribe la cantidad de daño(números negativos) o curación (números positivos) que genere la habilidad.
- Mod Type. Selecciona de qué forma afecta la habilidad.
    - STAT_BASED. Se basa en las estadísticas del personaje y la víctima.
    - FIXED. Afecta de forma fija, no se altera el “Amount” que designaste.
    - PERCENTAGE. Se basa en porcentaje. El “Amount” es el porcentaje del total de vida máxima de la víctima.
- Crit Chance. Desliza el deslizador para definir el porcentaje de probabilidad de dar un golpe crítico con la habilidad.

### Habilidades de cambios de estadísticas por turnos

1. Al objeto vacío hijo del objeto “Skills” (hijo propiamente del personaje) agrégale como componente el script de “ApplySCSkill” (Scripts > StatusCondition> ApplySCSkills), y rellena los campos con sus respectivos componentes.
- Skill Name. escribe el nombre de la habilidad.
- Animation Duration. Escribe cuántos segundos durará la animación de la habilidad.
- Targeting. Selecciona el tipo de víctima de la habilidad.
    - AUTO. El mismo personaje.
    - ALL_ALLIES. Todos los personajes dentro del mismo equipo.
    - ALL_OPPONENTS. Todos los personajes dentro del equipo enemigo.
    - SINGLE_ALLY. Un personaje dentro del mismo equipo.
    - SINGLE_OPPONENT. Un personaje dentro del equipo enemigo.
- Effect Prfb. Asigna el prefab del efecto que se va a crear en la misma posición de la víctima.
1. PARA AFECTAR LA SALUD. Crea como hijo un objeto vacío, y agrégale como componente el script “HealthModStatusCondition” (Scripts > StatusCondition > HealthModStatusCondition) y rellena los campos con sus respectivos componentes.}
- Effect Prfb. Asigna el prefab del efecto que se va a crear en la misma posición de la víctima.
- Animation Duration. Escribe cuántos segundos durará la animación de la habilidad.
- Reception Message. Escribe el mensaje que se imprimirá cuando se ejecute la habilidad.
- Apply Message. Escribe el mensaje que se imprimirá cada turno en que la habilidad esté activada.
- Expire Message. Escribe el mensaje que se imprimirá cuando la habilidad se desactive.
- Turn Duration. Escribe la cantidad de turnos que la habilidad estará activa.
1. PARA BLOQUEAR LOS TURNOS. Crea como hijo un objeto vacío, y agrégale como componente el script “TurnBlockStatusCondition” (Scripts > StatusCondition >TurnBlockStatusCondition) y rellena los campos con sus respectivos componentes.}
- Effect Prfb. Asigna el prefab del efecto que se va a crear en la misma posición de la víctima.
- Animation Duration. Escribe cuántos segundos durará la animación de la habilidad.
- Reception Message. Escribe el mensaje que se imprimirá cuando se ejecute la habilidad.
- Apply Message. Escribe el mensaje que se imprimirá cada turno en que la habilidad esté activada.
- Expire Message. Escribe el mensaje que se imprimirá cuando la habilidad se desactive.
- Turn Duration. Escribe la cantidad de turnos que la habilidad estará activa.
- Block Chance. Desliza el deslizador para definir el porcentaje de probabilidad de bloquear los turnos de la víctima.

# Recomendaciones

No modifiques los scripts, debido a que estos están relacionados entre sí. Solo modificalos para integrar el sistema a tus sistemas que ya tienes en el proyecto (Sistemas de progresión, de movimiento, de inventariuo, etc.).