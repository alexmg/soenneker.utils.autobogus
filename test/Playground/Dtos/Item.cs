using System;
using System.Collections.Generic;

namespace Soenneker.Utils.AutoBogus.Tests.Playground.Dtos;

public sealed class Item
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Product<int> ProductInt { get; set; }
    public Product<string> ProductString { get; set; }
    public decimal Price { get; set; }
    public uint Quantity { get; set; }
    public Units Units { get; set; }
    public ItemStatus Status { get; set; }
    public Uri InfoLink { get; set; }
    public ICollection<string> Comments { get; set; }
    public User ProcessedBy { get; set; }
    public string SupplierEmail { get; set; }
    public ITimestamp Timestamp { get; set; }
    public ISet<string> Amendments { get; set; }
}