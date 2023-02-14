using Codefarts.Input.MonoGameSources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Codefarts.Input.Tests;

[TestClass]
public class KeyboardSourcePerfTests
{
    [TestMethod]
    public void KeyboardPolling()
    {
        var kbSource = new KeyboardSource();

        for (var i = 0; i < 100000; i++)
        {
            var results = kbSource.Poll();
        }
    }
}