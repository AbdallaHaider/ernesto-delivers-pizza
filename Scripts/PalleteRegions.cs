using Godot;
using System;
using System.Linq;

public partial class PalleteRegions : Node2D
{
	Node[] areas;

	Vector3[,] palletes;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		areas = GetChildren().ToArray();
		palletes = new Vector3[4,4];

        palletes[0, 0] = new Vector3(90.0f / 255.0f, 57.0f / 255.0f, 33.0f / 255.0f);
        palletes[0, 1] = new Vector3(107.0f / 255.0f, 140.0f / 255.0f, 66.0f / 255.0f);
        palletes[0, 2] = new Vector3(123.0f / 255.0f, 198.0f / 255.0f, 123.0f / 255.0f);
        palletes[0, 3] = new Vector3(255.0f / 255.0f, 255.0f / 255.0f, 181.0f / 255.0f);

        palletes[1, 0] = new Vector3(0.0f / 255.0f, 33.0f / 255.0f, 69.0f / 255.0f);
        palletes[1, 1] = new Vector3(18.0f / 255.0f, 87.0f / 255.0f, 165.0f / 255.0f);
        palletes[1, 2] = new Vector3(64.0f / 255.0f, 180.0f / 255.0f, 229.0f / 255.0f);
        palletes[1, 3] = new Vector3(222.0f / 255.0f, 222.0f / 255.0f, 222.0f / 255.0f);

        palletes[2, 0] = new Vector3(50.0f / 255.0f, 47.0f / 255.0f, 61.0f / 255.0f);
        palletes[2, 1] = new Vector3(152.0f / 255.0f, 96.0f / 255.0f, 117.0f / 255.0f);
        palletes[2, 2] = new Vector3(238.0f / 255.0f, 148.0f / 255.0f, 137.0f / 255.0f);
        palletes[2, 3] = new Vector3(242.0f / 255.0f, 255.0f / 255.0f, 232.0f / 255.0f);

        palletes[3, 0] = new Vector3(57.0f / 255.0f, 44.0f / 255.0f, 66.0f / 255.0f);
        palletes[3, 1] = new Vector3(194.0f / 255.0f, 63.0f / 255.0f, 48.0f / 255.0f);
        palletes[3, 2] = new Vector3(229.0f / 255.0f, 124.0f / 255.0f, 72.0f / 255.0f);
        palletes[3, 3] = new Vector3(255.0f / 255.0f, 244.0f / 255.0f, 212.0f / 255.0f);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
