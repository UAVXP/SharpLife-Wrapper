/***
*
*	Copyright (c) 1996-2001, Valve LLC. All rights reserved.
*	
*	This product contains software technology licensed from Id 
*	Software, Inc. ("Id Technology").  Id Technology (c) 1996 Id Software, Inc. 
*	All Rights Reserved.
*
*   This source code contains proprietary and confidential information of
*   Valve LLC and its suppliers.  Access to this code is restricted to
*   persons who have executed a written SDK license with Valve.  Any access,
*   use or distribution of this code by or to any unlicensed person is illegal.
*
****/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Engine.API.Implementations
{
    internal sealed class Command : ICommand
    {
        public string Name { get; }

        public int Count => Arguments.Count;

        public string this[int index] => Arguments[index];

        private List<string> Arguments { get; }

        IReadOnlyList<string> ICommand.Arguments => Arguments;

        /// <summary>
        /// Creates a new command object
        /// </summary>
        /// <param name="allArguments">List of all arguments, with the command name as the first argument</param>
        internal Command(List<string> allArguments)
        {
            if (allArguments == null)
            {
                throw new ArgumentNullException(nameof(allArguments));
            }

            if (allArguments.Count == 0)
            {
                throw new ArgumentException("The argument list must at least contain the command name", nameof(allArguments));
            }

            Name = allArguments[0];

            Arguments = allArguments.GetRange(1, allArguments.Count - 1);
        }

        public string ArgumentsAsString(bool addQuotes = true)
        {
            var builder = new StringBuilder();

            var first = true;

            foreach(var arg in Arguments)
            {
                if (!first)
                {
                    builder.Append(' ');
                }

                var formattedArg = addQuotes && arg.Any(char.IsWhiteSpace) ? $"\"{arg}\"" : arg;

                builder.Append(formattedArg);

                first = false;
            }

            return builder.ToString();
        }
    }
}
