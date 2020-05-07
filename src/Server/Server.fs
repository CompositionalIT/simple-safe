open Giraffe
open Saturn
open Shared
open System.IO

let webApp = router {
    get "/api/init" (json { Value = 42 })
}

let app = application {
    url "http://localhost:8085"
    use_router webApp
    memory_cache
    use_static (Path.GetFullPath "../Client/public")
    use_json_serializer(Thoth.Json.Giraffe.ThothSerializer())
    use_gzip
}

run app