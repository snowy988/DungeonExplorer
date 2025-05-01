using System;
using System.Media;
using static System.Net.Mime.MediaTypeNames;

namespace DungeonExplorer
{
    public class Game
    {
        private Player player;
        private Room currentRoom;
        private bool playing;

        public Game()
        {
            // Initialize the game with one room and one player
            Console.WriteLine("Welcome to Dungeon Explorer!");
            Console.WriteLine("Please enter your character's name: ");
            string name = Console.ReadLine();

            player = new Player(name);
            currentRoom = CreateRooms(); // Initialize the rooms and start in the starting room
        }

        public void Start()
        {
            // Game loop to keep playing
            playing = true;

            Console.WriteLine("Hello " + player.Name + ", The game has started.");
            Console.WriteLine("You can: look, pickup, status, move, attack, or exit.");

            while (playing)
            {
                Console.WriteLine(); // Added empty line for spacing
                Console.WriteLine("What do you want to do?");
                string command = Console.ReadLine().ToLower();

                // Handle player commands
                if (command == "look")
                {
                    Console.WriteLine(currentRoom.GetDescription());
                }
                else if (command == "pickup")
                {
                    PickUpItemInRoom();
                }
                else if (command == "status")
                {
                    ShowPlayerStatus();
                }
                else if (command == "exit")
                {
                    Console.WriteLine("Goodbye!");
                    playing = false;
                }
                else if (command == "move")
                {
                    MovePlayer();
                }
                else if (command == "attack")
                {
                    AttackMonster();
                }
                else
                {
                    Console.WriteLine("Sorry, that isn't a valid command.");
                }
            }
        }

        // Display the player's current status
        private void ShowPlayerStatus()
        {
            Console.WriteLine("Your name is: " + player.Name);
            Console.WriteLine("You have " + player.Health + " health.");
            Console.WriteLine("You are carrying: " + player.InventoryContents());
        }

        // Handle item pickup from the room
        private void PickUpItemInRoom()
        {
            if (currentRoom.Items.Count > 0)
            {
                Console.WriteLine("Which item would you like to pick up?");
                for (int i = 0; i < currentRoom.Items.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {currentRoom.Items[i].Name}");
                }
                int choice = int.Parse(Console.ReadLine()) - 1;

                if (choice >= 0 && choice < currentRoom.Items.Count)
                {
                    string itemName = currentRoom.Items[choice].Name;
                    player.PickUpItem(itemName);
                    currentRoom.Items.RemoveAt(choice); // Remove item from room after pickup
                    Console.WriteLine($"{itemName} added to your inventory.");
                }
                else
                {
                    Console.WriteLine("Invalid item choice.");
                }
            }
            else
            {
                Console.WriteLine("There are no items in this room.");
            }
        }

        // Handle player movement between rooms (simplified)
        private void MovePlayer()
        {
            Console.WriteLine("Which direction would you like to move? (north, east, west, south)");
            string direction = Console.ReadLine().ToLower();

            if (direction == "north" && currentRoom.NorthRoom != null)
            {
                currentRoom = currentRoom.NorthRoom;
                Console.WriteLine($"You moved to the {currentRoom.GetDescription()}");
            }
            else if (direction == "east" && currentRoom.EastRoom != null)
            {
                currentRoom = currentRoom.EastRoom;
                Console.WriteLine($"You moved to the {currentRoom.GetDescription()}");
            }
            else if (direction == "west" && currentRoom.WestRoom != null)
            {
                currentRoom = currentRoom.WestRoom;
                Console.WriteLine($"You moved to the {currentRoom.GetDescription()}");
            }
            else if (direction == "south" && currentRoom.SouthRoom != null)
            {
                currentRoom = currentRoom.SouthRoom;
                Console.WriteLine($"You moved to the {currentRoom.GetDescription()}");
            }
            else
            {
                Console.WriteLine("You can't go that way.");
            }
        }

        // Handle attacking monsters in the room
        private void AttackMonster()
        {
            if (currentRoom.Monsters.Count > 0)
            {
                Console.WriteLine("Which monster would you like to attack?");
                for (int i = 0; i < currentRoom.Monsters.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {currentRoom.Monsters[i].Name} (Health: {currentRoom.Monsters[i].Health})");
                }
                int choice = int.Parse(Console.ReadLine()) - 1;

                if (choice >= 0 && choice < currentRoom.Monsters.Count)
                {
                    var monster = currentRoom.Monsters[choice];
                    player.Attack(monster);
                    Console.WriteLine($"{monster.Name} is attacked!");

                    if (monster.Health <= 0)
                    {
                        Console.WriteLine($"{monster.Name} has been defeated!");
                        currentRoom.Monsters.RemoveAt(choice); // Remove monster from the room
                        DropItemsAfterKill(monster);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid monster choice.");
                }
            }
            else
            {
                Console.WriteLine("There are no monsters in this room.");
            }
        }

        // Drop items when monsters are killed
        private void DropItemsAfterKill(Monster monster)
        {
            if (monster is Goblin)
            {
                currentRoom.Items.Add(new Potion("Healing Potion", 20));
                currentRoom.Items.Add(new Potion("Healing Potion", 20));
                Console.WriteLine("The goblin dropped 2 Healing Potions!");
            }
            else if (monster is Mage)
            {
                currentRoom.Items.Add(new Item("Key"));
                currentRoom.Items.Add(new Item("Shield"));
                Console.WriteLine("The mage dropped a Key and a Shield!");
            }
            else if (monster is Orc)
            {
                currentRoom.Items.Add(new Potion("Health Potion", 50));
                Console.WriteLine("The orc dropped a Health Potion!");
            }
        }

        // Create rooms and initialize the game map
        private Room CreateRooms()
        {
            // Simplified room creation with a basic example.
            Room startRoom = new Room("You are in a dark and damp room with suspicious stains. A torch lies on the floor.");
            Room swordRoom = new Room("A room with a sword on a pedestal.");
            Room goblinRoom = new Room("A dimly lit room with a goblin lurking.");
            Room lockedRoom = new Room("This room is locked and requires two keys to open.");
            Room mageRoom = new Room("A mage stands here, ready to fight.");
            Room orcRoom = new Room("A giant orc roars loudly in the corner.");

            // Room connections
            startRoom.NorthRoom = swordRoom;
            swordRoom.SouthRoom = startRoom;

            swordRoom.NorthRoom = goblinRoom;
            goblinRoom.SouthRoom = swordRoom;

            goblinRoom.NorthRoom = lockedRoom;
            lockedRoom.SouthRoom = goblinRoom;

            goblinRoom.EastRoom = mageRoom;
            mageRoom.WestRoom = goblinRoom;

            mageRoom.EastRoom = orcRoom;
            orcRoom.WestRoom = mageRoom;

            // Adding items to rooms
            startRoom.Items.Add(new Item("Torch"));
            swordRoom.Items.Add(new Weapon("Sword", 10));

            return startRoom;
        }
    }
}
