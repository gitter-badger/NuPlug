﻿using System;
using System.Collections.Generic;

namespace NuPlug
{
    public interface IPackageContainer<out TItem> where TItem : class
    {
        IEnumerable<TItem> Items { get; }
        void Update();
        event EventHandler Updated;
    }
}