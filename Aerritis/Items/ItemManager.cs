using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerritis.Items
{
    /// <summary>
    /// this class loads all Item objects in one spot
    /// </summary>
    public class ItemManager
    {
        public List<Item> itemList;
        public ItemManager()
        {
            itemList = new List<Item>();
            itemList.Add(new ShortSword());
        }
    }
}