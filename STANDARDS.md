# Asset Standards (draft v0.1)

> Valores propuestos por mí como punto de partida para un proyecto móvil 2D en Unity.
> No son los estándares de ningún estudio. Se ajustan en el asset `AssetValidatorRules`.

## Naming
- Sin espacios ni acentos, `PascalCase` tras el prefijo.
- Prefijos: `SP_` sprites y partes de personaje · `UI_` interfaz · `T_` otras texturas.
- Ejemplos válidos: `SP_HeroHead`, `UI_ButtonPlay`, `T_BackgroundSky`
- Ejemplos inválidos: `hero head.png`, `SP_hero`, `Final_v2_REAL`

## Texturas (import)
| Ajuste | Valor |
|---|---|
| Max Size | ≤ 2048 |
| Read/Write | desactivado |
| Generate Mip Maps | desactivado (2D) |
| Compression | activada |
| Pixels Per Unit (sprites) | 200 (canvas 2048 px, personaje de ~1970 px de alto ≈ 10 unidades; ajustar el tamaño de cámara) |

## "Ready for implementation"
- [ ] Capas del PSD con nombres según el prefijo
- [ ] Sin capas ocultas ni vacías
- [ ] El validador no devuelve errores
- [ ] Sprites incluidos en un Sprite Atlas

## Limitaciones conocidas
- El validador solo lee los ajustes *Default* del importer, no los overrides por plataforma.
- No comprueba el tamaño del archivo fuente, solo el Max Size del import.
