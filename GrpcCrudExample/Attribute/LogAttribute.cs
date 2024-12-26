using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GrpcCrudExample.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class LogAttribute : Attribute
    {
        public async Task LogMethodAsync(ILogger logger, Func<Task> methodInvocation, object request)
        {
            logger.LogInformation("Method {MethodName} called with input: {@Request}",
                new StackTrace().GetFrame(1).GetMethod().Name, request);

            try
            {
                await methodInvocation();
            }
            catch (RpcException ex)
            {
                logger.LogError("Error in method {MethodName}: {Message}",
                    new StackTrace().GetFrame(1).GetMethod().Name, ex.Message);
                throw;
            }
        }
    }
}