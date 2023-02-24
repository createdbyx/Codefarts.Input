using Codefarts.Input.Interfaces;
using Codefarts.Input.Models;

namespace Codefarts.Input;

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

/// <summary>
/// Provides a reader for reading xml based binding data.
/// </summary>
public class BindingsReader
{
    /// <summary>
    /// Reads binding data from a xml file.
    /// </summary>
    /// <param name="file">
    /// The xml file to be read.
    /// </param>
    /// <returns>
    /// Returns an array of <see cref="BindingData"/> types.
    /// </returns>
    public static BindingData[] Read(string file, Func<string, IInputSource> getInputSource)
    {
        // System.Diagnostics.Debugger.Launch();
        var fileStream = File.OpenRead(file);
        using (fileStream)
        {
            return Read(fileStream, getInputSource);
        }
    }

    /// <summary>
    /// Reads binding data from a xml stream.
    /// </summary>
    /// <param name="stream">
    /// The xml stream to be read.
    /// </param>
    /// <returns>
    /// Returns an array of <see cref="BindingData"/> types.
    /// </returns>
    public static BindingData[] Read(Stream stream, Func<string, IInputSource> getInputSource)
    {
        var doc = XDocument.Load(stream);

        var items = new List<BindingData>();

        if (doc.Root == null || doc.Root.Name.LocalName != "bindings")
        {
            return items.ToArray();
        }

        var bindings = doc.Root.Elements("bind");
        var att = new Dictionary<string, string>();
        foreach (var binding in bindings)
        {
            // assign default values
            att.Add("name", string.Empty);
            att.Add("player", 1.ToString());
            att.Add("inputsource", string.Empty);
            att.Add("source", string.Empty);

            // grab all relevant attributes
            var keys = new string[att.Count];
            att.Keys.CopyTo(keys, 0);
            foreach (var a in keys)
            {
                var tmp = binding.Attribute(a);
                if (tmp != null)
                {
                    att[a] = tmp.Value;
                }
            }

            // determine if the action, inputsource and source values were specified
            if (string.IsNullOrWhiteSpace(att["name"]) ||
                string.IsNullOrWhiteSpace(att["inputsource"]) ||
                string.IsNullOrWhiteSpace(att["source"]))
            {
                continue;
            }


            // try get player index
            int playerIndex = 0;
            if (!string.IsNullOrWhiteSpace(att["player"]) && !int.TryParse(att["player"], out playerIndex))
            {
                throw new Exception("A Player value exists but could not be parsed.");
            }

            var inputSource = getInputSource?.Invoke(att["inputsource"]);
            items.Add(new BindingData(att["name"], inputSource, att["source"], playerIndex));

            att.Clear();
        }

        // return the reference the Doc variable
        return items.ToArray();
    }
}