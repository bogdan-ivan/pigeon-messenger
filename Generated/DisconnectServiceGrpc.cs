// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: DisconnectService.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace Generated {
  public static partial class DisconnectService
  {
    static readonly string __ServiceName = "DisconnectService";

    static readonly grpc::Marshaller<global::Generated.DisconnectRequest> __Marshaller_DisconnectRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Generated.DisconnectRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Google.Protobuf.WellKnownTypes.Empty> __Marshaller_google_protobuf_Empty = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Google.Protobuf.WellKnownTypes.Empty.Parser.ParseFrom);

    static readonly grpc::Method<global::Generated.DisconnectRequest, global::Google.Protobuf.WellKnownTypes.Empty> __Method_Disconnect = new grpc::Method<global::Generated.DisconnectRequest, global::Google.Protobuf.WellKnownTypes.Empty>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Disconnect",
        __Marshaller_DisconnectRequest,
        __Marshaller_google_protobuf_Empty);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Generated.DisconnectServiceReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of DisconnectService</summary>
    [grpc::BindServiceMethod(typeof(DisconnectService), "BindService")]
    public abstract partial class DisconnectServiceBase
    {
      public virtual global::System.Threading.Tasks.Task<global::Google.Protobuf.WellKnownTypes.Empty> Disconnect(global::Generated.DisconnectRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for DisconnectService</summary>
    public partial class DisconnectServiceClient : grpc::ClientBase<DisconnectServiceClient>
    {
      /// <summary>Creates a new client for DisconnectService</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public DisconnectServiceClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for DisconnectService that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public DisconnectServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected DisconnectServiceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected DisconnectServiceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      public virtual global::Google.Protobuf.WellKnownTypes.Empty Disconnect(global::Generated.DisconnectRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Disconnect(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Google.Protobuf.WellKnownTypes.Empty Disconnect(global::Generated.DisconnectRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Disconnect, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Google.Protobuf.WellKnownTypes.Empty> DisconnectAsync(global::Generated.DisconnectRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return DisconnectAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Google.Protobuf.WellKnownTypes.Empty> DisconnectAsync(global::Generated.DisconnectRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Disconnect, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override DisconnectServiceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new DisconnectServiceClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(DisconnectServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_Disconnect, serviceImpl.Disconnect).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, DisconnectServiceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_Disconnect, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Generated.DisconnectRequest, global::Google.Protobuf.WellKnownTypes.Empty>(serviceImpl.Disconnect));
    }

  }
}
#endregion
