// package: personalBlog
// file: auth.proto

var auth_pb = require("./auth_pb");
var grpc = require("@improbable-eng/grpc-web").grpc;

var Authorization = (function () {
  function Authorization() {}
  Authorization.serviceName = "personalBlog.Authorization";
  return Authorization;
}());

Authorization.login = {
  methodName: "login",
  service: Authorization,
  requestStream: false,
  responseStream: false,
  requestType: auth_pb.LoginForm,
  responseType: auth_pb.TokenResponse
};

Authorization.register = {
  methodName: "register",
  service: Authorization,
  requestStream: false,
  responseStream: false,
  requestType: auth_pb.RegisterForm,
  responseType: auth_pb.TokenResponse
};

exports.Authorization = Authorization;

function AuthorizationClient(serviceHost, options) {
  this.serviceHost = serviceHost;
  this.options = options || {};
}

AuthorizationClient.prototype.login = function login(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(Authorization.login, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onEnd: function (response) {
      if (callback) {
        if (response.status !== grpc.Code.OK) {
          var err = new Error(response.statusMessage);
          err.code = response.status;
          err.metadata = response.trailers;
          callback(err, null);
        } else {
          callback(null, response.message);
        }
      }
    }
  });
  return {
    cancel: function () {
      callback = null;
      client.close();
    }
  };
};

AuthorizationClient.prototype.register = function register(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(Authorization.register, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onEnd: function (response) {
      if (callback) {
        if (response.status !== grpc.Code.OK) {
          var err = new Error(response.statusMessage);
          err.code = response.status;
          err.metadata = response.trailers;
          callback(err, null);
        } else {
          callback(null, response.message);
        }
      }
    }
  });
  return {
    cancel: function () {
      callback = null;
      client.close();
    }
  };
};

exports.AuthorizationClient = AuthorizationClient;

