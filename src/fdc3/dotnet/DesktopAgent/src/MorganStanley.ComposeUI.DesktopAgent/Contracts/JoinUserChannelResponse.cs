﻿/*
 * Morgan Stanley makes this available to you under the Apache License,
 * Version 2.0 (the "License"). You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0.
 *
 * See the NOTICE file distributed with this work for additional information
 * regarding copyright ownership. Unless required by applicable law or agreed
 * to in writing, software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express
 * or implied. See the License for the specific language governing permissions
 * and limitations under the License.
 */

using MorganStanley.ComposeUI.Fdc3.DesktopAgent.Protocol;

namespace MorganStanley.ComposeUI.Fdc3.DesktopAgent.Contracts;

internal sealed class JoinUserChannelResponse
{
    /// <summary>
    /// Error while executing the JoinUserChannel call.
    /// </summary>
    public string? Error { get; set; }

    /// <summary>
    /// Indicating if the request was successful.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// DisplayMetadata of the ChannelItem.
    /// </summary>
    public DisplayMetadata? DisplayMetadata { get; set; }

    public static JoinUserChannelResponse Joined(DisplayMetadata? displayMetadata = null) => new() { Success = true, DisplayMetadata = displayMetadata };
    public static JoinUserChannelResponse Failed(string error) => new() { Success = false, Error = error };
}