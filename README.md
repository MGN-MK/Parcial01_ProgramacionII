# Combat System

Sistema de Combate - Montserrat Godinez Núñez - Parcial 01

Este es un sistema de combate al estilo de Pokemón: Combate por turnos, en equipos y con varias habilidades con diferentes resultados, tanto en el enemigo como en uno mismo y el equipo.

El proyecto está listo para solo agregar los scripts y prefabs en los objetos de tu proyecto y desde el mismo editor configurarlos para brindarte una mayor comodidad sin que tengas que sufrir por saber qué código hace qué cosa.

# Índice

- [Contenido](https://www.notion.so/Contenido-3709c100d49d48559905b1fd5878fa47)
- [Instalación](https://www.notion.so/Instalaci-n-643b1423ebae4a489e47210af2b695a6)
    - [Configurar una Nueva Escena](https://www.notion.so/Configurar-una-Nueva-Escena-5da2241986444d6fbbcd5c6bd0ccceb4)
- [Configurar Personajes](https://www.notion.so/Configurar-Personajes-c08e24166e4e4f2da07d1ffc35d2b467)
    - [Jugadores (Personajes Jugables)](https://www.notion.so/Jugadores-Personajes-Jugables-780652788d82421da063beb1fe8b6216)
    - [Enemigos (Personajes con Inteligencia Artificial)](https://www.notion.so/Enemigos-Personajes-con-Inteligencia-Artificial-d335466beb684ab197fa71f30517a359)
- [Configurar las Skills de Cada Personaje](https://www.notion.so/Configurar-las-Skills-de-Cada-Personaje-5f81b3893f1d4b64ab53ef788e8849f7)
    - [](https://www.notion.so/a99453dd87fe493e83ca1b89e0c80c20)
    - [Habilidades con Efectos por Turnos](https://www.notion.so/Habilidades-con-Efectos-por-Turnos-4533b6ed8e99414c8c16ce5a0cfa11a0)
        - [Habilidades de Salud en Cada Turno](https://www.notion.so/Habilidades-de-Salud-en-Cada-Turno-9262e128dd8a43238a87f85b72525015)
        - [Habilidades para Bloquear Turnos](https://www.notion.so/Habilidades-para-Bloquear-Turnos-ba0603bd4ce2420dbac1e83c65552b65)
- [Funcionalidades de Modificar el Código](https://www.notion.so/Funcionalidades-de-Modificar-el-C-digo-c5d115ec665440a596833743be03c7cc)
    - [Stats](https://www.notion.so/Stats-86551321e63d4bbb8f6185f532b41242)
- [Recomendaciones](https://www.notion.so/Recomendaciones-6e605826b1544c11b7e4d8414532851b)
    - [Usa traductor para los errores si no sabes inglés](https://www.notion.so/Usa-traductor-para-los-errores-si-no-sabes-ingl-s-b84b056d546242488168f4bae47b4732)
    - [No modifiques los scripts!](https://www.notion.so/No-modifiques-los-scripts-e306d6f4bbb644c6b19b6b03ef6e190e)
- [Contacto](https://www.notion.so/Contacto-f11d3ab88392447397ca4ab993ef3e1d)

---

# Contenido

- Administrador de combate (Combat Manager).
- Interfaz de combate (Paneles de estatus, Panel de logs, Interfaz de skills, Interfaz de personajes).
- Combate por turnos.
- Interfaz de estatus actualizado en tiempo real.
- Combate en dos equipos (Sin límite de miembros).

```jsx
private void MakeTeams()
    {
        List<Character> playersBuffer = new List<Character>();
        List<Character> enemiesBuffer = new List<Character>();

				//Agrupa los personajes en listas de sus respectivos equipos de forma automática
        foreach (var chrs in this.characters)
        {
            if (chrs.team == Team.PLAYERS)
            {
                playersBuffer.Add(chrs);
            }
            else if (chrs.team == Team.ENEMIES)
            {
                enemiesBuffer.Add(chrs);
            }

            chrs.combatManager = this;
        }

        playerTeam = playersBuffer.ToArray();
        enemyTeam = enemiesBuffer.ToArray();
    }
```

- Habilidades que afectan la salud de tres formas diferentes: En base a las estadísticas, de cantidad fija y en base a porcentaje.

```jsx
public float GetModification(Character target)
    {
        switch (modType)
        {
						//En base a estadísticas
            case HealthModType.STAT_BASED:
                Stats senderStats = sender.GetCurrentStats();
                Stats targetStats = target.GetCurrentStats();

                // Fórmula de pokemon: https://bulbapedia.bulbagarden.net/wiki/Damage
                float rawDamage = (((2 * senderStats.lv) / 5) + 2) * this.amount * (senderStats.ap / targetStats.dp);

                return (rawDamage / 50) + 2;
						
						//De forma fija
            case HealthModType.FIXED:
                return amount;

						//En base a porcentaje
            case HealthModType.PERCENTAGE:
                Stats tStats = target.GetCurrentStats();

                return tStats.maxHP * amount;
        }

        throw new System.InvalidOperationException("HealthModSkill::GetDamage. Unreachable!");
    }
```

- Habilidades que le generan cambios a las estadísticas conforme pasen los turnos.
- Todas las habilidades tienen cinco formas de afectar a los personajes: Autoinfligido, todos los aliados, todos los enemigos, a un solo aliado, o a un solo enemigo.

```jsx
public enum SkillTargeting
{
    //Seleccón de la víctima de forma automática
    AUTO,
    ALL_ALLIES,
    ALL_OPPONENTS,

    //Seleccón de la víctima de forma manual
    SINGLE_ALLY,
    SINGLE_OPPONENT
}
```

[Índice](https://www.notion.so/ndice-607df48a93ae435683e2c25b86fb9bf3) 

---

# Instalación

- Descarga el paquete de Assets “CombatSystemAssets”, el cual lo encontrarás en el repositorio Parcial01_ProgramacionII.
- https://github.com/MGN-MK/Parcial01_ProgramacionII

<aside>
⚠️ En caso de que el acceso directo no funcione, copia y pega el siguiente link en un buscador: https://github.com/MGN-MK/Parcial01_ProgramacionII

</aside>

- Dentro de tu proyecto importa el paquete de Assets “CombatSystemAssets”, este generará los archivos necesarios para poder utilizarlo, incluyendo los scripts organizados en carpetas, algunos prefabs de prueba, y efectos de partículas de prueba.
    - Si no tienes un proyecto de unity, solo haz doble click sobre el archivo del paquete
    - Puedes simplemente arrastrar el archivo del paquete dentro de tu proyecto
    - Puedes hacer click derecho en la carpeta del proyecto y seleccionar (Import Package > Custom Package), para después elegir el archivo del paquete.
- Puedes utilizar y personalizar la escena “CombatSystem” que viene precargada, o crea una nueva escena donde se dará lugar al combate.

[Índice](https://www.notion.so/ndice-607df48a93ae435683e2c25b86fb9bf3) 

## Configurar una Nueva Escena

Si estás utilizando la escena que viene por defecto puedes saltarte estos pasos.

1. Agrega a la escena el prefab “CombatManager” (Prefabs > CombatManager).
2. Agrega a la escena el prefab “UI” (Prefabs > UI), es un canvas predeterminado para la interfaz de combate. En caso de que ocupes más elementos del interfaz, dentro de la carpeta “Prefabs” se encuentras los diferentes paneles, listos para ser integrados en el canvas.

[Índice](https://www.notion.so/ndice-607df48a93ae435683e2c25b86fb9bf3) 

---

## Configurar Personajes

### Jugadores (Personajes Jugables)

Agrega a tus jugadores. A estos agrégales el script “PlayerCharacter” (Scripts > Characters > PlayerCharacter), y rellena los campos con sus respectivos componentes.

- Team. Selecciona el equipo al que pertenece el personaje.
    - PLAYERS. Equipo de los jugadores.
    - ENEMIES. Equipo de los enemigos.
- NameID. Escribe el nombre del personaje.
- StatusPanel. Toma el panel de estatus que le corresponde al personaje (se debe de encontrar dentro del canvas “UI”) y arrástralo para agregárselo.
- CombatManager. Toma el objeto “Combat Manager” que se encuentra dentro de tu “Hierarchy” y arrástralo para agregárselo.
- Status Mods. No es necesario que lo modifiques, ya que es un listado para guardar las modificaciones de estatus durante la partida.
- Condition. No es necesario que le asignes nada, ya que el código lo hace en automático cuando se necesita.
- Skill Panel. Toma el panel de habilidades que le corresponde al personaje (se debe de encontrar dentro del canvas “UI”) y arrástralo para agregárselo.
- Enemies Panel. Toma el panel de enemigos que le corresponde al personaje (se debe de encontrar dentro del canvas “UI”) y arrástralo para agregárselo.
- Lv. Escribe el nivel del personaje.
- Max HP. Escribe la salud máxima del personaje.
- Ap. Escribe los puntos de ataque del personaje.
- Dp. Escribe los puntos de defensa del personaje
- Speed. Escribe la velocidad del personaje. Entre más veloz, será el primero en atacar.

[Índice](https://www.notion.so/ndice-607df48a93ae435683e2c25b86fb9bf3) 

### Enemigos (Personajes con Inteligencia Artificial)

Agrega a tus enemigos. A estos agrégales el script “EnemyCharacter” (Scripts > Characters > EnemyCharacter), y rellena los campos con sus respectivos componentes.

- Team. Selecciona el equipo al que pertenece el personaje.
    - PLAYERS. Equipo de los jugadores.
    - ENEMIES. Equipo de los enemigos.
- Name ID. Escribe el nombre del personaje.
- Status Panel. Toma el panel de estatus que le corresponde al personaje (se debe de encontrar dentro del canvas “UI”) y arrástralo para agregárselo.
- Combat Manager. Toma el objeto “Combat Manager” que se encuentra dentro de tu “Hierarchy” y arrástralo para agregárselo.
- Status Mods. No es necesario que lo modifiques, ya que es un listado para guardar las modificaciones de estatus durante la partida.
- Condition. No es necesario que le asignes nada, ya que el código lo hace en automático cuando se necesita.
- Lv. Escribe el nivel del personaje.
- Max HP. Escribe la salud máxima del personaje.
- Ap. Escribe los puntos de ataque del personaje.
- Dp. Escribe los puntos de defensa del personaje
- Speed. Escribe la velocidad del personaje. Entre más veloz, será el primero en atacar.

[Índice](https://www.notion.so/ndice-607df48a93ae435683e2c25b86fb9bf3) 

---

## Configurar las Skills de Cada Personaje

Crea un objeto vacío llamado “Skills” como hijo del respectivo personaje, para tener organizados las habilidades de cada personaje.

[Índice](https://www.notion.so/ndice-607df48a93ae435683e2c25b86fb9bf3) 

### Habilidades de Salud

Crea un objeto vacío hijo del objeto “Skills” ([Configurar las Skills de Cada Personaje](https://www.notion.so/Configurar-las-Skills-de-Cada-Personaje-5f81b3893f1d4b64ab53ef788e8849f7) ), agrégale como componente el script de “HealthModSkill” (Scripts > Skills > HealthModSkill), y rellena los campos con sus respectivos componentes.

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

[Índice](https://www.notion.so/ndice-607df48a93ae435683e2c25b86fb9bf3) 

---

## Habilidades con Efectos por Turnos

Crea objeto vacío hijo del objeto “Skills” ([Configurar las Skills de Cada Personaje](https://www.notion.so/Configurar-las-Skills-de-Cada-Personaje-5f81b3893f1d4b64ab53ef788e8849f7) ), agrégale como componente el script de “ApplySCSkill” (Scripts > StatusCondition> ApplySCSkills), y rellena los campos con sus respectivos componentes.

- Skill Name. Escribe el nombre de la habilidad.
- Animation Duration. Escribe cuántos segundos durará la animación de la habilidad.
- Targeting. Selecciona el tipo de víctima de la habilidad.
    - AUTO. El mismo personaje.
    - ALL_ALLIES. Todos los personajes dentro del mismo equipo.
    - ALL_OPPONENTS. Todos los personajes dentro del equipo enemigo.
    - SINGLE_ALLY. Un personaje dentro del mismo equipo.
    - SINGLE_OPPONENT. Un personaje dentro del equipo enemigo.
- Effect Prfb. Asigna el prefab del efecto que se va a crear en la misma posición de la víctima.

[Índice](https://www.notion.so/ndice-607df48a93ae435683e2c25b86fb9bf3) 

### Habilidades de Salud en Cada Turno

Crea un objeto vacío como hijo del objeto creado anteriormente([Habilidades con Efectos por Turnos](https://www.notion.so/Habilidades-con-Efectos-por-Turnos-4533b6ed8e99414c8c16ce5a0cfa11a0) ), y agrégale como componente el script “HealthModStatusCondition” (Scripts > StatusCondition > HealthModStatusCondition) y rellena los campos con sus respectivos componentes.}

- Effect Prfb. Asigna el prefab del efecto que se va a crear en la misma posición de la víctima.
- Animation Duration. Escribe cuántos segundos durará la animación de la habilidad.
- Reception Message. Escribe el mensaje que se imprimirá cuando se ejecute la habilidad.
- Apply Message. Escribe el mensaje que se imprimirá cada turno en que la habilidad esté activada.
- Expire Message. Escribe el mensaje que se imprimirá cuando la habilidad se desactive.
- Turn Duration. Escribe la cantidad de turnos que la habilidad estará activa.

[Índice](https://www.notion.so/ndice-607df48a93ae435683e2c25b86fb9bf3) 

### Habilidades para Bloquear Turnos

Crea un objeto vacío como hijo del objeto creado anteriormente([Habilidades con Efectos por Turnos](https://www.notion.so/Habilidades-con-Efectos-por-Turnos-4533b6ed8e99414c8c16ce5a0cfa11a0) ), y agrégale como componente el script “TurnBlockStatusCondition” (Scripts > StatusCondition >TurnBlockStatusCondition) y rellena los campos con sus respectivos componentes.}

- Effect Prfb. Asigna el prefab del efecto que se va a crear en la misma posición de la víctima.
- Animation Duration. Escribe cuántos segundos durará la animación de la habilidad.
- Reception Message. Escribe el mensaje que se imprimirá cuando se ejecute la habilidad.
- Apply Message. Escribe el mensaje que se imprimirá cada turno en que la habilidad esté activada.
- Expire Message. Escribe el mensaje que se imprimirá cuando la habilidad se desactive.
- Turn Duration. Escribe la cantidad de turnos que la habilidad estará activa.
- Block Chance. Desliza el deslizador para definir el porcentaje de probabilidad de bloquear los turnos de la víctima.

[Índice](https://www.notion.so/ndice-607df48a93ae435683e2c25b86fb9bf3) 

---

# Funcionalidades de Modificar el Código

## Stats

Los stats se modifican dentro del script que se haya agregado al personaje en [Configurar Personajes](https://www.notion.so/Configurar-Personajes-c08e24166e4e4f2da07d1ffc35d2b467), en el siguiente bloque de código precisamente:

```csharp
void Awake()
    {
        stats = new Stats(lv, maxHP, ap, dp, speed);
    }
```

Los tipos de dato que solicita son:

```csharp
Stats(int nivel, float saludMaxima, float ataque, float defensa, float velocidad);
```

Estos datos se pueden asociar con un sistema de progresión   asociando las variables correspondientes al sistema mencionado anteriormente.

[Índice](https://www.notion.so/ndice-607df48a93ae435683e2c25b86fb9bf3) 

---

# Recomendaciones

## Usa traductor para los errores si no sabes inglés

Los códigos y mensajes de error se encuentran en inglés debido a que es el lenguaje universal para estos. Sin embargo, el contenido es fácil de entender incluso traduciéndolo con algún traductor en línea, úsalo si así lo requieres.

## No modifiques los scripts!

Esto debido a que estos están relacionados entre sí. El sistema de combate está diseñado para solo agregar los scripts y prefabs en los objetos de tu proyecto y desde el mismo editor configurarlos para brindarte una mayor comodidad.

 Solo modifícalos para integrar el sistema a los sistemas que ya tienes en el proyecto (Sistemas de progresión, de movimiento, de inventario, etc.).

[Índice](https://www.notion.so/ndice-607df48a93ae435683e2c25b86fb9bf3) 

# Contacto

Dudas, comentarios y sugerencias las puedes enviar al siguiente correo con el asunto “GitHub CombatSystem”:

MontseG700@gmail.com

[Índice](https://www.notion.so/ndice-607df48a93ae435683e2c25b86fb9bf3)