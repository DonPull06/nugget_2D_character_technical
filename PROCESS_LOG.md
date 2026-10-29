# Process log — Lingokids Game Technical Artist portfolio

Objetivo: dos piezas pequeñas para la candidatura a Game Technical Artist (Lingokids), a entregar el **29/10/2026** como máximo.
Oferta: https://jobs.lingokids.com/jobs/8592957-game-technical-artist (publicada 22/10/2026, remoto en España)

## 2026-10-26

### Análisis de la oferta
- Pide: herramientas/validación en C# para el editor, estándares de assets (naming, export, "ready for implementation"), prefabs y reskins reutilizables, atlas, rendimiento móvil.
- Spine es *nice to have*. Están evaluando pasar de Adobe Animate a Spine. No es requisito.
- Huecos míos reales: Shader Graph / render pipelines, integración 2D en un juego publicado, validación en dispositivo.
- Experiencia con asistentes de IA para código es un plus, siempre con criterio de revisión.

### Decisiones
| Decisión | Motivo |
|---|---|
| Unity (última versión LTS/recomendada) + URP 2D | Plantilla Universal 2D; paquetes 2D mejor probados en LTS |
| Sin Spine: paquete oficial **2D Animation** + **2D PSD Importer** (gratis) | La trial de Spine no guarda ni exporta ni permite usar los runtimes; Spine no es requisito |
| Reskin con Sprite Library / Sprite Resolver | Cubre el requisito sin shader |
| Recortes por el plazo: sin auto-fix, sin shader, sin tests, 2 animaciones, 6-8 piezas | Del 26 al 29 solo hay 4 días (~18 h) |
| Aprender editor scripting **tecleando el código yo mismo**, paso a paso | La oferta pide crear y mantener herramientas; tengo que poder defenderlo |
| Android Build Support (+ OpenJDK, SDK & NDK) al instalar Unity | Para un posible build en móvil; no hace falta para el resto |

### Calendario
| Día | Qué |
|---|---|
| Lun 26 | Setup + validador funcionando (reglas, ventana, 1 regla) · diseño del personaje |
| Mar 27 | Validador cerrado (reglas, export, README, GIF) · PSD en capas |
| Mié 28 | Importar PSB, huesos y pesos, idle + acción |
| Jue 29 | Prefab, reskin, atlas, validador sobre el personaje, GIFs, README, subir a GitHub |

### Pieza 1 — Validador de assets
- Plan en pasos pequeños: (1) `AssetValidatorRules` (ScriptableObject) → (2) ventana vacía → (3) escaneo + regla de naming → (4) resto de reglas → (5) informe y export.
- Nota: se enviaron antes tres archivos con código completo (`AssetValidatorRules.cs`, `AssetValidatorWindow.cs`, `STANDARDS.md`). El código no se compiló en Unity. Se decidió empezar de cero y teclearlo paso a paso. `STANDARDS.md` se conserva como borrador.
- Estado: pendiente de empezar (esperando instalación de Unity).

### Pieza 2 — Personaje 2D
**Concepto:** un topo gigante, torpe, que no controla su volumen y tiene miedo de lo creativo y lo delicado por su tamaño. Rollo: el "grandullón al que sobrestiman", al revés que el trope del gordo subestimado.
- Pose: manos enormes sujetando algo diminuto (flor, lápiz), hombros encogidos, mirada al objeto.
- Animaciones: `Idle_Nervous` y `Reach_Hesitate`.
- Piezas (máx. 8): Body, Head, Eyes, ArmL, ArmR, LegL, LegR, Prop. Nombres `SP_[Nombre]…`.
- Reglas de corte: piezas solapadas, formas planas, lienzo 2048×2048, personaje de ~1500 px de alto.

