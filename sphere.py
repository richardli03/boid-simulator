from numpy import pi, cos, sin, sqrt, arange, arccos
import numpy as np
import matplotlib.pyplot as plt
import random


def metric(points):
    
    comparisons = []
    all_minimums = []

    for sample in points:
        sample_point = np.array(sample)        
        for comparison_point in points:
            comparison_point = np.array(comparison_point)
            
            if comparison_point[0] == sample_point[0] and comparison_point[1] == sample_point[1] and comparison_point[2] == sample_point[2]:
                # print(f"{comparison_point} and {sample_point} are the same")
                continue
            
            comparisons.append(np.linalg.norm(sample_point-comparison_point))
            
        all_minimums.append(min(comparisons))
        comparisons = []
        
    return all_minimums

def compute_mins(phi, num_pts, indices):
    """
    Compute the average distance between a point and its 5 closest neighbors for both
    the randomly dropped points and the fibonacci points on a sphere

    Args:
        random (_type_): _description_
        fib (_type_): _description_
    """
     # Random sample
    random_theta = random.sample(range(0,6000),num_pts)
    x, y, z = cos(random_theta) * sin(phi), sin(random_theta) * sin(phi), cos(phi)
    random_points = list(zip(x,y,z))

    random_all_minimums = metric(random_points)

    
    # Regular sample
    fib_theta = 2* pi * ((1 + 5**0.5)/2) * indices
    x, y, z = cos(fib_theta) * sin(phi), sin(fib_theta) * sin(phi), cos(phi)
    fib_points = list(zip(x,y,z))

    fib_all_minimums = metric(fib_points)
    
    return np.average(random_all_minimums), np.average(fib_all_minimums), (x,y,z)
    
    

def circle():
    
    # r = sqrt(indices/num_pts)
    # x = r*cos(theta)
    # y = r*sin(theta)
    pass

def main():
    fig = plt.figure()
    ax = fig.add_subplot(projection='3d')

    num_pts_range = arange(100, 350, 100)
   
    
    for num_pts in num_pts_range:
        indices = arange(0, num_pts, dtype=float)
        indices += 0.8
        phi = arccos(1 - 2*indices/num_pts)
        avg_rand, avg_fib, (x,y,z)= compute_mins(phi, num_pts, indices)
    
        print(f"random distribution at {num_pts} points:{avg_rand}")
        print(f"fibonacci lattice at {num_pts} points:{avg_fib}")
    
    origin = np.array([[0, 0, 0],[0, 0, 0]]) # origin point
    
    ax.scatter(x,y,z, color = " #e63946 ")
    # ax.scatter(0,0,0, color = "red")
    # plt.quiver()
    plt.axis("equal")
    ax.grid(False)
    plt.axis("off")

    plt.savefig(f"{num_pts}.png", transparent = True)
    plt.show()


if __name__ == "__main__":
    main()