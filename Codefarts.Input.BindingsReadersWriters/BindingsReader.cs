// <copyright file="BindingsReader.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

using System.Collections.ObjectModel;
using Codefarts.Input.Interfaces;
using Codefarts.Input.Models;

namespace Codefarts.Input;

using System;
using System.IO;
using System.Xml.Linq;

/// <summary>
/// Provides a reader for reading xml based binding data.
/// </summary>
public class BindingsReader
{
    private const string NameKey = "name";
    private const string InputSourceKey = "inputsource";
    private const string SourceKey = "source";
    private const string PlayerKey = "player";
    private const string BindingsKey = "bindings";
    private const string BindKey = "bind";

    /// <summary>
    /// Initializes a new instance of the <see cref="BindingsReader"/> class.
    /// </summary>
    /// <param name="getInputSource">Sets the callback function for retrieving input sources.</param>
    public BindingsReader(Func<string, IInputSource> getInputSource)
    {
        this.GetInputSource = getInputSource ?? throw new ArgumentNullException(nameof(getInputSource));
        this.Bindings = new ObservableCollection<BindingData>();
    }

    private Func<string, IInputSource> GetInputSource { get; }

    public ObservableCollection<BindingData> Bindings { get; }

    /// <summary>
    /// Reads binding data from a xml file.
    /// </summary>
    /// <param name="file">
    /// The xml file to be read.
    /// </param>
    public void Read(string file)
    {
        var fileStream = File.OpenRead(file);
        using (fileStream)
        {
            this.Read(fileStream);
        }
    }

    /// <summary>
    /// Reads binding data from a xml stream.
    /// </summary>
    /// <param name="stream">
    /// The xml stream to be read.
    /// </param>
    public void Read(Stream stream)
    {
        var doc = XDocument.Load(stream);

        if (doc.Root == null || doc.Root.Name.LocalName != BindingsKey)
        {
            throw new BindingsIOException($"Root node is not '{BindingsKey}'.");
        }

        var bindingElements = doc.Root.Elements(BindKey);
        foreach (var binding in bindingElements)
        {
            // assign default values
            var nameAttribute = binding.Attribute(NameKey);
            var playerAttribute = binding.Attribute(PlayerKey);
            var inputSourceAttribute = binding.Attribute(InputSourceKey);
            var sourceAttribute = binding.Attribute(SourceKey);

            // determine if the action, inputsource and source values were specified
            this.ValidateValueExists(NameKey, nameAttribute);
            this.ValidateValueExists(InputSourceKey, inputSourceAttribute);
            this.ValidateValueExists(SourceKey, sourceAttribute);

            // try get player index
            int playerIndex = this.DefaultPlayerIndex;
            if (playerAttribute != null && !string.IsNullOrWhiteSpace(playerAttribute.Value))
            {
                int.TryParse(playerAttribute.Value.Trim(), out playerIndex);
            }

            // try to fetch input source
            IInputSource inputSourceReference;
            try
            {
                inputSourceReference = this.GetInputSource(inputSourceAttribute.Value);
                if (inputSourceReference == null)
                {
                    throw new BindingsIOException($"No input source reference with name '{inputSourceAttribute.Value}' could be located.");
                }
            }
            catch (Exception ex)
            {
                throw new BindingsIOException($"Exception thrown while trying to fetch input source reference.", ex);
            }

            // add the bindings
            this.Bindings.Add(new BindingData(nameAttribute.Value, inputSourceReference, sourceAttribute.Value, playerIndex));
        }
    }

    /// <summary>
    /// Gets or sets the default player index to use when reading bindings and there is no player index specified.
    /// </summary>
    public int DefaultPlayerIndex { get; set; }

    private void ValidateValueExists(string name, XAttribute? attribute)
    {
        if (attribute == null)
        {
            throw new BindingsIOException($"'{name}' attribute not found on a binding.");
        }

        if (string.IsNullOrWhiteSpace(attribute.Value))
        {
            throw new BindingsIOException($"'{name}' attribute has no value.");
        }
    }
}