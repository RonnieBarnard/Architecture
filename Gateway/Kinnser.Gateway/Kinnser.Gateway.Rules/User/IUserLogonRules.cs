﻿/*
    File Header:
    ------------
    Author              Date            Comment
    ====================================================================================================================
    Ronnie Barnard      6/13/2017       Initial version
 */

using Kinnser.Gateway.Interfaces.Rules;

namespace Kinnser.Gateway.Rules.User
{
    /// <summary>
    /// Defines the set of rules that executes when a User logs onto the kinnser system
    /// </summary>
    /// <seealso cref="Kinnser.Gateway.Interfaces.Rules.IRuleSet" />
    public interface IUserLogonRules : IRuleSet
    {
    }
}