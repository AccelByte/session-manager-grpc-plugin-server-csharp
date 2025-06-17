// Copyright (c) 2025 AccelByte Inc. All Rights Reserved.
// This is licensed software from AccelByte Inc, for limitations
// and restrictions contact your company contract manager.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

using Grpc.Core;
using AccelByte.Session.Manager;
using Google.Protobuf.WellKnownTypes;

namespace AccelByte.PluginArch.SessionManager.Demo.Server.Services
{
    public class SessionManagerDemoService : Session.Manager.SessionManager.SessionManagerBase
    {
        private readonly ILogger<SessionManagerDemoService> _Logger;

        public SessionManagerDemoService(ILogger<SessionManagerDemoService> logger)
        {
            _Logger = logger;
        }

        public override Task<SessionResponse> OnSessionCreated(SessionCreatedRequest request, ServerCallContext context)
        {
            var gameSession = request.Session;

            gameSession.Session.Attributes.Fields.Add("SAMPLE", Value.ForString("value from GRPC server"));

            return Task.FromResult(new SessionResponse()
            {
                Session = gameSession
            });
        }

        public override Task<Empty> OnSessionUpdated(SessionUpdatedRequest request, ServerCallContext context)
        {
            _Logger.LogInformation($"Old game session id: {request.SessionOld.Session.Id}");
            _Logger.LogInformation($"New game session id: {request.SessionNew.Session.Id}");

            return Task.FromResult(new Empty());
        }

        public override Task<Empty> OnSessionDeleted(SessionDeletedRequest request, ServerCallContext context)
        {
            _Logger.LogInformation($"Deleted session id: {request.Session.Session.Id}");

            return Task.FromResult(new Empty());
        }

        public override Task<PartyResponse> OnPartyCreated(PartyCreatedRequest request, ServerCallContext context)
        {
            var gameSession = request.Session;

            _Logger.LogInformation($"Party session id: {gameSession.Session.Id}");

            gameSession.Session.Attributes.Fields.Add("PARTY_SAMPLE", Value.ForString("party value from GRPC server"));

            return Task.FromResult(new PartyResponse()
            {
                Session = gameSession
            });
        }

        public override Task<Empty> OnPartyUpdated(PartyUpdatedRequest request, ServerCallContext context)
        {
            _Logger.LogInformation($"Old party session id: {request.SessionOld.Session.Id}");
            _Logger.LogInformation($"New party session id: {request.SessionNew.Session.Id}");

            return Task.FromResult(new Empty());
        }

        public override Task<Empty> OnPartyDeleted(PartyDeletedRequest request, ServerCallContext context)
        {
            _Logger.LogInformation($"Deleted party session id: {request.Session.Session.Id}");

            return Task.FromResult(new Empty());
        }
    }
}
