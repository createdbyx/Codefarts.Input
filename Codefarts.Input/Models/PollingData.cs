// <copyright file="PollingData.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

namespace Codefarts.Input.Models;

public class PollingData
{
    public PollingData(string inputSource, string source, float value ,float min,float max)
    {
        this.InputSource = inputSource;
        this.Source = source;
        this.Value = value;
        this.DataType = DataType.Other;
    }

    public PollingData(string inputSource, string source, float value, float min,float max, DataType dataType)
    {
        this.InputSource = inputSource;
        this.Source = source;
        this.Value = value;
        this.DataType = dataType;
    }

    /// <summary>
    /// Gets or sets the inputSource name or id.
    /// </summary>
    public string InputSource { get; }

    /// <summary>
    /// Gets or sets the source axis or button on the inputSource.
    /// </summary>
    /// <remarks>This could also point to a inputSource state such as a led light or gyroscope etc.</remarks>
    public string Source { get; }

    /// <summary>
    /// Gets or sets the value return from the inputSource.
    /// </summary>
    public float Value { get; set; }

    public DataType DataType { get; }

    public float Minimum { get;  }
    public float Maximum { get;  }
}