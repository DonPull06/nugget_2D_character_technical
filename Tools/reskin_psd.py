import sys, numpy as np
from psd_tools import PSDImage
from psd_tools.api.layers import PixelLayer
from psd_tools.constants import Compression
from PIL import Image

def hx(h): h=h.lstrip('#'); return np.array([int(h[i:i+2],16) for i in (0,2,4)],float)

# (origen, destino)
MAP = [
 ("#776A63","#62779B"),  # pelaje cabeza/cuerpo
 ("#8A7A70","#7E93B5"),  # pelaje extremidades
 ("#B8AEA2","#D3DCE8"),  # barriga
 ("#E9BAA7","#F5BE8C"),  # palmas y pies
 ("#C3A5AC","#DB9C66"),  # hocico
 ("#A2757F","#8A4A2E"),  # fosas nasales
 ("#F1E3D0","#F1E3D0"),  # garras (sin cambio)
 ("#D9C7AE","#D9C7AE"),  # garras sombra (sin cambio)
]
SRC=np.array([hx(a) for a,_ in MAP]); DST=np.array([hx(b) for _,b in MAP])
KEEP_LAYERS=("Eyes","Pupil")  # ojos y pupilas sin tocar

def recolor(rgb):
    h,w,_=rgb.shape
    px=rgb.reshape(-1,3).astype(float)
    d=np.linalg.norm(px[:,None,:]-SRC[None,:,:],axis=2)
    order=np.argsort(d,axis=1)
    i1=order[:,0]; i2=order[:,1]
    c1=SRC[i1]; c2=SRC[i2]
    v=c2-c1; vv=(v*v).sum(1)+1e-9
    t=np.clip(((px-c1)*v).sum(1)/vv,0,1)
    proj=c1+v*t[:,None]
    dist_line=np.linalg.norm(px-proj,axis=1)
    on_line=(dist_line<14)&(t>0.02)
    out=np.empty_like(px)
    # cerca de la recta entre dos colores: mezcla de destinos
    out[on_line]=DST[i1[on_line]]*(1-t[on_line,None])+DST[i2[on_line]]*t[on_line,None]
    # resto: desplazamiento respecto al color mas cercano
    off=~on_line
    out[off]=DST[i1[off]]+(px[off]-c1[off])
    return np.clip(out,0,255).round().astype(np.uint8).reshape(h,w,3)

src,dst=sys.argv[1],sys.argv[2]
psd=PSDImage.open(src)
layers=list(psd)
new_layers=[]
for l in layers:
    name=l.name.rstrip('\0')
    img=l.topil().convert("RGBA")
    a=np.array(img)
    if not any(k in name for k in KEEP_LAYERS):
        a[:,:,:3]=recolor(a[:,:,:3])
    nl=PixelLayer.frompil(Image.fromarray(a,"RGBA"),psd,name=name,top=l.top,left=l.left,compression=Compression.RLE)
    nl.visible=l.visible
    nl.opacity=l.opacity
    new_layers.append(nl)
# quitar las capas originales y reinsertar en el mismo orden
for l in layers: psd.remove(l)
for nl in new_layers: psd.append(nl)
psd.save(dst)
print("guardado",dst)