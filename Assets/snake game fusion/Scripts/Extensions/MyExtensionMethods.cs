using System.Linq;
using UnityEngine;
using Fusion;

public static class MyExtensionMethods
{
    public static CellScript[] GetMazeCells(this MazeGenerator _)
    {
        var cellScritps =  Object.FindObjectsOfType<CellScript>();
        var cellScriptsNO = cellScritps.Select(i => i.GetComponent<NetworkObject>());
        var sorted = cellScriptsNO.OrderBy(i => i == i.Id).Reverse();

        return sorted.Select(i => i.GetComponent<CellScript>()).ToArray();
    }
}
