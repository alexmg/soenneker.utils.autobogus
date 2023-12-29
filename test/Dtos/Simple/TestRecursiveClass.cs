﻿using System.Collections.Generic;

namespace Soenneker.Utils.AutoBogus.Tests.Dtos.Simple;

public sealed class TestRecursiveClass
{
    public TestRecursiveClass Child { get; set; }
    public IEnumerable<TestRecursiveClass> Children { get; set; }
    public TestRecursiveSubClass Sub { get; set; }
}