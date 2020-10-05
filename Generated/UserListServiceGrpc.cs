// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: UserListService.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace Generated {
  public static partial class UserListService
  {
    static readonly string __ServiceName = "UserListService";

    static readonly grpc::Marshaller<global::Generated.User> __Marshaller_User = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Generated.User.Parser.ParseFrom);

    static readonly grpc::Method<global::Generated.User, global::Generated.User> __Method_GetUsers = new grpc::Method<global::Generated.User, global::Generated.User>(
        grpc::MethodType.DuplexStreaming,
        __ServiceName,
        "GetUsers",
        __Marshaller_User,
        __Marshaller_User);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Generated.UserListServiceReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of UserListService</summary>
    [grpc::BindServiceMethod(typeof(UserListService), "BindService")]
    public abstract partial class UserListServiceBase
    {
      public virtual global::System.Threading.Tasks.Task GetUsers(grpc::IAsyncStreamReader<global::Generated.User> requestStream, grpc::IServerStreamWriter<global::Generated.User> responseStream, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for UserListService</summary>
    public partial class UserListServiceClient : grpc::ClientBase<UserListServiceClient>
    {
      /// <summary>Creates a new client for UserListService</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public UserListServiceClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for UserListService that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public UserListServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected UserListServiceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected UserListServiceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      public virtual grpc::AsyncDuplexStreamingCall<global::Generated.User, global::Generated.User> GetUsers(grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetUsers(new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncDuplexStreamingCall<global::Generated.User, global::Generated.User> GetUsers(grpc::CallOptions options)
      {
        return CallInvoker.AsyncDuplexStreamingCall(__Method_GetUsers, null, options);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override UserListServiceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new UserListServiceClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(UserListServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_GetUsers, serviceImpl.GetUsers).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, UserListServiceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_GetUsers, serviceImpl == null ? null : new grpc::DuplexStreamingServerMethod<global::Generated.User, global::Generated.User>(serviceImpl.GetUsers));
    }

  }
}
#endregion
