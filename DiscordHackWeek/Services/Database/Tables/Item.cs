﻿using System.Collections;
using System.Collections.Generic;
using DiscordHackWeek.Entities;

namespace DiscordHackWeek.Services.Database.Tables
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Unique { get; set; }
        public ItemType ItemType { get; set; }

        public int HealthIncrease { get; set; }
        public int DamageIncrease { get; set; }
        public int CritIncrease { get; set; }
        public ICollection<Inventory> Users { get; set; }
    }
}