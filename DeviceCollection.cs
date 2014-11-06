/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/

namespace Codefarts.Input
{
    using System.Collections.Generic;

    using Codefarts.Input.Interfaces;

    /// <summary>
    /// Provides a collection type for <see cref="IDevice"/> implementations.
    /// </summary>
    public class DeviceCollection : List<IDevice>
    {
    }
}