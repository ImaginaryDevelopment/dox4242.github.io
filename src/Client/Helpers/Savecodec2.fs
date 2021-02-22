module Helpers.Imports.Savecodec2

open Fable.Core
open Fable.Core.JsInterop

[<AbstractClass>]
type ISaveHandler =
    abstract member Decode: string -> Shared.DecodedSave

[<Emit("window.SaveHandler")>]
let getSaveHandler : ISaveHandler = jsNative