using Godot;
using Godot.Collections;
using System;
using System.Linq;

public partial class PalleteRegions : Node2D
{
    Dictionary<string,int> indexConnection = new Dictionary<string,int>();

    public String LastVisit;

    [Export] public Sprite2D townTitle;

	private Area2D[] areas;

    private Area2D current_area;

    public override void _Ready()
	{
        
        areas = GetChildren().OfType<Area2D>().ToArray();

        GD.Print(areas.Length);

        indexConnection.Add("Suburbslop", 3);
        indexConnection.Add("Daniel", 0);
        indexConnection.Add("UBCA", 1);
        indexConnection.Add("LiquidCity", 2);


        for (int index = 0; index < areas.Length; index++)
        {
            string areaName = areas[index].GetName().ToString();
            areas[index].AreaEntered += (body) => SwapIt(areaName);
            GD.Print(areaName);
        }

        RenderingServer.GlobalShaderParameterSet("pallete_index", 3);
    }

    public void SwapIt(string Areaname)
    {
        if (!string.Equals(Areaname, LastVisit))
        {
        TownNameShow.Instance.ShowTownSign(indexConnection[Areaname]);
        GD.Print(Areaname);
        RenderingServer.GlobalShaderParameterSet("pallete_index", indexConnection[Areaname]);
        LastVisit = Areaname;
        }    
    }
}
