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

using System.Text.Json;
using Finos.Fdc3;
using MorganStanley.ComposeUI.Fdc3.DesktopAgent.Contracts;
using MorganStanley.ComposeUI.Fdc3.DesktopAgent.Converters;
using MorganStanley.ComposeUI.Fdc3.DesktopAgent.Infrastructure.Internal;
using AppMetadata = MorganStanley.ComposeUI.Fdc3.DesktopAgent.Protocol.AppMetadata;

namespace MorganStanley.ComposeUI.Fdc3.DesktopAgent.Tests.Infrastructure.Internal;

public class ResolverUiMessageRouterCommunicatorTests
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        Converters = { new AppMetadataJsonConverter() }
    };

    [Fact]
    public async Task SendResolverUIRequest_will_return_null()
    {
        var messageRouterMock = new Mock<IMessageRouter>();
        messageRouterMock.Setup(
                _ => _.InvokeAsync(
                    It.IsAny<string>(),
                    It.IsAny<MessageBuffer>(),
                    It.IsAny<InvokeOptions>(),
                    It.IsAny<CancellationToken>()))
            .Returns(null);

        var resolverUIMessageRouterCommunicator = new ResolverUiMessageRouterCommunicator(messageRouterMock.Object);

        var response = await resolverUIMessageRouterCommunicator.SendResolverUIRequest(It.IsAny<IEnumerable<IAppMetadata>>());

        response.Should().BeNull();
    }

    [Fact]
    public async Task SendResolverUIRequest_will_return_response()
    {
        var messageRouterMock = new Mock<IMessageRouter>();
        messageRouterMock.Setup(
                _ => _.InvokeAsync(
                    It.IsAny<string>(),
                    It.IsAny<MessageBuffer>(),
                    It.IsAny<InvokeOptions>(),
                    It.IsAny<CancellationToken>()))
            .Returns(ValueTask.FromResult<MessageBuffer?>(
                MessageBuffer.Factory.CreateJson(new ResolverUIResponse()
                {
                    AppMetadata = new AppMetadata(){ AppId = "testAppId" }
                }, _jsonSerializerOptions)));

        var resolverUIMessageRouterCommunicator = new ResolverUiMessageRouterCommunicator(messageRouterMock.Object);

        var response = await resolverUIMessageRouterCommunicator.SendResolverUIRequest(It.IsAny<IEnumerable<IAppMetadata>>());

        response.Should().NotBeNull();
        response!.AppMetadata.Should().NotBeNull();
        response.AppMetadata!.AppId.Should().Be("testAppId");
    }
}