//-----------------------------------------------------------------------
// <copyright file="Calculator.cs" company="Medavie Inc">
// Copyright (c) Medavie Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace MyClassLibrary
{
    using System;

    /// <summary>
    /// The rudimentary calculator implementation.
    /// </summary>
    public class Calculator : ICalculator
    {
        /// <summary>
        /// Adds two integers and returns the result.
        /// </summary>
        /// <param name="x">The first integer.</param>
        /// <param name="y">The second integer.</param>
        /// <returns>The result of the operation to add the integers.</returns>
        public int Add(int x, int y)
        {
            return x + y;
        }
    }
}