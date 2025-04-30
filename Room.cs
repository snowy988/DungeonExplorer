namespace DungeonExplorer
{
    public class Room
    {
        private string description;
        private string item;

        //constructor - description and item
        public Room(string description, string item = null)
        {
            this.description = description;
            this.item = item;
        }

        public string GetDescription()
        {
            return description;
        }
        //returns item to return
        public string GetItem()
        {
            return item;
        }

        //removes item from room after pickup
        public void RemoveItem()
        {
            item = null;
        }

        public bool HasItem()
        {
            return item != null;
        }
    }
}