**Comprobaciones de originalidad**
- Personajes de Lingokids (Baby Bot, Billy, Elliot —panda—, Lisa, Cowy —la artista—): ninguno es un topo ni un grandullón torpe. Aviso: Cowy comparte el terreno de "lo creativo". Fuentes: https://lingokids.com/lingokids-universe-old-2023 y https://lingokids.com/press/lingokids-creative-director-pocoyo-creator-guillermo-garcia-carsi (la página es de 2023).
- Topos existentes en infantil: Moley (https://en.wikipedia.org/wiki/Moley), Krtek (https://www.tvtropes.org/pmwiki/pmwiki.php/Animation/TheLittleMole), Mole Sisters. El concepto (gigante + miedo a lo delicado) no aparece, pero la especie está muy vista. **Decisión: mantener el topo**, diseñándolo para que no se parezca a esos (sin ojos enormes redondos tipo Krtek).
- Limitación: búsqueda solo de texto, sin imágenes.

**Revisión del primer boceto:** anatomía muscular, casco sin cara, protecciones y pose sentada. Descartado por (1) leerse como bruto, contrario al concepto, (2) imposible de cortar y pesar en 3 días, (3) demasiado realista para 2D con huesos. Se conserva: masa, proporciones (torso enorme, cabeza pequeña, manos/pies grandes) y postura encogida.

**Referencia visual:** se decide generar una referencia con Gemini.
> Regla propia: la referencia se usa solo para orientar. El arte final se dibuja desde cero, sin calcar. En el README del portfolio se indicará que existió esa referencia y cómo se usó.

**Boceto 2 (a lápiz, de pie, de frente) — revisión 26/10 por la tarde**
- Funciona: silueta (masa grande, cabeza pequeña, brazos grandes), vientre en huevo, proporciones y patas cortas.
- A corregir: (1) sonrisa y ojos muy abiertos desmienten el miedo; (2) se lee como perezoso/gorila más que topo: hocico largo en tres cuartos mientras el cuerpo está de frente; (3) manos de 4 garras largas y puntiagudas sin palma, mucho más pequeñas que la cabeza; (4) pose neutra, sin hombros caídos ni objeto; (5) orejas tipo persona/mono.
- Para el rig: brazos demasiado gruesos ocultan el torso; brazo+mano en una sola pieza; la flor como pieza aparte.
- Siguiente: segunda versión del boceto con estos cambios.

**Boceto 3 (27/10, a lápiz, de frente) — revisión contra la checklist**
- Conseguido: manos grandes con palma y uñas romas; boca apretada ondulada (se lee preocupado); ojos más juntos y bajos; hocico centrado y corto; brazo y mano en una pieza.
- Pendiente: (1) sigue leyéndose más gorila/cerdo que topo: orejas a los lados a la altura de los ojos, hocico ancho y chato en vez de puntiagudo, cabeza plana por arriba; (2) piernas sin resolver: bloque único con pies puntiagudos hacia fuera, hay que separarlas en dos piezas con la cadera oculta bajo el vientre; (3) las cejas no se leen (aros sobre los ojos); (4) manos asimétricas (la derecha tiene un pulgar extra); (5) hombros altos, no caídos (vale como encogimiento si la cabeza se separa mejor del cuerpo).
- Propuesta (pendiente de confirmar): nombrar las capas `SP_Mole…` (p. ej. `SP_MoleHead`) sin esperar al nombre del personaje, y pasar ya a Photoshop sin una versión 4 en papel.

**Herramienta de dibujo (27/10)**
- Photoshop frente a Illustrator: se descarta Illustrator. El importador oficial de Unity trabaja con .psb (y .psd eligiendo PSD Importer a mano); las docs no mencionan Illustrator y el CV no lo lista. Para el look plano: capas de forma (Pen/Shape).
- 10:44: la licencia de Photoshop parece caducada. Opciones: renovar o prueba de 7 días (Adobe dice una sola por persona; no verificado que su cuenta sea elegible) o Krita (gratis; exporta PSD pero no PSB).
- Riesgo con Krita: un hilo del foro de Krita reporta que su PSD llegó a Unity con las capas aplanadas, sin causa confirmada. Las docs de Unity dicen que un .psd usa por defecto el Texture Importer y hay que elegir PSD Importer en el desplegable.
- Plan: dibujar en Krita, pero antes hacer una prueba de 3 capas en Unity; si las capas no sobreviven, abrir el PSD en una herramienta que guarde .psb.
- PhotoCraft (proyecto open source `storytold/photocraft` en GitHub, en alfa temprana): según su wiki abre y guarda PSD/PSB y lee archivos de Krita. Candidato a convertidor Krita → .psb. Sin verificar con el importador de Unity; fidelidad PSD incompleta según los propios autores. No confundir con ArtCraft Studio (la herramienta de generación con IA de la misma familia), que no se usa.
- Prueba en 2 pasos cuando haya Unity: (a) PSD de Krita directo con PSD Importer; (b) si falla, Krita → PhotoCraft → .psb.

**Spec del archivo de Krita (27/10)**
- Lienzo 2048×2048 px, RGB/Alpha, 8 bits, sRGB, fondo transparente (borrar la capa Background vacía). Personaje de ~1500 px de alto, centrado, simétrico, pose neutra de frente.
- PPU en Unity: 300 (→ 5 unidades de alto). Cambiado en STANDARDS.md (antes 100).
- Capas, de arriba abajo: `SP_MoleSnout` (hocico en capa propia, encima de los ojos; añadida 27/10 12:28 porque los ojos solapan el hocico y, con Eyes sobre Head, lo taparían), `SP_MoleEyesNeutral` (+ `EyesWorried`, opcional `EyesLook`; ojos + cejas), `SP_MoleHead`, `SP_MoleArmL`, `SP_MoleArmR`, `SP_MoleBody`, `SP_MoleLegL`, `SP_MoleLegR`. L/R desde el punto de vista del personaje.
- Cada pieza se dibuja completa, incluso lo que otra tapa (hombro bajo la cabeza, torso hasta el centro de la cabeza, muslo bajo la barriga).
- Capas: Normal, 100 %, sin máscaras, sin grupos, sin herencia de alfa, sin capas de filtro ni vectoriales. Contorno de 6 px dentro de cada pieza.
- Archivos: `SP_MoleDefault` y `SP_MoleReskin` (.kra maestro, exportado a .psd/.psb). El reskin comparte nombres, estructura y posiciones de capas; solo cambia el color.
- Paleta definitiva (27/10, 12:39), archivos `SP_MoleDefault.gpl` y `SP_MoleReskin.gpl`. Default: pelaje `#8A7468` / sombra `#6E5A50`; barriga `#D8C2A2` / `#BFA786`; hocico y palmas `#E9A3A8` / `#D18890`; fosas `#7A3F4A`. Reskin: pelaje `#7E93B5` / `#62779B`; barriga `#D3DCE8` / `#B3C0D3`; hocico y palmas `#F5BE8C` / `#DB9C66`; fosas `#8A4A2E`. Comunes: contorno y boca `#2E2320`, cejas `#3D2433` (ciruela, elegida 27/10 13:25), pupila `#1E1A1A`, blanco del ojo `#FFFFFF`, uñas `#F1E3D0` / `#D9C7AE`.
- Se aclara el pelaje respecto a la propuesta inicial (`#6B5A57`) porque las cejas apenas contrastaban (2,65:1). Con el pelaje nuevo, cejas 3,92:1 y contorno 3,47:1 (ratio de contraste WCAG calculado con script).
- Decisión (27/10, 11:31): se elimina la flor y la capa `SP_MoleProp`; pose natural. La segunda animación pasa a ser `Hesitate`: la mano se acerca hacia la cámara, se frena y retrocede, y los ojos cambian a Worried. La flor puede añadirse al final como sprite suelto hijo del hueso de la mano, sin tocar el rig.
- Revisión de la cabeza en Krita (27/10 13:25, con cejas ciruela). Colores medidos en la imagen: pelaje `#6E655E`, cejas `#462E3B`, hocico `#B79FA7`, fosas `#9A6D78`; no coinciden con la paleta (más grises y apagados). Contraste cejas/pelaje real: 2,15:1 (con el pelaje de la paleta saldría ~3,2).
  - A corregir, por orden: (1) hocico ancho y ovalado con dos fosas grandes, se lee cerdo, y tapa la parte baja de las pupilas; (2) cejas como óvalos horizontales flotando, no leen preocupación; (3) orejas aún a la altura de los ojos (gorila); (4) sin contorno, cabeza y cuerpo se funden en el mismo color; (5) colores fuera de paleta (¿intencional o perfil de color?); (6) pupilas sin brillo.
  - Funciona: silueta de cúpula simple, mirada hacia abajo, blancos grandes con buen contraste con la pupila, orden de capas (hocico sobre ojos).
- Personaje completo, versiones Neutral y Worried (27/10 14:36). Lienzo 2048×2048 con fondo transparente; personaje de ~1850×1970 px (llena el lienzo; con PPU 300 serían ~6,6 unidades, valorar PPU 400 ≈ 4,9). Colores medidos: pelaje `#776A63`, barriga `#B8AEA2`, palmas y pies `#E9BAA7`, uñas `#F1E3D0`, hocico `#C3A5AC`, fosas `#A2757F`, cejas `#2B1B14` (marrón oscuro, opción 2 de la segunda comparación; ~3,2:1 con el pelaje), párpado y boca `#4F3E35`, pupila `#000000`. La paleta real ya no coincide con los `.gpl`.
  - Funciona: manos rosas con uñas crema (lo que más lee "topo"), paleta apagada y coherente, barriga, versión Worried (párpados + cejas inclinadas + boca ondulada) se lee al instante, piernas y pies claros, fondo transparente.
  - A corregir: (1) hocico ancho con dos fosas frontales, sigue leyéndose cerdo; (2) pelaje idéntico en cabeza, brazos y torso, sin contorno ni sombra: al rotar un brazo solo cambiará la silueta; (3) uñas largas y finas (≈ largo de la palma), parecen dedos; (4) orejas a la altura de los ojos; (5) la boca cambia entre expresiones, así que debe ir en la capa de expresión; pupilas sin brillo; (6) limpieza: marca suelta en la barriga, líneas finas en palmas y entre uñas, pequeños escalones en la silueta entre cabeza y hombros.
- Decisión (27/10 14:39): se deja el diseño sin sombras ni contorno, siguiendo el estilo plano de Lingokids (criterio del usuario; no verificado por mí). Riesgo pendiente: que el giro de un brazo no se lea al ser del mismo color que el torso. Comprobación: animar el brazo ±10° en Unity; si no se lee, separar con un tono plano algo distinto en el brazo o con un contorno fino. Siguen pendientes por rig: boca en la capa de expresión y limpieza de marcas.
- Separación plana por tono (27/10 14:51): brazos y piernas pasan a `#7E6E65` frente al torso `#776A63` (ratio 1,07:1) y la cabeza sigue igual que el torso (1,00). Demasiado sutil en móvil. Propuesta (pendiente de decidir): torso `#6B5F58`, cabeza `#776A63`, brazos y piernas `#8A7A70` → 1,50:1 y 1,18:1; cejas sobre cabeza se quedan en 3,17:1. Comprobación: reducir la imagen al 25 % y ver si se distinguen las piezas. Siguen la marca de la barriga, la línea entre uñas y un pequeño pico en el muslo y en la unión cabeza-hombro.
- Separación por tono aplicada (27/10 16:16): brazos y piernas `#8A7A70`; torso y cabeza se quedan en `#776A63` (no se oscureció el torso). Medido: brazo/torso 1,27:1, cabeza/torso 1,00, barriga/torso 2,39, cejas/cabeza 3,17. Prueba al 15 % del tamaño y en escala de grises: brazos y piernas se distinguen bien; cabeza y torso se leen como una masa. Se acepta: la cabeza se leerá por sus rasgos (ojos, hocico, boca) y un mismo color oculta huecos al girarla. Opcional: torso `#6B5F58` (cabeza/torso 1,18:1).
  - Pendiente: separar en capas con nombres `SP_Mole…`, boca en la capa de expresión, limpiar marca de la barriga y línea entre uñas, exportar PSD y probar importación en Unity.
- Técnica (27/10): contornos y formas con la herramienta Bezier Curve de Krita sobre capa vectorial (Autosmooth Curve activado), ajuste fino con Shape Edit, y conversión final con Layer > Convert > Convert to Paint Layer. La versión vectorial se guarda en un .kra aparte; el .kra de exportación solo lleva capas de pintura.

### Pendiente
- Nombre del personaje (hay que fijarlo antes de nombrar capas).
- Resultado de la referencia de Gemini.
- Instalar Unity, crear el proyecto y el paso 1.
- Enlace al portfolio para el CV (lo pasa el usuario en otro chat).
- CV: Captain's Crew termina 09/2026 (confirmado). En el perfil, no afirmar "C# tooling" hasta tener la herramienta hecha.

## 27/10 — Revisión del PSD `Topo.psd` (para import en Unity)
Hallazgos:
- Vista frontal y vista de espaldas conviven en el mismo PSD (visibles: MoleBody_Espaldas, ArmL/R_Espaldas, LegL/R_Espaldas, MoleHead; ocultas: variantes frontales, ojos, hocico). Con "Import Hidden Layers" desactivado Unity importaría la vista de espaldas.
- Nombres que no cumplen el regex del validador `^(SP|UI|T)_[A-Z][A-Za-z0-9]*$`: `SP_Eyes_Neutral`, `SP_Eyes_Worried Fusionadas`, `SP_MoleBody_Espaldas`, `SP_LegR_Espaldas`, etc. (guion bajo extra, espacio, sufijo "Fusionadas"). Posible carácter NUL al final de algunos nombres (verificar en Unity).
- Convención L/R: SP_LegR está a la derecha de la imagen y SP_LegL a la izquierda. Documentar: L/R = lado de la imagen, no del personaje.
- Tamaño: bbox frontal total 6.088.628 px (1,45 × 2048²). Brazos: 25-28 % de relleno por solapar bajo el torso. A 50 % (canvas 1024) cabe de sobra en 2048.
- Las capas de brazos de espaldas muestran palma rosa con garras: revisar si de espaldas debería verse el dorso (pelaje).
- Cabeza compartida entre frente y espalda (ojos y hocico en capas aparte): bien resuelto.
Decisiones pendientes: separar PSD de espaldas, renombrar capas, escalar a 50 %, test de import en Unity.

## 27/10 16:40 — Segunda revisión: Topo.psd / Topo_Espaldas.psd / Topo_Preocupado.psd
- Resuelto: frente y espalda separados, todo visible, nombres sin espacios ni guiones bajos extra, hocico y ojos en capas propias, cabeza compartida.
- Pendiente: (1) píxeles sueltos (384 px en frente, 1364 en espaldas) inflan los bbox; en SP_EyesWorried hay un punto gris en (≈2524, 821), fuera del canvas de 2048, que hace el bbox de 1667 px de ancho. (2) Nombres con NUL final en las tres PSD (verificar en Unity). (3) Nombres de espaldas: SP_LegREspaldas etc. cumplen el regex pero pierden la separación visual; opcional SP_LegRBack. (4) Atlas: con basura limpiada ≈0,88× 2048² en frente; con Preocupado ≈1,1× aún por el punto. (5) Nombre del personaje: Molly (Molly the Mole), pendiente de confirmar.

## 27/10 16:55 — Comprobación de motas y NUL (Topo_Preocupado cdc803fb)
- Punto gris en SP_EyesWorried: SIGUE ahí. Círculo de 1387 px, alpha 255, bbox (2483,780)-(2525,822) → 435 px fuera del canvas (2048). Hay que borrarlo.
- Motas (coordenadas de canvas): SP_ArmR tiene ~40 fragmentos semitransparentes (alpha 43-115) en x 495-725, y 612-1141, que son restos del contorno del brazo izquierdo (huella de duplicar/voltear). SP_ArmL tiene 2 fragmentos de ~40 px en (1443-1461, 452-489) y (1516-1523, 1205-1237): restos del brazo derecho. SP_MoleHead: fragmentos casi invisibles (alpha ≤31) en x 1431-1471, y 262-310. SP_MoleBody: fragmentos alpha ≤67 en los bordes izquierdo (x 618-648, y 1573-1720) y derecho (x 1457-1477). Piernas y hocico limpios.
- NUL: el nombre Pascal del PSD es limpio ("SP_LegR", 7 caracteres); el NUL está en el bloque Unicode `luni` (terminador, "SP_LegR\0"), lo escribe el programa que guarda el PSD. No es un error del usuario. Comportamiento en Unity: pendiente de probar.

## 28/10 08:58 — Alcance: solo frontal + preocupado
- Decisión: la vista de espaldas queda fuera del alcance (Topo_Espaldas.psd no se usa).
- Estado: Topo_Preocupado.psd (a14ed9fc) con brazos limpios; falta borrar el punto de SP_EyesWorried (bbox hasta x=2525). Topo.psd (c246c8fe) con brazos aún sin limpiar. Fragmentos de cuerpo/cabeza (alpha casi 0) sin tocar en ambos; no bloquean el test de import.
- Siguiente: test de import en Unity con Topo_Preocupado.psd tras quitar el punto.

## 28/10 09:32 — Nombre del personaje: Nugget (decidido)
- Personaje masculino. Nombre: **Nugget** (*Nugget the Mole*). Concepto: pepita = algo pequeño y valioso que se encuentra cavando; contraste con su tamaño enorme y su miedo a romper cosas.
- Descartados y por qué: Molly (nombre femenino); Milo (ya existe "Milo the Mole" en varios libros y Milo's World, app preescolar de DeAPlaneta); Moley (serie de Rainbow Productions y audiolibros); Dugo (suena a Duggee, de Hey Duggee); Bomo (BMO); Tupo (Tupu); Lumo (videojuego); Tippy (cerca de Tappy Mole, app).
- Alternativas inventadas que pasaron la búsqueda rápida: Torpo, Talpi, Mullo, Grumo, Clumpo.
- Riesgo conocido: Nugget es palabra común (Nugget de Super Meat Boy y Ace Combat, libro "Chicken Nugget", Denver Nuggets). No se ha visto ningún topo con ese nombre, pero la búsqueda fue rápida. Pendiente: mirar Google Play, App Store y TMview antes de uso comercial; para el portfolio basta.
- Capas del PSD siguen como SP_Mole… (el nombre no afecta a los assets).

## 28/10 09:40 — Primer import en Unity (PSD Importer)
- Al principio el asset usaba TextureImporter (aplana el PSD). Cambiado a UnityEditor.U2D.PSD.PSDImporter: aparecen las capas.
- Ajustes: Sprite Mode Multiple, Import Mode Individual Sprites (Mosaic), Include Hidden Layers ON (para SP_EyesWorried), PPU 400, Generate Physics Shape OFF, Use as Rig ON, Pivot Bottom, Main Skeleton None.
- Resultado de Print Sprite Names: 9 sprites con nombre + un carácter extra (SP_MoleHead length=12), y 9 objetos con nombre limpio (length=11), además de "Nugget" (prefab) y "Nugget Skeleton" (skeleton asset generado por Use as Rig). El carácter extra es el NUL del bloque `luni` del PSD; llega a Unity en los sprites.
- Pendiente: confirmar qué tipo de objeto lleva cada nombre; mover PrintSpriteNames.cs a una carpeta Editor (ahora está en Assets/Nugget/, rompería el build).

## 28/10 14:37 — Validador funcionando sobre el personaje
- Validador (EditorWindow + ScriptableObject de reglas): naming, Max Size, Read/Write, Mipmaps, Compression, PPU. Con PSDImporter valida el asset ya importado (tamaño de textura, legibilidad, mipmaps, PPU de los sprites) porque no es un TextureImporter.
- Hallazgos reales sobre SP_Nugget.psd: PPU 200 frente al esperado 400 en 10 sprites y mipmaps activados. Corregidos por el autor; el último escaneo del PSD sale limpio (solo el Info del importer).
- Limitaciones conocidas: no mira overrides por plataforma, no valida el tamaño del archivo fuente (solo el Max Size del import), no comprueba la compresión en el camino PSDImporter.
- PPU decidido: 400 (a confirmar el valor final con el Inspector). Se cambió el aviso de PPU a uno por asset.
- Pendiente: informes "antes/después" exportados, cerrar el rig (padre de los brazos, Bone Influence), reskin, atlas, GIFs, GitHub/README.

## 28/10 14:40 — Informes del validador guardados
- Before (Assets/ValidatorTest, 3 texturas): 3 errores de Naming y 3 avisos de PPU (100, esperado 200). Solo se ejercitan 2 de las 6 reglas.
- After (Assets/Nugget, 1 textura): sin errores ni avisos; solo el Info del importer (PSDImporter).
- Decisión: expectedPixelsPerUnit = 200 (coincide con el PSD). STANDARDS.md actualizado de 400 a 200; con un canvas de 2048 el personaje mide ~10 unidades de alto.
- Reskin aplazado al 29/10.

## 29/10 09:10 — Reskin por script (opción elegida por el autor)
- Script `reskin_psd.py` (psd-tools + numpy): remapea la paleta plana capa por capa (pelaje cabeza/cuerpo #776A63→#62779B, extremidades #8A7A70→#7E93B5, barriga #B8AEA2→#D3DCE8, palmas/pies #E9BAA7→#F5BE8C, hocico #C3A5AC→#DB9C66, fosas #A2757F→#8A4A2E; garras, ojos y pupilas sin cambio). Preserva nombres, posiciones, orden y visibilidad de capas; mezcla en los bordes entre colores para no dejar halos.
- Probado sobre una copia antigua del PSD (sin SP_PupilR/SP_Pupils): resultado visualmente correcto. Falta ejecutarlo sobre el PSD actual.
- Los nombres de capa del PSD generado salen sin el NUL final.
- Pendiente tras generar: importar, Copy/Paste Rig, Sprite Libraries, prefab variante.

## 29/10 10:10 — Draw calls: mediciones previas al atlas (NO concluyentes)
- Frame Debugger (Unity 6.6, URP 2D), sin atlas, capturas sueltas: "1" evento en Renderer2D Pass con un Nugget; "5" con dos; "3" con la variante sola (Draw Dynamic / SRP Batch / Draw Dynamic).
- Las mediciones no se hicieron con condiciones controladas (estado de animación, objetos en escena) y el panel Details del Frame Debugger salió vacío, así que no se pudo ver textura ni motivo de ruptura del batch.
- Descartado: Library_Reskin apuntando a sprites del PSD original (revisado: todo apunta al reskin).
- Pendiente: medir con Game > Stats (Batches) con PF_Nugget solo, PF_Nugget_Reskin solo y ambos, mismo frame de animación, antes y después de activar el Sprite Atlas. Solo se documentan en README/CV las cifras de esa medición controlada.
- Nota: 3 draw calls por personaje no es un problema de rendimiento; solo importa para poder afirmar con datos qué mejora el atlas.

## 29/10 10:20 — Draw calls medidos (Game > Stats, Play, URP 2D, Unity 6000.6.7f1)
Criterio: "Draw Calls" de Stats en el Game View (incluye 1 draw call que no es de sprites). Sin atlas = Project Settings > Editor > Sprite Atlas > Mode "Sprite Atlas V2 - Enabled for Builds"; con atlas = "Sprite Atlas V2 - Enabled" (restaurado al terminar).

| Escena | Sin atlas | Con atlas |
|---|---|---|
| PF_Nugget solo | 2 | 2 |
| PF_Nugget_Reskin solo | 5 | 5 |
| Los dos a la vez | 15 | 6 |

- El atlas no cambia nada con un solo personaje; con los dos baja de 15 a 6 (texturas separadas -> una).
- Sin explicar: la variante sola hace 4 draw calls de sprites (2 SRP Batcher + 2 Non-SRP Compatible) y el original 1, con el mismo rig y piezas. Pendiente de investigar; no es un problema de rendimiento a esta escala.
- Frame Debugger y Stats cuentan distinto; usar solo Stats para comparar. El panel Details del Frame Debugger salió vacío en esta versión.

## 29/10 10:58 — Draw calls sin remedir
- Los ojos pasaron a un único objeto con Sprite Resolver (Category Eyes, labels Neutral/Worried, keyframes en Constant) después de medir los draw calls. Decisión: no remedir; el README lo indica ("medido antes del cambio de ojos").
