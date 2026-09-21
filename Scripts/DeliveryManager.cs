using Godot;
using System.Linq; // Required for sorting/randomizing the list

public partial class DeliveryManager : Node
{
    public static DeliveryManager Instance { get; private set; }
    
    public int ActiveDeliveries { get; private set; } = 0;

    public override void _Ready()
    {
        Instance = this;
    }

    public void StartDeliveryRound(int amountOfHouses)
    {
        // Find all houses in the group
        var allHouses = GetTree().GetNodesInGroup("Houses").OfType<House>().ToList();
        
        // Randomly shuffle the list
        var random = new RandomNumberGenerator();
        random.Randomize();
        var shuffledHouses = allHouses.OrderBy(x => random.Randf()).ToList();

        // Set the counter based on how many houses actually exist
        ActiveDeliveries = Mathf.Min(amountOfHouses, shuffledHouses.Count);

        // Open the windows for the selected ones
        for (int i = 0; i < ActiveDeliveries; i++)
        {
            shuffledHouses[i].OpenWindow();
        }
    }

    public void HouseDelivered()
    {
        ActiveDeliveries--;
        
        // Add points to your existing score system!
        ScoreManager.Instance.AddScore(50); 
        
        GD.Print("Houses left: " + ActiveDeliveries);

        if (ActiveDeliveries <= 0)
        {
            GD.Print("All pizzas delivered!");
            // You can trigger the next wave or a win screen here
        }
    }
}