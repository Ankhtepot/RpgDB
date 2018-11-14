﻿using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace RpgDB
{
    public class AmmunitionDatabase : Database
    {
        public string[] ammunitionCategories = { "ammunition" };

        public static List<IRpgDBEntry> AmmunitionList = new List<IRpgDBEntry>();
        public static List<Ammunition> All = new List<Ammunition>();

        public void Awake()
        {
            if (All.Count < 1)
            {
                LoadData(ammunitionCategories, AmmunitionList);
                All = AmmunitionList.Cast<Ammunition>().ToList();
            }
        }

        // Add Object to Ammunition List
        public override void AddObject(JToken item, List<IRpgDBEntry> list, string category)
        {
            Ammunition ammunition = new Ammunition();
            ammunition.ConvertObject(item, category);
            list.Add(ammunition);
        }

        public Ammunition GetByName(string text)
        {
            return All.Find(x => x.Name.Equals(text));
        }
    }
}
