using System;
using System.Media;

namespace DungeonExplorer
{
    internal class Game
    {
        private Player player;
        private Room currentRoom;

        public Game()
        {
            // Initialize the game with one room and one player
            Console.WriteLine("Welcome to Dungeon Explorer!");
            Console.WriteLine("Please enter characters's name: ");
            string name = Console.ReadLine();

            player = new Player(name);
            currentRoom = new Room("You are in a dungeon");
        }
        public void Start()
        {
            // Playing logic is true and loop plays beginning of game
            bool playing = true;

            Console.WriteLine("Hello " + player.GetName() + "The game has started");
            Console.WriteLine("You can: look, pickup, status, or exit");

            while (playing)
            {
                Console.WriteLine();  //Added empty line for spacing
                Console.WriteLine("What do you want to do? ");
                string command = Console.ReadLine();

                // else if for player commands
                if (command == "look")
                {
                    Console.WriteLine(currentRoom.GetDescription());
                }
                else if (command == "pickup")
                {
                    player.PickUpItem("Torch");
                    Console.WriteLine("You picked up a Torch");
                }
                else if (command == "status")
                {
                    Console.WriteLine("Your name is: " + player.GetName());
                    Console.WriteLine("You are carrying: " + player.GetInventory());
                }
                else if (command == "exit")
                {
                    Console.WriteLine("Goodbye!");
                    playing = false;
                }
                else
                {
                    Console.WriteLine("Sorry, that isnt a command");
                }
            }
        }
    }
}