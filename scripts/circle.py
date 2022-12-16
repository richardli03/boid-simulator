from numpy import pi, cos, sin, sqrt, arange
import matplotlib.pyplot as plt
import matplotlib as mpl
# cir = mpl.patches.Circle((0,0), radius = 1, fill = False)

num_pts = 300
indices = arange(0, num_pts, dtype=float)

r = (indices/num_pts) **0.5
print(r)
theta = pi * (1 + 5**0.5) * indices

plt.scatter(r*cos(theta), r*sin(theta), c= "#e63946")
ax = plt.gca()
# ax.add_patch(cir)
plt.axis("equal")
ax.grid(False)
plt.axis("off")
plt.savefig(f"circle_{num_pts}.png", transparent = True)
plt.show()
