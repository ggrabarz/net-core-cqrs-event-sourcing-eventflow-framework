﻿using EventFlow.Snapshots;

namespace NetCoreEventFlow.Domain.Inventory
{
    [SnapshotVersion("InventoryItem", 1)]
    public class InventoryItemSnapshot : ISnapshot
    {
        public bool Activated { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public bool Inited { get; set; }
    }
}
