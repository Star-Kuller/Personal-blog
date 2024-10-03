1. ASP.NET core automatically generates .proto files from this folder. You just need to add it to `Personal blog.csproj`
2. For Angular you need to generate it manually using this command:
```
protoc   --plugin="protoc-gen-ts=C:\MyProjects\Personal blog\Frontend\node_modules\.bin\protoc-gen-ts.cmd"  --js_out="import_style=commonjs,binary:../../frontend/src/generated"   --ts_out="service=grpc-web:../../frontend/src/generated" greet.proto
```
