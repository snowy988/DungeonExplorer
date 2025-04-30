using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Player
    {
        public string Name { get; private set; }
        public int Health { get; private set; }
        private List<string> inventory = new List<string>();

        public Player(string name, int health = 100) 
        {
            Name = name;
            Health = health;
        }

        // Add an item to inventory
        public void PickUpItem(string item)
        {
            if (!string.IsNullOrEmpty(item))
            {
                inventory.Add(item);
                Console.WriteLine($"{item} added to inventory");
            }
            else
            {
                Console.WriteLine("No item to pick up");
            }

        }
        public string InventoryContents()
        {
            if (inventory.Count == 0)
                return "Inventory is empty";

            return string.Join(", ", inventory);
        }

        public void ShowStatus()
        {
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Health: {Health}");
            Console.WriteLine($"Inventory: {InventoryContents()}");
        }
    }
}