using Godot;
using Godot.Collections;
using System;
using System.Linq;

public partial class PalleteRegions : Node2D
{
    Dictionary<string,int> indexConnection = new Dictionary<string,int>();

	private Area2D[] areas;


    private Area2D current_area;

    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        
        areas = GetChildren().OfType<Area2D>().ToArray();

        GD.Print(areas.Length);

        indexConnection.Add("Suburbslop", 2);
        indexConnection.Add("Daniel", 0);
        indexConnection.Add("UBCA", 1);
        indexConnection.Add("LiquidCity", 3);


        for (int i = 0; i < areas.Length; i++)
        {
            int index = i;
            areas[index].AreaEntered += (body) => SwapIt(body, areas[index].GetName().ToString());
            GD.Print(areas[i].GetName().ToString());
        }

        RenderingServer.GlobalShaderParameterSet("pallete_index", 3);
    }

    public void SwapIt(Area2D body, String name)
    {
        GD.Print(name);
        RenderingServer.GlobalShaderParameterSet("pallete_index", indexConnection[name]);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
    }
}
