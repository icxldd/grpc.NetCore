﻿using Grpc.Core;
using GrpcGreeter;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grpcServer.Servers
{
    [Authorize]
    public class GreeterService : Greeter.GreeterBase
    {
        public override Task<HelloReply> SayHello(
            HelloRequest request, ServerCallContext context)
        {
            var httpContext = context.GetHttpContext();
            var clientCertificate = httpContext.Connection.ClientCertificate;

            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name + " from "
            });
        }
        public override Task<IcxlCallReply> IcxlCall(
            IcxlCallRequest request, ServerCallContext context)
        {
            var httpContext = context.GetHttpContext();
            var clientCertificate = httpContext.Connection.ClientCertificate;

            return Task.FromResult(new IcxlCallReply
            {
                Message = "Hello " + request.Name + " from "
            });
        }


    }
}
