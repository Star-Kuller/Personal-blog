// package: personalBlog
// file: articles.proto

var articles_pb = require("./articles_pb");
var grpc = require("@improbable-eng/grpc-web").grpc;

var Articles = (function () {
  function Articles() {}
  Articles.serviceName = "personalBlog.Articles";
  return Articles;
}());

Articles.getPagedList = {
  methodName: "getPagedList",
  service: Articles,
  requestStream: false,
  responseStream: false,
  requestType: articles_pb.PagedListQuery,
  responseType: articles_pb.PagedListResult
};

Articles.create = {
  methodName: "create",
  service: Articles,
  requestStream: false,
  responseStream: false,
  requestType: articles_pb.ArticleCreate,
  responseType: articles_pb.IdMassage
};

Articles.edit = {
  methodName: "edit",
  service: Articles,
  requestStream: false,
  responseStream: false,
  requestType: articles_pb.ArticleEdit,
  responseType: articles_pb.Empty
};

Articles.delete = {
  methodName: "delete",
  service: Articles,
  requestStream: false,
  responseStream: false,
  requestType: articles_pb.IdMassage,
  responseType: articles_pb.Empty
};

Articles.view = {
  methodName: "view",
  service: Articles,
  requestStream: false,
  responseStream: false,
  requestType: articles_pb.IdMassage,
  responseType: articles_pb.ArticleInfo
};

Articles.uploadFile = {
  methodName: "uploadFile",
  service: Articles,
  requestStream: true,
  responseStream: false,
  requestType: articles_pb.FileChunk,
  responseType: articles_pb.Empty
};

Articles.removeFile = {
  methodName: "removeFile",
  service: Articles,
  requestStream: false,
  responseStream: false,
  requestType: articles_pb.RemoveFile,
  responseType: articles_pb.Empty
};

Articles.publish = {
  methodName: "publish",
  service: Articles,
  requestStream: false,
  responseStream: false,
  requestType: articles_pb.IdMassage,
  responseType: articles_pb.Empty
};

Articles.like = {
  methodName: "like",
  service: Articles,
  requestStream: false,
  responseStream: false,
  requestType: articles_pb.IdMassage,
  responseType: articles_pb.Empty
};

Articles.unlike = {
  methodName: "unlike",
  service: Articles,
  requestStream: false,
  responseStream: false,
  requestType: articles_pb.IdMassage,
  responseType: articles_pb.Empty
};

exports.Articles = Articles;

function ArticlesClient(serviceHost, options) {
  this.serviceHost = serviceHost;
  this.options = options || {};
}

ArticlesClient.prototype.getPagedList = function getPagedList(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(Articles.getPagedList, {
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

ArticlesClient.prototype.create = function create(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(Articles.create, {
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

ArticlesClient.prototype.edit = function edit(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(Articles.edit, {
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

ArticlesClient.prototype.delete = function pb_delete(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(Articles.delete, {
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

ArticlesClient.prototype.view = function view(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(Articles.view, {
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

ArticlesClient.prototype.uploadFile = function uploadFile(metadata) {
  var listeners = {
    end: [],
    status: []
  };
  var client = grpc.client(Articles.uploadFile, {
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport
  });
  client.onEnd(function (status, statusMessage, trailers) {
    listeners.status.forEach(function (handler) {
      handler({ code: status, details: statusMessage, metadata: trailers });
    });
    listeners.end.forEach(function (handler) {
      handler({ code: status, details: statusMessage, metadata: trailers });
    });
    listeners = null;
  });
  return {
    on: function (type, handler) {
      listeners[type].push(handler);
      return this;
    },
    write: function (requestMessage) {
      if (!client.started) {
        client.start(metadata);
      }
      client.send(requestMessage);
      return this;
    },
    end: function () {
      client.finishSend();
    },
    cancel: function () {
      listeners = null;
      client.close();
    }
  };
};

ArticlesClient.prototype.removeFile = function removeFile(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(Articles.removeFile, {
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

ArticlesClient.prototype.publish = function publish(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(Articles.publish, {
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

ArticlesClient.prototype.like = function like(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(Articles.like, {
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

ArticlesClient.prototype.unlike = function unlike(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(Articles.unlike, {
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

exports.ArticlesClient = ArticlesClient;

