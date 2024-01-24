﻿namespace Soenneker.Utils.AutoBogus.Tests.Dtos.Complex;

public struct Price
{
    public Price(decimal amount, string units)
    {
        Amount = amount;
        Units = units;
    }

    public decimal Amount { get; }
    public string Units { get; }
}