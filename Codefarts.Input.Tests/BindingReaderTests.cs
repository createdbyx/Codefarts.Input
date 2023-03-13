using System;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using Codefarts.Input.Interfaces;
using Codefarts.Input.MonoGameSources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Codefarts.Input.Tests;

[TestClass]
public class BindingReaderTests
{
    private string badRootPath;
    private string testBindings;

    [TestInitialize]
    public void TestInit()
    {
        badRootPath = Path.Combine(Environment.CurrentDirectory, "TestData", "BadRootBindings.xml");
        testBindings = Path.Combine(Environment.CurrentDirectory, "TestData", "TestBindings.xml");
    }

    [TestCleanup]
    public void TestCleanup()
    {
    }

    [TestMethod]
    public void NullParams()
    {
        Assert.ThrowsException<ArgumentNullException>(() =>
        {
            var reader = new BindingsReader(null);
        });
    }

    [TestMethod]
    public void BadRoot()
    {
        Assert.ThrowsException<BindingsIOException>(() =>
        {
            var cb = new Func<string, IInputSource>(_ => null);
            var reader = new BindingsReader(cb);
            reader.Read(this.badRootPath);
        });
    }

    [TestMethod]
    public void ValidBindings()
    {
        var kbSource = new KeyboardSource();
        var cb = new Func<string, IInputSource>(_ => kbSource);
        var reader = new BindingsReader(cb);
        reader.Read(this.testBindings);
        Assert.IsTrue(reader.Bindings.Count > 0);
    }
    
    [TestMethod]
    public void MissingPlayer()
    {
        var kbSource = new KeyboardSource();
        var cb = new Func<string, IInputSource>(_ => kbSource);
        var reader = new BindingsReader(cb);
        reader.DefaultPlayerIndex = -1;
        reader.Read(this.testBindings);
        Assert.IsTrue(reader.Bindings.Count > 0);
        var missingPlayer = reader.Bindings.FirstOrDefault(x => x.Player == -1);
        Assert.IsNotNull(missingPlayer);
    }
}