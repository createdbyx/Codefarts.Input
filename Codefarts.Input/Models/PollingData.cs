// <copyright file="PollingData.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

namespace Codefarts.Input.Models;

public class PollingData : EventArgs
{
    public PollingData(string inputSource, string source, float value)
    {
        this.InputSource = inputSource;
        this.Source = source;
        this.Value = value;                 
    }

    /// <summary>
    /// Gets or sets the inputSource name or id.
    /// </summary>
    public string InputSource { get; protected set; }

    /// <summary>
    /// Gets or sets the source axis or button on the inputSource.
    /// </summary>
    /// <remarks>This could also point to a inputSource state such as a led light or gyroscope etc.</remarks>
    public string Source { get; protected set; }

    /// <summary>
    /// Gets or sets the value return from the inputSource.
    /// </summary>
    public virtual float Value { get; set; }
}